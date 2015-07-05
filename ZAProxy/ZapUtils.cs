using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

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

        /// <summary>
        /// Parses a query string to a dictionary.
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public static IDictionary<string, string> ParseQueryString(string queryString)
        {
            if (queryString == null)
                throw new ArgumentNullException(nameof(queryString));
            if (!ValidateQueryString(queryString))
                throw new InvalidOperationException(Resources.InvalidQueryString);

            var parameters = HttpUtility.ParseQueryString(queryString);
            return parameters.Cast<string>()
                .Select(k => new { Key = k, Value = parameters[k] })
                .ToDictionary(p => p.Key, p => p.Value);
        }

        /// <summary>
        /// Gets whether a query string is well-formed.
        /// </summary>
        /// <param name="queryString">The query string.</param>
        /// <returns>True if the query string is well-formed.</returns>
        public static bool ValidateQueryString(string queryString)
        {
            if (queryString == null)
                throw new ArgumentNullException(nameof(queryString));

            return Uri.IsWellFormedUriString(queryString, UriKind.Relative) && new Uri(queryString).Query == queryString;
        }
    }
}
