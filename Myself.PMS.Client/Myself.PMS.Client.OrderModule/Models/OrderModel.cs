using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.OrderModule.Models
{
    public class OrderModel: BindableBase
    {
        public int Index { get; set; }
        public string OrderId { get; set; }
        public string OrderType { get; set; }
        public string ServiceType { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Contacts { get; set; }
        public string Phone { get; set; }
        public DateTime FinishTime { get; set; }

        private int _state;

        public int State
        {
            get { return _state; }
            set { SetProperty<int>(ref _state, value); }
        }

        public bool IsUrgent { get; set; }
        public DateTime ModifyTime { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Title { get; set; }   
        public ObservableCollection<OrderImageModel> ImageList { get; set; } =
            new ObservableCollection<OrderImageModel>();
    }
}
