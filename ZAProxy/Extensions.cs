using System;
using System.Collections.Generic;
using System.Linq;

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
            return value.Equals(otherValue, StringComparison.OrdinalIgnoreCase);
        }
    }
}
