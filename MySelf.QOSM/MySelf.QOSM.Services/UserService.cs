using MySelf.QOSM.IServices;
using MySelf.QOSM.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.QOSM.Services
{
    public class UserService : IUserService
    {
        public UserInfo AdminLogin(string username, string password)
        {
            return new UserInfo() { UserName = "admin", UserState = true };
        }
    }
}
