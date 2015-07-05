using System;
using System.Collections.Generic;

namespace ZAProxy.Schema
{
    /// <summary>
    /// Base for specialized authentication method classes.
    /// </summary>
    public abstract class SpecializedAuthenticationMethodBase : AuthenticationMethod
    {
        /// <summary>
        /// Initiates a new instance of the <see cref="SpecializedAuthenticationMethodBase"/> class.
        /// </summary>
        /// <param name="authenticationMethod">The generic authentication method.</param>
        /// <param name="supportedMethodName">The method name this specialized class supports.</param>
        protected SpecializedAuthenticationMethodBase(AuthenticationMethod authenticationMethod, string supportedMethodName)
        {
            if (authenticationMethod == null)
                throw new ArgumentNullException(nameof(authenticationMethod));
            if (authenticationMethod.MethodName != supportedMethodName)
                throw new InvalidOperationException(Resources.InvalidAuthenticationMethodSupplied);
            
            BaseAuthenticationMethod = authenticationMethod;
            SupportedMethodName = supportedMethodName;

            base.MethodName = supportedMethodName;
        }

        /// <summary>
        /// Gets the name of the used method.
        /// </summary>
        public new string MethodName { get { return SupportedMethodName; } }

        /// <summary>
        /// Gets the authentication method that provides the base of information for this specialized class.
        /// </summary>
        protected AuthenticationMethod BaseAuthenticationMethod { get; private set; }

        /// <summary>
        /// Gets the method name this specialized class supports.
        /// </summary>
        protected string SupportedMethodName { get; private set; }

        /// <summary>
        /// Gets or sets the parameters and their values used by the method.
        /// </summary>
        public override IDictionary<string, string> Parameters
        {
            get { return BaseAuthenticationMethod.Parameters; }
            set { BaseAuthenticationMethod.Parameters = value; }
        }
    }
}
