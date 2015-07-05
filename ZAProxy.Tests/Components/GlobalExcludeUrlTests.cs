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
    [Trait("Component", "GlobalExcludeUrl")]
    public class GlobalExcludeUrlTests
    {
        [Theory, AutoTestData]
        public void ComponentName(
            [Greedy]GlobalExcludeUrl sut)
        {
            // ACT
            var result = sut.ComponentName;

            // ASSERT
            result.Should().Be("globalexcludeurl");
        }

        #region Views

        [Theory, AutoTestData]
        public void GetOptionTokens(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]GlobalExcludeUrl sut,
            IEnumerable<string> tokens)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("Tokens", tokens.ToJsonStringList()));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionTokens")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
#pragma warning disable CS0618 // Type or member is obsolete
            var result = sut.GetOptionTokens();
#pragma warning restore CS0618 // Type or member is obsolete

            // ASSERT
            result.ShouldBeEquivalentTo(tokens);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionTokensNames(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]GlobalExcludeUrl sut,
            IEnumerable<string> tokensNames)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("TokensNames", tokensNames.ToJsonStringList()));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionTokensNames")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionTokensNames();

            // ASSERT
            result.ShouldBeEquivalentTo(tokensNames);
            httpClientMock.Verify();
        }

        #endregion

        #region Actions

        [Theory, AutoTestData]
        public void AddOptionToken(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]GlobalExcludeUrl sut,
            string value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "addOptionToken",
                new Parameters
                {
                    { "String", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.AddOptionToken(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void RemoveOptionToken(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]GlobalExcludeUrl sut,
            string value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "removeOptionToken",
                new Parameters
                {
                    { "String", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.RemoveOptionToken(value);

            // ASSERT
            httpClientMock.Verify();
        }

        #endregion

        #region Others

        [Theory, AutoTestData]
        public void GenerateForm(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]GlobalExcludeUrl sut,
            int messageId,
            string form)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Other, "genForm",
                new Parameters
                {
                    { "hrefId", messageId }
                }, DataType.Other)
                .Returns(form);

            // ACT
#pragma warning disable CS0618 // Type or member is obsolete
            var result = sut.GenerateForm(messageId);
#pragma warning restore CS0618 // Type or member is obsolete

            // ASSERT
            result.Should().Be(form);
            httpClientMock.Verify();
        }

        #endregion
    }
}
