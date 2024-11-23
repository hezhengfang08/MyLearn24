
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace MySelf.PMS.Client.Common
{
    public class PageViewModelBase : BindableBase, INavigationAware
    {
        public string PageTitle { get; set; } = "页面标题";
        public bool IsCanClose { get; set; } = true;

        public string SearchKey { get; set; }

        public DelegateCommand CloseCommand { get; set; }
        public DelegateCommand RefreshCommand { get; set; }
        public DelegateCommand<object> ModifyCommand { get; set; }
        public DelegateCommand<object> DeleteCommand { get; set; }

        IRegionManager _regionManager;
        public PageViewModelBase(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            CloseCommand = new DelegateCommand(DoClose);

            RefreshCommand = new DelegateCommand(Refresh);
            ModifyCommand = new DelegateCommand<object>(DoModify);
            DeleteCommand = new DelegateCommand<object>(DoDelete);
        }
        private void DoClose()
        {
            //拿到主区域，从区域里移除对应的页面，根据页面的名称
            var region = _regionManager.Regions["PageRegion"];
            var view = region.Views.Where(v => v.GetType().Name == PageName).FirstOrDefault();
            if (view != null)
            {
                region.Remove(view);
            }
        }

        public virtual void Refresh() { }
        public virtual void DoModify(object model) { }
        public virtual void DoDelete(object model) { }

        private string PageName { get; set; }
        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
            PageName = navigationContext.Uri.ToString();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

    }


}
