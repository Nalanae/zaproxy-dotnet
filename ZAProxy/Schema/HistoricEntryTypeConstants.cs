namespace ZAProxy.Schema
{
    /// <summary>
    /// Contains constants of all well-known historic entry types.
    /// </summary>
    public static class HistoricEntryTypeConstants
    {
        /// <summary>
        /// Message obtained from proxy use.
        /// </summary>
        public const int Proxied = 1;
        
        /// <summary>
        /// Message obtained by the spider.
        /// </summary>
        public const int Spider = 2;

        /// <summary>
        /// Message obtained by a scanner.
        /// </summary>
        public const int Scanner = 3;

        /// <summary>
        /// Message is hidden because of an exclusion rule.
        /// </summary>
        public const int Hidden = 6;

        /// <summary>
        /// Message obtained by a brute forcer.
        /// </summary>
        public const int BruteForce = 7;

        /// <summary>
        /// Message obtained by the fuzzer.
        /// </summary>
        public const int Fuzzer = 8;

        /// <summary>
        /// Message obtained by spider task.
        /// </summary>
        public const int SpiderTask = 9;

        /// <summary>
        /// Message obtained by the ajax spider.
        /// </summary>
        public const int SpiderAjax = 10;

        /// <summary>
        /// Message obtained by an authentication action.
        /// </summary>
        public const int Authentication = 11;

        /// <summary>
        /// Message obtained by changing access controls.
        /// </summary>
        public const int AccessControl = 13;

        /// <summary>
        /// Message obtained by the active scanner, but set as temporary.
        /// </summary>
        public const int ScannerTemporary = 14;

        /// <summary>
        /// Message obtained by a manual action of the user in ZAP.
        /// </summary>
        public const int ZapUser = 15;
    }
}
