using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCapstone.Models.ViewModels.Trades
{
    public class TradeValueViewModel
    {
        public TradeDetailsViewModel Details { get; set; }

        public TradeWithItemsViewModel Items { get; set; }

        public double? SenderValue
        {
            get
            {
                double _totalValue = 0;

                var selectedItems = Items.SelectedItems.Where(i => i.IsSelected == true);

                if (selectedItems != null && Details.Trade.Sender == Items.Trade.Sender)
                {
                    foreach (var item in selectedItems)
                    {
                        _totalValue += item.Value * item.RequestedAmount;

                        //selected.RequestedAmount; how can I get this property 
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

                var selectedItems = Items.SelectedItems.Where(i => i.IsSelected == true);

                if (selectedItems != null && Details.Trade.Receiver == Items.Trade.Receiver)
                {
                    foreach (var item in selectedItems)
                    {
                        _totalValue += item.Value * item.RequestedAmount;

                        //selected.RequestedAmount; how can I get this property 
                    }
                }

                return _totalValue;
            }
        }
    }
}
