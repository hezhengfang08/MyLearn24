using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.PropertyModule.Models
{
    public class OwnerModel : BindableBase, INotifyDataErrorInfo
    {
        public int Index { get; set; }
        public int OwnerId { get; set; }
        private string _houseHolder;
        public string HouseHolder
        {
            get => _houseHolder; set
            {
                _houseHolder = value;
                _errors.Remove(nameof(HouseHolder));
                if (string.IsNullOrEmpty(value))
                {
                    _errors.Add("HouseHolder", new List<string> { "业主姓名不能为空" });
                }
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs("HouseHolder"));
            }
        }
        private string _idNumber;
        public string IdNumber
        {
            get { return _idNumber; }
            set
            {
                _idNumber = value;
                _errors.Remove(nameof(IdNumber));
                if (string.IsNullOrEmpty(value))
                {
                    _errors.Add("IdNumber", new List<string> { "身份证不能为空" });
                }
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs("IdNumber"));
            }
        }
        private string _phone;

        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                _errors.Remove(nameof(Phone));
                if (string.IsNullOrEmpty(value))
                {
                    _errors.Add("Phone", new List<string> { "手机号不能为空" });
                }
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs("Phone"));
            }
        }
        public int Bid { get; set; }
        public string Bname { get; set; }
        public int Qid { get; set; }
        public string Qname { get; set; }
        private string _roomNum;

        public string RoomNum
        {
            get { return _roomNum; }
            set
            {
                _roomNum = value;
                _errors.Remove(nameof(RoomNum));
                if (string.IsNullOrEmpty(value))
                {
                    _errors.Add("RoomNum", new List<string> { "房间号不能为空" });
                }
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs("RoomNum"));
            }
        }

        private int gender;

        public int Gender
        {
            get { return gender; }
            set { gender = value; RaisePropertyChanged(); }
        }

        public string CredentialImg1 { get; set; }
        public string CredentialImg2 { get; set; }
        public string Description { get; set; }
        public int State { get; set; }
        public DateTime ModifyTime { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }

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
