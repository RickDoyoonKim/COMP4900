using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImpactWebsite.Models
{
    public class OrderLine : BaseEntity
    {
        public Int64 OrderLineId { get; set; }
        public Int64 OrderHeaderId {get;set;}
        public Int64 ModuleId { get; set; }
    }
}
