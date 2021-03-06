// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa.Twin.Models {

    /// <summary>
    /// Node path target
    /// </summary>
    public class NodePathTargetModel {

        /// <summary>
        /// Target node
        /// </summary>
        public NodeModel Target { get; set; }

        /// <summary>
        /// Remaining index in path
        /// </summary>
        public int? RemainingPathIndex { get; set; }
    }
}
