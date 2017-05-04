using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImpactWebsite.Models
{
    public class NewsLetterUser : BaseEntity
    {
        public Int64 NewsLetterUserId { get; set; }
        public string Email { get; set; }
        public bool isSubscribed { get; set; }
    }
}
