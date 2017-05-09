using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImpactWebsite.Models
{
    public class Promotion : BaseEntity
    {
        [Key]
        public Int64 PromotionId { get; set; }

        [Required]
        [StringLength(160, MinimumLength = 2)]
        public string PromotionName { get; set; }

        [Required]
        [Range(0.0, double.MaxValue)]
        public Decimal DiscountRate { get; set; }

        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public string Description { get; set; }
    }
}
