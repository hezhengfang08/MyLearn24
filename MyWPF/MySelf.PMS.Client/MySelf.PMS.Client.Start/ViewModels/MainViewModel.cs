using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.PMS.Client.Start.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel(IDialogService dialogService)
        {
            // 打开登录弹窗
            dialogService.ShowDialog("LoginView");
        }
    }
}
