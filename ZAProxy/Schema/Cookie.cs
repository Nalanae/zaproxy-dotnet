using System;

namespace ZAProxy.Schema
{
    /// <summary>
    /// Describes an http cookie.
    /// </summary>
    public class Cookie
    {
        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the domain.
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Gets or sets whether the domain attribute is specified.
        /// </summary>
        public bool DomainAttributeSpecified { get; set; }

        /// <summary>
        /// Gets or sets whether the cookie has expired.
        /// </summary>
        public bool Expired { get; set; }

        /// <summary>
        /// Gets or sets the expiry date.
        /// </summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets whether the path attribute is specified.
        /// </summary>
        public bool PathAttributeSpecified { get; set; }
        
        /// <summary>
        /// Gets or sets whether the cookie is persistent.
        /// </summary>
        public bool Persistent { get; set; }

        /// <summary>
        /// Gets or sets whether the cookie is secure (HTTPS-only).
        /// </summary>
        public bool Secure { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        public int Version { get; set; }
    }
}
