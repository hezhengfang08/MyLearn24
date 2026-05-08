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
        public IEnumerable<FileEntiy> GetUpgradeFiles()
        {
            string json = _fileAccess.GetUpgradeFiles();
            
            return json.Deserialize<List<FileEntiy>>();  
        }
    }
}
