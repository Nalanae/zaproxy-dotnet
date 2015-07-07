using System;

namespace ZAProxy.Schema
{
    /// <summary>
    /// Describes what parameter-types are attacked.
    /// </summary>
    [Flags]
    public enum TargetInjectable
    {
        /// <summary>
        /// Attack query string parameters.
        /// </summary>
        QueryString = 1,

        /// <summary>
        /// Attack post data parameters.
        /// </summary>
        PostData = 1 << 1,

        /// <summary>
        /// Attack cookie parameters.
        /// </summary>
        Cookie = 1 << 2,

        /// <summary>
        /// Attack HTTP header parameters.
        /// </summary>
        HttpHeaders = 1 << 3,

        /// <summary>
        /// Attack URL path parameters.
        /// </summary>
        UrlPath = 1 << 4,

        /// <summary>
        /// Attack all the default parameters.
        /// </summary>
        Default = QueryString | PostData
    }
}
