using System;
using System.Collections.Generic;
using System.Linq;
using ZAProxy.Components;

namespace ZAProxy
{
    /// <summary>
    /// Entrypoint for connections to the ZAP API.
    /// </summary>
    public class ZapApi
    {
        /// <summary>
        /// Minimum release number of ZAP required to use this API client.
        /// </summary>
        public const string MinimumZapVersion = "2.4.0";
        
        /// <summary>
        /// Minimum daily build number of ZAP required to use this API client.
        /// </summary>
        public const string MinimumZapDailyVersion = "D-2015-05-10";

        private IZapProcess _zapProcess;
        private IDictionary<Type, ComponentBase> _registeredComponents;

        /// <summary>
        /// Initiates a new instance of the <see cref="ZapApi"/> class and registers the default components.
        /// </summary>
        /// <param name="zapProcess">The ZAP process to connect to.</param>
        public ZapApi(IZapProcess zapProcess)
        {
            _zapProcess = zapProcess;
            _registeredComponents = new Dictionary<Type, ComponentBase>();

            RegisterComponent(new ActiveScanner(zapProcess));
            RegisterComponent(new AntiCsrf(zapProcess));
            RegisterComponent(new Core(zapProcess));
        }

        /// <summary>
        /// Gets the active scanner component.
        /// </summary>
        public ActiveScanner ActiveScanner { get { return GetComponent<ActiveScanner>(); } }

        /// <summary>
        /// Gets the anti CSRF component.
        /// </summary>
        public AntiCsrf AntiCsrf { get { return GetComponent<AntiCsrf>(); } }

        /// <summary>
        /// Gets the core component.
        /// </summary>
        public Core Core { get { return GetComponent<Core>(); } }

        /// <summary>
        /// Gets a specified type of component from the registered components.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetComponent<T>() where T : ComponentBase
        {
            if (!_registeredComponents.Keys.Contains(typeof(T)))
                throw new ZapException(Resources.UnregisteredComponentRequested);
            return (T)_registeredComponents[typeof(T)];
        }

        /// <summary>
        /// Registers a component or replaces the previous instance.
        /// </summary>
        /// <typeparam name="T">The type of component.</typeparam>
        /// <param name="component">The component.</param>
        /// <returns>This object to be able to chain register calls.</returns>
        public ZapApi RegisterComponent<T>(T component) where T : ComponentBase
        {
            if (_registeredComponents.Keys.Contains(typeof(T)))
                _registeredComponents[typeof(T)] = component;
            else
                _registeredComponents.Add(typeof(T), component);
            return this;
        }

        /// <summary>
        /// Builds a ZAP API request url.
        /// </summary>
        /// <param name="dataType">The data type of the request.</param>
        /// <param name="component">The component the API call resides in.</param>
        /// <param name="callType">The call type of the request.</param>
        /// <param name="method">The method name of the API call.</param>
        /// <param name="parameters">Optional parameters to send to the API method.</param>
        /// <returns>ZAP API request url.</returns>
        public static string BuildRequestUrl(DataType dataType, string component, CallType callType, string method, IDictionary<string, object> parameters)
        {
            var urlPath = $"http://zap/{dataType.ToString().ToLower()}/{component}/{callType.ToString().ToLower()}/{method}/";
            if (parameters != null && parameters.Any())
            {
                var queryString = parameters.ToQueryString();
                urlPath = $"{urlPath}?{queryString}";
            }
            return urlPath;
        }

        /// <summary>
        /// Checks whether <paramref name="zapProcess"/>'s version is equal or higher than the minimum required version for this API client.
        /// </summary>
        /// <param name="zapProcess"></param>
        /// <returns></returns>
        public static bool CheckIfMinimumZapVersion(IZapProcess zapProcess)
        {
            var core = new Core(zapProcess);
            var version = core.GetVersion();
            if (version.StartsWith("D-"))
                return version.CompareTo(MinimumZapDailyVersion) >= 0;
            else
                return new Version(version) >= new Version(MinimumZapVersion);
        }

        //public class Utils
        //{
        //    private static Process _process;

        //    public static void StartZap(string zapPath, int port = 8080)
        //    {
        //        try
        //        {
        //            if (File.GetAttributes(zapPath).HasFlag(FileAttributes.Directory))
        //                zapPath = Path.Combine(zapPath, "zap.bat");
        //        }
        //        catch
        //        {
        //            throw new ZapException($"ZAP not found at \"{zapPath}\"");
        //        }

        //        _process = new Process
        //        {
        //            StartInfo = new ProcessStartInfo(zapPath, "-host 127.0.0.1 -port " + port)
        //            {
        //                //RedirectStandardError = true,
        //                //RedirectStandardOutput = true,
        //                //UseShellExecute = false,
        //                //CreateNoWindow = true
        //            }
        //        };

        //        _process.Start();
        //    }


        //}
    }
}
