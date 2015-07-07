using Newtonsoft.Json;

namespace ZAProxy.Schema
{
    /// <summary>
    /// Describes an active scan.
    /// </summary>
    public class ActiveScan
    {
        /// <summary>
        /// Gets or sets the progress (in percentage).
        /// </summary>
        public int Progress { get; set; }

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the current state.
        /// </summary>
        public ActiveScanState State { get; set; }
    }
}
