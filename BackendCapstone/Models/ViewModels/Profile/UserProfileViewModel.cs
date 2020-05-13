using Microsoft.EntityFrameworkCore.Storage;
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

        [Display(Name = "Received Trades")]
        public List<Trade> ReceivedTrades { get; set; }

        [Display(Name = "Sent Trades")]
        public List<Trade> SentTrades { get; set; }

        [Display(Name = "Completed Trades")]
        public List<Trade> CompletedTrades { get; set; }
    }
}
