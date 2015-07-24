using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZAProxy.Schema.Converters
{
    /// <summary>
    /// JSON converter for <see cref="HandleParametersOption"/> values.
    /// </summary>
    public class HandleParametersOptionEnumConverter : JsonConverter
    {
        /// <summary>
        /// Checks if <paramref name="objectType"/> is of type <see cref="HandleParametersOption"/>.
        /// </summary>
        /// <param name="objectType">The type to check.</param>
        /// <returns>True if <paramref name="objectType"/> is of type <see cref="HandleParametersOption"/>.</returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(HandleParametersOption);
        }

        /// <summary>
        /// Converts JSON to <see cref="HandleParametersOption"/>.
        /// </summary>
        /// <param name="reader">The JSON reader.</param>
        /// <param name="objectType">The type you're expecting. Should always be <see cref="HandleParametersOption"/>.</param>
        /// <param name="existingValue">The existing value of the type. Not used in this converter.</param>
        /// <param name="serializer">The serializer used for the conversion.</param>
        /// <returns>The obtained <see cref="HandleParametersOption"/> value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var handleParameterOptionValue = JToken.Load(reader);
            var handleParameterOptionString = handleParameterOptionValue.Value<string>();

            HandleParametersOption handleParameterOption;
            if (!Enum.TryParse(handleParameterOptionString, true, out handleParameterOption))
            {
                switch (handleParameterOptionString.ToUpperInvariant())
                {
                    case "IGNORE_COMPLETELY":
                        handleParameterOption = HandleParametersOption.IgnoreCompletely;
                        break;
                    case "IGNORE_VALUE":
                        handleParameterOption = HandleParametersOption.IgnoreValue;
                        break;
                    case "USE_ALL":
                        handleParameterOption = HandleParametersOption.UseAll;
                        break;
                    default:
                        throw new JsonSerializationException($"Error converting value \"{handleParameterOptionString}\" to type \"{typeof(HandleParametersOption).ToString()}\".");
                }
            }

            return handleParameterOption;
        }

        /// <summary>
        /// Converts <see cref="HandleParametersOption"/> to JSON.
        /// </summary>
        /// <param name="writer">The JSON writer to write the value to.</param>
        /// <param name="value">The value of the value to convert. Should always be a <see cref="HandleParametersOption"/> value.</param>
        /// <param name="serializer">The serializer used for the conversion.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var handleParameterOption = (HandleParametersOption)value;
            switch (handleParameterOption)
            {
                case HandleParametersOption.IgnoreCompletely:
                    writer.WriteValue("IGNORE_COMPLETELY");
                    break;
                case HandleParametersOption.IgnoreValue:
                    writer.WriteValue("IGNORE_VALUE");
                    break;
                case HandleParametersOption.UseAll:
                    writer.WriteValue("USE_ALL");
                    break;
                default:
                    writer.WriteValue(handleParameterOption.ToString().ToUpperInvariant());
                    break;
            }
        }
    }
}
