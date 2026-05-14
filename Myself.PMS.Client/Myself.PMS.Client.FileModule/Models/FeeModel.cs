using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.FeeModule.Models
{
    public class FeeModel : BindableBase, INotifyDataErrorInfo
    {
        public int Index { get; set; }

        public int FeeId { get; set; }

        public int FeeModeId { get; set; }

        public string FeeMode { get; set; }

        private decimal _amount;

        public decimal Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;

                _errors.Remove(nameof(Amount));
                if (value == 0)
                {
                    _errors.Add("Amount", new List<string> { "请输入正确的金额" });
                }
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs("Amount"));
            }
        }


        public int BId { get; set; }

        public string BName { get; set; }

        public int QId { get; set; }

        public string QName { get; set; }

        private string _roomNumber;

        public string RoomNumber
        {
            get { return _roomNumber; }
            set
            {
                SetProperty<string>(ref _roomNumber, value, () =>
                {
                    _errors.Remove(nameof(RoomNumber));
                    if (string.IsNullOrEmpty(value))
                    {
                        _errors.Add("RoomNumber", new List<string> { "房间号不能为空" });
                    }
                    ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs("RoomNumber"));
                });
            }
        }


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
