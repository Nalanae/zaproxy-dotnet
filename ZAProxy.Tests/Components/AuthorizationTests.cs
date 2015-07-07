using System;
using FluentAssertions;
using Moq;
using Newtonsoft.Json.Linq;
using Ploeh.AutoFixture.Xunit2;
using Xunit;
using ZAProxy.Infrastructure;
using ZAProxy.Schema;
using ZAProxy.Tests.TestUtils;

namespace ZAProxy.Tests.Components
{
    [Trait("Component", "Authorization")]
    public class AuthorizationTests
    {
        [Theory, AutoTestData]
        public void ComponentName(
            [Greedy]ZAProxy.Components.Authorization sut)
        {
            // ACT
            var result = sut.ComponentName;

            // ASSERT
            result.Should().Be("authorization");
        }

        #region Views

        [Theory, AutoTestData]
        public void GetAuthorizationDetectionMethod(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ZAProxy.Components.Authorization sut,
            int contextId,
            BasicAuthorizationDetectionMethod basicAuthorizationDetectionMethod)
        {
            // ARRANGE
            var json = JObject.FromObject(basicAuthorizationDetectionMethod);
            json.Add("methodType", "basic");
            httpClientMock.SetupApiCall(sut, CallType.View, "getAuthorizationDetectionMethod",
                new Parameters
                {
                    { "contextId", contextId }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetAuthorizationDetectionMethod(contextId);

            // ASSERT
            result.ShouldBeEquivalentTo(basicAuthorizationDetectionMethod);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetAuthorizationDetectionMethod_UnsupportedMethodType(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ZAProxy.Components.Authorization sut,
            int contextId)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("methodType", "alternative"));
            httpClientMock.SetupApiCall(sut, CallType.View, "getAuthorizationDetectionMethod",
                new Parameters
                {
                    { "contextId", contextId }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            Action act = () => sut.GetAuthorizationDetectionMethod(contextId);

            // ASSERT
            act.ShouldThrow<ZapException>().WithMessage(Resources.UnsupportedAuthorizationDetectionMethod);
            httpClientMock.Verify();
        }

        #endregion

        #region Actions

        public void SetBasicAuthorizationDetectionMethod(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ZAProxy.Components.Authorization sut,
            int contextId,
            BasicAuthorizationDetectionMethod basicAuthorizationDetectionMethod)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setBasicAuthorizationDetectionMethod",
                new Parameters
                {
                    { "contextId", contextId },
                    { "headerRegex", basicAuthorizationDetectionMethod.HeaderRegex },
                    { "bodyRegex", basicAuthorizationDetectionMethod.BodyRegex },
                    { "statusCode", basicAuthorizationDetectionMethod.StatusCode },
                    { "logicalOperator", basicAuthorizationDetectionMethod.LogicalOperator }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetBasicAuthorizationDetectionMethod(contextId, basicAuthorizationDetectionMethod);

            // ASSERT
            httpClientMock.Verify();
        }

        #endregion
    }
}
