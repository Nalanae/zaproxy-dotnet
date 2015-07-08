using ZAProxy.Infrastructure;

namespace ZAProxy.Components
{
    /// <summary>
    /// Component that manages the revealing of hidden/disabled fields in HTML forms.
    /// </summary>
    public class RevealComponent : ComponentBase
    {
        /// <summary>
        /// Initiates a new instance of the <see cref="RevealComponent"/> class.
        /// </summary>
        /// <param name="zapProcess">The ZAP process to connect to.</param>
        public RevealComponent(IZapProcess zapProcess)
            : this(null, zapProcess)
        { }

        /// <summary>
        /// Initiates a new instance of the <see cref="RevealComponent"/> class with a specific HTTP client implementation.
        /// </summary>
        /// <param name="httpClient">The HTTP client implementation.</param>
        /// <param name="zapProcess">The ZAP process to connect to.</param>
        public RevealComponent(IHttpClient httpClient, IZapProcess zapProcess)
            : base(httpClient, zapProcess, "reveal")
        { }

        #region Views

        /// <summary>
        /// Gets whether ZAP will show and enable all hidden/disabled fields in HTML forms.
        /// </summary>
        /// <returns>True if ZAP shows and enables all hidden/disabled fields.</returns>
        public bool IsRevealEnabled()
        {
            return CallView<bool>("reveal", "reveal");
        }

        #endregion

        #region Actions

        /// <summary>
        /// Sets whether ZAP should show and enable all hidden/disabled fields in HTML forms.
        /// </summary>
        /// <param name="enabled">True if ZAP should show and enable all hidden/disabled fields.</param>
        public void SetRevealEnabled(bool enabled)
        {
            CallAction("setReveal", new Parameters
            {
                { "reveal", enabled }
            });
        }

        #endregion
    }
}
