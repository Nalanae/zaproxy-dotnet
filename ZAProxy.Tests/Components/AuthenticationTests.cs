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
    [Trait("Component", "Authentication")]
    public class AuthenticationTests
    {
        [Theory, AutoTestData]
        public void ComponentName(
            [Greedy]Authentication sut)
        {
            // ACT
            var result = sut.ComponentName;

            // ASSERT
            result.Should().Be("authentication");
        }

        #region Views

        [Theory, AutoTestData]
        public void GetAuthenticationMethod(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Authentication sut,
            int contextId,
            AuthenticationMethod authenticationMethod)
        {
            // ARRANGE
            var json = JObject.FromObject(authenticationMethod);
            httpClientMock.SetupApiCall(sut, CallType.View, "getAuthenticationMethod",
                new Parameters
                {
                    { "contextId", contextId }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetAuthenticationMethod(contextId);

            // ASSERT
            result.ShouldBeEquivalentTo(authenticationMethod);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetAuthenticationMethodConfigParameters(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Authentication sut,
            string authenticationMethodName,
            IEnumerable<AuthenticationConfigParameter> authenticationMethodConfigParameters)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("methodConfigParams", JArray.FromObject(authenticationMethodConfigParameters)));
            httpClientMock.SetupApiCall(sut, CallType.View, "getAuthenticationMethodConfigParams",
                new Parameters
                {
                    { "authMethodName", authenticationMethodName }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetAuthenticationMethodConfigParameters(authenticationMethodName);

            // ASSERT
            result.ShouldBeEquivalentTo(authenticationMethodConfigParameters);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetLoggedInIndicator(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Authentication sut,
            int contextId,
            string regex)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("logged_in_regex", regex));
            httpClientMock.SetupApiCall(sut, CallType.View, "getLoggedInIndicator",
                new Parameters
                {
                    { "contextId", contextId }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetLoggedInIndicator(contextId);

            // ASSERT
            result.Should().Be(regex);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetLoggedOutIndicator(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Authentication sut,
            int contextId,
            string regex)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("logged_out_regex", regex));
            httpClientMock.SetupApiCall(sut, CallType.View, "getLoggedOutIndicator",
                new Parameters
                {
                    { "contextId", contextId }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetLoggedOutIndicator(contextId);

            // ASSERT
            result.Should().Be(regex);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetSupportedAuthenticationMethods(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Authentication sut,
            IEnumerable<string> supportedAuthenticationMethods)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("supportedMethods", JArray.FromObject(supportedAuthenticationMethods)));
            httpClientMock.SetupApiCall(sut, CallType.View, "getSupportedAuthenticationMethods")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetSupportedAuthenticationMethods();

            // ASSERT
            result.ShouldBeEquivalentTo(supportedAuthenticationMethods);
            httpClientMock.Verify();
        }

        #endregion

        #region Actions

        [Theory, AutoTestData]
        public void SetAuthenticationMethod(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Authentication sut,
            int contextId,
            AuthenticationMethod authenticationMethod)
        {
            // ARRANGE
            var parameters = new Parameters
            {
                    { "contextId", contextId },
                    { "authMethodName", authenticationMethod.MethodName }
            };
            foreach (var parameter in authenticationMethod.Parameters)
                parameters.Add(parameter.Key, parameter.Value);
            httpClientMock.SetupApiCall(sut, CallType.Action, "setAuthenticationMethod", parameters)
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetAuthenticationMethod(contextId, authenticationMethod);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetLoggedInIndicator(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Authentication sut,
            int contextId,
            string indicatorRegex)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setLoggedInIndicator",
                new Parameters
                {
                    { "contextId", contextId },
                    { "loggedInIndicatorRegex", indicatorRegex },
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetLoggedInIndicator(contextId, indicatorRegex);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetLoggedOutIndicator(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Authentication sut,
            int contextId,
            string indicatorRegex)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setLoggedOutIndicator",
                new Parameters
                {
                    { "contextId", contextId },
                    { "loggedOutIndicatorRegex", indicatorRegex },
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetLoggedOutIndicator(contextId, indicatorRegex);

            // ASSERT
            httpClientMock.Verify();
        }

        #endregion
    }
}
