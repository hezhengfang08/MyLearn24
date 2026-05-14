using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.Upgrade.DataAccess
{
    public class WebAccess
    {
        // 1、HTTP请求对象：HttpClient    HttpWebRequest    WebClient
        // 2、下载过程中，需要知道两个情况：正在下载的进度、下载完成
        WebClient client = new WebClient();
        public void DownloadFile(string web_file, string local_file,
            Action<AsyncCompletedEventArgs> completed,
            Action<int, long> progress)
        {
            // 需要知道当前文件已经下载完成，可进行下一个文件的下载
            client.DownloadFileCompleted += (se, ev) =>
            {
                Debug.WriteLine("下载完成：" + web_file);
                completed?.Invoke(ev);// 可以异常信息

                client.Dispose();
            };
            client.DownloadProgressChanged += (se, ev) =>
            {
                progress?.Invoke(ev.ProgressPercentage, ev.BytesReceived);
            };
            // https://localhost:7299/api/File/download/none/Myself.PMS.Client.BLL.dll
            Debug.WriteLine("开始下载：" + web_file);
            client.DownloadFileAsync(new Uri($"https://localhost:7299/api/File/download/{web_file}"),
                local_file);
        }
    }
}
