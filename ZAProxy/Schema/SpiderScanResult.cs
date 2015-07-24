using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZAProxy.Schema
{
    /// <summary>
    /// Describes the result of a spider scan.
    /// </summary>
    [JsonConverter(typeof(Converter))]
    public class SpiderScanResult
    {
        /// <summary>
        /// Gets or sets all the information on urls that are in scope.
        /// </summary>
        public IEnumerable<UrlInScope> UrlsInScope { get; set; }

        /// <summary>
        /// Gets or sets the urls that are out of scope.
        /// </summary>
        public IEnumerable<string> UrlsOutOfScope { get; set; }

        /// <summary>
        /// Describes the scan result of a url that is in scope.
        /// </summary>
        public class UrlInScope
        {
            /// <summary>
            /// Gets or sets the reason of the HTTP status code.
            /// </summary>
            public string StatusReason { get; set; }

            /// <summary>
            /// Gets or sets the HTTP method.
            /// </summary>
            public string Method { get; set; }

            /// <summary>
            /// Gets or sets the ID of the corresponding message.
            /// </summary>
            public int MessageId { get; set; }

            /// <summary>
            /// Gets or sets the url.
            /// </summary>
            public Uri Url { get; set; }

            /// <summary>
            /// Gets or sets the HTTP status code.
            /// </summary>
            public int StatusCode { get; set; }
        }

        internal class Converter : JsonConverter
        {
            private const string UrlsInScopePropertyName = "urlsInScope";
            private const string UrlsOutOfScopePropertyName = "urlsOutOfScope";

            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(SpiderScanResult);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                var spiderScanResult = new SpiderScanResult();
                var spiderScanResultJson = JToken.ReadFrom(reader);

                var inScopeJson = spiderScanResultJson.Value<JObject>(0);
                spiderScanResult.UrlsInScope = inScopeJson
                    .Value<JArray>(UrlsInScopePropertyName)
                    .ToObject<IEnumerable<UrlInScope>>();

                var outOfScopeJson = spiderScanResultJson.Value<JObject>(1);
                spiderScanResult.UrlsOutOfScope = outOfScopeJson
                    .Value<JArray>(UrlsOutOfScopePropertyName)
                    .ToObject<IEnumerable<string>>();

                return spiderScanResult;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                var spiderScanResult = (SpiderScanResult)value;

                var hostProcessesArray = new JArray(
                    new JObject(
                        new JProperty(UrlsInScopePropertyName, JArray.FromObject(spiderScanResult.UrlsInScope))),
                    new JObject(
                        new JProperty(UrlsOutOfScopePropertyName, JArray.FromObject(spiderScanResult.UrlsOutOfScope))));

                hostProcessesArray.WriteTo(writer);
            }
        }
    }
}
