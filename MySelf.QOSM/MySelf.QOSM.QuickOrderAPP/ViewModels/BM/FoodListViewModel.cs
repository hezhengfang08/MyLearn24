using MySelf.QOSM.IServices;
using MySelf.QOSM.Models.Entities;
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

namespace MySelf.QOSM.QuickOrderAPP.ViewModels.BM
{
   public class FoodListViewModel:ListVMBase
    {
        IFoodTypeService foodTypeService =InstanceFactory.CreateInstance<IFoodTypeService>();
        IFoodService foodService = InstanceFactory.CreateInstance<IFoodService>();
        bool oldShowDel = false;
        public FoodListViewModel()
        {
            ShowUnDel = true;
            ShowDeleted = false;
            IsCheckAll = false;
            PageInfoVM = new UControl.UPagerViewModel();
            PageInfoVM.PageSize = 10;
            PageInfoVM.PageChanged += (o, e) => LoadFoodList();
            //加载类别下拉框
            LoadFTypeList();
            FtypeId = 0;
            LoadFoodList();
            //命令初始化
            InitCommands();

        }

        private void InitCommands()
        {
            AddItemCmd = new RelayCommand(o =>
            {
                ShowFoodWindow(1, null);
            });
            EditItemCmd = new RelayCommand(f =>
            {
                ViewFoodInfo food = f as ViewFoodInfo;
                ShowFoodWindow(2, food);
            });
            FindDataListCmd = new RelayCommand(o =>
            {
                LoadFoodList();
            });
            DelItemCmd = new RelayCommand(f =>
            {
                FoodModel model = f as FoodModel;
                DeleteFood(model, 1);
            });
            RecoverItemCmd = new RelayCommand(f =>
            {
                FoodModel model = f as FoodModel;
                DeleteFood(model, 2);
            });
            RemoveItemCmd = new RelayCommand(f =>
            {
                FoodModel model = f as FoodModel;
                DeleteFood(model, 3);
            });
            OnFoodsCmd = new RelayCommand(o =>
            {
                SetFoodsOnStates(true);
            });
            DownFoodsCmd = new RelayCommand(o =>
            {
                SetFoodsOnStates(false);
            });

            CheckAllCmd = new RelayCommand(o =>
            {
                if (IsCheckAll.HasValue)
                {
                    foreach (var food in FoodList)
                    {
                        food.IsCheck = IsCheckAll.Value;
                    }
                }
            });

            CheckItemCmd = new RelayCommand(o =>
            {
                int count = FoodList.Where(f => f.IsCheck == true).Count();//选中了多少行
                if (count == 0)
                    IsCheckAll = false;
                else if (count < FoodList.Count)
                    IsCheckAll = null;
                else
                    IsCheckAll = true;
            });
        }

        #region 属性
        private bool? isCheckAll;
        //是否全选
        public bool? IsCheckAll
        {
            get { return isCheckAll; }
            set
            {
                isCheckAll = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<FoodModel> foodList;
        //菜品列表
        public ObservableCollection<FoodModel> FoodList
        {
            get { return foodList; }
            set
            {
                foodList = value;
                OnPropertyChanged();
            }
        }
        //类别列表
        public List<UIFType> FTypeList { get; set; }
        //类别编号
        public int FtypeId { get; set; }

        #endregion

        #region 命令
        //菜品上架命令
        public ICommand OnFoodsCmd { get; set; }
        //菜品下架命令
        public ICommand DownFoodsCmd { get; set; }
        //全选或全不选
        public ICommand CheckAllCmd
        {
            get; set;
        }
        //行选中命令
        public ICommand CheckItemCmd { get; set; }

        #endregion

        #region 方法
        private void LoadFTypeList()
        {
            List<UIFType> types = foodTypeService.GetCboFType();
            types.Insert(0, new UIFType() { FtypeId = 0, FtypeName = "请选择类别" });
            FTypeList = types;
        }

        //查询菜品列表方法
        private void LoadFoodList()
        {
            //调用查询方法来获菜品数据---FoodList
            ObservableCollection<FoodModel> list = new ObservableCollection<FoodModel>();
            if (HasDeleted != oldShowDel)
            {
                PageInfoVM.CurrentPage = 1;
                oldShowDel = HasDeleted;
            }
            PageModel<ViewFoodInfo> result = foodService.GetFoodList(KeyWords, FtypeId, HasDeleted, PageInfoVM.StartIndex, PageInfoVM.PageSize);
            if (result != null)
            {
                List<ViewFoodInfo> foodData = null;
                if (result.DataList.Count > 0)
                {
                    foodData = result.DataList;

                    PageInfoVM.TotalCount = result.TotalCount;//总记录数
                    foodData.ForEach(f =>
                    {
                        if (!string.IsNullOrEmpty(f.FoodImg))
                            f.FoodImg = "pack://siteoforigin:,,,/" + f.FoodImg;
                    });
                    foodData.ForEach(f => list.Add(new FoodModel(f)));
                }
                else
                {
                    PageInfoVM.TotalCount = 0;
                }
            }
            FoodList = list;
        }

        //显示菜品信息窗口
        private void ShowFoodWindow(int actType, ViewFoodInfo food)
        {
            FoodInfoViewModel foodVM = new FoodInfoViewModel(actType, food);
            foodVM.ReloadList += FoodVM_ReloadList;
            ShowDialog("foodWindow", foodVM);
        }

        private void FoodVM_ReloadList(object sender, InfoArgs<ViewFoodInfo> e)
        {
            if (e != null)
            {
                if (e.ActType == 1 && FoodList.Count < PageInfoVM.PageSize)
                {
                    FoodList.Add(new FoodModel(e.Info));
                }
                else if (e.ActType == 2)
                {
                    var foodModel = FoodList.Where(f => f.Food.FoodId == e.Info.FoodId).First();
                    foodModel.Food = e.Info;//替换
                    foodModel.IsOn = e.Info.IsOn;
                    foodModel.IsDel = e.Info.IsOn ? false : true;
                }
            }
        }

        //菜品信息的删除、恢复、移除处理
        private void DeleteFood(FoodModel model, int delType)
        {
            string info = "菜品";
            //操作名称
            string delName = CommonHelper.GetDelTypeName(delType);
            string msgTitle = $"{info}{delName}";
            if (ShowQuestion($"你确定要{delName}该{info}吗？", msgTitle) == MessageBoxResult.OK)
            {
                ViewFoodInfo food = model.Food;//菜品信息对象
                bool blResult = false;//操作结果
                switch (delType)
                {
                    case 1://逻辑删除
                        blResult = foodService.DeleteFood(food.FoodId);//能够点击删除的，一定是下架状态
                        break;
                    case 2://恢复 
                        if (foodService.ExistFood(food.FoodName))//检查是否有同名的菜品
                        {
                            ShowError("已存在与该名称相同的菜品，不能恢复！", msgTitle);
                            return;
                        }
                        blResult = foodService.RecoveryFood(food.FoodId);
                        break;
                    case 3://真删除
                        blResult = foodService.RemoveFood(food.FoodId);
                        break;
                    default: break;
                }
                if (blResult)
                {
                    ShowSuccess($"该{info}{delName}成功！", msgTitle);
                    //刷新---菜品信息从当前列表中移除
                    FoodList.Remove(model);
                }
                else
                {
                    ShowError($"该{info}{delName}失败！", msgTitle);
                    return;
                }
            }
        }

        //设置菜品的IsOn属性值-----上架或下架
        private void SetFoodsOnStates(bool isOn)
        {
            string setText = isOn ? "上架" : "下架";
            string msgTtile = $"菜品{setText}";
            var foods = FoodList.Where(f => f.IsCheck).ToList();
            if (foods.Count > 0)
            {
                if (ShowQuestion($"你确定要{setText}选择的菜品吗？", msgTtile) == MessageBoxResult.OK)
                {
                    bool blSet = false;
                    List<int> foodIds = foods.Select(f => f.Food.FoodId).ToList();
                    blSet = foodService.SetFoodOnline(foodIds, isOn);
                    if (blSet)
                    {
                        ShowSuccess($"选择的菜品{setText}成功！", msgTtile);
                        //刷新
                        foods.ForEach(f =>
                        {
                            f.IsOn = isOn;
                            f.IsDel = isOn ? false : true;
                            f.IsCheck = false;
                        });
                        IsCheckAll = false;
                    }
                }
            }
            else
            {
                ShowError($"请选择{setText}的菜品！", msgTtile);
                return;
            }
        }


        #endregion
    }
}
