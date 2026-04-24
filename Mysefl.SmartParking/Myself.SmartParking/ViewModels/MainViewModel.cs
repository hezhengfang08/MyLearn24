using Microsoft.Win32;
using Myself.SmartParing.IService;
using Myself.SmartParking.Base;
using Myself.SmartParking.Entities;
using Myself.SmartParking.Models;
using Myself.SmartParking.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Myself.SmartParking.ViewModels
{
    public class MainViewModel : BindableBase
    {
        List<Entities.SysMenu> sysMenuList;
        public UserModel CurrentUser { get; set; } = new UserModel();

        public DelegateCommand ModifyPasswordCommand { get; set; }
        public DelegateCommand SwitchCommand { get; set; }
        public DelegateCommand<string> SetAvatarCommand { get; set; }
        public bool IsDropdownAvatar { get; set; }
        IRegionManager _regionManager;
        IDialogService _dialogService;
        IUserService _userService;
        private readonly IMenuService _menuService;
        public MainViewModel(
            IDialogService dialogService
            , IMenuService menuService
            , IRegionManager regionManager
            , IUserService userService
            , IEventAggregator eventAggregator)
        {
            _dialogService = dialogService;
            _regionManager = regionManager;
            _userService = userService;
            _menuService = menuService;
            // 打开登录窗口
            _dialogService.ShowDialog("LoginView", result =>
            {
                if (result.Result != ButtonResult.OK)
                {
                    System.Environment.Exit(0);
                }
                else 
                {
                    // 记录当前登录用户信息
                    var su = result.Parameters.GetValue<SysUser>("user");
                    CurrentUser.UserId = su.UserId;
                    CurrentUser.UserName = su.UserName;
                    CurrentUser.RealName = su.RealName;
                    CurrentUser.Password = su.Password;
                    CurrentUser.UserIcon = "pack://siteoforigin:,,,/Avatars/" + su.UserIcon;
                    CurrentUser.Gender = su.Gender;
                    CurrentUser.Address = su.Address;
                    CurrentUser.Age = su.Age;
                    CurrentUser.Status = su.Status;
                    CurrentUser.Phone = su.Phone;
                }
            });
            // 当前窗口要做的事
            OpenViewCommand = new DelegateCommand<MenuItemModel>(DoOpenView);
            CloseCommand = new DelegateCommand(() =>
            {
                System.Environment.Exit(0);
            });

            ModifyPasswordCommand = new DelegateCommand(DoModifyPassword);

            SwitchCommand = new DelegateCommand(DoSwitch);

            SetAvatarCommand = new DelegateCommand<string>(DoSetAvatar);
            // 加载菜单
            eventAggregator.GetEvent<RefreshMenuEvent>()
               .Subscribe(() =>
               {
                   LoadMenus();
               });
            // 加载菜单
            LoadMenus();
           
            FillMenus(Menus, 0);
        }
        private void LoadMenus()
        {
            Menus.Clear();
            // 获取所有菜单
            sysMenuList = _menuService.GetMenuList().ToList();
            // 树状填充
            FillMenus(Menus, 0);
        }
        #region 菜单相关功能

        public DelegateCommand<MenuItemModel> OpenViewCommand { get; set; }
        public DelegateCommand CloseCommand { get; set; }
        /// <summary>
        /// 菜单集合
        /// </summary>
        private ObservableCollection<MenuItemModel> _menus =
          new ObservableCollection<MenuItemModel>();
        public ObservableCollection<MenuItemModel> Menus
        {
            get { return _menus; }
            set { SetProperty(ref _menus, value); }
        }

        /// <summary>
        /// 递归查找子菜单
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="parent_id"></param>
        private void FillMenus(ObservableCollection<MenuItemModel> menus,
           int parent_id)
        {
            var sub = sysMenuList.Where(m => m.ParentId == parent_id)
                .OrderBy(o => o.Index)
                .ToList();

            if (sub.Count() > 0)
            {
                foreach (Entities.SysMenu item in sub)
                {
                    MenuItemModel model = new MenuItemModel
                    {
                        MenuHeader = item.MenuHeader,
                        MenuIcon = item.MenuIcon,
                        TargetView = item.TargetView
                    };
                    menus.Add(model);

                    FillMenus(model.Children, item.MenuId);
                }
            }
        }
        private void DoOpenView(MenuItemModel itemModel)
        {
            // 需要判断：双击的是父节点的时候，关闭或者打开；双击的是子节点，打开对应的页面
            if (itemModel.Children != null && itemModel.Children.Count > 0)
            {
                itemModel.IsExpanded = !itemModel.IsExpanded;

            }
            else if (!string.IsNullOrEmpty(itemModel.TargetView))
            {
                _regionManager.RequestNavigate("MainRegion", itemModel.TargetView);
            }
           
        }
        #endregion

        public void DoModifyPassword()
        {
            DialogParameters param = new DialogParameters();
            param.Add("uid", CurrentUser.UserId);
            param.Add("pwd", CurrentUser.Password);
            _dialogService.ShowDialog(
                "ModifyPasswordView",
                param,
                result =>
                {
                    // 密码修改完成后的回调逻辑？
                    // 如果修改完成
                    if (result.Result == ButtonResult.OK)
                    {
                        // 重启应用，重新登录
                        // 提示一下   是否立即重启，允许用户选择，不然直接重启会有些唐突
                        // 自己完善

                        //
                        //Process.Start("Zhaoxi.SmartParking.exe");
                        //System.Environment.Exit(0);
                        DoSwitch();
                    }
                });
        }
        public void DoSwitch()
        {
            // 切换用户
            // 
            Process.Start("Myself.SmartParking.exe");
            System.Environment.Exit(0);
        }
        private void DoSetAvatar(string avatar)
        {
            try
            {
                if (string.IsNullOrEmpty(avatar))
                {
                    // 打开选择文件窗口
                    OpenFileDialog dialog = new OpenFileDialog();
                    dialog.Filter = "*.jpg,*.png,*.jpeg|*.jpg;*.png;*.jpeg";
                    dialog.CheckFileExists = true;
                    if (dialog.ShowDialog() == true)
                    {
                        // 开发头像选择功能的时候，可能还需要做图像的裁切

                        // XXXXX.jpg
                        avatar = dialog.SafeFileName;//文件名称，不是路径
                        // 可能出现：两次选择了不同目录下的两个图像文件，这两个文件名称一致
                        // 但是在判断的时候，以是名称来时行判断的，导致后一次图像无法替代
                        // 如果需要解决：可以使用用户ID进行图像文件的重命名（复制的时候，以用用户ID作为图像文件的名称）
                        //　　　　 注意：二次修改的时候需要覆盖操作（涉及图像文件的占用问题）
                        // 如果需要解决文件占用问题的话，需要将图像进行内在读取，然后转ImageSource对象，提供给页面显示

                        // 复制到当前目录下
                        string target_path =
                             Path.Combine(System.Environment.CurrentDirectory, "Avatars", avatar);
                        if (!File.Exists(target_path))
                        {
                            File.Copy(dialog.FileName, target_path);
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                // avatar:文件名称

                var user = _userService.Find<SysUser>(CurrentUser.UserId);
                user.UserIcon = avatar;
                _userService.Update(user);

                CurrentUser.UserIcon = "pack://siteoforigin:,,,/Avatars/" + avatar;

                IsDropdownAvatar = false;
                this.RaisePropertyChanged(nameof(IsDropdownAvatar));
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
