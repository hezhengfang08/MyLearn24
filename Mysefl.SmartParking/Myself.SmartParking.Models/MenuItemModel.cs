using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.SmartParking.Models
{
    public class MenuItemModel:BindableBase

    {
        public int MenuId { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string MenuIcon { get; set; }    

        /// <summary>
        /// 标题
        /// </summary>
        public string MenuHeader { get; set; }

        /// <summary>
        ///  双击这个节点的时候打开的页面
        /// </summary>
        public string TargetView { get; set; }

        public int? MenuType { get; set; }
        public int? ParentId { get; set; }
        public bool IsLastChild { get; set; }
        /// <summary>
        /// 是否展开
        /// </summary>
        private bool _isExpanded;

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set { SetProperty(ref _isExpanded, value); }
        }
        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                SetProperty<bool>(ref _isSelected, value);
            }
        }
        /// <summary>
        /// 子节点集合
        /// </summary>
        public ObservableCollection<MenuItemModel> Children { get; set; } = new ObservableCollection<MenuItemModel>();
        //父节点
        public MenuItemModel Parent { get; set; }
    }
}
