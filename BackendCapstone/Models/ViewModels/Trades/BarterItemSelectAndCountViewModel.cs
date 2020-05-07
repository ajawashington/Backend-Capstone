using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCapstone.Models.ViewModels.Trades
{
    public class BarterItemSelectViewModel
    {

        public string ImagePath { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Value { get; set; }
        public bool IsSelected { get; set; }


        //adding the items 
        //image path 
        //Title
        //Description 
        //Location 
        //Value
        //IsSelected bool 
    }
}
