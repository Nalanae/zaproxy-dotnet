using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Security.AntiXss;
using ZAProxy.Components;

namespace ZAProxy
{
    public class ClientApi
    {
        public AntiCSRF AntiCSRF { get; private set; }

        public ClientApi(string apiKey = null)
        {
            AntiCSRF = new AntiCSRF(apiKey);
        }

        public static string BuildRequestUri(DataType dataType, string component, CallType callType, string method, IDictionary<string, object> parameters)
        {
            var urlPath = $"http://zap/{dataType.ToString().ToLower()}/{component}/{callType.ToString().ToLower()}/{method}/";
            if (parameters != null && parameters.Any())
            {
                var encodedParameterParts = parameters.Select(
                    p => AntiXssEncoder.UrlEncode(p.Key) + "=" + AntiXssEncoder.UrlEncode((p.Value ?? "").ToString()));
                var queryString = string.Join("&", encodedParameterParts);
                urlPath = $"{urlPath}?{queryString}";
            }
            return urlPath;
        }
    }
}
