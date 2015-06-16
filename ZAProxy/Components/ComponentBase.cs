using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Security.AntiXss;
using ZAProxy.Infrastructure;

namespace ZAProxy.Components
{
    public abstract class ComponentBase
    {
        private readonly IHttpClient _httpClient;

        internal ComponentBase(IHttpClient httpClient, string apiKey, string componentUrlName)
        {
            _httpClient = httpClient ?? new HttpClient();
            ApiKey = apiKey;
            ComponentName = componentUrlName;
        }

        public string ApiKey { get; private set; }
        public string ComponentName { get; private set; }

        protected T CallView<T>(string method, string takeValueFromProperty = null, IDictionary<string, object> parameters = null)
        {
            parameters = parameters ?? new Dictionary<string, object>();
            var result = CallApi(CallType.View, method, parameters);
            if (takeValueFromProperty != null)
                result = result[takeValueFromProperty];
            if (result == null)
                throw new ClientApiException(Resources.CallViewUnknownResult);
            return result.ToObject<T>();
        }

        protected void CallAction(string method, IDictionary<string, object> parameters = null)
        {
            var result = CallAction<string>(method, "Result", parameters);
            if (result == null)
                throw new ClientApiException(Resources.CallActionUnknownResult);
            switch(result.ToString())
            {
                case "OK":
                    break;
                case "FAIL":
                    throw new ClientApiException(Resources.CallActionFailedResult);
                default:
                    throw new ClientApiException(Resources.CallActionUnknownResult);
            }
        }

        protected T CallAction<T>(string method, string takeValueFromProperty = null, IDictionary<string, object> parameters = null)
        {
            parameters = parameters ?? new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(ApiKey))
                parameters.Add("apikey", ApiKey);
            var result = CallApi(CallType.Action, method, parameters);
            if (takeValueFromProperty != null)
                result = result[takeValueFromProperty];
            if (result == null)
                throw new ClientApiException(Resources.CallActionUnknownResult);
            return result.ToObject<T>();
        }

        protected IEnumerable<string> ParseStringList(string input)
        {
            if (Regex.IsMatch(input, @"^\[.*\]$"))
            {
                var contents = input.Substring(1, input.Length - 2);
                return contents.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                return new List<string>();
            }
        }

        private JToken CallApi(CallType callType, string method, IDictionary<string, object> parameters)
        {
            var url = ClientApi.BuildRequestUri(DataType.Json, ComponentName, callType, method, parameters);
            var result = _httpClient.DownloadString(url);
            var json = JToken.Parse(result);
            if (json["code"] != null && json["message"] != null)
                throw new ClientApiException(Resources.CallApiFailedResult,
                    json["code"].ToString(), json["message"].ToString());
            return json;
        }
    }
}
