using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZAProxy.Schema
{
    /// <summary>
    /// Describes a configuration parameter of an authentication method.
    /// </summary>
    public class AuthenticationMethodConfigParameter
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets whether the parameter is mandatory.
        /// </summary>
        public bool Mandatory { get; set; }
    }
}
