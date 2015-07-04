using Newtonsoft.Json;
using System;

namespace ZAProxy.Schema
{
    /// <summary>
    /// Describes an alert in ZAP.
    /// </summary>
    public class Alert
    {
        /// <summary>
        /// Gets or sets other info.
        /// </summary>
        public string Other { get; set; }

        /// <summary>
        /// Gets or sets evidence.
        /// </summary>
        public string Evidence { get; set; }

        /// <summary>
        /// Gets or sets the Common Weakness Enumeration ID. See <see href="https://cwe.mitre.org/"/>.
        /// </summary>
        public int CweId { get; set; }

        /// <summary>
        /// Gets or sets the confidence level.
        /// </summary>
        public Confidence Confidence { get; set; }

        /// <summary>
        /// Gets or sets the Web Application Security Consortium ID. See <see href="http://www.webappsec.org/"/>.
        /// </summary>
        public int WascId { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ID of the message where this alert was found.
        /// </summary>
        public int MessageId { get; set; }

        /// <summary>
        /// Gets or sets the url where the alert was found.
        /// </summary>
        public Uri Url { get; set; }

        /// <summary>
        /// Gets or sets a url where more info can be found on the alert.
        /// </summary>
        public Uri Reference { get; set; }

        /// <summary>
        /// Gets or sets the proposed solution for the alert.
        /// </summary>
        public string Solution { get; set; }

        /// <summary>
        /// Gets or sets the alert.
        /// </summary>
        [JsonProperty("alert")]
        public string AlertText{ get; set; }

        /// <summary>
        /// Gets or sets the param.
        /// </summary>
        public string Param { get; set; }

        /// <summary>
        /// Gets or sets the attack.
        /// </summary>
        public string Attack { get; set; }

        /// <summary>
        /// Gets or sets the risk level.
        /// </summary>
        public Risk Risk { get; set; }

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public int Id { get; set; }
    }
}
