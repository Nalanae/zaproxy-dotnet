namespace ZAProxy.Schema
{
    /// <summary>
    /// Describes the configuration for the http authorization session management method.
    /// </summary>
    public class HttpAuthSessionManagementMethod : SpecializedSessionManagementMethodBase
    {
        /// <summary>
        /// Initiates a new instance of the <see cref="HttpAuthSessionManagementMethod"/> class.
        /// </summary>
        public HttpAuthSessionManagementMethod()
            : this(new SessionManagementMethod { MethodName = SessionManagementMethodNameConstants.HttpAuth })
        { }

        /// <summary>
        /// Initiates a new instance of the <see cref="HttpAuthSessionManagementMethod"/> class.
        /// </summary>
        /// <param name="sessionManagementMethod">The generic session management method.</param>
        public HttpAuthSessionManagementMethod(SessionManagementMethod sessionManagementMethod)
            : base(sessionManagementMethod, SessionManagementMethodNameConstants.HttpAuth)
        { }
    }
}
