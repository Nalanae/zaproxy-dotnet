using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZAProxy
{
    public static class Extensions
    {
        public static string ToString<T>(this IEnumerable<T> values, string separator)
        {
            return string.Join(separator, values.Select(v => v.ToString()));
        }
    }
}
