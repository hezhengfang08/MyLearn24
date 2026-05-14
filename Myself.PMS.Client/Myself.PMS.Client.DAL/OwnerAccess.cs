using Myself.PMS.Client.Entities;
using Myself.PMS.Client.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.DAL
{
    public class OwnerAccess : WebAccess, IOwnerAccess
    {
        public OwnerAccess(GlobalValues globalValues)
           : base(globalValues)
        {
        }
        public string GetBuildings()
        {
            string uri = "/api/owner/buildings";
            return this.Get(uri);
        }
        public string GetOwners(string paramsJson, int index, int size)
        {
            string uri = $"/api/owner/page/{index}/{size}";
            return this.PostJson(uri, paramsJson);
        }
        public string GetQuarters()
        {
            string uri = "/api/owner/quarters";
            return this.Get(uri);
        }
        public string UpdateOwner(string ownerJson)
        {
            string uri = $"/api/owner/update";



            return this.PostJson(uri, ownerJson);
        }

        public string DeleteOwner(int id)
        {
            string uri = $"/api/owner/delete/{id}";
            var content = new StringContent(string.Empty);
            return this.Post(uri, content);
        }
    }
}
