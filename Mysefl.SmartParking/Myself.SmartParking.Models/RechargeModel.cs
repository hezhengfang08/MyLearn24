using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.SmartParking.Models
{
    public class RechargeModel : BindableBase
    {
        public int Index { get; set; }

        public int RId { get; set; }

        public string AutoLisence { get; set; }

        public int FeeModeId { get; set; }
        public string FeeModeName { get; set; }

        public int? RechargeCount { get; set; }

        public DateTime? RechargeStartTime { get; set; }

        public DateTime? RechargeEndTime { get; set; }

        private int _state;

        public int State
        {
            get { return _state; }
            set { SetProperty<int>(ref _state, value); }
        }


        public DateTime? CreateTime { get; set; }

        public int CreateId { get; set; }

        public string CreateName { get; set; }


        private bool _isCanCancel = true;

        public bool IsCanCancel
        {
            get { return _isCanCancel; }
            set { SetProperty<bool>(ref _isCanCancel, value); }
        }

    }
}
