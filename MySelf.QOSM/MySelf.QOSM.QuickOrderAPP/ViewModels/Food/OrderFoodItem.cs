using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.QOSM.QuickOrderAPP.ViewModels.Food
{
   public class OrderFoodItem
    {
        public int FoodId { get; set; }
        public string FoodName { get; set; }
        public int Count { get; set; }
        public decimal Amount { get; set; }
        public decimal PackAmounts { get; set; }
        public string OrderRemark { get; set; }
    }
}

