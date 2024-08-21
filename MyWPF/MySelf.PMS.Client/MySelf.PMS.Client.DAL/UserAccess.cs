using MySelf.PMS.Client.Entities;
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
        public UserAccess(GlobalValues globalValues) : base(globalValues)
        {
        }
        public string Login(string username, string password)
        {
            //string uri = $"/api/User?un={username}&pw={password}";
            string uri = $"/api/User/login";
            Dictionary<string, HttpContent> FormData = new Dictionary<string, HttpContent>();
            FormData.Add("un", new StringContent(username));
            FormData.Add("pw", new StringContent(password));
            var mp = this.GetFormData(FormData);
            string result = this.Post(uri, mp);// Json字符串
            return result;
        }
    }
}
