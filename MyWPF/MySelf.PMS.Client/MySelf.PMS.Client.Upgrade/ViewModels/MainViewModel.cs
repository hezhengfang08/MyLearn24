using MySelf.PMS.Client.Upgrade.Base;
using MySelf.PMS.Client.Upgrade.DataAccess;
using MySelf.PMS.Client.Upgrade.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MySelf.PMS.Client.Upgrade.ViewModels
{
    public class MainViewModel : NotifyPropertyBase
    {
        public int TotalCount { get; set; }
        private int completed;
        //已经下载的文件数
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
                    FileMd5 = info[3]
                });
            }
        }
        int index = 0;
        AutoResetEvent resetEvent = new AutoResetEvent(false);
        private void DoStart(object? arg)
        {
            Task.Run(() =>
            {
                foreach (var file in FileList) {
                    if (file.HasCompleted) { continue; }
                    WebAccess webAccess = new WebAccess();  
                    string local_file = System.IO.Path.Combine(file.FilePath, file.FileName);
                    if (!string.IsNullOrEmpty(file.FilePath)&& !Directory.Exists(file.FilePath)) 
                    {
                        Directory.CreateDirectory(file.FilePath);
                    }
                    string path = (string.IsNullOrEmpty(file.FilePath) ? "none" : file.FilePath);
                    string web_file = path+"/"+file.FileName;
                    Debug.WriteLine("VM开始下载：" + web_file);
                    webAccess.DownloadFile(web_file, local_file,
                        completed_ev =>
                        {
                            Debug.WriteLine("VM开始完成：" + web_file);
                            if (completed_ev != null && completed_ev.Error != null)
                            {
                                file.ErrorMsg = completed_ev.Error.Message;
                                file.State = "异常";
                                file.StateColor = "Red";
                            }
                            else
                            {
                                file.State = "完成";
                                file.StateColor = "Green";
                                file.HasCompleted = true;
                            }
                            Completed++;
                            file.Progress = 0;
                            resetEvent.Set();
                        },
                    (progress, byte_len) =>
                    {
                        // 接收进度百分比和接收到的字节数
                        file.Progress = progress / 100;
                        file.CompletedLen = byte_len;
                    });
                    resetEvent.WaitOne();
                }
                if (FileList.ToList().Exists(f => !f.HasCompleted)) return;
                /// 下载完成后
                /// 1、将文件与入到对应的json   保留最新文件列表
                string path_temp = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                path_temp = Path.Combine(path_temp, "MySelf.PMS");
                if (!Directory.Exists(path_temp))
                    Directory.CreateDirectory(path_temp);
                path_temp = Path.Combine(path_temp, "upgrade_temp.json");
                string json_str = System.Text.Json.JsonSerializer.Serialize(FileList);
                File.WriteAllText(path_temp, json_str);
                /// 2、关闭更新程序，打开主程序

                Application.Current.Dispatcher.Invoke(() =>
                {
                    Process.Start("MySelf.PMS.Client.Start.exe");
                    Application.Current.Shutdown();
                });
            });
        }
    }
}
