using Myself.PMS.Client.Common;
using Myself.PMS.Client.Entities;
using Myself.PMS.Client.IBLL;
using Myself.PMS.Client.SystemModule.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Myself.PMS.Client.SystemModule.ViewModels
{
    public class RoleViewModel: PageViewModelBase
    {
        public ObservableCollection<RoleModel> RoleList { get; set; } =
           new ObservableCollection<RoleModel>();
        private RoleModel _currentRole;
        public RoleModel CurrentRole
        {
            get { return _currentRole; }
            set
            {
                SetProperty<RoleModel>(ref _currentRole, value, () =>
                {
                    // 当属性值发生变化的时候
                    // value.MenuIds
                    if (value == null) return;

                    // 选择当前权限组时  刷新菜单数据
                    SetMenuStatus(Menus, value.MenuIds);
                    // 刷新用户数据
                    LoadUsers(value.UserIds);
                });
            }
        }
        public ObservableCollection<MenuModel> Menus { get; set; } =
           new ObservableCollection<MenuModel>();
        public ObservableCollection<UserModel> Users { get; set; } =
                    new ObservableCollection<UserModel>();

        public DelegateCommand<MenuModel> SelectMenuCommand { get; set; }
        public DelegateCommand SelectUserCommand { get; set; }
        public DelegateCommand<UserModel> DeleteUserCommand { get; set; }

        int _rid = 0;

        IEventAggregator _eventAggregator;
        IRoleService _roleService;
        IDialogService _dialogService;
        IMenuService _menuService;
        IUserService _userService;
        public RoleViewModel(IRegionManager regionManager,
            IEventAggregator eventAggregator,
            IRoleService roleService,
            IDialogService dialogService,
            IMenuService menuService,
            IUserService userService
            ) : base(regionManager,eventAggregator)
        {
            this.PageTitle = "角色权限组";

            _eventAggregator = eventAggregator;
            _roleService = roleService;
            _dialogService = dialogService;
            _menuService = menuService;
            _userService = userService;

            SelectMenuCommand = new DelegateCommand<MenuModel>(DoSelectMenu);
            SelectUserCommand = new DelegateCommand(DoSelcetUser);
            DeleteUserCommand = new DelegateCommand<UserModel>(DoDeleteUser);

            this.Refresh();
        }
        public override void Refresh()
        {
            RoleList.Clear();
            Menus.Clear();
           this.BeginLoading();

            Task.Run(() =>
            {
                try
                {
                    var rs = _roleService.GetAllRoles(this.SearchKey);

                    foreach (var role in rs)
                    {
                        var model = new RoleModel
                        {
                            RoleId = role.RoleId,
                            RoleName = role.RoleName,
                            RoleDesc = role.RoleDesc,

                            MenuIds = role.Menus.Select(m => m.MenuId.ToString()).ToList(),
                            UserIds = role.Users.Select(u => u.UserId).ToList(),
                        };

                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            RoleList.Add(model);
                        });

                    }
                    // 加载所有菜单   生成菜单 树
                    var ms = _menuService.GetAllMenus();
                    Application.Current.Dispatcher?.Invoke(() =>
                    {
                        FillMenus(Menus, null, ms.ToList());

                        CurrentRole = RoleList.Count > 0 ?
                           (_rid == 0 ? RoleList[0] : RoleList.FirstOrDefault(r => r.RoleId == _rid)) :
                            null;
                    });
                }
                catch { }
                finally
                {
                    this.EndLoading();  
                }
            });
        }
        private void FillMenus(ObservableCollection<MenuModel> menus,
            MenuModel parent, List<Entities.MenuEntity> origMenus, bool expandedNode = true)
        {
            var sub = origMenus.Where(m => m.ParentId == (parent == null ? "-1" : parent.MenuId))
                .OrderBy(o => o.MenuId)
                .ToList();

            if (sub.Count() > 0)
            {
                foreach (Entities.MenuEntity item in sub)
                {
                    MenuModel model = new MenuModel
                    {
                        MenuId = item.MenuId,
                        MenuHeader = item.MenuHeader,
                        MenuIcon = item.MenuIcon,
                        TargetView = item.TargetView,
                        ParentId = (parent == null ? "-1" : parent.MenuId),
                        IsExpanded = expandedNode,
                        Parent = parent
                    };
                    menus.Add(model);

                    FillMenus(model.Children, model, origMenus);
                }

                if (menus.Count > 0)
                {
                    menus[menus.Count - 1].IsLastChild = true;
                }
            }
        }
        public override void DoModify(object model)
        {
            DialogParameters ps = new DialogParameters();
            ps.Add("model", model);
            _dialogService.ShowDialog("ModifyRoleView", ps, result =>
            {
                // 判断子窗口的返回状态，如果OK，刷新当前页面，否则不管
                if (result.Result == ButtonResult.OK)
                {
                    this.Refresh();
                }
            });
        }
        public override void DoDelete(object model)
        {
            try
            {
                if (MessageBox.Show("是否确定删除此项？", "提示", MessageBoxButton.YesNo) ==
                    MessageBoxResult.Yes)
                {
                    // 物理删除
                    _roleService.DeleteRole((model as RoleModel).RoleId);

                    MessageBox.Show("删除完成！", "提示");

                    RoleList.Remove(model as RoleModel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }
        private void SetMenuStatus(ObservableCollection<MenuModel> menus, List<string> mids)
        {
            foreach (var item in menus)
            {
                item.IsSelected = mids.Contains(item.MenuId);

                if (item.Children != null && item.Children.Count > 0)
                {
                    SetMenuStatus(item.Children, mids);
                }
            }
        }
        private void LoadUsers(List<int> uids)
        {
            Users.Clear();
            var us = _userService.GetUsersByIds(uids.ToArray());
            foreach (var u in us)
            {
                Users.Add(new UserModel
                {
                    UserId = u.eId,
                    UserName = u.userName,
                    RealName = u.realName,
                    UserIcon = "https://localhost:7299/api/File/img/" + u.eIcon
                });
            }
        }
        private void DoSelectMenu(MenuModel model)
        {
            try
            {
                if (model.IsSelected)
                {
                    CurrentRole.MenuIds.Add(model.MenuId);
                    if (model.Children != null && model.Children.Count > 0)
                    {
                        foreach (var item in model.Children)
                        {
                            item.IsSelected = true;
                            CurrentRole.MenuIds.Add(item.MenuId);
                        }
                    }
                    if (model.Parent != null && !model.Parent.IsSelected)
                    {
                        model.Parent.IsSelected = true;
                        CurrentRole.MenuIds.Add(model.Parent.MenuId);
                    }
                }
                else
                {
                    CurrentRole.MenuIds.RemoveAll(mid => mid == model.MenuId);
                    if (model.Children != null && model.Children.Count > 0)
                    {
                        foreach (var item in model.Children)
                        {
                            // 遍历所有菜单子项，然后返回一个MenuId的集合，
                            // 过程中将所有Menu节点置为未选中
                            var mids = model.Children.Select(m =>
                            {
                                m.IsSelected = false;
                                return m.MenuId;
                            }).ToList();
                            CurrentRole.MenuIds.RemoveAll(m => mids.Contains(m));
                        }
                    }
                    if (model.Parent != null &&
                        !model.Parent.Children.ToList().Exists(m => m.IsSelected))
                    {
                        model.Parent.IsSelected = false;
                        CurrentRole.MenuIds.RemoveAll(m => m == model.Parent.MenuId);
                    }
                }

                // 根据最终的CurrentRole.MenuIds进行更新操作
                var rms = CurrentRole.MenuIds.Select(m => new RoleMenu
                {
                    RoleId = CurrentRole.RoleId,
                    MenuId = int.Parse(m)
                }).ToArray();
                _roleService.UpdateRoleMenus(rms);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }
        private void DoSelcetUser()
        {

            DialogParameters dps = new DialogParameters();
            dps.Add("uids", CurrentRole.UserIds);
            dps.Add("rid", CurrentRole.RoleId);
            _rid = CurrentRole.RoleId;
            _dialogService.ShowDialog("SelectUserView", dps, result =>
            {
                if (result.Result == ButtonResult.OK)
                {
                    this.Refresh();
                    _rid = 0;
                }
            });
        }

        private void DoDeleteUser(UserModel model)
        {
            try
            {
                if (MessageBox.Show("是否确定从当前角色权限组删除此用户？", "提示", MessageBoxButton.YesNo) ==
                    MessageBoxResult.Yes)
                {
                    // 物理删除
                    var role = _roleService.DeleteRoleUser(CurrentRole.RoleId, model.UserId);

                    MessageBox.Show("删除完成！", "提示");

                    Users.Remove(model);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }
    }
}
