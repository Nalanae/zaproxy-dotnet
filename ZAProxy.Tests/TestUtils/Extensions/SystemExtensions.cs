using System.Collections.Generic;

namespace System
{
    public static class SystemExtensions
    {
        public static string ToJsonStringList(this IEnumerable<string> values)
        {
            return $"[{string.Join(", ", values)}]";
        }
    }
}
