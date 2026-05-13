using Myself.PMS.Client.Entities;
using Myself.PMS.Client.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.DAL
{
    public class BaseInfoAccess : WebAccess, IBaseInfoAccess
    {
        public BaseInfoAccess(GlobalValues globalValues) : base(globalValues)
        {
        }

        public string CancelState(int id)
        {
            string uri = $"/api/bi/cancel/{id}";
            var content = new StringContent(string.Empty);
            return this.Post(uri, content);
        }

        public string DeleteInfo(int id)
        {
            string uri = $"/api/bi/delete/{id}";
            var content = new StringContent(string.Empty);
            return this.Post(uri, content);
        }

        public string GetInfoPage(string key, int index, int size)
        {
            key = string.IsNullOrEmpty(key) ? "none" : key;
            string uri = $"/api/bi/page/{key}/{index}/{size}";
            return this.Get(uri);
        }

        public string PublishState(int id)
        {
            string uri = $"/api/bi/publish/{id}";
            var content = new StringContent(string.Empty);
            return this.Post(uri, content);
        }

        public string RevokeState(int id)
        {
            string uri = $"/api/bi/revoke/{id}";
            var content = new StringContent(string.Empty);
            return this.Post(uri, content);
        }

        public string UpdateInfo(string infoJson)
        {
            string uri = "/api/bi/update";
            return this.PostJson(uri, infoJson);
        }
    }
}
