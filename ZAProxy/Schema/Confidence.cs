using Newtonsoft.Json;
using ZAProxy.Schema.Converters;

namespace ZAProxy.Schema
{
    /// <summary>
    /// Describes the confidence of the correctness of an alert.
    /// </summary>
    [JsonConverter(typeof(CapitalizedEnumConverter))]
    public enum Confidence
    {
        /// <summary>
        /// Low confidence.
        /// </summary>
        Low,

        /// <summary>
        /// Medium confidence.
        /// </summary>
        Medium,

        /// <summary>
        /// High confidence.
        /// </summary>
        High,

        /// <summary>
        /// Alert is confirmed, so 100% confidence.
        /// </summary>
        Confirmed
    }
}
