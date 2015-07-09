namespace ZAProxy.Schema
{
    /// <summary>
    /// Contains constants of all the well-known session management method names.
    /// </summary>
    public static class SessionManagementMethodNameConstants
    {
        /// <summary>
        /// Method name of cookie based session management.
        /// </summary>
        public const string CookieBased = "cookieBasedSessionManagement";

        /// <summary>
        /// Method name of http authentication session management.
        /// </summary>
        public const string HttpAuth = "httpAuthSessionManagement";
    }
}
