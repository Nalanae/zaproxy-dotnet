using System;
using System.Collections.Generic;

namespace ZAProxy.Schema
{
    /// <summary>
    /// Describes a context in ZAP.
    /// </summary>
    public class Context
    {
        /// <summary>
        /// Gets or sets the ID of the authentication detection method.
        /// </summary>
        public int AuthenticationDetectionMethodId { get; set; }
        
        /// <summary>
        /// Gets or sets the regex pattern in the response to verify the user is logged out.
        /// </summary>
        public string LoggedOutPattern { get; set; }

        /// <summary>
        /// Gets or sets the regex patterns of urls that are excluded from the context.
        /// </summary>
        public IEnumerable<string> ExcludeRegexes { get; set; }

        /// <summary>
        /// Gets or sets the regex pattern in the response to verify the user is logged in.
        /// </summary>
        public string LoggedInPattern { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the regex patterns of urls that are included in the context.
        /// </summary>
        public IEnumerable<string> IncludeRegexes { get; set; }
        
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the authentication type used.
        /// </summary>
        /// <remarks>
        /// This returns an arbitrary value in ZAP and shouldn't be used.
        /// </remarks>
        [Obsolete("This returns an arbitrary value in ZAP and shouldn't be used.")]
        public string AuthenticationType { get; set; }
    }
}
