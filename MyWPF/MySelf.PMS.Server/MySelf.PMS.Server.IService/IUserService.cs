using MySelf.PMS.Server.Entities;

namespace MySelf.PMS.Server.IService
{
    public interface IUserService
    {
        SysEmployee? CheckLogin(string username, string password);
        bool UpdatePassword(int id,string old_password,string new_password);
        SysEmployee[] GetUsers(string key);
    }
}
