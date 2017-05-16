using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImpactWebsite.Models.OrderModels
{
    public enum OrderStatusList
    {
        AwaitingPayment,
        Processing,
        Completed,
        Cancelled,       
    }

    public class OrderHeader
    {
        [Key]
        public int OrderHeaderId { get; set; }

        public int OrderNum { get; set; }

        [Display(Name = "Sales Representative Info")]
        [StringLength(160, MinimumLength = 2)]
        public string SalesRep { get; set; }

        public DateTime OrderedDate{ get; set; }
        public DateTime DeliveredDate { get; set; }

        // Uses list of order statuses
        public OrderStatusList OrderStatus { get; set; }

        public string NoteFromUser { get; set; }
        public string NoteFromAdmin { get; set; }
        [Required]
        public string UserEmail { get; set; }

        public string UserId { get; set; }
        //public Promotion Promotion { get; set; }
        //public int PromotionId { get; set; }
        public List<OrderLine> OrderLines { get; set; }

        [Display(Name = "Total Amount")]
        public int TotalAmount { get; set; }
    
        //public List<Investment> Investments { get; set; }
    }
}
