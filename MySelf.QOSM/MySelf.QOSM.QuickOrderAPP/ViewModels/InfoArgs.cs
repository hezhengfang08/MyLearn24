using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.QOSM.QuickOrderAPP.ViewModels
{
    public class InfoArgs<T> : EventArgs
    {
        public   int ActType { get; set; }
        public  T Info { get; set; }
        public InfoArgs(int actType, T info) {
            this.ActType = actType;
            this.Info = info;
}

    }
}
