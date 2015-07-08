using Newtonsoft.Json.Linq;
using ZAProxy.Infrastructure;
using ZAProxy.Schema;

namespace ZAProxy.Components
{
    /// <summary>
    /// Component to manage the authorization for contexts.
    /// </summary>
    public class AuthorizationComponent : ComponentBase
    {
        /// <summary>
        /// Initiates a new instance of the <see cref="AuthorizationComponent"/> class.
        /// </summary>
        /// <param name="zapProcess">The ZAP process to connect to.</param>
        public AuthorizationComponent(IZapProcess zapProcess)
            : this(null, zapProcess)
        { }

        /// <summary>
        /// Initiates a new instance of the <see cref="AuthorizationComponent"/> class with a specific HTTP client implementation.
        /// </summary>
        /// <param name="httpClient">The HTTP client implementation.</param>
        /// <param name="zapProcess">The ZAP process to connect to.</param>
        public AuthorizationComponent(IHttpClient httpClient, IZapProcess zapProcess)
            : base(httpClient, zapProcess, "authorization")
        { }

        #region Views

        /// <summary>
        /// Gets the authorization detection method configuration for a specific context.
        /// </summary>
        /// <param name="contextId">The ID of the context.</param>
        /// <returns>Authorization detection method configuration for specified context.</returns>
        public BasicAuthorizationDetectionMethod GetAuthorizationDetectionMethod(int contextId)
        {
            var authorizationDetectionMethod = CallView<JObject>("getAuthorizationDetectionMethod", null, new Parameters
            {
                { "contextId", contextId }
            });

            if (!authorizationDetectionMethod.Value<string>("methodType").EqualsOrdinalIgnoreCase("basic"))
                throw new ZapException(Resources.UnsupportedAuthorizationDetectionMethod);

            return authorizationDetectionMethod.ToObject<BasicAuthorizationDetectionMethod>();
        }

        #endregion

        #region Actions

        /// <summary>
        /// Sets the basic authorization detection method configuration for a specific context.
        /// </summary>
        /// <param name="contextId">The ID of the context.</param>
        /// <param name="basicAuthorizationDetectionMethod">The basic authorization detection method configuration.</param>
        public void SetBasicAuthorizationDetectionMethod(int contextId, BasicAuthorizationDetectionMethod basicAuthorizationDetectionMethod)
        {
            CallAction("setBasicAuthorizationDetectionMethod", new Parameters
            {
                { "contextId", contextId },
                { "headerRegex", basicAuthorizationDetectionMethod.HeaderRegex },
                { "bodyRegex", basicAuthorizationDetectionMethod.BodyRegex },
                { "statusCode", basicAuthorizationDetectionMethod.StatusCode },
                { "logicalOperator", basicAuthorizationDetectionMethod.LogicalOperator }
            });
        }

        #endregion
    }
}
