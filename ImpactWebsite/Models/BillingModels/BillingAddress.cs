using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImpactWebsite.Models.BillingModels
{
    public class BillingAddress
    {

        [Display(Name = "BillingAddress AddressLine1 Address 1")]
        public string AddressLine1 { get; set; }

        [Display(Name = "BillingAddress AddressLine2 Address 2")]
        public string AddressLine2 { get; set; }

        [Display(Name = "BillingAddress City City")]
        public string City { get; set; }

        [Display(Name = "BillingAddress Country Country")]
        public string Country { get; set; }

        [Display(Name = "BillingAddress Name Name")]
        public string Name { get; set; }

        [Display(Name = "BillingAddress State State")]
        public string State { get; set; }

        [Display(Name = "BillingAddress Vat VAT Number")]
        public string Vat { get; set; }

        [Display(Name = "BillingAddress ZipCode Zip Code")]
        public string ZipCode { get; set; }
    }
}
