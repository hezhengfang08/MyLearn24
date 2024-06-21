using MySelf.QOSM.BLLFactory;
using MySelf.QOSM.IServices;
using MySelf.QOSM.Models.UIModels;
using MySelf.QOSM.QuickOrderAPP.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MySelf.QOSM.QuickOrderAPP.ViewModels.Stat
{
    public class DataStatisticsViewModel : ViewModelBase
    {
        IStatisticsService statisticsService = InstanceFactory.CreateInstance<IStatisticsService>();
        public DataStatisticsViewModel()
        {
            StartTime = "";
            EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            //初始统计数据
            StatisticsData();
            //命令初始化
            InitCommands();
        }

        #region 属性
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        private decimal totalAmount;

        public decimal TotalAmount
        {
            get { return totalAmount; }
            set
            {
                totalAmount = value;
                OnPropertyChanged();
            }
        }
        private UIFTypeStatItem selType;
        //选择的类别
        public UIFTypeStatItem SelType
        {
            get { return selType; }
            set
            {
                selType = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<StatisticsAmountItem> statisticsAmounts;

        public ObservableCollection<StatisticsAmountItem> StatisticsAmounts
        {
            get { return statisticsAmounts; }
            set { statisticsAmounts = value; }
        }

        private ObservableCollection<UIFTypeStatItem> uIFTypeStats;

        public ObservableCollection<UIFTypeStatItem> UIFTypeStats
        {
            get { return uIFTypeStats; }
            set
            {
                uIFTypeStats = value;
                OnPropertyChanged();
            }
        }
        private UIFTypeStatItem uIFTypeStat;

        public UIFTypeStatItem UIFTypeStat
        {
            get { return uIFTypeStat; }
            set
            {
                uIFTypeStat = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<UIFoodCountItem> statFoodList;

        public ObservableCollection<UIFoodCountItem> StatFoodList
        {
            get { return statFoodList; }
            set
            {
                statFoodList = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region 方法
        private void InitCommands()
        {
            FindStatisticsDataCmd = new RelayCommand(o =>
            {
                StatisticsData();
            });
            FindStatTypeFoodListCmd = new RelayCommand(o =>
            {
                StatisticsFoodCounts();
            });
        }
        private void StatisticsData()
        {
            StartTime = StartTime == null ? "" : StartTime;
            EndTime = EndTime==null?"":EndTime;
            if (!string.IsNullOrEmpty(StartTime))
            {
                StartTime = DateTime.Parse(StartTime).ToString("yyyy-MM-dd HHmm");
            }
            if (!string.IsNullOrEmpty(EndTime))
            {
                EndTime = DateTime.Parse(EndTime).ToString("yyyy-MM-dd HHmm");
            }
            GetStatisticsAmounts();//统计总销售额
            StatisticsFTypeCounts();//按菜品类别统计销售量
            StatisticsFoodCounts();//按当前选择类别统计菜品销量
           
        }
        //按菜品类别统计销售量
        private void StatisticsFTypeCounts()
        {
            var typeList = statisticsService.StaticsFoodCountByFType(StartTime, EndTime);
            if (UIFTypeStats == null)
                UIFTypeStats = new ObservableCollection<UIFTypeStatItem>();
            else
                UIFTypeStats.Clear();
            typeList.ForEach(t =>
            {
                t.SaleCount = t.SaleCount == null ? 0 : t.SaleCount.Value;
                UIFTypeStats.Add(t);
            });
            if (SelType == null && UIFTypeStats.Count > 0)
                SelType = UIFTypeStats[0];
        }

        private void GetStatisticsAmounts()
        {
           var payWayData=  statisticsService.StaticsAmountByPayWay(StartTime, EndTime);
            if (payWayData.Count >0)
            {
                if (StatisticsAmounts ==null ||  StatisticsAmounts.Count ==0 )
                {
                    payWayData.ForEach(i =>
                    {
                        StatisticsAmounts.Add(new StatisticsAmountItem
                        {
                            Amount = i.TotalAmount,
                            PayCount = i.PayCount,
                            PayWayText = GetPayWayText(i.PayWay),
                            PayWay = i.PayWay
                        });
                    });
                }
                else { 
                    foreach (var item in StatisticsAmounts) {
                        var payItem = payWayData.Find(d=>d.PayWay == item.PayWay);
                        if (payItem != null)
                        {
                            item.PayCount = payItem.PayCount;
                            item.Amount = payItem.TotalAmount;
                        }
                        else
                        {
                            item.PayCount = 0;
                            item.Amount = 0;
                        }
                
                }
            }
                //总金额
                TotalAmount = payWayData.Sum(p => p.TotalAmount);
            }
            else
            {
                payWayData = new List<UIPayWayStatItem>()
                {
                    new UIPayWayStatItem(){PayWay=1,PayCount=0,TotalAmount=0},
                    new UIPayWayStatItem(){PayWay=2,PayCount=0,TotalAmount=0},
                    new UIPayWayStatItem(){PayWay=3,PayCount=0,TotalAmount=0},
                };
                StatisticsAmounts.Clear();
                payWayData.ForEach(data => StatisticsAmounts.Add(new StatisticsAmountItem()
                {
                    PayWay = data.PayWay,
                    PayCount = data.PayCount,
                    Amount = data.TotalAmount,
                    PayWayText = GetPayWayText(data.PayWay)
                }));
                TotalAmount = 0;
            }
        }
        private void StatisticsFoodCounts()
        {
            if (SelType != null)
            {
                var foodList = statisticsService.StaticsFoodSaleCountList(selType.FTypeId, StartTime, EndTime);
                StatFoodList = new ObservableCollection<UIFoodCountItem>();
                foodList.ForEach(food =>
                {
                    food.FoodImg = "pack://siteoforigin:,,,/" + food.FoodImg;
                    food.Count = food.Count == null ? 0 : food.Count;
                    StatFoodList.Add(food);
                });
            }
        }
        private string GetPayWayText(int payWay)
        {
            var result = "";
            switch (payWay)
            {
                case 0: result = "现金支付"; break;
                case 1: result = "微信支付"; break;
                case 2: result = "支付宝支付"; break;
                default: break;
            }
            return result;
        }
        #endregion
        #region 命令
        //统计点餐数据命令
        public ICommand FindStatisticsDataCmd { get; set; }
        //统计指定类别的菜品销售命令
        public ICommand FindStatTypeFoodListCmd { get; set; }
        #endregion
    }
}
