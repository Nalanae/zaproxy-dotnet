using System.Collections.Generic;
using HttpArchive;
using ZAProxy.Infrastructure;
using ZAProxy.Schema;

namespace ZAProxy.Components
{
    /// <summary>
    /// Component to search for historic messages in ZAP.
    /// </summary>
    public class SearchComponent : ComponentBase
    {
        /// <summary>
        /// Initiates a new instance of the <see cref="SearchComponent"/> class.
        /// </summary>
        /// <param name="zapProcess">The ZAP process to connect to.</param>
        public SearchComponent(IZapProcess zapProcess)
            : this(null, zapProcess)
        { }

        /// <summary>
        /// Initiates a new instance of the <see cref="SearchComponent"/> class with a specific HTTP client implementation.
        /// </summary>
        /// <param name="httpClient">The HTTP client implementation.</param>
        /// <param name="zapProcess">The ZAP process to connect to.</param>
        public SearchComponent(IHttpClient httpClient, IZapProcess zapProcess)
            : base(httpClient, zapProcess, "search")
        { }

        #region Views

        /// <summary>
        /// Gets messages by searching the headers. Optionally filtered by a base url and/or paged.
        /// </summary>
        /// <param name="regex">The regex pattern to search for.</param>
        /// <param name="baseUrl">Optional base url of the requested messages.</param>
        /// <param name="start">Optional starting index (1-based).</param>
        /// <param name="count">Optional maximum amount of messages to retrieve.</param>
        /// <returns>All found messages.</returns>
        public IEnumerable<Message> GetMessagesByHeader(string regex, string baseUrl = null, int? start = null, int? count = null)
        {
            return CallView<IEnumerable<Message>>("messagesByHeaderRegex", "messagesByHeaderRegex", new Parameters
            {
                { "regex", regex },
                { "baseurl", baseUrl },
                { "start", start },
                { "count", count }
            });
        }

        /// <summary>
        /// Gets messages by searching the request. Optionally filtered by a base url and/or paged.
        /// </summary>
        /// <param name="regex">The regex pattern to search for.</param>
        /// <param name="baseUrl">Optional base url of the requested messages.</param>
        /// <param name="start">Optional starting index (1-based).</param>
        /// <param name="count">Optional maximum amount of messages to retrieve.</param>
        /// <returns>All found messages.</returns>
        public IEnumerable<Message> GetMessagesByRequest(string regex, string baseUrl = null, int? start = null, int? count = null)
        {
            return CallView<IEnumerable<Message>>("messagesByRequestRegex", "messagesByRequestRegex", new Parameters
            {
                { "regex", regex },
                { "baseurl", baseUrl },
                { "start", start },
                { "count", count }
            });
        }

        /// <summary>
        /// Gets messages by searching the response. Optionally filtered by a base url and/or paged.
        /// </summary>
        /// <param name="regex">The regex pattern to search for.</param>
        /// <param name="baseUrl">Optional base url of the requested messages.</param>
        /// <param name="start">Optional starting index (1-based).</param>
        /// <param name="count">Optional maximum amount of messages to retrieve.</param>
        /// <returns>All found messages.</returns>
        public IEnumerable<Message> GetMessagesByResponse(string regex, string baseUrl = null, int? start = null, int? count = null)
        {
            return CallView<IEnumerable<Message>>("messagesByResponseRegex", "messagesByResponseRegex", new Parameters
            {
                { "regex", regex },
                { "baseurl", baseUrl },
                { "start", start },
                { "count", count }
            });
        }

        /// <summary>
        /// Gets messages by searching the url. Optionally filtered by a base url and/or paged.
        /// </summary>
        /// <param name="regex">The regex pattern to search for.</param>
        /// <param name="baseUrl">Optional base url of the requested messages.</param>
        /// <param name="start">Optional starting index (1-based).</param>
        /// <param name="count">Optional maximum amount of messages to retrieve.</param>
        /// <returns>All found messages.</returns>
        public IEnumerable<Message> GetMessagesByUrl(string regex, string baseUrl = null, int? start = null, int? count = null)
        {
            return CallView<IEnumerable<Message>>("messagesByUrlRegex", "messagesByUrlRegex", new Parameters
            {
                { "regex", regex },
                { "baseurl", baseUrl },
                { "start", start },
                { "count", count }
            });
        }

        /// <summary>
        /// Gets message urls by searching the headers. Optionally filtered by a base url and/or paged.
        /// </summary>
        /// <param name="regex">The regex pattern to search for.</param>
        /// <param name="baseUrl">Optional base url of the requested messages.</param>
        /// <param name="start">Optional starting index (1-based).</param>
        /// <param name="count">Optional maximum amount of messages to retrieve.</param>
        /// <returns>All found message urls.</returns>
        public IEnumerable<MessageUrl> GetUrlsByHeader(string regex, string baseUrl = null, int? start = null, int? count = null)
        {
            return CallView<IEnumerable<MessageUrl>>("urlsByHeaderRegex", "urlsByHeaderRegex", new Parameters
            {
                { "regex", regex },
                { "baseurl", baseUrl },
                { "start", start },
                { "count", count }
            });
        }


        /// <summary>
        /// Gets message urls by searching the request. Optionally filtered by a base url and/or paged.
        /// </summary>
        /// <param name="regex">The regex pattern to search for.</param>
        /// <param name="baseUrl">Optional base url of the requested messages.</param>
        /// <param name="start">Optional starting index (1-based).</param>
        /// <param name="count">Optional maximum amount of messages to retrieve.</param>
        /// <returns>All found message urls.</returns>
        public IEnumerable<MessageUrl> GetUrlsByRequest(string regex, string baseUrl = null, int? start = null, int? count = null)
        {
            return CallView<IEnumerable<MessageUrl>>("urlsByRequestRegex", "urlsByRequestRegex", new Parameters
            {
                { "regex", regex },
                { "baseurl", baseUrl },
                { "start", start },
                { "count", count }
            });
        }


        /// <summary>
        /// Gets message urls by searching the response. Optionally filtered by a base url and/or paged.
        /// </summary>
        /// <param name="regex">The regex pattern to search for.</param>
        /// <param name="baseUrl">Optional base url of the requested messages.</param>
        /// <param name="start">Optional starting index (1-based).</param>
        /// <param name="count">Optional maximum amount of messages to retrieve.</param>
        /// <returns>All found message urls.</returns>
        public IEnumerable<MessageUrl> GetUrlsByResponse(string regex, string baseUrl = null, int? start = null, int? count = null)
        {
            return CallView<IEnumerable<MessageUrl>>("urlsByResponseRegex", "urlsByResponseRegex", new Parameters
            {
                { "regex", regex },
                { "baseurl", baseUrl },
                { "start", start },
                { "count", count }
            });
        }


        /// <summary>
        /// Gets message urls by searching the urls. Optionally filtered by a base url and/or paged.
        /// </summary>
        /// <param name="regex">The regex pattern to search for.</param>
        /// <param name="baseUrl">Optional base url of the requested messages.</param>
        /// <param name="start">Optional starting index (1-based).</param>
        /// <param name="count">Optional maximum amount of messages to retrieve.</param>
        /// <returns>All found message urls.</returns>
        public IEnumerable<MessageUrl> GetUrlsByUrl(string regex, string baseUrl = null, int? start = null, int? count = null)
        {
            return CallView<IEnumerable<MessageUrl>>("urlsByUrlRegex", "urlsByUrlRegex", new Parameters
            {
                { "regex", regex },
                { "baseurl", baseUrl },
                { "start", start },
                { "count", count }
            });
        }

        #endregion

        #region Others

        /// <summary>
        /// Gets messages in <see cref="Har"/> format by searching the headers. Optionally filtered by a base url and/or paged.
        /// </summary>
        /// <param name="regex">The regex pattern to search for.</param>
        /// <param name="baseUrl">Optional base url of the requested messages.</param>
        /// <param name="start">Optional starting index (1-based).</param>
        /// <param name="count">Optional maximum amount of messages to retrieve.</param>
        /// <returns>All found messages in <see cref="Har"/> format.</returns>
        public Har GetHarByHeader(string regex, string baseUrl = null, int? start = null, int? count = null)
        {
            var result = CallOther("harByHeaderRegex", new Parameters
            {
                { "regex", regex },
                { "baseurl", baseUrl },
                { "start", start },
                { "count", count }
            });
            return Har.Deserialize(result);
        }

        /// <summary>
        /// Gets messages in <see cref="Har"/> format by searching the request. Optionally filtered by a base url and/or paged.
        /// </summary>
        /// <param name="regex">The regex pattern to search for.</param>
        /// <param name="baseUrl">Optional base url of the requested messages.</param>
        /// <param name="start">Optional starting index (1-based).</param>
        /// <param name="count">Optional maximum amount of messages to retrieve.</param>
        /// <returns>All found messages in <see cref="Har"/> format.</returns>
        public Har GetHarByRequest(string regex, string baseUrl = null, int? start = null, int? count = null)
        {
            var result = CallOther("harByRequestRegex", new Parameters
            {
                { "regex", regex },
                { "baseurl", baseUrl },
                { "start", start },
                { "count", count }
            });
            return Har.Deserialize(result);
        }

        /// <summary>
        /// Gets messages in <see cref="Har"/> format by searching the response. Optionally filtered by a base url and/or paged.
        /// </summary>
        /// <param name="regex">The regex pattern to search for.</param>
        /// <param name="baseUrl">Optional base url of the requested messages.</param>
        /// <param name="start">Optional starting index (1-based).</param>
        /// <param name="count">Optional maximum amount of messages to retrieve.</param>
        /// <returns>All found messages in <see cref="Har"/> format.</returns>
        public Har GetHarByResponse(string regex, string baseUrl = null, int? start = null, int? count = null)
        {
            var result = CallOther("harByResponseRegex", new Parameters
            {
                { "regex", regex },
                { "baseurl", baseUrl },
                { "start", start },
                { "count", count }
            });
            return Har.Deserialize(result);
        }

        /// <summary>
        /// Gets messages in <see cref="Har"/> format by searching the urls. Optionally filtered by a base url and/or paged.
        /// </summary>
        /// <param name="regex">The regex pattern to search for.</param>
        /// <param name="baseUrl">Optional base url of the requested messages.</param>
        /// <param name="start">Optional starting index (1-based).</param>
        /// <param name="count">Optional maximum amount of messages to retrieve.</param>
        /// <returns>All found messages in <see cref="Har"/> format.</returns>
        public Har GetHarByUrl(string regex, string baseUrl = null, int? start = null, int? count = null)
        {
            var result = CallOther("harByUrlRegex", new Parameters
            {
                { "regex", regex },
                { "baseurl", baseUrl },
                { "start", start },
                { "count", count }
            });
            return Har.Deserialize(result);
        }

        #endregion
    }
}
