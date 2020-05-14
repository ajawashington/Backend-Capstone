using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SwapShop.Models.ViewModels.Trades
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

                        if (item.RequestedAmount > 0)
                        {

                            _totalValue += item.Value * item.RequestedAmount;

                        }
                        else
                        {
                            _totalValue += item.Value;

                        }

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
                        if (item.RequestedAmount > 0)
                        {

                        _totalValue += item.Value * item.RequestedAmount;

                        }
                        else
                        {
                            _totalValue += item.Value;

                        }

                    }
                }

                return _totalValue;
            }
        }
    }
}
