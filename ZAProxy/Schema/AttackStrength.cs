using Newtonsoft.Json;
using ZAProxy.Schema.Converters;

namespace ZAProxy.Schema
{
    /// <summary>
    /// Describes the strength of attacks that are attempted in a scan.
    /// </summary>
    [JsonConverter(typeof(CapitalizedEnumConverter))]
    public enum AttackStrength
    {
        /// <summary>
        /// Default attack strength.
        /// </summary>
        Default,

        /// <summary>
        /// Low attack strength.
        /// </summary>
        Low,
        
        /// <summary>
        /// Medium attack strength.
        /// </summary>
        Medium,

        /// <summary>
        /// High attack strength.
        /// </summary>
        High,

        /// <summary>
        /// Insane attack strength.
        /// </summary>
        Insane
    }
}
