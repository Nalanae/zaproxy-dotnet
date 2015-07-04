using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using Newtonsoft.Json.Linq;
using Ploeh.AutoFixture.Xunit2;
using Xunit;
using ZAProxy.Components;
using ZAProxy.Infrastructure;

namespace ZAProxy.Tests.Components
{
    public class AntiCSRFTests
    {
        #region Views

        [Theory, AutoTestData]
        public void GetOptionTokenNames(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]AntiCSRF sut,
            IEnumerable<string> tokenNames)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("TokensNames", tokenNames.ToJsonStringList()));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionTokensNames")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionTokenNames();

            // ASSERT
            result.Should().BeEquivalentTo(tokenNames);
            httpClientMock.Verify();
        }

        #endregion

        #region Actions

        [Theory, AutoTestData]
        public void AddOptionTokenName(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]AntiCSRF sut,
            string tokenName)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "addOptionToken",
                new Dictionary<string, object>
                {
                    { "String", tokenName }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.AddOptionTokenName(tokenName);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void RemoveOptionTokenName(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]AntiCSRF sut,
            string tokenName)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "removeOptionToken",
                new Dictionary<string, object>
                {
                    { "String", tokenName }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.RemoveOptionTokenName(tokenName);

            // ASSERT
            httpClientMock.Verify();
        }

        #endregion

        #region Others

        [Theory, AutoTestData]
        public void GenerateForm(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]AntiCSRF sut,
            int messageId,
            string form)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Other, "genForm",
                new Dictionary<string, object>
                {
                    { "hrefId", messageId }
                }, DataType.Other)
                .Returns(form);

            // ACT
            var result = sut.GenerateForm(messageId);

            // ASSERT
            result.Should().Be(form);
            httpClientMock.Verify();
        }

        #endregion
    }
}
