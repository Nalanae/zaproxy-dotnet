using Newtonsoft.Json;
using ZAProxy.Schema.Converters;

namespace ZAProxy.Schema
{
    /// <summary>
    /// Describes a logical operator in ZAP.
    /// </summary>
    [JsonConverter(typeof(CapitalizedEnumConverter))]
    public enum LogicalOperator
    {
        /// <summary>
        /// All predicates must be true.
        /// </summary>
        And,

        /// <summary>
        /// At least one predicate must be true.
        /// </summary>
        Or
    }
}
