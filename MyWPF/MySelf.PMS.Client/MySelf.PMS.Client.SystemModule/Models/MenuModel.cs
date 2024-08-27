using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.PMS.Client.SystemModule.Models
{
    public class MenuModel : BindableBase
    {
        public string MenuId { get; set; }

        public string MenuIcon { get; set; }
        public string MenuHeader { get; set; }
        public string TargetView { get; set; }// 双击这个节点的时候打开的页面

        public string ParentId { get; set; }
        public bool IsLastChild { get; set; }

        private int? _menuType;

        public int? MenuType
        {
            get { return _menuType; }
            set { SetProperty<int?>(ref _menuType, value); }
        }


        private bool _isExpanded;
        // 是否展开节点
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set { SetProperty(ref _isExpanded, value); }
        }
        // 子节点
        public ObservableCollection<MenuModel> Children { get; set; } =
            new ObservableCollection<MenuModel>();
        public MenuModel Parent { get; set; }

        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                SetProperty<bool>(ref _isSelected, value);
            }
        }
    }
}
