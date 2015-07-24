using System;
using System.Collections.Generic;
using ZAProxy.Infrastructure;
using ZAProxy.Schema;

namespace ZAProxy.Components
{
    /// <summary>
    /// Component for managing the spider of ZAP.
    /// </summary>
    public class SpiderComponent : ComponentBase
    {
        /// <summary>
        /// Initiates a new instance of the <see cref="SpiderComponent"/> class.
        /// </summary>
        /// <param name="zapProcess">The ZAP process to connect to.</param>
        public SpiderComponent(IZapProcess zapProcess)
            : this(null, zapProcess)
        { }

        /// <summary>
        /// Initiates a new instance of the <see cref="SpiderComponent"/> class with a specific HTTP client implementation.
        /// </summary>
        /// <param name="httpClient">The HTTP client implementation.</param>
        /// <param name="zapProcess">The ZAP process to connect to.</param>
        public SpiderComponent(IHttpClient httpClient, IZapProcess zapProcess)
            : base(httpClient, zapProcess, "spider")
        { }

        #region Views

        /// <summary>
        /// Gets the urls that are excluded from the spider.
        /// </summary>
        /// <returns>Urls excluded from the spider.</returns>
        public IEnumerable<string> GetExcludedFromScan()
        {
            return CallView<IEnumerable<string>>("excludedFromScan", "excludedFromScan");
        }

        /// <summary>
        /// Gets the full results of a scan.
        /// </summary>
        /// <param name="scanId">The ID of a scan.</param>
        /// <returns>Full results of a scan.</returns>
        public SpiderScanResult GetFullResults(int scanId)
        {
            return CallView<SpiderScanResult>("fullResults", "fullResults", new Parameters
            {
                { "scanId", scanId }
            });
        }

        /// <summary>
        /// Gets the domains that are always in scope.
        /// </summary>
        /// <remarks>
        /// Might be updated in a future version if ZAP fixes the bug.
        /// </remarks>
        /// <returns>The domains that are always in scope.</returns>
        [Obsolete("This call contains a bug in ZAP 2.4.0, which makes the output useless.")]
        public IEnumerable<string> GetOptionDomainsAlwaysInScope()
        {
            return ParseJsonListString(CallView<string>("optionDomainsAlwaysInScope", "DomainsAlwaysInScope"));
        }


        /// <summary>
        /// Gets whether the domains always in scope setting is enabled.
        /// </summary>
        /// <remarks>
        /// Might be updated in a future version if ZAP fixes the bug.
        /// </remarks>
        /// <returns>True if the domains always in scope setting is enabled.</returns>
        [Obsolete("This call contains a bug in ZAP 2.4.0, which makes the output useless.")]
        public bool GetOptionDomainsAlwaysInScopeEnabled()
        {
            return CallView<bool>("optionDomainsAlwaysInScopeEnabled", "DomainsAlwaysInScopeEnabled");
        }

        /// <summary>
        /// Gets whether the spider takes into account OData-specific parameters (i.e : resource identifiers) in order to identify already visited URL.
        /// </summary>
        /// <returns>True if spider takes into account OData-specific parameters in order to identify already visited URL.</returns>
        public bool GetOptionHandleODataParametersVisited()
        {
            return CallView<bool>("optionHandleODataParametersVisited", "HandleODataParametersVisited");
        }

        /// <summary>
        /// Gets how the spider handles parameters when checking URIs visited.
        /// </summary>
        /// <returns>How the spider handles parameters when checking URIs visited.</returns>
        public HandleParametersOption GetOptionHandleParameters()
        {
            return CallView<HandleParametersOption>("optionHandleParameters", "HandleParameters");
        }

        /// <summary>
        /// Gets the maximum branch depth of the spider.
        /// </summary>
        /// <returns>Maximum branch depth of the spider.</returns>
        public int GetOptionMaxDepth()
        {
            return CallView<int>("optionMaxDepth", "MaxDepth");
        }

        /// <summary>
        /// Gets the maximum amount of scans shown in the UI.
        /// </summary>
        /// <returns>Maximum amount of scans shown in the UI.</returns>
        public int GetOptionMaxScansInUI()
        {
            return CallView<int>("optionMaxScansInUI", "MaxScansInUI");
        }

        /// <summary>
        /// Gets whethet the spider parses the comments.
        /// </summary>
        /// <returns>True if the spider parses the comments.</returns>
        public bool GetOptionParseComments()
        {
            return CallView<bool>("optionParseComments", "ParseComments");
        }

        /// <summary>
        /// Gets whether the spider parses the Git files for URIs.
        /// </summary>
        /// <returns>True if the spider parses the Git files for URIs</returns>
        public bool GetOptionParseGit()
        {
            return CallView<bool>("optionParseGit", "ParseGit");
        }

        /// <summary>
        /// Gets whether the spider parses the robot.txt for URIs.
        /// </summary>
        /// <returns>True if the spider parses the robots.txt for URIs.</returns>
        public bool GetOptionParseRobotsTxt()
        {
            return CallView<bool>("optionParseRobotsTxt", "ParseRobotsTxt");
        }

        /// <summary>
        /// Gets whether the spider parses SVN entries for URIs.
        /// </summary>
        /// <returns>True if the spider parses SVN entriesfor URIs.</returns>
        public bool GetOptionParseSVNEntries()
        {
            return CallView<bool>("optionParseSVNEntries", "ParseSVNEntries");
        }

        /// <summary>
        /// Gets whether the spider parses the sitemap.xml for URIs.
        /// </summary>
        /// <returns>True if the spider parses the sitemap.xml for URIs.</returns>
        public bool GetOptionParseSitemapXml()
        {
            return CallView<bool>("optionParseSitemapXml", "ParseSitemapXml");
        }

        /// <summary>
        /// Gets whether the spider submits forms with the POST verb.
        /// </summary>
        /// <returns>True if the spider submits forms with the POST verb.</returns>
        public bool GetOptionPostForm()
        {
            return CallView<bool>("optionPostForm", "PostForm");
        }

        /// <summary>
        /// Gets whether the spider processes forms.
        /// </summary>
        /// <returns>True if the spider processes forms.</returns>
        public bool GetOptionProcessForm()
        {
            return CallView<bool>("optionProcessForm", "ProcessForm");
        }

        /// <summary>
        /// Gets the amount of time the spider waits between requests (in milliseconds).
        /// </summary>
        /// <returns>Amount of time the spider waits between requests.</returns>
        public int GetOptionRequestWaitTime()
        {
            return CallView<int>("optionRequestWaitTime", "RequestWaitTime");
        }

        /// <summary>
        /// Gets the regex pattern that determines the scope.
        /// </summary>
        /// <remarks>
        /// Depricated in ZAP 2.3.0 in favor of <see cref="GetOptionDomainsAlwaysInScope"/>.
        /// </remarks>
        /// <returns>Regex pattern that determines the scope.</returns>
        [Obsolete("Depricated in ZAP 2.3.0 in favor of GetOptionDomainsAlwaysInScope")]
        public string GetOptionScope()
        {
            return CallView<string>("optionScope", "Scope");
        }

        /// <summary>
        /// Gets the text that determines the scope.
        /// </summary>
        /// <remarks>
        /// Depricated in ZAP 2.3.0 in favor of <see cref="GetOptionDomainsAlwaysInScope"/>.
        /// </remarks>
        /// <returns>Text that determines the scope.</returns>
        [Obsolete("Depricated in ZAP 2.3.0 in favor of GetOptionDomainsAlwaysInScope")]
        public string GetOptionScopeText()
        {
            return CallView<string>("optionScopeText", "ScopeText");
        }

        /// <summary>
        /// Gets whether the spider sends a referer header on subsequent requests.
        /// </summary>
        /// <returns>True if the spider sends a referer heaader on subsequent requests.</returns>
        public bool GetOptionSendRefererHeader()
        {
            return CallView<bool>("optionSendRefererHeader", "SendRefererHeader");
        }

        /// <summary>
        /// Gets whether the spider UI will show the advanced options.
        /// </summary>
        /// <returns>True if the spider UI will show advanced options.</returns>
        public bool GetOptionShowAdvancedDialog()
        {
            return CallView<bool>("optionShowAdvancedDialog", "ShowAdvancedDialog");
        }

        /// <summary>
        /// Gets the skip url regex pattern.
        /// </summary>
        /// <returns>Skip url regex pattern.</returns>
        public string GetOptionSkipURLString()
        {
            return CallView<string>("optionSkipURLString", "SkipURLString");
        }

        /// <summary>
        /// Gets the amount of threads used by the spider.
        /// </summary>
        /// <returns>Amount of threads used by the spider.</returns>
        public int GetOptionThreadCount()
        {
            return CallView<int>("optionThreadCount", "ThreadCount");
        }

        /// <summary>
        /// Gets the user agent string used by the spider.
        /// </summary>
        /// <returns>User agent string used by the spider.</returns>
        public string GetOptionUserAgent()
        {
            return CallView<string>("optionUserAgent", "UserAgent");
        }

        /// <summary>
        /// Gets all urls found by the spider during a scan.
        /// </summary>
        /// <param name="scanId">The ID of the scan.</param>
        /// <returns>Urls found by the spider.</returns>
        public IEnumerable<string> GetResults(int scanId)
        {
            return CallView<IEnumerable<string>>("results", "results", new Parameters
            {
                { "scanId", scanId }
            });
        }

        /// <summary>
        /// Gets all the scans and their status.
        /// </summary>
        /// <returns>All the scans and their status.</returns>
        public IEnumerable<SpiderScan> GetScans()
        {
            return CallView<IEnumerable<SpiderScan>>("scans", "scans");
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
        /// Clears all the exclusions from the spider.
        /// </summary>
        public void ClearExcludedFromScan()
        {
            CallAction("clearExcludedFromScan");
        }

        /// <summary>
        /// Excludes a regex pattern from the spider.
        /// </summary>
        /// <param name="regex">The regex pattern.</param>
        public void ExcludeFromScan(string regex)
        {
            CallAction("excludeFromScan", new Parameters
            {
                { "regex", regex }
            });
        }

        /// <summary>
        /// Pauses a specific scan.
        /// </summary>
        /// <param name="scanId">The ID of the scan.</param>
        public void Pause(int scanId)
        {
            CallAction("pause", new Parameters
            {
                { "scanId", scanId }
            });
        }

        /// <summary>
        /// Pauses all the scans.
        /// </summary>
        public void PauseAllScans()
        {
            CallAction("pauseAllScans");
        }

        /// <summary>
        /// Removes all the scans.
        /// </summary>
        public void RemoveAllScans()
        {
            CallAction("removeAllScans");
        }

        /// <summary>
        /// Removes a specific scan.
        /// </summary>
        /// <param name="scanId">The ID of the scan.</param>
        public void RemoveScan(int scanId)
        {
            CallAction("removeScan", new Parameters
            {
                { "scanId", scanId }
            });
        }

        /// <summary>
        /// Resumes a specific scan.
        /// </summary>
        /// <param name="scanId">The ID of the scan.</param>
        public void Resume(int scanId)
        {
            CallAction("resume", new Parameters
            {
                { "scanId", scanId }
            });
        }

        /// <summary>
        /// Resumes all the scans.
        /// </summary>
        public void ResumeAllScans()
        {
            CallAction("resumeAllScans");
        }

        /// <summary>
        /// Starts a new scan.
        /// </summary>
        /// <param name="url">The url where the spider starts.</param>
        /// <param name="maxChildren">Optionally the amount of links deep the spider will go.</param>
        /// <returns>The ID of the scan.</returns>
        public int Scan(string url, int? maxChildren = null)
        {
            return CallAction<int>("scan", "scan", new Parameters
            {
                { "url", url },
                { "maxChildren", maxChildren }
            });
        }

        /// <summary>
        /// Starts a new scan as a specific user.
        /// </summary>
        /// <param name="url">The url where the spider starts.</param>
        /// <param name="contextId">The context which is used for the authentication.</param>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="maxChildren">The amount of links deep the spider will go.</param>
        /// <returns>The ID of the scan.</returns>
        public int ScanAsUser(string url, int contextId, int userId, int maxChildren)
        {
            return CallAction<int>("scanAsUser", "scanAsUser", new Parameters
            {
                { "url", url },
                { "contextId", contextId },
                { "userId", userId },
                { "maxChildren", maxChildren }
            });
        }

        /// <summary>
        /// Sets whether the spider takes into account OData-specific parameters (i.e : resource identifiers) in order to identify already visited URL.
        /// </summary>
        /// <param name="value">True if spider should take into account OData-specific parameters in order to identify already visited URL.</param>
        public void SetOptionHandleODataParametersVisited(bool value)
        {
            CallAction("setOptionHandleODataParametersVisited", new Parameters
            {
                { "Boolean", value }
            });
        }

        /// <summary>
        /// Sets how the spider handles parameters when checking URIs visited.
        /// </summary>
        /// <param name="value">How the spider should handle parameters when checking URIs visited.</param>
        public void SetOptionHandleParameters(HandleParametersOption value)
        {
            CallAction("setOptionHandleParameters", new Parameters
            {
                { "String", value.ToString().ToUpperInvariant() }
            });
        }

        /// <summary>
        /// Sets the maximum branch depth of the spider.
        /// </summary>
        /// <param name="value">Maximum branch depth the spider should use.</param>
        public void SetOptionMaxDepth(int value)
        {
            CallAction("setOptionMaxDepth", new Parameters
            {
                { "Integer", value }
            });
        }

        /// <summary>
        /// Sets the maximum amount of scans shown in the UI.
        /// </summary>
        /// <param name="value">Maximum amount of scans that should be shown in the UI.</param>
        public void SetOptionMaxScansInUI(int value)
        {
            CallAction("setOptionMaxScansInUI", new Parameters
            {
                { "Integer", value }
            });
        }

        /// <summary>
        /// Sets whethet the spider parses the comments.
        /// </summary>
        /// <param name="value">True if the spider should parse the comments.</param>
        public void SetOptionParseComments(bool value)
        {
            CallAction("setOptionParseComments", new Parameters
            {
                { "Boolean", value }
            });
        }

        /// <summary>
        /// Sets whether the spider parses the Git files for URIs.
        /// </summary>
        /// <param name="value">True if the spider should parse the Git files for URIs</param>
        public void SetOptionParseGit(bool value)
        {
            CallAction("setOptionParseGit", new Parameters
            {
                { "Boolean", value }
            });
        }

        /// <summary>
        /// Sets whether the spider parses the robot.txt for URIs.
        /// </summary>
        /// <param name="value">True if the spider should parse the robots.txt for URIs.</param>
        public void SetOptionParseRobotsTxt(bool value)
        {
            CallAction("setOptionParseRobotsTxt", new Parameters
            {
                { "Boolean", value }
            });
        }

        /// <summary>
        /// Sets whether the spider parses SVN entries for URIs.
        /// </summary>
        /// <param name="value">True if the spider should parse SVN entriesfor URIs.</param>
        public void SetOptionParseSVNEntries(bool value)
        {
            CallAction("setOptionParseSVNEntries", new Parameters
            {
                { "Boolean", value }
            });
        }

        /// <summary>
        /// Sets whether the spider parses the sitemap.xml for URIs.
        /// </summary>
        /// <param name="value">True if the spider should parse the sitemap.xml for URIs.</param>
        public void SetOptionParseSitemapXml(bool value)
        {
            CallAction("setOptionParseSitemapXml", new Parameters
            {
                { "Boolean", value }
            });
        }

        /// <summary>
        /// Sets whether the spider submits forms with the POST verb.
        /// </summary>
        /// <param name="value">True if the spider should submit forms with the POST verb.</param>
        public void SetOptionPostForm(bool value)
        {
            CallAction("setOptionPostForm", new Parameters
            {
                { "Boolean", value }
            });
        }

        /// <summary>
        /// Sets whether the spider processes forms.
        /// </summary>
        /// <param name="value">True if the spider should process forms.</param>
        public void SetOptionProcessForm(bool value)
        {
            CallAction("setOptionProcessForm", new Parameters
            {
                { "Boolean", value }
            });
        }

        /// <summary>
        /// Sets the amount of time the spider waits between requests (in milliseconds).
        /// </summary>
        /// <param name="value">Amount of time the spider should wait between requests.</param>
        public void SetOptionRequestWaitTime(int value)
        {
            CallAction("setOptionRequestWaitTime", new Parameters
            {
                { "Integer", value }
            });
        }

        /// <summary>
        /// Sets the regex pattern that determines the scope.
        /// </summary>
        /// <param name="value">Regex pattern that should determine the scope.</param>
        public void SetOptionScopeString(string value)
        {
            CallAction("setOptionScopeString", new Parameters
            {
                { "String", value }
            });
        }

        /// <summary>
        /// Sets whether the spider sends a referer header on subsequent requests.
        /// </summary>
        /// <param name="value">True if the spider should send a referer heaader on subsequent requests.</param>
        public void SetOptionSendRefererHeader(bool value)
        {
            CallAction("setOptionSendRefererHeader", new Parameters
            {
                { "Boolean", value }
            });
        }

        /// <summary>
        /// Sets whether the spider UI will show the advanced options.
        /// </summary>
        /// <param name="value">True if the spider UI should show advanced options.</param>
        public void SetOptionShowAdvancedDialog(bool value)
        {
            CallAction("setOptionShowAdvancedDialog", new Parameters
            {
                { "Boolean", value }
            });
        }

        /// <summary>
        /// Sets the skip url regex pattern.
        /// </summary>
        /// <param name="value">Skip url regex pattern.</param>
        public void SetOptionSkipURLString(string value)
        {
            CallAction("setOptionSkipURLString", new Parameters
            {
                { "String", value }
            });
        }

        /// <summary>
        /// Sets the amount of threads used by the spider.
        /// </summary>
        /// <param name="value">Amount of threads the spider should use.</param>
        public void SetOptionThreadCount(int value)
        {
            CallAction("setOptionThreadCount", new Parameters
            {
                { "Integer", value }
            });
        }

        /// <summary>
        /// Sets the user agent string used by the spider.
        /// </summary>
        /// <param name="value">User agent string the spider should use.</param>
        public void SetOptionUserAgent(string value)
        {
            CallAction("setOptionUserAgent", new Parameters
            {
                { "String", value }
            });
        }

        /// <summary>
        /// Stops a specific scan.
        /// </summary>
        /// <param name="scanId">Optionally the ID of the scan. If it's left empty, the newest scan is used.</param>
        public void Stop(int? scanId = null)
        {
            CallAction("stop", new Parameters
            {
                { "scanId", scanId }
            });
        }

        /// <summary>
        /// Stops all the scans.
        /// </summary>
        public void StopAllScans()
        {
            CallAction("stopAllScans");
        }

        #endregion
    }
}
