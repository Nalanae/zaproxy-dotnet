using System.Collections.Generic;
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
    [Trait("Component", "Users")]
    public class UsersComponentTests
    {
        [Theory, AutoTestData]
        public void ComponentName(
            [Greedy]UsersComponent sut)
        {
            // ACT
            var result = sut.ComponentName;

            // ASSERT
            result.Should().Be("users");
        }

        #region Views

        [Theory, AutoTestData]
        public void GetAuthenticationCredentials(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]UsersComponent sut,
            int contextId,
            int userId,
            AuthenticationCredentials authenticationCredentials)
        {
            // ARRANGE
            var json = JObject.FromObject(authenticationCredentials);
            httpClientMock.SetupApiCall(sut, CallType.View, "getAuthenticationCredentials",
                new Parameters
                {
                    { "contextId", contextId },
                    { "userId", userId }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetAuthenticationCredentials(contextId, userId);

            // ASSERT
            result.ShouldBeEquivalentTo(authenticationCredentials);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetAuthenticationCredentialsConfigParameters(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]UsersComponent sut,
            int contextId,
            IEnumerable<AuthenticationConfigParameter> authenticationConfigParameters)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("methodConfigParams", JArray.FromObject(authenticationConfigParameters)));
            httpClientMock.SetupApiCall(sut, CallType.View, "getAuthenticationCredentialsConfigParams",
                new Parameters
                {
                    { "contextId", contextId }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetAuthenticationCredentialsConfigParameters(contextId);

            // ASSERT
            result.ShouldBeEquivalentTo(authenticationConfigParameters);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetUser(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]UsersComponent sut,
            int contextId,
            int userId,
            User user)
        {
            // ARRANGE
            var json = JObject.FromObject(user);
            httpClientMock.SetupApiCall(sut, CallType.View, "getUserById",
                new Parameters
                {
                    { "contextId", contextId },
                    { "userId", userId }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetUser(contextId, userId);

            // ASSERT
            result.ShouldBeEquivalentTo(user);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetUsers(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]UsersComponent sut,
            int contextId,
            IEnumerable<User> users)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("usersList", JArray.FromObject(users)));
            httpClientMock.SetupApiCall(sut, CallType.View, "usersList",
                new Parameters
                {
                    { "contextId", contextId }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetUsers(contextId);

            // ASSERT
            result.ShouldBeEquivalentTo(users);
            httpClientMock.Verify();
        }

        #endregion

        #region Actions

        [Theory, AutoTestData]
        public void CreateUser(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]UsersComponent sut,
            int contextId,
            string name,
            int userId)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("userId", userId));
            httpClientMock.SetupApiCall(sut, CallType.Action, "newUser",
                new Parameters
                {
                    { "contextId", contextId },
                    { "name", name }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.CreateUser(contextId, name);

            // ASSERT
            result.Should().Be(userId);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void RemoveUser(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]UsersComponent sut,
            int contextId,
            int userId)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "removeUser",
                new Parameters
                {
                    { "contextId", contextId },
                    { "userId", userId }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.RemoveUser(contextId, userId);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetAuthenticationCredentials(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]UsersComponent sut,
            int contextId,
            int userId,
            AuthenticationCredentials authenticationCredentials)
        {
            // ARRANGE
            var parameters = new Parameters
            {
                { "contextId", contextId },
                { "userId", userId }
            };
            foreach (var parameter in authenticationCredentials.Parameters)
                parameters.Add(parameter.Key, parameter.Value);
            httpClientMock.SetupApiCall(sut, CallType.Action, "setAuthenticationCredentials", parameters)
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetAuthenticationCredentials(contextId, userId, authenticationCredentials);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetUserEnabled(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]UsersComponent sut,
            int contextId,
            int userId,
            bool enabled)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setUserEnabled",
                new Parameters
                {
                    { "contextId", contextId },
                    { "userId", userId },
                    { "enabled", enabled }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetUserEnabled(contextId, userId, enabled);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetUserName(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]UsersComponent sut,
            int contextId,
            int userId,
            string name)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setUserName",
                new Parameters
                {
                    { "contextId", contextId },
                    { "userId", userId },
                    { "name", name }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetUserName(contextId, userId, name);

            // ASSERT
            httpClientMock.Verify();
        }

        #endregion
    }
}
