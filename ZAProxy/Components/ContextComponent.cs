using System.Collections.Generic;
using ZAProxy.Infrastructure;

namespace ZAProxy.Components
{
    /// <summary>
    /// Component to manage the contexts in ZAP.
    /// </summary>
    public class ContextComponent : ComponentBase
    {
        /// <summary>
        /// Initiates a new instance of the <see cref="ContextComponent"/> class.
        /// </summary>
        /// <param name="zapProcess">The ZAP process to connect to.</param>
        public ContextComponent(IZapProcess zapProcess)
            : this(null, zapProcess)
        { }

        /// <summary>
        /// Initiates a new instance of the <see cref="ContextComponent"/> class with a specific HTTP client implementation.
        /// </summary>
        /// <param name="httpClient">The HTTP client implementation.</param>
        /// <param name="zapProcess">The ZAP process to connect to.</param>
        public ContextComponent(IHttpClient httpClient, IZapProcess zapProcess)
            : base(httpClient, zapProcess, "context")
        { }

        #region Views

        /// <summary>
        /// Gets a context.
        /// </summary>
        /// <param name="name">The name of the context.</param>
        /// <returns>The context.</returns>
        public Schema.Context GetContext(string name)
        {
            return CallView<Schema.Context>("context", null, new Parameters
            {
                { "contextName", name }
            });
        }

        /// <summary>
        /// Gets the names of all contexts.
        /// </summary>
        /// <returns>The names of all contexts.</returns>
        public IEnumerable<string> GetContextList()
        {
            return ParseJsonListString(CallView<string>("contextList", "contextList"));
        }

        /// <summary>
        /// Gets or sets the regex patterns of urls that are excluded from the context.
        /// </summary>
        /// <returns>The regex patterns that are excluded from the context.</returns>
        public IEnumerable<string> GetExcludedRegexes(string name)
        {
            return ParseJsonListString(CallView<string>("excludeRegexs", "excludeRegexs", new Parameters
            {
                { "contextName", name }
            }));
        }

        /// <summary>
        /// Gets or sets the regex patterns of urls that are included in the context.
        /// </summary>
        /// <returns>The regex patterns that are included in the context.</returns>
        public IEnumerable<string> GetIncludedRegexes(string name)
        {
            return ParseJsonListString(CallView<string>("includeRegexs", "includeRegexs", new Parameters
            {
                { "contextName", name }
            }));
        }

        #endregion

        #region Actions

        /// <summary>
        /// Excludes a url regex from the context.
        /// </summary>
        /// <param name="name">The name of the context.</param>
        /// <param name="regex">The regex pattern.</param>
        public void ExcludeFromContext(string name, string regex)
        {
            CallAction("excludeFromContext", new Parameters
            {
                { "contextName", name },
                { "regex", regex }
            });
        }

        /// <summary>
        /// Exports the context to a file at the given path.
        /// </summary>
        /// <param name="name">The name of the context.</param>
        /// <param name="filePath">The path where the file is saved.</param>
        public void ExportContext(string name, string filePath)
        {
            CallAction("exportContext", new Parameters
            {
                { "contextName", name },
                { "contextFile", filePath }
            });
        }

        /// <summary>
        /// Imports a context from a file at the given path.
        /// </summary>
        /// <param name="filePath">The path of the file that contains the context.</param>
        public void ImportContext(string filePath)
        {
            CallAction("importContext", new Parameters
            {
                { "contextFile", filePath }
            });
        }

        /// <summary>
        /// Includes a url regex in the context.
        /// </summary>
        /// <param name="name">The name of the context.</param>
        /// <param name="regex">The regex pattern.</param>
        public void IncludeInContext(string name, string regex)
        {
            CallAction("includeInContext", new Parameters
            {
                { "contextName", name },
                { "regex", regex }
            });
        }

        /// <summary>
        /// Creates a new context with the given name.
        /// </summary>
        /// <param name="name">The name of the context.</param>
        /// <returns>The ID of the newly created context.</returns>
        public int NewContext(string name)
        {
            return CallAction<int>("newContext", "contextId", new Parameters
            {
                { "contextName", name }
            });
        }

        /// <summary>
        /// Sets whether a context should be in scope.
        /// </summary>
        /// <param name="name">The name of the context.</param>
        /// <param name="inScope">True if the context should be in scope.</param>
        public void SetContextInScope(string name, bool inScope)
        {
            CallAction("setContextInScope", new Parameters
            {
                { "contextName", name },
                { "booleanInScope", inScope }
            });
        }

        #endregion
    }
}
