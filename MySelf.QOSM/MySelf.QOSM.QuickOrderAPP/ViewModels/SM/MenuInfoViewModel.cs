using MySelf.QOSM.BLLFactory;
using MySelf.QOSM.IServices;
using MySelf.QOSM.Models.Entities;
using MySelf.QOSM.Models.UIModels;
using MySelf.QOSM.QuickOrderAPP.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MySelf.QOSM.QuickOrderAPP.ViewModels.SM
{
    public class MenuInfoViewModel:InfoVMBase<ViewMenuInfo>
    {
        IMenuService menuService = InstanceFactory.CreateInstance<IMenuService>();
        MenuInfo menuInfo = null;
        string oldName = "";
        public MenuInfoViewModel(int actType,ViewMenuInfo info)
        {
            ActType = actType;
            ShowMNameError = Visibility.Collapsed;
            LoadParentMenuList();//加载父菜单下拉框
            var iconList = CommonHelper.GetIconList();
            iconList.ForEach(icon => IconItems.Add(GetIconCode(icon)));//加载字体图标列表
            PageItems = CommonHelper.GetPageList();//加载页面集合下拉框
            if (actType == 1)//新增
            {
                menuInfo = new MenuInfo();
                IsRequired = true;
                ParentId = 0;
                SelIcon = IconItems[0];//选择第一个图标
                MenuPic = "";
                PageURL = "";
                SubmitText = "新增";
            }
            else//修改
            {
                menuInfo = new MenuInfo()
                {
                    MenuId = info.MenuId,
                    MenuName = info.MenuName,
                    ParentId = info.ParentId,
                    MenuPic = info.MenuPic,
                    MenuUrl = info.MenuUrl,
                    MenuOrder = info.MenuOrder,
                    IsSysMenu = info.IsSysMenu
                };
                if (PageURL != null && PageURL.Length > 0)
                {
                    PageInfo pInfo = PageItems.Find(p => p.PageURL.Contains(PageURL));
                    PageURL = pInfo == null ? "" : pInfo.PageURL;
                }
                MenuPic = string.IsNullOrEmpty(info.MenuPic) == true ? "" : GetIconCode(info.MenuPic);
                oldName = info.MenuName;
                SubmitText = "修改";
            }
            InitCommands();//命令的初始化
        }

        #region 属性
        private bool isRequired;

        public bool IsRequired
        {
            get { return isRequired; }
            set { isRequired = value; 
             OnPropertyChanged();
            }
        }
        private Visibility showMNameError;

        public Visibility ShowMNameError
        {
            get { return showMNameError; }
            set { showMNameError = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// 编号
        /// </summary>
        public int MenuId
        {
            get { return menuInfo.MenuId; }
            set
            {
                menuInfo.MenuId = value;
            }
        }
        //菜单名称
        public string MenuName
        {
            get { return menuInfo.MenuName; }
            set { menuInfo.MenuName = value; }
        }
        private List<CboMenu> pMenuList;

        public List<CboMenu> PMenuList
        {
            get { return pMenuList; }
            set { pMenuList = value;
                OnPropertyChanged();
            }
        }
        //父编号
        public int ParentId
        {
            get { return menuInfo.ParentId; }
            set
            {
                menuInfo.ParentId = value;
            }
        }
        private string  menuPic;

        public string MenuPic
        {
            get
            {
                if (!string.IsNullOrEmpty(menuInfo.MenuPic))
                {
                    menuPic = GetIconCode(menuInfo.MenuPic);
                }
                return menuPic;
            }
            set { 
                menuPic = value;
                if (!string.IsNullOrEmpty(value))
                {
                    string iconText = ((int)value.ToArray()[0]).ToString("x");
                    menuInfo.MenuPic = iconText;
                }
                OnPropertyChanged();    
            
            }
        }

        private List<string> iconItems = new List<string>();
        //图标列表
        public List<string> IconItems
        {
            get { return iconItems; }
            set { iconItems = value; }
        }
        //选择的图标
        public string SelIcon { get; set; }
        private List<PageInfo> pageItems = new List<PageInfo>();
        //页面集合
        public List<PageInfo> PageItems
        {
            get { return pageItems; }
            set { pageItems = value; }
        }
        //页面地址
        public string PageURL
        {
            get { return menuInfo.MenuUrl; }
            set { menuInfo.MenuUrl = value; }
        }
        //排序号
        public int MenuOrder
        {
            get { return menuInfo.MenuOrder; }
            set { menuInfo.MenuOrder = value; }
        }
        public bool IsSysMenu
        {
            get { return menuInfo.IsSysMenu; }
            set { menuInfo.IsSysMenu = value; }
        }
        #endregion
        #region 命令
          public ICommand SelectIconCmd { get; set; }


        #endregion
        #region 方法

        private void InitCommands()
        {
            //拖动窗口命令、关闭命令、提交命令、选择图标命令
            MoveWindowCmd = CreateMoveWindowCmd();
            CloseWindowCmd = CreateCloseWindowCmd();
            ConfirmCmd =new RelayCommand(win =>
            {
                SaveMenu(win);
            });
            SelectIconCmd = new RelayCommand(o =>
            {
                MenuPic = SelIcon;
            });
        }
        private void LoadParentMenuList()
        {
            List<CboMenu> list = menuService.GetCboMenus();
            list.Insert(0, new CboMenu()
            {
                MenuId = 0,
                MenuName = "请选择"
            });
            pMenuList = list;
        }
        private void SaveMenu(object win)
        {
            string conType = ActType == 1 ? "新增" : "修改";
            string msgTitle = $"{conType}菜单";
            if (string.IsNullOrEmpty(MenuName))
            {
                IsRequired = true;
                if(ShowMNameError == Visibility.Visible)
                {
                    ShowMNameError = Visibility.Collapsed;
                }
                return;
            }
            if(ActType == 1 ||(ActType == 2 && MenuName != oldName)) {
                if (menuService.ExistMenu(MenuName))
                {
                    ShowMNameError = Visibility.Visible;
                    return;
                }
            }
            bool lblConfirm = menuService.SaveMenu(menuInfo);
            if (lblConfirm) {
                ShowSuccess($"菜单：{MenuName} {conType}成功！", msgTitle);
                ViewMenuInfo viewMenuInfo = new ViewMenuInfo()
                {
                    MenuId = menuInfo.MenuId,
                    MenuName = menuInfo.MenuName,
                    ParentId = menuInfo.ParentId,
                    ParentName = PMenuList.Find(p=>p.MenuId == menuInfo.ParentId).MenuName,
                    MenuPic = menuInfo.MenuPic,
                    MenuOrder = menuInfo.MenuOrder,
                    MenuUrl = menuInfo.MenuUrl,
                    IsSysMenu = menuInfo.IsSysMenu,
                };
                OnReloadList(viewMenuInfo);
                CloseWindow(win);
            }
            else
            {
                ShowError($"菜单：{MenuName} {conType}失败", msgTitle);
                return;
            }
        }
        #endregion
    }

}
