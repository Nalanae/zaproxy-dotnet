using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ZAProxy
{
    [Serializable]
    public class ClientApiException : Exception
    {
        public ClientApiException()
            : base()
        { }

        public ClientApiException(string message)
            : base(message)
        { }

        public ClientApiException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public ClientApiException(string message, string code, string detail)
            : base(message)
        {
            Code = code;
            Detail = detail;
        }

        public ClientApiException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info != null)
            {
                Code = info.GetString("code");
                Detail = info.GetString("detail");
            }
        }

        public string Code { get; private set; }
        public string Detail { get; private set; }

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
