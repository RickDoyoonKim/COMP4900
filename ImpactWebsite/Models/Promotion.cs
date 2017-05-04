using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImpactWebsite.Models
{
    public class Promotion : BaseEntity
    {
        public Int64 PromotionId { get; set; }
        public string PromotionName { get; set; }
        public Decimal DiscountRate { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Description { get; set; }
    }
}
