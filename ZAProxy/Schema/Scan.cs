using Newtonsoft.Json;

namespace ZAProxy.Schema
{
    /// <summary>
    /// Describes a scan.
    /// </summary>
    public class Scan
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
        [JsonConverter(typeof(Converters.ScanStateEnumConverter))]
        public ScanState State { get; set; }
    }
}
