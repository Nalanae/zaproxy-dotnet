using System;

namespace ZAProxy.Schema
{
    /// <summary>
    /// Describes the URL used in an HTTP message.
    /// </summary>
    public class MessageUrl
    {
        /// <summary>
        /// Gets or sets the HTTP status code.
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Gets or sets the HTTP method/verb.
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the time needed for the message.
        /// </summary>
        public int Time { get; set; }

        /// <summary>
        /// Gets or sets the type of the historic entry of this message.
        /// </summary>
        public int HistoricEntryType { get; set; }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        public Uri Url { get; set; }
    }
}
