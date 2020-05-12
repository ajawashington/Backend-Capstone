using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCapstone.Models.ViewModels.Trades
{
    public class TradeValueViewModel
    {

        [Required]
        public int TradeId { get; set; }

        [Required]
        public Trade Trade { get; set; }

        public TradeWithItemsViewModel Items { get; set; }

        public double? SenderValue
        {
            get
            {
                double _totalValue = 0;

                var selectedItems = Items.SenderSelectedItems;

                if (selectedItems != null)
                {
                    foreach (var item in selectedItems)
                    {
                        _totalValue += item.Value * item.RequestedAmount;

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

                var selectedItems = Items.ReceiverSelectedItems;

                if (selectedItems != null)
                {
                    foreach (var item in selectedItems)
                    {
                        _totalValue += item.Value * item.RequestedAmount;

                    }
                }

                return _totalValue;
            }
        }
    }
}
