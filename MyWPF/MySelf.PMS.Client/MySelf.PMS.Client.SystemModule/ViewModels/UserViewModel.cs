using MySelf.PMS.Client.Common;
using MySelf.PMS.Client.IBLL;
using MySelf.PMS.Client.SystemModule.Models;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.PMS.Client.SystemModule.ViewModels
{
    public class UserViewModel : PageViewModelBase
    {
        public ObservableCollection<UserModel> Users { get; set; } =
            new ObservableCollection<UserModel>();

        IUserService _userService;
        public UserViewModel(IRegionManager regionManager,
            IUserService userService) : base(regionManager)
        {
            PageTitle = "系统用户管理";

            _userService = userService;

            this.Refresh();
        }

        public override void Refresh()
        {
            Users.Clear();
            var users = _userService.GetUsers(SearchKey).ToList();

            int index = 1;
            foreach (var user in users)
            {
                UserModel model = new UserModel
                {
                    Index = index++,
                    UserId = user.eId,
                    UserName = user.userName,
                    RealName = user.realName,
                    UserIcon = "http://localhost:5273/api/File/img/" + user.eIcon,
                    Address = user.address,
                    Age = user.age,
                    Password = user.password,
                    Gender = user.gender,
                    Email = user.email,
                    Status = user.status,

                    LockButtonText = user.status == 1 ? "锁定" : "启用"
                };
                Users.Add(model);
            }
        }

        public override void DoModify(object model)
        {
            base.DoModify(model);
        }

        public override void DoDelete(object model)
        {
            base.DoDelete(model);
        }
    }
}

