namespace ZAProxy.Schema
{
    /// <summary>
    /// Contains constants of all well-known application credential types.
    /// </summary>
    public static class AuthenticationCredentialsTypeConstants
    {
        /// <summary>
        /// Type name of user name and password based credentials.
        /// </summary>
        public const string UserNamePassword = "UsernamePasswordAuthenticationCredentials";

        /// <summary>
        /// Type name of manually created credentials.
        /// </summary>
        public const string Manual = "ManualAuthenticationCredentials";

        /// <summary>
        /// Type name of generic credentials.
        /// </summary>
        public const string Generic = "GenericAuthenticationCredentials";
    }
}
