using HandyControl.Controls;
using MySelf.QOSM.BLLFactory;
using MySelf.QOSM.IServices;
using MySelf.QOSM.Models.UIModels;
using MySelf.QOSM.QuickOrderAPP.Utils;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MySelf.QOSM.QuickOrderAPP.ViewModels.SM
{
    public class RightSetViewModel:ViewModelBase
    {
        IRoleService roleService = InstanceFactory.CreateInstance<IRoleService>();
        IMenuService menuService = InstanceFactory.CreateInstance<IMenuService>();

        public RightSetViewModel()
        {
            LoadCboRoles();//加载角色下拉框
            LoadMenuList();//加载菜单树（生成TreeView的数据源）
            InitCommands();//相关命令初始化
        }
        #region 命令
        //全选或取消全选命令
        public ICommand CheckAllCmd { get; set; }
        //加载指定角色的已有权限设置命令
        public ICommand LoadRoleRightCmd { get; set; }
        //保存权限设置命令
        public ICommand SaveRightCmd { get; set; }
        #endregion
        #region 属性
        private bool isCheckALL;

        public bool IsCheckALL
        {
            get { return isCheckALL; }
            set { isCheckALL = value; }
        }

        private List<UIRole>  roleList;

        public List<UIRole> RoleList
        {
            get { return roleList; }
            set { roleList = value; }
        }

        private int roleId;

        public int RoleId
        {
            get { return roleId; }
            set { roleId = value; }
        }
        private List<TreeMenuItem> menuList;

        public List<TreeMenuItem> MenuList
        {
            get { return menuList; }
            set { menuList = value; }
        }

        #endregion
        #region 方法
        private void LoadCboRoles()
        {
            List<UIRole> roles = roleService.GetCboRoles();
            roles.Insert(0, new UIRole() { RoleId = 0, RoleName = "请选择角色" });
            roles.Add(new UIRole()
            {
                RoleId = 1,
                RoleName = "客户"
            });
            RoleList = roles;
        }
        private void LoadMenuList()
        {
            List<CboMenu> menus = menuService.GetCboMenus();
            List<TreeMenuItem> List = new List<TreeMenuItem>();
            AddMenuItem(menus, List, null, 0);

        }
        private void AddMenuItem(List<CboMenu> allMenu,List<TreeMenuItem> tvList, TreeMenuItem pItem, int parentId)
        {
            var subItems = allMenu.Where(a=>a.ParentId == parentId);
            foreach (var subItem in subItems)
            {
                TreeMenuItem node = new TreeMenuItem()
                {
                    MenuId = subItem.MenuId,
                    MenuName = subItem.MenuName,

                };
                if(pItem != null)
                {
                    pItem.SubItems.Add(node);
                    node.ParentNode = pItem;
                }
                else
                {
                    tvList.Add(node);
                }
                AddMenuItem(allMenu, tvList, node, subItem.MenuId);
            }
        }
        /// <summary>
        /// 全选与取消全选
        /// </summary>
        /// <param name="blCheck"></param>
        private void CheckAllMenusState(bool blCheck)
        {
            IsCheckALL = blCheck;
            //处理树中节点的勾选
            CheckMenuNodeState(MenuList, blCheck);
        }

        /// <summary>
        /// 递归处理节点的勾选
        /// </summary>
        /// <param name="nodeList"></param>
        /// <param name="blCheck"></param>
        private void CheckMenuNodeState(List<TreeMenuItem> nodeList, bool blCheck)
        {
            foreach (TreeMenuItem node in nodeList)
            {
                node.IsCheck = blCheck;
                CheckMenuNodeState(node.SubItems, blCheck);
            }
        }

        //加载已有设置
        private void LoadRoleRightSet()
        {
            CheckAllMenusState(false);//清空勾选--全不选
            if (RoleId > 0)
            {
                //获取该角色的角色菜单关系数据---菜单编号集合
                List<int> menuIds = menuService.GetRoleMenuIds(RoleId);
                //加载处理
                if (menuIds.Count > 0)
                {
                    foreach (var menu in MenuList)
                    {
                        if (menuIds.Contains(menu.MenuId))
                        {
                            int count = 0;//计数有多个子节点已勾选
                            foreach (var subMenu in menu.SubItems)
                            {
                                if (menuIds.Contains(subMenu.MenuId))
                                {
                                    subMenu.IsCheck = true;//已设置
                                    count++;
                                }
                            }
                            if (count == menu.SubItems.Count)//子节点全勾选、没有子节点
                            {
                                menu.IsCheck = true;
                            }
                            else if (count > 0)//子节点部分勾选
                            {
                                menu.IsCheck = null;//中间态
                            }
                            else
                                menu.IsCheck = false;//不勾选
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 保存权限设置
        /// </summary>
        private void SaveRightSet()
        {
            //1.获取当前已勾选或中间态的节点的菜单编号
            List<int> checkedMenuIds = new List<int>();
            GetCheckdMenuIds(checkedMenuIds, MenuList);
            string msgTitle = "保存角色权限设置";
            if (RoleId == 0)
            {
                ShowError("请选择要设置权限的角色！", msgTitle);
                return;
            }
            if (checkedMenuIds.Count == 0)
            {
                ShowError("请选设置该角色的权限菜单！", msgTitle);
                return;
            }
            //保存
            bool blSave = roleService.SaveRolePermissionSet(checkedMenuIds, RoleId);
            string roleName = RoleList.Find(r => r.RoleId == RoleId).RoleName;
            if (blSave)
            {
                Growl.Success(new HandyControl.Data.GrowlInfo()
                {
                    Message = $"角色：{roleName} 的权限设置成功！",
                    ShowCloseButton = true,
                    Token = "SucMes",
                    ShowDateTime = true
                });
            }
            else
            {
                ShowError($"角色：{roleName} 的权限设置失败！", msgTitle);
                return;
            }
        }

        //获取已设置的菜单编号集合：中间态和已勾选
        private void GetCheckdMenuIds(List<int> checkedIds, List<TreeMenuItem> mList)
        {
            foreach (var menu in mList)
            {
                if (menu.IsCheck == true || menu.IsCheck == null)
                    checkedIds.Add(menu.MenuId);
                GetCheckdMenuIds(checkedIds, menu.SubItems);
            }
        }
        private void InitCommands()
        {
            CheckAllCmd = new RelayCommand(o =>
            {
                CheckAllMenusState(IsCheckALL);//递归处理
            });
            LoadRoleRightCmd = new RelayCommand(o =>
            {
                LoadRoleRightSet();//加载已有设置
            });

            SaveRightCmd = new RelayCommand(o =>
            {
                SaveRightSet();//保存角色的权限设置（角色菜单关系数据）
            });
        }
        #endregion
    }
}
