using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.PMS.Server.IService
{
    public interface IUserService
    {
        bool CheckLogin(string username, string password);
    }
}
