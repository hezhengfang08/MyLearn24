using Microsoft.EntityFrameworkCore;
using MySelf.QOSM.IServices;
using MySelf.QOSM.Models.Entities;
using MySelf.QOSM.Models.UIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.QOSM.Services
{
    public class MenuService : BaseService, IMenuService
    {

        public PageModel<ViewMenuInfo> GetMenuList(string keywords, bool showDel, int startIndex, int pageSize)
        {
            PageModel<ViewMenuInfo> result = new PageModel<ViewMenuInfo>();
            var menuList = Query<ViewMenuInfo>(m => m.IsDeleted == showDel);
            if (menuList.Any())
            {
                if (!string.IsNullOrEmpty(keywords))
                {
                    menuList = menuList.Where(m => m.MenuName.Contains(keywords) || m.ParentName.Contains(keywords));
                }
                if (menuList.Any())
                {
                    menuList = menuList.OrderBy(m => m.ParentId).ThenBy(m => m.MenuOrder);
                    result.DataList = menuList.Skip(startIndex - 1).Take(pageSize).ToList();
                    result.TotalCount = menuList.Count();
                }
            }
            return result;
        }


        public List<CboMenu> GetCboMenus()
        {
            var menus = Query<MenuInfo>(m => m.IsDeleted == false).Select(m => new CboMenu()
            {
                MenuId = m.MenuId,
                MenuName = m.MenuName,
                ParentId = m.ParentId
            }).ToList();
            return menus;
        }

        public List<UIMenu> GetRoleMenuList(int roleId, int ParentId)
        {
            List<UIMenu> menuList = new List<UIMenu>();
            //未登录状态
            if (roleId == 0)
            {
                //导航列表只有点餐导航
                MenuInfo menu = Query<MenuInfo>(m => m.MenuName == "点餐").FirstOrDefault();
                if (menu != null)
                {
                    menuList.Add(new UIMenu()
                    {
                        MenuId = menu.MenuId,
                        MenuName = menu.MenuName,
                        MenuPic = menu.MenuPic,
                        ParentId = 0,
                        MenuUrl = menu.MenuUrl
                    });
                }
            }
            else  //登录后
            {
                RoleInfo role = null;
                //后台角色、客户=1
                if (roleId != 1)
                {
                    role = Query<RoleInfo>(r => r.RoleId == roleId).Include(r => r.Menus).FirstOrDefault();
                    if (role != null && role.Menus.Count > 0)
                    {
                        menuList = role.Menus.Where(m => m.ParentId == ParentId).Select(m => new UIMenu()
                        {
                            MenuId = m.MenuId,
                            MenuName = m.MenuName,
                            MenuPic = m.MenuPic,
                            ParentId = ParentId,
                            MenuUrl = m.MenuUrl
                        }).ToList();
                    }
                }
                else
                {
                    //客户导航列表获取
                    List<int> menuIds = Query<RoleMenuInfo>(rm => rm.RoleId == roleId).Select(rm => rm.MenuId).ToList();
                    menuList = Query<MenuInfo>(m => menuIds.Contains(m.MenuId) && m.ParentId == ParentId).Select(m => new UIMenu()
                    {
                        MenuId = m.MenuId,
                        MenuName = m.MenuName,
                        MenuPic = m.MenuPic,
                        ParentId = ParentId,
                        MenuUrl = m.MenuUrl
                    }).ToList();
                }
            }

            return menuList;
        }

        public bool DeleteMenu(int menuId)
        {
            return UpdateMenuDelState(menuId, 0, true);
        }

        public bool RemoveMenu(int menuId)
        {
            return UpdateMenuDelState(menuId, 1, true);
        }

        /// <summary>
        /// 菜单信息的删除、恢复、移除处理
        /// </summary>
        /// <param name="role"></param>
        /// <param name="delType"></param>
        /// <param name="isDeleted"></param>
        /// <returns></returns>
        private bool UpdateMenuDelState(int menuId, int delType, bool isDeleted)
        {
            var menuInfo = Find<MenuInfo>(menuId);
            var rmenus = Query<RoleMenuInfo>(rm => rm.MenuId == menuId && rm.IsDeleted == !isDeleted).ToList();
            //恢复---筛选出有效的角色菜单关系
            if (rmenus.Count > 0 && isDeleted == false)
            {
                var roleIds = Query<RoleInfo>(r => r.IsDeleted == false).Select(r => r.RoleId).ToList();
                rmenus = rmenus.Where(rm => roleIds.Contains(rm.RoleId)).ToList(); //筛选出有效的角色菜单关系
            }
            return ExecuteTrans(() =>
            {
                if (delType == 0)//删除或恢复
                {
                    menuInfo.IsDeleted = isDeleted;
                    dbContext.Entry(menuInfo).State = EntityState.Modified;//已修改
                    if (rmenus.Count > 0)
                    {
                        rmenus.ForEach(rm =>
                        {
                            rm.IsDeleted = isDeleted;
                            dbContext.Entry(rm).State = EntityState.Modified;
                        });
                    }
                }
                else  //真删除
                {
                    UnCommitDelete(menuInfo);
                    if (rmenus.Count > 0)
                    {
                        rmenus.ForEach(rm =>
                        {
                            UnCommitDelete(rm);
                        });
                    }
                }
            }, () =>
            {
                if (delType == 0)
                {
                    Detach(menuInfo);
                    if (rmenus.Count > 0)
                    {
                        DetachList(rmenus);
                    }
                }
            });
        }

        public bool ExistMenu(string menuName)
        {
            return Query<MenuInfo>(m => m.MenuName == menuName && m.IsDeleted == false).Any();
        }

        public bool SaveMenu(MenuInfo menuInfo)
        {
            bool blSave = false;
            if (menuInfo.MenuId == 0)
            {
                Insert(menuInfo);
                if (menuInfo.MenuId > 0)
                    blSave = true;
            }
            else
            {
                Update(menuInfo);
                if (dbContext.Entry(menuInfo).State == EntityState.Unchanged)
                    blSave = true;
            }
            Detach(menuInfo);
            return blSave;
        }



        public List<int> GetRoleMenuIds(int roleId)
        {
            return Query<RoleMenuInfo>(rm => rm.RoleId == roleId && rm.IsDeleted == false).Select(rm => rm.MenuId).ToList();
        }

        public bool RecoveryMenu(int menuId)
        {
            return UpdateMenuDelState(menuId, 0, false);
        }

        public bool IsHasChildMenu(int menuId)
        {
            return Query<MenuInfo>(m => m.ParentId == menuId && m.IsDeleted == false).Any();
        }
    }
}
