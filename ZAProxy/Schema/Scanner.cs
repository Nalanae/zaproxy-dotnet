using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZAProxy.Schema
{
    public class Scanner
    {
        public bool AllDependenciesAvailable { get; set; }
        public int PolicyId { get; set; }
        public int CweId { get; set; }
        public AttackStrength AttackStrength { get; set; }
        public AlertThreshold AlertThreshold { get; set; }
        public string Name { get; set; }
        public int WascId { get; set; }
        public int Id { get; set; }
        public bool Enabled { get; set; }
        public IEnumerable<int> Dependancies { get; set; }
    }
}
