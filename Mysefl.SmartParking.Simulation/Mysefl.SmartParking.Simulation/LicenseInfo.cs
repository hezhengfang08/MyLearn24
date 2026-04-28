using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mysefl.SmartParking.Simulation
{
    internal class LicenseInfo
    {
        public int id { get; set; }
        public int count { get; set; }
        public RecTime rec_time { get; set; }
        public List<ItemInfo> item { get; set; }
    }

    internal class RecTime
    {
        public int y { get; set; }
        public int m { get; set; }
        public int d { get; set; }
        public int hh { get; set; }
        public int mm { get; set; }
        public int ss { get; set; }
    }

    internal class ItemInfo
    {
        public string license { get; set; }
        public string color { get; set; }
        public int nType { get; set; }// 车牌类型
        public int nConfidence { get; set; }// 车牌可信度
        public int nTime { get; set; }//识别耗时
        public int nDirection { get; set; }
        public Location rcLocation { get; set; }
        public int nCountry { get; set; }
        public int nCarBright { get; set; }
        public int nCarColor { get; set; }
        public int nCarLogo { get; set; }
        public int nCarType { get; set; }
        public string reserved { get; set; }
    }

    internal class Location
    {
        public int l { get; set; }
        public int t { get; set; }
        public int r { get; set; }
        public int b { get; set; }
    }
}
