using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZAProxy.Schema
{
    /// <summary>
    /// Describes the risk of an alert.
    /// </summary>
    public enum Risk
    {
        /// <summary>
        /// Alert is information.
        /// </summary>
        Info,
        
        /// <summary>
        /// Alert has low risk.
        /// </summary>
        Low,

        /// <summary>
        /// Alert has medium risk.
        /// </summary>
        Medium,

        /// <summary>
        /// Alert has high risk.
        /// </summary>
        High
    }
}
