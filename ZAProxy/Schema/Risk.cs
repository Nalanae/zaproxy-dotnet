using Newtonsoft.Json;
using ZAProxy.Schema.Converters;

namespace ZAProxy.Schema
{
    /// <summary>
    /// Describes the risk of an alert.
    /// </summary>
    [JsonConverter(typeof(CapitalizedEnumConverter))]
    public enum Risk
    {
        /// <summary>
        /// Alert is information.
        /// </summary>
        Info,
        
        /// <summary>
        /// Alert has low risk.
        /// </summary>
        Low,

        /// <summary>
        /// Alert has medium risk.
        /// </summary>
        Medium,

        /// <summary>
        /// Alert has high risk.
        /// </summary>
        High
    }
}
