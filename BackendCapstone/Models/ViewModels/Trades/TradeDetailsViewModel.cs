using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCapstone.Models.ViewModels.Trades
{
    public class TradeDetailsViewModel
    {
        [Required]
        public int TradeId { get; set; }

        [Required]
        public Trade Trade { get; set; }

        public List<BarterTrade> AssociatedTrades { get; set; }

        public double TotalTradeValue
        {
            get
            {
                double _totalValue = 0;

                if (Trade.BarterTrades != null)
                {

                    foreach (var barter in Trade.BarterTrades)
                    {
                        _totalValue += barter.BarterItem.Value;
                    }

                }

                return _totalValue;
            }
        }

    }

}
