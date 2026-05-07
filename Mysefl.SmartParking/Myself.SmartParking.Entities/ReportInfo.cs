using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.SmartParking.Entities
{
    public class ReportInfo
    {
        public string DateTime { get; set; }// 日期
        public int OrderCount { get; set; }// 订单数
        public double Payable { get; set; }// 应付
        public double PaymentCash { get; set; }//现金
        public double PaymentElec { get; set; }//电子
    }
}
