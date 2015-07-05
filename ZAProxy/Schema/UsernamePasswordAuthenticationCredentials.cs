namespace ZAProxy.Schema
{
    /// <summary>
    /// Describes the configuration for user name and password based authentication credentials.
    /// </summary>
    public class UserNamePasswordAuthenticationCredentials : SpecializedAuthenticationCredentialsBase
    {
        /// <summary>
        /// Initiates a new instance of the <see cref="UserNamePasswordAuthenticationCredentials"/> class.
        /// </summary>
        public UserNamePasswordAuthenticationCredentials()
            : this(new AuthenticationCredentials { Type = AuthenticationCredentialsTypeConstants.UserNamePassword })
        { }

        /// <summary>
        /// Initiates a new instance of the <see cref="UserNamePasswordAuthenticationCredentials"/> class.
        /// </summary>
        /// <param name="authenticationCredentials">The generic authentication credentials.</param>
        public UserNamePasswordAuthenticationCredentials(AuthenticationCredentials authenticationCredentials)
            : base(authenticationCredentials, AuthenticationMethodNameConstants.Http)
        { }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName
        {
            get { return BaseAuthenticationCredential.Parameters[AuthenticationCredentialsParameterConstants.UserName]; }
            set { BaseAuthenticationCredential.Parameters[AuthenticationCredentialsParameterConstants.UserName] = value; }
        }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password
        {
            get { return BaseAuthenticationCredential.Parameters[AuthenticationCredentialsParameterConstants.Password]; }
            set { BaseAuthenticationCredential.Parameters[AuthenticationCredentialsParameterConstants.Password] = value; }
        }
    }
}
