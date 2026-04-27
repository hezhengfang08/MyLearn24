using Prism.Commands;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Myself.SmartParking.ViewModels.Pages.Dialogs
{
    public class DialogViewModelBase : BindableBase, IDialogAware, INotifyDataErrorInfo
    {
        public string Title { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }
        public DialogViewModelBase()
        {
            SaveCommand = new DelegateCommand(DoSave);
            CancelCommand = new DelegateCommand(OnCloseRequested);
        }

        #region IDialogAware
        public virtual void DoSave()
        {
            this.RequestClose.Invoke(new DialogResult(ButtonResult.OK));
        }
        public virtual bool CanCloseDialog()
        {
            return true;
        }
        private void OnCloseRequested()
        {
            if (CanCloseDialog())
            {
                // 点 X 视为取消/放弃，传 None 或 Cancel
                RequestClose.Invoke(new DialogResult(ButtonResult.Cancel));
            }
        }

        public virtual void OnDialogClosed()
        {
        }

        public virtual void OnDialogOpened(IDialogParameters parameters)
        {
        }

        public virtual DialogCloseListener RequestClose { get; private set; }
        #endregion
        #region INotifyDataErrorInfo
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public Dictionary<string, IList<string>> ErrorList = new Dictionary<string, IList<string>>();
        public bool HasErrors => ErrorList.Count > 0;
        public IEnumerable GetErrors(string? propertyName)
        {
            if (ErrorList.ContainsKey(propertyName))
                return ErrorList[propertyName];
            return null;
        }
        public void RaiseErrorsChanged([CallerMemberName] string propNmae = "")
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propNmae));
        }

        #endregion
    }
}
