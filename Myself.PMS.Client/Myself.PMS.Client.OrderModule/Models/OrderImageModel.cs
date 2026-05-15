using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.OrderModule.Models
{
    public class OrderImageModel
    {
        public string OrderId { get; set; }
        public string ImageId { get; set; }
        public string ImageName { get; set; }

        public bool IsModified { get; set; }
    }
}
