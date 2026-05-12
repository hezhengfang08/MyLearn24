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

        public string GetRoleByIds(string id_json)
        {
            string uri = "/api/role/list";

            StringContent content = new StringContent(id_json);
            content.Headers.ContentType =
                new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            return this.Post(uri, content);
        }
    }
}
