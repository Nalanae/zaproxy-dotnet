using System.Collections.Generic;
using ZAProxy.Infrastructure;
using ZAProxy.Schema;

namespace ZAProxy.Components
{
    /// <summary>
    /// Component to manage the authentication for contexts.
    /// </summary>
    public class AuthenticationComponent : ComponentBase
    {
        /// <summary>
        /// Initiates a new instance of the <see cref="AuthenticationComponent"/> class.
        /// </summary>
        /// <param name="zapProcess">The ZAP process to connect to.</param>
        public AuthenticationComponent(IZapProcess zapProcess)
            : this(null, zapProcess)
        { }

        /// <summary>
        /// Initiates a new instance of the <see cref="AuthenticationComponent"/> class with a specific HTTP client implementation.
        /// </summary>
        /// <param name="httpClient">The HTTP client implementation.</param>
        /// <param name="zapProcess">The ZAP process to connect to.</param>
        public AuthenticationComponent(IHttpClient httpClient, IZapProcess zapProcess)
            : base(httpClient, zapProcess, "authentication")
        { }

        #region Views

        /// <summary>
        /// Gets the authentication method used by specified context.
        /// </summary>
        /// <param name="contextId">The ID of the context.</param>
        /// <returns>Authentication method used by specified content.</returns>
        public AuthenticationMethod GetAuthenticationMethod(int contextId)
        {
            // TODO: Fix dynamic type.
            return CallView<AuthenticationMethod>("getAuthenticationMethod", null, new Parameters
            {
                { "contextId", contextId }
            });
        }

        /// <summary>
        /// Gets the configuration parameters of the specified authentication mode.
        /// </summary>
        /// <param name="authenticationMethodName">The name of the authentication mode.</param>
        /// <returns>The configuration parameters of the authentication mode.</returns>
        public IEnumerable<AuthenticationConfigParameter> GetAuthenticationMethodConfigParameters(string authenticationMethodName)
        {
            return CallView<IEnumerable<AuthenticationConfigParameter>>("getAuthenticationMethodConfigParams", "methodConfigParams", new Parameters
            {
                { "authMethodName", authenticationMethodName }
            });
        }

        /// <summary>
        /// Gets the regex pattern to verify in the response if the user is logged in for a specific context.
        /// </summary>
        /// <param name="contextId">The ID of the context.</param>
        /// <returns>The regex pattern to verify if the user is logged in.</returns>
        public string GetLoggedInIndicator(int contextId)
        {
            return CallView<string>("getLoggedInIndicator", "logged_in_regex", new Parameters
            {
                { "contextId", contextId }
            });
        }

        /// <summary>
        /// Gets the regex pattern to verify in the response if the user is logged out for a specific context.
        /// </summary>
        /// <param name="contextId">The ID of the context.</param>
        /// <returns>The regex pattern to verify if the user is logged out.</returns>
        public string GetLoggedOutIndicator(int contextId)
        {
            return CallView<string>("getLoggedOutIndicator", "logged_out_regex", new Parameters
            {
                { "contextId", contextId }
            });
        }

        /// <summary>
        /// Gets the names of all the supported authentication methods.
        /// </summary>
        /// <returns>Names of all the supported authentication methods.</returns>
        public IEnumerable<string> GetSupportedAuthenticationMethods()
        {
            return CallView<IEnumerable<string>>("getSupportedAuthenticationMethods", "supportedMethods");
        }

        #endregion

        #region Actions

        /// <summary>
        /// Sets the authentication method of the specified context.
        /// </summary>
        /// <param name="contextId">The ID of the context.</param>
        /// <param name="authenticationMethod">The authentication method configuration.</param>
        public void SetAuthenticationMethod(int contextId, AuthenticationMethod authenticationMethod)
        {
            var parameters = new Parameters
            {
                { "contextId", contextId },
                { "authMethodName", authenticationMethod.MethodName }
            };
            foreach (var parameter in authenticationMethod.Parameters)
                parameters.Add(parameter.Key, parameter.Value);

            CallAction("setAuthenticationMethod", parameters);
        }

        /// <summary>
        /// Sets the regex pattern to verify in the response if the user is logged in for a specific context.
        /// </summary>
        /// <param name="contextId">The ID of the context.</param>
        /// <param name="indicatorRegex">The regex.</param>
        public void SetLoggedInIndicator(int contextId, string indicatorRegex)
        {
            CallAction("setLoggedInIndicator", new Parameters
            {
                { "contextId", contextId },
                { "loggedInIndicatorRegex", indicatorRegex }
            });
        }

        /// <summary>
        /// Sets the regex pattern to verify in the response if the user is logged out for a specific context.
        /// </summary>
        /// <param name="contextId">The ID of the context.</param>
        /// <param name="indicatorRegex">The regex.</param>
        public void SetLoggedOutIndicator(int contextId, string indicatorRegex)
        {
            CallAction("setLoggedOutIndicator", new Parameters
            {
                { "contextId", contextId },
                { "loggedOutIndicatorRegex", indicatorRegex }
            });
        }

        #endregion
    }
}
