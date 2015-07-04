namespace ZAProxy
{
    /// <summary>
    /// Describes a ZAP process to connect the API to.
    /// </summary>
    public interface IZapProcess
    {
        /// <summary>
        /// Gets or sets the API key to connect to the API.
        /// </summary>
        string ApiKey { get; set; }
        
        /// <summary>
        /// Gets whether the process has started.
        /// </summary>
        bool Started { get; }

        /// <summary>
        /// Gets the port of the ZAP proxy.
        /// </summary>
        int Port { get; }

        /// <summary>
        /// Gets the host of the ZAP proxy.
        /// </summary>
        string Host { get; }

        /// <summary>
        /// Starts the ZAP process.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops the ZAP process.
        /// </summary>
        void Stop();
    }
}
