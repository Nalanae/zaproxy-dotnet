using System;
using System.Runtime.Serialization;

namespace ZAProxy
{
    /// <summary>
    /// Describes an exception triggered by the ZAP API.
    /// </summary>
    [Serializable]
    public class ZapException : Exception
    {
        /// <summary>
        /// Initiates a new instance of the <see cref="ZapException"/> class.
        /// </summary>
        public ZapException()
            : base()
        { }

        /// <summary>
        /// Initiates a new instance of the <see cref="ZapException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ZapException(string message)
            : base(message)
        { }

        /// <summary>
        /// Initiates a new instance of the <see cref="ZapException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public ZapException(string message, Exception innerException)
            : base(message, innerException)
        { }

        /// <summary>
        /// Initiates a new instance of the <see cref="ZapException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="code">The error code given by the ZAP API.</param>
        /// <param name="detail">The error details given by the ZAP API.</param>
        public ZapException(string message, string code, string detail)
            : base(message)
        {
            Code = code;
            Detail = detail;
        }

        /// <summary>
        /// Initiates a new instance of the <see cref="ZapException"/> class.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        public ZapException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info != null)
            {
                Code = info.GetString("code");
                Detail = info.GetString("detail");
            }
        }

        /// <summary>
        /// Gets the error code given by the ZAP API.
        /// </summary>
        public string Code { get; private set; }
        
        /// <summary>
        /// Gets the error details given by the ZAP API.
        /// </summary>
        public string Detail { get; private set; }

        /// <summary>
        /// Gets the object's data for serialization.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            if (info != null)
            {
                info.AddValue("code", Code);
                info.AddValue("detail", Detail);
            }
        }
    }
}
