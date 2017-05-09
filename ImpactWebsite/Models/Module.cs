using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImpactWebsite.Models
{
    public class Module : BaseEntity
    {
        [Key]
        public Int64 ModuleId { get; set; }

        [Required]
        [StringLength(160, MinimumLength = 2)]
        public string ModuleName { get; set; }

        [Display(Name = "Module Image URL")]
        [StringLength(1024)]
        public string ModuleUrl { get; set; }

        // Devlivery finishes at least in a month
        [Display(Name = "Delivery days")]
        [Range(1, 30)]
        public int DeliveryDays { get; set; }

        public string Description { get; set; }

        public Int64 UnitPriceId { get; set; }
    }
}
