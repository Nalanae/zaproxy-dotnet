namespace ZAProxy.Schema
{
    /// <summary>
    /// Describes the configuration for the manual authentication method.
    /// </summary>
    public class ManualAuthenticationMethod : SpecializedAuthenticationMethodBase
    {
        /// <summary>
        /// Initiates a new instance of the <see cref="ManualAuthenticationMethod"/> class.
        /// </summary>
        public ManualAuthenticationMethod()
            : this(new AuthenticationMethod { MethodName = AuthenticationMethodNameConstants.Manual })
        { }

        /// <summary>
        /// Initiates a new instance of the <see cref="ManualAuthenticationMethod"/> class.
        /// </summary>
        /// <param name="authenticationMethod">The generic authentication method.</param>
        public ManualAuthenticationMethod(AuthenticationMethod authenticationMethod)
            : base(authenticationMethod, AuthenticationMethodNameConstants.Manual)
        { }
    }
}
