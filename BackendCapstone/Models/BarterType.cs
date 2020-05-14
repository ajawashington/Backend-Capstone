using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SwapShop.Models
{
    public class BarterType
    {
        [Key]
        public int BarterTypeId { get; set; }

        [Required]
        public string Title { get; set; }
        //food, goods, services, commissions 

           [NotMapped]
        public virtual ICollection<BarterItem> BarterItemsByType { get; set; }
    }
}
