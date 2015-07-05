using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZAProxy.Schema
{
    /// <summary>
    /// Describes an http parameter obtained by ZAP.
    /// </summary>
    [JsonConverter(typeof(Converter))]
    public class HttpParameter
    {
        /// <summary>
        /// Initiates a new instance of the <see cref="HttpParameter"/> class.
        /// </summary>
        public HttpParameter()
        {
            Flags = new List<HttpParameterFlag>();
            Values = new List<string>();
        }

        /// <summary>
        /// Gets or sets the site that holds the parameter.
        /// </summary>
        public string Site { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets how many times the parameter has been used.
        /// </summary>
        public int TimesUsed { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public HttpParameterType Type { get; set; }

        /// <summary>
        /// Gets or sets the flags.
        /// </summary>
        public IList<HttpParameterFlag> Flags { get; set; }

        /// <summary>
        /// Gets or sets the values the parameter has had.
        /// </summary>
        public IList<string> Values { get; set; }

        internal class Converter : JsonConverter
        {
            private const string ParameterPropertyName = "Parameter";
            private const string SitePropertyName = "site";
            private const string NamePropertyName = "name";
            private const string TimesUsedPropertyName = "timesUsed";
            private const string TypePropertyName = "type";
            private const string FlagsPropertyName = "Flags";
            private const string ValuesPropertyName = "Values";

            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(HttpParameter);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                var httpParameterObject = JObject.Load(reader);
                var httpParameterArray = httpParameterObject.Value<JArray>(ParameterPropertyName);

                var mainObject = httpParameterArray.Value<JObject>(0);
                var httpParameter = new HttpParameter
                {
                    Site = mainObject.Value<string>(SitePropertyName),
                    Name = mainObject.Value<string>(NamePropertyName),
                    TimesUsed = mainObject.Value<int>(TimesUsedPropertyName),
                    Type = mainObject.Value<JToken>(TypePropertyName).ToObject<HttpParameterType>()
                };

                foreach (JObject extraObject in httpParameterArray)
                {
                    if (extraObject == mainObject)
                        continue;

                    var flagsArray = extraObject.Value<JArray>(FlagsPropertyName);
                    if (flagsArray != null)
                        httpParameter.Flags = flagsArray.ToObject<IList<HttpParameterFlag>>();

                    var valuesArray = extraObject.Value<JArray>(ValuesPropertyName);
                    if (valuesArray != null)
                        httpParameter.Values = valuesArray.ToObject<IList<string>>();
                }

                return httpParameter;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                var httpParameter = (HttpParameter)value;
                var httpParameterObject = new JObject(
                    new JProperty(ParameterPropertyName, new JArray(
                        new JObject(
                            new JProperty(SitePropertyName, httpParameter.Site),
                            new JProperty(NamePropertyName, httpParameter.Name),
                            new JProperty(TimesUsedPropertyName, httpParameter.TimesUsed),
                            new JProperty(TypePropertyName, httpParameter.Type.ToString())))));

                if (httpParameter.Flags.Any())
                    httpParameterObject.Value<JArray>(ParameterPropertyName).Add(
                        new JObject(
                            new JProperty(FlagsPropertyName, JArray.FromObject(httpParameter.Flags.Select(f => f.ToString())))));

                if (httpParameter.Values.Any())
                    httpParameterObject.Value<JArray>(ParameterPropertyName).Add(
                        new JObject(
                            new JProperty(ValuesPropertyName, JArray.FromObject(httpParameter.Values))));

                httpParameterObject.WriteTo(writer);
            }
        }
    }
}
