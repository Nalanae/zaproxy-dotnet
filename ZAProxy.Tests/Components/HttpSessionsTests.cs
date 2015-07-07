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
    [Trait("Component", "HttpSessions")]
    public class HttpSessionsTests
    {
        [Theory, AutoTestData]
        public void ComponentName(
            [Greedy]ZAProxy.Components.HttpSessions sut)
        {
            // ACT
            var result = sut.ComponentName;

            // ASSERT
            result.Should().Be("httpSessions");
        }

        #region Views

        [Theory, AutoTestData]
        public void GetActiveSession(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ZAProxy.Components.HttpSessions sut,
            string site,
            string activeSession)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("active_session", activeSession));
            httpClientMock.SetupApiCall(sut, CallType.View, "activeSession",
                new Parameters
                {
                    { "site", site }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetActiveSession(site);

            // ASSERT
            result.Should().Be(activeSession);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetSessionTokens(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ZAProxy.Components.HttpSessions sut,
            string site,
            IEnumerable<string> sessionTokens)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("session_tokens", JArray.FromObject(sessionTokens)));
            httpClientMock.SetupApiCall(sut, CallType.View, "sessionTokens",
                new Parameters
                {
                    { "site", site }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetSessionTokens(site);

            // ASSERT
            result.ShouldBeEquivalentTo(sessionTokens);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetSessions(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ZAProxy.Components.HttpSessions sut,
            string site,
            string name,
            IEnumerable<HttpSession> sessions)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("sessions", JArray.FromObject(sessions)));
            httpClientMock.SetupApiCall(sut, CallType.View, "sessions",
                new Parameters
                {
                    { "site", site },
                    { "session", name }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetSessions(site, name);

            // ASSERT
            result.ShouldBeEquivalentTo(sessions);
            httpClientMock.Verify();
        }

        #endregion

        #region Actions

        [Theory, AutoTestData]
        public void AddSessionToken(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ZAProxy.Components.HttpSessions sut,
            string site,
            string name)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "addSessionToken",
                new Parameters
                {
                    { "site", site },
                    { "sessionToken", name }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.AddSessionToken(site, name);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void CreateEmptySession(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ZAProxy.Components.HttpSessions sut,
            string site,
            string name)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "createEmptySession",
                new Parameters
                {
                    { "site", site },
                    { "session", name }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.CreateEmptySession(site, name);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void RemoveSession(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ZAProxy.Components.HttpSessions sut,
            string site,
            string name)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "removeSession",
                new Parameters
                {
                    { "site", site },
                    { "session", name }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.RemoveSession(site, name);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void RemoveSessionToken(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ZAProxy.Components.HttpSessions sut,
            string site,
            string name)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "removeSessionToken",
                new Parameters
                {
                    { "site", site },
                    { "sessionToken", name }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.RemoveSessionToken(site, name);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void RenameSession(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ZAProxy.Components.HttpSessions sut,
            string site,
            string oldName,
            string newName)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "renameSession",
                new Parameters
                {
                    { "site", site },
                    { "oldSessionName", oldName },
                    { "newSessionName", newName }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.RenameSession(site, oldName, newName);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetActiveSession(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ZAProxy.Components.HttpSessions sut,
            string site,
            string name)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setActiveSession",
                new Parameters
                {
                    { "site", site },
                    { "session", name }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetActiveSession(site, name);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetSessionTokenValue(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ZAProxy.Components.HttpSessions sut,
            string site,
            string sessionName,
            string sessionTokenName,
            string value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setSessionTokenValue",
                new Parameters
                {
                    { "site", site },
                    { "session", sessionName },
                    { "sessionToken", sessionTokenName },
                    { "tokenValue", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetSessionTokenValue(site, sessionName, sessionTokenName, value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void UnsetActiveSession(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ZAProxy.Components.HttpSessions sut,
            string site)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "unsetActiveSession",
                new Parameters
                {
                    { "site", site }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.UnsetActiveSession(site);

            // ASSERT
            httpClientMock.Verify();
        }

        #endregion
    }
}
