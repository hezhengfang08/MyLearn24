
using Myself.SmartParing.IService;
using Myself.SmartParking.Base;
using Myself.SmartParking.Entities;
using Myself.SmartParking.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Myself.SmartParking.ViewModels.Pages
{
    public class RoleViewModel : ViewModelBase<RoleModel>
    {
        public ObservableCollection<RoleModel> RoleList { get; set; } =
           new ObservableCollection<RoleModel>();
        public ObservableCollection<MenuItemModel> Menus { get; set; } =
           new ObservableCollection<MenuItemModel>();
        public ObservableCollection<UserModel> Users { get; set; } =
           new ObservableCollection<UserModel>();

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

                    SetMenuStatus(Menus, value.MenuIds);
                    // value.UserIds
                    LoadUsers(value.UserIds);
                });
            }
        }

        IRoleService _roleService;
        IMenuService _menuService;
        IDialogService _dialogService;
        IUserService _userService;
        public DelegateCommand<MenuItemModel> SelectMenuCommand { get; set; }
        public DelegateCommand SelectUserCommand { get; set; }
        public RoleViewModel(
            IRegionManager regionManager,
            IRoleService roleService,
            IMenuService menuService,
            IDialogService dialogService,
            IUserService userService)
            : base(regionManager)
        {
            this.PageTitle = "角色权限组";

            _roleService = roleService;
            _menuService = menuService;
            _dialogService = dialogService;
            _userService = userService;

            SelectMenuCommand = new DelegateCommand<MenuItemModel>(DoSelectMenu);
            SelectUserCommand = new DelegateCommand(DoSelcetUser);
            Refresh();
        }

        public override void Refresh()
        {
            RoleList.Clear();
            var rs = _roleService.GetRoles(SearchKey);
            foreach (var role in rs)
            {
                RoleList.Add(new RoleModel
                {
                    RoleId = role.RoleId,
                    RoleName = role.RoleName,
                    RoleDesc = role.RoleDesc,
                    MenuIds = role.Menus.Select(m => m.MenuId).ToList(),
                    UserIds = role.Users.Select(u => u.UserId).ToList()
                });
            }
            // 加载供选择的所有菜单
            var ms = _menuService.GetMenuList().ToList();
            MenuHelper.FillMenus(Menus, null, ms);

            CurrentRole = RoleList.Count > 0 ?
               (_rid == 0 ? RoleList[0] : RoleList.FirstOrDefault(r => r.RoleId == _rid)) :
                null;
        }

        public override void DoModify(RoleModel model)
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

        public override void DoDelete(RoleModel model)
        {
            try
            {
                if (MessageBox.Show("是否确定删除此项？", "提示", MessageBoxButton.YesNo) ==
                    MessageBoxResult.Yes)
                {
                    // 物理删除
                    _roleService.Delete<SysRole>((model as RoleModel).RoleId);

                    MessageBox.Show("删除完成！", "提示");

                    RoleList.Remove(model as RoleModel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }
        private void SetMenuStatus(ObservableCollection<MenuItemModel> menus, List<int> mids)
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

        private void DoSelectMenu(MenuItemModel model)
        {
            try
            {
                // model.IsSelected  两种状态  选中   非选中
                // 选中：需要在RoleMenu表中添加一条记录 ：  角色ID   菜单ID
                // 非选中：需要从RoleMenu中删除一条记录 ：  角色ID   菜单ID
                var role = _roleService.Find<SysRole>(CurrentRole.RoleId);
                if (role == null) return;

                if (model.IsSelected)
                {
                    role.Menus.Add(new RoleMenu
                    {
                        RoleId = CurrentRole.RoleId,
                        MenuId = model.MenuId,
                        SysRole = role
                    }
                    );
                    CurrentRole.MenuIds.Add(model.MenuId);

                    // 判断所有子节点---多层级菜单：
                    // 父节点被选中的时候，把所有子节点都选中，并且存入数据库
                    if (model.Children != null && model.Children.Count > 0)
                    {
                        foreach (var item in model.Children)
                        {
                            item.IsSelected = true;
                            role.Menus.Add(new RoleMenu
                            {
                                RoleId = CurrentRole.RoleId,
                                MenuId = item.MenuId,
                                SysRole = role
                            });
                            CurrentRole.MenuIds.Add(item.MenuId);
                        }
                    }
                    // 如果是子节点选中的时候，判断父节点是否被选中，如果未被选中，由选中它
                    if (model.Parent != null && !model.Parent.IsSelected)
                    {
                        model.Parent.IsSelected = true;
                        role.Menus.Add(new RoleMenu
                        {
                            RoleId = CurrentRole.RoleId,
                            MenuId = model.Parent.MenuId,
                            SysRole = role
                        });
                        CurrentRole.MenuIds.Add(model.Parent.MenuId);
                    }
                }
                else
                {
                    role.Menus.RemoveAll(m => m.MenuId == model.MenuId);
                    CurrentRole.MenuIds.RemoveAll(mid => mid == model.MenuId);

                    // 父节点被勾选掉的时候，把所有子节点都去掉，并且存入数据库
                    if (model.Children != null && model.Children.Count > 0)
                    {
                        var mids = model.Children.Select(m =>
                        {
                            m.IsSelected = false;
                            return m.MenuId;
                        }).ToList();
                        role.Menus.RemoveAll(m => mids.Contains(m.MenuId));
                        CurrentRole.MenuIds.RemoveAll(m => mids.Contains(m));
                    }

                    // 如果勾选掉子节点，判断父节点所有的子节点，如果所有子节点都未选中，那么将这个父节点的选中状态去掉
                    if (model.Parent != null &&
                        !model.Parent.Children.ToList().Exists(m => m.IsSelected))
                    {
                        model.Parent.IsSelected = false;
                        role.Menus.RemoveAll(m => m.MenuId == model.Parent.MenuId);
                        CurrentRole.MenuIds.RemoveAll(m => m == model.Parent.MenuId);
                    }

                }
                _roleService.Update(role);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }
        private void LoadUsers(List<int> uids)
        {
            Users.Clear();
            var us = _userService.Query<SysUser>(u => uids.Contains(u.UserId)).ToList();
            foreach (var u in us)
            {
                Users.Add(new UserModel
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    RealName = u.RealName,
                    UserIcon = "pack://siteoforigin:,,,/Avatars/" + u.UserIcon
                });
            }
        }
        int _rid = 0;
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
    }
}