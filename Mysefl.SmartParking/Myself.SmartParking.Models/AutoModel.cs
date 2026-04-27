using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.SmartParking.Models
{
    public class AutoModel : BindableBase
    {
        public int Index { get; set; }
        public int AutoId { get; set; }
        public string AutoLicense { get; set; }
        public int LColorId { get; set; }
        public string LColorName { get; set; }
        public int LTypeId { get; set; }
        public string LTypeName { get; set; }
        public int AColorId { get; set; }
        public string AColorName { get; set; }
        public int FeeModeId { get; set; }
        public string FeeModeName { get; set; }
        public string Description { get; set; }
        public DateTime ValidStartTime { get; set; }
        public DateTime ValidEndTime { get; set; }
        public int? ValidCount { get; set; }

        private int _state;

        public int State
        {
            get { return _state; }
            set { SetProperty<int>(ref _state, value); }
        }

        private string _lockButtonText = "锁定";

        public string LockButtonText
        {
            get { return _lockButtonText; }
            set { SetProperty<string>(ref _lockButtonText, value); }
        }
    }
}
