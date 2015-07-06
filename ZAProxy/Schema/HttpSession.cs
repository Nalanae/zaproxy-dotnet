using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZAProxy.Schema
{
    /// <summary>
    /// Describes an HTTP session in ZAP.
    /// </summary>
    [JsonConverter(typeof(Converter))]
    public class HttpSession
    {
        /// <summary>
        /// Initiates a new instance of the <see cref="HttpSession"/> class.
        /// </summary>
        public HttpSession()
        {
            Tokens = new Dictionary<string, Cookie>();
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the dictionary of tokens.
        /// Key is the token name. Value is the cookie.
        /// </summary>
        public IDictionary<string, Cookie> Tokens { get; set; }

        /// <summary>
        /// Gets or sets the amount of messages that are affected by this session.
        /// </summary>
        public int MessagesMatched { get; set; }

        internal class Converter : JsonConverter
        {
            private const string SessionPropertyName = "session";

            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(HttpSession);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                var httpSessionObject = JObject.Load(reader);
                var httpSessionArray = httpSessionObject.Value<JArray>(SessionPropertyName);

                var httpSession = new HttpSession
                {
                    Name = httpSessionArray.Value<string>(0),
                    Tokens = httpSessionArray.Value<JObject>(1)
                        .Properties()
                        .ToDictionary(
                            p => p.Name, 
                            p => p.Value.ToObject<Cookie>()),
                    MessagesMatched = httpSessionArray.Value<int>(2)
                };

                return httpSession;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                var httpSession = (HttpSession)value;

                var httpSessionObject = new JObject(
                    new JProperty(SessionPropertyName, new JArray(
                        httpSession.Name,
                        new JObject(
                            httpSession.Tokens.Select(t => 
                                new JProperty(t.Key, JObject.FromObject(t.Value)))),
                        httpSession.MessagesMatched)));

                httpSessionObject.WriteTo(writer);
            }
        }
    }
}
