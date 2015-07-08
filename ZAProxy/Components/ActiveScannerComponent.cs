using System;
using System.Collections.Generic;
using ZAProxy.Infrastructure;
using ZAProxy.Schema;

namespace ZAProxy.Components
{
    /// <summary>
    /// Component for actively scanning (i.e. attack) an applcation.
    /// </summary>
    public class ActiveScannerComponent : ComponentBase
    {
        /// <summary>
        /// Initiates a new instance of the <see cref="ActiveScannerComponent"/> class.
        /// </summary>
        /// <param name="zapProcess">The ZAP process to connect to.</param>
        public ActiveScannerComponent(IZapProcess zapProcess)
            : this(null, zapProcess)
        { }

        /// <summary>
        /// Initiates a new instance of the <see cref="ActiveScannerComponent"/> class with a specific HTTP client implementation.
        /// </summary>
        /// <param name="httpClient">The HTTP client implementation.</param>
        /// <param name="zapProcess">The ZAP process to connect to.</param>
        public ActiveScannerComponent(IHttpClient httpClient, IZapProcess zapProcess)
            : base(httpClient, zapProcess, "ascan")
        { }

        #region Views

        /// <summary>
        /// Gets the IDs of all the alerts from a specific scan.
        /// </summary>
        /// <param name="scanId">The ID of the scan.</param>
        /// <returns>All IDs of alerts from the scan.</returns>
        public IEnumerable<int> GetAlertIds(int scanId)
        {
            return CallView<IEnumerable<int>>("alertsIds", "alertsIds", new Parameters
            {
                { "scanId", scanId }
            });
        }

        /// <summary>
        /// Gets the amount of nodes still in the attack queue.
        /// </summary>
        /// <returns>Amount of nodes in the attack queue.</returns>
        public int? GetAttackModeQueue()
        {
            var value = CallView<int>("attackModeQueue", "attackModeQueue");
            return value == -1 ? (int?)null : value;
        }

        /// <summary>
        /// Gets all regex patterns for urls excluded from scanning.
        /// </summary>
        /// <returns>Regex patterns excluded from scanning.</returns>
        public IEnumerable<string> GetExcludedFromScan()
        {
            return CallView<IEnumerable<string>>("excludedFromScan", "excludedFromScan");
        }

        /// <summary>
        /// Gets the IDs of all the messages from a specific scan.
        /// </summary>
        /// <param name="scanId">The ID of the scan.</param>
        /// <returns>All IDs of messages from the scan.</returns>
        public IEnumerable<int> GetMessagesIds(int scanId)
        {
            return CallView<IEnumerable<int>>("messagesIds", "messagesIds", new Parameters
            {
                { "scanId", scanId }
            });
        }

        /// <summary>
        /// Gets whether the scanner goes into attack mode automatically on start.
        /// </summary>
        /// <returns>True if the scanner goes into attack mode on start.</returns>
        public bool GetOptionAllowAttackOnStart()
        {
            return CallView<bool>("optionAllowAttackOnStart", "AllowAttackOnStart");
        }

        /// <summary>
        /// Gets the attack scan policy name.
        /// </summary>
        /// <returns>The active attack scan policy name.</returns>
        public string GetOptionAttackPolicy()
        {
            return CallView<string>("optionAttackPolicy", "AttackPolicy");
        }

        /// <summary>
        /// Gets the default scan policy name.
        /// </summary>
        /// <returns>The default scan policy name.</returns>
        public string GetOptionDefaultPolicy()
        {
            return CallView<string>("optionDefaultPolicy", "DefaultPolicy");
        }

        /// <summary>
        /// Gets the amount of milliseconds between each request fired off by the scanner.
        /// </summary>
        /// <returns>The amount of milliseconds between each request.</returns>
        public int GetOptionDelayInMs()
        {
            return CallView<int>("optionDelayInMs", "DelayInMs");
        }

        /// <summary>
        /// Gets the parameters (header, cookie etc) excluded from fuzzing by the scanner.
        /// </summary>
        /// <remarks>
        /// Might be updated in a future version if ZAP fixes the bug.
        /// </remarks>
        /// <returns>The parameters excluded from fuzzing.</returns>
        [Obsolete("This call contains a bug in ZAP 2.4.0, which makes the output useless.")]
        public IEnumerable<string> GetOptionExcludedParamList()
        {
            return ParseJsonListString(CallView<string>("optionExcludedParamList", "ExcludedParamList"));
        }

        /// <summary>
        /// Gets whether the scanner understands, and implements CSRF methods gracefully.
        /// </summary>
        /// <returns>True if the scanner will remember CSRF tokens and send correct ones on requests.</returns>
        public bool GetOptionHandleAntiCSRFTokens()
        {
            return CallView<bool>("optionHandleAntiCSRFTokens", "HandleAntiCSRFTokens");
        }

        /// <summary>
        /// Gets the number of hosts scanned concurrently.
        /// </summary>
        /// <returns>Number of hosts scanned concurrently.</returns>
        public int GetOptionHostPerScan()
        {
            return CallView<int>("optionHostPerScan", "HostsPerScan");
        }

        /// <summary>
        /// Gets the maximum amount of results returned from a scan.
        /// </summary>
        /// <returns>Maximum amount of results returned from a scan.</returns>
        public int GetOptionMaxResultsToList()
        {
            return CallView<int>("optionMaxResultsToList", "MaxResultsToList");
        }

        /// <summary>
        /// Gets the maximum amount of (most recent) scans shown in the UI.
        /// </summary>
        /// <returns>Maximum amount of scans shown in the UI.</returns>
        public int GetOptionMaxScansInUI()
        {
            return CallView<int>("optionMaxScansInUI", "MaxScansInUI");
        }

        /// <summary>
        /// Gets whether ZAP prompts the user to rescan nodes when scope changes and ZAP is in attack mode.
        /// </summary>
        /// <returns>True if ZAP prompts user to rescan on scope change and in attack mode.</returns>
        public bool GetOptionPromptInAttackMode()
        {
            return CallView<bool>("optionPromptInAttackMode", "PromptInAttackMode");
        }

        /// <summary>
        /// Gets whether ZAP prompts the user when all finished scans are cleared.
        /// </summary>
        /// <returns>True if ZAP prompts the user when all finished scans are cleared.</returns>
        public bool GetOptionPromptToClearFinishedScans()
        {
            return CallView<bool>("optionPromptToClearFinishedScans", "PromptToClearFinishedScans");
        }

        /// <summary>
        /// Gets whether ZAP rescans when scope changes and ZAP is in attack mode.
        /// </summary>
        /// <returns>True if ZAP rescans on scope change and in attack mode.</returns>
        public bool GetOptionRescanInAttackMode()
        {
            return CallView<bool>("optionRescanInAttackMode", "RescanInAttackMode");
        }

        /// <summary>
        /// Gets whether ZAP shows the advanced options when starting a new active scan.
        /// </summary>
        /// <returns>True if ZAP shows advanced options on new active scan start.</returns>
        public bool GetOptionShowAdvancedDialog()
        {
            return CallView<bool>("optionShowAdvancedDialog", "ShowAdvancedDialog");
        }

        /// <summary>
        /// Gets the parameter content-types the scanner will attack.
        /// </summary>
        /// <returns>Parameter content-types the scanner will attack.</returns>
        public TargetEnabledRPC GetOptionTargetParamsEnabledRPC()
        {
            return CallView<TargetEnabledRPC>("optionTargetParamsEnabledRPC", "TargetParamsEnabledRPC");
        }

        /// <summary>
        /// Gets the parameter-types the scanner will attack.
        /// </summary>
        /// <returns>Parameter-types the scanner will attack.</returns>
        public TargetInjectable GetOptionTargetParamsInjectable()
        {
            return CallView<TargetInjectable>("optionTargetParamsInjectable", "TargetParamsInjectable");
        }

        /// <summary>
        /// Gets the number of threads used per host that is scanned.
        /// </summary>
        /// <returns>Number of threads used per host.</returns>
        public int GetOptionThreadPerHost()
        {
            return CallView<int>("optionThreadPerHost", "ThreadPerHost");
        }

        /// <summary>
        /// Gets the policies. Optionally filtered by a scan policy name or an ID.
        /// </summary>
        /// <param name="scanPolicyName">Optional scan policy name.</param>
        /// <param name="policyId">Optional policy ID.</param>
        /// <returns>The policies.</returns>
        public IEnumerable<Policy> GetPolicies(string scanPolicyName = null, int? policyId = null)
        {
            return CallView<IEnumerable<Policy>>("policies", "policies", new Parameters
            {
                { "scanPolicyName", scanPolicyName },
                { "policyId", policyId }
            });
        }

        /// <summary>
        /// Gets all scan policy names.
        /// </summary>
        /// <returns>All scan policy names.</returns>
        public IEnumerable<string> GetScanPolicyNames()
        {
            return ParseJsonListString(CallView<string>("scanPolicyNames", "scanPolicyNames"));
        }

        /// <summary>
        /// Gets the progress of a specific scan.
        /// </summary>
        /// <param name="scanId">The ID of the scan.</param>
        /// <returns>Progress of the specified scan.</returns>
        public ActiveScanProgress GetScanProgress(int scanId)
        {
            return CallView<ActiveScanProgress>("scanProgress", "scanProgress", new Parameters
            {
                { "scanId", scanId }
            });
        }

        /// <summary>
        /// Gets all the active scanners registered in ZAP. Optionally filtered by a scan policy name or an ID.
        /// </summary>
        /// <param name="scanPolicyName">Optional scan policy name.</param>
        /// <param name="policyId">Optional policy ID.</param>
        /// <returns>The scanners.</returns>
        public IEnumerable<Schema.ActiveScanner> GetScanners(string scanPolicyName = null, int? policyId = null)
        {
            return CallView<IEnumerable<Schema.ActiveScanner>>("scanners", "scanners", new Parameters
            {
                { "scanPolicyName", scanPolicyName },
                { "policyId", policyId }
            });
        }

        /// <summary>
        /// Gets all the scans.
        /// </summary>
        /// <returns>All the scans.</returns>
        public IEnumerable<ActiveScan> GetScans()
        {
            return CallView<IEnumerable<ActiveScan>>("scans", "scans");
        }

        /// <summary>
        /// Gets the completed percentage of a specific scan.
        /// </summary>
        /// <param name="scanId">Optional scan ID. Leave empty for latest scan.</param>
        /// <returns>Completed percentage of a scan.</returns>
        public int GetStatus(int? scanId = null)
        {
            return CallView<int>("status", "status", new Parameters
            {
                { "scanId", scanId }
            });
        }

        #endregion

        #region Actions

        /// <summary>
        /// Adds a scan policy.
        /// </summary>
        /// <param name="scanPolicyName">The name of the new policy.</param>
        public void AddScanPolicy(string scanPolicyName)
        {
            CallAction("addScanPolicy", new Parameters
            {
                { "scanPolicyName", scanPolicyName }
            });
        }

        /// <summary>
        /// Clears the list of scan exclusions.
        /// </summary>
        public void ClearExcludedFromScan()
        {
            CallAction("clearExcludedFromScan");
        }

        /// <summary>
        /// Disables all scanners. Optionally filtered by a scan policy name.
        /// </summary>
        /// <param name="scanPolicyName">Optional scan policy name.</param>
        public void DisableAllScanners(string scanPolicyName = null)
        {
            CallAction("disableAllScanners", new Parameters
            {
                { "scanPolicyName", scanPolicyName }
            });
        }

        /// <summary>
        /// Disables a set of scanners.
        /// </summary>
        /// <param name="scannerIds">The IDs of the scanners to disable.</param>
        public void DisableScanners(IEnumerable<int> scannerIds)
        {
            CallAction("disableScanners", new Parameters
            {
                { "ids", scannerIds.ToString(",") }
            });
        }

        /// <summary>
        /// Enables all scanners. Optionally filtered by a scan policy name.
        /// </summary>
        /// <param name="scanPolicyName">Optional scan policy name.</param>
        public void EnableAllScanners(string scanPolicyName = null)
        {
            CallAction("enableAllScanners", new Parameters
            {
                { "scanPolicyName", scanPolicyName }
            });
        }

        /// <summary>
        /// Enables a set of scanners
        /// </summary>
        /// <param name="scannerIds">The IDs of the scanners to enable.</param>
        public void EnableScanners(IEnumerable<int> scannerIds)
        {
            CallAction("enableScanners", new Parameters
            {
                { "ids", scannerIds.ToString(",") }
            });
        }

        /// <summary>
        /// Adds a regex to the scan exclusion list.
        /// </summary>
        /// <param name="regexPattern">The regex pattern to add to the exclusion list.</param>
        public void ExcludeFromScan(string regexPattern)
        {
            CallAction("excludeFromScan", new Parameters
            {
                { "regex", regexPattern }
            });
        }

        /// <summary>
        /// Pauses a scan.
        /// </summary>
        /// <param name="scanId">The ID of the scan to pause.</param>
        public void Pause(int scanId)
        {
            CallAction("pause", new Parameters
            {
                { "scanId", scanId }
            });
        }

        /// <summary>
        /// Pauses all scans.
        /// </summary>
        public void PauseAllScans()
        {
            CallAction("pauseAllScans");
        }

        /// <summary>
        /// Removes all scans.
        /// </summary>
        public void RemoveAllScans()
        {
            CallAction("removeAllScans");
        }

        /// <summary>
        /// Removes a scan.
        /// </summary>
        /// <param name="scanId">The ID of the scan to remove.</param>
        public void RemoveScan(int scanId)
        {
            CallAction("removeScan", new Parameters
            {
                { "scanId", scanId }
            });
        }

        /// <summary>
        /// Removes a scan policy.
        /// </summary>
        /// <param name="scanPolicyName">The name of the scan policy to remove.</param>
        public void RemoveScanPolicy(string scanPolicyName)
        {
            CallAction("removeScanPolicy", new Parameters
            {
                { "scanPolicyName", scanPolicyName }
            });
        }

        /// <summary>
        /// Resume a scan.
        /// </summary>
        /// <param name="scanId">The ID of the scan to resume.</param>
        public void Resume(int scanId)
        {
            CallAction("resume", new Parameters
            {
                { "scanId", scanId }
            });
        }
        
        /// <summary>
        /// Resume all scans.
        /// </summary>
        public void ResumeAllScans()
        {
            CallAction("resumeAllScans");
        }

        /// <summary>
        /// Creates a new scan.
        /// </summary>
        /// <param name="url">The base URL to scan.</param>
        /// <param name="recurse">Optional choice to recurse. Default is true.</param>
        /// <param name="inScopeOnly">Optional choice to keep scan within set scope. Default is false.</param>
        /// <param name="scanPolicyName">Optional scan policy name.</param>
        /// <param name="method">Optional method.</param>
        /// <param name="postData">Optional post data.</param>
        /// <returns>The ID of the newly created scan.</returns>
        public int Scan(string url, bool recurse = true, bool inScopeOnly = false, string scanPolicyName = null, string method = null, string postData = null)
        {
            return CallAction<int>("scan", "scan", new Parameters
            {
                { "url", url },
                { "recurse", recurse },
                { "inScopeOnly", inScopeOnly },
                { "scanPolicyName", scanPolicyName },
                { "method", method },
                { "postData", postData }
            });
        }

        /// <summary>
        /// Enables a set of policies.
        /// </summary>
        /// <param name="policyIds">The IDs of the policies to enable.</param>
        public void SetEnabledPolicies(IEnumerable<int> policyIds)
        {
            CallAction("setEnabledPolicies", new Parameters
            {
                { "ids", policyIds.ToString(",") }
            });
        }

        /// <summary>
        /// Sets whether the scanner goes into attack mode automatically on start.
        /// </summary>
        /// <param name="value">True if the scanner goes into attack mode on start.</param>
        public void SetOptionAllowAttackOnStart(bool value)
        {
            CallAction("setOptionAllowAttackOnStart", new Parameters
            {
                { "Boolean", value }
            });
        }

        /// <summary>
        /// Sets the attack scan policy name.
        /// </summary>
        /// <param name="value">The scan policy name.</param>
        public void SetOptionAttackPolicy(string value)
        {
            CallAction("setOptionAttackPolicy", new Parameters
            {
                { "String", value }
            });
        }

        /// <summary>
        /// Sets the default scan policy name.
        /// </summary>
        /// <param name="value">The default scan policy name.</param>
        public void SetOptionDefaultPolicy(string value)
        {
            CallAction("setOptionDefaultPolicy", new Parameters
            {
                { "String", value }
            });
        }

        /// <summary>
        /// Sets the amount of milliseconds between each request fired off by the scanner.
        /// </summary>
        /// <param name="value">The amount of milliseconds between each request.</param>
        public void SetOptionDelayInMs(int value)
        {
            CallAction("setOptionDelayInMs", new Parameters
            {
                { "Integer", value }
            });
        }

        /// <summary>
        /// Sets whether the scanner understands, and implements CSRF methods gracefully.
        /// </summary>
        /// <param name="value">True if the scanner will remember CSRF tokens and send corrrect ones on requests.</param>
        public void SetOptionHandleAntiCSRFTokens(bool value)
        {
            CallAction("setOptionHandleAntiCSRFTokens", new Parameters
            {
                { "Boolean", value }
            });
        }
        
        /// <summary>
        /// Sets the number of hosts scanned concurrently.
        /// </summary>
        /// <param name="value">Number of hosts scanned concurrently.</param>
        public void SetOptionHostPerScan(int value)
        {
            CallAction("setOptionHostPerScan", new Parameters
            {
                { "Integer", value }
            });
        }
        
        /// <summary>
        /// Sets the maximum amount of results returned from a set.
        /// </summary>
        /// <param name="value">Maximum amount of results returned from a scan.</param>
        public void SetOptionMaxResultsToList(int value)
        {
            CallAction("setOptionMaxResultsToList", new Parameters
            {
                { "Integer", value }
            });
        }

        /// <summary>
        /// Sets the maximum amount of (most recent) scans shown in the UI.
        /// </summary>
        /// <param name="value">Maximum amount of scans shown in the UI.</param>
        public void SetOptionMaxScansInUI(int value)
        {
            CallAction("setOptionMaxScansInUI", new Parameters
            {
                { "Integer", value }
            });
        }

        /// <summary>
        /// Sets whether ZAP prompts the user to rescan nodes when scope changes and ZAP is in attack mode.
        /// </summary>
        /// <param name="value">True if ZAP should prompt user to rescan on scope change and in attack mode.</param>
        public void SetOptionPromptInAttackMode(bool value)
        {
            CallAction("setOptionPromptInAttackMode", new Parameters
            {
                { "Boolean", value }
            });
        }

        /// <summary>
        /// Sets whether ZAP prompts the user when all finished scans are cleared.
        /// </summary>
        /// <param name="value">True if ZAP should prompt the user when all finished scans are cleared.</param>
        public void SetOptionPromptToClearFinishedScans(bool value)
        {
            CallAction("setOptionPromptToClearFinishedScans", new Parameters
            {
                { "Boolean", value }
            });
        }

        /// <summary>
        /// Sets whether ZAP rescans when scope changes and ZAP is in attack mode.
        /// </summary>
        /// <param name="value">True if ZAP should rescan on scope change and in attack mode.</param>
        public void SetOptionRescanInAttackMode(bool value)
        {
            CallAction("setOptionRescanInAttackMode", new Parameters
            {
                { "Boolean", value }
            });
        }

        /// <summary>
        /// Sets whether ZAP shows the advanced options when starting a new active scan.
        /// </summary>
        /// <param name="value">True if ZAP should show advanced options on new active scan start.</param>
        public void SetOptionShowAdvancedDialog(bool value)
        {
            CallAction("setOptionShowAdvancedDialog", new Parameters
            {
                { "Boolean", value }
            });
        }

        /// <summary>
        /// Sets the parameter content-types the scanner will attack.
        /// </summary>
        /// <param name="value">Parameter content-types the scanner will attack.</param>
        public void SetOptionTargetParamsEnabledRPC(TargetEnabledRPC value)
        {
            CallAction("setOptionTargetParamsEnabledRPC", new Parameters
            {
                { "Integer", value }
            });
        }

        /// <summary>
        /// Sets the parameter-types the scanner will attack.
        /// </summary>
        /// <param name="value">Parameter-types the scanner will attack.</param>
        public void SetOptionTargetParamsInjectable(TargetInjectable value)
        {
            CallAction("setOptionTargetParamsInjectable", new Parameters
            {
                { "Integer", value }
            });
        }

        /// <summary>
        /// Sets the number of threads used per host that is scanned.
        /// </summary>
        /// <param name="value">Number of threads used per host.</param>
        public void SetOptionThreadPerHost(int value)
        {
            CallAction("setOptionThreadPerHost", new Parameters
            {
                { "Integer", value }
            });
        }

        /// <summary>
        /// Sets the alert threshold for a specific policy. Optionally filtered by a scan policy.
        /// </summary>
        /// <param name="policyId">The ID of the policy.</param>
        /// <param name="alertThreshold">The alert threshold.</param>
        /// <param name="scanPolicyName">Optional scan policy name.</param>
        public void SetPolicyAlertThreshold(int policyId, AlertThreshold alertThreshold, string scanPolicyName = null)
        {
            CallAction("setPolicyAlertThreshold", new Parameters
            {
                { "id", policyId },
                { "alertThreshold", alertThreshold },
                { "scanPolicyName", scanPolicyName }
            });
        }

        /// <summary>
        /// Sets the attack strength for a specific policy. Optionally filtered by a scan policy.
        /// </summary>
        /// <param name="policyId">The ID of the policy.</param>
        /// <param name="attackStrength">The attack strength.</param>
        /// <param name="scanPolicyName">Optional scan policy name</param>
        public void SetPolicyAttackStrength(int policyId, AttackStrength attackStrength, string scanPolicyName = null)
        {
            CallAction("setPolicyAttackStrength", new Parameters
            {
                { "id", policyId },
                { "attackStrength", attackStrength },
                { "scanPolicyName", scanPolicyName }
            });
        }

        /// <summary>
        /// Sets the alert threshold for a specific scanner. Optionally filtered by a scan policy.
        /// </summary>
        /// <param name="scannerId">The ID of the scanner.</param>
        /// <param name="alertThreshold">The alert threshold.</param>
        /// <param name="scanPolicyName">Optional scan policy name.</param>
        public void SetScannerAlertThreshold(int scannerId, AlertThreshold alertThreshold, string scanPolicyName = null)
        {
            CallAction("setScannerAlertThreshold", new Parameters
            {
                { "id", scannerId },
                { "alertThreshold", alertThreshold },
                { "scanPolicyName", scanPolicyName }
            });
        }

        /// <summary>
        /// Sets the attack strength for a specific scanner. Optionally filtered by a scan policy.
        /// </summary>
        /// <param name="scannerId">The ID of the scanner.</param>
        /// <param name="attackStrength">The attack strength.</param>
        /// <param name="scanPolicyName">Optional scan policy name</param>
        public void SetScannerAttackStrength(int scannerId, AttackStrength attackStrength, string scanPolicyName = null)
        {
            CallAction("setScannerAttackStrength", new Parameters
            {
                { "id", scannerId },
                { "attackStrength", attackStrength },
                { "scanPolicyName", scanPolicyName }
            });
        }

        /// <summary>
        /// Stops a scan.
        /// </summary>
        /// <param name="scanId">The ID of the scan.</param>
        public void Stop(int scanId)
        {
            CallAction("stop", new Parameters
            {
                { "scanId", scanId }
            });
        }

        /// <summary>
        /// Stops all scans.
        /// </summary>
        public void StopAllScans()
        {
            CallAction("stopAllScans");
        }

        #endregion
    }
}
