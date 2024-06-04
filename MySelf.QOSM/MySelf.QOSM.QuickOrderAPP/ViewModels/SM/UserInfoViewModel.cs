using MySelf.QOSM.BLLFactory;
using MySelf.QOSM.IServices;
using MySelf.QOSM.Models.Entities;
using MySelf.QOSM.Models.UIModels;
using MySelf.QOSM.QuickOrderAPP.Utils;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MySelf.QOSM.QuickOrderAPP.ViewModels.SM
{
    public class UserInfoViewModel:InfoVMBase<ViewUserInfo>
    {
        IRoleService roleService = InstanceFactory.CreateInstance<IRoleService>();
        IUserService userService = InstanceFactory.CreateInstance<IUserService>();
        UserInfo userInfo;
        string oldName = "";
        public UserInfoViewModel (int actType, ViewUserInfo vuser)
        {
            ActType = actType;
            ShowUNameError = Visibility.Hidden;
            ShowTime = Visibility.Collapsed;
            LoadCboRoles();
            if(ActType == 1)
            {
                userInfo = new UserInfo();
                IsRequired = true;
                IsPRequired = true;
                RoleId = 0;
                UserState = true;
                SubmitText = "新增";
            }
            else
            {
                userInfo = new UserInfo()
                {
                    UserId = vuser.UserId,
                    UserName = vuser.UserName,
                    RoleId = vuser.RoleId,
                    Phone = vuser.Phone,
                    CreateTime = vuser.CreateTime,
                    UserState = vuser.UserState
                };
                oldName = vuser.UserName;
                ShowTime = Visibility.Visible;
                SubmitText = "修改";
            }
            //命令初始化
            InitCommands();

        }
        #region 属性
        private bool isRequired;

        public bool IsRequired
        {
            get { return isRequired; }
            set { isRequired = value;
                OnPropertyChanged();
            }
        }
        private Visibility showUNameError;

        public Visibility ShowUNameError
        {
            get { return showUNameError; }
            set { showUNameError = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 编号
        /// </summary>
        public int UserId
        {
            get { return userInfo.UserId; }
            set
            {
                userInfo.UserId = value;
            }
        }
        //用户名
        public string UserName
        {
            get { return userInfo.UserName; }
            set { userInfo.UserName = value; }
        }
        //用户密码
        public string UserPwd
        {
            get { return userInfo.UserPwd; }
            set { userInfo.UserPwd = value; }
        }
        private bool  isPRequired;

        public bool IsPRequired
        {
            get { return isPRequired; }
            set { isPRequired = value;
                OnPropertyChanged();
            }
        }
        private List<UIRole> roleList;

        public List<UIRole> RoleList
        {
            get { return roleList; }
            set { roleList = value; }
        }
        private int roleId;

        public int RoleId
        {
            get { return roleId; }
            set { roleId = value; }
        }

        //手机号码
        public string Phone
        {
            get { return userInfo.Phone; }
            set { userInfo.Phone = value; }
        }
        //创建时间
        public string CreateTime
        {
            get
            {
                if (userInfo.CreateTime != DateTime.MinValue)
                    return userInfo.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
                else
                    return null;
            }
        }

        private Visibility showTime;

        public Visibility ShowTime
        {
            get { return showTime; }
            set { showTime = value; }
        }
        //用户状态
        public bool UserState
        {
            get { return userInfo.UserState; }
            set { userInfo.UserState = value; }
        }

        #endregion
        #region 方法
        private void LoadCboRoles()
        {
            List<UIRole> roles = roleService.GetCboRoles();
            roles.Insert(0, new UIRole() { RoleId = 0, RoleName = "请选择" });
            RoleList = roles;
        }

        private void SaveUser(object win)
        {
            string conType = ActType == 1? "新增":"修改";
            string msgTitle = $"用户{conType}";
            if (string.IsNullOrEmpty(UserName))
            {
                IsRequired = true;
                if (ShowUNameError == Visibility.Collapsed)
                    ShowUNameError = Visibility.Visible;
                return;
            }
            if(ActType == 1 ||(ActType ==2 && UserName != oldName) ){
                if (userService.ExistUser(UserName))
                {
                    ShowUNameError = Visibility.Visible;
                    return;
                }
            }
            if(string.IsNullOrEmpty(UserPwd) && ActType == 1)
            {
                IsPRequired = true;
                return;
            }
            if(RoleId == 0)
            {
                ShowError("请设置用户角色");
                return;
            }
            bool blConfirm = userService.SaveUser(userInfo);
            if (blConfirm)
            {
                ShowSuccess($"用户：{UserName} {conType}成功！", msgTitle);
                //刷新
                ViewUserInfo vuser = new ViewUserInfo()
                {
                    UserId = userInfo.UserId,
                    UserName = userInfo.UserName,
                    RoleId = userInfo.RoleId,
                    RoleName = RoleList.Find(r => r.RoleId == RoleId).RoleName,
                    Phone = userInfo.Phone,
                    CreateTime = userInfo.CreateTime,
                    UserState = userInfo.UserState
                };
                OnReloadList(vuser);
                //关闭窗口
                CloseWindow(win);
            }
            else
            {
                ShowError($"用户：{UserName} {conType}失败！", msgTitle);
                return;
            }
        }
        private void InitCommands()
        {
            MoveWindowCmd = CreateMoveWindowCmd();
            CloseWindowCmd = CreateCloseWindowCmd();
            ConfirmCmd = new RelayCommand(win =>
            {
                SaveUser(win);
            });
        }
        #endregion
    }
}
