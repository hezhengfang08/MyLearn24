using MySelf.PMS.Client.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.PMS.Client.DAL
{
    public class UserAccess : WebAccess, IUserAccess
    {
        public string Login(string username, string password)
        {
            string uri = $"/api/User?un={username}&pw={password}";
            string result = this.Get(uri);

            return result;
        }
    }
}
