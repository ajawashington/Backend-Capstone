using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SwapShop.Models
{
    public class Trade
    {
        [Key]
        public int TradeId { get; set; }

        [Required]
        public string ReceiverId { get; set; }
        public ApplicationUser Receiver { get; set; }

        [Required]
        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; }

        public string Message { get; set; }

        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Completed")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? DateCompleted { get; set; }

        public bool IsCompleted { get; set; }

        public bool Accepted { get; set; }

        public virtual ICollection<BarterTrade> BarterTrades { get; set; }
    }
}
