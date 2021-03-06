// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa.Edge {
    using Microsoft.Azure.IIoT.OpcUa.Registry.Models;
    using System.Threading.Tasks;

    /// <summary>
    /// Export services
    /// </summary>
    public interface IExportServices {

        /// <summary>
        /// Start exporting model from endpoint
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="contentType"></param>
        /// <returns>file name of the model exported</returns>
        Task<string> StartModelExportAsync(EndpointModel endpoint,
            string contentType);

        /// <summary>
        /// Stop exporting model using returned id.
        /// </summary>
        /// <param name="id">Id returned by start.</param>
        /// <returns></returns>
        Task StopModelExportAsync(string id);
    }
}
