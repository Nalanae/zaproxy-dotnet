using System.Collections.Generic;
using System.Linq;
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
    [Trait("Component", "Script")]
    public class ScriptComponentTests
    {
        [Theory, AutoTestData]
        public void ComponentName(
            [Greedy]ScriptComponent sut)
        {
            // ACT
            var result = sut.ComponentName;

            // ASSERT
            result.Should().Be("script");
        }

        #region Views

        [Theory, AutoTestData]
        public void GetOptionTokenNames(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ScriptComponent sut,
            IDictionary<string, string> engines)
        {
            // ARRANGE
            var enginesStrings = engines.Select(e => $"{e.Key} , {e.Value}");
            var json = new JObject(
                new JProperty("listEngines", JArray.FromObject(enginesStrings)));
            httpClientMock.SetupApiCall(sut, CallType.View, "listEngines")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetEngines();

            // ASSERT
            result.ShouldBeEquivalentTo(engines);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetScripts(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ScriptComponent sut,
            IEnumerable<Script> scripts)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("listScripts", JArray.FromObject(scripts)));
            httpClientMock.SetupApiCall(sut, CallType.View, "listScripts")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetScripts();

            // ASSERT
            result.ShouldBeEquivalentTo(scripts);
            httpClientMock.Verify();
        }

        #endregion

        #region Actions

        [Theory, AutoTestData]
        public void DisableScript(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ScriptComponent sut,
            string name)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "disable",
                new Parameters
                {
                    { "scriptName", name }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.DisableScript(name);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void EnableScript(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ScriptComponent sut,
            string name)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "enable",
                new Parameters
                {
                    { "scriptName", name }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.EnableScript(name);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void LoadScript(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ScriptComponent sut,
            string name,
            string type,
            string engine,
            string path,
            string description)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "load",
                new Parameters
                {
                    { "scriptName", name },
                    { "scriptType", type },
                    { "scriptEngine", engine },
                    { "fileName", path },
                    { "scriptDescription", description }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.LoadScript(name, type, engine, path, description);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void RemoveScript(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ScriptComponent sut,
            string name)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "remove",
                new Parameters
                {
                    { "scriptName", name }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.RemoveScript(name);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void RunStandAloneScript(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ScriptComponent sut,
            string name)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "runStandAloneScript",
                new Parameters
                {
                    { "scriptName", name }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.RunStandAloneScript(name);

            // ASSERT
            httpClientMock.Verify();
        }

        #endregion
    }
}
