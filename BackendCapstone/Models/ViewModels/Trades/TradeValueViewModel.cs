﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCapstone.Models.ViewModels.Trades
{
    public class TradeValueViewModel
    {

        public Trade Trade { get; set; }
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