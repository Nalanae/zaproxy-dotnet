using System.Collections.Generic;
using ZAProxy.Infrastructure;
using ZAProxy.Schema;

namespace ZAProxy.Components
{
    /// <summary>
    /// Component to manage the passive scanner.
    /// </summary>
    public class PassiveScanner : ComponentBase
    {
        /// <summary>
        /// Initiates a new instance of the <see cref="PassiveScanner"/> class.
        /// </summary>
        /// <param name="zapProcess">The ZAP process to connect to.</param>
        public PassiveScanner(IZapProcess zapProcess)
            : this(null, zapProcess)
        { }

        /// <summary>
        /// Initiates a new instance of the <see cref="PassiveScanner"/> class with a specific HTTP client implementation.
        /// </summary>
        /// <param name="httpClient">The HTTP client implementation.</param>
        /// <param name="zapProcess">The ZAP process to connect to.</param>
        public PassiveScanner(IHttpClient httpClient, IZapProcess zapProcess)
            : base(httpClient, zapProcess, "pscan")
        { }

        #region Views

        /// <summary>
        /// Gets the number of messages the passive scanner still has to scan.
        /// </summary>
        /// <returns>Number of messages the passive scanner still has to scan.</returns>
        public int GetRecordsToScan()
        {
            return CallView<int>("recordsToScan", "recordsToScan");
        }

        /// <summary>
        /// Gets all the passive scanners registered in ZAP.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Schema.PassiveScanner> GetScanners()
        {
            return CallView<IEnumerable<Schema.PassiveScanner>>("scanners", "scanners");
        }

        #endregion

        #region Actions

        /// <summary>
        /// Disables all the passive scanners.
        /// </summary>
        public void DisableAllScanners()
        {
            CallAction("disableAllScanners");
        }

        /// <summary>
        /// Disables one or more scanners.
        /// </summary>
        /// <param name="ids">The IDs of the scanners to disable.</param>
        public void DisableScanners(params int[] ids)
        {
            CallAction("disableScanners", new Parameters
            {
                { "ids", string.Join(",", ids) }
            });
        }

        /// <summary>
        /// Enables all the passive scanners.
        /// </summary>
        public void EnableAllScanners()
        {
            CallAction("enableAllScanners");
        }

        /// <summary>
        /// Enables one or more scanners.
        /// </summary>
        /// <param name="ids">The IDs of the scanners to enable.</param>
        public void EnableScanners(params int[] ids)
        {
            CallAction("enableScanners", new Parameters
            {
                { "ids", string.Join(",", ids) }
            });
        }

        /// <summary>
        /// Sets whether passive scanning is enabled.
        /// </summary>
        /// <param name="enabled">True if passive scanning should be enabled.</param>
        public void SetEnabled(bool enabled)
        {
            CallAction("setEnabled", new Parameters
            {
                { "enabled", enabled }
            });
        }

        /// <summary>
        /// Sets the alert threshold of a specific passive scanner.
        /// </summary>
        /// <param name="id">The ID of the passive scanner.</param>
        /// <param name="alertThreshold">The alert threshold.</param>
        public void SetScannerAlertThreshold(int id, AlertThreshold alertThreshold)
        {
            CallAction("setScannerAlertThreshold", new Parameters
            {
                { "id", id },
                { "alertThreshold", alertThreshold }
            });
        }

        #endregion
    }
}
