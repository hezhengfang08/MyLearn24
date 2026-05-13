using Myself.PMS.Client.Entities;
using Myself.PMS.Client.IBLL;
using Myself.PMS.Client.IDAL;
using Myself.PMS.Client.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.BLL
{
    public class RoleService : BaseService, IRoleService
    {
        IRoleAccess _roleAccess;
        public RoleService(IRoleAccess roleAccess)
        {
            _roleAccess = roleAccess;
        }

        public bool CheckRoleName(string roleName, int id)
        {
            string json = _roleAccess.CheckRoleName(roleName, id);
            return this.GetResult<bool>(json);
        }

        public int DeleteRole(int id)
        {
            string json = _roleAccess.Delete(id);
            return this.GetResult<int>(json);
        }

        public int DeleteRoleUser(int rid, int uid)
        {
            string json = _roleAccess.DeleteRoleUser(rid, uid);
            return this.GetResult<int>(json);
        }

        public SysRole[] GetAllRoles(string key = "none")
        {
            string json = _roleAccess.GetAllRoles(key);
            return this.GetResult<SysRole[]>(json);
        }

        public SysRole[] GetRoleByIds(int[] ids)
        {
            string json =ids.Serialize();
            json = _roleAccess.GetRoleByIds(json);

            return this.GetResult<SysRole[]>(json);
        }

        public int UpdateRole(SysRole role)
        {
            string json = role.Serialize();
            json = _roleAccess.Update(json);
            return this.GetResult<int>(json);
        }

        public int UpdateRoleMenus(RoleMenu[] roleMenus)
        {
            string json = roleMenus.Serialize();
            json = _roleAccess.UpdateRoleMenus(json);
            return this.GetResult<int>(json);
        }

        public int UpdateRoleUsers(RoleUser[] roleUsers)
        {
            string json = roleUsers.Serialize();
            json = _roleAccess.UpdateRoleUsers(json);
            return this.GetResult<int>(json);
        }
    }
}
