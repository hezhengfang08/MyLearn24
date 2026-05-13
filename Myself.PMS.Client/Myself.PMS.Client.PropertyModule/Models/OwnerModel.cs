using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.PropertyModule.Models
{
    public class OwnerModel
    {
        public int Index { get; set; }
        public int OwnerId { get; set; }
        public string HouseHolder { get; set; }
        public string IdNumber { get; set; }
        public string Phone { get; set; }
        public int Bid { get; set; }
        public string Bname { get; set; }
        public int Qid { get; set; }
        public string Qname { get; set; }
        public string RoomNum { get; set; }
        public int Gender { get; set; }
        public string CredentialImg1 { get; set; }
        public string CredentialImg2 { get; set; }
        public string Description { get; set; }
        public int State { get; set; }
        public DateTime ModifyTime { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
