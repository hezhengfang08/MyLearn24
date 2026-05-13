using Myself.PMS.Server.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Server.IService
{
    public interface IRoleService
    {
        SysRole[] GetRoleByIds(int[] id);
        SysRole[] GetAllRoles(string key);

        bool CheckRoleName(string roleName, int id);

        int Update(SysRole role);

        int DeleteRole(int id);
        int UpdateRoleMenus(RoleMenu[] rms);

        int UpdateRoleUsers(RoleUser[] users);

        int DeleteRoleUser(int rid, int uid);
    }
}
