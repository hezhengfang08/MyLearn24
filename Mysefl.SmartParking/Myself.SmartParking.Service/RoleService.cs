using Microsoft.EntityFrameworkCore;
using Myself.SmartParing.IService;
using Myself.SmartParking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.SmartParking.Service
{
    public class RoleService : BaseService, IRoleService
    {
        public RoleService(DbContext context) : base(context)
        {
        }
        public IEnumerable<Entities.SysRole> GetRoles(string key)
        {
            var query = this.Query<SysRole>(m =>

                string.IsNullOrEmpty(key) ||

                m.RoleName.Contains(key) ||

                m.RoleDesc.Contains(key));
            return query.ToList();
        }
    }
}
