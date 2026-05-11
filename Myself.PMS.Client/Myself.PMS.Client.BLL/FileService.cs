using Myself.PMS.Client.Entities;
using Myself.PMS.Client.IBLL;
using Myself.PMS.Client.IDAL;
using Myself.PMS.Client.Utils;
using System.Collections.Generic;

namespace Myself.PMS.Client.BLL
{
    public class FileService : IFileService
    {
        IFileAccess _fileAccess;
        public FileService(IFileAccess fileAccess)
        {
            _fileAccess = fileAccess;
        }
        public IEnumerable<FileEntiy> GetUpgradeFiles(string key = "")
        {
            string json = _fileAccess.GetUpgradeFiles(key);
            var result = json.Deserialize<Result<FileEntiy[]>>();

            if (result == null)
                throw new Exception("文件数据获取失败!");
            if (result.State != 200)
                throw new Exception(result.ExceptionMessage);

            return result.Data;
        }

        public void UploadFile(string file, string filePath, Action<int> prograssChanged, Action completed)
        {
            _fileAccess.UploadFile(file, filePath, prograssChanged, completed);
        }
    }
}
