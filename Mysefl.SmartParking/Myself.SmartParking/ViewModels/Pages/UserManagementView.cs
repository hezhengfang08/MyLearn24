using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.SmartParking.ViewModels.Pages
{
    public class UserManagementView:ViewModelBase
    {
        public UserManagementView(IRegionManager regionManager) : base(regionManager)
        {
            PageTitle = "用户数据维护";
           
        }
    }
}
