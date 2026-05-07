using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.SmartParking.Models
{
    public class OrderModel : BindableBase
    {
        /// <summary>
        /// 当前行（子项）是否要展开
        /// </summary>
        private bool _isExpanded;

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set { SetProperty<bool>(ref _isExpanded, value); }
        }

        #region 订单基本数据
        public int OrderId { get; set; }
        public string? AutoLicense { get; set; }

        public string EnterTime { get; set; }
        public string LeaveTime { get; set; }
        public int FeeModeId { get; set; }
        public string FeeMode { get; set; }
        private int? _state;
        public int? State
        {
            get { return _state; }
            set { SetProperty<int?>(ref _state, value); }
        }

        public long Payable { get; set; }// 应付
        public long Payment { get; set; }
        public long Discount { get; set; }
        #endregion

        #region 入口记录
        public int EnterRecord { get; set; }
        public string? EnterPassTime { get; set; }
        public long? EnterChannal { get; set; }
        public string EnterCarColor { get; set; }
        public string EnterLicenseColor { get; set; }
        public string? EnterImageFull { get; set; }
        public string? EnterImageSmall { get; set; }
        #endregion

        #region 出口记录
        public int LeaveRecord { get; set; }
        public string? LeavePassTime { get; set; }
        public long? LeaveChannal { get; set; }
        public string LeaveCarColor { get; set; }
        public string LeaveLicenseColor { get; set; }
        public string? LeaveImageFull { get; set; }
        public string? LeaveImageSmall { get; set; }
        #endregion
    }
}
