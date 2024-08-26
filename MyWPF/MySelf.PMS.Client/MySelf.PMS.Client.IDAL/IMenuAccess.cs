using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.PMS.Client.IDAL
{
    public interface IMenuAccess
    {
        string GetAllMenus(string key="");
        string UpdateMenu(string menuJson);
        string DeleteMenu(string id);
    }
}
