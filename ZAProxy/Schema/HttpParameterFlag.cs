namespace ZAProxy.Schema
{
    /// <summary>
    /// Contains constants of all well-known flags that can be assigned to http parameters.
    /// </summary>
    public static class HttpParameterFlag
    {
        /// <summary>
        /// Parameter is part of the anti CSRF protection.
        /// </summary>
        public const string AntiCsrf = "anticsrf";

        /// <summary>
        /// Parameter is part of the session state.
        /// </summary>
        public const string Session = "session";

        /// <summary>
        /// Parameter is a structural part of the system.
        /// </summary>
        public const string Structural = "structural";
    }
}
