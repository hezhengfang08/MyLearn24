using Myself.PMS.Client.Entities;
using Myself.PMS.Client.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.DAL
{
    public class MenuAccess : WebAccess, IMenuAccess
    {
        public MenuAccess(GlobalValues globalValues) : base(globalValues)
        {
        }

        public string GetAllMenus(string key)
        {
            key = string.IsNullOrEmpty(key) ? "none" : key;
            string url = $"/api/Menu/all/{key}";
            return this.Get(url);
        }
        public string UpdateMenu(string menuJson)
        {
            string uri = "api/menu/update";

            StringContent content = new StringContent(menuJson);
            content.Headers.ContentType =
                new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            return this.Post(uri, content);
        }

        public string DeleteMenu(string id)
        {
            string uri = $"api/menu/delete/{id}";
            return this.Get(uri);
        }
    }
}
