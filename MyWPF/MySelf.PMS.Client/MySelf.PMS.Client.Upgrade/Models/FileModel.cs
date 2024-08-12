
using MySelf.PMS.Client.Upgrade.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.PMS.Client.Upgrade.Models
{
    public class FileModel: NotifyPropertyBase
    {
        // 界面中有些列表需要有序号
        public int Index { get; set; }

        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileMd5 { get; set; }// 更新完成后是不是需要做本地缓存的更新
        public int FileLenght { get; set; }

        private int _completedLen;
        public int CompletedLen
        {
            get { return _completedLen; }
            set { SetProperty<int>(ref _completedLen, value); }
        }

        private string _state;
        public string State
        {
            get { return _state; }
            set { SetProperty(ref _state, value); }
        }
        //进度
        private double _progress;
        public double Progress
        {
            get { return _progress; }
            set { SetProperty(ref _progress, value); }
        }
        /// <summary>
        /// 错误
        /// </summary>
        private string _errorMsg;
        public string ErrorMsg
        {
            get { return _errorMsg; }
            set { SetProperty(ref _errorMsg, value); }
        }
    }
}
