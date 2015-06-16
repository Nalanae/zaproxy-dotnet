using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZAProxy.Infrastructure
{
    public interface IHttpClient
    {
        string DownloadString(string url);
    }

    internal class HttpClient : IHttpClient
    {
        public string DownloadString(string url)
        {
            return new System.Net.WebClient().DownloadString(url);
        }
    }
}
