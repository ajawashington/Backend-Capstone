using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SwapShop.Models
{
    public class ApplicationUser : IdentityUser
    {

        [Required]
        [Display (Name = "Username") ]
        public string TagName { get; set; }

        [Required]
        public string Location { get; set; }

        [Display(Name = " ")]
        public string ImagePath { get; set; }

        [NotMapped]
        //would be better to have a viewmodel instead NotMapped prop
        public IFormFile ImageFile { get; set; }

     
       public virtual ICollection<BarterItem> MyBarterItems { get; set; }

        [Display(Name = "Received Trades")]
        public virtual ICollection<Trade> ReceivedTrades { get; set; }

        [Display(Name = "Sent Trades")]
        public virtual ICollection<Trade> SentTrades { get; set; }

        [Display(Name = "Completed Trades")]
        public virtual ICollection<Trade> CompletedTrades { get; set; }
    }
}
