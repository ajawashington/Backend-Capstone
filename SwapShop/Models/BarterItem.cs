using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SwapShop.Models
{
    public class BarterItem
    {
        [Key]
        public int BarterItemId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Barter Type")]
        public int BarterTypeId { get; set; }

        [Display(Name = "Barter Type")]
        public BarterType BarterType { get; set; }

        [Required]
        public string AppUserId { get; set; }

        public ApplicationUser AppUser { get; set; }

        [Display(Name = " ")]
        public string ImagePath { get; set; }

        [NotMapped]
        //would be better to have a viewmodel instead of notMapped prop
        public IFormFile ImageFile { get; set; }

        [Required]
        [Range(1, 3, ErrorMessage = "Value must be between 1 - 3")]
        [Display(Name = "Value" , Description = "1 - abundant | 2 - balanced | 3 - scarce")]
        public int Value { get; set; }
        //"scarce" (4-5) or "abundant (1-3)"

        [Required]
        public int Quantity { get; set; }

        [Display(Name = "Is Available")]
        public bool IsAvailable { get; set; }

        public virtual ICollection<BarterTrade> AssociatedTrades { get; set; }
    }
}
