using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.Start.Models
{
  public  class MenuModel
    {
        public string MenuId { get; set; }  
        public string MenuHeader { get; set; }
        public bool IsSelected { get; set; }
    }
}
