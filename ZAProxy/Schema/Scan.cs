using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZAProxy.Schema
{
    public class Scan
    {
        public int Progress { get; set; }
        public int Id { get; set; }
        [JsonConverter(typeof(ScanStateEnumConverter))]
        public ScanState State { get; set; }
    }
}
