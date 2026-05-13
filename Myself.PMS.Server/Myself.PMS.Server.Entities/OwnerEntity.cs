using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Server.Entities
{
    [SugarTable("owner_info")]
    public class OwnerEntity
    {
        [SugarColumn(ColumnName = "owner_id", IsPrimaryKey = true, IsIdentity = true)]
        public int OwnerId { get; set; }

        [SugarColumn(ColumnName = "householder")]
        public string HouseHolder { get; set; }

        [SugarColumn(ColumnName = "id_number")]
        public string IdNumber { get; set; }

        [SugarColumn(ColumnName = "phone")]
        public string Phone { get; set; }

        [SugarColumn(ColumnName = "b_id")]
        public int Bid { get; set; }

        [SugarColumn(ColumnName = "b_name")]
        public string Bname { get; set; }

        [SugarColumn(ColumnName = "q_id")]
        public int Qid { get; set; }

        [SugarColumn(ColumnName = "q_name")]
        public string Qname { get; set; }

        [SugarColumn(ColumnName = "room_num")]
        public string RoomNum { get; set; }

        [SugarColumn(ColumnName = "gender")]
        public int Gender { get; set; }

        [SugarColumn(ColumnName = "credentials_img_1")]
        public string CredentialImg1 { get; set; }

        [SugarColumn(ColumnName = "credentials_img_2")]
        public string CredentialImg2 { get; set; }

        [SugarColumn(ColumnName = "description")]
        public string Description { get; set; }

        [SugarColumn(ColumnName = "state")]
        public int State { get; set; }

        [SugarColumn(ColumnName = "modify_time")]
        public DateTime ModifyTime { get; set; }

        [SugarColumn(ColumnName = "user_id")]
        public int UserId { get; set; }

        [SugarColumn(ColumnName = "user_name")]
        public string UserName { get; set; }
    }
}
