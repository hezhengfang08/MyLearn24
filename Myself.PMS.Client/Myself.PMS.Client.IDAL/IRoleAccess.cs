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
    }
}
