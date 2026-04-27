using Myself.SmartParking.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Myself.SmartParking.ViewModels.Pages
{
    public class MonitorViewModel : ViewModelBase<UserModel>
    {
        public ObservableCollection<RecordModel> RecordList { get; set; } =
            new ObservableCollection<RecordModel>();

        public UserModel CurrentUser { get; set; } = new UserModel();


        public DelegateCommand GotoManagerCommand { get; set; }

        IRegionManager _regionManager;
        public MonitorViewModel(IRegionManager regionManager)
            : base(regionManager)
        {
            _regionManager = regionManager;


            for (var i = 0; i < 10; i++)
            {
                RecordList.Add(new RecordModel() { Index = (i + 1).ToString() });
            }

            GotoManagerCommand = new DelegateCommand(DoGotoManager);


            byte[] datas = new byte[1024];
            datas[0] = 2;
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            CurrentUser = navigationContext.Parameters.GetValue<UserModel>("user");
        }

        private async void DoGotoManager()
        {
            // 如何返回到管理后台？
            // 直接从MonitorRegion中移除已有的页面，
            // 首先需要获取到RegionManger对象，通过这个对象进行Region管理

            var region = _regionManager.Regions["MonitorRegion"];
            if (region == null) return;

            var view = region.Views.FirstOrDefault();
            VisualStateManager.GoToElementState((FrameworkElement)view, "CloseState", true);

            await Task.Delay(300);
            region.RemoveAll();


        }
    }
}
