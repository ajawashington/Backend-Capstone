using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SwapShop.Models.ViewModels.Profile
{
    public class ProfileFormViewModel
    {

            [Key]
            public string AppUserId { get; set; }


            [Display(Name = "TagName")]
            public string TagName { get; set; }


            [Display(Name = "Location")]
            public string Location { get; set; }


            [Display(Name = " ")]
            public string ImagePath { get; set; }

            public IFormFile ImageFile { get; set; }


    }
    }
