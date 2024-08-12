using MySelf.PMS.Client.Upgrade.Base;
using MySelf.PMS.Client.Upgrade.DataAccess;
using MySelf.PMS.Client.Upgrade.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MySelf.PMS.Client.Upgrade.ViewModels
{
    public class MainViewModel : NotifyPropertyBase
    {
        public int TotalCount { get; set; }
        private int completed;

        public int Completed
        {
            get { return completed; }
            set { SetProperty<int>(ref completed, value); }
        }
        public ICommand StartCommand { get; set; }
        //列表
        public ObservableCollection<FileModel> FileList { get; set; } =
           new ObservableCollection<FileModel>();

        public MainViewModel(string[] files)
        {
            TotalCount = files.Length;

            StartCommand = new Command(DoStart);

            for (int i = 1; i <= files.Length; i++)
            {
                var file = files[i - 1];
                // Zhaoxi.PMS.Client.BLL.dll|UpgradeFiles|100
                string[] info = file.Split("|");
                int.TryParse(info[2], out int len);
                FileList.Add(new FileModel
                {
                    Index = i,
                    FileName = info[0],
                    FilePath = info[1],
                    FileLenght = len,
                    FileMd5 = ""
                });
            }
        }
        private void DoStart(object? arg)
        {
            // 开始下载文件    从服务器上下载相关文件
            WebAccess webAccess = new WebAccess();
            foreach (var file in FileList)
            {
                webAccess.DownloadFile(file.FileName, "");
            }
        }
    }
}
