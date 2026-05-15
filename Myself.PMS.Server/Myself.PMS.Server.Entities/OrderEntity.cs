using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Server.Entities
{
    [SugarTable("work_order")]
    public class OrderEntity
    {
        [SugarColumn(ColumnName = "order_id", IsPrimaryKey = true)]
        public string OrderId { get; set; }

        [SugarColumn(ColumnName = "order_type")]
        public string OrderType { get; set; }

        [SugarColumn(ColumnName = "service_type")]
        public string ServiceType { get; set; }

        [SugarColumn(ColumnName = "description")]
        public string Description { get; set; }

        [SugarColumn(ColumnName = "address")]
        public string Address { get; set; }

        [SugarColumn(ColumnName = "contacts")]
        public string Contacts { get; set; }

        [SugarColumn(ColumnName = "phone")]
        public string Phone { get; set; }

        [SugarColumn(ColumnName = "finish_time")]
        public DateTime FinishTime { get; set; }

        [SugarColumn(ColumnName = "state")]
        public int State { get; set; }

        [SugarColumn(ColumnName = "is_urgent")]
        public bool IsUrgent { get; set; }

        [SugarColumn(ColumnName = "modify_time")]
        public DateTime ModifyTime { get; set; }

        [SugarColumn(ColumnName = "user_id")]
        public int UserId { get; set; }

        [SugarColumn(ColumnName = "user_name")]
        public string UserName { get; set; }

        [SugarColumn(ColumnName = "title")]
   
        public string Title { get; set; }

        [SugarColumn(IsIgnore = true)]
        public List<OrderImageEntity> Images { get; set; }
     
    }
}
