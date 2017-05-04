using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImpactWebsite.Models
{
    public class UnitPrice : BaseEntity
    {
        public Int64 UnitPriceId { get; set; }
        public Decimal UnitPriceValue { get; set; }
        public DateTime DateEffectFrom { get; set; }
        public DateTime DateEffectTo { get; set; }
    }
}
