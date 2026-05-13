using Myself.PMS.Client.Entities;
using Myself.PMS.Client.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.DAL
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

        public string UpdatePassword(int id, string opd, string npd)
        {
            // http://localhost:5273/api/User/update_pwd
            string uri = $"/api/User/update_pwd";

            // 通过Post方式传两个数据进入接口：id  pwd
            Dictionary<string, HttpContent> FormData = new Dictionary<string, HttpContent>();

            FormData.Add("id", new StringContent(id.ToString()));
            FormData.Add("opd", new StringContent(opd));
            FormData.Add("npd", new StringContent(npd));

            var mp = this.GetFormData(FormData);
            string result = this.Post(uri, mp);// Json字符串


            return result;
        }

        public string GetUsers(string key) {

            string uri = "/api/user/list/" + (string.IsNullOrEmpty(key) ? "none" : key);
            return this.Get(uri);
        }

        public string DeleteUser(int id)
        {
            string uri = $"/api/user/delete/{id}";
            return this.Get(uri);
        }

        public string LockUser(int id, int status)
        {
            string uri = $"/api/user/lock/{id}/{status}";
            return this.Get(uri);
        }
        public string UpdateUser(string user_json)
        {
            string uri = "/api/user/update";

            StringContent content = new StringContent(user_json);
            content.Headers.ContentType =
                new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            return this.Post(uri, content);
        }

        public string ResetPassword(string id)
        {
            string uri = "/api/user/reset_pwd";
           
            StringContent content = new StringContent(id);
            content.Headers.ContentType =
                new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            return this.Post(uri, content);
        }

        public string CheckUserName(string username, int id)
        {
            string uri = $"/api/user/check/{id}/{username}";
            return this.Get(uri);
        }

        public string GetUsersByIds(string ids_json)
        {
            string uri = "api/user/ids";

            StringContent content = new StringContent(ids_json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            return this.Post(uri, content);
        }

        public string SaveUserRoles(string roles)
        {
            string uri = "api/user/save_roles";

            StringContent content = new StringContent(roles);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            return this.Post(uri, content);
        }
    }
}
