using Newtonsoft.Json;
using ZAProxy.Schema.Converters;

namespace ZAProxy.Schema
{
    /// <summary>
    /// Describes the configuration of a basic authorization detection method.
    /// </summary>
    public class BasicAuthorizationDetectionMethod
    {
        /// <summary>
        /// Gets or sets the regex pattern to verify the body of the response if user is unauthorized.
        /// </summary>
        public string BodyRegex { get; set; }
        
        /// <summary>
        /// Gets or sets the logical operator of the detection method.
        /// With <see cref="LogicalOperator.And"/> all predicates will need to be true, whereas with <see cref="LogicalOperator.Or"/> only one needs to.
        /// </summary>
        public LogicalOperator LogicalOperator { get; set; }

        /// <summary>
        /// Gets or sets the regex pattern to verify the header of the response if user is unauthorized.
        /// </summary>
        public string HeaderRegex { get; set; }

        /// <summary>
        /// Gets or sets the status code the server returns if a user is unauthorized.
        /// </summary>
        [JsonConverter(typeof(NullableInt32Converter))]
        public int? StatusCode { get; set; }
    }
}
