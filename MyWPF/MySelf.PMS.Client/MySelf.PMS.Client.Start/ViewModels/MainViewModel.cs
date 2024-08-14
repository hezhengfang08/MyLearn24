using Prism.Services.Dialogs;
using System;

using System.Windows;

namespace MySelf.PMS.Client.Start.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel(IDialogService dialogService)
        {
            // 打开登录弹窗
            dialogService.ShowDialog("LoginView", result =>
            {
                if (result.Result != ButtonResult.OK)
                {
                    Application.Current.Shutdown();
                }
            });
        }
    }
}
