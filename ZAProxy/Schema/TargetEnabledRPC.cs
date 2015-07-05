using System;

namespace ZAProxy.Schema
{
    /// <summary>
    /// Describes what parameter content-types are attacked.
    /// </summary>
    [Flags]
    public enum TargetEnabledRPC
    {
        /// <summary>
        /// Attack multipart content.
        /// </summary>
        Multipart = 1,

        /// <summary>
        /// Attack XML-based content.
        /// </summary>
        Xml = 1 << 1,

        /// <summary>
        /// Attack JSON-based content.
        /// </summary>
        Json = 1 << 2,

        /// <summary>
        /// Attack Google Webkit content.
        /// </summary>
        GoogleWebToolkit = 1 << 3,

        /// <summary>
        /// Attack OData content.
        /// </summary>
        OData = 1 << 4,

        /// <summary>
        /// Attack Direct Web Remoting content.
        /// </summary>
        DirectWebRemoting = 1 << 5,

        /// <summary>
        /// Attack custom content.
        /// </summary>
        Custom = 1 << 7,

        /// <summary>
        /// Attack user-defined content.
        /// </summary>
        UserDefined = 1 << 8,

        /// <summary>
        /// Attack all default content.
        /// </summary>
        Default = Multipart | Xml | Json | GoogleWebToolkit | OData | DirectWebRemoting
    }
}
