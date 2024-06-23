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
using System.Windows;
using MySelf.QOSM.BLLFactory;

namespace MySelf.QOSM.QuickOrderAPP.ViewModels.Food
{
   public class FoodOrderListViewModel:ViewModelBase
    {
        IFoodTypeService foodTypeService = InstanceFactory.CreateInstance<IFoodTypeService>();
        IFoodService foodService = InstanceFactory.CreateInstance<IFoodService>();
        bool isSettlement = false;//标识当前是否已结算
        #region 属性
        private ObservableCollection<SelFoodItem> selectedFoods;
        //点餐列表
        public ObservableCollection<SelFoodItem> SelectedFoods
        {
            get { return selectedFoods; }
            set { selectedFoods = value; }
        }

        private decimal packAmounts;
        //总打包费
        public decimal PackAmounts
        {
            get { return packAmounts; }
            set
            {
                packAmounts = value;
                OnPropertyChanged();
            }
        }

        private decimal totalAmount;
        //总计金额
        public decimal TotalAmount
        {
            get { return totalAmount; }
            set
            {
                totalAmount = value;
                OnPropertyChanged();
            }
        }
        //菜品类别列表
        public List<UIFType> FoodTypeList { get; set; }
        //选择的类别
        public UIFType SelFType { get; set; }

        private ObservableCollection<UIFoodItem> foodList;
        //菜品列表
        public ObservableCollection<UIFoodItem> FoodList
        {
            get { return foodList; }
            set
            {
                foodList = value;
                OnPropertyChanged();
            }
        }

        private bool isCanSettle;
        //是否可结算
        public bool IsCanSettle
        {
            get { return isCanSettle; }
            set
            {
                isCanSettle = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region 命令
        //清空点餐列表命令
        public ICommand ClearSelectFoodsCmd { get; set; }
        //点餐份数自增1命令
        public ICommand AddCountCmd { get; set; }
        //点餐份数自减1命令
        public ICommand ReduceCountCmd { get; set; }
        //选择菜品类别命令
        public ICommand SelectFTypeCmd { get; set; }
        //点菜响应命令
        public ICommand AddFoodCmd { get; set; }
        //结算按钮命令
        public ICommand FoodSettleCmd { get; set; }
        //修改点餐描述命令
        public ICommand EditSelFoodRemarkCmd { get; set; }
        //移除点餐项命令
        public ICommand DeleteSelFoodCmd { get; set; }
        #endregion

        #region 方法
        private void LoadFoodTypeList()
        {
            List<UIFType> types = foodTypeService.GetCboFType();
            FoodTypeList = types;
            SelFType = types.Count != 0 ? types[0] : new UIFType() { FtypeId = 0, FtypeName = "" };
        }

        private void LoadFoodList()
        {
            List<UIFoodItem> foods = foodService.GetOnlineFoodList(SelFType.FtypeId);
            var list = new ObservableCollection<UIFoodItem>();
            if (foods.Count > 0)
            {
                foods.ForEach(f =>
                {
                    f.FoodImg = "pack://siteoforigin:,,,/" + f.FoodImg;
                    list.Add(f);
                });
            }

            FoodList = list;
        }

        //添加点餐记录
        private void AddFoodRecord(UIFoodItem food)
        {
            AddFoodInfoViewModel addFoodVM = new AddFoodInfoViewModel(food.FoodName, "", 1);
            ShowDialog("addFoodWindow", addFoodVM);
            //点餐项信息封装
            var selFoodItem = new SelFoodItem()
            {
                FoodId = food.FoodId,
                FoodName = food.FoodName,
                FoodRemark = addFoodVM.OrderRemark,
                FoodPrice = food.FoodPrice,
                PackAmount = food.PackAmount,
                SelCount = 1
            };
            SelectedFoods.Add(selFoodItem);//添加到点餐列表
            CommonHelper.hasSelFoods.Add(selFoodItem);
            PackAmounts += selFoodItem.PackAmount;
            TotalAmount += (selFoodItem.FoodPrice + selFoodItem.PackAmount);
            if (!IsCanSettle)
                IsCanSettle = true;
            if (isSettlement)
                isSettlement = false;
        }

        //打开点餐结算窗口
        private void ShowFoodSettleWindow()
        {
            if (ShowQuestion("你确定已经点好菜单了吗？", "结算确认") == MessageBoxResult.OK)
            {
                List<OrderFoodItem> settleList = new List<OrderFoodItem>();
                foreach (var food in SelectedFoods)
                {
                    settleList.Add(new OrderFoodItem()
                    {
                        FoodId = food.FoodId,
                        FoodName = food.FoodName,
                        Count = food.SelCount,
                        Amount = food.FoodPrice * food.SelCount,
                        PackAmounts = food.PackAmount * food.SelCount,
                        OrderRemark = food.FoodRemark
                    });
                }
                //显示结算窗口
                SettlementViewModel settleVM = new SettlementViewModel(settleList);
                ShowDialog("settleWindow", settleVM);
                if (settleVM.IsOrdered)
                {
                    isSettlement = true;//已结算
                    SelectedFoods.Clear();//清空点餐列表
                    CommonHelper.hasSelFoods.Clear();
                    TotalAmount = 0;
                    PackAmounts = 0;
                    IsCanSettle = false;
                }
            }
        }

        //修改点餐描述并更新
        private void EditSelFoodRemark(SelFoodItem selFoodItem)
        {
            AddFoodInfoViewModel editVM = new AddFoodInfoViewModel(selFoodItem.FoodName, selFoodItem.FoodRemark, 2);
            ShowDialog("addFoodWindow", editVM);
            if (selFoodItem.FoodRemark != editVM.OrderRemark)
                selFoodItem.FoodRemark = editVM.OrderRemark;
        }
        #endregion
    }
}
