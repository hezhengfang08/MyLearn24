using MySelf.QOSM.Models.Entities;
using MySelf.QOSM.QuickOrderAPP.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MySelf.QOSM.QuickOrderAPP.ViewModels.SM
{
  public  class TreeMenuItem:ViewModelBase
    {
        public TreeMenuItem()
        {
            SubItems = new List<TreeMenuItem>();
            CheckItemCmd = new RelayCommand(o =>
            {
                SetItemChecked(IsCheck, true, true);
            });
        }
        private MenuInfo menuInfo = new MenuInfo();
        //编号
        public int MenuId
        {
            get { return menuInfo.MenuId; }
            set { menuInfo.MenuId = value; }
        }
        //节点名称（菜单名）
        public string MenuName
        {
            get { return menuInfo.MenuName; }
            set { menuInfo.MenuName = value; }
        }

        private bool isExpand = true;
        //节点是否展开
        public bool IsExpand
        {
            get { return isExpand; }
            set
            {
                isExpand = value;
                OnPropertyChanged();
            }
        }

        private TreeMenuItem parentNode = null;
        //父节点
        public TreeMenuItem ParentNode
        {
            get { return parentNode; }
            set { parentNode = value; }
        }

        private List<TreeMenuItem> subItems = null;
        //子节点列表
        public List<TreeMenuItem> SubItems
        {
            get { return subItems; }
            set { subItems = value; }
        }

        private bool? isCheck = false;
        //选中状态
        public bool? IsCheck
        {
            get { return isCheck; }
            set
            {
                isCheck = value;
                OnPropertyChanged();
            }
        }

        //节点勾选命令
        public ICommand CheckItemCmd { get; set; }

        #region 方法
        //当前节点状态处理---父节点、子节点处理
        private void SetItemChecked(bool? chkValue, bool updateChildren, bool updateParent)
        {
            isCheck = chkValue;
            if (updateChildren && isCheck.HasValue && SubItems.Count > 0)
            {
                //设置当前节点的子节点的勾选
                foreach (var subItem in SubItems)
                {
                    subItem.SetItemChecked(isCheck, true, false);
                }
            }
            if (updateParent && ParentNode != null)
            {
                //有父节点，设置父节点勾选
                ParentNode.SetParentNodeChecked();
            }
            OnPropertyChanged("IsCheck");
        }

        //设置父节点勾选:如果子节点没有全部选中，父节点就是中间态，如果全部选中，就选中，如果都没选中，就不选中
        private void SetParentNodeChecked()
        {
            bool? state = false;
            int count = 0;//子节点选中的数目
            for (int i = 0; i < SubItems.Count; i++)
            {
                bool? subState = SubItems[i].IsCheck;
                if (subState != null && subState.Value == true)//选中
                {
                    count++;
                }
            }
            if (count == SubItems.Count)
                state = true;//全部选中
            else if (count > 0)
                state = null;//部分选中
            else
                state = false;//全部没选
            SetItemChecked(state, false, true);
        }

        #endregion

    }
}
