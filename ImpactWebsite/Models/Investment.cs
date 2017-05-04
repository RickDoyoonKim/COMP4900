using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImpactWebsite.Models
{
    public class Investment : BaseEntity
    {
        public Int64 InvestmentId { get; set; }
        public string InvestmentName { get; set; }
        public string ISIN { get; set; }
        public int ShareNumber { get; set; }
        public decimal EstimateValue { get; set; }                
    }
}
