using Prism.Regions;
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

namespace MySelf.PMS.Client.MainModule.Views
{
    /// <summary>
    /// PageView.xaml 的交互逻辑
    /// </summary>
    public partial class PageView : UserControl
    {
        IRegionManager _regionManager;
        public PageView(IRegionManager regionManager)
        {
            InitializeComponent();

            _regionManager = regionManager;


            // 如果这个页面中的区域无法默认加载，需要添加这个事件，
            // 在事件进行区域更新，这个功能需要注意

            //this.Loaded += PageView_Loaded;
        }

        //private void PageView_Loaded(object sender, RoutedEventArgs e)
        //{
        //    if (_regionManager.Regions.ToList().Exists(r => r.Name == "PageRegion"))
        //        return;

        //    RegionManager.SetRegionManager(this, _regionManager);
        //    RegionManager.UpdateRegions();
        //}
    }
}
