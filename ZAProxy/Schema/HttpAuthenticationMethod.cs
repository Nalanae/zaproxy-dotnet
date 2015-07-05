namespace ZAProxy.Schema
{
    /// <summary>
    /// Describes the configuration for the HTTP/NTLM authentication method.
    /// </summary>
    public class HttpAuthenticationMethod : SpecializedAuthenticationMethodBase
    {
        /// <summary>
        /// Initiates a new instance of the <see cref="HttpAuthenticationMethod"/> class.
        /// </summary>
        public HttpAuthenticationMethod()
            : this(new AuthenticationMethod { MethodName = AuthenticationMethodNameConstants.Http })
        { }

        /// <summary>
        /// Initiates a new instance of the <see cref="HttpAuthenticationMethod"/> class.
        /// </summary>
        /// <param name="authenticationMethod">The generic authentication method.</param>
        public HttpAuthenticationMethod(AuthenticationMethod authenticationMethod)
            : base(authenticationMethod, AuthenticationMethodNameConstants.Http)
        { }

        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        public string Host
        {
            get { return BaseAuthenticationMethod.Parameters[AuthenticationMethodParameterConstants.Host]; }
            set { BaseAuthenticationMethod.Parameters[AuthenticationMethodParameterConstants.Host] = value; }
        }

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        public int Port
        {
            get { return int.Parse(BaseAuthenticationMethod.Parameters[AuthenticationMethodParameterConstants.Port]); }
            set { BaseAuthenticationMethod.Parameters[AuthenticationMethodParameterConstants.Port] = value.ToString(); }
        }

        /// <summary>
        /// Gets or sets the realm.
        /// </summary>
        public string Realm
        {
            get { return BaseAuthenticationMethod.Parameters[AuthenticationMethodParameterConstants.Realm]; }
            set { BaseAuthenticationMethod.Parameters[AuthenticationMethodParameterConstants.Realm] = value; }
        }
    }
}
