using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZAProxy.Schema
{
    public class Policy
    {
        public AttackStrength AttackStrength { get; set; }
        public AlertThreshold AlertThreshold { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public bool Enabled { get; set; }
    }
}
