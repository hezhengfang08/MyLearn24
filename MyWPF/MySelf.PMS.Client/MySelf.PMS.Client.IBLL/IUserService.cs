using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.PMS.Client.IBLL
{
    public interface IUserService
    {
        bool Login(string username, string password);
    }
}
