using MySelf.QOSM.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MySelf.QOSM.QuickOrderAPP.ViewModels.Customer
{
   public class CustomerModel:ViewModelBase
    {

        public CustomerModel(CustomerInfo cust)
        {
            Customer = cust;
            ShowEnable = (cust.CustomerState == false && cust.IsDeleted == false) ? Visibility.Visible : Visibility.Collapsed;
            ShowReset = (cust.IsDeleted == false && cust.CustomerState == true) ? Visibility.Visible : Visibility.Collapsed;
        }
        #region 属性

        private CustomerInfo customer;

		public CustomerInfo Customer
        {
			get { return customer; }
			set { customer = value;
				OnPropertyChanged();
			}
		}

		private Visibility showReset;

		public Visibility ShowReset
        {
			get { return showReset; }
			set { showReset = value; 
			  OnPropertyChanged();
			}
		}
		private Visibility  showEnable;

		public Visibility ShowEnable
        {
			get { return showEnable; }
			set { showEnable = value;
				OnPropertyChanged();
			}
		}


		#endregion

	}
}
