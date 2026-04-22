using Myself.SmartParking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.SmartParing.IService
{
    public interface IUserService:IBaseService
    {
        SysUser Login(string username, string password);
        IEnumerable<SysUser> GetUsers(string key);
        bool CheckUserName(string userName);
    }
}
