using MySelf.QOSM.BLLFactory;
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
using System.Windows;
using System.Windows.Input;


namespace MySelf.QOSM.QuickOrderAPP.ViewModels.SM
{
    public class UserListViewModel:ListVMBase
    {
        bool oldShowDel = false;
        IUserService userService = InstanceFactory.CreateInstance<IUserService>();
        public UserListViewModel()
        {
            ShowDeleted = true;
            ShowDeleted = false;
            PageInfoVM = new UControl.UPagerViewModel();
            PageInfoVM.PageSize = 10;
            PageInfoVM.PageChanged += (o, e) => LoadUserList();
            LoadUserList();
            InitCommands();


        }
        #region 属性
        private ObservableCollection<UserModel> userList;
        public ObservableCollection<UserModel> UserList
        {
            get {
                return userList;
            }

            set
            {
                userList =value;
                OnPropertyChanged();
            }
        }

        #endregion
        #region 命令
        public ICommand EnableUser { get; set; }
        #endregion
        #region 方法
       private void LoadUserList()
        {
            ObservableCollection<UserModel> users = new ObservableCollection<UserModel>();
            if(HasDeleted != oldShowDel)
            {
                PageInfoVM.CurrentPage = 1;
                oldShowDel = HasDeleted;
            }
            PageModel<ViewUserInfo> res = userService.GetUserList(KeyWords, HasDeleted, PageInfoVM.StartIndex, PageInfoVM.PageSize);
            if(res != null) {
                List<ViewUserInfo> userData = null;
                if (res.DataList.Count > 0)
                {
                    userData = res.DataList;
                    PageInfoVM.TotalCount = res.TotalCount;
                    userData.ForEach( u=> users.Add(new UserModel(u)));
                }
                else
                {
                    PageInfoVM.TotalCount =0 ;
                }
            }
            UserList = users;
        }
        private void ShowUserWindow(int actType, ViewUserInfo user)
        {
            UserInfoViewModel userVM = new UserInfoViewModel(actType, user);
            userVM.ReloadList += UserVM_ReLoadList;
            ShowDialog("userWindow", userVM);
        }
        private void UserVM_ReLoadList(object sender, InfoArgs<ViewUserInfo> e)
        {
            if(e != null)
            {
                if(e.ActType == 1 && UserList.Count < PageInfoVM.PageCount) {
                    UserList.Add(new UserModel(e.Info));
                }
                else if(e.ActType == 2)
                {
                    var userModel = UserList.Where(u=>u.User.UserId == e.Info.UserId).FirstOrDefault();
                    userModel.User = e.Info;//替换
                    if(userModel.ShowEnable == Visibility.Visible)
                    {
                        userModel.EnableText = e.Info.UserState ? "启用" : "停用";
                    }
                }
            }
        }
        private void SetUserState(UserModel model)
        {
            var user = model.User;
            string enableType = user.UserState ? "启用" : "停用";
            string msgTitle = $"{enableType}用户";
            if(ShowQuestion($"确定要{enableType}该用户吗？",msgTitle)==MessageBoxResult.OK)
            {
                bool setState = user.UserState ? false : true;
                bool lbSet = false;
                lbSet = userService.SetUserState(user.UserId, setState);
                if (lbSet)
                {
                    ShowSuccess($"用户：{user.UserName} {enableType}成功！", msgTitle);
                    //刷新
                    user.UserState = setState;
                    model.EnableText = setState ? "停用" : "启用";
                    model.User = user;
                }
                else
                {
                    ShowError($"用户：{user.UserName} {enableType}失败！", msgTitle);
                    return;
                }
            }
        }
        private  void InitCommands()
        {
            AddItemCmd = new RelayCommand(
                win =>
                {
                    ShowUserWindow(1, null);

                });
            EditItemCmd = new RelayCommand(
                u =>
                {
                    ViewUserInfo viewUserInfo = u as ViewUserInfo;
                    ShowUserWindow(2, viewUserInfo);
                });
            FindDataListCmd = new RelayCommand(
                o =>
                {
                    LoadUserList();
                });
            EnableUser = new RelayCommand(
                o =>
                {
                    UserModel user = o as UserModel;
                    SetUserState(user);
                });
            DelItemCmd = new RelayCommand(u =>
            {
                UserModel model = u as UserModel;
                DeleteUser(model, 1);
            });
            RecoverItemCmd = new RelayCommand(u =>
            {
                UserModel model = u as UserModel;
                DeleteUser(model, 2);
            });
            RemoveItemCmd = new RelayCommand(u =>
            {
                UserModel model = u as UserModel;
                DeleteUser(model, 3);
            });
        }
        private void DeleteUser(UserModel model,int delType)
        {
            string info = "用户";
            string delName = CommonHelper.GetDelTypeName(delType);
            string msgTitle = $"{info}{delName}";
            if(ShowQuestion($"你确定要{delName}该用户吗？",msgTitle)  == MessageBoxResult.OK) { 
                ViewUserInfo user = model.User;
                bool blResult = false;
                switch(delType)
                {
                    case 1:
                        blResult = userService.DeleteUser(user.UserId);
                        break;
                        case 2:
                        if (userService.ExistUser(user.UserName))
                        {
                            ShowError("已存在与该账号相同的用户，不能恢复！", msgTitle);
                            return;
                        }
                        blResult = userService.RecoverUser(user.UserId);
                        break;
                    case 3:
                        blResult = userService.RemoveUser(user.UserId);
                        break;
                    default: break;
                }
                if (blResult)
                {
                    ShowSuccess($"该{info}{delName}成功！", msgTitle);
                    //刷新---用户信息从当前列表中移除
                   if(delType != 2) UserList.Remove(model);
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
