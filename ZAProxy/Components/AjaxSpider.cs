//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using ZAProxy.Infrastructure;

//namespace ZAProxy.Components
//{
//    public class AjaxSpider : ComponentBase
//    {
//        public AjaxSpider(string apiKey = null)
//            : this(null, apiKey)
//        { }

//        public AjaxSpider(IHttpClient httpClient, string apiKey = null)
//            : base(httpClient, apiKey, "ajaxSpider")
//        { }

//        public int GetNumberOfResults()
//        {
//            return CallView<int>("numberOfResults", "numberOfResults");
//        }

//        public IEnumerable<object> GetResults(int start, int count)
//        {
//            return CallView<IEnumerable<object>>("results", "results");
//        }

//        public string GetStatus()
//        {
//            return CallView<string>("status", "status");
//        }

//        public void Scan(string url, string inScope)
//        {
//            CallAction("scan", new Dictionary<string, object>
//            {
//                { "url", url },
//                { "inScope", inScope }
//            });
//        }

//        public void Stop()
//        {
//            CallAction("stop");
//        }
//    }
//}