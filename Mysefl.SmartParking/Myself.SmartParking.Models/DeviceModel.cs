using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.SmartParking.Models
{
    public class DeviceModel
    {
        public int Index { get; set; }
        public int DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string AddrIp { get; set; }
        public int? AddrPort { get; set; }
        public string AddrCom { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CallbackUrl1 { get; set; }
        public string CallbackUrl2 { get; set; }
        public string CallbackUrl3 { get; set; }
    }
}
