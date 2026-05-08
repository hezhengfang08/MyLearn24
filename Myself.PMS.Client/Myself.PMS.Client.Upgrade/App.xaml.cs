using Myself.PMS.Client.Upgrade.ViewModels;
using Myself.PMS.Client.Upgrade.Views;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Myself.PMS.Client.Upgrade
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);

            if (e.Args.Length <= 0) return;

            MainViewModel viewModel = new MainViewModel(e.Args);

            MainView mainView = new MainView();
            mainView.DataContext = viewModel;
            mainView.Show();
        }
    }

}
