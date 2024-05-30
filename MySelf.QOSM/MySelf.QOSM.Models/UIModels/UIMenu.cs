using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.QOSM.Models.UIModels
{
    public class UIMenu
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public int ParentId{ get; set; }
        public string MenuPic { get; set; }
        public string MenuUrl { get; set; }
    }
}
