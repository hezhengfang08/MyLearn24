using Myself.PMS.Server.Entities;
using Myself.PMS.Server.IService;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Server.Service
{
    public class FileService : IFileService
    {
        ISqlSugarClient _sqlSugarClient;
        public FileService(ISqlSugarClient sqlSugarClient)
        {
            _sqlSugarClient = sqlSugarClient;
        }
        public IEnumerable<UpgradeFileEntity> GetUpgradeFiles()
        {
            return _sqlSugarClient.Queryable<UpgradeFileEntity>().ToList();
        }
    }
}
