using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.Entities
{
    public class IEEntity
    {
        public int IEID { get; set; }
        public DateTime HappendTime { get; set; }
        public int RecordType { get; set; }
        public decimal Amount { get; set; }
        public string AmountType { get; set; }
        public string AmountDesc { get; set; }
        public string ProjectName { get; set; }
        public string Account { get; set; }
        public string Department { get; set; }
        public string Operator { get; set; }
        public string Description { get; set; }
        public int RecordId { get; set; }
        public string RecordName { get; set; }
        public DateTime RecordTime { get; set; }
        public int State { get; set; }
        public DateTime ModifyTime { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
