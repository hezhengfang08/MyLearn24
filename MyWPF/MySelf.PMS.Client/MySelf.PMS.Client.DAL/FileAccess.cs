﻿using MySelf.PMS.Client.Entities;
using MySelf.PMS.Client.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.PMS.Client.DAL
{
    public class FileAccess : WebAccess, IFileAccess
    {
        public FileAccess(GlobalValues globalValues) : base(globalValues)
        {
        }
        public string GetUpgradeFiles()
        {
            string uri = "/api/File";

            return this.Get(uri);
        }
    }
}
