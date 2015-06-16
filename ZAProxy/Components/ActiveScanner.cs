using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZAProxy.Infrastructure;
using ZAProxy.Schema;

namespace ZAProxy.Components
{
    public class ActiveScanner : ComponentBase
    {
        public ActiveScanner(string apiKey = null)
            : this(null, apiKey)
        { }

        public ActiveScanner(IHttpClient httpClient, string apiKey = null)
            : base(httpClient, apiKey, "ascan")
        { }

        #region Views

        public IEnumerable<int> GetAlertIds(int scanId)
        {
            return CallView<IEnumerable<int>>("alertsIds", "alertsIds", new Dictionary<string, object>
            {
                { "scanId", scanId }
            });
        }

        public int? GetAttackModeQueue()
        {
            var value = CallView<int>("attackModeQueue", "attackModeQueue");
            return value == -1 ? (int?)null : value;
        }

        public IEnumerable<string> GetExcludedFromScan()
        {
            return CallView<IEnumerable<string>>("excludedFromScan", "excludedFromScan");
        }

        public IEnumerable<int> GetMessagesIds(int scanId)
        {
            return CallView<IEnumerable<int>>("messagesIds", "messagesIds", new Dictionary<string, object>
            {
                { "scanId", scanId }
            });
        }

        public bool GetOptionAllowAttackOnStart()
        {
            return CallView<bool>("optionAllowAttackOnStart", "AllowAttackOnStart");
        }

        public string GetOptionAttackPolicy()
        {
            return CallView<string>("optionAttackPolicy", "AttackPolicy");
        }

        public string GetOptionDefaultPolicy()
        {
            return CallView<string>("optionDefaultPolicy", "DefaultPolicy");
        }

        public int GetOptionDelayInMilliseconds()
        {
            return CallView<int>("optionDelayInMs", "DelayInMs");
        }

        public IEnumerable<string> GetOptionExcludedParamList()
        {
            return ParseStringList(CallView<string>("optionExcludedParamList", "ExcludedParamList"));
        }

        public bool GetOptionHandleAntiCSRFTokens()
        {
            return CallView<bool>("optionHandleAntiCSRFTokens", "HandleAntiCSRFTokens");
        }

        public int GetOptionHostPerScan()
        {
            return CallView<int>("optionHostPerScan", "HostsPerScan");
        }

        public int GetOptionMaxResultsToList()
        {
            return CallView<int>("optionMaxResultsToList", "MaxResultsToList");
        }

        public int GetOptionMaxScansInUI()
        {
            return CallView<int>("optionMaxScansInUI", "MaxScansInUI");
        }

        public bool GetOptionPromptInAttackMode()
        {
            return CallView<bool>("optionPromptInAttackMode", "PromptInAttackMode");
        }

        public bool GetOptionPromptToClearFinishedScans()
        {
            return CallView<bool>("optionPromptToClearFinishedScans", "PromptToClearFinishedScans");
        }

        public bool GetOptionRescanInAttackMode()
        {
            return CallView<bool>("optionRescanInAttackMode", "RescanInAttackMode");
        }

        public bool GetOptionShowAdvancedDialog()
        {
            return CallView<bool>("optionShowAdvancedDialog", "ShowAdvancedDialog");
        }

        public int GetOptionTargetParamsEnabledRPC()
        {
            return CallView<int>("optionTargetParamsEnabledRPC", "TargetParamsEnabledRPC");
        }

        public int GetOptionTargetParamsInjectable()
        {
            return CallView<int>("optionTargetParamsInjectable", "TargetParamsInjectable");
        }

        public int GetOptionThreadPerHost()
        {
            return CallView<int>("optionThreadPerHost", "ThreadPerHost");
        }

        public IEnumerable<Policy> GetPolicies(string scanPolicyName = null, int? policyId = null)
        {
            return CallView<IEnumerable<Policy>>("policies", "policies", new Dictionary<string, object>
            {
                { "scanPolicyName", scanPolicyName },
                { "policyId", policyId }
            });
        }

        public IEnumerable<string> GetScanPolicyNames()
        {
            return ParseStringList(CallView<string>("scanPolicyNames", "scanPolicyNames"));
        }

        public ScanProgress GetScanProgress(int scanId)
        {
            return CallView<ScanProgress>("scanProgress", "scanProgress", new Dictionary<string, object>
            {
                { "scanId", scanId }
            });
        }

        public IEnumerable<Scanner> GetScanners(string scanPolicyName = null, int? policyId = null)
        {
            return CallView<IEnumerable<Scanner>>("scanners", "scanners", new Dictionary<string, object>
            {
                { "scanPolicyName", scanPolicyName },
                { "policyId", policyId }
            });
        }

        public IEnumerable<Scan> GetScans()
        {
            return CallView<IEnumerable<Scan>>("scans", "scans");
        }

        public int GetStatus()
        {
            return CallView<int>("status", "status");
        }

        #endregion

        #region Actions

        public void AddScanPolicy(string scanPolicyName)
        {
            CallAction("addScanPolicy", new Dictionary<string, object>
            {
                { "scanPolicyName", scanPolicyName }
            });
        }

        public void ClearExcludedFromScan()
        {
            CallAction("clearExcludedFromScan");
        }

        public void DisableAllScanners(string scanPolicyName = null)
        {
            CallAction("disableAllScanners", new Dictionary<string, object>
            {
                { "scanPolicyName", scanPolicyName }
            });
        }

        public void DisableScanners(IEnumerable<int> scannerIds)
        {
            CallAction("disableScanners", new Dictionary<string, object>
            {
                { "ids", scannerIds.ToString(",") }
            });
        }

        public void EnableAllScanners(string scanPolicyName = null)
        {
            CallAction("enableAllScanners", new Dictionary<string, object>
            {
                { "scanPolicyName", scanPolicyName }
            });
        }

        public void EnableScanners(IEnumerable<int> scannerIds)
        {
            CallAction("enableScanners", new Dictionary<string, object>
            {
                { "ids", scannerIds.ToString(",") }
            });
        }

        public void ExcludeFromScan(string regexPattern)
        {
            CallAction("excludeFromScan", new Dictionary<string, object>
            {
                { "regex", regexPattern }
            });
        }

        public void Pause(int scanId)
        {
            CallAction("pause", new Dictionary<string, object>
            {
                { "scanId", scanId }
            });
        }

        public void PauseAllScans()
        {
            CallAction("pauseAllScans");
        }

        public void RemoveAllScans()
        {
            CallAction("removeAllScans");
        }

        public void RemoveScan(int scanId)
        {
            CallAction("removeScan", new Dictionary<string, object>
            {
                { "scanId", scanId }
            });
        }

        public void RemoveScanPolicy(string scanPolicyName)
        {
            CallAction("removeScanPolicy", new Dictionary<string, object>
            {
                { "scanPolicyName", scanPolicyName }
            });
        }

        public void Resume(int scanId)
        {
            CallAction("resume", new Dictionary<string, object>
            {
                { "scanId", scanId }
            });
        }

        public void ResumeAllScans()
        {
            CallAction("resumeAllScans");
        }

        public int Scan(string url, bool recurse = true, bool inScopeOnly = false, string scanPolicyName = null, string method = null, string postData = null)
        {
            return CallAction<int>("scan", "scan", new Dictionary<string, object>
            {
                { "url", url },
                { "recurse", recurse },
                { "inScopeOnly", inScopeOnly },
                { "scanPolicyName", scanPolicyName },
                { "method", method },
                { "postData", postData }
            });
        }

        public void SetEnabledPolicies(IEnumerable<int> policyIds)
        {
            CallAction("setEnabledPolicies", new Dictionary<string, object>
            {
                { "ids", policyIds.ToString(",") }
            });
        }

        public void SetOptionAllowAttackOnStart(bool value)
        {
            CallAction("setOptionAllowAttackOnStart", new Dictionary<string, object>
            {
                { "Boolean", value }
            });
        }

        public void SetOptionAttackPolicy(string value)
        {
            CallAction("setOptionAttackPolicy", new Dictionary<string, object>
            {
                { "String", value }
            });
        }

        public void SetOptionDefaultPolicy(string value)
        {
            CallAction("setOptionDefaultPolicy", new Dictionary<string, object>
            {
                { "String", value }
            });
        }

        public void SetOptionDelayInMs(int value)
        {
            CallAction("setOptionDelayInMs", new Dictionary<string, object>
            {
                { "Integer", value }
            });
        }

        public void SetOptionHandleAntiCSRFTokens(bool value)
        {
            CallAction("setOptionHandleAntiCSRFTokens", new Dictionary<string, object>
            {
                { "Boolean", value }
            });
        }

        public void SetOptionHostPerScan(int value)
        {
            CallAction("setOptionHostPerScan", new Dictionary<string, object>
            {
                { "Integer", value }
            });
        }

        public void SetOptionMaxResultsToList(int value)
        {
            CallAction("setOptionMaxResultsToList", new Dictionary<string, object>
            {
                { "Integer", value }
            });
        }

        public void SetOptionMaxScansInUI(int value)
        {
            CallAction("setOptionMaxScansInUI", new Dictionary<string, object>
            {
                { "Integer", value }
            });
        }

        public void SetOptionPromptInAttackMode(bool value)
        {
            CallAction("setOptionPromptInAttackMode", new Dictionary<string, object>
            {
                { "Boolean", value }
            });
        }

        public void SetOptionPromptToClearFinishedScans(bool value)
        {
            CallAction("setOptionPromptToClearFinishedScans", new Dictionary<string, object>
            {
                { "Boolean", value }
            });
        }

        public void SetOptionRescanInAttackMode(bool value)
        {
            CallAction("setOptionRescanInAttackMode", new Dictionary<string, object>
            {
                { "Boolean", value }
            });
        }

        public void SetOptionShowAdvancedDialog(bool value)
        {
            CallAction("setOptionShowAdvancedDialog", new Dictionary<string, object>
            {
                { "Boolean", value }
            });
        }

        public void SetOptionTargetParamsEnabledRPC(int value)
        {
            CallAction("setOptionTargetParamsEnabledRPC", new Dictionary<string, object>
            {
                { "Integer", value }
            });
        }

        public void SetOptionTargetParamsInjectable(int value)
        {
            CallAction("setOptionTargetParamsInjectable", new Dictionary<string, object>
            {
                { "Integer", value }
            });
        }

        public void SetOptionThreadPerHost(int value)
        {
            CallAction("setOptionThreadPerHost", new Dictionary<string, object>
            {
                { "Integer", value }
            });
        }

        public void SetPolicyAlertThreshold(int policyId, AlertThreshold alertThreshold, string scanPolicyName = null)
        {
            CallAction("setPolicyAlertThreshold", new Dictionary<string, object>
            {
                { "id", policyId },
                { "alertThreshold", alertThreshold },
                { "scanPolicyName", scanPolicyName }
            });
        }

        public void SetPolicyAttackStrength(int policyId, AttackStrength attackStrength, string scanPolicyName = null)
        {
            CallAction("setPolicyAttackStrength", new Dictionary<string, object>
            {
                { "id", policyId },
                { "attackStrength", attackStrength },
                { "scanPolicyName", scanPolicyName }
            });
        }

        public void SetScannerAlertThreshold(int scannerId, AlertThreshold alertThreshold, string scanPolicyName = null)
        {
            CallAction("setScannerAlertThreshold", new Dictionary<string, object>
            {
                { "id", scannerId },
                { "alertThreshold", alertThreshold },
                { "scanPolicyName", scanPolicyName }
            });
        }

        public void SetScannerAttackStrength(int scannerId, AttackStrength attackStrength, string scanPolicyName = null)
        {
            CallAction("setScannerAttackStrength", new Dictionary<string, object>
            {
                { "id", scannerId },
                { "attackStrength", attackStrength },
                { "scanPolicyName", scanPolicyName }
            });
        }

        public void Stop(int scanId)
        {
            CallAction("stop", new Dictionary<string, object>
            {
                { "scanId", scanId }
            });
        }

        public void StopAllScans()
        {
            CallAction("stopAllScans");
        }

        #endregion
    }
}
