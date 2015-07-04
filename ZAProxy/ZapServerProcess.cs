namespace ZAProxy
{
    /// <summary>
    /// Implementation of a <see cref="IZapProcess"/> to connect to an externally started ZAP process.
    /// </summary>
    public class ZapServerProcess : IZapProcess
    {
        /// <summary>
        /// Initiates a new instance of the <see cref="ZapServerProcess"/> class.
        /// </summary>
        /// <param name="host">The host of the ZAP proxy.</param>
        /// <param name="port">The port of the ZAP proxy.</param>
        /// <param name="apiKey">Optional API key of the ZAP proxy.</param>
        public ZapServerProcess(string host, int port, string apiKey = null)
        {
            Host = host;
            Port = port;
            ApiKey = apiKey;
            Started = false;
        }

        /// <summary>
        /// Gets or sets the API key to connect to the API.
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Gets the host of the ZAP proxy.
        /// </summary>
        public string Host { get; private set; }

        /// <summary>
        /// Gets the port of the ZAP proxy.
        /// </summary>
        public int Port { get; private set; }

        /// <summary>
        /// Gets whether the process has started.
        /// </summary>
        public bool Started { get; private set; }

        /// <summary>
        /// Checks whether ZAP is the minimum version an sets <see cref="Started"/> accordingly.
        /// </summary>
        public void Start()
        {
            if (ZapApi.CheckIfMinimumZapVersion(this))
                Started = true;
        }

        /// <summary>
        /// Sets <see cref="Started"/> to false.
        /// </summary>
        public void Stop()
        {
            Started = false;
        }
    }
}
