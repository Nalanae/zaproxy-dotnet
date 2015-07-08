using System.Collections.Generic;
using ZAProxy.Infrastructure;
using System.Linq;
using System;
using ZAProxy.Schema;

namespace ZAProxy.Components
{
    /// <summary>
    /// Component for managing the scripting engines in ZAP.
    /// </summary>
    public class ScriptComponent : ComponentBase
    {
        /// <summary>
        /// Initiates a new instance of the <see cref="ScriptComponent"/> class.
        /// </summary>
        /// <param name="zapProcess">The ZAP process to connect to.</param>
        public ScriptComponent(IZapProcess zapProcess)
            : this(null, zapProcess)
        { }

        /// <summary>
        /// Initiates a new instance of the <see cref="ScriptComponent"/> class with a specific HTTP client implementation.
        /// </summary>
        /// <param name="httpClient">The HTTP client implementation.</param>
        /// <param name="zapProcess">The ZAP process to connect to.</param>
        public ScriptComponent(IHttpClient httpClient, IZapProcess zapProcess)
            : base(httpClient, zapProcess, "script")
        { }

        #region Views

        /// <summary>
        /// Gets all the scripting engines registered in ZAP.
        /// Keys are the languages, values are the engine used.
        /// </summary>
        /// <returns>All scripting engines.</returns>
        public IDictionary<string, string> GetEngines()
        {
            return CallView<IEnumerable<string>>("listEngines", "listEngines")
                .Select(e =>
                {
                    var splitKeyValue = e.Split(new[] { " , " }, StringSplitOptions.None);
                    return new { Key = splitKeyValue[0], Value = splitKeyValue[1] };
                }).ToDictionary(e => e.Key, e => e.Value);
        }

        /// <summary>
        /// Gets all the scripts.
        /// </summary>
        /// <returns>All the scripts.</returns>
        public IEnumerable<Script> GetScripts()
        {
            return CallView<IEnumerable<Script>>("listScripts", "listScripts");
        }

        #endregion

        #region Actions

        /// <summary>
        /// Disables a script.
        /// </summary>
        /// <param name="name">The name of the script.</param>
        public void DisableScript(string name)
        {
            CallAction("disable", new Parameters
            {
                { "scriptName", name }
            });
        }

        /// <summary>
        /// Enables a script.
        /// </summary>
        /// <param name="name">The name of the script.</param>
        public void EnableScript(string name)
        {
            CallAction("enable", new Parameters
            {
                { "scriptName", name }
            });
        }

        /// <summary>
        /// Loads a new script from a file.
        /// </summary>
        /// <param name="name">The name of the new script.</param>
        /// <param name="type">The type of the script.</param>
        /// <param name="engine">The engine used to run the script.</param>
        /// <param name="path">The path to the file that contains the script.</param>
        /// <param name="description">Optional description for the script.</param>
        public void LoadScript(string name, string type, string engine, string path, string description = null)
        {
            CallAction("load", new Parameters
            {
                { "scriptName", name },
                { "scriptType", type },
                { "scriptEngine", engine },
                { "fileName", path },
                { "scriptDescription", description }
            });
        }

        /// <summary>
        /// Removes a script.
        /// </summary>
        /// <param name="name">The name of the script.</param>
        public void RemoveScript(string name)
        {
            CallAction("remove", new Parameters
            {
                { "scriptName", name }
            });
        }

        /// <summary>
        /// Runs a stand-alone script.
        /// </summary>
        /// <param name="name">The name of the script.</param>
        public void RunStandAloneScript(string name)
        {
            CallAction("runStandAloneScript", new Parameters
            {
                { "scriptName", name }
            });
        }

        #endregion
    }
}
