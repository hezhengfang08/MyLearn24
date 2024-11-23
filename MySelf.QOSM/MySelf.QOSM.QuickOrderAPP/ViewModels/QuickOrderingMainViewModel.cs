using HandyControl.Controls;
using MySelf.QOSM.BLLFactory;
using MySelf.QOSM.IServices;
using MySelf.QOSM.Models.UIModels;
using MySelf.QOSM.QuickOrderAPP.Utils;
using MySelf.QOSM.QuickOrderAPP.ViewModels.UControl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace MySelf.QOSM.QuickOrderAPP.ViewModels
{
    public class QuickOrderingMainViewModel : ViewModelBase
    {
        IMenuService menuService = InstanceFactory.CreateInstance<IMenuService>();
        int loginType = 2;//1 客户登录  2 后台登录
        public QuickOrderingMainViewModel()
        {
            if (loginType == 1)
            {
                ShowCustLoginBtn = Visibility.Visible;
                ShowAdminLoginBtn = Visibility.Collapsed;
                CommonHelper.existLogin = ExitLogin;
            }
            else
            {
                //后台登录
                ShowCustLoginBtn = Visibility.Collapsed;
                ShowAdminLoginBtn = Visibility.Visible;

            }
            ShowWelcome = Visibility.Collapsed;

            //默认导航条加载
            LoadMenuList();//加载导航菜单
            //命令初始化
            InitCommands();
            //设置默认页
            CurrentPageURL = "Food/FoodOrderListPage.xaml";
        }

        private void ExitLogin()
        {
            ShowCustLoginBtn = Visibility.Visible;
            ShowAdminLoginBtn = Visibility.Collapsed;
            ShowWelcome = Visibility.Collapsed;
            LoadMenuList();//登录后，加载默认导航
            CurrentPageURL = "Food/FoodOrderListPage.xaml";
        }

        ManagePageViewModel managePageViewModel;
        private void InitCommands()
        {
            //拖动
            MoveWindowCmd = new RelayCommand(win =>
            {
                System.Windows.Window window = win as System.Windows.Window;
                if (window != null)
                {
                    if (window.WindowState == WindowState.Maximized)
                    {
                        window.WindowState = WindowState.Normal;
                        window.Top = 10;
                        MaxBtnText = "1";
                    }
                    if (Mouse.LeftButton == MouseButtonState.Pressed)
                    {
                        window.DragMove();
                    }

                }
            });
            //最小化
            MinWindowCmd = CreateMinWindowCmd();
            //最大化与正常状态切换
            MaxWindowCmd = new RelayCommand(win =>
            {
                System.Windows.Window window = win as System.Windows.Window;
                if (window != null)
                {
                    if (window.WindowState == WindowState.Maximized)
                    {
                        window.WindowState = WindowState.Normal;
                        MaxBtnText = "1";
                    }
                    else if (window.WindowState == WindowState.Normal)
                    {
                        window.WindowState = WindowState.Maximized;
                        MaxBtnText = "2";
                    }
                }
            });
            //主窗口关闭
            CloseWindowCmd = new RelayCommand(win =>
            {
                //主窗口----关闭----退出程序
                if (ShowQuestion("你确定要退出系统吗？", "退出提示") == MessageBoxResult.OK)
                {
                    Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                    Application.Current.Shutdown();//关闭应用程序
                }
            });
            //登录命令
            LoginCmd = new RelayCommand(o =>
            {
                LoginSystem();
            });

            //导航项选择命令
            SelectItemCmd = new RelayCommand(o =>
            {
                if (SelectMenu == null)
                    SelectMenu = NavMenus[0];
                if (string.IsNullOrEmpty(SelectMenu.ItemURL))
                {
                    IsShowManage = true;//显示MangePage
                    CommonHelper.selMainMenuId = SelectMenu.ItemId;//保存选择的导航项
                    //显示ManagePage  ---提供ViewModel
                    if (managePageViewModel == null)
                        managePageViewModel = new ManagePageViewModel();
                    else
                        managePageViewModel.LoadPage();
                    CurrentPageObj = managePageViewModel;
                    CurrentPageURL = null;
                }
                else
                {
                    IsShowManage = false;
                    CurrentPageObj = null;
                    CurrentPageURL = SelectMenu.ItemURL;
                }
            });

            SetWindowSizeCmd = new RelayCommand(content =>
            {
                var c = content as UIElement;
                var layer = AdornerLayer.GetAdornerLayer(c);
                layer.Add(new WindowResizeAdorner(c));
            });

            IconClickCmd = new RelayCommand(win =>
            {
                if (WinState == System.Windows.WindowState.Minimized)
                {
                    WinState = System.Windows.WindowState.Normal;
                }
            });

            ChangIconBlinkCmd = new RelayCommand(o =>
            {
                if (WinState == WindowState.Minimized)
                {
                    IconBlink = true;//闪烁
                }
                else
                {
                    IconBlink = false;//不闪烁
                }
            });
        }

        #region 属性
        private string loginName;
        //登录名
        public string LoginName
        {
            get { return loginName; }
            set
            {
                loginName = value;
                OnPropertyChanged();
            }
        }

        private Visibility showWelcome;
        //显示登录信息
        public Visibility ShowWelcome
        {
            get { return showWelcome; }
            set
            {
                showWelcome = value;
                OnPropertyChanged();
            }
        }

        private Visibility showCustLoginBtn;
        //显示客户登录按钮
        public Visibility ShowCustLoginBtn
        {
            get { return showCustLoginBtn; }
            set
            {
                showCustLoginBtn = value;
                OnPropertyChanged();
            }
        }

        private Visibility showAdminLoginBtn;
        //显示后台登录按钮
        public Visibility ShowAdminLoginBtn
        {
            get { return showAdminLoginBtn; }
            set
            {
                showAdminLoginBtn = value;
                OnPropertyChanged();
            }
        }

        private string maxBtnText = "1";
        //最大化按钮文本
        public string MaxBtnText
        {
            get { return maxBtnText; }
            set
            {
                maxBtnText = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MainItemInfo> navMenus;
        //主导航列表
        public ObservableCollection<MainItemInfo> NavMenus
        {
            get { return navMenus; }
            set
            {
                navMenus = value;
                OnPropertyChanged();
            }
        }

        private MainItemInfo selectMenu;
        //选择的菜单项
        public MainItemInfo SelectMenu
        {
            get { return selectMenu; }
            set
            {
                selectMenu = value;
                OnPropertyChanged();
            }
        }

        private bool isShowManage;
        //是否显示管理控件--ManagePage
        public bool IsShowManage
        {
            get { return isShowManage; }
            set
            {
                isShowManage = value;
                OnPropertyChanged();
            }
        }

        private ManagePageViewModel currentPageObj;
        //管理控件中显示的当前页面对象
        public ManagePageViewModel CurrentPageObj
        {
            get { return currentPageObj; }
            set
            {
                currentPageObj = value;
                OnPropertyChanged();
            }
        }

        private string currentPageURL;
        //当前页面地址
        public string CurrentPageURL
        {
            get { return currentPageURL; }
            set
            {
                currentPageURL = value;
                OnPropertyChanged();
            }
        }

        private WindowState winState;
        //Window的状态
        public WindowState WinState
        {
            get { return winState; }
            set
            {
                winState = value;
                OnPropertyChanged();
            }
        }

        private bool iconBlink;
        //托盘图标的闪烁状态
        public bool IconBlink
        {
            get { return iconBlink; }
            set
            {
                iconBlink = value;
                OnPropertyChanged();
            }
        }


        #endregion

        #region 命令
        //最大化、正常切换命令
        public ICommand MaxWindowCmd { get; set; }
        //登录命令
        public ICommand LoginCmd { get; set; }
        //导航项选择命令
        public ICommand SelectItemCmd { get; set; }
        //界面尺寸调整命令
        public ICommand SetWindowSizeCmd { get; set; }
        //点击托盘图标响应命令
        public ICommand IconClickCmd { get; set; }
        //改变托盘图标的闪烁命令
        public ICommand ChangIconBlinkCmd { get; set; }
        #endregion

        #region 方法
        //显示登录及登录成功后处理
        private void LoginSystem()
        {
            bool isLoginSuccess = false;
            if (loginType == 1)
            {
                CustomerLoginViewModel custVM = new CustomerLoginViewModel();
                //显示客户登录页
                ShowDialog("custLogin", custVM);
                if (custVM.CustomerId > 0)
                {
                    //登录成功
                    Growl.Success(new HandyControl.Data.GrowlInfo()
                    {
                        Message = "客户登录成功！",
                        ShowCloseButton = true,
                        Token = "SucMes",
                        ShowDateTime = true
                    });
                    LoginName = CommonHelper.loginUser.LoginName;
                    ShowCustLoginBtn = Visibility.Collapsed;
                    isLoginSuccess = true;
                }
            }
            else//后台登录
            {
                AdminLoginViewModel adminVM = new AdminLoginViewModel();
                //显示后台登录页
                ShowDialog("adminLogin", adminVM);
                if (adminVM.UserId > 0)
                {
                    //登录成功---显示登录成功提示消息
                    Growl.Success(new HandyControl.Data.GrowlInfo()
                    {
                        Message = "后台用户登录成功！",
                        ShowCloseButton = true,
                        Token = "SucMes",
                        ShowDateTime = true
                    });
                    LoginName = CommonHelper.loginUser.LoginName;
                    ShowAdminLoginBtn = Visibility.Collapsed;
                    isLoginSuccess = true;
                }
            }
            if (isLoginSuccess)
            {
                ShowWelcome = Visibility.Visible;
                LoadMenuList();//登录成功后重新加载导航列表
            }

        }

        //加载导航菜单（未登录还是登录后）
        private void LoadMenuList()
        {
            ObservableCollection<MainItemInfo> menuList = new ObservableCollection<MainItemInfo>();
            List<UIMenu> mList = new List<UIMenu>();
            if (CommonHelper.loginRoleId == 0)//未登录状态
            {
                mList = menuService.GetRoleMenuList(0, 0);//---只有一个一级导航项
            }
            else
            {
                mList = menuService.GetRoleMenuList(CommonHelper.loginRoleId, 0);//一级导航列表：1客户、不是1--后台用户
            }
            mList.ForEach(m => menuList.Add(new MainItemInfo()
            {
                ItemId = m.MenuId,
                IconCode = GetIconCode(m.MenuPic),
                ItemText = m.MenuName,
                ItemURL = m.MenuUrl
            }));
            NavMenus = menuList;
            if (CommonHelper.loginRoleId == 0 && NavMenus!=null && NavMenus.Count >0)
                SelectMenu = NavMenus[0];
        }
        #endregion
    }
}

