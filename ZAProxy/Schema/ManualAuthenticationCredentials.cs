namespace ZAProxy.Schema
{
    /// <summary>
    /// Describes the configuration for manual authentication credentials.
    /// </summary>
    public class ManualAuthenticationCredentials : SpecializedAuthenticationCredentialsBase
    {
        /// <summary>
        /// Initiates a new instance of the <see cref="ManualAuthenticationCredentials"/> class.
        /// </summary>
        public ManualAuthenticationCredentials()
            : this(new AuthenticationCredentials { Type = AuthenticationCredentialsTypeConstants.Manual })
        { }

        /// <summary>
        /// Initiates a new instance of the <see cref="ManualAuthenticationCredentials"/> class.
        /// </summary>
        /// <param name="authenticationCredentials">The generic authentication credentials.</param>
        public ManualAuthenticationCredentials(AuthenticationCredentials authenticationCredentials)
            : base(authenticationCredentials, AuthenticationMethodNameConstants.Http)
        { }

        /// <summary>
        /// Gets or sets the session name.
        /// </summary>
        public string Host
        {
            get { return BaseAuthenticationCredential.Parameters[AuthenticationCredentialsParameterConstants.SessionName]; }
            set { BaseAuthenticationCredential.Parameters[AuthenticationCredentialsParameterConstants.SessionName] = value; }
        }
    }
}
