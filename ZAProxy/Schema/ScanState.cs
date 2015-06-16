using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZAProxy.Schema
{
    public enum ScanState
    {
        NotStarted,
        Running,
        Paused,
        Finished
    }

    public class ScanStateEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ScanState);
        }

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
