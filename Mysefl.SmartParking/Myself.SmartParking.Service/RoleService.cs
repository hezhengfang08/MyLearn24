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
            return this.Set<SysRole>()
                  .Include(r => r.Users)
                  .Include(r => r.Menus)
                  .Where(r =>
                      string.IsNullOrEmpty(key) ||
                      r.RoleName.Contains(key) ||
                      r.RoleDesc.Contains(key)
                  );
          
        }
        public bool CheckRoleName(string roleName, int roleId)
        {
            return this.Query<SysRole>(r =>
                                        r.RoleName == roleName &&
                                        r.RoleId != roleId)
                .Count() > 0;
        }
    }
}
