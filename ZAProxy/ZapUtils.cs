using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace ZAProxy
{
    /// <summary>
    /// General collection of utilities for the ZAP API client to use.
    /// </summary>
    public static class ZapUtils
    {
        /// <summary>
        /// Obtains the version of Java installed on the system.
        /// </summary>
        /// <returns>Version of Java installed on the system.</returns>
        public static Version CheckJavaVersion()
        {
            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo("java", "-version")
                    {
                        RedirectStandardError = true,
                        UseShellExecute = false
                    }
                };
                process.Start();
                var output = process.StandardError.ReadToEnd();
                var match = Regex.Split(output, "java version \"([0-9]+).([0-9]+).([0-9]+)_([0-9]+)\"");
                var versionString = $"{match[1]}.{match[2]}.{match[3]}.{match[4]}";
                return new Version(versionString);
            }
            catch
            {
                return null;
            }
        }
    }
}
