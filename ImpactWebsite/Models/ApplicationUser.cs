using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using ImpactWebsite.Models.OrderModels;

namespace ImpactWebsite.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        //Auto generated
        //public Int64 UserId { get; set; }

        [Required]
        [StringLength(160, MinimumLength = 1)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(160, MinimumLength = 1)]
        public string LastName { get; set; }

        [StringLength(160, MinimumLength = 2)]
        public string CompanyName { get; set; }

        public bool NewsletterRequired { get; set; }
        
        public List<OrderHeader> Orders { get; set; }
    }
}
