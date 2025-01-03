﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;

using System.Text;
using System.Threading.Tasks;


namespace MySelf.PMS.Client.Upgrade.DataAccess
{
    public class WebAccess
    {   //1.http请求对象：httpClient;httpwebrequest;webclient
        //2、下载过程中，需要知道两个情况：正在下载的进度、下载完成
        WebClient client = new WebClient();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="file_path"></param>
        public void DownloadFile(string web_file, string local_file,
            Action<AsyncCompletedEventArgs> completed,
            Action<int, long> progress)
        {
            // 需要知道当前文件已经下载完成，可进行下一个文件的下载
            client.DownloadFileCompleted += (sender, eventarg) =>
            {
                Debug.WriteLine("下载完成：" + web_file);
                completed?.Invoke(eventarg);// 可以异常信息
                client.Dispose();        //释放资源
            };
            client.DownloadProgressChanged += (sender, eventarg) => { 
               progress?.Invoke(eventarg.ProgressPercentage,eventarg.BytesReceived);
            };
            // http://localhost:5273/api/File/download/none/MySelf.PMS.Client.BLL.dll
            Debug.WriteLine("开始下载：" + web_file);
            client.DownloadFileAsync(new Uri($"http://localhost:5273/api/File/download/{web_file}"),
               local_file);
        }



    }
}
