using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MySelf.QOSM.QuickOrderAPP.ViewModels
{
    public class ModelBase : ViewModelBase
    {
        private Visibility showEdit;
        public Visibility ShowEdit
        {
            get { return showEdit; }
            set
            {
                showEdit = value;
                OnPropertyChanged();
            }
        }
        private Visibility showDel;

        public Visibility ShowDel
        {
            get { return showDel; }
            set { 
                showDel = value;
                OnPropertyChanged();
            }
        }
        private Visibility showRecovery;

        public Visibility ShowRecovery
        {
            get { return showRecovery; }
            set { showRecovery = value;
                OnPropertyChanged();
            }
        }
        private Visibility showRemove;

        public Visibility ShowRemove
        {
            get { return showRemove; }
            set { showRemove = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// 按钮的显示值
        /// </summary>
        /// <param name="isDeleted"></param>
        /// <param name="unDel"></param>
        /// <returns></returns>
        protected Visibility GetVisibility(bool isDeleted, bool unDel)
        {
            if (unDel)
                return isDeleted ? Visibility.Collapsed : Visibility.Visible;
            else
                return isDeleted ? Visibility.Visible : Visibility.Collapsed;
        }

    }
}
