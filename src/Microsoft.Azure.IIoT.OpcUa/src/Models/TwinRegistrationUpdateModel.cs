﻿// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa.Models {
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Endpoint registration update request
    /// </summary>
    public class TwinRegistrationUpdateModel {

        /// <summary>
        /// Identifier of the twin to update
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Whether to copy existing registration
        /// rather than replacing
        /// </summary>
        public bool? Duplicate { get; set; }

        /// <summary>
        /// Enable (=true) or disable twin
        /// </summary>
        public bool? Activate { get; set; }

        /// <summary>
        /// User name to use
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// User token to pass to server
        /// </summary>
        public JToken Token { get; set; }

        /// <summary>
        /// Type of token
        /// </summary>
        public TokenType? TokenType { get; set; }
    }
}