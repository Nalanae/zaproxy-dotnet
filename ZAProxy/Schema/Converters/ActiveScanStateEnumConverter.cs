using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZAProxy.Schema.Converters
{
    /// <summary>
    /// JSON converter for <see cref="ActiveScanState"/> values.
    /// </summary>
    public class ActiveScanStateEnumConverter : JsonConverter
    {
        /// <summary>
        /// Checks if <paramref name="objectType"/> is of type <see cref="ActiveScanState"/>.
        /// </summary>
        /// <param name="objectType">The type to check.</param>
        /// <returns>True if <paramref name="objectType"/> is of type <see cref="ActiveScanState"/>.</returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ActiveScanState);
        }

        /// <summary>
        /// Converts JSON to <see cref="ActiveScanState"/>.
        /// </summary>
        /// <param name="reader">The JSON reader.</param>
        /// <param name="objectType">The type you're expecting. Should always be <see cref="ActiveScanState"/>.</param>
        /// <param name="existingValue">The existing value of the type. Not used in this converter.</param>
        /// <param name="serializer">The serializer used for the conversion.</param>
        /// <returns>The obtained <see cref="ActiveScanState"/> value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var scanStateValue = JToken.Load(reader);
            var scanStateString = scanStateValue.Value<string>();

            ActiveScanState scanState;
            if (!Enum.TryParse(scanStateString, true, out scanState))
            {
                switch (scanStateString.ToUpperInvariant())
                {
                    case "NOT_STARTED":
                        scanState = ActiveScanState.NotStarted;
                        break;
                    default:
                        throw new JsonSerializationException($"Error converting value \"{scanStateString}\" to type \"{typeof(ActiveScanState).ToString()}\".");
                }
            }

            return scanState;
        }

        /// <summary>
        /// Converts <see cref="ActiveScanState"/> to JSON.
        /// </summary>
        /// <param name="writer">The JSON writer to write the value to.</param>
        /// <param name="value">The value of the value to convert. Should always be a <see cref="ActiveScanState"/> value.</param>
        /// <param name="serializer">The serializer used for the conversion.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var scanState = (ActiveScanState)value;
            switch (scanState)
            {
                case ActiveScanState.NotStarted:
                    writer.WriteValue("NOT_STARTED");
                    break;
                default:
                    writer.WriteValue(scanState.ToString().ToUpperInvariant());
                    break;
            }
        }
    }
}
