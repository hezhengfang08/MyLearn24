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
    }
}
