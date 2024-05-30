using MySelf.QOSM.QuickOrderAPP.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MySelf.QOSM.QuickOrderAPP.ViewModels.UControl
{
   public class UPagerViewModel:ViewModelBase
    {
        public UPagerViewModel() {
            PageChangedCmd = new RelayCommand((action) => { OnPageChanged(); });
        }
        #region 属性
        private int startIndex;
        /// <summary>
        /// 当前页
        /// </summary>
        public int StartIndex
        {
            get { return (CurrentPage - 1) * PageSize + 1; }
            set { startIndex = value; }
        }
        private int totalCount;
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount
        {
            get { return totalCount; }
            set { totalCount = value;
                PageCount = GetPageCount();
            }
        }
        private int pageSize;
        //每页显示的记录数
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        private int currentPage;
        /// <summary>
        /// 当前页码
        /// </summary>
        public int CurrentPage
        {
            get { return currentPage; }
            set { currentPage = value; 
                OnPropertyChanged();

            }
        }
        private int pageCount;
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get { return pageCount; }
            set { pageCount = value; 
                OnPropertyChanged();
            }
        }
        private int maxInterval = 4;
        /// <summary>
        /// 最大间隔
        /// </summary>
        public int MaxInterval
        {
            get { return maxInterval; }
            set { maxInterval = value; 
                OnPropertyChanged();
            }
        }
        private bool isJump = true;

        public bool SsJump
        {
            get { return isJump; }
            set { isJump = value; 
                OnPropertyChanged();
            }
        }


        #endregion 属性
        #region  命令
        public ICommand PageChangedCmd { get; set; }
        #endregion  命令
        #region 事件
        public delegate void PageHandler(object sender, EventArgs e);   
        public event PageHandler PageChanged;//翻页事件
        public void OnPageChanged()
        {
            PageChanged?.Invoke(this,EventArgs.Empty);  
        }
        #endregion 事件
        #region 属性
        #endregion 属性
        #region  方法
        private int GetPageCount()
        {
            int pcount = 0;
            if (totalCount == 0)
                pcount = 0;
            else
            {
                if (totalCount % pageSize > 0)
                    pcount = totalCount / pageSize + 1;
                else
                    pcount = totalCount / pageSize;
            }
            return pcount;
        }
        #endregion

    }
}
