using System.Collections.Generic;
using ZAProxy.Infrastructure;
using ZAProxy.Schema;

namespace ZAProxy.Components
{
    /// <summary>
    /// Component that manages session management for contexts.
    /// </summary>
    public class SessionManagementComponent : ComponentBase
    {
        /// <summary>
        /// Initiates a new instance of the <see cref="SessionManagementComponent"/> class.
        /// </summary>
        /// <param name="zapProcess">The ZAP process to connect to.</param>
        public SessionManagementComponent(IZapProcess zapProcess)
            : this(null, zapProcess)
        { }

        /// <summary>
        /// Initiates a new instance of the <see cref="SessionManagementComponent"/> class with a specific HTTP client implementation.
        /// </summary>
        /// <param name="httpClient">The HTTP client implementation.</param>
        /// <param name="zapProcess">The ZAP process to connect to.</param>
        public SessionManagementComponent(IHttpClient httpClient, IZapProcess zapProcess)
            : base(httpClient, zapProcess, "sessionManagement")
        { }

        #region Views

        /// <summary>
        /// Gets the session management method of a specific context.
        /// </summary>
        /// <param name="contextId">The ID of the context.</param>
        /// <returns>Session management method of specified context.</returns>
        public SessionManagementMethod GetSessionManagementMethod(int contextId)
        {
            return CallView<SessionManagementMethod>("getSessionManagementMethod", null, new Parameters
            {
                { "contextId", contextId }
            });
        }

        /// <summary>
        /// Gets all configuration parameters of a specific session management method.
        /// </summary>
        /// <param name="methodName">The name of the method.</param>
        /// <returns>Configuration parameters of specified session management method.</returns>
        public IEnumerable<ConfigurationParameter> GetSessionManagementMethodConfigParameters(string methodName)
        {
            return CallView<IEnumerable<ConfigurationParameter>>("getSessionManagementMethodConfigParams", "methodConfigParams", new Parameters
            {
                { "methodName", methodName }
            });
        }

        /// <summary>
        /// Gets the names of all the supported session management methods.
        /// </summary>
        /// <returns>Names of all the supported session management methods.</returns>
        public IEnumerable<string> GetSupportedSessionManagementMethods()
        {
            return CallView<IEnumerable<string>>("getSupportedSessionManagementMethods", "supportedMethods");
        }

        #endregion

        #region Actions

        /// <summary>
        /// Sets the session management method of a specific context.
        /// </summary>
        /// <param name="contextId">The ID of the context.</param>
        /// <param name="sessionManagementMethod">The session management method.</param>
        public void SetSessionManagementMethod(int contextId, SessionManagementMethod sessionManagementMethod)
        {
            var parameters = new Parameters
            {
                { "contextId", contextId },
                { "methodName", sessionManagementMethod.MethodName }
            };
            foreach (var parameter in sessionManagementMethod.Parameters)
                parameters.Add(parameter.Key, parameter.Value);

            CallAction("setSessionManagementMethod", parameters);
        }

        #endregion
    }
}
