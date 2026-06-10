using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.WinAlarmApp.Models
{
    public class SlaveInfo
    {
        public byte SlaveId { get; set; }
        public byte FuntionCode { get; set; }
        public ushort StartAddress { get; set; }
        public ushort Count { get; set; }
    }
}
