using MySelf.QOSM.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.QOSM.QuickOrderAPP.ViewModels.BM
{
    public class FoodModel:ModelBase
    {
        public FoodModel() { }
        public FoodModel(ViewFoodInfo info)
        {
            Food = info;
            IsDel = info.IsOn ? false : true;
            ShowDel = GetVisibility(info.IsDeleted, true);
            ShowEdit = GetVisibility(info.IsDeleted, true);
            ShowRecovery = GetVisibility(info.IsDeleted, false);
            ShowRemove = GetVisibility(info.IsDeleted, false);
        }

        private ViewFoodInfo food;
        //菜品信息
        public ViewFoodInfo Food
        {
            get { return food; }
            set
            {
                food = value;
                OnPropertyChanged();
            }
        }

        private bool isCheck;
        //行选中状态
        public bool IsCheck
        {
            get { return isCheck; }
            set
            {
                isCheck = value;
                OnPropertyChanged();
            }
        }

        public bool IsOn
        {
            get { return Food.IsOn; }
            set
            {
                Food.IsOn = value;
                OnPropertyChanged();
            }
        }

        private bool isDel;
        //是否可删除
        public bool IsDel
        {
            get { return isDel; }
            set
            {
                isDel = value;
                OnPropertyChanged();
            }
        }


    }
}
