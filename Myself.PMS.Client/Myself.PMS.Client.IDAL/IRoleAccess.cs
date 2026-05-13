using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.IDAL
{
    public interface IRoleAccess
    {
        string GetRoleByIds(string id_json);
        string GetAllRoles(string key);

        string CheckRoleName(string roleName, int id);

        string Update(string role);

        string Delete(int id);

        string UpdateRoleMenus(string roleMenus_json);

        string UpdateRoleUsers(string roleUsers_json);

        string DeleteRoleUser(int rid, int uid);
    }
}
