using Moq;
using Moq.Language.Flow;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZAProxy.Components;
using ZAProxy.Infrastructure;

namespace ZAProxy.Tests
{
    public static class MockExtensions
    {
        public static ISetup<IHttpClient, string> SetupApiCall(this Mock<IHttpClient> httpClientMock, ComponentBase component, CallType callType, string method, IDictionary<string, object> parameters = null)
        {
            if (callType == CallType.Action)
            {
                parameters = parameters ?? new Dictionary<string, object>();
                parameters.Add("apikey", component.ApiKey);
            }
            var url = ClientApi.BuildRequestUri(DataType.Json, component.ComponentName, callType, method, parameters);
            return httpClientMock.Setup(m => m.DownloadString(url));
        }

        public static IReturnsResult<IHttpClient> ReturnsOkResult(this ISetup<IHttpClient, string> httpClientMockSetupFlow)
        {
            return httpClientMockSetupFlow.Returns(
                new JObject(
                    new JProperty("Result", "OK"))
                .ToString());
        }

        public static string ToJsonStringList(this IEnumerable<string> values)
        {
            return $"[{string.Join(", ", values)}]";
        }
    }
}
