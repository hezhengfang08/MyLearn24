using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.Entities
{
    public class FeeEntity
    {
        public int FeeId { get; set; }

        public int FeeModeId { get; set; }

        public string FeeMode { get; set; }

        public decimal Amount { get; set; }

        public int BId { get; set; }

        public string BName { get; set; }

        public int QId { get; set; }

        public string QName { get; set; }

        public string RoomNumber { get; set; }

        public string Description { get; set; }

        public int State { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public DateTime ModifyTime { get; set; }

        public DateTime ValidTime { get; set; }
    }
}
