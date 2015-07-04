using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ZAProxy.Infrastructure;

namespace ZAProxy.Components
{
    /// <summary>
    /// Provides a base for all ZAP components to fire off API calls.
    /// </summary>
    public abstract class ComponentBase
    {
        private readonly IHttpClient _httpClient;
        private readonly IZapProcess _zapProcess;

        /// <summary>
        /// Initiates a new instance of the <see cref="ComponentBase"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client implementation.</param>
        /// <param name="zapProcess">The ZAP process to connect to.</param>
        /// <param name="componentUrlName">The API name of the implementing component.</param>
        protected ComponentBase(IHttpClient httpClient, IZapProcess zapProcess, string componentUrlName)
        {
            _httpClient = httpClient ?? new HttpClient(zapProcess);
            _zapProcess = zapProcess;
            ComponentName = componentUrlName;
        }
        
        /// <summary>
        /// Gets the API name of component.
        /// </summary>
        public string ComponentName { get; private set; }

        /// <summary>
        /// Calls a JSON view API method in ZAP and returns it result deserialized.
        /// </summary>
        /// <typeparam name="T">Type to deserialize the result to.</typeparam>
        /// <param name="method">The name of the API method.</param>
        /// <param name="takeValueFromProperty">Optional JSON property name to take as the input for deserialization.</param>
        /// <param name="parameters">Optional parameters to send to the API method.</param>
        /// <returns>Deserialized API result.</returns>
        protected T CallView<T>(string method, string takeValueFromProperty = null, IDictionary<string, object> parameters = null)
        {
            var result = CallJsonApi(CallType.View, method, parameters);
            if (takeValueFromProperty != null)
                result = result[takeValueFromProperty];
            if (result == null)
                throw new ZapException(Resources.CallViewUnknownResult);
            return result.ToObject<T>();
        }

        /// <summary>
        /// Calls a JSON action API method in ZAP.
        /// </summary>
        /// <param name="method">The name of the API method.</param>
        /// <param name="parameters">Optional parameters to send to the API method.</param>
        protected void CallAction(string method, IDictionary<string, object> parameters = null)
        {
            var result = CallAction<string>(method, "Result", parameters);
            switch(result.ToString())
            {
                case "OK":
                    break;
                case "FAIL":
                    throw new ZapException(Resources.CallActionFailedResult);
                default:
                    throw new ZapException(Resources.CallActionUnknownResult);
            }
        }

        /// <summary>
        /// Calls a JSON action API method in ZAP and returns it result deserialized.
        /// </summary>
        /// <typeparam name="T">Type to deserialize the result to.</typeparam>
        /// <param name="method">The name of the API method.</param>
        /// <param name="takeValueFromProperty">Optional JSON property name to take as the input for deserialization.</param>
        /// <param name="parameters">Optional parameters to send to the API method.</param>
        /// <returns>Deserialized API result.</returns>
        protected T CallAction<T>(string method, string takeValueFromProperty = null, IDictionary<string, object> parameters = null)
        {
            var result = CallJsonApi(CallType.Action, method, parameters);
            if (takeValueFromProperty != null)
                result = result[takeValueFromProperty];
            if (result == null)
                throw new ZapException(Resources.CallActionUnknownResult);
            return result.ToObject<T>();
        }
        
        /// <summary>
        /// Call an "other" API method in ZAP and return it's result.
        /// </summary>
        /// <param name="method">The name of the API method.</param>
        /// <param name="parameters">Optional parameters to send to the API method.</param>
        /// <returns>The result of the API call.</returns>
        protected string CallOther(string method, IDictionary<string, object> parameters = null)
        {
            return CallApi(DataType.Other, CallType.Other, method, parameters);
        }

        /// <summary>
        /// Calls an "other" API method in ZAP and return it's result as binary data.
        /// </summary>
        /// <param name="method">The name of the API method.</param>
        /// <param name="parameters">Optional parameters to send to the API method.</param>
        /// <returns>The result of the API call as binary data.</returns>
        protected byte[] CallOtherData(string method, IDictionary<string, object> parameters = null)
        {
            parameters = AddApiKey(parameters);

            var url = ZapApi.BuildRequestUrl(DataType.Other, ComponentName, CallType.Other, method, parameters);
            var result = _httpClient.DownloadData(url);

            if (!result.Any())
                throw new ZapException(Resources.ResultFromServerWasEmpty);

            return result;
        }

        /// <summary>
        /// Parses a string with a JSON-formatted list to a string list.
        /// </summary>
        /// <param name="input">The JSON-formatted list.</param>
        /// <returns>String list.</returns>
        protected IEnumerable<string> ParseJsonListString(string input)
        {
            if (Regex.IsMatch(input, @"^\[.*\]$"))
            {
                var contents = input.Substring(1, input.Length - 2);
                return contents
                    .Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Trim());
            }
            else
            {
                return new List<string>();
            }
        }

        private IDictionary<string, object> AddApiKey(IDictionary<string, object> parameters)
        {
            parameters = parameters ?? new Parameters();
            if (!string.IsNullOrEmpty(_zapProcess.ApiKey))
                parameters.Add("apikey", _zapProcess.ApiKey);
            return parameters;
        }

        private JToken CallJsonApi(CallType callType, string method, IDictionary<string, object> parameters)
        {
            var result = CallApi(DataType.Json, callType, method, parameters);
            var json = JToken.Parse(result);

            if (json["code"] != null && json["message"] != null)
                throw new ZapException(Resources.CallApiFailedResult,
                    json["code"].ToString(), json["message"].ToString());

            return json;
        }

        private string CallApi(DataType dataType, CallType callType, string method, IDictionary<string, object> parameters)
        {
            parameters = AddApiKey(parameters);
            var url = ZapApi.BuildRequestUrl(dataType, ComponentName, callType, method, parameters);
            var result = _httpClient.DownloadString(url);

            if (string.IsNullOrEmpty(result))
                throw new ZapException(Resources.ResultFromServerWasEmpty);

            return result;
        }
    }
}
