using MySelf.PMS.Client.Entities;
using MySelf.PMS.Client.IBLL;
using MySelf.PMS.Client.IDAL;
using System;
using System.Collections.Generic;
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
        public IEnumerable<FileEntiy> GetUpgradeFiles()
        {
            string json = _fileAccess.GetUpgradeFiles();
            return System.Text.Json.JsonSerializer.Deserialize<List<FileEntiy>>(json);
        }
    }
}
