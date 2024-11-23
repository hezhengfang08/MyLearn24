using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.SmartParking.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel(IDialogService dialogService) {
            // 打开登录窗口
            dialogService.ShowDialog("LoginView", rerult =>
            {
                if (rerult.Result != ButtonResult.OK)
                {
                    System.Environment.Exit(0);
                }
            });
            // 当前窗口要做的事
        }
    }
}
