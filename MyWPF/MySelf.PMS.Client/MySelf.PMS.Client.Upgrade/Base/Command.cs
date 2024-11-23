using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MySelf.PMS.Client.Upgrade.Base
{
    internal class Command : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            doExecute?.Invoke(parameter);
        }
        Action<object?> doExecute;
        public Command(Action<object?> doExecute)
        {
            this.doExecute = doExecute;
        }
    }
}
