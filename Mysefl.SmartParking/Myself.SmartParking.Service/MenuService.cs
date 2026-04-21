using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

        public IEnumerable<SysMenu> GetMenuList(string search_key = "")
        {
            
            return this.Query<SysMenu>(m =>

               string.IsNullOrEmpty(search_key) ? 1 == 1 :


               /// 有条件的时候，检查每个子项的时候，需要同时检查其子项
               /// 
               /// 检查当前菜单项的Header有没有包括关键词
               (m.MenuHeader.Contains(search_key) ||
               /// 检查当前菜单项的TargetView有没有包括关键词
               m.TargetView.Contains(search_key) ||
               /// 检查当前菜单项的子项中有没有符合条件的：Header或者TargetView包括了关键词
               Context.Set<SysMenu>().Where(sm => sm.ParentId == m.MenuId &&
                                       (sm.MenuHeader.Contains(search_key) || sm.TargetView.Contains(search_key))).Count() > 0
               )
           );
        }
    }
}
