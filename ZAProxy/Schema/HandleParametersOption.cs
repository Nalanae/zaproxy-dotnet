using Newtonsoft.Json;
using ZAProxy.Schema.Converters;

namespace ZAProxy.Schema
{
    /// <summary>
    /// Describes how the parameters are used when checking if an URI was already visited.
    /// </summary>
    [JsonConverter(typeof(HandleParametersOptionEnumConverter))]
    public enum HandleParametersOption
    {
        /// <summary>
        /// Parameters are ignored completely.
        /// </summary>
        IgnoreCompletely,
        
        /// <summary>
        /// Only the name of the parameter is used, but not the value.
        /// </summary>
        IgnoreValue,

        /// <summary>
        /// Both the name and value of the parameter are used.
        /// </summary>
        UseAll
    }
}
