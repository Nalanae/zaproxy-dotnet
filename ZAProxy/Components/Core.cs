using HttpArchive;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using ZAProxy.Infrastructure;
using ZAProxy.Schema;
using System;

namespace ZAProxy.Components
{
    /// <summary>
    /// Component that contains the core calls of ZAP.
    /// </summary>
    public class Core : ComponentBase
    {
        /// <summary>
        /// Initiates a new instance of the <see cref="Core"/> class.
        /// </summary>
        /// <param name="zapProcess">The ZAP process to connect to.</param>
        public Core(IZapProcess zapProcess)
            : this(null, zapProcess)
        { }

        /// <summary>
        /// Initiates a new instance of the <see cref="Core"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client implementation.</param>
        /// <param name="zapProcess">The ZAP process to connect to.</param>
        public Core(IHttpClient httpClient, IZapProcess zapProcess)
            : base(httpClient, zapProcess, "core")
        { }

        #region Views

        /// <summary>
        /// Gets a specific alert.
        /// </summary>
        /// <param name="id">The ID of the alert.</param>
        /// <returns>The alert.</returns>
        public Alert GetAlert(int id)
        {
            return CallView<Alert>("alert", "alert", new Parameters
            {
                { "id", id }
            });
        }

        /// <summary>
        /// Gets all alerts. Optionally filtered by a base url and/or paged.
        /// </summary>
        /// <param name="baseUrl">Optional base url of the requested alerts.</param>
        /// <param name="start">Optional starting index (1-based).</param>
        /// <param name="count">Optional maximum amount of alerts to retrieve.</param>
        /// <returns>All the alerts.</returns>
        public IEnumerable<Alert> GetAlerts(string baseUrl = null, int? start = null, int? count = null)
        {
            return CallView<IEnumerable<Alert>>("alerts", "alerts", new Parameters
            {
                { "baseurl", baseUrl },
                { "start", start },
                { "count", count }
            });
        }

        /// <summary>
        /// Gets all regex patterns for urls excluded from the proxy.
        /// </summary>
        /// <returns>Regex patterns excluded from the proxy.</returns>
        public IEnumerable<string> GetExcludedFromProxy()
        {
            return CallView<IEnumerable<string>>("excludedFromProxy", "excludedFromProxy");
        }

        /// <summary>
        /// Gets the home directory of ZAP.
        /// </summary>
        /// <returns>Home directory of ZAP.</returns>
        public string GetHomeDirectory()
        {
            return CallView<string>("homeDirectory", "homeDirectory");
        }

        /// <summary>
        /// Gets all the hosts known by ZAP.
        /// </summary>
        /// <returns>All the hoststs known by ZAP.</returns>
        public IEnumerable<string> GetHosts()
        {
            return CallView<IEnumerable<string>>("hosts", "hosts");
        }

        /// <summary>
        /// Gets a specific message.
        /// </summary>
        /// <param name="id">The ID of the message.</param>
        /// <returns>The message.</returns>
        public Message GetMessage(int id)
        {
            return CallView<Message>("message", "message", new Parameters
            {
                { "id", id }
            });
        }

        /// <summary>
        /// Gets all messages. Optionally filtered by a base url and/or paged.
        /// </summary>
        /// <param name="baseUrl">Optional base url of the requested messages.</param>
        /// <param name="start">Optional starting index (1-based).</param>
        /// <param name="count">Optional maximum amount of messages to retrieve.</param>
        /// <returns>All the messages.</returns>
        public IEnumerable<Message> GetMessages(string baseUrl = null, int? start = null, int? count = null)
        {
            return CallView<IEnumerable<Message>>("messages", "messages", new Parameters
            {
                { "baseurl", baseUrl },
                { "start", start },
                { "count", count }
            });
        }

        /// <summary>
        /// Gets the total number of alerts. Optionally filtered by a base url.
        /// </summary>
        /// <param name="baseUrl">Optional base url of the alert.</param>
        /// <returns>Total number of alerts.</returns>
        public int GetNumberOfAlerts(string baseUrl = null)
        {
            return CallView<int>("numberOfAlerts", "numberOfAlerts", new Parameters
            {
                { "baseurl", baseUrl }
            });
        }

        /// <summary>
        /// Gets the total number of messages. Optionally filtered by a base url.
        /// </summary>
        /// <param name="baseUrl">Optional base url of the message.</param>
        /// <returns>Total number of messages.</returns>
        public int GetNumberOfMessages(string baseUrl = null)
        {
            return CallView<int>("numberOfMessages", "numberOfMessages", new Parameters
            {
                { "baseurl", baseUrl }
            });
        }

        /// <summary>
        /// Gets the HTTP state of ZAP.
        /// </summary>
        /// <remarks>
        /// This method is mainly for internal use by ZAP.
        /// </remarks>
        /// <returns>HTTP state of ZAP.</returns>
        public string GetOptionHttpState()
        {
            return CallView<string>("optionHttpState", "HttpState");
        }

        /// <summary>
        /// Gets whether HTTP state is enabled.
        /// </summary>
        /// <returns>True if HTTP state is enabled.</returns>
        public bool GetOptionHttpStateEnabled()
        {
            return CallView<dynamic>("optionHttpStateEnabled", "HttpStateEnabled");
        }

        /// <summary>
        /// Gets the hostname of the proxy ZAP connects to.
        /// </summary>
        /// <returns>Hostname of the proxy ZAP connects to.</returns>
        public string GetOptionProxyChainName()
        {
            return CallView<string>("optionProxyChainName", "ProxyChainName");
        }

        /// <summary>
        /// Gets the password of the proxy ZAP connects to.
        /// </summary>
        /// <returns>Password of the proxy ZAP connects to.</returns>
        public string GetOptionProxyChainPassword()
        {
            return CallView<string>("optionProxyChainPassword", "ProxyChainPassword");
        }

        /// <summary>
        /// Gets the port of the proxy ZAP connects to.
        /// </summary>
        /// <returns>Port of the proxy ZAP connects to.</returns>
        public int GetOptionProxyChainPort()
        {
            return CallView<int>("optionProxyChainPort", "ProxyChainPort");
        }

        /// <summary>
        /// Gets whether ZAP prompts the user for proxy settings on start-up.
        /// </summary>
        /// <returns>True if ZAP prompts the user for proxy settings on start-up.</returns>
        public bool GetOptionProxyChainPrompt()
        {
            return CallView<bool>("optionProxyChainPrompt", "ProxyChainPrompt");
        }

        /// <summary>
        /// Gets the user's realm of the proxy ZAP connects to.
        /// </summary>
        /// <returns>User's realm of the proxy ZAP connects to.</returns>
        public string GetOptionProxyChainRealm()
        {
            return CallView<string>("optionProxyChainRealm", "ProxyChainRealm");
        }

        /// <summary>
        /// Gets the excluded domains (semicolon delimited) that ZAP will not route through the proxy.
        /// </summary>
        /// <remarks>
        /// Replaced by <see cref="GetOptionProxyExcludedDomains"/>.
        /// </remarks>
        /// <returns>Excluded domains that ZAP will not route through the proxy.</returns>
        [Obsolete("Replaced by " + nameof(GetOptionProxyExcludedDomains) + ".")]
        public string GetOptionProxyChainSkipName()
        {
            return CallView<string>("optionProxyChainSkipName", "ProxyChainSkipName");
        }

        /// <summary>
        /// Gets the user name of the proxy ZAP connects to.
        /// </summary>
        /// <returns>User name of the proxy ZAP connects to.</returns>
        public string GetOptionProxyChainUserName()
        {
            return CallView<string>("optionProxyChainUserName", "ProxyChainUserName");
        }

        /// <summary>
        /// Gets all regex patterns for domains excluded from the proxy.
        /// </summary>
        /// <returns>Regex patterns for domains excluded from the proxy.</returns>
        public IEnumerable<string> GetOptionProxyExcludedDomains()
        {
            return CallView<IEnumerable<string>>("optionProxyExcludedDomains", "ProxyExcludedDomains");
        }

        /// <summary>
        /// Gets whether proxy will honor the exclusion of domains.
        /// </summary>
        /// <returns>True if the proxy will honor the exclusion of domains.</returns>
        public IEnumerable<string> GetOptionProxyExcludedDomainsEnabled()
        {
            return CallView<IEnumerable<string>>("optionProxyExcludedDomainsEnabled", "ProxyExcludedDomainsEnabled");
        }

        /// <summary>
        /// Gets whether the cookies are set on a single "Cookie" header or multiple when sending an HTTP request to the server.
        /// </summary>
        /// <returns>True if only a single "Cookie" header is used.</returns>
        public bool GetOptionSingleCookieRequestHeader()
        {
            return CallView<bool>("optionSingleCookieRequestHeader", "SingleCookieRequestHeader");
        }

        /// <summary>
        /// Gets the timeout (in seconds) after which ZAP will drop an HTTP request.
        /// </summary>
        /// <returns>Timeout after which ZAP will drop an HTTP request.</returns>
        public int GetOptionTimeoutInSecs()
        {
            return CallView<int>("optionTimeoutInSecs", "TimeoutInSecs");
        }

        /// <summary>
        /// Gets whether ZAP will connect to another proxy.
        /// </summary>
        /// <returns>True if ZAP will connect to another proxy.</returns>
        public bool GetOptionUseProxyChain()
        {
            return CallView<bool>("optionUseProxyChain", "UseProxyChain");
        }

        /// <summary>
        /// Gets whether ZAP uses authentication for it's proxy chain.
        /// </summary>
        /// <returns>True if ZAP uses authentication for it's proxy chain.</returns>
        public bool GetOptionUseProxyChainAuth()
        {
            return CallView<bool>("optionUseProxyChainAuth", "UseProxyChainAuth");
        }

        /// <summary>
        /// Get all sites known in the session.
        /// </summary>
        /// <returns>All the sites.</returns>
        public IEnumerable<string> GetSites()
        {
            return CallView<IEnumerable<string>>("sites", "sites");
        }

        /// <summary>
        /// Gets all the urls known in the session.
        /// </summary>
        /// <returns>All the urls.</returns>
        public IEnumerable<string> GetUrls()
        {
            return CallView<IEnumerable<string>>("urls", "urls");
        }

        /// <summary>
        /// Gets the version of ZAP.
        /// </summary>
        /// <returns>Version of ZAP.</returns>
        public string GetVersion()
        {
            return CallView<string>("version", "version");
        }

        #endregion

        #region Actions

        /// <summary>
        /// Clears all the excludes from the proxy.
        /// </summary>
        public void ClearExcludedFromProxy()
        {
            CallAction("clearExcludedFromProxy");
        }

        /// <summary>
        /// Deletes all the alerts.
        /// </summary>
        public void DeleteAllAlerts()
        {
            CallAction("deleteAllAlerts");
        }

        /// <summary>
        /// Excludes the regex pattern from the proxy.
        /// </summary>
        /// <param name="regex">The regex pattern to exclude from the proxy.</param>
        public void ExcludeFromProxy(string regex)
        {
            CallAction("excludeFromProxy", new Parameters
            {
                { "regex", regex }
            });
        }

        /// <summary>
        /// Generates a new root certificate authority.
        /// </summary>
        public void GenerateRootCA()
        {
            CallAction("generateRootCA");
        }

        /// <summary>
        /// Loads a new session from the given path.
        /// </summary>
        /// <param name="path">The path to the session file. Must be absolute.</param>
        public void LoadSession(string path)
        {
            CallAction("loadSession", new Parameters
            {
                { "name", path }
            });
        }

        /// <summary>
        /// Creates a new session. Optionally saves it on a specified path.
        /// </summary>
        /// <param name="path">Optional path to save the session file. Must be absolute.</param>
        /// <param name="overwrite">Optional choice to overwrite an existing file. Default is true.</param>
        public void NewSession(string path = null, bool overwrite = true)
        {
            CallAction("newSession", new Parameters
            {
                { "name", path },
                { "overwrite", overwrite }
            });
        }

        /// <summary>
        /// Saves the session to the specified path.
        /// </summary>
        /// <param name="path">The path to save the session to. Must be absolute.</param>
        /// <param name="overwrite">Optional choice to overwrite an existing file. Default is true.</param>
        public void SaveSession(string path, bool overwrite = true)
        {
            CallAction("saveSession", new Parameters
            {
                { "name", path },
                { "overwrite", overwrite }
            });
        }

        /// <summary>
        /// Sends a self-constructed HTTP request through the ZAP proxy and returns the obtained result messages.
        /// </summary>
        /// <param name="httpRequest">The HTTP request.</param>
        /// <param name="followRedirects">Optional choice whether ZAP should follow redirects. Default is false.</param>
        /// <returns>Obtained result messages.</returns>
        public IEnumerable<Message> SendRequest(string httpRequest, bool followRedirects = false)
        {
            return CallAction<IEnumerable<Message>>("sendRequest", "sendRequest", new Parameters
            {
                { "request", httpRequest },
                { "followRedirects", followRedirects }
            });
        }

        /// <summary>
        /// Sets the home directory of ZAP.
        /// </summary>
        /// <param name="value">The home directory of ZAP.</param>
        public void SetHomeDirectory(string value)
        {
            CallAction("setHomeDirectory", new Parameters
            {
                { "dir", value }
            });
        }

        /// <summary>
        /// Sets whether HTTP state is enabled.
        /// </summary>
        /// <param name="value">True if HTTP state should be enabled.</param>
        public void SetOptionHttpStateEnabled(bool value)
        {
            CallAction("setOptionHttpStateEnabled", new Parameters
            {
                { "Boolean", value }
            });
        }

        /// <summary>
        /// Sets the hostname of the proxy ZAP should connect to.
        /// </summary>
        /// <param name="value">Hostname of the proxy ZAP should connect to.</param>
        public void SetOptionProxyChainName(string value)
        {
            CallAction("setOptionProxyChainName", new Parameters
            {
                { "String", value }
            });
        }

        /// <summary>
        /// Sets the password of the proxy ZAP should connect to.
        /// </summary>
        /// <param name="value">Password of the proxy ZAP should connect to.</param>
        public void SetOptionProxyChainPassword(string value)
        {
            CallAction("setOptionProxyChainPassword", new Parameters
            {
                { "String", value }
            });
        }

        /// <summary>
        /// Sets the port of the proxy ZAP should connect to.
        /// </summary>
        /// <param name="value">Port of the proxy ZAP should connect to.</param>
        public void SetOptionProxyChainPort(int value)
        {
            CallAction("setOptionProxyChainPort", new Parameters
            {
                { "Integer", value }
            });
        }

        /// <summary>
        /// Sets whether ZAP should prompt the user for proxy settings on start-up.
        /// </summary>
        /// <param name="value">True if ZAP should prompt the user for proxy settings on start-up.</param>
        public void SetOptionProxyChainPrompt(bool value)
        {
            CallAction("setOptionProxyChainPrompt", new Parameters
            {
                { "Boolean", value }
            });
        }

        /// <summary>
        /// Sets the user's realm of the proxy ZAP should connect to.
        /// </summary>
        /// <param name="value">User's realm of the proxy ZAP should connect to.</param>
        public void SetOptionProxyChainRealm(string value)
        {
            CallAction("setOptionProxyChainRealm", new Parameters
            {
                { "String", value }
            });
        }

        /// <summary>
        /// Sets the excluded domains (semicolon delimited) that ZAP should not route through the proxy.
        /// </summary>
        /// <remarks>
        /// Replaced by <see cref="ExcludeFromProxy(string)"/> and <see cref="ClearExcludedFromProxy"/>.
        /// </remarks>
        /// <param name="value">Excluded domains that ZAP should not route through the proxy.</param>
        [Obsolete("Replaced by " + nameof(ExcludeFromProxy) + " and " + nameof(ExcludeFromProxy) + ".")]
        public void SetOptionProxyChainSkipName(string value)
        {
            CallAction("setOptionProxyChainSkipName", new Parameters
            {
                { "String", value }
            });
        }

        /// <summary>
        /// Sets the user name of the proxy ZAP should connect to.
        /// </summary>
        /// <param name="value">User name of the proxy ZAP should connect to.</param>
        public void SetOptionProxyChainUserName(string value)
        {
            CallAction("setOptionProxyChainUserName", new Parameters
            {
                { "String", value }
            });
        }

        /// <summary>
        /// Sets whether the cookies should be set on a single "Cookie" header or multiple when sending an HTTP request to the server.
        /// </summary>
        /// <param name="value">True if only a single "Cookie" header should be used.</param>
        public void SetOptionSingleCookieRequestHeader(bool value)
        {
            CallAction("setOptionSingleCookieRequestHeader", new Parameters
            {
                { "Boolean", value }
            });
        }

        /// <summary>
        /// Sets the timeout (in seconds) after which ZAP should drop an HTTP request.
        /// </summary>
        /// <param name="value">Timeout after which ZAP should drop an HTTP request.</param>
        public void SetOptionTimeoutInSecs(int value)
        {
            CallAction("setOptionTimeoutInSecs", new Parameters
            {
                { "Integer", value }
            });
        }

        /// <summary>
        /// Sets whether ZAP should connect to another proxy.
        /// </summary>
        /// <param name="value">True if ZAP should connect to another proxy.</param>
        public void SetOptionUseProxyChain(bool value)
        {
            CallAction("setOptionUseProxyChain", new Parameters
            {
                { "Boolean", value }
            });
        }

        /// <summary>
        /// Sets whether ZAP should use authentication for it's proxy chain.
        /// </summary>
        /// <param name="value">True if ZAP should use authentication for it's proxy chain.</param>
        public void SetOptionUseProxyChainAuth(bool value)
        {
            CallAction("setOptionUseProxyChainAuth", new Parameters
            {
                { "Boolean", value }
            });
        }

        /// <summary>
        /// Shuts down ZAP.
        /// </summary>
        public void Shutdown()
        {
            CallAction("shutdown");
        }
        
        /// <summary>
        /// Snapshots the current session in ZAP.
        /// </summary>
        public void SnapshotSession()
        {
            CallAction("snapshotSession");
        }

        #endregion

        #region Others

        /// <summary>
        /// Gets an HTML report of the current session.
        /// </summary>
        /// <returns>HTML report of the current session.</returns>
        public string GetHtmlReport()
        {
            return CallOther("htmlreport");
        }

        /// <summary>
        /// Gets a specific message in the <see cref="Har"/> format.
        /// </summary>
        /// <param name="id">The ID of the message.</param>
        /// <returns>Message in <see cref="Har"/> format.</returns>
        public Har GetMessageHar(int id)
        {
            var result = CallOther("messageHar", new Parameters
            {
                { "id", id }
            });
            return Har.Deserialize(result);
        }

        /// <summary>
        /// Gets all messages in the <see cref="Har"/> format. Optionally filtered by a base url and/or paged.
        /// </summary>
        /// <param name="baseUrl">Optional base url of the requested messages.</param>
        /// <param name="start">Optional starting index (1-based).</param>
        /// <param name="count">Optional maximum amount of messages to retrieve.</param>
        /// <returns>All the nessages in <see cref="Har"/> format.</returns>
        public Har GetMessagesHar(string baseUrl = null, int? start = null, int? count = null)
        {
            var result = CallOther("messagesHar", new Parameters
            {
                { "baseurl", baseUrl },
                { "start", start },
                { "count", count }
            });
            return Har.Deserialize(result);
        }

        /// <summary>
        /// Gets the ZAP proxy auto-config.
        /// </summary>
        /// <returns>ZAP proxy auto-config.</returns>
        public string GetProxyPac()
        {
            return CallOther("proxy.pac");
        }

        /// <summary>
        /// Gets the root certificate authority used by ZAP to resign HTTPS messages.
        /// </summary>
        /// <returns>Root certificate authority used by ZAP.</returns>
        public X509Certificate2 GetRootCertificate()
        {
            var result = CallOtherData("rootcert");
            return new X509Certificate2(result);
        }

        /// <summary>
        /// Sends an HTTP request defined in the <see cref="Har"/> format through the ZAP proxy and returns the obtained result messages.
        /// </summary>
        /// <param name="har">The HTTP request defined in the <see cref="Har"/> format.</param>
        /// <param name="followRedirects">Optional choice whether ZAP should follow redirects. Default is false.</param>
        /// <returns>Obtained result messages in <see cref="Har"/> format.</returns>
        public Har SendHarRequest(Har har, bool? followRedirects = false)
        {
            var result = CallOther("sendHarRequest", new Parameters
            {
                { "request", Har.Serialize(har) },
                { "followRedirects", followRedirects }
            });
            return Har.Deserialize(result);
        }

        /// <summary>
        /// Sets the proxy ZAP should connect to.
        /// </summary>
        /// <param name="host">Hostname of the proxy.</param>
        /// <param name="port">Port of the proxy.</param>
        public void SetProxy(string host, int port)
        {
            var proxyJson = new JObject(
                new JProperty("type", 1),
                new JProperty("http", new JObject(
                    new JProperty("host", host),
                    new JProperty("port", port))));

            var result = CallOther("setproxy", new Parameters
            {
                { "proxy", proxyJson }
            });
            if (result != "OK")
                throw new ZapException(Resources.SetProxyUnknownResult);
        }

        /// <summary>
        /// Gets an XML report of the current session.
        /// </summary>
        /// <returns>XML report of the current session.</returns>
        public string GetXmlReport()
        {
            return CallOther("xmlreport");
        }

        // TODO: Add deserialized XML report

        #endregion
    }
}
