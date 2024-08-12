using MySelf.PMS.Client.Upgrade.ViewModels;
using System.Configuration;
using System.Data;
using System.Windows;
using MySelf.PMS.Client.Upgrade.ViewModels;
using MySelf.PMS.Client.Upgrade.Views;

namespace MySelf.PMS.Client.Upgrade
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            if (e.Args.Length <= 0) return;
            MainViewModel viewModel = new MainViewModel(e.Args);

            MainView mainView = new MainView();
            mainView.DataContext = viewModel;
            mainView.Show();
        }
    }

}
