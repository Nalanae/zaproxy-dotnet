namespace ZAProxy.Schema
{
    /// <summary>
    /// Describes the threshold of shown alerts in ZAP.
    /// </summary>
    public enum AlertThreshold
    {
        /// <summary>
        /// Alerts are never reported.
        /// </summary>
        Off,

        /// <summary>
        /// All alerts are reported.
        /// </summary>
        Default,

        /// <summary>
        /// Low, medium and high alerts are reported.
        /// </summary>
        Low,

        /// <summary>
        /// Medium and high alerts are reported.
        /// </summary>
        Medium,

        /// <summary>
        /// Only high alerts are reported.
        /// </summary>
        High
    }
}
