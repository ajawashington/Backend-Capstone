using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Threading.Tasks;

namespace SwapShop.Models.ViewModels.BarterItems
{
    public class BarterItemFormViewModel
    {

  
        public int BarterItemId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }


        [Required]
        public string AppUserId { get; set; }

        public ApplicationUser AppUser { get; set; }

        [Display(Name = " ")]
        public string ImagePath { get; set; }


        public IFormFile ImageFile { get; set; }


        [Range(1, 3, ErrorMessage = "Value must be between 1 - 3")]
        [Display(Name = "Value", Description = "1 - abundant | 2 - balanced | 3 - scarce")]
        public int Value { get; set; }


        [Required]
        public int Quantity { get; set; }

        [Display(Name = "Is Available")]
        public bool IsAvailable { get; set; }

        [Required]
        [Display(Name = "Barter Type")]
        public int BarterTypeId { get; set; }

        public BarterType BarterType { get; set; }
        public List<SelectListItem> BarterTypeOptions { get; set; }
        public virtual ICollection<BarterTrade> AssociatedTrades { get; set; }

        public BarterItemFormViewModel()
        {
            IsAvailable = true;
        }



    }
}
