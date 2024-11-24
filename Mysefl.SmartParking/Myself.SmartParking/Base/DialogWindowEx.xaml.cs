using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Myself.SmartParking.Base
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class DialogWindowEx : Window,IDialogWindow
    {
        public DialogWindowEx()
        {
            InitializeComponent();
            //this.Loaded += DialogWindowEx_Loaded;
            //this.ContentRendered += DialogWindowEx_ContentRendered;
        }

        private void DialogWindowEx_ContentRendered(object? sender, EventArgs e)
        {
            this.SizeToContent = SizeToContent.WidthAndHeight;
        }

        private void DialogWindowEx_Loaded(object sender, RoutedEventArgs e)
        {
            this.SizeToContent = SizeToContent.WidthAndHeight;
        }

        public IDialogResult Result { get ; set ; }
    }
}
