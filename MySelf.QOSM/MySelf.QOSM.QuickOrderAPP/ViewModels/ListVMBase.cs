using MySelf.QOSM.QuickOrderAPP.ViewModels.UControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MySelf.QOSM.QuickOrderAPP.ViewModels
{
    /// <summary>
    /// 列表页的视图模型
    /// </summary>
   public class ListVMBase:ViewModelBase
    {
        #region 属性
        private int keyWords;

        public int KeyWords
        {
            get { return keyWords; }
            set { keyWords = value;
                OnPropertyChanged();
            }
        }
        private UPagerViewModel pageInofVM;

        public UPagerViewModel PageInofVM
        {
            get { return pageInofVM; }
            set { pageInofVM = value; }
        }
        /// <summary>
        /// 已经删除
        /// </summary>
        public bool HasDeleted { get; set; }
        private bool showUnDel;
        /// <summary>
        /// 未删除选中状态
        /// </summary>
        public bool ShowUnDel
        {
            get { return showUnDel; }
            set { showUnDel = value; }
        }
        private bool showDeleted;
        /// <summary>
        /// 已删除选中状态
        /// </summary>
        public bool ShowDeleted
        {
            get { return showDeleted; }
            set { showDeleted = value; }
        }

        #endregion 属性
        #region 命令
        public ICommand FindDataListCmd { get; set; }
        public ICommand AddItemCmd { get; set; }
        public ICommand EditItemCmd { get; set; }
        public ICommand RecoverItemCmd { get; set; }
        public ICommand RemoveItemCmd { get; set; }
        public ICommand DelItemCmd { get; set; }

        #endregion
    }
}
