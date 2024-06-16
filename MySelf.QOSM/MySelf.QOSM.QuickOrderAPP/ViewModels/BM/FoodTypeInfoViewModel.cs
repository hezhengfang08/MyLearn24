using MySelf.QOSM.BLLFactory;
using MySelf.QOSM.IServices;
using MySelf.QOSM.Models.Entities;
using MySelf.QOSM.QuickOrderAPP.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MySelf.QOSM.QuickOrderAPP.ViewModels.BM
{
   public class FoodTypeInfoViewModel:InfoVMBase<FoodTypeInfo>
    {
        IFoodTypeService foodTypeService = InstanceFactory.CreateInstance<IFoodTypeService>();
        FoodTypeInfo ftypeInfo;
        string oldName = "";//存储修改的名称
        public FoodTypeInfoViewModel(int actType, FoodTypeInfo fType)
        {
            ActType = actType;
            ShowTNameError = Visibility.Hidden;
            if (actType == 1)
            {
                ftypeInfo = new FoodTypeInfo();
                SubmitText = "新增";
                IsRequired = true;
            }
            else
            {
                ftypeInfo = fType;
                SubmitText = "修改";
                oldName = fType.FtypeName;
            }
            //命令初始化
            InitCommands();
        }

        private void InitCommands()
        {
            MoveWindowCmd = CreateMoveWindowCmd();
            CloseWindowCmd = CreateCloseWindowCmd();
            ConfirmCmd = new RelayCommand(win =>
            {
                SaveFoodType(win);
            });
        }

        #region 属性
        private bool isRequired;
        //类别名称是否提示必填符号
        public bool IsRequired
        {
            get { return isRequired; }
            set
            {
                isRequired = value;
                OnPropertyChanged();
            }
        }

        private Visibility showTNameError;
        //当类别名称已存在时，显示错误信息
        public Visibility ShowTNameError
        {
            get { return showTNameError; }
            set
            {
                showTNameError = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 编号
        /// </summary>
        public int FtypeId
        {
            get { return ftypeInfo.FtypeId; }
            set
            {
                ftypeInfo.FtypeId = value;
            }
        }
        //类别名称
        public string FtypeName
        {
            get { return ftypeInfo.FtypeName; }
            set { ftypeInfo.FtypeName = value; }
        }
        //描述
        public string Remark
        {
            get { return ftypeInfo.Remark; }
            set { ftypeInfo.Remark = value; }
        }
        #endregion

        #region 方法
        private void SaveFoodType(object win)
        {
            string conType = ActType == 1 ? "新增" : "修改";
            string msgTitle = $"菜品类别{conType}";
            //检查信息
            if (string.IsNullOrEmpty(FtypeName))
            {
                IsRequired = true;
                return;
            }
            if (ActType == 1 || (ActType == 2 && FtypeName != oldName))
            {
                if (foodTypeService.ExistFType(FtypeName))
                {
                    ShowTNameError = Visibility.Visible;
                    return;
                }
            }
            //提交处理
            bool blConfirm = foodTypeService.SaveFType(ftypeInfo);
            if (blConfirm)
            {
                ShowSuccess($"菜品类别：{FtypeName} {conType}成功！", msgTitle);
                //刷新
                OnReloadList(ftypeInfo);
                //关闭窗口
                CloseWindow(win);
            }
            else
            {
                ShowError($"菜品类别：{FtypeName} {conType}失败！", msgTitle);
                return;
            }
        }
        #endregion
    }
}
