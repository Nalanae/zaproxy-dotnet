using System;
using System.Collections.Generic;

namespace ZAProxy.Schema
{
    /// <summary>
    /// Base for specialized authentication credential classes.
    /// </summary>
    public abstract class SpecializedAuthenticationCredentialsBase : AuthenticationCredentials
    {
        /// <summary>
        /// Initiates a new instance of the <see cref="SpecializedAuthenticationCredentialsBase"/> class.
        /// </summary>
        /// <param name="authenticationCredential">The generic authentication credential.</param>
        /// <param name="supportedType">The method name this specialized class supports.</param>
        protected SpecializedAuthenticationCredentialsBase(AuthenticationCredentials authenticationCredential, string supportedType)
        {
            if (authenticationCredential == null)
                throw new ArgumentNullException(nameof(authenticationCredential));
            if (authenticationCredential.Type != supportedType)
                throw new InvalidOperationException(Resources.InvalidAuthenticationCredentialSupplied);
            
            BaseAuthenticationCredential = authenticationCredential;
            SupportedType = supportedType;

            base.Type = SupportedType;
        }

        /// <summary>
        /// Gets the name of the type of credential.
        /// </summary>
        public new string Type { get { return SupportedType; } }

        /// <summary>
        /// Gets or sets the parameters and their values used by the method.
        /// </summary>
        public override IDictionary<string, string> Parameters
        {
            get { return BaseAuthenticationCredential.Parameters; }
            set { BaseAuthenticationCredential.Parameters = value; }
        }

        /// <summary>
        /// Gets the authentication credential that provides the base of information for this specialized class.
        /// </summary>
        protected AuthenticationCredentials BaseAuthenticationCredential { get; private set; }

        /// <summary>
        /// Gets the method name this specialized class supports.
        /// </summary>
        protected string SupportedType { get; private set; }
    }
}
