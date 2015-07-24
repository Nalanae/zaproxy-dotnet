namespace ZAProxy.Schema
{
    /// <summary>
    /// Describes the status of a spider scan.
    /// </summary>
    public class SpiderScan
    {
        /// <summary>
        /// Gets or sets the completed percentage.
        /// </summary>
        public int Progress { get; set; }

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        public ScanState State { get; set; }
    }
}
