using MySelf.QOSM.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.QOSM.QuickOrderAPP.ViewModels.SM
{
    public class MenuModel:ModelBase
    {

        public MenuModel() { }  
        public MenuModel(ViewMenuInfo menuInfo)
        {
            Menu = menuInfo;
            IsDel = menuInfo.IsSysMenu ? false : true;
            MenuIconCode = (string.IsNullOrEmpty(menuInfo.MenuPic)) ? "" : GetIconCode(menuInfo.MenuPic);
            ShowDel = GetVisibility(menuInfo.IsDeleted, true);
            ShowEdit = GetVisibility(menuInfo.IsDeleted, true);
            ShowRecovery = GetVisibility(menuInfo.IsDeleted, false);
            ShowRemove = GetVisibility(menuInfo.IsDeleted, false);
        }
        #region 属性

        private ViewMenuInfo menu;  

        public ViewMenuInfo Menu
        {
            get { return menu; }
            set { menu = value; 
             OnPropertyChanged();
            }
        }

        private bool isDel;

        public bool IsDel
        {
            get { return isDel; }
            set
            {
                isDel = value;
                OnPropertyChanged();
            }
        }
        private string  menuIconCode;

        public string MenuIconCode
        {
            get { return menuIconCode; }
            set { menuIconCode = value;
                OnPropertyChanged();
            }
        }

        #endregion
        #region 方法


        #endregion
    }
}
