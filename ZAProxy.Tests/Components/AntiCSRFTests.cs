using Moq;
using Newtonsoft.Json.Linq;
using Ploeh.AutoFixture.Xunit2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ZAProxy.Components;
using ZAProxy.Infrastructure;
using FluentAssertions;

namespace ZAProxy.Tests.Components
{
    public class AntiCSRFTests
    {
        [Theory, AutoMoqData]
        public void GetOptionTokenNames(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]AntiCSRF sut,
            IEnumerable<string> tokenNames)
        {
            // ASSIGN
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

        [Theory, AutoMoqData]
        public void AddOptionTokenName(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]AntiCSRF sut,
            string tokenName)
        {
            // ASSIGN
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

        [Theory, AutoMoqData]
        public void RemoveOptionTokenName(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]AntiCSRF sut,
            string tokenName)
        {
            // ASSIGN
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
    }
}
