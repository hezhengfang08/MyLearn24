using Myself.PMS.Client.MainModule.Models;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.MainModule.ViewModels
{
    public class DashboardViewModel : INavigationAware
    {
        public List<DataModel> MainDatas { get; set; } =
            new List<DataModel>();

        public List<AmountModel> AmountList { get; set; } =
                new List<AmountModel>();

        public UserModel CurrentUser { get; set; } = new UserModel();


        public DashboardViewModel()
        {
            MainDatas.Add(new DataModel()
            {
                Header = "业主数量",
                Value = 6178,
                Icon = "\ue606",
                Color = "#3F4BFE"
            });
            MainDatas.Add(new DataModel()
            {
                Header = "房屋数量",
                Value = 5124,
                Icon = "\ue602",
                Color = "#FE3D3D"
            });
            MainDatas.Add(new DataModel()
            {
                Header = "设备数量",
                Value = 412,
                Icon = "\ue604",
                Color = "#953DFF"
            });
            MainDatas.Add(new DataModel()
            {
                Header = "车辆数量",
                Value = 2165,
                Icon = "\ue604",
                Color = "#3EB2FE"
            });
            MainDatas.Add(new DataModel()
            {
                Header = "车辆数量",
                Value = 2165,
                Icon = "\ue604",
                Color = "#3E4BFF"
            });



            AmountList.Add(new AmountModel
            {
                Rate = 93,
                Color = "#9A3DFE",
                Header = "广告费",
                Amount = 304.12
            }); AmountList.Add(new AmountModel
            {
                Rate = 68,
                Color = "#3F5AFF",
                Header = "物业管理费",
                Amount = 416.37
            }); AmountList.Add(new AmountModel
            {
                Rate = 62,
                Color = "#FD3BB4",
                Header = "车位管理费",
                Amount = 164.56
            }); AmountList.Add(new AmountModel
            {
                Rate = 76,
                Color = "#FE923C",
                Header = "垃圾处理费",
                Amount = 147.18
            });
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Entities.EmployeeEntity _currentUser = navigationContext.Parameters
                .GetValue<Entities.EmployeeEntity>("user");
            CurrentUser.UserId = _currentUser.eId;
            CurrentUser.RealName = _currentUser.realName;
            CurrentUser.Avatar = _currentUser.eIcon;
            CurrentUser.Password = _currentUser.password;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
    }
}
