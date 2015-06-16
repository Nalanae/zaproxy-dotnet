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
using ZAProxy.Schema;

namespace ZAProxy.Tests.Components
{
    public class ActiveScannerTests
    {
        #region Views

        [Theory, AutoMoqData]
        public void GetAlertIds(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            int scanId,
            IEnumerable<int> alertsIds)
        {
            // ASSIGN
            var json = new JObject(
                new JProperty("alertsIds", new JArray(alertsIds)));
            httpClientMock.SetupApiCall(sut, CallType.View, "alertsIds",
                new Dictionary<string, object>
                {
                    { "scanId", scanId }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetAlertIds(scanId);

            // ASSERT
            result.ShouldBeEquivalentTo(alertsIds);
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void GetAttackModeQueue(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            int attackModeQueue)
        {
            // ASSIGN
            var json = new JObject(
                new JProperty("attackModeQueue", $"{attackModeQueue}"));
            httpClientMock.SetupApiCall(sut, CallType.View, "attackModeQueue")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetAttackModeQueue();

            // ASSERT
            result.Should().Be(attackModeQueue);
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void GetAttackModeQueue_MinusValue_ReturnsNull(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut)
        {
            // ASSIGN
            var json = new JObject(
                new JProperty("attackModeQueue", "-1"));
            httpClientMock.SetupApiCall(sut, CallType.View, "attackModeQueue")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetAttackModeQueue();

            // ASSERT
            result.Should().NotHaveValue();
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void GetExcludedFromScan(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            IEnumerable<string> excludedFromScan)
        {
            // ASSIGN
            var json = new JObject(
                new JProperty("excludedFromScan", new JArray(excludedFromScan)));
            httpClientMock.SetupApiCall(sut, CallType.View, "excludedFromScan")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetExcludedFromScan();

            // ASSERT
            result.ShouldBeEquivalentTo(excludedFromScan);
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void GetMessagesIds(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            int scanId,
            IEnumerable<int> messagesIds)
        {
            // ASSIGN
            var json = new JObject(
                new JProperty("messagesIds", new JArray(messagesIds)));
            httpClientMock.SetupApiCall(sut, CallType.View, "messagesIds",
                new Dictionary<string, object>
                {
                    { "scanId", scanId }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetMessagesIds(scanId);

            // ASSERT
            result.ShouldBeEquivalentTo(messagesIds);
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void GetOptionAllowAttackOnStart(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            bool allowAttacksOnStart)
        {
            // ASSIGN
            var json = new JObject(
                new JProperty("AllowAttackOnStart", $"{allowAttacksOnStart}"));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionAllowAttackOnStart")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionAllowAttackOnStart();

            // ASSERT
            result.Should().Be(allowAttacksOnStart);
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void GetOptionAttackPolicy(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            string attackPolicy)
        {
            // ASSIGN
            var json = new JObject(
                new JProperty("AttackPolicy", $"{attackPolicy}"));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionAttackPolicy")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionAttackPolicy();

            // ASSERT
            result.Should().Be(attackPolicy);
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void GetOptionDefaultPolicy(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            string defaultPolicy)
        {
            // ASSIGN
            var json = new JObject(
                new JProperty("DefaultPolicy", $"{defaultPolicy}"));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionDefaultPolicy")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionDefaultPolicy();

            // ASSERT
            result.Should().Be(defaultPolicy);
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void GetOptionDelayInMilliseconds(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            int delayInMilliseconds)
        {
            // ASSIGN
            var json = new JObject(
                new JProperty("DelayInMs", $"{delayInMilliseconds}"));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionDelayInMs")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionDelayInMilliseconds();

            // ASSERT
            result.Should().Be(delayInMilliseconds);
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void GetOptionExcludedParamList(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            IEnumerable<string> excludedParamList)
        {
            // ASSIGN
            var json = new JObject(
                new JProperty("ExcludedParamList", excludedParamList.ToJsonStringList()));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionExcludedParamList")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionExcludedParamList();

            // ASSERT
            result.Should().BeEquivalentTo(excludedParamList);
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void GetOptionHandleAntiCSRFTokens(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            bool handleAntiCSRFTokens)
        {
            // ASSIGN
            var json = new JObject(
                new JProperty("HandleAntiCSRFTokens", $"{handleAntiCSRFTokens}"));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionHandleAntiCSRFTokens")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionHandleAntiCSRFTokens();

            // ASSERT
            result.Should().Be(handleAntiCSRFTokens);
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void GetOptionHostPerScan(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            int hostPerScan)
        {
            // ASSIGN
            var json = new JObject(
                new JProperty("HostsPerScan", $"{hostPerScan}"));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionHostPerScan")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionHostPerScan();

            // ASSERT
            result.Should().Be(hostPerScan);
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void GetOptionMaxResultsToList(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            int maxResultsToList)
        {
            // ASSIGN
            var json = new JObject(
                new JProperty("MaxResultsToList", $"{maxResultsToList}"));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionMaxResultsToList")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionMaxResultsToList();

            // ASSERT
            result.Should().Be(maxResultsToList);
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void GetOptionMaxScansInUI(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            int maxScansInUI)
        {
            // ASSIGN
            var json = new JObject(
                new JProperty("MaxScansInUI", $"{maxScansInUI}"));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionMaxScansInUI")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionMaxScansInUI();

            // ASSERT
            result.Should().Be(maxScansInUI);
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void GetOptionPromptInAttackMode(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            bool promptInAttackMode)
        {
            // ASSIGN
            var json = new JObject(
                new JProperty("PromptInAttackMode", $"{promptInAttackMode}"));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionPromptInAttackMode")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionPromptInAttackMode();

            // ASSERT
            result.Should().Be(promptInAttackMode);
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void GetOptionPromptToClearFinishedScans(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            bool promptToClearFinishScans)
        {
            // ASSIGN
            var json = new JObject(
                new JProperty("PromptToClearFinishedScans", $"{promptToClearFinishScans}"));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionPromptToClearFinishedScans")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionPromptToClearFinishedScans();

            // ASSERT
            result.Should().Be(promptToClearFinishScans);
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void GetOptionRescanInAttackMode(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            bool rescanInAttackMode)
        {
            // ASSIGN
            var json = new JObject(
                new JProperty("RescanInAttackMode", $"{rescanInAttackMode}"));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionRescanInAttackMode")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionRescanInAttackMode();

            // ASSERT
            result.Should().Be(rescanInAttackMode);
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void GetOptionShowAdvancedDialog(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            bool showAdvancedDialog)
        {
            // ASSIGN
            var json = new JObject(
                new JProperty("ShowAdvancedDialog", $"{showAdvancedDialog}"));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionShowAdvancedDialog")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionShowAdvancedDialog();

            // ASSERT
            result.Should().Be(showAdvancedDialog);
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void GetOptionTargetParamsEnabledRPC(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            int targetParamsEnabledRPC)
        {
            // ASSIGN
            var json = new JObject(
                new JProperty("TargetParamsEnabledRPC", $"{targetParamsEnabledRPC}"));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionTargetParamsEnabledRPC")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionTargetParamsEnabledRPC();

            // ASSERT
            result.Should().Be(targetParamsEnabledRPC);
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void GetOptionTargetParamsInjectable(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            int targetParamsInjectable)
        {
            // ASSIGN
            var json = new JObject(
                new JProperty("TargetParamsInjectable", $"{targetParamsInjectable}"));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionTargetParamsInjectable")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionTargetParamsInjectable();

            // ASSERT
            result.Should().Be(targetParamsInjectable);
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void GetOptionThreadPerHost(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            int threadPerHost)
        {
            // ASSIGN
            var json = new JObject(
                new JProperty("ThreadPerHost", $"{threadPerHost}"));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionThreadPerHost")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionThreadPerHost();

            // ASSERT
            result.Should().Be(threadPerHost);
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void GetPolicies(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            string scanPolicyName,
            int policyId,
            IEnumerable<Policy> policies)
        {
            // ASSIGN
            var json = new JObject(
                new JProperty("policies", JArray.FromObject(policies)));
            httpClientMock.SetupApiCall(sut, CallType.View, "policies",
                new Dictionary<string, object>
                {
                    { "scanPolicyName", scanPolicyName },
                    { "policyId", policyId }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetPolicies(scanPolicyName, policyId);

            // ASSERT
            result.ShouldBeEquivalentTo(policies);
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void GetScanPolicyNames(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            IEnumerable<string> scanPolicyNames)
        {
            // ASSIGN
            var json = new JObject(
                new JProperty("scanPolicyNames", scanPolicyNames.ToJsonStringList()));
            httpClientMock.SetupApiCall(sut, CallType.View, "scanPolicyNames")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetScanPolicyNames();

            // ASSERT
            result.Should().BeEquivalentTo(scanPolicyNames);
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void GetScanProgress(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            int scanId,
            ScanProgress scanProgress)
        {
            // ASSIGN
            var json = new JObject(
                new JProperty("scanProgress", JToken.FromObject(scanProgress)));
            httpClientMock.SetupApiCall(sut, CallType.View, "scanProgress",
                new Dictionary<string, object>
                {
                    { "scanId", scanId }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetScanProgress(scanId);

            // ASSERT
            result.ShouldBeEquivalentTo(scanProgress);
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void GetScanners(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            string scanPolicyName,
            int policyId,
            IEnumerable<Scanner> scanners)
        {
            // ASSIGN
            var json = new JObject(
                new JProperty("scanners", JArray.FromObject(scanners)));
            httpClientMock.SetupApiCall(sut, CallType.View, "scanners",
                new Dictionary<string, object>
                {
                    { "scanPolicyName", scanPolicyName },
                    { "policyId", policyId }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetScanners(scanPolicyName, policyId);

            // ASSERT
            result.ShouldBeEquivalentTo(scanners);
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void GetScans(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            IEnumerable<Scan> scans)
        {
            // ASSIGN
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

        [Theory, AutoMoqData]
        public void GetStatus(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            int status)
        {
            // ASSIGN
            var json = new JObject(
                new JProperty("status", status));
            httpClientMock.SetupApiCall(sut, CallType.View, "status")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetStatus();

            // ASSERT
            result.ShouldBeEquivalentTo(status);
            httpClientMock.Verify();
        }

        #endregion

        #region Actions

        [Theory, AutoMoqData]
        public void AddScanPolicy(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            string scanPolicyName)
        {
            // ASSIGN
            httpClientMock.SetupApiCall(sut, CallType.Action, "addScanPolicy",
                new Dictionary<string, object>
                {
                    {"scanPolicyName", scanPolicyName }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.AddScanPolicy(scanPolicyName);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void ClearExcludedFromScan(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut)
        {
            // ASSIGN
            httpClientMock.SetupApiCall(sut, CallType.Action, "clearExcludedFromScan")
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.ClearExcludedFromScan();

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void DisableAllScanners(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            string scanPolicyName)
        {
            // ASSIGN
            httpClientMock.SetupApiCall(sut, CallType.Action, "disableAllScanners",
                new Dictionary<string, object>
                {
                    {"scanPolicyName", scanPolicyName }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.DisableAllScanners(scanPolicyName);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void DisableScanners(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            IEnumerable<int> scannerIds)
        {
            // ASSIGN
            httpClientMock.SetupApiCall(sut, CallType.Action, "disableScanners",
                new Dictionary<string, object>
                {
                    {"ids", scannerIds.ToString(",") }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.DisableScanners(scannerIds);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void EnableAllScanners(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            string scanPolicyName)
        {
            // ASSIGN
            httpClientMock.SetupApiCall(sut, CallType.Action, "enableAllScanners",
                new Dictionary<string, object>
                {
                    {"scanPolicyName", scanPolicyName }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.EnableAllScanners(scanPolicyName);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void EnableScanners(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            IEnumerable<int> scannerIds)
        {
            // ASSIGN
            httpClientMock.SetupApiCall(sut, CallType.Action, "enableScanners",
                new Dictionary<string, object>
                {
                    {"ids", scannerIds.ToString(",") }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.EnableScanners(scannerIds);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void ExcludeFromScan(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            string regexPattern)
        {
            // ASSIGN
            httpClientMock.SetupApiCall(sut, CallType.Action, "excludeFromScan",
                new Dictionary<string, object>
                {
                    {"regex", regexPattern }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.ExcludeFromScan(regexPattern);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void Pause(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            int scanId)
        {
            // ASSIGN
            httpClientMock.SetupApiCall(sut, CallType.Action, "pause",
                new Dictionary<string, object>
                {
                    {"scanId", scanId }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.Pause(scanId);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void PauseAllScans(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut)
        {
            // ASSIGN
            httpClientMock.SetupApiCall(sut, CallType.Action, "pauseAllScans")
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.PauseAllScans();

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void RemoveAllScans(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut)
        {
            // ASSIGN
            httpClientMock.SetupApiCall(sut, CallType.Action, "removeAllScans")
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.RemoveAllScans();

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void RemoveScan(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            int scanId)
        {
            // ASSIGN
            httpClientMock.SetupApiCall(sut, CallType.Action, "removeScan",
                new Dictionary<string, object>
                {
                    {"scanId", scanId }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.RemoveScan(scanId);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void RemoveScanPolicy(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            string scanPolicyName)
        {
            // ASSIGN
            httpClientMock.SetupApiCall(sut, CallType.Action, "removeScanPolicy",
                new Dictionary<string, object>
                {
                    {"scanPolicyName", scanPolicyName }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.RemoveScanPolicy(scanPolicyName);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void Resume(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            int scanId)
        {
            // ASSIGN
            httpClientMock.SetupApiCall(sut, CallType.Action, "resume",
                new Dictionary<string, object>
                {
                    {"scanId", scanId }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.Resume(scanId);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void ResumeAllScans(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut)
        {
            // ASSIGN
            httpClientMock.SetupApiCall(sut, CallType.Action, "resumeAllScans")
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.ResumeAllScans();

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void Scan(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            string url,
            bool recurse,
            bool inScopeOnly,
            string scanPolicyName,
            string method,
            string postData,
            int scanId)
        {
            // ASSIGN
            var json = new JObject(
                new JProperty("scan", scanId));
            httpClientMock.SetupApiCall(sut, CallType.Action, "scan",
                new Dictionary<string, object>
                {
                    { "url", url },
                    { "recurse", recurse },
                    { "inScopeOnly", inScopeOnly },
                    { "scanPolicyName", scanPolicyName },
                    { "method", method },
                    { "postData", postData }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.Scan(url, recurse, inScopeOnly, scanPolicyName, method, postData);

            // ASSERT
            result.Should().Be(scanId);
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void SetEnabledPolicies(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            IEnumerable<int> policyIds)
        {
            // ASSIGN
            httpClientMock.SetupApiCall(sut, CallType.Action, "setEnabledPolicies",
                new Dictionary<string, object>
                {
                    {"ids", policyIds.ToString(",") }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetEnabledPolicies(policyIds);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void SetOptionAllowAttackOnStart(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            bool value)
        {
            // ASSIGN
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionAllowAttackOnStart",
                new Dictionary<string, object>
                {
                    {"Boolean", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionAllowAttackOnStart(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void SetOptionAttackPolicy(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            string value)
        {
            // ASSIGN
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionAttackPolicy",
                new Dictionary<string, object>
                {
                    {"String", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionAttackPolicy(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void SetOptionDefaultPolicy(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            string value)
        {
            // ASSIGN
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionDefaultPolicy",
                new Dictionary<string, object>
                {
                    {"String", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionDefaultPolicy(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void SetOptionDelayInMs(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            int value)
        {
            // ASSIGN
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionDelayInMs",
                new Dictionary<string, object>
                {
                    {"Integer", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionDelayInMs(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void SetOptionHandleAntiCSRFTokens(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            bool value)
        {
            // ASSIGN
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionHandleAntiCSRFTokens",
                new Dictionary<string, object>
                {
                    {"Boolean", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionHandleAntiCSRFTokens(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void SetOptionHostPerScan(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            int value)
        {
            // ASSIGN
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionHostPerScan",
                new Dictionary<string, object>
                {
                    {"Integer", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionHostPerScan(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void SetOptionMaxResultsToList(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            int value)
        {
            // ASSIGN
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionMaxResultsToList",
                new Dictionary<string, object>
                {
                    {"Integer", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionMaxResultsToList(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void SetOptionMaxScansInUI(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            int value)
        {
            // ASSIGN
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionMaxScansInUI",
                new Dictionary<string, object>
                {
                    {"Integer", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionMaxScansInUI(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void SetOptionPromptInAttackMode(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            bool value)
        {
            // ASSIGN
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionPromptInAttackMode",
                new Dictionary<string, object>
                {
                    {"Boolean", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionPromptInAttackMode(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void SetOptionPromptToClearFinishedScans(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            bool value)
        {
            // ASSIGN
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionPromptToClearFinishedScans",
                new Dictionary<string, object>
                {
                    {"Boolean", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionPromptToClearFinishedScans(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void SetOptionRescanInAttackMode(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            bool value)
        {
            // ASSIGN
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionRescanInAttackMode",
                new Dictionary<string, object>
                {
                    {"Boolean", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionRescanInAttackMode(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void SetOptionShowAdvancedDialog(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            bool value)
        {
            // ASSIGN
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionShowAdvancedDialog",
                new Dictionary<string, object>
                {
                    {"Boolean", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionShowAdvancedDialog(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void SetOptionTargetParamsEnabledRPC(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            int value)
        {
            // ASSIGN
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionTargetParamsEnabledRPC",
                new Dictionary<string, object>
                {
                    {"Integer", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionTargetParamsEnabledRPC(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void SetOptionTargetParamsInjectable(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            int value)
        {
            // ASSIGN
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionTargetParamsInjectable",
                new Dictionary<string, object>
                {
                    {"Integer", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionTargetParamsInjectable(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void SetOptionThreadPerHost(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            int value)
        {
            // ASSIGN
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionThreadPerHost",
                new Dictionary<string, object>
                {
                    {"Integer", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionThreadPerHost(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void SetPolicyAlertThreshold(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            int policyId,
            AlertThreshold alertThreshold,
            string scanPolicyName)
        {
            // ASSIGN
            httpClientMock.SetupApiCall(sut, CallType.Action, "setPolicyAlertThreshold",
                new Dictionary<string, object>
                {
                    { "id", policyId },
                    { "alertThreshold", alertThreshold },
                    { "scanPolicyName", scanPolicyName }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetPolicyAlertThreshold(policyId, alertThreshold, scanPolicyName);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void SetPolicyAttackStrength(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            int policyId,
            AttackStrength attackStrength,
            string scanPolicyName)
        {
            // ASSIGN
            httpClientMock.SetupApiCall(sut, CallType.Action, "setPolicyAttackStrength",
                new Dictionary<string, object>
                {
                    { "id", policyId },
                    { "attackStrength", attackStrength },
                    { "scanPolicyName", scanPolicyName }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetPolicyAttackStrength(policyId, attackStrength, scanPolicyName);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void SetScannerAlertThreshold(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            int scannerId,
            AlertThreshold alertThreshold,
            string scanPolicyName)
        {
            // ASSIGN
            httpClientMock.SetupApiCall(sut, CallType.Action, "setScannerAlertThreshold",
                new Dictionary<string, object>
                {
                    { "id", scannerId },
                    { "alertThreshold", alertThreshold },
                    { "scanPolicyName", scanPolicyName }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetScannerAlertThreshold(scannerId, alertThreshold, scanPolicyName);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void SetScannerAttackStrength(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            int policyId,
            AttackStrength attackStrength,
            string scanPolicyName)
        {
            // ASSIGN
            httpClientMock.SetupApiCall(sut, CallType.Action, "setScannerAttackStrength",
                new Dictionary<string, object>
                {
                    { "id", policyId },
                    { "attackStrength", attackStrength },
                    { "scanPolicyName", scanPolicyName }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetScannerAttackStrength(policyId, attackStrength, scanPolicyName);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void Stop(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut,
            int scanId)
        {
            // ASSIGN
            httpClientMock.SetupApiCall(sut, CallType.Action, "stop",
                new Dictionary<string, object>
                {
                    {"scanId", scanId }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.Stop(scanId);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoMoqData]
        public void StopAllScans(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScanner sut)
        {
            // ASSIGN
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
