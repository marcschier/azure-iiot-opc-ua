// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa.Twin.Clients {
    using Microsoft.Azure.IIoT.OpcUa.Twin.Models;
    using Microsoft.Azure.IIoT.Module;
    using Microsoft.Azure.IIoT.Hub;
    using Microsoft.Azure.IIoT.Diagnostics;
    using Newtonsoft.Json;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Diagnostics;

    /// <summary>
    /// Implements node and publish services through command control against
    /// the OPC twin module receiving service requests via device method calls.
    /// </summary>
    public sealed class TwinClient : IBrowseServices<string>,
        INodeServices<string>, IPublishServices<string> {

        /// <summary>
        /// Create service
        /// </summary>
        /// <param name="client"></param>
        /// <param name="logger"></param>
        public TwinClient(IMethodClient client, ILogger logger) {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public async Task<PublishStartResultModel> NodePublishStartAsync(string endpointId,
            PublishStartRequestModel request) {
            if (request == null) {
                throw new ArgumentNullException(nameof(request));
            }
            if (request.Node == null) {
                throw new ArgumentNullException(nameof(request.Node));
            }
            if (string.IsNullOrEmpty(request.Node.NodeId)) {
                throw new ArgumentNullException(nameof(request.Node.NodeId));
            }
            var result = await CallServiceOnTwin<PublishStartRequestModel, PublishStartResultModel>(
                "PublishStart_V1", endpointId, request);
            return result;
        }

        /// <inheritdoc/>
        public async Task<PublishStopResultModel> NodePublishStopAsync(string endpointId,
            PublishStopRequestModel request) {
            if (request == null) {
                throw new ArgumentNullException(nameof(request));
            }
            if (string.IsNullOrEmpty(request.NodeId)) {
                throw new ArgumentNullException(nameof(request.NodeId));
            }
            var result = await CallServiceOnTwin<PublishStopRequestModel, PublishStopResultModel>(
                "PublishStop_V1", endpointId, request);
            return result;
        }

        /// <inheritdoc/>
        public async Task<PublishedNodeListResultModel> NodePublishListAsync(
            string endpointId, PublishedNodeListRequestModel request) {
            var result = await CallServiceOnTwin<PublishedNodeListRequestModel, PublishedNodeListResultModel>(
                "PublishList_V1", endpointId, request);
            return result;
        }

        /// <inheritdoc/>
        public async Task<BrowseResultModel> NodeBrowseFirstAsync(string endpointId,
            BrowseRequestModel request) {
            return await CallServiceOnTwin<BrowseRequestModel, BrowseResultModel>(
                "Browse_V1", endpointId, request);
        }

        /// <inheritdoc/>
        public async Task<BrowseNextResultModel> NodeBrowseNextAsync(string endpointId,
            BrowseNextRequestModel request) {
            if (request == null) {
                throw new ArgumentNullException(nameof(request));
            }
            if (string.IsNullOrEmpty(request.ContinuationToken)) {
                throw new ArgumentNullException(nameof(request.ContinuationToken));
            }
            return await CallServiceOnTwin<BrowseNextRequestModel, BrowseNextResultModel>(
                "BrowseNext_V1", endpointId, request);
        }

        /// <inheritdoc/>
        public async Task<BrowsePathResultModel> NodeBrowsePathAsync(string endpointId,
            BrowsePathRequestModel request) {
            if (request == null) {
                throw new ArgumentNullException(nameof(request));
            }
            if (request.PathElements == null || request.PathElements.Length == 0) {
                throw new ArgumentNullException(nameof(request.PathElements));
            }
            return await CallServiceOnTwin<BrowsePathRequestModel, BrowsePathResultModel>(
                "BrowsePath_V1", endpointId, request);
        }

        /// <inheritdoc/>
        public async Task<ValueReadResultModel> NodeValueReadAsync(string endpointId,
            ValueReadRequestModel request) {
            if (request == null) {
                throw new ArgumentNullException(nameof(request));
            }
            if (string.IsNullOrEmpty(request.NodeId)) {
                throw new ArgumentException(nameof(request.NodeId));
            }
            return await CallServiceOnTwin<ValueReadRequestModel, ValueReadResultModel>(
                "ValueRead_V1", endpointId, request);
        }

        /// <inheritdoc/>
        public async Task<ValueWriteResultModel> NodeValueWriteAsync(string endpointId,
            ValueWriteRequestModel request) {
            if (request == null) {
                throw new ArgumentNullException(nameof(request));
            }
            if (request.Value == null) {
                throw new ArgumentNullException(nameof(request.Value));
            }
            if (string.IsNullOrEmpty(request.NodeId)) {
                throw new ArgumentException(nameof(request.NodeId));
            }
            return await CallServiceOnTwin<ValueWriteRequestModel, ValueWriteResultModel>(
                "ValueWrite_V1", endpointId, request);
        }

        /// <inheritdoc/>
        public async Task<MethodMetadataResultModel> NodeMethodGetMetadataAsync(
            string endpointId, MethodMetadataRequestModel request) {
            if (request == null) {
                throw new ArgumentNullException(nameof(request));
            }
            if (string.IsNullOrEmpty(request.MethodId)) {
                throw new ArgumentNullException(nameof(request.MethodId));
            }
            return await CallServiceOnTwin<MethodMetadataRequestModel, MethodMetadataResultModel>(
                "MethodMetadata_V1", endpointId, request);
        }

        /// <inheritdoc/>
        public async Task<MethodCallResultModel> NodeMethodCallAsync(
            string endpointId, MethodCallRequestModel request) {
            if (request == null) {
                throw new ArgumentNullException(nameof(request));
            }
            if (string.IsNullOrEmpty(request.MethodId)) {
                throw new ArgumentNullException(nameof(request.MethodId));
            }
            return await CallServiceOnTwin<MethodCallRequestModel, MethodCallResultModel>(
                "MethodCall_V1", endpointId, request);
        }

        /// <inheritdoc/>
        public async Task<BatchReadResultModel> NodeBatchReadAsync(
            string endpointId, BatchReadRequestModel request) {
            if (request == null) {
                throw new ArgumentNullException(nameof(request));
            }
            if (request.Attributes == null || request.Attributes.Count == 0) {
                throw new ArgumentNullException(nameof(request.Attributes));
            }
            if (request.Attributes.Any(r => string.IsNullOrEmpty(r.NodeId))) {
                throw new ArgumentException(nameof(request.Attributes));
            }
            return await CallServiceOnTwin<BatchReadRequestModel, BatchReadResultModel>(
                "BatchRead_V1", endpointId, request);
        }

        /// <inheritdoc/>
        public async Task<BatchWriteResultModel> NodeBatchWriteAsync(
            string endpointId, BatchWriteRequestModel request) {
            if (request == null) {
                throw new ArgumentNullException(nameof(request));
            }
            if (request.Attributes == null || request.Attributes.Count == 0) {
                throw new ArgumentNullException(nameof(request.Attributes));
            }
            if (request.Attributes.Any(r => string.IsNullOrEmpty(r.NodeId))) {
                throw new ArgumentException(nameof(request.Attributes));
            }
            return await CallServiceOnTwin<BatchWriteRequestModel, BatchWriteResultModel>(
                "BatchWrite_V1", endpointId, request);
        }

        /// <inheritdoc/>
        public async Task<HistoryReadResultModel> NodeHistoryReadAsync(
            string endpointId, HistoryReadRequestModel request) {
            if (request == null) {
                throw new ArgumentNullException(nameof(request));
            }
            if (string.IsNullOrEmpty(request.NodeId)) {
                throw new ArgumentNullException(nameof(request.NodeId));
            }
            return await CallServiceOnTwin<HistoryReadRequestModel, HistoryReadResultModel>(
                "HistoryRead_V1", endpointId, request);
        }

        /// <inheritdoc/>
        public async Task<HistoryReadNextResultModel> NodeHistoryReadNextAsync(
            string endpointId, HistoryReadNextRequestModel request) {
            if (request == null) {
                throw new ArgumentNullException(nameof(request));
            }
            if (string.IsNullOrEmpty(request.ContinuationToken)) {
                throw new ArgumentNullException(nameof(request.ContinuationToken));
            }
            return await CallServiceOnTwin<HistoryReadNextRequestModel, HistoryReadNextResultModel>(
                "HistoryReadNext_V1", endpointId, request);
        }

        /// <inheritdoc/>
        public async Task<HistoryUpdateResultModel> NodeHistoryUpdateAsync(
            string endpointId, HistoryUpdateRequestModel request) {
            if (request == null) {
                throw new ArgumentNullException(nameof(request));
            }
            if (request.Request == null) {
                throw new ArgumentNullException(nameof(request.Request));
            }
            return await CallServiceOnTwin<HistoryUpdateRequestModel, HistoryUpdateResultModel>(
                "HistoryUpdate_V1", endpointId, request);
        }

        /// <summary>
        /// helper to invoke service
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="service"></param>
        /// <param name="endpointId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<R> CallServiceOnTwin<T, R>(string service,
            string endpointId, T request) {
            if (string.IsNullOrEmpty(endpointId)) {
                throw new ArgumentNullException(nameof(endpointId));
            }
            var sw = Stopwatch.StartNew();
            var result = await _client.CallMethodAsync(endpointId, null, service,
                JsonConvertEx.SerializeObject(request));
            _logger.Debug($"Twin call '{service}' took {sw.ElapsedMilliseconds} ms)!");
            return JsonConvertEx.DeserializeObject<R>(result);
        }

        private readonly IMethodClient _client;
        private readonly ILogger _logger;
    }
}
