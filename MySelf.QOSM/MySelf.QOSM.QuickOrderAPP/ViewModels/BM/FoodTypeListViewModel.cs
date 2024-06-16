using MySelf.QOSM.BLLFactory;
using MySelf.QOSM.IServices;
using MySelf.QOSM.Models.Entities;
using MySelf.QOSM.Models.UIModels;
using MySelf.QOSM.QuickOrderAPP.Utils;
using MySelf.QOSM.QuickOrderAPP.ViewModels.SM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MySelf.QOSM.QuickOrderAPP.ViewModels.BM
{
  public  class FoodTypeListViewModel:ListVMBase
    {
        IFoodTypeService foodTypeService = InstanceFactory.CreateInstance<IFoodTypeService>();
        bool oldShowDel = false;
        public FoodTypeListViewModel()
        {
            //页面初始化
            ShowUnDel = true;
            ShowDeleted = false;
            PageInfoVM = new UControl.UPagerViewModel();
            PageInfoVM.PageSize = 10;
            PageInfoVM.PageChanged += (o, e) => LoadFoodTypeList();
            LoadFoodTypeList();
            //命令初始化
            InitCommands();
        }

        private void InitCommands()
        {
            AddItemCmd = new RelayCommand(o =>
            {
                ShowFTypeWindow(1, null);
            });
            EditItemCmd = new RelayCommand(info =>
            {
                FoodTypeInfo typeInfo = info as FoodTypeInfo;
                ShowFTypeWindow(2, typeInfo);
            });
            FindDataListCmd = new RelayCommand(o =>
            {
                LoadFoodTypeList();
            });
            DelItemCmd = new RelayCommand(t =>
            {
                FoodTypeModel model = t as FoodTypeModel;
                DeleteFType(model, 1);
            });
            RecoverItemCmd = new RelayCommand(t =>
            {
                FoodTypeModel model = t as FoodTypeModel;
                DeleteFType(model, 2);
            });
            RemoveItemCmd = new RelayCommand(t =>
            {
                FoodTypeModel model = t as FoodTypeModel;
                DeleteFType(model, 3);
            });
        }

        #region 属性
        private ObservableCollection<FoodTypeModel> foodTypeList;
        //菜品类别列表
        public ObservableCollection<FoodTypeModel> FoodTypeList
        {
            get { return foodTypeList; }
            set
            {
                foodTypeList = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region 方法
        //查询菜品类别列表
        private void LoadFoodTypeList()
        {
            //调用查询方法来获取菜品类别数据---FoodTypeList
            ObservableCollection<FoodTypeModel> list = new ObservableCollection<FoodTypeModel>();
            if (HasDeleted != oldShowDel)
            {
                PageInfoVM.CurrentPage = 1;
                oldShowDel = HasDeleted;
            }
            PageModel<FoodTypeInfo> result = foodTypeService.GetFoodTypeList(KeyWords, HasDeleted, PageInfoVM.StartIndex, PageInfoVM.PageSize);
            if (result != null)
            {
                List<FoodTypeInfo> ftypeData = null;
                if (result.DataList.Count > 0)
                {
                    ftypeData = result.DataList;
                    PageInfoVM.TotalCount = result.TotalCount;//总记录数
                    ftypeData.ForEach(t => list.Add(new FoodTypeModel(t)));
                }
                else
                {
                    PageInfoVM.TotalCount = 0;
                }
            }
            FoodTypeList = list;
        }

        //显示菜品类别信息窗口
        private void ShowFTypeWindow(int actType, FoodTypeInfo foodTypeInfo)
        {
            FoodTypeInfoViewModel typeVM = new FoodTypeInfoViewModel(actType, foodTypeInfo);
            typeVM.ReloadList += TypeVM_ReloadList;//刷新数据到列表
            ShowDialog("foodTypeWindow", typeVM);
        }

        private void TypeVM_ReloadList(object sender, InfoArgs<FoodTypeInfo> e)
        {
            if (e != null)
            {
                if (e.ActType == 1 && FoodTypeList.Count < PageInfoVM.PageSize)
                {
                    FoodTypeList.Add(new FoodTypeModel(e.Info));
                }
                else if (e.ActType == 2)
                {
                    var typeModel = FoodTypeList.Where(t => t.FType.FtypeId == e.Info.FtypeId).First();
                    typeModel.FType = e.Info;//替换
                }
            }
        }

        //处理菜品类别信息的删除、恢复、移除
        private void DeleteFType(FoodTypeModel model, int delType)
        {
            string info = "菜品类别";
            //操作名称
            string delName = CommonHelper.GetDelTypeName(delType);
            string msgTitle = $"{info}{delName}";
            if (ShowQuestion($"你确定要{delName}该{info}吗？", msgTitle) == MessageBoxResult.OK)
            {
                FoodTypeInfo type = model.FType;//类别信息对象
                bool blResult = false;//操作结果
                switch (delType)
                {
                    case 1://逻辑删除
                        if (foodTypeService.IsHasFoods(type.FtypeId))//判断指定类别是否已添加了菜品信息
                        {
                            ShowError("该菜品类别已添加了菜品，不能删除！", msgTitle);
                            return;
                        }
                        blResult = foodTypeService.DeleteFType(type);
                        break;
                    case 2://恢复 
                        if (foodTypeService.ExistFType(type.FtypeName))//检查是否有同名类别
                        {
                            ShowError("已存在与该名称相同的菜品类别，不能恢复！", msgTitle);
                            return;
                        }
                        blResult = foodTypeService.RecoveryFType(type);
                        break;
                    case 3://真删除
                        blResult = foodTypeService.RecoveryFType(type);
                        break;
                    default: break;
                }
                if (blResult)
                {
                    ShowSuccess($"该{info}{delName}成功！", msgTitle);
                    //刷新---类别信息从当前列表中移除
                    FoodTypeList.Remove(model);
                }
                else
                {
                    ShowError($"该{info}{delName}失败！", msgTitle);
                    return;
                }
            }
        }
        #endregion
    }
}

