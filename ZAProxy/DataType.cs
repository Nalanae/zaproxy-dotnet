namespace ZAProxy
{
    /// <summary>
    /// Describes the data type returned from ZAP.
    /// </summary>
    public enum DataType
    {
        /// <summary>
        /// ZAP will return JSON.
        /// </summary>
        Json,
        
        /// <summary>
        /// ZAP will return XML.
        /// </summary>
        Xml,

        /// <summary>
        /// ZAP will return HTML.
        /// </summary>
        Html,

        /// <summary>
        /// ZAP will return something else (e.g. binary or string).
        /// </summary>
        Other
    }
}
