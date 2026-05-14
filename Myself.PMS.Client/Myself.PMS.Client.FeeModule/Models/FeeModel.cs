using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.FeeModule.Models
{
    public class FeeModel : BindableBase
    {
        public int Index { get; set; }

        public int FeeId { get; set; }

        public int FeeModeId { get; set; }

        public string FeeMode { get; set; }

        public decimal Amount { get; set; }

        public int BId { get; set; }

        public string BName { get; set; }

        public int QId { get; set; }

        public string QName { get; set; }

        public string RoomNumber { get; set; }

        public string Description { get; set; }

        private int _state;

        public int State
        {
            get { return _state; }
            set { SetProperty<int>(ref _state, value); }
        }


        public int UserId { get; set; }

        public string UserName { get; set; }

        public DateTime ModifyTime { get; set; }
        public DateTime ValidTime { get; set; }
    }
}
