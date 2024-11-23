using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zero.Application.Contracts.Auth
{
    public class RegistInput
    {
        public string Phone { get; set; }

        public string Password { get; set; }

        public string NickName { get; set; }

        public string Email { get; set; }

        public int? Sex { get; set; }
    }

}
