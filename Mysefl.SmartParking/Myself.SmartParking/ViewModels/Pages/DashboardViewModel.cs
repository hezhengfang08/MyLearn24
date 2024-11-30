using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Myself.SmartParking.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.SmartParking.ViewModels.Pages
{
    public class DashboardViewModel
    {
        public string PageTitle { get; set; } = "数据中心";

        public ObservableCollection<AreaModel> AraeList { get; set; } =
            new ObservableCollection<AreaModel>();

        public ObservableCollection<BoardModel> BoardList { get; set; } =
            new ObservableCollection<BoardModel>();

        public ObservableCollection<RecordModel> RecordList { get; set; } =
            new ObservableCollection<RecordModel>();

        public ObservableCollection<PassRecordItemModel> InfoList { get; set; } =
            new ObservableCollection<PassRecordItemModel>();

        public LiveCharts.SeriesCollection Series { get; set; } =
            new LiveCharts.SeriesCollection();

        public DateTime CurrentDate { get; set; } = DateTime.Now;

        public DashboardViewModel()
        {
            BoardList.Add(new BoardModel { Header = "总收入", Value = 568768, Color = "#EC9606", Icon = "\ue66d" });
            BoardList.Add(new BoardModel { Header = "优惠卷(张)", Value = 24, Color = "#088DF6", Icon = "\ue624" });
            BoardList.Add(new BoardModel { Header = "会员累计人数", Value = 698, Color = "#F76E55", Icon = "\ue604" });
            BoardList.Add(new BoardModel { Header = "当前空车位", Value = 80, Color = "#0ACEB1", Icon = "\ue68f" });
            BoardList.Add(new BoardModel { Header = "访客未处理(人)", Value = 5, Color = "#954AF2", Icon = "\ue606" });



            // 从数据库获取相关数据
            // 后台有数据后，再做对接
            RecordList.Add(new RecordModel
            {
                CarImage = "/Zhaoxi.SmartParking.Assets;component/Images/covers/huA.jpg",
                Number = "沪A59P65",
                RecordInfo = "测试车辆进出记录，未见异常"
            });
            RecordList.Add(new RecordModel
            {
                CarImage = "/Zhaoxi.SmartParking.Assets;component/Images/covers/wanF.jpg",
                Number = "皖FC001C",
                RecordInfo = "测试车辆进出记录，未见异常"
            });
            RecordList.Add(new RecordModel
            {
                CarImage = "/Zhaoxi.SmartParking.Assets;component/Images/covers/yiB.jpg",
                Number = "冀B850AR",
                RecordInfo = "测试车辆进出记录，未见异常"
            });
            RecordList.Add(new RecordModel
            {
                CarImage = "/Zhaoxi.SmartParking.Assets;component/Images/covers/yueA.jpg",
                Number = "粤AFQ787",
                RecordInfo = "测试车辆进出记录，未见异常"
            });



            Series.Add(new PieSeries
            {
                Title = "微信支付",
                Values = new ChartValues<ObservableValue> { new ObservableValue(16.0) }
            });
            Series.Add(new PieSeries
            {
                Title = "支付宝支付",
                Values = new ChartValues<ObservableValue> { new ObservableValue(22.0) }
            });
            Series.Add(new PieSeries
            {
                Title = "现金支付",
                Values = new ChartValues<ObservableValue> { new ObservableValue(11.0) }
            });
            Series.Add(new PieSeries
            {
                Title = "优惠减免",
                Values = new ChartValues<ObservableValue> { new ObservableValue(39.0) }
            });
            Series.Add(new PieSeries
            {
                Title = "会员",
                Values = new ChartValues<ObservableValue> { new ObservableValue(12.0) }
            });


            InfoList.Add(new PassRecordItemModel()
            {
                PassDate = "2021-03-01 10:20:36",
                Passageway = "A1",
                TollCollector = "1024",
                LiftingState = "自动",
                Discount = 0,
                State = "正常",
                //MenuItemCommand = new DelegateCommand<object>(DataGridContextMenu)
            });
            InfoList.Add(new PassRecordItemModel() { PassDate = "2021-03-01 10:20:36", Passageway = "A1", TollCollector = "1024", LiftingState = "自动", Discount = 0, State = "正常" });
            InfoList.Add(new PassRecordItemModel() { PassDate = "2021-03-01 10:20:36", Passageway = "A1", TollCollector = "1024", LiftingState = "自动", Discount = 0, State = "正常" });
            InfoList.Add(new PassRecordItemModel() { PassDate = "2021-03-01 10:20:36", Passageway = "A1", TollCollector = "1024", LiftingState = "自动", Discount = 0, State = "正常" });
            InfoList.Add(new PassRecordItemModel() { PassDate = "2021-03-01 10:20:36", Passageway = "A1", TollCollector = "1024", LiftingState = "自动", Discount = 0, State = "正常" });
            InfoList.Add(new PassRecordItemModel() { PassDate = "2021-03-01 10:20:36", Passageway = "A1", TollCollector = "1024", LiftingState = "自动", Discount = 0, State = "正常" });
            InfoList.Add(new PassRecordItemModel() { PassDate = "2021-03-01 10:20:36", Passageway = "A1", TollCollector = "1024", LiftingState = "自动", Discount = 0, State = "正常" });
        }

    }
}
