using ZAProxy.Infrastructure;

namespace ZAProxy.Components
{
    /// <summary>
    /// Component that manages the updating of ZAP and it's components.
    /// </summary>
    public class AutoUpdateComponent : ComponentBase
    {
        /// <summary>
        /// Initiates a new instance of the <see cref="AutoUpdateComponent"/> class.
        /// </summary>
        /// <param name="zapProcess">The ZAP process to connect to.</param>
        public AutoUpdateComponent(IZapProcess zapProcess)
            : this(null, zapProcess)
        { }

        /// <summary>
        /// Initiates a new instance of the <see cref="AutoUpdateComponent"/> class with a specific HTTP client implementation.
        /// </summary>
        /// <param name="httpClient">The HTTP client implementation.</param>
        /// <param name="zapProcess">The ZAP process to connect to.</param>
        public AutoUpdateComponent(IHttpClient httpClient, IZapProcess zapProcess)
            : base(httpClient, zapProcess, "autoupdate")
        { }

        #region Views

        /// <summary>
        /// Gets whether the current running version of ZAP is the latest.
        /// </summary>
        /// <returns></returns>
        public bool IsLatestVersion()
        {
            return CallView<bool>("isLatestVersion", "isLatestVersion");
        }

        /// <summary>
        /// Gets the latest version number of ZAP.
        /// Can be either a daily build or release number, depending on what type of version of ZAP you are running.
        /// </summary>
        /// <returns>Latest version number of ZAP.</returns>
        public string GetLatestVersionNumber()
        {
            return CallView<string>("latestVersionNumber", "latestVersionNumber");
        }

        /// <summary>
        /// Gets whether ZAP automatically checks for updates.
        /// </summary>
        /// <returns>True if ZAP automatically checks for updates.</returns>
        public bool GetOptionCheckAddonUpdates()
        {
            return CallView<bool>("optionCheckAddonUpdates", "CheckAddonUpdates");
        }

        /// <summary>
        /// Gets whether ZAP checks for updates when the application is started.
        /// </summary>
        /// <returns>True if ZAP checks for updates when the application is started.</returns>
        public bool GetOptionCheckOnStart()
        {
            return CallView<bool>("optionCheckOnStart", "CheckOnStart");
        }

        /// <summary>
        /// Gets whether ZAP automatically downloads new releases.
        /// </summary>
        /// <returns>True if ZAP automatically downloads new releases.</returns>
        public bool GetOptionDownloadNewRelease()
        {
            return CallView<bool>("optionDownloadNewRelease", "DownloadNewRelease");
        }

        /// <summary>
        /// Gets whether ZAP automatically installs addon updates.
        /// </summary>
        /// <returns>True if ZAP automatically installs addon updates.</returns>
        public bool GetOptionInstallAddonUpdates()
        {
            return CallView<bool>("optionInstallAddonUpdates", "InstallAddonUpdates");
        }

        /// <summary>
        /// Gets whether ZAP automatically installs updated scanner rules.
        /// </summary>
        /// <returns>True if ZAP automatically installs updated scanner rules.</returns>
        public bool GetOptionInstallScannerRules()
        {
            return CallView<bool>("optionInstallScannerRules", "InstallScannerRules");
        }

        /// <summary>
        /// Gets whether ZAP reports alpha releases of addons as updates.
        /// </summary>
        /// <returns>True if ZAP reports alpha releases of addons as updates.</returns>
        public bool GetOptionReportAlphaAddons()
        {
            return CallView<bool>("optionReportAlphaAddons", "ReportAlphaAddons");
        }

        /// <summary>
        /// Gets whether ZAP reports beta releases of addons as updates.
        /// </summary>
        /// <returns>True if ZAP reports beta releases of addons as updates.</returns>
        public bool GetOptionReportBetaAddons()
        {
            return CallView<bool>("optionReportBetaAddons", "ReportBetaAddons");
        }

        /// <summary>
        /// Gets whether ZAP reports stable releases of addons as updates.
        /// </summary>
        /// <returns>True if ZAP reports stable releases of addons as updates.</returns>
        public bool GetOptionReportReleaseAddons()
        {
            return CallView<bool>("optionReportReleaseAddons", "ReportReleaseAddons");
        }

        #endregion

        #region Actions

        /// <summary>
        /// Downloads the latest release of ZAP.
        /// </summary>
        public void DownloadLatestRelease()
        {
            CallAction("downloadLatestRelease");
        }

        /// <summary>
        /// Sets whether ZAP should automatically check for addon updates.
        /// </summary>
        /// <param name="value">True if ZAP should automatically check for addon updates.</param>
        public void SetOptionCheckAddonUpdates(bool value)
        {
            CallAction("optionCheckAddonUpdates", new Parameters
            {
                { "Boolean", value }
            });
        }

        /// <summary>
        /// Sets whether ZAP should check for updates when the application is started.
        /// </summary>
        /// <param name="value">True if ZAP should check for updates when the application is started.</param>
        public void SetOptionCheckOnStart(bool value)
        {
            CallAction("optionCheckOnStart", new Parameters
            {
                { "Boolean", value }
            });
        }

        /// <summary>
        /// Sets whether ZAP should automatically download new releases.
        /// </summary>
        /// <param name="value">True if ZAP should automatically download new releases.</param>
        public void SetOptionDownloadNewRelease(bool value)
        {
            CallAction("optionDownloadNewRelease", new Parameters
            {
                { "Boolean", value }
            });
        }

        /// <summary>
        /// Sets whether ZAP should automatically install addon updates.
        /// </summary>
        /// <param name="value">True if ZAP should automatically install addon updates.</param>
        public void SetOptionInstallAddonUpdates(bool value)
        {
            CallAction("optionInstallAddonUpdates", new Parameters
            {
                { "Boolean", value }
            });
        }

        /// <summary>
        /// Sets whether ZAP should automatically install updates to scanner rules.
        /// </summary>
        /// <param name="value">True if ZAP should automatically install updates to scanner rules.</param>
        public void SetOptionInstallScannerRules(bool value)
        {
            CallAction("optionInstallScannerRules", new Parameters
            {
                { "Boolean", value }
            });
        }

        /// <summary>
        /// Sets whether ZAP should report alpha releases of addons as updates.
        /// </summary>
        /// <param name="value">True if ZAP should report alpha releases of addons as updates.</param>
        public void SetOptionReportAlphaAddons(bool value)
        {
            CallAction("optionReportAlphaAddons", new Parameters
            {
                { "Boolean", value }
            });
        }

        /// <summary>
        /// Sets whether ZAP should report beta releases of addons as updates.
        /// </summary>
        /// <param name="value">True if ZAP should report beta releases of addons as updates.</param>
        public void SetOptionReportBetaAddons(bool value)
        {
            CallAction("optionReportBetaAddons", new Parameters
            {
                { "Boolean", value }
            });
        }

        /// <summary>
        /// Sets whether ZAP should report stable releases of addons as updates.
        /// </summary>
        /// <param name="value">True if ZAP should report stable releases of addons as updates.</param>
        public void SetOptionReportReleaseAddons(bool value)
        {
            CallAction("optionReportReleaseAddons", new Parameters
            {
                { "Boolean", value }
            });
        }

        #endregion
    }
}
