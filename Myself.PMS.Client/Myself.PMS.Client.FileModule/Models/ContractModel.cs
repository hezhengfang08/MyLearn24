using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.FileModule.Models
{
    public class ContractModel : BindableBase, INotifyDataErrorInfo
    {
        public int Index { get; set; }
        public int ContractId { get; set; }
        private string _contractName;

        public string ContractName
        {
            get { return _contractName; }
            set
            {
                _contractName = value;
                if (_errors.ContainsKey("ContractName"))
                    _errors.Remove("ContractName");
                if (string.IsNullOrEmpty(value))
                    _errors.Add("ContractName", new List<string> { "合同名称不能为空" });
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs("ContractName"));
            }
        }

        private string _contractNumber;

        public string ContractNumber
        {
            get { return _contractNumber; }
            set
            {
                _contractNumber = value;
                if (_errors.ContainsKey("ContractNumber"))
                    _errors.Remove("ContractNumber");
                if (string.IsNullOrEmpty(value))
                    _errors.Add("ContractNumber", new List<string> { "合同编号不能为空" });
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs("ContractNumber"));
            }
        }

        public decimal ContractAmount { get; set; }
        public decimal ExcuteAmount { get; set; }
        public DateTime SignTime { get; set; }
        public string Operator { get; set; }
        public string Opposite { get; set; }
        private int _state;

        public int State
        {
            get { return _state; }
            set { SetProperty<int>(ref _state, value); }
        }

        private DateTime? _archivedTimie;

        public DateTime? ArchivedTime
        {
            get { return _archivedTimie; }
            set { SetProperty<DateTime?>(ref _archivedTimie, value); }
        }

        public int ArchivedUserId { get; set; }

        private string _archivedUserName;

        public string ArchivedUserName
        {
            get { return _archivedUserName; }
            set { SetProperty<string>(ref _archivedUserName, value); }
        }

        public DateTime ModifyTime { get; set; }
        public int Userid { get; set; }
        public string UserName { get; set; }

        public decimal Prograss { get; set; }



        Dictionary<string, IList<string>> _errors = new Dictionary<string, IList<string>>();
        public bool HasErrors => _errors.Count > 0;

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
            if (_errors.ContainsKey(propertyName))
                return _errors[propertyName];
            return null;
        }
    }
}
