using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security.AntiXss;

namespace ZAProxy
{
    /// <summary>
    /// General class to add extension methods to well-known types.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Returns a concatenated string of the string values of all elements in the set, delimited by <paramref name="separator"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in the enumeration.</typeparam>
        /// <param name="set">The set.</param>
        /// <param name="separator">The separator delimiting the individual elements from the set.</param>
        /// <returns>Concatenated string of all elements in the set.</returns>
        public static string ToString<T>(this IEnumerable<T> set, string separator)
        {
            if (set == null)
                throw new ArgumentNullException(nameof(set));

            return string.Join(separator, set.Select(v => v.ToString()));
        }

        /// <summary>
        /// Returns whether <paramref name="otherValue"/> is the same, ignoring case and culture.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="otherValue">The string to compare.</param>
        /// <returns>True if <paramref name="otherValue"/> is the same, ignoring case and culture.</returns>
        public static bool EqualsOrdinalIgnoreCase(this string value, string otherValue)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            return value.Equals(otherValue, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Joins all parameters into a query string.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>A query string.</returns>
        public static string ToQueryString(this IDictionary<string, object> parameters)
        {
            if (parameters == null || !parameters.Any())
                return string.Empty;

            var encodedParameterParts = parameters.Select(
                p => $"{AntiXssEncoder.UrlEncode(p.Key)}={AntiXssEncoder.UrlEncode((p.Value ?? "").ToString())}");
            return string.Join("&", encodedParameterParts);
        }

        /// <summary>
        /// Joins all parameters into a query string.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>A query string.</returns>
        public static string ToQueryString(this IDictionary<string, string> parameters)
        {
            if (parameters == null || !parameters.Any())
                return string.Empty;

            return parameters.ToDictionary(p => p.Key, p => (object)p.Value).ToQueryString();
        }
    }
}
