namespace ZAProxy.Schema
{
    /// <summary>
    /// Describes a script in ZAP.
    /// </summary>
    public class Script
    {
        /// <summary>
        /// Gets or sets the engine used for this script.
        /// </summary>
        public string Engine { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the type of script.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets whether the script contains a syntax error.
        /// </summary>
        public bool Error { get; set; }

        /// <summary>
        /// Gets or sets whether the script is enabled.
        /// </summary>
        public bool Enabled { get; set; }
    }
}
