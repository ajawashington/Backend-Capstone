using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SwapShop.Models.ViewModels.Trades
{
    public class TradeWithItemsViewModel
    {

        [Required]
        public int TradeId { get; set; }

        [Required]
        public Trade Trade { get; set; }

        public List<BarterItemSelectViewModel> SenderSelectedItems { get; set; }

        public List<BarterItemSelectViewModel> ReceiverSelectedItems { get; set; }



    }
}
