using Microsoft.Extensions.Configuration;
using MySelf.QOSM.Common;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;

namespace MySelf.QOSM.QuickOrderAPP
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IConfiguration Configuration { get; }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigHelper.Init("appsettings.json");
        }
    }

}
