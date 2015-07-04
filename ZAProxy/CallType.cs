namespace ZAProxy
{
    /// <summary>
    /// Describes the type of API call in ZAP.
    /// </summary>
    public enum CallType
    {
        /// <summary>
        /// API call is a view and retrieves data from ZAP.
        /// </summary>
        View,

        /// <summary>
        /// API call is an action and tells ZAP to act on something.
        /// </summary>
        Action,

        /// <summary>
        /// API call does something else (non-standard).
        /// </summary>
        Other
    }
}
