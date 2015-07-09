using System;
using System.Collections.Generic;

namespace ZAProxy.Schema
{
    /// <summary>
    /// Base for specialized session management method classes.
    /// </summary>
    public abstract class SpecializedSessionManagementMethodBase : SessionManagementMethod
    {
        /// <summary>
        /// Initiates a new instance of the <see cref="SpecializedSessionManagementMethodBase"/> class.
        /// </summary>
        /// <param name="sessionManagementMethod">The generic session management method.</param>
        /// <param name="supportedMethodName">The method name this specialized class supports.</param>
        protected SpecializedSessionManagementMethodBase(SessionManagementMethod sessionManagementMethod, string supportedMethodName)
        {
            if (sessionManagementMethod == null)
                throw new ArgumentNullException(nameof(sessionManagementMethod));
            if (sessionManagementMethod.MethodName != supportedMethodName)
                throw new InvalidOperationException(Resources.InvalidSessionManagementMethodSupplied);
            
            BaseSessionManagementMethod = sessionManagementMethod;
            SupportedMethodName = supportedMethodName;

            base.MethodName = supportedMethodName;
        }

        /// <summary>
        /// Gets the name of the used method.
        /// </summary>
        public new string MethodName { get { return SupportedMethodName; } }

        /// <summary>
        /// Gets the session management method that provides the base of information for this specialized class.
        /// </summary>
        protected SessionManagementMethod BaseSessionManagementMethod { get; private set; }

        /// <summary>
        /// Gets the method name this specialized class supports.
        /// </summary>
        protected string SupportedMethodName { get; private set; }

        /// <summary>
        /// Gets or sets the parameters and their values used by the method.
        /// </summary>
        public override IDictionary<string, string> Parameters
        {
            get { return BaseSessionManagementMethod.Parameters; }
            set { BaseSessionManagementMethod.Parameters = value; }
        }
    }
}
