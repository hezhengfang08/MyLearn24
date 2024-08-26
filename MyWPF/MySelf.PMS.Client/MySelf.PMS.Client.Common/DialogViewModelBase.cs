using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.PMS.Client.Common
{
    public class DialogViewModelBase : BindableBase, IDialogAware, INotifyDataErrorInfo
    {
        #region IDialogAware接口实现
        public string Title { get; set; }

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog() => true;

        public void OnDialogClosed() { }

        public virtual void OnDialogOpened(IDialogParameters parameters)
        {

        }
        #endregion

        #region INotifyDataErrorInfo接口实现
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public bool HasErrors => ErrorList.Count > 0;
        public IEnumerable GetErrors(string? propertyName)
        {
            if (ErrorList.ContainsKey(propertyName))
                return ErrorList[propertyName];
            return null;
        }
        public Dictionary<string, IList<string>> ErrorList = new Dictionary<string, IList<string>>();
        public void RaiseErrorsChanged([CallerMemberName] string propNmae = "")
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propNmae));
        }
        #endregion

        public DelegateCommand SaveCommand { get; set; }

        DialogCloseListener IDialogAware.RequestClose => throw new NotImplementedException();

        public DialogViewModelBase()
        {
            SaveCommand = new DelegateCommand(DoSave);
        }
        /// <summary>
        /// 正常关闭弹窗逻辑
        /// </summary>
        public virtual void DoSave()
        {
            this.RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
        }
    }
}
