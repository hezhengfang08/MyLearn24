using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.QOSM.QuickOrderAPP.ViewModels.Orders
{
    public class OrderStateItem
    {
        public int OrderState { get; set; }
        public string StateText { get; set; }
        public OrderStateItem(int orderState, string stateText)
        {
            OrderState = orderState;
            StateText = stateText;
        }
    }
}
