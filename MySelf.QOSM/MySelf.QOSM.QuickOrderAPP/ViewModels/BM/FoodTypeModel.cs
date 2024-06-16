using MySelf.QOSM.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.QOSM.QuickOrderAPP.ViewModels.BM
{
   public class FoodTypeModel:ModelBase
    {

        public FoodTypeModel() { }
        public FoodTypeModel(FoodTypeInfo info)
        {
            FType = info;
            ShowDel = GetVisibility(info.IsDeleted, true);
            ShowEdit = GetVisibility(info.IsDeleted, true);
            ShowRecovery = GetVisibility(info.IsDeleted, false);
            ShowRemove = GetVisibility(info.IsDeleted, false);
        }

        private FoodTypeInfo fType;
        //菜品类别信息
        public FoodTypeInfo FType
        {
            get { return fType; }
            set
            {
                fType = value;
                OnPropertyChanged();
            }
        }

    }
}
