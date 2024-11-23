using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.PMS.Client.Entities
{
    public class MenuEntity
    {
        public string MenuId { get; set; }
        public string MenuHeader { get; set; }
        public string TargetView { get; set; }
        public string ParentId { get; set; }
        public string MenuIcon { get; set; }
        public int State { get; set; }
    }
}
