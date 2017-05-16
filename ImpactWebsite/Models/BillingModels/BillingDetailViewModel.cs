using ImpactWebsite.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImpactWebsite.Models.BillingModels
{
    public class BillingDetailViewModel
    {
        public Int32 OrderHeaderId { get; set; }

        public string UserId { get; set; }

        public string UserEmail { get; set; }

        public Int32 ModuleId { get; set; }

        public string ModuleName { get; set; }

        public int TotalAmount { get; set; }
    }
}
