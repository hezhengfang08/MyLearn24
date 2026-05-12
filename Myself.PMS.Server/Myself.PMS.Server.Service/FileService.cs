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

        public int AddOrUpdate(UpgradeFileEntity entity)
        {
            // 判断具体是新增还是更新
            // 以文件名称进行区分
            int count = 0;
            var file = _sqlSugarClient.Queryable<UpgradeFileEntity>()
                .First(f => f.FileName == entity.FileName);
            if (file == null)
            {
                // 新增
                count = _sqlSugarClient.Insertable(entity).ExecuteCommand();
            }
            else
            {
                file.FileMd5 = entity.FileMd5;
                file.FilePath = entity.FilePath;
                file.UploadTime = entity.UploadTime;
                file.Length = entity.Length;
                // 更新
                count = _sqlSugarClient.Updateable(file).ExecuteCommand();
            }

            return count;
        }

        public int Delete(string file_name)
        {
            return _sqlSugarClient.Deleteable<UpgradeFileEntity>()
                .Where(f => f.FileName == file_name).ExecuteCommand();
        }

        public UpgradeFileEntity GetFileByName(string file_name)
        {
            return _sqlSugarClient.Queryable<UpgradeFileEntity>()
                .First(f => f.FileName == file_name);
        }

        public IEnumerable<UpgradeFileEntity> GetUpgradeFiles(string key)
        {
            return _sqlSugarClient.Queryable<UpgradeFileEntity>()
                            .Where(f => string.IsNullOrEmpty(key) ||
                                        f.FileName.Contains(key))
                            .ToList();
        }
    }
}
