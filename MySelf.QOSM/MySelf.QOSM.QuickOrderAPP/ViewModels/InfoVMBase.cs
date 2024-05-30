using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MySelf.QOSM.QuickOrderAPP.ViewModels
{
    /// <summary>
    /// 信息页视图的基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class InfoVMBase<T> : ViewModelBase 
    {
        public int ActType { get; set; } //提交标识
        public string SubmitText { get; set; } //提交文本
        public ICommand ConfirmCmd { get; set; }    //提交命令

        public event EventHandler<InfoArgs<T>> ReloadList; //重新加载 刷新事件

        /// <summary>
        /// 刷新事件的动作
        /// </summary>
        /// <param name="newInfo"></param>
       protected void OnReloadList(T newInfo)
        {
            ReloadList?.Invoke(this, new InfoArgs<T>(ActType, newInfo));
        }

    }
}
