using MySelf.QOSM.BLLFactory;
using MySelf.QOSM.IServices;
using MySelf.QOSM.Models.Entities;
using MySelf.QOSM.QuickOrderAPP.Utils;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MySelf.QOSM.QuickOrderAPP.ViewModels.SM
{
    public class RoleListViewModel:ListVMBase
    {
        IRoleService roleService = InstanceFactory.CreateInstance<IRoleService>();
        public RoleListViewModel()
        {
            ShowDeleted = false;
            ShowUnDel = true;
            LoadRoleList();
            InitCommands();
        }

        #region 属性
        private ObservableCollection<RoleModel> roleList;
        //角色列表
        public ObservableCollection<RoleModel> RoleList
        {
            get { return roleList; }
            set
            {
                roleList = value;
                OnPropertyChanged();
            }
        }

        #endregion
        #region 方法
        private void LoadRoleList()
        {
            ObservableCollection<RoleModel> list = new ObservableCollection<RoleModel>();
            List<RoleInfo> roles = roleService.GetRoleList(HasDeleted);
            roles.ForEach(r=>list.Add(new RoleModel(r)));
            RoleList = list;
        }
        private void ShowRoleWindow(int actType, RoleInfo roleInfo)
        {
            RoleInfoViewModel roleVm = new RoleInfoViewModel(actType,  roleInfo);
            roleVm.ReloadList += RoleVm_ReloadList;
            ShowDialog("roleWindow", roleVm);

        }

        private void RoleVm_ReloadList(object sender, InfoArgs<RoleInfo> e)
        {
           if (e != null)
            {
                if (e.ActType == 1)
                {
                    RoleList.Add(new RoleModel(e.Info));
                }
                else
                {
                    RoleList.Where(r => r.Role.RoleId == e.Info.RoleId).First().Role = e.Info; //替换它的显示
                }
            }
        }
        private void InitCommands()
        {
            FindDataListCmd = new RelayCommand(
                w =>
                {
                    LoadRoleList();
                });
            AddItemCmd = new RelayCommand(
                w =>
                {
                    ShowRoleWindow(1, null);
                });
            EditItemCmd = new RelayCommand(w =>
            {
                RoleInfo roleInfo = w as RoleInfo;
                ShowRoleWindow(2, roleInfo);
            });
            DelItemCmd = new RelayCommand(r =>
            {
                RoleModel model = r as RoleModel;
                HandleRole(model, 1);
            });
            RecoverItemCmd = new RelayCommand(r =>
            {
                RoleModel model = r as RoleModel;
                HandleRole(model, 2);
            });
            RemoveItemCmd = new RelayCommand(r =>
            {
                RoleModel model = r as RoleModel;
                HandleRole(model, 3);
            });
        }
        private void HandleRole(RoleModel model, int handleType)
        {
            string info = "角色";
            string delName = CommonHelper.GetDelTypeName(handleType);
            string msgTitle = $"{info}{delName}";
            if(ShowQuestion($"确定要{delName}该{info}吗？",msgTitle)== MessageBoxResult.OK)
            {
                RoleInfo role = model.Role;
                bool blResult = false;
                switch (handleType)
                {
                    case 1://逻辑删除
                        if (roleService.HasRoleUsers(role.RoleId))
                        {
                            ShowError("该角色的相关用户已存在，不能删除！", msgTitle);
                            return;
                        }
                        blResult = roleService.DeleteRole(role);
                        break;
                    case 2://恢复 
                        if (roleService.ExistRole(role.RoleName))
                        {
                            ShowError("已存在与该名称相同的角色，不能恢复！", msgTitle);
                            return;
                        }
                        blResult = roleService.RecoveryRole(role);
                        break;
                    case 3://真删除
                        blResult = roleService.RemoveRole(role);
                        break;
                    default: break;
                }
                if (blResult)
                {
                    ShowSuccess($"该{info}{delName}成功！", msgTitle);
                    //刷新
                    RoleList.Remove(model);
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
