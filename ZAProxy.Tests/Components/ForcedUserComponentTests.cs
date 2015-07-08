using FluentAssertions;
using Moq;
using Newtonsoft.Json.Linq;
using Ploeh.AutoFixture.Xunit2;
using Xunit;
using ZAProxy.Components;
using ZAProxy.Infrastructure;
using ZAProxy.Tests.TestUtils;

namespace ZAProxy.Tests.Components
{
    [Trait("Component", "ForcedUser")]
    public class ForcedUserComponentTests
    {
        [Theory, AutoTestData]
        public void ComponentName(
            [Greedy]ForcedUserComponent sut)
        {
            // ACT
            var result = sut.ComponentName;

            // ASSERT
            result.Should().Be("forcedUser");
        }

        #region Views

        [Theory, AutoTestData]
        public void GetForcedUser(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ForcedUserComponent sut,
            int contextId,
            int userId)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("forcedUserId", userId));
            httpClientMock.SetupApiCall(sut, CallType.View, "getForcedUser",
                new Parameters
                {
                    { "contextId", contextId }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetForcedUser(contextId);

            // ASSERT
            result.Should().Be(userId);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void IsForcedUserModeEnabled(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ForcedUserComponent sut,
            bool enabled)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("forcedModeEnabled", enabled));
            httpClientMock.SetupApiCall(sut, CallType.View, "isForcedUserModeEnabled")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.IsForcedUserModeEnabled();

            // ASSERT
            result.Should().Be(enabled);
            httpClientMock.Verify();
        }

        #endregion

        #region Actions

        [Theory, AutoTestData]
        public void SetForcedUser(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ForcedUserComponent sut,
            int contextId,
            int userId)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setForcedUser",
                new Parameters
                {
                    { "contextId", contextId },
                    { "userId", userId }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetForcedUser(contextId, userId);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetForcedUserModeEnabled(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ForcedUserComponent sut,
            bool enabled)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setForcedUserModeEnabled",
                new Parameters
                {
                    { "boolean", enabled }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetForcedUserModeEnabled(enabled);

            // ASSERT
            httpClientMock.Verify();
        }

        #endregion
    }
}
