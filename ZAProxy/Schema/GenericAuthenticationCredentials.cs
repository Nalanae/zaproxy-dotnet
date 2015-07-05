namespace ZAProxy.Schema
{
    /// <summary>
    /// Describes the configuration for generic authentication credentials.
    /// </summary>
    public class GenericAuthenticationCredentials : SpecializedAuthenticationCredentialsBase
    {
        /// <summary>
        /// Initiates a new instance of the <see cref="GenericAuthenticationCredentials"/> class.
        /// </summary>
        public GenericAuthenticationCredentials()
            : this(new AuthenticationCredentials { Type = AuthenticationCredentialsTypeConstants.Generic })
        { }

        /// <summary>
        /// Initiates a new instance of the <see cref="GenericAuthenticationCredentials"/> class.
        /// </summary>
        /// <param name="authenticationCredentials">The generic authentication credentials.</param>
        public GenericAuthenticationCredentials(AuthenticationCredentials authenticationCredentials)
            : base(authenticationCredentials, AuthenticationMethodNameConstants.Http)
        { }
    }
}
