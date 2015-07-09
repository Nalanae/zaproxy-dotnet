namespace ZAProxy.Schema
{
    /// <summary>
    /// Describes a configuration parameter used in authentication.
    /// </summary>
    public class ConfigurationParameter
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets whether the parameter is mandatory.
        /// </summary>
        public bool Mandatory { get; set; }
    }
}
