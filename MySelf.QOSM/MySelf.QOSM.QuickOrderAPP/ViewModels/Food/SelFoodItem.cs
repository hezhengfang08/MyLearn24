using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.QOSM.QuickOrderAPP.ViewModels.Food
{
    public class SelFoodItem:ViewModelBase
    {
        public int FoodId { get; set; }
        public string FoodName { get; set; }
        private string foodRemark;
        public string FoodRemark
        {
            get { return foodRemark; }
            set
            {
                foodRemark = value;
                OnPropertyChanged();
            }

        }
        public decimal FoodPrice { get; set; }
        public decimal PackAmount { get; set; }
        private int selCount;
        //点餐份数
        public int SelCount
        {
            get { return selCount; }
            set
            {
                selCount = value;
                OnPropertyChanged();
            }
        }

    }
}
