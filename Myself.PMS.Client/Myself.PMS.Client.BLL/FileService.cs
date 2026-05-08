using Myself.PMS.Client.Entities;
using Myself.PMS.Client.IBLL;
using Myself.PMS.Client.IDAL;

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
            return System.Text.Json.JsonSerializer.Deserialize<List<FileEntiy>>(json);
        }
    }
}
