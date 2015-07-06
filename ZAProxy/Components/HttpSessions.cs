using System.Collections.Generic;
using ZAProxy.Infrastructure;
using ZAProxy.Schema;

namespace ZAProxy.Components
{
    /// <summary>
    /// Component to manage the sessions of sites in ZAP.
    /// </summary>
    public class HttpSessions : ComponentBase
    {
        /// <summary>
        /// Initiates a new instance of the <see cref="HttpSessions"/> class.
        /// </summary>
        /// <param name="zapProcess">The ZAP process to connect to.</param>
        public HttpSessions(IZapProcess zapProcess)
            : this(null, zapProcess)
        { }

        /// <summary>
        /// Initiates a new instance of the <see cref="HttpSessions"/> class with a specific HTTP client implementation.
        /// </summary>
        /// <param name="httpClient">The HTTP client implementation.</param>
        /// <param name="zapProcess">The ZAP process to connect to.</param>
        public HttpSessions(IHttpClient httpClient, IZapProcess zapProcess)
            : base(httpClient, zapProcess, "httpSessions")
        { }

        #region Views

        /// <summary>
        /// Gets the name of the active session for a specific site.
        /// </summary>
        /// <param name="site">The site.</param>
        /// <returns>Name of the active session for specified site.</returns>
        public string GetActiveSession(string site)
        {
            return CallView<string>("activeSession", "active_session", new Parameters
            {
                { "site", site }
            });
        }

        /// <summary>
        /// Gets all the token names for a specific site that make up sessions.
        /// </summary>
        /// <param name="site">The site.</param>
        /// <returns>All token names for specified site that make up sessions.</returns>
        public IEnumerable<string> GetSessionTokens(string site)
        {
            return CallView<IEnumerable<string>>("sessionTokens", "session_tokens", new Parameters
            {
                { "site", site }
            });
        }

        /// <summary>
        /// Gets all the sessions for a specific site. Optionally filtered by session name.
        /// </summary>
        /// <param name="site">The site.</param>
        /// <param name="name">Optional name of the session.</param>
        /// <returns>All sessions for a specific site.</returns>
        public IEnumerable<HttpSession> GetSessions(string site, string name = null)
        {
            return CallView<IEnumerable<HttpSession>>("sessions", "sessions", new Parameters
            {
                { "site", site },
                { "session", name }
            });
        }

        #endregion

        #region Actions

        /// <summary>
        /// Adds a new session token name.
        /// </summary>
        /// <param name="site">The site.</param>
        /// <param name="name">The name of the session token.</param>
        public void AddSessionToken(string site, string name)
        {
            CallAction("addSessionToken", new Parameters
            {
                { "site", site },
                { "sessionToken", name }
            });
        }

        /// <summary>
        /// Creates a new empty session and sets it active.
        /// </summary>
        /// <param name="site">The site.</param>
        /// <param name="name">Optional name of the session. If no value is supplied ZAP will create a unique name.</param>
        public void CreateEmptySession(string site, string name = null)
        {
            CallAction("createEmptySession", new Parameters
            {
                { "site", site },
                { "session", name }
            });
        }

        /// <summary>
        /// Removes a session.
        /// </summary>
        /// <param name="site">The site.</param>
        /// <param name="name">The name of the session.</param>
        public void RemoveSession(string site, string name)
        {
            CallAction("removeSession", new Parameters
            {
                { "site", site },
                { "session", name }
            });
        }

        /// <summary>
        /// Removes a session token.
        /// </summary>
        /// <param name="site">The site.</param>
        /// <param name="name">The name of the session token.</param>
        public void RemoveSessionToken(string site, string name)
        {
            CallAction("removeSessionToken", new Parameters
            {
                { "site", site },
                { "sessionToken", name }
            });
        }

        /// <summary>
        /// Renames a session.
        /// </summary>
        /// <param name="site">The site.</param>
        /// <param name="oldName">The old name of the session.</param>
        /// <param name="newName">The new name of the session.</param>
        public void RenameSession(string site, string oldName, string newName)
        {
            CallAction("renameSession", new Parameters
            {
                { "site", site },
                { "oldSessionName", oldName },
                { "newSessionName", newName }
            });
        }

        /// <summary>
        /// Sets the active session.
        /// </summary>
        /// <param name="site">The site.</param>
        /// <param name="name">The name of the session.</param>
        public void SetActiveSession(string site, string name)
        {
            CallAction("setActiveSession", new Parameters
            {
                { "site", site },
                { "session", name }
            });
        }

        /// <summary>
        /// Sets a session token value for a specific session.
        /// </summary>
        /// <param name="site">The site.</param>
        /// <param name="sessionName">The name of the session.</param>
        /// <param name="sessionTokenName">The name of the session token.</param>
        /// <param name="value">The value of the session token.</param>
        public void SetSessionTokenValue(string site, string sessionName, string sessionTokenName, string value)
        {
            CallAction("setSessionTokenValue", new Parameters
            {
                { "site", site },
                { "session", sessionName },
                { "sessionToken", sessionTokenName },
                { "tokenValue", value }
            });
        }

        /// <summary>
        /// Unsets the current active session.
        /// </summary>
        /// <param name="site">The site.</param>
        public void UnsetActiveSession(string site)
        {
            CallAction("unsetActiveSession", new Parameters
            {
                { "site", site }
            });
        }

        #endregion
    }
}
