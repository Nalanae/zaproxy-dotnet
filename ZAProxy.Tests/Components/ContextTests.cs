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
    [Trait("Component", "Context")]
    public class ContextTests
    {
        [Theory, AutoTestData]
        public void ComponentName(
            [Greedy]Context sut)
        {
            // ACT
            var result = sut.ComponentName;

            // ASSERT
            result.Should().Be("context");
        }

        #region Views

        [Theory, AutoTestData]
        public void GetContext(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Context sut,
            string name,
            Schema.Context context)
        {
            // ARRANGE
            var json = JObject.FromObject(context);
            httpClientMock.SetupApiCall(sut, CallType.View, "context",
                new Dictionary<string, object>
                {
                    { "contextName", name }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetContext(name);

            // ASSERT
            result.ShouldBeEquivalentTo(context);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetContextList(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Context sut,
            string name,
            IEnumerable<string> contextList)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("contextList", contextList.ToJsonStringList()));
            httpClientMock.SetupApiCall(sut, CallType.View, "contextList")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetContextList();

            // ASSERT
            result.ShouldBeEquivalentTo(contextList);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetExcludedRegexes(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Context sut,
            string name,
            IEnumerable<string> excludedRegexes)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("excludeRegexs", excludedRegexes.ToJsonStringList()));
            httpClientMock.SetupApiCall(sut, CallType.View, "excludeRegexs",
                new Dictionary<string, object>
                {
                    { "contextName", name }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetExcludedRegexes(name);

            // ASSERT
            result.ShouldBeEquivalentTo(excludedRegexes);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetIncludedRegexes(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Context sut,
            string name,
            IEnumerable<string> includedRegexes)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("includeRegexs", includedRegexes.ToJsonStringList()));
            httpClientMock.SetupApiCall(sut, CallType.View, "includeRegexs",
                new Dictionary<string, object>
                {
                    { "contextName", name }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetIncludedRegexes(name);

            // ASSERT
            result.ShouldBeEquivalentTo(includedRegexes);
            httpClientMock.Verify();
        }

        #endregion

        #region Actions

        [Theory, AutoTestData]
        public void ExcludeFromContext(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Context sut,
            string name,
            string regex)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "excludeFromContext",
                new Dictionary<string, object>
                {
                    { "contextName", name },
                    { "regex", regex }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.ExcludeFromContext(name, regex);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void ExportContext(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Context sut,
            string name,
            string filePath)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "exportContext",
                new Dictionary<string, object>
                {
                    { "contextName", name },
                    { "contextFile", filePath }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.ExportContext(name, filePath);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void ImportContext(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Context sut,
            string filePath)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "importContext",
                new Dictionary<string, object>
                {
                    { "contextFile", filePath }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.ImportContext(filePath);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void IncludeInContext(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Context sut,
            string name,
            string regex)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "includeInContext",
                new Dictionary<string, object>
                {
                    { "contextName", name },
                    { "regex", regex }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.IncludeInContext(name, regex);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void NewContext(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Context sut,
            string name,
            int contextId)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("contextId", contextId));
            httpClientMock.SetupApiCall(sut, CallType.Action, "newContext",
                new Dictionary<string, object>
                {
                    { "contextName", name }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.NewContext(name);

            // ASSERT
            result.Should().Be(contextId);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetContextInScope(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Context sut,
            string name,
            bool inScope)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setContextInScope",
                new Dictionary<string, object>
                {
                    { "contextName", name },
                    { "booleanInScope", inScope }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetContextInScope(name, inScope);

            // ASSERT
            httpClientMock.Verify();
        }

        #endregion
    }
}
