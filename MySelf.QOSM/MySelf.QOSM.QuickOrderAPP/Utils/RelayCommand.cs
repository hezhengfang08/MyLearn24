using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MySelf.QOSM.QuickOrderAPP.Utils
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action<object> actExcute;//封装要做的操作
        private Func<object, bool> canExcute; //是否可做
        public RelayCommand()
        {

        }
        public RelayCommand(Action<object> actExcute, Func<object, bool> canExcute)
        {
            this.actExcute = actExcute;
            this.canExcute = canExcute;
        }
        public RelayCommand(Action<object> actExcute):this(actExcute,null)
        {

        }

        public bool CanExecute(object parameter)
        {
            if (canExcute != null)
            {
                return canExcute.Invoke(parameter);
            }
            else
                return true;
        }

        public void Execute(object parameter)
        {
            if(actExcute == null)
            {
                return;
            }
            actExcute(parameter);
            OnCanExecuteChanged();
        }
        /// <summary>
        /// 执行OnCanExecuteChanged事件
        /// </summary>
        public void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
