using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZAProxy.Schema.Converters
{
    /// <summary>
    /// JSON converter to capitalize enum values.
    /// </summary>
    public class CapitalizedEnumConverter : JsonConverter
    {
        /// <summary>
        /// Checks if <paramref name="objectType"/> is an enum.
        /// </summary>
        /// <param name="objectType">The type to check.</param>
        /// <returns>True if <paramref name="objectType"/> is an enum.</returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Enum);
        }

        /// <summary>
        /// Converts JSON to an enum value.
        /// </summary>
        /// <param name="reader">The JSON reader.</param>
        /// <param name="objectType">The type you're expecting. Should always be <see cref="LogicalOperator"/>.</param>
        /// <param name="existingValue">The existing value of the type. Not used in this converter.</param>
        /// <param name="serializer">The serializer used for the conversion.</param>
        /// <returns>The obtained <see cref="LogicalOperator"/> value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var enumValue = JToken.Load(reader);
            return Enum.Parse(objectType, enumValue.Value<string>(), true);
        }

        /// <summary>
        /// Converts an enum to a capitalized JSON value.
        /// </summary>
        /// <param name="writer">The JSON writer to write the value to.</param>
        /// <param name="value">The value of the value to convert. Should always be a <see cref="LogicalOperator"/> value.</param>
        /// <param name="serializer">The serializer used for the conversion.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var enumValue = (Enum)value;
            writer.WriteValue(enumValue.ToString().ToUpperInvariant());
        }
    }
}
