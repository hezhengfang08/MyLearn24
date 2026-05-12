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
        public SysRole[] GetRoleByIds(int[] ids)
        {
            string json =ids.Serialize();
            json = _roleAccess.GetRoleByIds(json);

            return this.GetResult<SysRole[]>(json);
        }
    }
}
