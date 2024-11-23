using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.PMS.Client.Entities
{
    public class EmployeeEntity
    {
        public int eId { get; set; }

        public string userName { get; set; }

        public string realName { get; set; }

        public string password { get; set; }

        public int status { get; set; }

        public int age { get; set; }

        public int gender { get; set; }

        public string eIcon { get; set; }

        public string phone { get; set; }

        public string mobile { get; set; }

        public string address { get; set; }

        public string email { get; set; }

        public string qq { get; set; }

        public string weChat { get; set; }

        public string lastLoginTime { get; set; }

        public string createTime { get; set; }

        public int createId { get; set; }

        public string lastModifyTime { get; set; }

        public int lastModifyId
        {
            get; set;
        }

        public string Token { get; set; }
    }
}
