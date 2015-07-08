using System.Collections.Generic;
using ZAProxy.Infrastructure;
using ZAProxy.Schema;

namespace ZAProxy.Components
{
    /// <summary>
    /// Component to retreive obtained http parameters from ZAP.
    /// </summary>
    public class ParamsComponent : ComponentBase
    {
        /// <summary>
        /// Initiates a new instance of the <see cref="ParamsComponent"/> class.
        /// </summary>
        /// <param name="zapProcess">The ZAP process to connect to.</param>
        public ParamsComponent(IZapProcess zapProcess)
            : this(null, zapProcess)
        { }

        /// <summary>
        /// Initiates a new instance of the <see cref="ParamsComponent"/> class with a specific HTTP client implementation.
        /// </summary>
        /// <param name="httpClient">The HTTP client implementation.</param>
        /// <param name="zapProcess">The ZAP process to connect to.</param>
        public ParamsComponent(IHttpClient httpClient, IZapProcess zapProcess)
            : base(httpClient, zapProcess, "params")
        { }

        #region Views

        /// <summary>
        /// Gets all obtained http parameters. Optionally filtered by site.
        /// </summary>
        /// <param name="site">Optionally the hostname of the site.</param>
        /// <returns>All obtained http parameters.</returns>
        public IEnumerable<HttpParameter> GetParameters(string site = null)
        {
            return CallView<IEnumerable<HttpParameter>>("params", "Parameters", new Parameters
            {
                { "site", site }
            });
        }

        #endregion
    }
}
