using Myself.PMS.Client.Upgrade.Base;
using Myself.PMS.Client.Upgrade.DataAccess;
using Myself.PMS.Client.Upgrade.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Myself.PMS.Client.Upgrade.ViewModels
{
    public class MainViewModel: NotifyPropertyBase
    {
        public int TotalCount { get; set; }

		private int _completed;

		public int Completed
        {
			get { return _completed; }
			set { _completed = value; }
		}
		public ICommand StartCommand { get; set; }
		public ObservableCollection<FileModel> FileList { get; set; }= new ObservableCollection<FileModel>();
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
        AutoResetEvent autoResetEvent = new AutoResetEvent(false);
        private void DoStart(object? arg) {
			// 服务器
			// ./FileName
			// ./Modules/fileName

			// 客户端
			// ./FileName
			// ./Modules/fileName
			Task.Run(() => { 
			foreach(var file in FileList)
				{
					if(file.HasCompleted) { continue; }
					WebAccess webAccess = new WebAccess();
					string local_file = System.IO.Path.Combine(file.FilePath, file.FileName);
                    if (!string.IsNullOrEmpty(file.FilePath) && !Directory.Exists(file.FilePath))
                    {
                        Directory.CreateDirectory(file.FilePath);
                    }
                    string path = (string.IsNullOrEmpty(file.FilePath) ? "none" : file.FilePath);
                    string web_file = path + "/" + file.FileName;
                    Debug.WriteLine("VM开始下载：" + web_file);
                    webAccess.DownloadFile(web_file, local_file,
                        /// 当下载完成时回调
                        completed_ev =>
                        {
                            Debug.WriteLine("VM下载完成：" + web_file);
                            if (completed_ev != null && completed_ev.Error != null)
                            {
                                // 需要提示异常
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
                            autoResetEvent.Set();
                        },

                        /// 当下载进程发生变化时回调
                        (progress, byte_len) =>
                        {
                            // 接收进度百分比和接收到的字节数
                            file.Progress = progress / 100;
                            file.CompletedLen = byte_len;
                        }
                      );
                    autoResetEvent.WaitOne();
                }
                if (FileList.ToList().Exists(f => !f.HasCompleted)) return;

                /// 下载完成后
                /// 1、将文件写入到对应的json   保留最新文件列表
                string path_temp = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
                path_temp = Path.Combine(path_temp, "Myself.PMS");
                if (!Directory.Exists(path_temp))
                {
                    Directory.CreateDirectory(path_temp);
                }
                path_temp = Path.Combine(path_temp, "upgrade_temp.json");
                string json_str = System.Text.Json.JsonSerializer.Serialize(FileList);
                File.WriteAllText(path_temp, json_str);
                /// 2、关闭更新程序，打开主程序

                Application.Current.Dispatcher.Invoke(() =>
                {
                    Process.Start("Zhaoxi.PMS.Client.Start.exe");
                    Application.Current.Shutdown();
                });
        });
            // 第二种情况的下载逻辑，待测试
            //var file = FileList[index];
            //this.DoDownload(file);
        }
        readonly object lock_obj = new object();
        private void DoDownload(FileModel file)
        {
            string local_file = System.IO.Path.Combine(file.FilePath, file.FileName);
            if (!string.IsNullOrEmpty(file.FilePath) && !Directory.Exists(file.FilePath))
            {
                Directory.CreateDirectory(file.FilePath);
            }

            string path = (string.IsNullOrEmpty(file.FilePath) ? "none" : file.FilePath);
            string web_file = path + "/" + file.FileName;
            Debug.WriteLine("VM开始下载：" + web_file);

            WebAccess webAccess = new WebAccess();
            webAccess.DownloadFile(web_file, local_file,
                /// 当下载完成时回调
                completed_ev =>
                {
                    Debug.WriteLine("VM下载完成：" + web_file);
                    if (completed_ev != null && completed_ev.Error != null)
                    {
                        // 需要提示异常
                        file.ErrorMsg = completed_ev.Error.Message;
                    }
                    index++;
                    if (index == FileList.Count) return;
                    Thread.Sleep(100);
                    this.DoDownload(FileList[index]);
                },

                /// 当下载进程发生变化时回调
                (progress, byte_len) =>
                {
                    // 接收进度百分比和接收到的字节数
                    file.Progress = progress;
                    file.CompletedLen = byte_len;
                }
              );
        }
    }
}
