// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa {
    using Microsoft.Azure.IIoT.OpcUa.Models;
    using System.Threading.Tasks;

    /// <summary>
    /// Twin registry
    /// </summary>
    public interface ITwinRegistry {

        /// <summary>
        /// Get all twins in paged form
        /// </summary>
        /// <param name="onlyServerState">Whether only
        /// desired endpoint state should be returned.
        /// </param>
        /// <param name="continuation"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<TwinInfoListModel> ListTwinsAsync(string continuation,
            bool onlyServerState, int? pageSize);

        /// <summary>
        /// Find registration of the supplied endpoint.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="onlyServerState">Whether only
        /// desired twin state should be returned.
        /// </param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<TwinInfoListModel> QueryTwinsAsync(
            TwinRegistrationQueryModel query, bool onlyServerState,
            int? pageSize);

        /// <summary>
        /// Find registration of the supplied endpoint.
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="onlyServerState">Whether only
        /// desired twin state should be returned.
        /// </param>
        /// <returns></returns>
        Task<TwinInfoModel> FindTwinAsync(EndpointModel endpoint,
            bool onlyServerState);

        /// <summary>
        /// Get twin registration by identifer.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="onlyServerState">Whether only
        /// desired twin state should be returned.
        /// </param>
        /// <returns></returns>
        Task<TwinInfoModel> GetTwinAsync(string id, bool onlyServerState);

        /// <summary>
        /// Update existing server twin registration. Note that
        /// Id and url field in request must not be null and
        /// endpoint registration must exist.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task UpdateTwinAsync(TwinRegistrationUpdateModel request);
    }
}