using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.QOSM.Models.UIModels
{
    public class UIFoodItem
    {
        public int FoodId { get; set; }
        public string FoodName { get; set; }
        public int FTypeId { get; set; }
        public decimal FoodPrice { get; set; }
        public decimal FoodAmount { get; set; }
        public string FoodImg { get; set; }

    }
}
