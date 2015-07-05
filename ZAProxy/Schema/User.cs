using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZAProxy.Schema
{
    /// <summary>
    /// Describes a user used by a context.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the ID of the context these credentials belong to.
        /// </summary>
        public virtual int ContextId { get; set; }

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// Gets or sets whether the credentials are enabled.
        /// </summary>
        public virtual bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the credentials used by the user.
        /// </summary>
        public virtual AuthenticationCredentials Credentials { get; set; }
    }
}
