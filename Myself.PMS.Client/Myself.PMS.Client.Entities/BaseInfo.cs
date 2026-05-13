using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.Entities
{
    public class BaseInfo
    {
        public int InfoId { get; set; }
        public string InfoHeader { get; set; }

        public string InfoContent { get; set; }
        public string InfoKey { get; set; }
        public int InfoType { get; set; }
        public int state { get; set; }

        public DateTime ModifyTime { get; set; }
        public DateTime? PublishTime { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}

