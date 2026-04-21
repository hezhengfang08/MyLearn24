using Microsoft.Xaml.Behaviors.Core;
using Myself.SmartParking.Models;
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
        public DelegateCommand<string> RefreshCommand { get; set; }
        public DelegateCommand<MenuItemModel> ModifyCommand { get; set; }
        public DelegateCommand<MenuItemModel> DeleteCommand { get; set; }

        private readonly IRegionManager _regionManager;
        public ViewModelBase(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            CloseCommand = new DelegateCommand(DoClose);
            RefreshCommand = new DelegateCommand<string>(Refresh);
            ModifyCommand = new DelegateCommand<MenuItemModel>(DoModify);
            DeleteCommand = new DelegateCommand<MenuItemModel>(DoDelete);
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

        public virtual void Refresh(string key = "") { }
        public virtual void DoModify(MenuItemModel model) { }
        public virtual void DoDelete(MenuItemModel model) { }
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
