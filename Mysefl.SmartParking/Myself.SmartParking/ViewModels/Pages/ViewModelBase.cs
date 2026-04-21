using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.SmartParking.ViewModels.Pages
{
    public class ViewModelBase : INavigationAware
    {
        public string PageTitle { get; set; } = "页面标题";
        public bool IsCanClose { get; set; } = true;
        public DelegateCommand CloseCommand { get; set; }
        private readonly IRegionManager _regionManager;
        public ViewModelBase(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            CloseCommand = new DelegateCommand(DoClose);
        }
        private string PageName { get; set; }
        private void DoClose()
        {
            //拿到主区域，从区域里移除对应的页面，根据页面的名称
            var region = _regionManager.Regions["MainRegion"];
            var view = region.Views.Where(m => m.GetType().Name == PageName).FirstOrDefault();
            if (view != null)
            {
                region.Remove(view);
            }
        }
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
          
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            PageName = navigationContext.Uri.ToString();
        }
    }
}
