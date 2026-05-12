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
    }
}
