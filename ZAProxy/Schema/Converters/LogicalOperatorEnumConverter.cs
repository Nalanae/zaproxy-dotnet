using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZAProxy.Schema.Converters
{
    /// <summary>
    /// JSON converter for <see cref="LogicalOperator"/> values.
    /// </summary>
    public class LogicalOperatorEnumConverter : JsonConverter
    {
        /// <summary>
        /// Checks if <paramref name="objectType"/> is of type <see cref="LogicalOperator"/>.
        /// </summary>
        /// <param name="objectType">The type to check.</param>
        /// <returns>True if <paramref name="objectType"/> is of type <see cref="LogicalOperator"/>.</returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(LogicalOperator);
        }

        /// <summary>
        /// Converts JSON to <see cref="LogicalOperator"/>.
        /// </summary>
        /// <param name="reader">The JSON reader.</param>
        /// <param name="objectType">The type you're expecting. Should always be <see cref="LogicalOperator"/>.</param>
        /// <param name="existingValue">The existing value of the type. Not used in this converter.</param>
        /// <param name="serializer">The serializer used for the conversion.</param>
        /// <returns>The obtained <see cref="LogicalOperator"/> value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var logicalOperatorValue = JToken.Load(reader);

            LogicalOperator logicalOperator;
            if (!Enum.TryParse(logicalOperatorValue.Value<string>(), true, out logicalOperator))
                return LogicalOperator.And;

            return logicalOperator;
        }

        /// <summary>
        /// Converts <see cref="LogicalOperator"/> to JSON.
        /// </summary>
        /// <param name="writer">The JSON writer to write the value to.</param>
        /// <param name="value">The value of the value to convert. Should always be a <see cref="LogicalOperator"/> value.</param>
        /// <param name="serializer">The serializer used for the conversion.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var logicalOperator = (LogicalOperator)value;
            writer.WriteValue(logicalOperator.ToString().ToUpperInvariant());
        }
    }
}
