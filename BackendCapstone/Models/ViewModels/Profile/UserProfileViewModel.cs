using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCapstone.Models.ViewModels.Profile
{
    public class UserProfileViewModel
    {
        public string AppUserId { get; set; }
        public ApplicationUser User { get; set; }

        [Display(Name = "Barter Items")]
        public List<BarterItem> BarterItems { get; set; }
    }
}
