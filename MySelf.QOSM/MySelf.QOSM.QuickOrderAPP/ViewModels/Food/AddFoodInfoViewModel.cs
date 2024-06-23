using MySelf.QOSM.QuickOrderAPP.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MySelf.QOSM.QuickOrderAPP.ViewModels.Food
{
   public class AddFoodInfoViewModel:ViewModelBase
    {
        int actType = 1;

        public AddFoodInfoViewModel(string foodName,string orderRemark,int actType)
        {
            this.actType = actType; 
            FoodName = foodName;
            OrderRemark = orderRemark;
            InitCommands();
        }
        private void InitCommands()
        {
            MoveWindowCmd = CreateMoveWindowCmd();
            CloseWindowCmd = CreateCloseWindowCmd();
            ConfirmCmd = new RelayCommand(win =>
            {
                if (string.IsNullOrEmpty(OrderRemark))
                    OrderRemark = "无";
                CloseWindow(win);
            });
        }
        #region 属性

        public string FoodName { get; set; }
        public string OrderRemark { get; set;}
        #endregion

        #region 命令

        public ICommand ConfirmCmd { get; set; }
        #endregion
    }
}
