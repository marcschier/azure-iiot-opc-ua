// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa.Edge.Supervisor {
    using Microsoft.Azure.IIoT.OpcUa.Edge.Publisher;
    using Microsoft.Azure.IIoT.OpcUa.Twin;
    using Microsoft.Azure.IIoT.OpcUa.Protocol;
    using Microsoft.Azure.IIoT.OpcUa.Protocol.Stack;
    using Microsoft.Azure.IIoT.Diagnostics;
    using Microsoft.Azure.IIoT.Hub;
    using Microsoft.Azure.IIoT.Edge;
    using Microsoft.Azure.IIoT.Edge.Hub;
    using Microsoft.Azure.IIoT.Edge.Services;
    using Autofac;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Threading;
    using Microsoft.Azure.IIoT.Utils;
    using Microsoft.Azure.IIoT.Exceptions;

    /// <summary>
    /// Twin supervisor service
    /// </summary>
    public class OpcUaSupervisorServices : IOpcUaSupervisorServices, IDisposable {

        /// <summary>
        /// Create twin supervisor creating and managing twin instances
        /// </summary>
        public OpcUaSupervisorServices(IOpcUaClient client, IEdgeConfig config,
            IEventEmitter events, Action<ContainerBuilder> inject, ILogger logger) {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _events = events ?? throw new ArgumentNullException(nameof(events));

            _container = CreateTwinContainer(client, logger, inject);
        }

        /// <summary>
        /// Start twin
        /// </summary>
        /// <param name="secret"></param>
        /// <returns></returns>
        public async Task StartTwinAsync(string id, string secret) {
            ILifetimeScope twinScope;
            try {
                await _lock.WaitAsync();
                if (_twinScopes.ContainsKey(id)) {
                    _logger.Debug($"{id} twin already running.", () => { });
                    return;
                }
                _logger.Debug($"{id} twin starting...", () => { });

                // Create twin scoped component context with twin host
                twinScope = _container.BeginLifetimeScope(builder => {
                    // Register twin scope level configuration...
                    var config = new TwinConfig(_config, id, secret);
                    builder.RegisterInstance(config)
                        .AsImplementedInterfaces().SingleInstance();
                });
                // host is disposed when twin scope is disposed...
                _twinScopes.Add(id, twinScope);
            }
            finally {
                _lock.Release();
            }

            try {
                var host = twinScope.Resolve<IEdgeHost>();
                await host.StartAsync("twin", _events.SiteId);
                _logger.Info($"{id} twin started.", () => { });
            }
            catch (Exception ex) {
                try {
                    await _lock.WaitAsync();
                    _twinScopes.Remove(id);
                }
                finally {
                    _lock.Release();
                }
                twinScope.Dispose();
                _logger.Error($"{id} twin failed to start...", () => ex);
                throw ex;
            }
        }

        /// <summary>
        /// Stop twin
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task StopTwinAsync(string id) {
            ILifetimeScope twinScope;
            try {
                await _lock.WaitAsync();
                if (!_twinScopes.TryGetValue(id, out twinScope)) {
                    _logger.Debug($"{id} twin not running.", () => { });
                    return;
                }
                _twinScopes.Remove(id);
            }
            finally {
                _lock.Release();
            }

            _logger.Debug($"{id} twin stopping...", () => { });
            var host = twinScope.Resolve<IEdgeHost>();
            try {
                // Clear endpoint edge controller
                var events = twinScope.Resolve<IEventEmitter>();

                // Stop host async
                await host.StopAsync();
            }
            catch (Exception ex) {
                // _logger.Error($"{id} twin failed to stop...", () => ex);
                // throw ex;

                // BUGBUG: IoT Hub client SDK throws general exceptions independent
                // of what actually happened.  Instead of parsing the message,
                // just continue.
                _logger.Debug($"{id} twin stop raised exception, continue...",
                    () => ex);
            }
            finally {
                twinScope.Dispose();
            }
            _logger.Info($"{id} twin stopped.", () => { });
        }

        /// <summary>
        /// Dispose container and remaining active nested scopes...
        /// </summary>
        public void Dispose() {
            // _container.Dispose();
        }

        /// <summary>
        /// Build di container for twins
        /// </summary>
        /// <param name="client"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        private static IContainer CreateTwinContainer(IOpcUaClient client,
            ILogger logger, Action<ContainerBuilder> inject) {
            // Create container for all twin level scopes...
            var builder = new ContainerBuilder();

            // Register logger singleton instance
            builder.RegisterInstance(logger)
                .AsImplementedInterfaces().SingleInstance();

            // Register edge host module and twin state for the lifetime of the host
            builder.RegisterType<OpcUaTwinServices>()
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterModule<EdgeHostModule>();

            // Register opc ua client singleton instance
            builder.RegisterInstance(client)
                .AsImplementedInterfaces().SingleInstance();
            // Register opc ua services
            builder.RegisterType<OpcUaNodeServices>()
                .AsImplementedInterfaces();
            builder.RegisterType<OpcUaJsonVariantCodec>()
                .AsImplementedInterfaces();

            // Build twin container
            inject?.Invoke(builder);
            return builder.Build();
        }

        /// <summary>
        /// Twin host configuration wrapper
        /// </summary>
        private class TwinConfig : IOpcUaConfig, IEdgeConfig {

            /// <summary>
            /// Create twin configuration
            /// </summary>
            /// <param name="config"></param>
            /// <param name="secret"></param>
            public TwinConfig(IEdgeConfig config, string endpointId, string secret) {
                BypassCertVerification = config.BypassCertVerification;
                Transport = config.Transport;
                HubConnectionString = GetEdgeConnectionString(config, endpointId, secret);
            }

            /// <summary>
            /// Twin configuration
            /// </summary>
            public string HubConnectionString { get; }
            public bool BypassCertVerification { get; }
            public TransportOption Transport { get; }

            /// <summary>
            /// Dummy Service configuration
            /// </summary>
            public string IoTHubConnString => null;
            public string IoTHubResourceId => null;
            public bool BypassProxy => true;

            /// <summary>
            /// Create new connection string from existing edge connection string.
            /// </summary>
            /// <param name="config"></param>
            /// <param name="endpointId"></param>
            /// <param name="secret"></param>
            /// <returns></returns>
            private static string GetEdgeConnectionString(IEdgeConfig config,
                string endpointId, string secret) {

                var cs = config.HubConnectionString;
                if (string.IsNullOrEmpty(cs)) {
                    // Retrieve information from environment
                    var hostName = Environment.GetEnvironmentVariable("IOTEDGE_IOTHUBHOSTNAME");
                    if (string.IsNullOrEmpty(hostName)) {
                        throw new InvalidConfigurationException(
                            "Missing IOTEDGE_IOTHUBHOSTNAME variable in environment");
                    }
                    cs = $"HostName={hostName};DeviceId={endpointId};SharedAccessKey={secret}";
                    var edgeName = Environment.GetEnvironmentVariable("IOTEDGE_GATEWAYHOSTNAME");
                    if (!string.IsNullOrEmpty(edgeName)) {
                        cs += $";GatewayHostName={edgeName}";
                    }
                }
                else {
                    // Use existing connection string as a master plan
                    var lookup = cs
                        .Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => s.Trim().Split('='))
                        .ToDictionary(s => s[0].ToLowerInvariant(), v => v[1]);
                    if (!lookup.TryGetValue("hostname", out var hostName) ||
                        string.IsNullOrEmpty(hostName)) {
                        throw new InvalidConfigurationException(
                            "Missing HostName in connection string");
                    }
                    cs = $"HostName={hostName};DeviceId={endpointId};SharedAccessKey={secret}";
                    if (lookup.TryGetValue("gatewayhostname", out var edgeName) &&
                        !string.IsNullOrEmpty(edgeName)) {
                        cs += $";GatewayHostName={edgeName}";
                    }
                }
                return cs;
            }
        }

        private readonly IOpcUaClient _client;
        private readonly ILogger _logger;
        private readonly IEdgeConfig _config;
        private readonly IEventEmitter _events;
        private readonly IContainer _container;

        private readonly SemaphoreSlim _lock =
            new SemaphoreSlim(1);
        private readonly Dictionary<string, ILifetimeScope> _twinScopes =
            new Dictionary<string, ILifetimeScope>();
    }
}
