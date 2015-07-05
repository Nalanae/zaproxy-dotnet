using System;
using System.Collections.Generic;
using ZAProxy.Infrastructure;

namespace ZAProxy.Components
{
    /// <summary>
    /// Component to manage the globally excluded url patterns.
    /// </summary>
    public class GlobalExcludeUrl : ComponentBase
    {
        /// <summary>
        /// Initiates a new instance of the <see cref="GlobalExcludeUrl"/> class.
        /// </summary>
        /// <param name="zapProcess">The ZAP process to connect to.</param>
        public GlobalExcludeUrl(IZapProcess zapProcess)
            : this(null, zapProcess)
        { }

        /// <summary>
        /// Initiates a new instance of the <see cref="GlobalExcludeUrl"/> class with a specific HTTP client implementation.
        /// </summary>
        /// <param name="httpClient">The HTTP client implementation.</param>
        /// <param name="zapProcess">The ZAP process to connect to.</param>
        public GlobalExcludeUrl(IHttpClient httpClient, IZapProcess zapProcess)
            : base(httpClient, zapProcess, "globalexcludeurl")
        { }

        #region Views

        /// <summary>
        /// Gets all the globally excluded url patterns.
        /// </summary>
        /// <remarks>
        /// Might be updated in a future version if ZAP fixes the bug.
        /// </remarks>
        /// <returns>All globally excluded url patterns.</returns>
        [Obsolete("This call contains a bug in ZAP 2.4.0, which makes the output useless.")]
        public IEnumerable<string> GetOptionTokens()
        {
            return ParseJsonListString(CallView<string>("optionTokens", "Tokens"));
        }

        /// <summary>
        /// Gets all the globally excluded url patterns.
        /// </summary>
        /// <returns>All globally excluded url patterns.</returns>
        public IEnumerable<string> GetOptionTokensNames()
        {
            return ParseJsonListString(CallView<string>("optionTokensNames", "TokensNames"));
        }

        #endregion

        #region Actions

        /// <summary>
        /// Adds a new pattern to the globally excluded url patterns.
        /// </summary>
        /// <param name="value">The pattern.</param>
        public void AddOptionToken(string value)
        {
            CallAction("addOptionToken", new Parameters
            {
                { "String", value }
            });
        }

        /// <summary>
        /// Removes a pattern from the globally excluded url patterns.
        /// </summary>
        /// <param name="value">The pattern.</param>
        public void RemoveOptionToken(string value)
        {
            CallAction("removeOptionToken", new Parameters
            {
                { "String", value }
            });
        }

        #endregion

        #region Others

        /// <summary>
        /// Generates an HTML page with a form to fuzz/test a specific POST message.
        /// </summary>
        /// <remarks>
        /// This method is probably for internal use by ZAP.
        /// </remarks>
        /// <param name="messageId">The ID of a historic POST message to create this form for.</param>
        /// <returns>HTML to render a form for the specified POST message.</returns>
        [Obsolete("This call contains a bug in ZAP 2.4.0, which makes the output useless.")]
        public string GenerateForm(int messageId)
        {
            return CallOther("genForm", new Parameters
            {
                { "hrefId", messageId }
            });
        }

        #endregion
    }
}
