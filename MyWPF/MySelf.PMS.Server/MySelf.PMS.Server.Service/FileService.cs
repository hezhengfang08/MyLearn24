using MySelf.PMS.Server.Entities;
using MySelf.PMS.Server.IService;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.PMS.Server.Service
{
    public class FileService : IFileService
    {
        ISqlSugarClient _sugarClient;
        public FileService(ISqlSugarClient sugarClient)
        {
            _sugarClient = sugarClient;
        }
        public IEnumerable<UpgradeFileEntity> GetUpgradeFiles()
        {
            return _sugarClient.Queryable<UpgradeFileEntity>().ToList();
        }
    }
}
