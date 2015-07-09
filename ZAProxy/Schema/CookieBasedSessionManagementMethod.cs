namespace ZAProxy.Schema
{
    /// <summary>
    /// Describes the configuration for the cookie based session management method.
    /// </summary>
    public class CookieBasedSessionManagementMethod : SpecializedSessionManagementMethodBase
    {
        /// <summary>
        /// Initiates a new instance of the <see cref="CookieBasedSessionManagementMethod"/> class.
        /// </summary>
        public CookieBasedSessionManagementMethod()
            : this(new SessionManagementMethod { MethodName = SessionManagementMethodNameConstants.CookieBased })
        { }

        /// <summary>
        /// Initiates a new instance of the <see cref="CookieBasedSessionManagementMethod"/> class.
        /// </summary>
        /// <param name="sessionManagementMethod">The generic session management method.</param>
        public CookieBasedSessionManagementMethod(SessionManagementMethod sessionManagementMethod)
            : base(sessionManagementMethod, SessionManagementMethodNameConstants.CookieBased)
        { }
    }
}
