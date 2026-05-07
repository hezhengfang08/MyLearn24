using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.SmartParking.Entities
{
    public class ResponseMessage
    {
        public string msg { get; set; }
        public string value { get; set; }
    }

    public class ResponseState
    {
        public string msg { get; set; }
        public int value { get; set; }
    }
}
