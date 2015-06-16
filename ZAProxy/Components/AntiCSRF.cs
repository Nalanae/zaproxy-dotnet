using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ZAProxy.Infrastructure;

namespace ZAProxy.Components
{
    /// <summary>
    /// Component for testing protection against Cross Site Request Forgery.
    /// </summary>
    public class AntiCSRF : ComponentBase
    {
        public AntiCSRF(string apiKey = null)
            : this(null, apiKey)
        { }

        public AntiCSRF(IHttpClient httpClient, string apiKey = null)
            : base(httpClient, apiKey, "acsrf")
        { }

        public IEnumerable<string> GetOptionTokenNames()
        {
            return ParseStringList(CallView<string>("optionTokensNames", "TokensNames"));
        }

        public void AddOptionTokenName(string name)
        {
            CallAction("addOptionToken", new Dictionary<string, object>
            {
                { "String", name }
            });
        }

        public void RemoveOptionTokenName(string name)
        {
            CallAction("removeOptionToken", new Dictionary<string, object>
            {
                { "String", name }
            });
        }

        public byte[] GenerateForm(string hrefId)
        {
            throw new NotImplementedException();
        }
    }
}
