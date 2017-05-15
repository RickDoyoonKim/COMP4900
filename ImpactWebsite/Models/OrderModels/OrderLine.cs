using ImpactWebsite.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImpactWebsite.Models.OrderModels
{
    public class OrderLine : BaseEntity
    {
        [Key]
        public int OrderLineId { get; set; }
        public int OrderHeaderId {get;set;}
        public OrderHeader OrderHeader { get; set; }
        public int ModuleId { get; set; }
        public OrderModule Modules { get; set; }
    }
}
