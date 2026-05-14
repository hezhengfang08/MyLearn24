using Myself.PMS.Client.Entities;
using Myself.PMS.Client.IBLL;
using Myself.PMS.Client.IDAL;
using Myself.PMS.Client.Utils;
using System.Collections.Generic;
using System.ComponentModel;

namespace Myself.PMS.Client.BLL
{
    public class FileService : BaseService, IFileService
    {
        IFileAccess _fileAccess;
        public FileService(IFileAccess fileAccess)
        {
            _fileAccess = fileAccess;
        }


        public int DeleteFile(string fileName)
        {
            string json = _fileAccess.DeleteFile(fileName);
            //var result = json.Deserialize<Result<int>>();

            //if (result == null)
            //    throw new Exception("删除文件数据失败!");
            //if (result.State != 200)
            //    throw new Exception(result.ExceptionMessage);

            //return result.Data;
            return this.GetResult<int>(json);
        }
        public IEnumerable<FileEntiy> GetUpgradeFiles(string key = "")
        {
            string json = _fileAccess.GetUpgradeFiles(key);
            //var result = json.Deserialize<Result<FileEntiy[]>>();

            //if (result == null)
            //    throw new Exception("文件数据获取失败!");
            //if (result.State != 200)
            //    throw new Exception(result.ExceptionMessage);

            //return result.Data;
            return this.GetResult<FileEntiy[]>(json);
        }

        public void UploadFile(string file, string filePath,
            Action<int> prograssChanged,
            Action<AsyncCompletedEventArgs> completed)
        {
            _fileAccess.UploadFile(file, filePath, prograssChanged, completed);
        }
        public void UploadIdCard(string file, string name)
        {
            _fileAccess.UploadIdCard(file, name);
        }
    }
}
