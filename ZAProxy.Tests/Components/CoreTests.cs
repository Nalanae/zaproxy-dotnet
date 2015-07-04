using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using FluentAssertions;
using HttpArchive;
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
    [Trait("Component", "Core")]
    public class CoreTests
    {
        [Theory, AutoTestData]
        public void ComponentName(
            [Greedy]Core sut)
        {
            // ACT
            var result = sut.ComponentName;

            // ASSERT
            result.Should().Be("core");
        }

        #region Views

        [Theory, AutoTestData]
        public void GetAlert(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            int id,
            Alert alert)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("alert", JObject.FromObject(alert)));
            httpClientMock.SetupApiCall(sut, CallType.View, "alert",
                new Dictionary<string, object>
                {
                    { "id", id }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetAlert(id);

            // ASSERT
            result.ShouldBeEquivalentTo(alert);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetAlerts(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            string baseUrl,
            int start,
            int count,
            IEnumerable<Alert> alerts)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("alerts", JArray.FromObject(alerts)));
            httpClientMock.SetupApiCall(sut, CallType.View, "alerts",
                new Dictionary<string, object>
                {
                    { "baseurl", baseUrl },
                    { "start", start },
                    { "count", count }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetAlerts(baseUrl, start, count);

            // ASSERT
            result.ShouldBeEquivalentTo(alerts);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetExcludedFromProxy(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            IEnumerable<string> excludedFromProxy)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("excludedFromProxy", excludedFromProxy));
            httpClientMock.SetupApiCall(sut, CallType.View, "excludedFromProxy")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetExcludedFromProxy();

            // ASSERT
            result.ShouldBeEquivalentTo(excludedFromProxy);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetHomeDirectory(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            string homeDirectory)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("homeDirectory", homeDirectory));
            httpClientMock.SetupApiCall(sut, CallType.View, "homeDirectory")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetHomeDirectory();

            // ASSERT
            result.Should().Be(homeDirectory);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetHosts(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            IEnumerable<string> hosts)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("hosts", hosts));
            httpClientMock.SetupApiCall(sut, CallType.View, "hosts")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetHosts();

            // ASSERT
            result.ShouldBeEquivalentTo(hosts);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetMessage(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            int id,
            Message message)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("message", JObject.FromObject(message)));
            httpClientMock.SetupApiCall(sut, CallType.View, "message",
                new Dictionary<string, object>
                {
                    { "id", id }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetMessage(id);

            // ASSERT
            result.ShouldBeEquivalentTo(message);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetMessages(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            string baseUrl,
            int start,
            int count,
            IEnumerable<Message> messages)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("messages", JArray.FromObject(messages)));
            httpClientMock.SetupApiCall(sut, CallType.View, "messages",
                new Dictionary<string, object>
                {
                    { "baseurl", baseUrl },
                    { "start", start },
                    { "count", count }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetMessages(baseUrl, start, count);

            // ASSERT
            result.ShouldBeEquivalentTo(messages);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetNumberOfAlerts(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            string baseUrl,
            int numberOfAlerts)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("numberOfAlerts", numberOfAlerts));
            httpClientMock.SetupApiCall(sut, CallType.View, "numberOfAlerts",
                new Dictionary<string, object>
                {
                    { "baseurl", baseUrl }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetNumberOfAlerts(baseUrl);

            // ASSERT
            result.Should().Be(numberOfAlerts);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetNumberOfMessages(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            string baseUrl,
            int numberOfMessages)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("numberOfMessages", numberOfMessages));
            httpClientMock.SetupApiCall(sut, CallType.View, "numberOfMessages",
                new Dictionary<string, object>
                {
                    { "baseurl", baseUrl }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetNumberOfMessages(baseUrl);

            // ASSERT
            result.Should().Be(numberOfMessages);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionHttpState(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            string httpState)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("HttpState", httpState));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionHttpState")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionHttpState();

            // ASSERT
            result.Should().Be(httpState);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionHttpStateEnabled(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            bool httpStateEnabled)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("HttpStateEnabled", httpStateEnabled));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionHttpStateEnabled")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionHttpStateEnabled();

            // ASSERT
            result.Should().Be(httpStateEnabled);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionProxyChainName(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            string proxyChainName)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("ProxyChainName", proxyChainName));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionProxyChainName")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionProxyChainName();

            // ASSERT
            result.Should().Be(proxyChainName);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionProxyChainPassword(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            string proxyChainPassword)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("ProxyChainPassword", proxyChainPassword));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionProxyChainPassword")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionProxyChainPassword();

            // ASSERT
            result.Should().Be(proxyChainPassword);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionProxyChainPort(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            int proxyChainPort)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("ProxyChainPort", proxyChainPort));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionProxyChainPort")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionProxyChainPort();

            // ASSERT
            result.Should().Be(proxyChainPort);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionProxyChainPrompt(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            bool proxyChainPrompt)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("ProxyChainPrompt", proxyChainPrompt));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionProxyChainPrompt")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionProxyChainPrompt();

            // ASSERT
            result.Should().Be(proxyChainPrompt);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionProxyChainRealm(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            string proxyChainRealm)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("ProxyChainRealm", proxyChainRealm));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionProxyChainRealm")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionProxyChainRealm();

            // ASSERT
            result.Should().Be(proxyChainRealm);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionProxyChainSkipName(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            string proxyChainSkipName)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("ProxyChainSkipName", proxyChainSkipName));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionProxyChainSkipName")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
#pragma warning disable 0618 // Turn deprication warning off for test.
            var result = sut.GetOptionProxyChainSkipName();
#pragma warning restore 0618 // Restore deprication warning.

            // ASSERT
            result.Should().Be(proxyChainSkipName);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionProxyChainUserName(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            string proxyChainUserName)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("ProxyChainUserName", proxyChainUserName));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionProxyChainUserName")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionProxyChainUserName();

            // ASSERT
            result.Should().Be(proxyChainUserName);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionProxyExcludedDomains(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            IEnumerable<string> proxyExcludedDomains)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("ProxyExcludedDomains", proxyExcludedDomains));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionProxyExcludedDomains")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionProxyExcludedDomains();

            // ASSERT
            result.ShouldBeEquivalentTo(proxyExcludedDomains);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionProxyExcludedDomainsEnabled(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            IEnumerable<string> proxyExcludedDomainsEnabled)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("ProxyExcludedDomainsEnabled", proxyExcludedDomainsEnabled));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionProxyExcludedDomainsEnabled")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionProxyExcludedDomainsEnabled();

            // ASSERT
            result.ShouldBeEquivalentTo(proxyExcludedDomainsEnabled);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionSingleCookieRequestHeader(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            bool singleCookieRequestHeader)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("SingleCookieRequestHeader", singleCookieRequestHeader));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionSingleCookieRequestHeader")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionSingleCookieRequestHeader();

            // ASSERT
            result.Should().Be(singleCookieRequestHeader);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionTimeoutInSecs(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            int timeoutInSecs)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("TimeoutInSecs", timeoutInSecs));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionTimeoutInSecs")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionTimeoutInSecs();

            // ASSERT
            result.Should().Be(timeoutInSecs);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionUseProxyChain(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            bool useProxyChain)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("UseProxyChain", useProxyChain));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionUseProxyChain")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionUseProxyChain();

            // ASSERT
            result.Should().Be(useProxyChain);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionUseProxyChainAuth(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            bool useProxyChainAuth)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("UseProxyChainAuth", useProxyChainAuth));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionUseProxyChainAuth")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionUseProxyChainAuth();

            // ASSERT
            result.Should().Be(useProxyChainAuth);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetSites(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            IEnumerable<string> sites)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("sites", sites));
            httpClientMock.SetupApiCall(sut, CallType.View, "sites")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetSites();

            // ASSERT
            result.ShouldBeEquivalentTo(sites);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetUrls(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            IEnumerable<string> urls)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("urls", urls));
            httpClientMock.SetupApiCall(sut, CallType.View, "urls")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetUrls();

            // ASSERT
            result.ShouldBeEquivalentTo(urls);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetVersion(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            string version)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("version", version));
            httpClientMock.SetupApiCall(sut, CallType.View, "version")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetVersion();

            // ASSERT
            result.Should().Be(version);
            httpClientMock.Verify();
        }

        #endregion

        #region Actions

        [Theory, AutoTestData]
        public void ClearExcludedFromProxy(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "clearExcludedFromProxy")
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.ClearExcludedFromProxy();

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void DeleteAllAlerts(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "deleteAllAlerts")
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.DeleteAllAlerts();

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void ExcludeFromProxy(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            string regex)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "excludeFromProxy",
                new Dictionary<string, object>
                {
                    {"regex", regex }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.ExcludeFromProxy(regex);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GenerateRootCA(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "generateRootCA")
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.GenerateRootCA();

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void LoadSession(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            string path)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "loadSession",
                new Dictionary<string, object>
                {
                    {"name", path }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.LoadSession(path);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void NewSession(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            string path,
            bool overwrite)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "newSession",
                new Dictionary<string, object>
                {
                    { "name", path },
                    { "overwrite", overwrite }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.NewSession(path, overwrite);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SaveSession(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            string path,
            bool overwrite)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "saveSession",
                new Dictionary<string, object>
                {
                    { "name", path },
                    { "overwrite", overwrite }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SaveSession(path, overwrite);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SendRequest(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            string httpRequest,
            bool followRedirects,
            IEnumerable<Message> messages)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("sendRequest", JArray.FromObject(messages)));
            httpClientMock.SetupApiCall(sut, CallType.Action, "sendRequest",
                new Dictionary<string, object>
                {
                    { "request", httpRequest },
                    { "followRedirects", followRedirects }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.SendRequest(httpRequest, followRedirects);

            // ASSERT
            result.ShouldBeEquivalentTo(messages);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetHomeDirectory(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            string value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setHomeDirectory",
                new Dictionary<string, object>
                {
                    {"dir", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetHomeDirectory(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetOptionHttpStateEnabled(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            bool value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionHttpStateEnabled",
                new Dictionary<string, object>
                {
                    {"Boolean", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionHttpStateEnabled(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetOptionProxyChainName(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            string value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionProxyChainName",
                new Dictionary<string, object>
                {
                    {"String", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionProxyChainName(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetOptionProxyChainPassword(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            string value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionProxyChainPassword",
                new Dictionary<string, object>
                {
                    {"String", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionProxyChainPassword(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetOptionProxyChainPort(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            int value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionProxyChainPort",
                new Dictionary<string, object>
                {
                    {"Integer", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionProxyChainPort(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetOptionProxyChainPrompt(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            bool value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionProxyChainPrompt",
                new Dictionary<string, object>
                {
                    {"Boolean", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionProxyChainPrompt(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetOptionProxyChainRealm(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            string value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionProxyChainRealm",
                new Dictionary<string, object>
                {
                    {"String", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionProxyChainRealm(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetOptionProxyChainSkipName(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            string value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionProxyChainSkipName",
                new Dictionary<string, object>
                {
                    {"String", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
#pragma warning disable 0618 // Turn deprication warning off for test.
            sut.SetOptionProxyChainSkipName(value);
#pragma warning restore 0618 // Restore deprication warning.

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetOptionProxyChainUserName(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            string value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionProxyChainUserName",
                new Dictionary<string, object>
                {
                    {"String", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionProxyChainUserName(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetOptionSingleCookieRequestHeader(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            bool value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionSingleCookieRequestHeader",
                new Dictionary<string, object>
                {
                    {"Boolean", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionSingleCookieRequestHeader(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetOptionTimeoutInSecs(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            int value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionTimeoutInSecs",
                new Dictionary<string, object>
                {
                    {"Integer", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionTimeoutInSecs(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetOptionUseProxyChain(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            bool value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionUseProxyChain",
                new Dictionary<string, object>
                {
                    {"Boolean", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionUseProxyChain(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetOptionUseProxyChainAuth(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            bool value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionUseProxyChainAuth",
                new Dictionary<string, object>
                {
                    {"Boolean", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionUseProxyChainAuth(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void Shutdown(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "shutdown")
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.Shutdown();

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SnapshotSession(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "snapshotSession")
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SnapshotSession();

            // ASSERT
            httpClientMock.Verify();
        }

        #endregion

        #region Others

        [Theory, AutoTestData]
        public void GetHtmlReport(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            string htmlReport)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Other, "htmlreport", null, DataType.Other)
                .Returns(htmlReport)
                .Verifiable();

            // ACT
            var result = sut.GetHtmlReport();

            // ASSERT
            result.Should().Be(htmlReport);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetMessageHar(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            int id,
            Har message)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Other, "messageHar",
                new Dictionary<string, object>
                {
                    {"id", id }
                }, DataType.Other)
                .Returns(Har.Serialize(message))
                .Verifiable();

            // ACT
            var result = sut.GetMessageHar(id);

            // ASSERT
            result.ShouldBeEquivalentTo(message);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetMessagesHar(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            string baseUrl,
            int start,
            int count,
            Har messages)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Other, "messagesHar",
                new Dictionary<string, object>
                {
                    { "baseurl", baseUrl },
                    { "start", start },
                    { "count", count }
                }, DataType.Other)
                .Returns(Har.Serialize(messages))
                .Verifiable();

            // ACT
            var result = sut.GetMessagesHar(baseUrl, start, count);

            // ASSERT
            result.ShouldBeEquivalentTo(messages);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetProxyPac(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            string proxyPac)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Other, "proxy.pac", null, DataType.Other)
                .Returns(proxyPac)
                .Verifiable();

            // ACT
            var result = sut.GetProxyPac();

            // ASSERT
            result.Should().Be(proxyPac);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetRootCertificate(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut)
        {
            // ARRANGE
            // AutoFixture isn't reliable with making certificates, so we use a static one.
            var certificate = new X509Certificate2(TestResources.TestCertificate);
            httpClientMock.SetupOtherDataApiCall(sut, "rootcert")
                .Returns(certificate.RawData)
                .Verifiable();

            // ACT
            var result = sut.GetRootCertificate();

            // ASSERT
            result.RawData.ShouldBeEquivalentTo(certificate.RawData);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SendHarRequest(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            Har request,
            bool followRedirects,
            Har response)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Other, "sendHarRequest",
                new Dictionary<string, object>
                {
                    { "request", Har.Serialize(request) },
                    { "followRedirects", followRedirects }
                }, DataType.Other)
                .Returns(Har.Serialize(response))
                .Verifiable();

            // ACT
            var result = sut.SendHarRequest(request, followRedirects);

            // ASSERT
            result.ShouldBeEquivalentTo(response);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetProxy(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            string host,
            int port)
        {
            // ARRANGE
            var proxy = new JObject(
                new JProperty("type", 1),
                new JProperty("http", new JObject(
                    new JProperty("host", host),
                    new JProperty("port", port))));
            httpClientMock.SetupApiCall(sut, CallType.Other, "setproxy",
                new Dictionary<string, object>
                {
                    { "proxy", proxy }
                }, DataType.Other)
                .Returns("OK")
                .Verifiable();

            // ACT
            sut.SetProxy(host, port);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetProxy_UnknownResult(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            string host,
            int port)
        {
            // ARRANGE
            var proxy = new JObject(
                new JProperty("type", 1),
                new JProperty("http", new JObject(
                    new JProperty("host", host),
                    new JProperty("port", port))));
            httpClientMock.SetupApiCall(sut, CallType.Other, "setproxy",
                new Dictionary<string, object>
                {
                    { "proxy", proxy }
                }, DataType.Other)
                .Returns("FAILED")
                .Verifiable();

            // ACT
            Action act = () => sut.SetProxy(host, port);

            // ASSERT
            act.ShouldThrow<ZapException>().WithMessage(Resources.SetProxyUnknownResult);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetXmlReport(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]Core sut,
            string xmlReport)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Other, "xmlreport", null, DataType.Other)
                .Returns(xmlReport)
                .Verifiable();

            // ACT
            var result = sut.GetXmlReport();

            // ASSERT
            result.Should().Be(xmlReport);
            httpClientMock.Verify();
        }

        #endregion
    }
}
