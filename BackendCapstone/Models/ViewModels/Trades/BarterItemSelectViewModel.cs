using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwapShop.Models.ViewModels.Trades
{
    public class BarterItemSelectViewModel
    {
        public BarterItem BarterItem { get; set; }
        public int BarterItemId { get; set; }
        public string ImagePath { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Value { get; set; }
        public int RequestedAmount { get; set; }
        public bool IsSelected { get; set; }
    }
}
