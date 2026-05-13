using Myself.PMS.Client.Entities;
using Myself.PMS.Client.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.DAL
{
    public class RoleAccess : WebAccess, IRoleAccess
    {
        public RoleAccess(GlobalValues globalValues) : base(globalValues)
        {
        }

        public string CheckRoleName(string roleName, int id)
        {
            string uri = $"/api/role/check/{id}/{roleName}";
            return this.Get(uri);
        }

        public string Delete(int id)
        {
            string uri = $"/api/role/delete/{id}";
            return this.Get(uri);
        }

        public string DeleteRoleUser(int rid, int uid)
        {
            string uri = $"/api/role/del_user/{rid}/{uid}";
            return this.Get(uri);
        }

        public string GetAllRoles(string key)
        {
            key = string.IsNullOrEmpty(key) ? "none" : key;
            string uri = $"/api/role/all/{key}";
            return this.Get(uri);
        }

        public string GetRoleByIds(string id_json)
        {
            string uri = "/api/role/list";
            return this.PostJons(uri, id_json);
        }

        public string Update(string role)
        {
            string uri = "/api/role/update";
            return this.PostJons(uri, role);
        }

        public string UpdateRoleMenus(string roleMenus_json)
        {
            string uri = "/api/role/rmenus";
            return this.PostJons(uri, roleMenus_json);
        }

        public string UpdateRoleUsers(string roleUsers_json)
        {
            string uri = "/api/role/rusers";
            return this.PostJons(uri, roleUsers_json);
        }
    }
}
