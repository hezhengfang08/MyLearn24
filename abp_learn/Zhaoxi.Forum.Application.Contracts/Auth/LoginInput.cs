﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhaoxi.Forum.Application.Contracts.Auth
{
    public class LoginInput
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }

}
