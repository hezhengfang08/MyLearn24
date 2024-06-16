using MySelf.QOSM.IServices;
using MySelf.QOSM.Models.Entities;
using MySelf.QOSM.Models.UIModels;
using MySelf.QOSM.QuickOrderAPP.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using MySelf.QOSM.BLLFactory;

namespace MySelf.QOSM.QuickOrderAPP.ViewModels.BM
{
  public  class FoodInfoViewModel:InfoVMBase<ViewFoodInfo>
    {
        IFoodTypeService foodTypeService = InstanceFactory.CreateInstance<IFoodTypeService>();
        IFoodService foodService = InstanceFactory.CreateInstance<IFoodService>();
        FoodInfo foodInfo;
        string oldName = "";
        string oldPic = "";
        bool blUpdateImg = false;//标识是否修改了图片
        public FoodInfoViewModel(int actType, ViewFoodInfo food)
        {
            ActType = actType;
            ShowFNameError = Visibility.Hidden;
            LoadFTypeList();//加载菜品类别下拉框
            if (actType == 1)//新增
            {
                foodInfo = new FoodInfo();
                IsRequired = true;
                FtypeId = 0;
                SubmitText = "新增";
            }
            else//修改
            {
                //原有信息呈现
                foodInfo = new FoodInfo()
                {
                    FoodId = food.FoodId,
                    FoodName = food.FoodName,
                    FtypeId = food.FtypeId,
                    FoodPrice = food.FoodPrice,
                    FoodImg = food.FoodImg,
                    IsOn = food.IsOn,
                    PackAmount = food.PackAmount,
                    Remark = food.Remark
                };
                oldName = food.FoodName;
                oldPic = food.FoodImg;
                SubmitText = "修改";
            }

            //命令初始化
            InitCommands();
        }

        private void InitCommands()
        {
            CloseWindowCmd = CreateCloseWindowCmd();
            MoveWindowCmd = CreateMoveWindowCmd();
            ChooseImgCmd = new RelayCommand(o =>
            {
                ChooseFoodImg();//选择图片处理
            });
            ConfirmCmd = new RelayCommand(win =>
            {
                SaveFood(win);
            });
        }

        #region 属性
        private bool isRequired;
        //菜品名称是否提示必填符号
        public bool IsRequired
        {
            get { return isRequired; }
            set
            {
                isRequired = value;
                OnPropertyChanged();
            }
        }

        private Visibility showFNameError;
        //当菜品名称已存在时，显示错误信息
        public Visibility ShowFNameError
        {
            get { return showFNameError; }
            set
            {
                showFNameError = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 编号
        /// </summary>
        public int FoodId
        {
            get { return foodInfo.FoodId; }
            set
            {
                foodInfo.FoodId = value;
            }
        }
        //菜品名称
        public string FoodName
        {
            get { return foodInfo.FoodName; }
            set { foodInfo.FoodName = value; }
        }

        private List<UIFType> fTypeList;
        //菜品类别列表
        public List<UIFType> FTypeList
        {
            get { return fTypeList; }
            set { fTypeList = value; }
        }
        //类别编号
        public int FtypeId
        {
            get { return foodInfo.FtypeId; }
            set
            {
                foodInfo.FtypeId = value;
            }
        }

        //菜品价格
        public decimal FoodPrice
        {
            get { return foodInfo.FoodPrice; }
            set
            {
                foodInfo.FoodPrice = value;
            }
        }

        //菜品打包费
        public decimal PackAmount
        {
            get { return foodInfo.PackAmount; }
            set
            {
                foodInfo.PackAmount = value;
            }
        }

        //菜品图片
        public string FoodImg
        {
            get { return foodInfo.FoodImg; }
            set
            {
                foodInfo.FoodImg = value;
                OnPropertyChanged();
            }
        }

        //菜品是否上架
        public bool IsOn
        {
            get { return foodInfo.IsOn; }
            set
            {
                foodInfo.IsOn = value;
            }
        }

        //菜品描述
        public string Remark
        {
            get { return foodInfo.Remark; }
            set
            {
                foodInfo.Remark = value;
            }
        }


        #endregion

        #region 命令
        //图片选择命令
        public ICommand ChooseImgCmd { get; set; }
        #endregion

        #region 方法
        //加载菜品类别下拉框
        private void LoadFTypeList()
        {
            List<UIFType> types = foodTypeService.GetCboFType();
            types.Insert(0, new UIFType() { FtypeId = 0, FtypeName = "请选择类别" });
            FTypeList = types;
        }
        //图片选择---打开对话框---选择图片---确定
        private void ChooseFoodImg()
        {
            var ofd = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "png 文件(*.png)|*.png| jpg 文件 (*.jpg)|*.jpg|bmp 文件  (*.bmp)|*.bmp"
            };
            var result = ofd.ShowDialog();
            if (result == false) return;
            string fileName = ofd.FileName;//选择的文件名
            if (!string.IsNullOrEmpty(fileName))
                FoodImg = fileName;

        }

        //菜品信息提交处理
        private void SaveFood(object win)
        {
            string conType = ActType == 1 ? "新增" : "修改";
            string msgTitle = $"菜品{conType}";
            //检查信息
            if (string.IsNullOrEmpty(FoodName))
            {
                IsRequired = true;
                if (ShowFNameError == Visibility.Visible)
                    ShowFNameError = Visibility.Hidden;
                return;
            }
            if (ActType == 1 || (ActType == 2 && FoodName != oldName))
            {
                if (foodService.ExistFood(FoodName))
                {
                    ShowFNameError = Visibility.Visible;
                    return;
                }
            }
            if (FtypeId == 0)
            {
                ShowError("请设置菜品类别！", msgTitle);
                return;
            }
            if (FoodPrice == 0 || FoodPrice == 0.00M)
            {
                ShowError("请设置菜品价格！", msgTitle);
                return;
            }
            if (PackAmount == 0 || PackAmount == 0.00M)
            {
                ShowError("请设置菜品打包费！", msgTitle);
                return;
            }
            //处理选择的图片文件及图片地址
            SetUpdateImg();
            //提交处理
            bool blConfirm = foodService.SaveFood(foodInfo);
            if (blConfirm)
            {
                ShowSuccess($"菜品：{FoodName} {conType}成功！", msgTitle);
                //刷新
                ViewFoodInfo vfood = new ViewFoodInfo()
                {
                    FoodId = foodInfo.FoodId,
                    FoodName = foodInfo.FoodName,
                    FtypeId = foodInfo.FtypeId,
                    FtypeName = FTypeList.Find(t => t.FtypeId == FtypeId).FtypeName,
                    FoodImg = "pack://siteoforigin:,,,/" + foodInfo.FoodImg,
                    FoodPrice = foodInfo.FoodPrice,
                    PackAmount = foodInfo.PackAmount,
                    IsOn = foodInfo.IsOn,
                    Remark = foodInfo.Remark
                };
                OnReloadList(vfood);
                //关闭窗口
                CloseWindow(win);
            }
            else
            {
                ShowError($"菜品：{foodInfo} {conType}失败！", msgTitle);
                return;
            }
        }

        private void SetUpdateImg()
        {
            if (oldPic != FoodImg && FoodImg != "")
            {
                blUpdateImg = true;
            }
            string folder = @"imgs/food";
            if (blUpdateImg)
            {
                string fileName = Path.GetFileName(FoodImg);
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);//创建目标文件夹
                }
                string url = folder + "/" + fileName;//图片文件路径（相对）
                if (!string.IsNullOrEmpty(fileName) && !File.Exists(url))
                {
                    File.Copy(FoodImg, url);//图片保存
                    FoodImg = url;
                }
            }
            else if (oldPic == FoodImg && FoodImg != "")
            {
                string fileName = Path.GetFileName(FoodImg);
                FoodImg = folder + "/" + fileName;
            }
            else
                FoodImg = null;
        }
        #endregion
    }
}
