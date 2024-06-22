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
    //角色接口实现类
    public class RoleService : BaseService, IRoleService
    {

        public List<RoleInfo> GetRoleList(bool isDel)
        {
            return Query<RoleInfo>(r => r.IsDeleted == isDel).ToList();
        }

        public List<UIRole> GetCboRoles()
        {
            var roles = Query<RoleInfo>(r => r.IsDeleted == false).Select(r => new UIRole()
            {
                RoleId = r.RoleId,
                RoleName = r.RoleName
            }).ToList();
            return roles;
        }

        public bool DeleteRole(RoleInfo role)
        {
            return UpdateRoleDelState(role, 0, true);
        }



        public bool RemoveRole(RoleInfo role)
        {
            return UpdateRoleDelState(role, 1, true);
        }

        /// <summary>
        /// 角色信息的删除、恢复、移除处理
        /// </summary>
        /// <param name="role"></param>
        /// <param name="delType"></param>
        /// <param name="isDeleted"></param>
        /// <returns></returns>
        private bool UpdateRoleDelState(RoleInfo role, int delType, bool isDeleted)
        {
            var rmenus = Query<RoleMenuInfo>(rm => rm.RoleId == role.RoleId).ToList();
            return ExecuteTrans(() =>
            {
                if (delType == 0)//删除或恢复
                {
                    role.IsDeleted = isDeleted;
                    dbContext.Entry(role).State = EntityState.Modified;//已修改
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
                    UnCommitDelete(role);
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
                    Detach(role);
                    if (rmenus.Count > 0)
                    {
                        DetachList(rmenus);
                    }
                }
            });
        }

        public bool ExistRole(string roleName)
        {
            return Query<RoleInfo>(r => r.RoleName == roleName && r.IsDeleted == false).Any();
        }

        public bool SaveRole(RoleInfo roleInfo)
        {
            bool blSave = false;
            if (roleInfo.RoleId == 0)
            {
                Insert(roleInfo);
                if (roleInfo.RoleId > 0)
                    blSave = true;
            }
            else
            {
                Update(roleInfo);
                if (dbContext.Entry(roleInfo).State == EntityState.Unchanged)
                    blSave = true;
            }
            Detach(roleInfo);
            return blSave;
        }

        public bool HasRoleUsers(int roleId)
        {
            return Query<UserInfo>(u => u.RoleId == roleId && u.IsDeleted == false).Any();
        }


        public bool RecoveryRole(RoleInfo roleInfo)
        {

            return UpdateRoleDelState(roleInfo, 0, false);

        }
        public bool SaveRolePermissionSet(List<int> menuIds, int roleId)
        {
            //原来的关系
            var hasrmenus = Query<RoleMenuInfo>(rm => rm.RoleId == roleId).ToList();
            //已取消的角色菜单关系
            var deletedrmenus = hasrmenus.Where(rm => !menuIds.Contains(rm.MenuId)).ToList();
            //移除取消的关系
            deletedrmenus.ForEach(rm => hasrmenus.Remove(rm));
            //移除已存在的菜单编号----剩下的就是新增的菜单编号
            hasrmenus.ForEach(rm => menuIds.Remove(rm.MenuId));

            //生成新增的关系
            List<RoleMenuInfo> newRoleMenus = new List<RoleMenuInfo>();

            return ExecuteTrans(() =>
            {
                Delete<RoleMenuInfo>(deletedrmenus);//删除已取消的关系
                if (menuIds.Count > 0)
                {
                    menuIds.ForEach(id => newRoleMenus.Add(new RoleMenuInfo() { RoleId = roleId, MenuId = id }));
                    Insert<RoleMenuInfo>(newRoleMenus);//新增关系
                }
            }, () =>
            {
                if (newRoleMenus.Count > 0)
                    DetachList(newRoleMenus);
            });
        }
    }
}
