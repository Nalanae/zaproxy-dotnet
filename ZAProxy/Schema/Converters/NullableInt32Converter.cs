using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZAProxy.Schema.Converters
{
    /// <summary>
    /// JSON converter for nullable <see cref="int"/> values.
    /// </summary>
    public class NullableInt32Converter : JsonConverter
    {
        /// <summary>
        /// Checks if <paramref name="objectType"/> is a nullable <see cref="int"/>.
        /// </summary>
        /// <param name="objectType">The type to check.</param>
        /// <returns>True if <paramref name="objectType"/> is a nullable <see cref="int"/>.</returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(int?);
        }

        /// <summary>
        /// Converts JSON to nullable <see cref="int"/>.
        /// </summary>
        /// <param name="reader">The JSON reader.</param>
        /// <param name="objectType">The type you're expecting. Should always be a nullable <see cref="int"/>.</param>
        /// <param name="existingValue">The existing value of the type. Not used in this converter.</param>
        /// <param name="serializer">The serializer used for the conversion.</param>
        /// <returns>The obtained nullable <see cref="int"/> value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var nullableIntValue = JToken.Load(reader);

            int parsedInt;
            if (int.TryParse(nullableIntValue.Value<string>(), out parsedInt))
                return parsedInt;
            else
                return null;
        }

        /// <summary>
        /// Converts nullable <see cref="int"/> to JSON.
        /// </summary>
        /// <param name="writer">The JSON writer to write the value to.</param>
        /// <param name="value">The value of the value to convert. Should always be a nullable <see cref="int"/> value.</param>
        /// <param name="serializer">The serializer used for the conversion.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var nullableInt = (int?)value;
            if (nullableInt.HasValue)
                writer.WriteValue(nullableInt.Value);
            else
                writer.WriteNull();
        }
    }
}
