using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCapstone.Models
{
    public class BarterTrade
    {
        [Key]
        public int BarterTradeId { get; set; }

        [Required]
        public int BarterItemId { get; set; }

        [Required]
        public BarterItem BarterItem { get; set; }

        [Required]
        public int TradeId { get; set; }

        [Required]
        public Trade Trade { get; set; }

    }
}
