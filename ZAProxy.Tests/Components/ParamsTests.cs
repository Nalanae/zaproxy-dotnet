using System.Collections.Generic;
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
    [Trait("Component", "Params")]
    public class ParamsTests
    {
        [Theory, AutoTestData]
        public void ComponentName(
            [Greedy]ZAProxy.Components.Params sut)
        {
            // ACT
            var result = sut.ComponentName;

            // ASSERT
            result.Should().Be("params");
        }

        #region Views

        [Theory, AutoTestData]
        public void GetParameters(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ZAProxy.Components.Params sut,
            string site,
            IEnumerable<HttpParameter> parameters)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("Parameters", JArray.FromObject(parameters)));
            httpClientMock.SetupApiCall(sut, CallType.View, "params",
                new Parameters
                {
                    { "site", site }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetParameters(site);

            // ASSERT
            result.ShouldBeEquivalentTo(parameters);
            httpClientMock.Verify();
        }

        #endregion
    }
}
