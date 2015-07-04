using System.Collections.Generic;
using Moq.Language;
using Moq.Language.Flow;
using Newtonsoft.Json.Linq;
using ZAProxy;
using ZAProxy.Components;
using ZAProxy.Infrastructure;

namespace Moq
{
    public static class MoqExtensions
    {
        public static ISetup<IHttpClient, string> SetupApiCall(this Mock<IHttpClient> httpClientMock, ComponentBase component, CallType callType, string method, IDictionary<string, object> parameters = null, DataType? dataType = null, string apiKey = null)
        {
            dataType = dataType ?? DataType.Json;
            if (apiKey != null)
            {
                parameters = parameters ?? new Parameters();
                parameters.Add("apikey", apiKey);
            }
            var url = ZapApi.BuildRequestUrl(dataType.Value, component.ComponentName, callType, method, parameters);
            return httpClientMock.Setup(m => m.DownloadString(url));
        }

        public static ISetup<IHttpClient, byte[]> SetupOtherDataApiCall(this Mock<IHttpClient> httpClientMock, ComponentBase component, string method, IDictionary<string, object> parameters = null, string apiKey = null)
        {
            if (apiKey != null)
            {
                parameters = parameters ?? new Parameters();
                parameters.Add("apikey", apiKey);
            }
            var url = ZapApi.BuildRequestUrl(DataType.Other, component.ComponentName, CallType.Other, method, parameters);
            return httpClientMock.Setup(m => m.DownloadData(url));
        }

        public static IReturnsResult<IHttpClient> ReturnsOkResult(this ISetup<IHttpClient, string> httpClientMockSetupFlow)
        {
            return httpClientMockSetupFlow.Returns(
                new JObject(
                    new JProperty("Result", "OK"))
                .ToString());
        }

        public static IReturnsResult<TMock> ReturnsNull<TMock, TProperty>(this IReturnsGetter<TMock, TProperty> returnsGetter)
            where TMock : class
            where TProperty : class
        {
            return returnsGetter.Returns((TProperty)null);
        }

        public static IReturnsResult<TMock> ReturnsNull<TMock, TProperty>(this IReturns<TMock, TProperty> returns)
            where TMock : class
            where TProperty : class
        {
            return returns.Returns((TProperty)null);
        }
    }
}
