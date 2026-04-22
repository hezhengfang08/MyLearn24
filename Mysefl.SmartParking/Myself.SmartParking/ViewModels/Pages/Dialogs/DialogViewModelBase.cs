using Prism.Commands;


namespace Myself.SmartParking.ViewModels.Pages.Dialogs
{
    public class DialogViewModelBase : IDialogAware
    {
        public string Title { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DialogViewModelBase()
        {
            SaveCommand = new DelegateCommand(DoSave);
        }
        public virtual void DoSave()
        {
            this.RequestClose.Invoke(new DialogResult(ButtonResult.OK));
        }
        public virtual bool CanCloseDialog()
        {
            return true;
        }

        public virtual void OnDialogClosed()
        {
        }

        public virtual void OnDialogOpened(IDialogParameters parameters)
        {
        }

        public virtual DialogCloseListener RequestClose { get; private set; }
    }
}
