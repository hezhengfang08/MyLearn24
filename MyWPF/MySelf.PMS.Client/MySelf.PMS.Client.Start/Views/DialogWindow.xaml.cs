
using Prism.Services.Dialogs;
using System.Windows;
using System.Windows.Input;


namespace MySelf.PMS.Client.Start.Views
{
    /// <summary>
    /// DialogWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DialogWindow : Window, IDialogWindow
    {
        public DialogWindow()
        {
            InitializeComponent();
        }

        public IDialogResult Result { get; set; }


        private void CloseBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }
    }
}
