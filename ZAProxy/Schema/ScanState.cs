using Newtonsoft.Json;
using System;

namespace ZAProxy.Schema
{
    /// <summary>
    /// Describes the state of a scan.
    /// </summary>
    public enum ScanState
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
