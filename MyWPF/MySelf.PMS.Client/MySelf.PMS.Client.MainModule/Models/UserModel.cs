using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.PMS.Client.MainModule.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string RealName { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
    }
}
