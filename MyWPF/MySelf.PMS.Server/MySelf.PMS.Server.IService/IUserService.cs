using MySelf.PMS.Server.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.PMS.Server.IService
{
    public interface IUserService
    {
        SysEmployee? CheckLogin(string username, string password);
    }
}
