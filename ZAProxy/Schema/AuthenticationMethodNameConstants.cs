namespace ZAProxy.Schema
{
    /// <summary>
    /// Contains constants of all the well-known authentication method names.
    /// </summary>
    public static class AuthenticationMethodNameConstants
    {
        /// <summary>
        /// Method name of forms based authentication.
        /// </summary>
        public const string FormBased = "formBasedAuthentication";
        
        /// <summary>
        /// Method name of HTTP/NTLM authentication.
        /// </summary>
        public const string Http = "httpAuthentication";

        /// <summary>
        /// Method name of manual authentication.
        /// </summary>
        public const string Manual = "manualAuthentication";

        /// <summary>
        /// Method name of script-based authentication.
        /// </summary>
        public const string ScriptBased = "scriptBasedAuthentication";
    }
}
