// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa {
    using Microsoft.Azure.IIoT.OpcUa.Models;
    using System.Threading.Tasks;

    /// <summary>
    /// Node services via endpoint model
    /// </summary>
    public interface INodeServices<T> {

        /// <summary>
        /// Read node value
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ValueReadResultModel> NodeValueReadAsync(T endpoint,
            ValueReadRequestModel request);

        /// <summary>
        /// Write node value
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ValueWriteResultModel> NodeValueWriteAsync(T endpoint,
            ValueWriteRequestModel request);

        /// <summary>
        /// Get meta data for method call (input and output arguments)
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<MethodMetadataResultModel> NodeMethodGetMetadataAsync(
            T endpoint, MethodMetadataRequestModel request);

        /// <summary>
        /// Call method
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<MethodCallResultModel> NodeMethodCallAsync(T endpoint,
            MethodCallRequestModel request);
    }
}