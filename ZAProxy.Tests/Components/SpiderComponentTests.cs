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
    [Trait("Component", "Spider")]
    public class SpiderComponentTests
    {
        [Theory, AutoTestData]
        public void ComponentName(
            [Greedy]SpiderComponent sut)
        {
            // ACT
            var result = sut.ComponentName;

            // ASSERT
            result.Should().Be("spider");
        }

        #region Views

        [Theory, AutoTestData]
        public void GetExcludedFromScan(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            IEnumerable<string> excludedFromScan)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("excludedFromScan", JArray.FromObject(excludedFromScan)));
            httpClientMock.SetupApiCall(sut, CallType.View, "excludedFromScan")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetExcludedFromScan();

            // ASSERT
            result.ShouldBeEquivalentTo(excludedFromScan);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetFullResults(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            int scanId,
            SpiderScanResult fullResult)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("fullResults", JArray.FromObject(fullResult)));
            httpClientMock.SetupApiCall(sut, CallType.View, "fullResults",
                new Parameters
                {
                    { "scanId", scanId }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetFullResults(scanId);

            // ASSERT
            result.ShouldBeEquivalentTo(fullResult);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionDomainsAlwaysInScope(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            IEnumerable<string> domainsAlwaysInScope)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("DomainsAlwaysInScope", domainsAlwaysInScope.ToJsonStringList()));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionDomainsAlwaysInScope")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
#pragma warning disable CS0618 // Type or member is obsolete
            var result = sut.GetOptionDomainsAlwaysInScope();
#pragma warning restore CS0618 // Type or member is obsolete

            // ASSERT
            result.ShouldBeEquivalentTo(domainsAlwaysInScope);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionDomainsAlwaysInScopeEnabled(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            bool domainsAlwaysInScopeEnabled)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("DomainsAlwaysInScopeEnabled", domainsAlwaysInScopeEnabled));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionDomainsAlwaysInScopeEnabled")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
#pragma warning disable CS0618 // Type or member is obsolete
            var result = sut.GetOptionDomainsAlwaysInScopeEnabled();
#pragma warning restore CS0618 // Type or member is obsolete

            // ASSERT
            result.Should().Be(domainsAlwaysInScopeEnabled);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionHandleODataParametersVisited(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            bool handleODataParametersVisited)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("HandleODataParametersVisited", handleODataParametersVisited));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionHandleODataParametersVisited")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionHandleODataParametersVisited();

            // ASSERT
            result.Should().Be(handleODataParametersVisited);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionHandleParameters(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            HandleParametersOption handleParameters)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("HandleParameters", handleParameters));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionHandleParameters")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionHandleParameters();

            // ASSERT
            result.Should().Be(handleParameters);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionMaxDepth(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            int maxDepth)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("MaxDepth", maxDepth));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionMaxDepth")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionMaxDepth();

            // ASSERT
            result.Should().Be(maxDepth);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionMaxScansInUI(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            int maxScansInUI)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("MaxScansInUI", maxScansInUI));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionMaxScansInUI")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionMaxScansInUI();

            // ASSERT
            result.Should().Be(maxScansInUI);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionParseComments(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            bool parseComments)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("ParseComments", parseComments));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionParseComments")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionParseComments();

            // ASSERT
            result.Should().Be(parseComments);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionParseGit(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            bool parseGit)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("ParseGit", parseGit));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionParseGit")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionParseGit();

            // ASSERT
            result.Should().Be(parseGit);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionParseRobotsTxt(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            bool parseRobotsTxt)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("ParseRobotsTxt", parseRobotsTxt));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionParseRobotsTxt")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionParseRobotsTxt();

            // ASSERT
            result.Should().Be(parseRobotsTxt);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionParseSVNEntries(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            bool parseSVNEntries)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("ParseSVNEntries", parseSVNEntries));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionParseSVNEntries")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionParseSVNEntries();

            // ASSERT
            result.Should().Be(parseSVNEntries);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionParseSitemapXml(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            bool parseSitemapXml)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("ParseSitemapXml", parseSitemapXml));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionParseSitemapXml")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionParseSitemapXml();

            // ASSERT
            result.Should().Be(parseSitemapXml);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionPostForm(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            bool postForm)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("PostForm", postForm));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionPostForm")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionPostForm();

            // ASSERT
            result.Should().Be(postForm);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionProcessForm(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            bool processForm)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("ProcessForm", processForm));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionProcessForm")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionProcessForm();

            // ASSERT
            result.Should().Be(processForm);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionRequestWaitTime(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            int requestWaitTime)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("RequestWaitTime", requestWaitTime));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionRequestWaitTime")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionRequestWaitTime();

            // ASSERT
            result.Should().Be(requestWaitTime);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionScope(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            string scope)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("Scope", scope));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionScope")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
#pragma warning disable CS0618 // Type or member is obsolete
            var result = sut.GetOptionScope();
#pragma warning restore CS0618 // Type or member is obsolete

            // ASSERT
            result.Should().Be(scope);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionScopeText(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            string scopeText)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("ScopeText", scopeText));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionScopeText")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
#pragma warning disable CS0618 // Type or member is obsolete
            var result = sut.GetOptionScopeText();
#pragma warning restore CS0618 // Type or member is obsolete

            // ASSERT
            result.Should().Be(scopeText);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionSendRefererHeader(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            bool sendRefererHeader)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("SendRefererHeader", sendRefererHeader));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionSendRefererHeader")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionSendRefererHeader();

            // ASSERT
            result.Should().Be(sendRefererHeader);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionShowAdvancedDialog(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            bool showAdvancedDialog)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("ShowAdvancedDialog", showAdvancedDialog));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionShowAdvancedDialog")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionShowAdvancedDialog();

            // ASSERT
            result.Should().Be(showAdvancedDialog);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionSkipURLString(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            string scopeText)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("SkipURLString", scopeText));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionSkipURLString")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionSkipURLString();

            // ASSERT
            result.Should().Be(scopeText);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionThreadCount(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            int threadCount)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("ThreadCount", threadCount));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionThreadCount")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionThreadCount();

            // ASSERT
            result.Should().Be(threadCount);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionUserAgent(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            string userAgent)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("UserAgent", userAgent));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionUserAgent")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionUserAgent();

            // ASSERT
            result.Should().Be(userAgent);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetResults(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            int scanId,
            IEnumerable<string> results)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("results", JArray.FromObject(results)));
            httpClientMock.SetupApiCall(sut, CallType.View, "results",
                new Parameters
                {
                    { "scanId", scanId }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetResults(scanId);

            // ASSERT
            result.ShouldBeEquivalentTo(results);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetScans(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            IEnumerable<SpiderScan> scans)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("scans", JArray.FromObject(scans)));
            httpClientMock.SetupApiCall(sut, CallType.View, "scans")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetScans();

            // ASSERT
            result.ShouldBeEquivalentTo(scans);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetStatus(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            int scanId,
            int status)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("status", status));
            httpClientMock.SetupApiCall(sut, CallType.View, "status",
                new Parameters
                {
                    { "scanId", scanId }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetStatus(scanId);

            // ASSERT
            result.Should().Be(status);
            httpClientMock.Verify();
        }

        #endregion

        #region Actions

        [Theory, AutoTestData]
        public void ClearExcludedFromScan(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "clearExcludedFromScan")
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.ClearExcludedFromScan();

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void ExcludeFromScan(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            string regex)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "excludeFromScan",
                new Parameters
                {
                    { "regex", regex }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.ExcludeFromScan(regex);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void Pause(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            int scanId)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "pause",
                new Parameters
                {
                    { "scanId", scanId }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.Pause(scanId);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void PauseAllScans(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "pauseAllScans")
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.PauseAllScans();

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void RemoveAllScans(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "removeAllScans")
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.RemoveAllScans();

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void RemoveScan(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            int scanId)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "removeScan",
                new Parameters
                {
                    { "scanId", scanId }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.RemoveScan(scanId);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void Resume(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            int scanId)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "resume",
                new Parameters
                {
                    { "scanId", scanId }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.Resume(scanId);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void ResumeAllScans(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "resumeAllScans")
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.ResumeAllScans();

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void Scan(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            string url,
            int maxChildren,
            int scanId)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("scan", scanId));
            httpClientMock.SetupApiCall(sut, CallType.Action, "scan",
                new Parameters
                {
                    { "url", url },
                    { "maxChildren", maxChildren }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.Scan(url, maxChildren);

            // ASSERT
            result.Should().Be(scanId);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void ScanAsUser(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            string url,
            int contextId,
            int userId,
            int maxChildren,
            int scanId)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("scanAsUser", scanId));
            httpClientMock.SetupApiCall(sut, CallType.Action, "scanAsUser",
                new Parameters
                {
                    { "url", url },
                    { "contextId", contextId },
                    { "userId", userId },
                    { "maxChildren", maxChildren }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.ScanAsUser(url, contextId, userId, maxChildren);

            // ASSERT
            result.Should().Be(scanId);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetOptionHandleODataParametersVisited(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            bool value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionHandleODataParametersVisited",
                new Parameters
                {
                    { "Boolean", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionHandleODataParametersVisited(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetOptionHandleParameters(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            HandleParametersOption value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionHandleParameters",
                new Parameters
                {
                    { "String", value.ToString().ToUpperInvariant() }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionHandleParameters(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetOptionMaxDepth(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            int value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionMaxDepth",
                new Parameters
                {
                    { "Integer", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionMaxDepth(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetOptionMaxScansInUI(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            int value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionMaxScansInUI",
                new Parameters
                {
                    { "Integer", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionMaxScansInUI(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetOptionParseComments(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            bool value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionParseComments",
                new Parameters
                {
                    { "Boolean", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionParseComments(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetOptionParseGit(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            bool value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionParseGit",
                new Parameters
                {
                    { "Boolean", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionParseGit(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetOptionParseRobotsTxt(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            bool value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionParseRobotsTxt",
                new Parameters
                {
                    { "Boolean", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionParseRobotsTxt(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetOptionParseSVNEntries(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            bool value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionParseSVNEntries",
                new Parameters
                {
                    { "Boolean", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionParseSVNEntries(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetOptionParseSitemapXml(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            bool value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionParseSitemapXml",
                new Parameters
                {
                    { "Boolean", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionParseSitemapXml(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetOptionPostForm(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            bool value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionPostForm",
                new Parameters
                {
                    { "Boolean", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionPostForm(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetOptionProcessForm(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            bool value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionProcessForm",
                new Parameters
                {
                    { "Boolean", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionProcessForm(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetOptionRequestWaitTime(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            int value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionRequestWaitTime",
                new Parameters
                {
                    { "Integer", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionRequestWaitTime(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetOptionScopeString(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            string value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionScopeString",
                new Parameters
                {
                    { "String", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionScopeString(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetOptionSendRefererHeader(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            bool value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionSendRefererHeader",
                new Parameters
                {
                    { "Boolean", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionSendRefererHeader(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetOptionShowAdvancedDialog(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            bool value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionShowAdvancedDialog",
                new Parameters
                {
                    { "Boolean", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionShowAdvancedDialog(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetOptionSkipURLString(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            string value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionSkipURLString",
                new Parameters
                {
                    { "String", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionSkipURLString(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetOptionThreadCount(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            int value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionThreadCount",
                new Parameters
                {
                    { "Integer", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionThreadCount(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetOptionUserAgent(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            string value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionUserAgent",
                new Parameters
                {
                    { "String", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionUserAgent(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void Stop(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut,
            int scanId)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "stop",
                new Parameters
                {
                    { "scanId", scanId }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.Stop(scanId);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void StopAllScans(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SpiderComponent sut)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "stopAllScans")
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.StopAllScans();

            // ASSERT
            httpClientMock.Verify();
        }

        #endregion
    }
}
