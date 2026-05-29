using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.FinanceModule.Models
{
    public class IEModel : BindableBase
    {
        public int Index { get; set; }
        public int IEID { get; set; }
        public DateTime HappendTime { get; set; }
        public int RecordType { get; set; }
        public decimal IncomeAmount { get; set; }
        public string IncomeType { get; set; }
        public string IncomeDesc { get; set; }
        public decimal ExpensesAmount { get; set; }
        public string ExpensesType { get; set; }
        public string ExpensesDesc { get; set; }
        public string ProjectName { get; set; }
        public string Account { get; set; }
        public string Department { get; set; }
        public string Operator { get; set; }
        public string Description { get; set; }
        public int RecordId { get; set; }
        public string RecordName { get; set; }
        public DateTime RecordTime { get; set; }
        public DateTime ModifyTime { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }

        private int _state;

        public int State
        {
            get { return _state; }
            set { SetProperty<int>(ref _state, value); }
        }

    }
}
