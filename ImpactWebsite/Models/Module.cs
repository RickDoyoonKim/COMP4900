using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImpactWebsite.Models
{
    public class Module : BaseEntity
    {
        public Int64 ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string ModuleUrl { get; set; }
        public int DeliveryDays { get; set; }
        public string Description { get; set; }

        public Int64 UnitPriceId { get; set; }
    }
}
