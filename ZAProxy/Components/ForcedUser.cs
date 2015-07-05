using ZAProxy.Infrastructure;

namespace ZAProxy.Components
{
    /// <summary>
    /// Component for managing forced user sessions.
    /// </summary>
    public class ForcedUser : ComponentBase
    {
        /// <summary>
        /// Initiates a new instance of the <see cref="ForcedUser"/> class.
        /// </summary>
        /// <param name="zapProcess">The ZAP process to connect to.</param>
        public ForcedUser(IZapProcess zapProcess)
            : this(null, zapProcess)
        { }

        /// <summary>
        /// Initiates a new instance of the <see cref="ForcedUser"/> class with a specific HTTP client implementation.
        /// </summary>
        /// <param name="httpClient">The HTTP client implementation.</param>
        /// <param name="zapProcess">The ZAP process to connect to.</param>
        public ForcedUser(IHttpClient httpClient, IZapProcess zapProcess)
            : base(httpClient, zapProcess, "forcedUser")
        { }

        #region Views
        
        /// <summary>
        /// Gets the ID of the forced user for a specific context.
        /// </summary>
        /// <param name="contextId">The ID of the context.</param>
        /// <returns>The ID of the forced user for the specified context.</returns>
        public int GetForcedUser(int contextId)
        {
            return CallView<int>("getForcedUser", "forcedUserId", new Parameters
            {
                { "contextId", contextId }
            });
        }

        /// <summary>
        /// Gets whether forced user mode is enabled.
        /// </summary>
        /// <returns>True if forced user mode is enabled.</returns>
        public bool IsForcedUserModeEnabled()
        {
            return CallView<bool>("isForcedUserModeEnabled", "forcedModeEnabled");
        }

        #endregion

        #region Actions

        /// <summary>
        /// Sets the forced user for a specific context.
        /// </summary>
        /// <param name="contextId">The ID of the context.</param>
        /// <param name="userId">The ID of the user.</param>
        public void SetForcedUser(int contextId, int userId)
        {
            CallAction("setForcedUser", new Parameters
            {
                { "contextId", contextId },
                { "userId", userId }
            });
        }

        /// <summary>
        /// Sets whether forced user mode is enabled.
        /// </summary>
        /// <param name="enabled">True if forced user mode should be enabled.</param>
        public void SetForcedUserModeEnabled(bool enabled)
        {
            CallAction("setForcedUserModeEnabled", new Parameters
            {
                { "boolean", enabled }
            });
        }

        #endregion
    }
}
