using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SwapShop.Models.ViewModels.Trades
{
    public class TradeDetailsViewModel
    {
        [Required]
        public int TradeId { get; set; }

        [Required]
        public Trade Trade { get; set; }

        public List<BarterTrade> AssociatedTrades { get; set; }

    }

}
