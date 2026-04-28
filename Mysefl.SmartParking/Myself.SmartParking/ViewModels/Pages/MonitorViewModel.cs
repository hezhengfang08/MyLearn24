using Myself.SmartParing.IService;
using Myself.SmartParking.Entities;
using Myself.SmartParking.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Myself.SmartParking.ViewModels.Pages
{
    public class MonitorViewModel : ViewModelBase<UserModel>
    {
        public ObservableCollection<RecordModel> RecordList { get; set; } =
            new ObservableCollection<RecordModel>();

        public UserModel CurrentUser { get; set; } = new UserModel();

        private ImageSource _enterSnapFull;
        public ImageSource EnterSnapFull
        {
            get { return _enterSnapFull; }
            set { SetProperty<ImageSource>(ref _enterSnapFull, value); }
        }
        private ImageSource _snapSmall;

        public ImageSource SnapSmall
        {
            get { return _snapSmall; }
            set { SetProperty<ImageSource>(ref _snapSmall, value); }
        }

        public DelegateCommand GotoManagerCommand { get; set; }

        IRegionManager _regionManager;
        IDeviceService _deviceService;
        public MonitorViewModel(IRegionManager regionManager, IDeviceService deviceService)
            : base(regionManager)
        {
            _regionManager = regionManager;
            _deviceService = deviceService;
            for (var i = 0; i < 10; i++)
            {
                RecordList.Add(new RecordModel() { Index = (i + 1).ToString() });
            }

            GotoManagerCommand = new DelegateCommand(DoGotoManager);

            this.StartMonitor();
            
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
        private void StartMonitor()
        {
            // 监控系统的核心功能，开启监控
            var ds = _deviceService.Query<DeviceInfo>(d=>true).ToList();
            // 入口设备连接
            Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ds[0].AddrIp, (int)ds[0].AddrPort);

            Task.Run(async () =>
            {
                while (true)
                {
                    byte[] all_header_bytes = new byte[8];
                    socket.Receive(all_header_bytes);
                    // 后续所有子包的字节
                    int len = BitConverter.ToInt32(all_header_bytes, 4);

                    // 将所子包的字节获取到
                    byte[] bytes = new byte[len];
                    socket.Receive(bytes);

                    int index = 0;
                    // 识别信息
                    byte[] bytes_len = new byte[] {
                        bytes[index+4],
                        bytes[index+5],
                        bytes[index+6],
                        bytes[index+7],
                    };
                    len = BitConverter.ToInt32(bytes_len, 0);
                    byte[] info_bytes = bytes.ToList().GetRange(index + 8, len).ToArray();
                    string info_json = Encoding.UTF8.GetString(info_bytes);
                    var li = System.Text.Json.JsonSerializer.Deserialize<LicenseInfo>(info_json);
                    index += len + 8;

                    // 大图数据
                    bytes_len = new byte[] {
                        bytes[index+4],
                        bytes[index+5],
                        bytes[index+6],
                        bytes[index+7],
                    };
                    len = BitConverter.ToInt32(bytes_len, 0);
                    byte[] full_bytes = bytes.ToList().GetRange(index + 8, len).ToArray();
                    index += len + 8;
                    // 转成图像对象
                    // ImageBrush---ImageSource
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        using (var ms = new MemoryStream(full_bytes))
                        {
                            BitmapImage bi = new BitmapImage();
                            bi.BeginInit();
                            bi.StreamSource = ms;
                            bi.EndInit();

                            this.EnterSnapFull = bi;
                        }
                    

                    // 小图数据 
                    bytes_len = new byte[] {
                        bytes[index+4],
                        bytes[index+5],
                        bytes[index+6],
                        bytes[index+7],
                    };
                    len = BitConverter.ToInt32(bytes_len, 0);
                    byte[] small_bytes = bytes.ToList().GetRange(index + 8, len).ToArray();
                    // 转成图像对象
                    // ImageBrush---ImageSource
                    
                        using (var ms = new MemoryStream(small_bytes))
                        {
                            BitmapImage bi = new BitmapImage();
                            bi.BeginInit();
                            bi.StreamSource = ms;
                            bi.EndInit();
                            this.SnapSmall = bi;
                        }
                    });


                    // 出入记录

                    // 订单生成


                    // 抬杆通知

                }
            });
        }   
    }
}
