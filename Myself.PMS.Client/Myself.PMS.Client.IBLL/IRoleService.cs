using Myself.PMS.Client.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.IBLL
{
    public interface IRoleService
    {
        SysRole[] GetRoleByIds(int[] id);

        SysRole[] GetAllRoles(string key = "none");

        bool CheckRoleName(string roleName, int id);

        int UpdateRole(SysRole role);

        int DeleteRole(int id);

        int UpdateRoleMenus(RoleMenu[] roleMenus);

        int UpdateRoleUsers(RoleUser[] roleUsers);

        int DeleteRoleUser(int rid, int uid);
    }
}
