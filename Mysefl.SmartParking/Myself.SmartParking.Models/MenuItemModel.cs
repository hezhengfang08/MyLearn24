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

        /// <summary>
        /// 是否展开
        /// </summary>
        private bool _isExpanded;

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set { SetProperty(ref _isExpanded, value); }
        }

        /// <summary>
        /// 子节点
        /// </summary>
        public ObservableCollection<MenuItemModel> Children { get; set; } = new ObservableCollection<MenuItemModel>();  
    }
}
