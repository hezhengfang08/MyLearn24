using Myself.SmartParing.IService;
using Myself.SmartParking.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.SmartParking.ViewModels.Pages
{
    public class RoleViewModel: ViewModelBase<RoleModel>
    {
        public ObservableCollection<RoleModel> RoleList { get; set; } =
           new ObservableCollection<RoleModel>();

        IRoleService _roleService;
        public RoleViewModel(
            IRegionManager regionManager,
            IRoleService roleService)
            : base(regionManager)
        {
            this.PageTitle = "角色权限组";

            _roleService = roleService;

            Refresh();
        }

        public override void Refresh()
        {
            RoleList.Clear();
            var rs = _roleService.GetRoles(SearchKey);
            foreach (var role in rs)
            {
                RoleList.Add(new RoleModel
                {
                    RoleId = role.RoleId,
                    RoleName = role.RoleName,
                    RoleDesc = role.RoleDesc
                });
            }
        }

        public override void DoModify(RoleModel model)
        {
            base.DoModify(model);
        }

        public override void DoDelete(RoleModel model)
        {
            base.DoDelete(model);
        }
    }
}
