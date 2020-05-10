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

        public double? SenderValue
        {
            get
            {
                double _totalValue = 0;
                
                if (Trade.BarterTrades != null)
                {
                    foreach (var barter in Trade.BarterTrades.Where(i => i.BarterItem.AppUserId == Trade.SenderId))
                    {
                        _totalValue += barter.BarterItem.Value;
                    }
                }

                return _totalValue;
            }
        }
        public double? ReceiverValue
        {
            get
            {
                double _totalValue = 0;

                if (Trade.BarterTrades != null)
                {
                    foreach (var barter in Trade.BarterTrades.Where(i => i.BarterItem.AppUserId == Trade.ReceiverId))
                    {
                        _totalValue += barter.BarterItem.Value;
                    }
                }

                return _totalValue;
            }
        }
    }

}
