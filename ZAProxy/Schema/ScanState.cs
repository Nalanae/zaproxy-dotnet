using Newtonsoft.Json;
using System;

namespace ZAProxy.Schema
{
    /// <summary>
    /// Describes the state of a scan.
    /// </summary>
    public enum ScanState
    {
        /// <summary>
        /// The scan hasn't started yet.
        /// </summary>
        NotStarted,

        /// <summary>
        /// The scan is running.
        /// </summary>
        Running,

        /// <summary>
        /// The scan is paused.
        /// </summary>
        Paused,

        /// <summary>
        /// The scan is finished.
        /// </summary>
        Finished
    }

    namespace Converters
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
                var enumString = (string)reader.Value;
                ScanState scanState;

                if (!Enum.TryParse(enumString, true, out scanState))
                {
                    switch (enumString.ToUpperInvariant())
                    {
                        case "NOT_STARTED":
                            scanState = ScanState.NotStarted;
                            break;
                        default:
                            throw new JsonSerializationException($"Error converting value \"{enumString}\" to type \"{typeof(ScanState).ToString()}\".");
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
                ScanState scanState = (ScanState)value;
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
}
