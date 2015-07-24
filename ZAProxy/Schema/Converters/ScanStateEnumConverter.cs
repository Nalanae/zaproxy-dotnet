using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZAProxy.Schema.Converters
{
    /// <summary>
    /// JSON converter for <see cref="ScanState"/> values.
    /// </summary>
    public class ScanStateEnumConverter : JsonConverter
    {
        /// <summary>
        /// Checks if <paramref name="objectType"/> is of type <see cref="ScanState"/>.
        /// </summary>
        /// <param name="objectType">The type to check.</param>
        /// <returns>True if <paramref name="objectType"/> is of type <see cref="ScanState"/>.</returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ScanState);
        }

        /// <summary>
        /// Converts JSON to <see cref="ScanState"/>.
        /// </summary>
        /// <param name="reader">The JSON reader.</param>
        /// <param name="objectType">The type you're expecting. Should always be <see cref="ScanState"/>.</param>
        /// <param name="existingValue">The existing value of the type. Not used in this converter.</param>
        /// <param name="serializer">The serializer used for the conversion.</param>
        /// <returns>The obtained <see cref="ScanState"/> value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var scanStateValue = JToken.Load(reader);
            var scanStateString = scanStateValue.Value<string>();

            ScanState scanState;
            if (!Enum.TryParse(scanStateString, true, out scanState))
            {
                switch (scanStateString.ToUpperInvariant())
                {
                    case "NOT_STARTED":
                        scanState = ScanState.NotStarted;
                        break;
                    default:
                        throw new JsonSerializationException($"Error converting value \"{scanStateString}\" to type \"{typeof(ScanState).ToString()}\".");
                }
            }

            return scanState;
        }

        /// <summary>
        /// Converts <see cref="ScanState"/> to JSON.
        /// </summary>
        /// <param name="writer">The JSON writer to write the value to.</param>
        /// <param name="value">The value of the value to convert. Should always be a <see cref="ScanState"/> value.</param>
        /// <param name="serializer">The serializer used for the conversion.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var scanState = (ScanState)value;
            switch (scanState)
            {
                case ScanState.NotStarted:
                    writer.WriteValue("NOT_STARTED");
                    break;
                default:
                    writer.WriteValue(scanState.ToString().ToUpperInvariant());
                    break;
            }
        }
    }
}
