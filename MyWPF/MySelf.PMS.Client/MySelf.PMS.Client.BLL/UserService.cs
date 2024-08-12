using MySelf.PMS.Client.IBLL;
using MySelf.PMS.Client.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.PMS.Client.BLL
{
    public class UserService : IUserService
    {
        IUserAccess _userAccess;
        public UserService(IUserAccess userAccess)
        {
            _userAccess = userAccess;
        }
        public bool Login(string username, string password)
        {
            return _userAccess.Login(username, password);
        }
    }
}
