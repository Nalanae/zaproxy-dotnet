using System;
using System.Collections.Generic;

namespace ZAProxy.Schema
{
    /// <summary>
    /// Describes the configuration for the form-based authentication method.
    /// </summary>
    public class FormBasedAuthenticationMethod : SpecializedAuthenticationMethodBase
    {
        /// <summary>
        /// Value of the login request data parameter where the user name should be injected.
        /// </summary>
        public const string UserNameParameterValue = "{%username%}";

        /// <summary>
        /// Value of the login request data parameter where the password should be injected.
        /// </summary>
        public const string PasswordParameterValue = "{%password%}";

        /// <summary>
        /// Initiates a new instance of the <see cref="FormBasedAuthenticationMethod"/> class.
        /// </summary>
        public FormBasedAuthenticationMethod()
            : this(new AuthenticationMethod { MethodName = AuthenticationMethodNameConstants.FormBased })
        { }

        /// <summary>
        /// Initiates a new instance of the <see cref="FormBasedAuthenticationMethod"/> class.
        /// </summary>
        /// <param name="authenticationMethod">The generic authentication method.</param>
        public FormBasedAuthenticationMethod(AuthenticationMethod authenticationMethod)
            : base(authenticationMethod, AuthenticationMethodNameConstants.FormBased)
        { }

        /// <summary>
        /// Gets or sets the login url.
        /// </summary>
        public Uri LoginUrl
        {
            get { return new Uri(BaseAuthenticationMethod.Parameters[AuthenticationMethodParameterConstants.LoginUrl]); }
            set { BaseAuthenticationMethod.Parameters[AuthenticationMethodParameterConstants.LoginUrl] = value.ToString(); }
        }

        /// <summary>
        /// Gets or sets the login request data.
        /// </summary>
        public string LoginRequestData
        {
            get { return BaseAuthenticationMethod.Parameters[AuthenticationMethodParameterConstants.LoginRequestData]; }
            set
            {
                if (!string.IsNullOrEmpty(value) && !ZapUtils.ValidateQueryString(value))
                    throw new InvalidOperationException(Resources.InvalidQueryString);

                BaseAuthenticationMethod.Parameters[AuthenticationMethodParameterConstants.LoginRequestData] = value;
            }
        }

        /// <summary>
        /// Gets whether a parameter exists in the login request data.
        /// </summary>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <returns>True if the parameter exists in the login request data.</returns>
        public bool HasLoginRequestDataParameter(string parameterName)
        {
            return GetLoginRequestDataParameters().Keys.Contains(parameterName);
        }

        /// <summary>
        /// Gets the value of a parameter in the login request data.
        /// </summary>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <returns>Value of the parameter.</returns>
        public string GetLoginRequestDataParameter(string parameterName)
        {
            var parameters = GetLoginRequestDataParameters();
            if (!HasLoginRequestDataParameter(parameterName))
                throw new KeyNotFoundException();
            return parameters[parameterName];
        }

        /// <summary>
        /// Sets the value of a parameter or adds the parameter in the login request data.
        /// </summary>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        public void SetLoginRequestDataParameter(string parameterName, string value)
        {
            var parameters = GetLoginRequestDataParameters();
            if (HasLoginRequestDataParameter(parameterName))
                parameters[parameterName] = value;
            else
                parameters.Add(parameterName, value);
            SetLoginRequestDataParameters(parameters);
        }

        /// <summary>
        /// Removes a parameter from the login request data.
        /// </summary>
        /// <param name="parameterName">The name of the parameter.</param>
        public void RemoveLoginRequestDataParameter(string parameterName)
        {
            var parameters = GetLoginRequestDataParameters();
            if (!HasLoginRequestDataParameter(parameterName))
                throw new KeyNotFoundException();
            parameters.Remove(parameterName);
            SetLoginRequestDataParameters(parameters);
        }

        /// <summary>
        /// Gets all the login request data parameters.
        /// </summary>
        /// <returns>All the login request data parameters.</returns>
        public IDictionary<string, string> GetLoginRequestDataParameters()
        {
            return ZapUtils.ParseQueryString(LoginRequestData);
        }

        /// <summary>
        /// Sets all the login request data parameters.
        /// </summary>
        /// <param name="parameters">The request data parameters.</param>
        public void SetLoginRequestDataParameters(IDictionary<string, string> parameters)
        {
            LoginRequestData = parameters.ToQueryString();
        }
    }
}
