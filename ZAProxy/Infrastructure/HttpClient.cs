using System;
using System.Net;

namespace ZAProxy.Infrastructure
{
    /// <summary>
    /// Describes a client to make HTTP calls.
    /// </summary>
    public interface IHttpClient
    {
        /// <summary>
        /// Makes an HTTP request and returns the result as a string. 
        /// </summary>
        /// <param name="url">The url to make the request to.</param>
        /// <returns>The result of the HTTP request as a string.</returns>
        string DownloadString(string url);

        /// <summary>
        /// Makes an HTTP request and returns the result as binary.
        /// </summary>
        /// <param name="url">The url to make the request to.</param>
        /// <returns>The result of the HTTP request as binary.</returns>
        byte[] DownloadData(string url);
    }

    internal class HttpClient : IHttpClient
    {
        private readonly IWebProxy _webProxy;

        public HttpClient(IZapProcess zapProcess)
        {
            var uriBuilder = new UriBuilder("http", zapProcess.Host, zapProcess.Port, "proxy.pac");
            _webProxy = new WebProxy(uriBuilder.Uri);
        }

        public string DownloadString(string url)
        {
            using (var client = GetClient())
            {
                return client.DownloadString(url);
            }
        }

        public byte[] DownloadData(string url)
        {
            using (var client = GetClient())
            {
                return client.DownloadData(url);
            }
        }

        private WebClient GetClient()
        {
            return new WebClient() { Proxy = _webProxy };
        }
    }
}
