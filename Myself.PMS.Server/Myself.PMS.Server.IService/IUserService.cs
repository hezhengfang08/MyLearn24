using Myself.PMS.Server.Entities;

namespace Myself.PMS.Server.IService
{

        public interface IUserService
        {
            SysEmployee? CheckLogin(string username, string password);
        }
    
}
