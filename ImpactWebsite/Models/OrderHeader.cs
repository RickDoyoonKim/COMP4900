﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImpactWebsite.Models
{
    public enum OrderStatusList
    {
        Pedning,
        AwaitingPayment,
        Processing,
        Completed,
        Cancelled,       
    }

    public class OrderHeader : BaseEntity
    {


        [Key]
        public Int64 OrderHeaderId { get; set; }

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

        public Int64 UserId { get; set; }
        public Int64 PromotionId { get; set; }

        public List<Investment> Investments { get; set; }
    }
}
