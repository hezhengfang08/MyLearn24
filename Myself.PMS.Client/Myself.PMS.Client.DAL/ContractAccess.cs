using Myself.PMS.Client.Entities;
using Myself.PMS.Client.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.DAL
{
    public class ContractAccess : WebAccess, IContractAccess
    {
        public ContractAccess(GlobalValues globalValues) : base(globalValues)
        {
        }

        public string GetDatas(string key, string start, string end, int index, int size)
        {
            key = string.IsNullOrEmpty(key) ? "none" : key;
            string uri = $"/api/contract/page/{key}/{start}/{end}/{index}/{size}";
            return this.Get(uri);
        }

        public string ChangeState(int id, int state)
        {
            string uri = $"/api/contract/state/{id}/{state}";
            return this.Get(uri);
        }

        public string DeleteInfo(int id)
        {
            string uri = $"/api/contract/delete/{id}";
            return this.Get(uri);
        }

        public string UpdateInfo(string ceJson)
        {
            string uri = "/api/contract/update";

            return this.PostJson(uri, ceJson);
        }

        public string Execute(string ceJson)
        {
            string uri = "/api/contract/execute";
            return this.PostJson(uri, ceJson);
        }

        public string Archived(string ceJson)
        {
            string uri = "/api/contract/archived";
            return this.PostJson(uri, ceJson);
        }
    }
}
