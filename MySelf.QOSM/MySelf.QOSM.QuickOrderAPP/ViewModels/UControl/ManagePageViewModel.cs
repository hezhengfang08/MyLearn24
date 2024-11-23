using MySelf.QOSM.BLLFactory;
using MySelf.QOSM.IServices;
using MySelf.QOSM.Models.UIModels;
using MySelf.QOSM.QuickOrderAPP.Utils;
using MySelf.QOSM.QuickOrderAPP.ViewModels.Center;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MySelf.QOSM.QuickOrderAPP.ViewModels.UControl
{
    public class ManagePageViewModel : ViewModelBase
    {
        IMenuService menuService = InstanceFactory.CreateInstance<IMenuService>();
        public ManagePageViewModel()
        {
            int parentId = CommonHelper.selMainMenuId;//父菜单编号
            LoadSubMenus(parentId);//加载子菜单列表
            InitCommands();
        }

        public void LoadPage()
        {
            int parentId = CommonHelper.selMainMenuId;//父菜单编号
            LoadSubMenus(parentId);//加载子菜单列表
            InitCommands();
        }

        private void InitCommands()
        {
            SelectPageCmd = new RelayCommand(o =>
            {
                if (SelItem != null)
                {
                    if (!string.IsNullOrEmpty(SelItem.ItemURL) && selItem.ItemText != "修改密码")
                    {
                        SubPageURL = "../" + SelItem.ItemURL;
                    }
                    else if (selItem.ItemText == "修改密码")
                    {
                        ShowMidifyPwdWindow();
                    }
                }

            });
        }


        #region 属性
        private List<MainItemInfo> subMenus;
        //子菜单 列表
        public List<MainItemInfo> SubMenus
        {
            get { return subMenus; }
            set
            {
                subMenus = value;
                OnPropertyChanged();
            }
        }

        private MainItemInfo selItem;
        //选择的菜单项
        public MainItemInfo SelItem
        {
            get { return selItem; }
            set
            {
                selItem = value;
                OnPropertyChanged();
            }
        }

        private string subPageURL;
        //当前导航的页面地址
        public string SubPageURL
        {
            get { return subPageURL; }
            set
            {
                subPageURL = value;
                OnPropertyChanged();
            }
        }


        #endregion

        #region 命令
        //选择子菜单命令
        public ICommand SelectPageCmd { get; set; }
        #endregion

        #region 方法
        private void LoadSubMenus(int parentId)
        {
            List<UIMenu> menuList = menuService.GetRoleMenuList(CommonHelper.loginRoleId, parentId);
            List<MainItemInfo> subList = new List<MainItemInfo>();
            menuList.ForEach(m => subList.Add(new MainItemInfo()
            {
                ItemId = m.MenuId,
                ItemText = m.MenuName,
                ItemURL = m.MenuUrl
            }));
            SubMenus = subList;
            if (subList.Count > 0)
                SelItem = SubMenus[0];
            SubPageURL = "../" + SelItem.ItemURL;

        }

        private void ShowMidifyPwdWindow()
        {
            ModifyCustPwdViewModel modifyVM = new ModifyCustPwdViewModel();
            ShowDialog("modifyWindow", modifyVM);//显示密码修改窗口
            if (modifyVM.isModified)
            {
                //密码修改成功
                CustomerLoginViewModel custLoginVM = new CustomerLoginViewModel();
                custLoginVM.CustomerNo = CommonHelper.loginUser.LoginName;
                ShowDialog("custLogin", custLoginVM);//打开客户登录页
            }
        }

        #endregion
    }
}

