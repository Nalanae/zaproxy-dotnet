using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZAProxy.Schema
{
    /// <summary>
    /// Describes flags that can be assigned to http parameters.
    /// </summary>
    public enum HttpParameterFlag
    {
        /// <summary>
        /// Parameter is part of the anti CSRF protection.
        /// </summary>
        AntiCsrf,

        /// <summary>
        /// Parameter is part of the session state.
        /// </summary>
        Session,

        /// <summary>
        /// Parameter is a structural part of the system.
        /// </summary>
        Structural
    }
}
