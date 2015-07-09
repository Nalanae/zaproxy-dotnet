using System.Collections.Generic;
using FluentAssertions;
using Moq;
using Newtonsoft.Json.Linq;
using Ploeh.AutoFixture.Xunit2;
using Xunit;
using ZAProxy.Components;
using ZAProxy.Infrastructure;
using ZAProxy.Schema;
using ZAProxy.Tests.TestUtils;

namespace ZAProxy.Tests.Components
{
    [Trait("Component", "SessionManagement")]
    public class SessionManagementComponentTests
    {
        [Theory, AutoTestData]
        public void ComponentName(
            [Greedy]SessionManagementComponent sut)
        {
            // ACT
            var result = sut.ComponentName;

            // ASSERT
            result.Should().Be("sessionManagement");
        }

        #region Views

        [Theory, AutoTestData]
        public void GetSessionManagementMethod(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SessionManagementComponent sut,
            int contextId,
            SessionManagementMethod sessionManagementMethod)
        {
            // ARRANGE
            var json = JObject.FromObject(sessionManagementMethod);
            httpClientMock.SetupApiCall(sut, CallType.View, "getSessionManagementMethod",
                new Parameters
                {
                    { "contextId", contextId }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetSessionManagementMethod(contextId);

            // ASSERT
            result.ShouldBeEquivalentTo(sessionManagementMethod);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetSessionManagementMethodConfigParameters(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SessionManagementComponent sut,
            string methodName,
            IEnumerable<ConfigurationParameter> configParameters)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("methodConfigParams", JArray.FromObject(configParameters)));
            httpClientMock.SetupApiCall(sut, CallType.View, "getSessionManagementMethodConfigParams",
                new Parameters
                {
                    { "methodName", methodName }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetSessionManagementMethodConfigParameters(methodName);

            // ASSERT
            result.ShouldBeEquivalentTo(configParameters);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetSupportedSessionManagementMethods(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SessionManagementComponent sut,
            IEnumerable<string> methodNames)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("supportedMethods", JArray.FromObject(methodNames)));
            httpClientMock.SetupApiCall(sut, CallType.View, "getSupportedSessionManagementMethods")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetSupportedSessionManagementMethods();

            // ASSERT
            result.ShouldBeEquivalentTo(methodNames);
            httpClientMock.Verify();
        }

        #endregion

        #region Actions

        [Theory, AutoTestData]
        public void SetSessionManagementMethod(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SessionManagementComponent sut,
            int contextId,
            SessionManagementMethod sessionManagementMethod)
        {
            // ARRANGE
            var parameters = new Parameters
            {
                    { "contextId", contextId },
                    { "methodName", sessionManagementMethod.MethodName }
            };
            foreach (var parameter in sessionManagementMethod.Parameters)
                parameters.Add(parameter.Key, parameter.Value);
            httpClientMock.SetupApiCall(sut, CallType.Action, "setSessionManagementMethod", parameters)
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetSessionManagementMethod(contextId, sessionManagementMethod);

            // ASSERT
            httpClientMock.Verify();
        }

        #endregion
    }
}
