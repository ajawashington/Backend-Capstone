using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCapstone.Models
{
    public class ApplicationUser : IdentityUser
    {

        public ApplicationUser() 
        {
        
        }

        [Required]
        [Display (Name = "Username") ]
        public string TagName { get; set; }

        [Required]
        public string Location { get; set; }

        [Display(Name = " ")]
        public string ImagePath { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

     
       public virtual ICollection<BarterItem> MyBarterItems { get; set; }

        [NotMapped]
        public virtual ICollection<Trade> ReceivedTrades { get; set; }

        [NotMapped]
        public virtual ICollection<Trade> SentTrades { get; set; }
    }
}
