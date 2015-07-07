using Newtonsoft.Json;
using System;

namespace ZAProxy.Schema
{
    /// <summary>
    /// Describes the state of an active scan.
    /// </summary>
    [JsonConverter(typeof(Converters.ActiveScanStateEnumConverter))]
    public enum ActiveScanState
    {
        /// <summary>
        /// The scan hasn't started yet.
        /// </summary>
        NotStarted,

        /// <summary>
        /// The scan is running.
        /// </summary>
        Running,

        /// <summary>
        /// The scan is paused.
        /// </summary>
        Paused,

        /// <summary>
        /// The scan is finished.
        /// </summary>
        Finished
    }
}
