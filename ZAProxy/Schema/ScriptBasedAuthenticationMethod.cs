namespace ZAProxy.Schema
{
    /// <summary>
    /// Describes the configuration for the script-based authentication method.
    /// </summary>
    public class ScriptBasedAuthenticationMethod : SpecializedAuthenticationMethodBase
    {
        /// <summary>
        /// Initiates a new instance of the <see cref="ScriptBasedAuthenticationMethod"/> class.
        /// </summary>
        public ScriptBasedAuthenticationMethod()
            : this(new AuthenticationMethod { MethodName = AuthenticationMethodNameConstants.ScriptBased })
        { }

        /// <summary>
        /// Initiates a new instance of the <see cref="ScriptBasedAuthenticationMethod"/> class.
        /// </summary>
        /// <param name="authenticationMethod">The generic authentication method.</param>
        public ScriptBasedAuthenticationMethod(AuthenticationMethod authenticationMethod)
            : base(authenticationMethod, AuthenticationMethodNameConstants.ScriptBased)
        { }

        /// <summary>
        /// Gets or sets the script name.
        /// </summary>
        public string ScriptName
        {
            get { return BaseAuthenticationMethod.Parameters[AuthenticationMethodParameterConstants.ScriptName]; }
            set { BaseAuthenticationMethod.Parameters[AuthenticationMethodParameterConstants.ScriptName] = value; }
        }
    }
}
