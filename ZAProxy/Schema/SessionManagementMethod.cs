using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZAProxy.Schema
{
    /// <summary>
    /// Describes a session management method used by ZAP.
    /// </summary>
    [JsonConverter(typeof(Converter))]
    public class SessionManagementMethod
    {
        private IDictionary<string, string> _parameters;

        /// <summary>
        /// Initiates a new instance of the <see cref="SessionManagementMethod"/> class.
        /// </summary>
        public SessionManagementMethod()
        {
            _parameters = new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets or sets the name of the used method.
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// Gets or sets the parameters and their values used by the method.
        /// </summary>
        public virtual IDictionary<string, string> Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }

        internal class Converter : JsonConverter
        {
            private const string NamePropertyName = "methodName";

            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(SessionManagementMethod);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                var sessionManagementMethodObject = JObject.Load(reader);
                var sessionManagementMethod = new SessionManagementMethod
                {
                    MethodName = sessionManagementMethodObject.Value<string>(NamePropertyName)
                };

                var parameterProperties = sessionManagementMethodObject.Properties()
                    .Where(p => p.Name != NamePropertyName);
                foreach (var parameterProperty in parameterProperties)
                    sessionManagementMethod.Parameters.Add(parameterProperty.Name, parameterProperty.Value.ToString());

                return sessionManagementMethod;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                var sessionManagementMethod = (SessionManagementMethod)value;
                var sessionManagementMethodObject = new JObject(
                    new JProperty(NamePropertyName, sessionManagementMethod.MethodName));

                foreach (var parameter in sessionManagementMethod.Parameters)
                    sessionManagementMethodObject.Add(parameter.Key, parameter.Value);

                sessionManagementMethodObject.WriteTo(writer);
            }
        }
    }
}
