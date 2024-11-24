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
    public class MenuService : BaseService, IMenuService
    {
        public MenuService(DbContext context) : base(context)
        {

        }

        public IEnumerable<SysMenu> GetMenuList()
        {
            return this.Query<SysMenu>(m => true);
        }
    }
}
