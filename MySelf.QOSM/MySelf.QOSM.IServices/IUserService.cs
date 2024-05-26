using MySelf.QOSM.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.QOSM.IServices
{
    public interface IUserService
    {
        //管理员登录方法
        UserInfo AdminLogin(string username, string password);
    }
}
