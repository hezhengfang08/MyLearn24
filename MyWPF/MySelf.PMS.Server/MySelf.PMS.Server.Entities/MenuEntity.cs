using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.PMS.Server.Entities
{
    [SugarTable(TableName ="menus")]
    public class MenuEntity
    {
        [SugarColumn(ColumnName = "menu_id")]
        public string MenuId { get; set; }
        [SugarColumn(ColumnName = "menu_header")]
        public string MenuHeader { get; set; }
        [SugarColumn(ColumnName = "target_view")]
        public string TargetView { get; set; }
        [SugarColumn(ColumnName = "parent_id")]
        public string ParentId { get; set; }
        [SugarColumn(ColumnName = "menu_icon")]
        public string MenuIcon { get; set; }
        [SugarColumn(ColumnName = "state")]
        public int State { get; set; }
    }
}
