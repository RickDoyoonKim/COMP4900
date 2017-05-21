﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImpactWebsite.Models.BillingModels
{
    public class BillingAddress
    {
        [Key]
        public Int32 BillingAddressId { get; set; }

        [Display(Name = "Billing Name")]
        public string BillingName { get; set; }

        [Display(Name = "Address 1")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address 2")]
        public string AddressLine2 { get; set; }

        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        [Display(Name = "Province/State")]
        public string State { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "User")]
        public string UserId { get; set; }
    }
}
