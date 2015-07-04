namespace System.Collections.Generic
{
    public static class SystemCollectionsGenericExtensions
    {
        public static string ToJsonStringList(this IEnumerable<string> values)
        {
            return $"[{string.Join(", ", values)}]";
        }
    }
}
