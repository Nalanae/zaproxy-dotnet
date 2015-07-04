namespace ZAProxy.Schema
{
    /// <summary>
    /// Describes an HTTP message.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Gets or sets a note about the message.
        /// </summary>
        public string Note { get; set; }
        
        /// <summary>
        /// Gets or sets the response body.
        /// </summary>
        public string ResponseBody { get; set; }

        /// <summary>
        /// Gets or sets the cookie parameters.
        /// </summary>
        public string CookieParams { get; set; }

        /// <summary>
        /// Gets or sets the request body.
        /// </summary>
        public string RequestBody { get; set; }

        /// <summary>
        /// Gets or sets the response header.
        /// </summary>
        public string ResponseHeader { get; set; }

        /// <summary>
        /// Gets or sets the request header.
        /// </summary>
        public string RequestHeader { get; set; }

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public int Id { get; set; }
    }
}
