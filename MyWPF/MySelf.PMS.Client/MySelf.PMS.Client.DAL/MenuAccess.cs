using MySelf.PMS.Client.Entities;
using MySelf.PMS.Client.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.PMS.Client.DAL
{
    public class MenuAccess : WebAccess, IMenuAccess
    {
        public MenuAccess(GlobalValues globalValues) : base(globalValues)
        {
        }
        public string GetAllMenus()
        {
            string url = "/api/Menu/all";
            return this.Get(url);
        }
    }
}
