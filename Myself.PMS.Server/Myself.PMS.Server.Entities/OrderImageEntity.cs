using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Server.Entities
{
    [SugarTable("work_order_images")]
    public class OrderImageEntity
    {
        [SugarColumn(ColumnName = "order_id")]
        public string OrderId { get; set; }

        [SugarColumn(ColumnName = "img_id", IsPrimaryKey = true)]
        public string ImageId { get; set; }

        [SugarColumn(ColumnName = "img_name")]
        public string ImageName { get; set; }
    }
}
