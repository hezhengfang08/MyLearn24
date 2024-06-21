using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.QOSM.QuickOrderAPP.ViewModels.Stat
{
    public class StatisticsAmountItem : ViewModelBase
    {
        #region 属性
        private int payCount;

        public int PayCount
        {
            get { return payCount; }
            set
            {
                payCount = value;
                OnPropertyChanged();
            }
        }
        public int PayWay { get; set; }
        public string PayWayText { get; set; }
        private decimal amount;

        public decimal Amount
        {
            get { return amount; }
            set
            {
                amount = value;
                OnPropertyChanged();
            }
        }



        #endregion

    }
}
