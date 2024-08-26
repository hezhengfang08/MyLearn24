using MySelf.PMS.Client.Entities;
using MySelf.PMS.Client.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.PMS.Client.DAL
{
    public class FileAccess : WebAccess, IFileAccess
    {
        public FileAccess(GlobalValues globalValues) : base(globalValues)
        {
        }
        public string GetUpgradeFiles(string key)
        {
            key = string.IsNullOrEmpty(key) ? "none" : key;
            string uri = $"/api/File/list/{key}";
            return this.Get(uri);
        }
        public void UploadFile(string file, string save_path, Action<int> progress, Action completed)
        {
            string uri = "";
            // 地址参数
            Dictionary<string, object> datas = new Dictionary<string, object>();
            datas.Add("md5", GetFileMd5(file));
            datas.Add("save_path", save_path);
            // 调用什么方法
            this.Upload(uri, file, progress, completed, datas);
        }
         private string GetFileMd5(string fileName)
        {
            try
            {
                if (!File.Exists(fileName))
                    throw new Exception("文件不存在");

                FileStream file = new FileStream(fileName, System.IO.FileMode.Open);
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetFileMd5() fail,error:" + ex.Message);
            }
        }
    }
}
