using System.Collections.Generic;
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
    [Trait("Component", "Reveal")]
    public class RevealComponentTests
    {
        [Theory, AutoTestData]
        public void ComponentName(
            [Greedy]RevealComponent sut)
        {
            // ACT
            var result = sut.ComponentName;

            // ASSERT
            result.Should().Be("reveal");
        }

        #region Views

        [Theory, AutoTestData]
        public void IsRevealEnabled(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]RevealComponent sut,
            bool isEnabled)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("reveal", isEnabled));
            httpClientMock.SetupApiCall(sut, CallType.View, "reveal")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.IsRevealEnabled();

            // ASSERT
            result.Should().Be(isEnabled);
            httpClientMock.Verify();
        }

        #endregion

        #region Actions

        [Theory, AutoTestData]
        public void SetRevealEnabled(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]RevealComponent sut,
            bool enabled)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setReveal",
                new Parameters
                {
                    { "reveal", enabled }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetRevealEnabled(enabled);

            // ASSERT
            httpClientMock.Verify();
        }

        #endregion
    }
}
