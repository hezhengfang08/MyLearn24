using Myself.PMS.Server.Entities;

namespace Myself.PMS.Server.IService
{

        public interface IUserService
        {
            SysEmployee? CheckLogin(string username, string password);
        bool UpdatePassword(int id, string old_password, string new_password);
        SysEmployee[] GetUsers(string key);
        SysEmployee[] GetUsersByIds(int[] ids);
        int Update(SysEmployee employee);

        int Delete(int id);

        bool LockUser(int id, int status);
        bool CheckUserName(string username, int id);

        int SaveUserRoles(RoleUser[] roleUser);

        int ResetPassword(int id);
    }
    
}
