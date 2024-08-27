using MySelf.PMS.Client.Entities;
using MySelf.PMS.Client.IBLL;
using MySelf.PMS.Client.IDAL;
using MySelf.PMS.Client.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.PMS.Client.BLL
{
    public class FileService : IFileService
    {
        IFileAccess _fileAccess;
        public FileService(IFileAccess fileAccess)
        {
            _fileAccess = fileAccess;
        }

        public int DeleteFile(string fileName)
        {
            string json=_fileAccess.DeleteFile(fileName);
            var result = JsonUtil.Deserializer<ResultEntiy<int>>(json);

            if (result == null)
                throw new Exception("删除文件数据失败!");
            if (result.State != ResultCode.Success)
                throw new Exception(result.Message);

            return result.Data;
        }

        public IEnumerable<FileEntiy> GetUpgradeFiles(string key = "")
        {
            string json = _fileAccess.GetUpgradeFiles(key);
            var result = JsonUtil.Deserializer<ResultEntiy<FileEntiy[]>>(json);

            if (result == null)
                throw new Exception("文件数据获取失败!");
            if (result.State != ResultCode.Success)
                throw new Exception(result.Message);

            return result.Data;
        }

        public void UploadFile(string file, string filePath, 
            Action<int> prograssChanged, 
            Action<AsyncCompletedEventArgs> completed)
        {
            _fileAccess.UploadFile(file, filePath, prograssChanged, completed);
        }
    }
}
