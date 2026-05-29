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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Myself.PMS.Client.FinanceModule.Views
{
    /// <summary>
    /// IEDetailView.xaml 的交互逻辑
    /// </summary>
    public partial class IEDetailView : UserControl
    {
        public IEDetailView()
        {
            InitializeComponent();
        }
    
    private void ListBox_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            this.sv_header.ScrollToHorizontalOffset(e.HorizontalOffset);

            //A  ScrollViewer    需要同步B的位置
            // 取B的Offset（只读）　　　通过A的ScrollToHorizontalOffset设置到A
        }
    }
}
