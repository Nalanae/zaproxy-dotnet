using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ZAProxy.Infrastructure
{
    /// <summary>
    /// Shorthand for when parameter dictionaries are needed.
    /// </summary>
    [Serializable]
    public class Parameters : Dictionary<string, object>
    {
        /// <summary>
        /// Initiates a new instance of the <see cref="Parameters"/> class.
        /// </summary>
        public Parameters()
            : base()
        { }

        /// <summary>
        /// Initiates a new instance of the <see cref="Parameters"/> class.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        protected Parameters(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
