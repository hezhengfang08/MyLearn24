using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.PMS.Client.IDAL
{
    public interface IUserAccess : IWebAccess
    {
        string Login(string username, string password);
    }
}
