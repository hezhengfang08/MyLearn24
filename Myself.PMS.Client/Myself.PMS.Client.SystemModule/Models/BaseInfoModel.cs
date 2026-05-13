using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.SystemModule.Models
{
    public class BaseInfoModel : BindableBase
    {
        public int Index { get; set; }
        public int InfoId { get; set; }
        public string InfoHeader { get; set; }

        public string InfoContent { get; set; }
        public string InfoKey { get; set; }
        public int InfoType { get; set; }

        private int _state;

        public int State
        {
            get { return _state; }
            set { SetProperty<int>(ref _state, value); }
        }


        public DateTime ModifyTime { get; set; }

        private DateTime? _publishTime;

        public DateTime? PublishTime
        {
            get { return _publishTime; }
            set { SetProperty<DateTime?>(ref _publishTime, value); }
        }


        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
