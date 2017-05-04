using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImpactWebsite.Models
{
    public class OrderHeader : BaseEntity
    {
        public Int64 OrderHeaderId { get; set; }
        public int OrderNum { get; set; }
        public string SalesRep { get; set; }
        public DateTime OrderedDate{ get; set; }
        public DateTime DeliveredDate { get; set; }
        public string OrderStatus { get; set; }
        public string NoteFromUser { get; set; }
        public string NoteFromAdmin { get; set; }

        public Int64 UserId { get; set; }
        public Int64 PromotionId { get; set; }

        public List<Investment> Investments { get; set; }
    }
}
