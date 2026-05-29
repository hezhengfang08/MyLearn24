using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.Entities
{
    public class ContractEntity
    {
        public int ContractId { get; set; }
        public string ContractName { get; set; }
        public string ContractNumber { get; set; }
        public decimal ContractAmount { get; set; }
        public decimal ExcuteAmount { get; set; }
        public DateTime SignTime { get; set; }
        public string Operator { get; set; }
        public string Opposite { get; set; }
        public int State { get; set; }
        public DateTime? ArchivedTime { get; set; }
        public int ArchivedUserId { get; set; }
        public string ArchivedUserName { get; set; }
        public DateTime ModifyTime { get; set; }
        public int Userid { get; set; }
        public string UserName { get; set; }
    }
}
