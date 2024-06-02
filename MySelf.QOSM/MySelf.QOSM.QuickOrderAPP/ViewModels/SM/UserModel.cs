using MySelf.QOSM.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MySelf.QOSM.QuickOrderAPP.ViewModels.SM
{
    public class UserModel : ModelBase
    {
        public UserModel() { }
        public UserModel(ViewUserInfo viewUserInfo)
        {
            User = viewUserInfo;
            IsDel= viewUserInfo.IsAdmin?false:true;
            EnableText = viewUserInfo.UserState? "停用" : "启用";
            ShowEnable = (viewUserInfo.IsDeleted||viewUserInfo.IsAdmin)?Visibility.Collapsed:Visibility.Visible;
            ShowDel = GetVisibility(viewUserInfo.IsDeleted, true);
            ShowEdit = GetVisibility(viewUserInfo.IsDeleted, true);
            ShowRecovery = GetVisibility(viewUserInfo.IsDeleted, false);
            ShowRemove = GetVisibility(viewUserInfo.IsDeleted, false);
        }
        private ViewUserInfo user;

        public ViewUserInfo User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged();
            }
        }
        private Visibility showEnable;

        public Visibility ShowEnable
        {
            get { return showEnable; }
            set { showEnable = value;
                OnPropertyChanged();
            }
        }
        private string enableText ;

        public string EnableText
        {
            get { return enableText ; }
            set { enableText  = value; }
        }

        private bool isDel;

        public bool IsDel
        {
            get { return isDel; }
            set { isDel = value; }
        }


    }
}
