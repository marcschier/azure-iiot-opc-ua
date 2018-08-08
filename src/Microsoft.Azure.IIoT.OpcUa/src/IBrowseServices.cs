// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa {
    using Microsoft.Azure.IIoT.OpcUa.Models;
    using System.Threading.Tasks;

    /// <summary>
    /// Browse services via handle
    /// </summary>
    public interface IBrowseServices<T> {

        /// <summary>
        /// Browse nodes on endpoint
        /// </summary>
        /// <param name="endpoint">Endpoint url of the server
        /// to talk to</param>
        /// <param name="request">Browse request</param>
        /// <returns></returns>
        Task<BrowseResultModel> NodeBrowseFirstAsync(T endpoint,
            BrowseRequestModel request);

        /// <summary>
        /// Browse remainder of references
        /// </summary>
        /// <param name="endpoint">Endpoint url of the server
        /// to talk to</param>
        /// <param name="request">Continuation token</param>
        /// <returns></returns>
        Task<BrowseNextResultModel> NodeBrowseNextAsync(T endpoint,
            BrowseNextRequestModel request);
    }
}