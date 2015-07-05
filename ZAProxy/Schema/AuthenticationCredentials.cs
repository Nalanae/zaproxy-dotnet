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
    public class AuthenticationCredentials
    {
        private IDictionary<string, string> _parameters;

        /// <summary>
        /// Initiates a new instance of the <see cref="AuthenticationCredentials"/> class.
        /// </summary>
        public AuthenticationCredentials()
        {
            _parameters = new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets or sets the type of credentials.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the parameters and their values used by the credential type.
        /// </summary>
        public virtual IDictionary<string, string> Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }

        internal class Converter : JsonConverter
        {
            private const string TypePropertyName = "type";

            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(AuthenticationCredentials);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                var authenticationCredentialObject = JObject.Load(reader);
                var authenticationCredential = new AuthenticationCredentials
                {
                    Type = authenticationCredentialObject.Value<string>(TypePropertyName)
                };

                var parameterProperties = authenticationCredentialObject.Properties()
                    .Where(p => p.Name != TypePropertyName);
                foreach (var parameterProperty in parameterProperties)
                    authenticationCredential.Parameters.Add(parameterProperty.Name, parameterProperty.Value.ToString());

                return authenticationCredential;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                var authenticationCredential = (AuthenticationCredentials)value;
                var authenticationCredentialObject = new JObject(
                    new JProperty(TypePropertyName, authenticationCredential.Type));

                foreach (var parameter in authenticationCredential.Parameters)
                    authenticationCredentialObject.Add(parameter.Key, parameter.Value);

                authenticationCredentialObject.WriteTo(writer);
            }
        }
    }
}
