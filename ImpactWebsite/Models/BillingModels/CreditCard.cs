using Microsoft.ApplicationInsights.AspNetCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ImpactWebsite.Models.BillingModels
{

    public class CreditCard
    {

        [Display(Name = "CreditCard AddressCity City")]
        [Required(ErrorMessageResourceName = "CreditCard AddressCity Please enter your City ")]
        public string AddressCity { get; set; }

        [Display(Name = "CreditCard_AddressCountry_Country")]
        [Required(ErrorMessageResourceName = "CreditCard AddressCountry Please enter your Country")]
        public string AddressCountry { get; set; }

        [Display(Name = "CreditCard AddressLine1 Address")]
        [Required(ErrorMessageResourceName = "CreditCard AddressLine1 Please enter your address")]
        public string AddressLine1 { get; set; }

        [Display(Name = "CreditCard AddressLine2 Address")]
        public string AddressLine2 { get; set; }

        [Display(Name = "CreditCard AddressState State")]
        public string AddressState { get; set; }

        [Display(Name = "CreditCard AddressZip Post code")]
        [Required(ErrorMessageResourceName = "CreditCard AddressZip Please enter your Post Code")]
        public string AddressZip { get; set; }

        public string CardCountry { get; set; }

        [Display(Name = "CreditCard CardNumber Card_Number")]
        [MaxLength(16)]
        [NotMapped]
        public string CardNumber { get; set; }

        [Display(Name = "CreditCard Cvc CVC")]
        [MaxLength(4, ErrorMessageResourceName = "CreditCard Cvc 3 digits only")]
        [Required(ErrorMessageResourceName = "CreditCard Cvc Required")]
        public string Cvc { get; set; }

        [Range(1, 12, ErrorMessageResourceName = "CreditCard ExpirationMonth Invalid")]
        [Required(ErrorMessageResourceName = "CreditCard Cvc Required")]
        public string ExpirationMonth { get; set; }

        [Range(2015, 2030, ErrorMessageResourceName = "CreditCard ExpirationMonth Invalid")]
        [Required(ErrorMessageResourceName = "CreditCard Cvc Required")]
        public string ExpirationYear { get; set; }
       
        // Check same card
        public string Fingerprint { get; set; }

        public int Id { get; set; }

        //Gets or sets the last 4 digits of the credit card.
        public string Last4 { get; set; }

        [Display(Name = "CreditCard_Name_Name")]
        [Required(ErrorMessageResourceName = "CreditCard_Name_Please_enter_the_name_on_the_card_")]
        public string Name { get; set; }

        public string StripeId { get; set; }

        [NotMapped]
        public string StripeToken { get; set; }

        public string Type { get; set; }

    }
}