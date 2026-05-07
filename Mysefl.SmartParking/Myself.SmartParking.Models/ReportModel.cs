using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.SmartParking.Models
{
    public class ReportModel
    {
        public int Index { get; set; }
        public string Date { get; set; }
        public int TotalCount { get; set; }
        public double ReceAmount { get; set; }
        public double CashPayment { get; set; }
        public double ElecPayment { get; set; }
        public double Subtotal { get; set; }
        public double DeduAmount { get; set; }
    }
}
