using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImpactWebsite.Models
{
    public class UnitPrice : BaseEntity
    {
        [Key]
        public Int64 UnitPriceId { get; set; }

        [Required]
        [Range(0.0, double.MaxValue)]
        public Decimal UnitPriceValue { get; set; }

        public DateTime DateEffectFrom { get; set; }
        public DateTime DateEffectTo { get; set; }
    }
}
