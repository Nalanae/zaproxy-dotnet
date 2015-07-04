using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security.AntiXss;
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

        /// <summary>
        /// Initiates a new instance of the <see cref="ZapApi"/> class.
        /// </summary>
        /// <param name="zapProcess">The ZAP process to connect to.</param>
        public ZapApi(IZapProcess zapProcess)
        {
            _zapProcess = zapProcess;
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
                var encodedParameterParts = parameters.Select(
                    p => AntiXssEncoder.UrlEncode(p.Key) + "=" + AntiXssEncoder.UrlEncode((p.Value ?? "").ToString()));
                var queryString = string.Join("&", encodedParameterParts);
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
