using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZAProxy.Schema
{
    /// <summary>
    /// Describes an authentication method used by a context.
    /// </summary>
    [JsonConverter(typeof(Converter))]
    public class AuthenticationMethod
    {
        private IDictionary<string, string> _parameters;

        /// <summary>
        /// Initiates a new instance of the <see cref="AuthenticationMethod"/> class.
        /// </summary>
        public AuthenticationMethod()
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
                return objectType == typeof(AuthenticationMethod);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                var authenticationMethodObject = JObject.Load(reader);
                var authenticationMethod = new AuthenticationMethod
                {
                    MethodName = authenticationMethodObject.Value<string>(NamePropertyName)
                };

                var parameterProperties = authenticationMethodObject.Properties()
                    .Where(p => p.Name != NamePropertyName);
                foreach (var parameterProperty in parameterProperties)
                    authenticationMethod.Parameters.Add(parameterProperty.Name, parameterProperty.Value.ToString());

                return authenticationMethod;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                var authenticationMethod = (AuthenticationMethod)value;
                var authenticationMethodObject = new JObject(
                    new JProperty(NamePropertyName, authenticationMethod.MethodName));

                foreach (var parameter in authenticationMethod.Parameters)
                    authenticationMethodObject.Add(parameter.Key, parameter.Value);

                authenticationMethodObject.WriteTo(writer);
            }
        }
    }
}
