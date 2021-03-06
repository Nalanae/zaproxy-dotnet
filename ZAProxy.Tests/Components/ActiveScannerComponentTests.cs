﻿using System.Collections.Generic;
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
    [Trait("Component", "ActiveScanner")]
    public class ActiveScannerComponentTests
    {
        [Theory, AutoTestData]
        public void ComponentName(
            [Greedy]ActiveScannerComponent sut)
        {
            // ACT
            var result = sut.ComponentName;

            // ASSERT
            result.Should().Be("ascan");
        }

        #region Views

        [Theory, AutoTestData]
        public void GetAlertIds(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            int scanId,
            IEnumerable<int> alertsIds)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("alertsIds", new JArray(alertsIds)));
            httpClientMock.SetupApiCall(sut, CallType.View, "alertsIds",
                new Parameters
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

        [Theory, AutoTestData]
        public void GetAttackModeQueue(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            int attackModeQueue)
        {
            // ARRANGE
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

        [Theory, AutoTestData]
        public void GetAttackModeQueue_MinusValue_ReturnsNull(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut)
        {
            // ARRANGE
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

        [Theory, AutoTestData]
        public void GetExcludedFromScan(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            IEnumerable<string> excludedFromScan)
        {
            // ARRANGE
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

        [Theory, AutoTestData]
        public void GetMessagesIds(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            int scanId,
            IEnumerable<int> messagesIds)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("messagesIds", new JArray(messagesIds)));
            httpClientMock.SetupApiCall(sut, CallType.View, "messagesIds",
                new Parameters
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

        [Theory, AutoTestData]
        public void GetOptionAllowAttackOnStart(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            bool allowAttacksOnStart)
        {
            // ARRANGE
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

        [Theory, AutoTestData]
        public void GetOptionAttackPolicy(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            string attackPolicy)
        {
            // ARRANGE
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

        [Theory, AutoTestData]
        public void GetOptionDefaultPolicy(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            string defaultPolicy)
        {
            // ARRANGE
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

        [Theory, AutoTestData]
        public void GetOptionDelayInMilliseconds(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            int delayInMilliseconds)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("DelayInMs", $"{delayInMilliseconds}"));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionDelayInMs")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionDelayInMs();

            // ASSERT
            result.Should().Be(delayInMilliseconds);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionExcludedParamList(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            IEnumerable<string> excludedParamList)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("ExcludedParamList", excludedParamList.ToJsonStringList()));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionExcludedParamList")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
#pragma warning disable CS0618 // Type or member is obsolete
            var result = sut.GetOptionExcludedParamList();
#pragma warning restore CS0618 // Type or member is obsolete

            // ASSERT
            result.ShouldBeEquivalentTo(excludedParamList);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionHandleAntiCSRFTokens(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            bool handleAntiCSRFTokens)
        {
            // ARRANGE
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

        [Theory, AutoTestData]
        public void GetOptionHostPerScan(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            int hostPerScan)
        {
            // ARRANGE
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

        [Theory, AutoTestData]
        public void GetOptionMaxResultsToList(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            int maxResultsToList)
        {
            // ARRANGE
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

        [Theory, AutoTestData]
        public void GetOptionMaxScansInUI(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            int maxScansInUI)
        {
            // ARRANGE
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

        [Theory, AutoTestData]
        public void GetOptionPromptInAttackMode(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            bool promptInAttackMode)
        {
            // ARRANGE
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

        [Theory, AutoTestData]
        public void GetOptionPromptToClearFinishedScans(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            bool promptToClearFinishScans)
        {
            // ARRANGE
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

        [Theory, AutoTestData]
        public void GetOptionRescanInAttackMode(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            bool rescanInAttackMode)
        {
            // ARRANGE
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

        [Theory, AutoTestData]
        public void GetOptionShowAdvancedDialog(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            bool showAdvancedDialog)
        {
            // ARRANGE
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

        [Theory, AutoTestData]
        public void GetOptionTargetParamsEnabledRPC(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            TargetEnabledRPC targetParamsEnabledRPC)
        {
            // ARRANGE
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

        [Theory, AutoTestData]
        public void GetOptionTargetParamsInjectable(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            TargetInjectable targetParamsInjectable)
        {
            // ARRANGE
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

        [Theory, AutoTestData]
        public void GetOptionThreadPerHost(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            int threadPerHost)
        {
            // ARRANGE
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

        [Theory, AutoTestData]
        public void GetPolicies(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            string scanPolicyName,
            int policyId,
            IEnumerable<Policy> policies)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("policies", JArray.FromObject(policies)));
            httpClientMock.SetupApiCall(sut, CallType.View, "policies",
                new Parameters
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

        [Theory, AutoTestData]
        public void GetScanPolicyNames(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            IEnumerable<string> scanPolicyNames)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("scanPolicyNames", scanPolicyNames.ToJsonStringList()));
            httpClientMock.SetupApiCall(sut, CallType.View, "scanPolicyNames")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetScanPolicyNames();

            // ASSERT
            result.ShouldBeEquivalentTo(scanPolicyNames);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetScanProgress(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            int scanId,
            ActiveScanProgress scanProgress)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("scanProgress", JToken.FromObject(scanProgress)));
            httpClientMock.SetupApiCall(sut, CallType.View, "scanProgress",
                new Parameters
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

        [Theory, AutoTestData]
        public void GetScanners(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            string scanPolicyName,
            int policyId,
            IEnumerable<Schema.ActiveScanner> scanners)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("scanners", JArray.FromObject(scanners)));
            httpClientMock.SetupApiCall(sut, CallType.View, "scanners",
                new Parameters
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

        [Theory, AutoTestData]
        public void GetScans(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            IEnumerable<ActiveScan> scans)
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
            [Greedy]ActiveScannerComponent sut,
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
            result.ShouldBeEquivalentTo(status);
            httpClientMock.Verify();
        }

        #endregion

        #region Actions

        [Theory, AutoTestData]
        public void AddScanPolicy(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            string scanPolicyName)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "addScanPolicy",
                new Parameters
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

        [Theory, AutoTestData]
        public void ClearExcludedFromScan(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut)
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
        public void DisableAllScanners(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            string scanPolicyName)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "disableAllScanners",
                new Parameters
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

        [Theory, AutoTestData]
        public void DisableScanners(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            IEnumerable<int> scannerIds)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "disableScanners",
                new Parameters
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

        [Theory, AutoTestData]
        public void EnableAllScanners(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            string scanPolicyName)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "enableAllScanners",
                new Parameters
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

        [Theory, AutoTestData]
        public void EnableScanners(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            IEnumerable<int> scannerIds)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "enableScanners",
                new Parameters
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

        [Theory, AutoTestData]
        public void ExcludeFromScan(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            string regexPattern)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "excludeFromScan",
                new Parameters
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

        [Theory, AutoTestData]
        public void Pause(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            int scanId)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "pause",
                new Parameters
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

        [Theory, AutoTestData]
        public void PauseAllScans(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut)
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
            [Greedy]ActiveScannerComponent sut)
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
            [Greedy]ActiveScannerComponent sut,
            int scanId)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "removeScan",
                new Parameters
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

        [Theory, AutoTestData]
        public void RemoveScanPolicy(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            string scanPolicyName)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "removeScanPolicy",
                new Parameters
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

        [Theory, AutoTestData]
        public void Resume(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            int scanId)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "resume",
                new Parameters
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

        [Theory, AutoTestData]
        public void ResumeAllScans(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut)
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
            [Greedy]ActiveScannerComponent sut,
            string url,
            bool recurse,
            bool inScopeOnly,
            string scanPolicyName,
            string method,
            string postData,
            int scanId)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("scan", scanId));
            httpClientMock.SetupApiCall(sut, CallType.Action, "scan",
                new Parameters
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

        [Theory, AutoTestData]
        public void SetEnabledPolicies(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            IEnumerable<int> policyIds)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setEnabledPolicies",
                new Parameters
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

        [Theory, AutoTestData]
        public void SetOptionAllowAttackOnStart(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            bool value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionAllowAttackOnStart",
                new Parameters
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

        [Theory, AutoTestData]
        public void SetOptionAttackPolicy(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            string value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionAttackPolicy",
                new Parameters
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

        [Theory, AutoTestData]
        public void SetOptionDefaultPolicy(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            string value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionDefaultPolicy",
                new Parameters
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

        [Theory, AutoTestData]
        public void SetOptionDelayInMs(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            int value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionDelayInMs",
                new Parameters
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

        [Theory, AutoTestData]
        public void SetOptionHandleAntiCSRFTokens(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            bool value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionHandleAntiCSRFTokens",
                new Parameters
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

        [Theory, AutoTestData]
        public void SetOptionHostPerScan(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            int value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionHostPerScan",
                new Parameters
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

        [Theory, AutoTestData]
        public void SetOptionMaxResultsToList(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            int value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionMaxResultsToList",
                new Parameters
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

        [Theory, AutoTestData]
        public void SetOptionMaxScansInUI(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            int value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionMaxScansInUI",
                new Parameters
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

        [Theory, AutoTestData]
        public void SetOptionPromptInAttackMode(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            bool value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionPromptInAttackMode",
                new Parameters
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

        [Theory, AutoTestData]
        public void SetOptionPromptToClearFinishedScans(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            bool value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionPromptToClearFinishedScans",
                new Parameters
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

        [Theory, AutoTestData]
        public void SetOptionRescanInAttackMode(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            bool value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionRescanInAttackMode",
                new Parameters
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

        [Theory, AutoTestData]
        public void SetOptionShowAdvancedDialog(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            bool value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionShowAdvancedDialog",
                new Parameters
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

        [Theory, AutoTestData]
        public void SetOptionTargetParamsEnabledRPC(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            TargetEnabledRPC value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionTargetParamsEnabledRPC",
                new Parameters
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

        [Theory, AutoTestData]
        public void SetOptionTargetParamsInjectable(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            TargetInjectable value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionTargetParamsInjectable",
                new Parameters
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

        [Theory, AutoTestData]
        public void SetOptionThreadPerHost(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            int value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setOptionThreadPerHost",
                new Parameters
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

        [Theory, AutoTestData]
        public void SetPolicyAlertThreshold(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            int policyId,
            AlertThreshold alertThreshold,
            string scanPolicyName)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setPolicyAlertThreshold",
                new Parameters
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

        [Theory, AutoTestData]
        public void SetPolicyAttackStrength(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            int policyId,
            AttackStrength attackStrength,
            string scanPolicyName)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setPolicyAttackStrength",
                new Parameters
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

        [Theory, AutoTestData]
        public void SetScannerAlertThreshold(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            int scannerId,
            AlertThreshold alertThreshold,
            string scanPolicyName)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setScannerAlertThreshold",
                new Parameters
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

        [Theory, AutoTestData]
        public void SetScannerAttackStrength(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            int policyId,
            AttackStrength attackStrength,
            string scanPolicyName)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setScannerAttackStrength",
                new Parameters
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

        [Theory, AutoTestData]
        public void Stop(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut,
            int scanId)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "stop",
                new Parameters
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

        [Theory, AutoTestData]
        public void StopAllScans(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]ActiveScannerComponent sut)
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
