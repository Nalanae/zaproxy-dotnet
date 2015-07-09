using System.Collections.Generic;
using ZAProxy.Infrastructure;
using ZAProxy.Schema;

namespace ZAProxy.Components
{
    /// <summary>
    /// Component that manages the users of a context.
    /// </summary>
    public class UsersComponent : ComponentBase
    {
        /// <summary>
        /// Initiates a new instance of the <see cref="UsersComponent"/> class.
        /// </summary>
        /// <param name="zapProcess">The ZAP process to connect to.</param>
        public UsersComponent(IZapProcess zapProcess)
            : this(null, zapProcess)
        { }

        /// <summary>
        /// Initiates a new instance of the <see cref="UsersComponent"/> class with a specific HTTP client implementation.
        /// </summary>
        /// <param name="httpClient">The HTTP client implementation.</param>
        /// <param name="zapProcess">The ZAP process to connect to.</param>
        public UsersComponent(IHttpClient httpClient, IZapProcess zapProcess)
            : base(httpClient, zapProcess, "users")
        { }

        #region Views

        /// <summary>
        /// Gets the authentication credentials of a specific user.
        /// </summary>
        /// <param name="contextId">The ID of the context.</param>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>Authentication credentials of the specified user.</returns>
        public AuthenticationCredentials GetAuthenticationCredentials(int contextId, int userId)
        {
            return CallView<AuthenticationCredentials>("getAuthenticationCredentials", null, new Parameters
            {
                { "contextId", contextId },
                { "userId", userId }
            });
        }

        /// <summary>
        /// Gets the authentication credentials configuration parameters.
        /// </summary>
        /// <param name="contextId">The ID of the context.</param>
        /// <returns>Authentication credentials configuration parameters.</returns>
        public IEnumerable<ConfigurationParameter> GetAuthenticationCredentialsConfigParameters(int contextId)
        {
            return CallView<IEnumerable<ConfigurationParameter>>("getAuthenticationCredentialsConfigParams", "methodConfigParams", new Parameters
            {
                { "contextId", contextId }
            });
        }

        /// <summary>
        /// Gets a specific user.
        /// </summary>
        /// <param name="contextId">The ID of the context.</param>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>Specified user.</returns>
        public User GetUser(int contextId, int userId)
        {
            return CallView<User>("getUserById", null, new Parameters
            {
                { "contextId", contextId },
                { "userId", userId }
            });
        }

        /// <summary>
        /// Gets all users of a context.
        /// </summary>
        /// <param name="contextId">The ID of the context.</param>
        /// <returns>All users of the context.</returns>
        public IEnumerable<User> GetUsers(int contextId)
        {
            return CallView<IEnumerable<User>>("usersList", "usersList", new Parameters
            {
                { "contextId", contextId }
            });
        }

        #endregion

        #region Actions

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="contextId">The ID of the context.</param>
        /// <param name="name">The name of the user.</param>
        /// <returns>The ID of the newly created user.</returns>
        public int CreateUser(int contextId, string name)
        {
            return CallAction<int>("newUser", "userId", new Parameters
            {
                { "contextId", contextId },
                { "name", name }
            });
        }

        /// <summary>
        /// Removes a user.
        /// </summary>
        /// <param name="contextId">The ID of the context.</param>
        /// <param name="userId">The ID of the user.</param>
        public void RemoveUser(int contextId, int userId)
        {
            CallAction("removeUser", new Parameters
            {
                { "contextId", contextId },
                { "userId", userId }
            });
        }

        /// <summary>
        /// Sets the authentication credentials of a specific user.
        /// </summary>
        /// <param name="contextId">The ID of the context.</param>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="authenticationCredentials">The authentication credentials.</param>
        public void SetAuthenticationCredentials(int contextId, int userId, AuthenticationCredentials authenticationCredentials)
        {
            var parameters = new Parameters
            {
                { "contextId", contextId },
                { "userId", userId }
            };
            foreach (var parameter in authenticationCredentials.Parameters)
                parameters.Add(parameter.Key, parameter.Value);

            CallAction("setAuthenticationCredentials", parameters);
        }

        /// <summary>
        /// Sets whether a user is enabled.
        /// </summary>
        /// <param name="contextId">The ID of the context.</param>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="enabled">True if the user should be enabled.</param>
        public void SetUserEnabled(int contextId, int userId, bool enabled)
        {
            CallAction("setUserEnabled", new Parameters
            {
                { "contextId", contextId },
                { "userId", userId },
                { "enabled", enabled }
            });
        }

        /// <summary>
        /// Sets the name of a specific user.
        /// </summary>
        /// <remarks>
        /// This only changes the name of the user entity, not the user name of the credentials!
        /// </remarks>
        /// <param name="contextId">The ID of the context.</param>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="name">The name of the user.</param>
        public void SetUserName(int contextId, int userId, string name)
        {
            CallAction("setUserName", new Parameters
            {
                { "contextId", contextId },
                { "userId", userId },
                { "name", name }
            });
        }

        #endregion
    }
}
