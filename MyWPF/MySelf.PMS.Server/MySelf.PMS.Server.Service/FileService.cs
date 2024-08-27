using MySelf.PMS.Server.Entities;
using MySelf.PMS.Server.IService;
using SqlSugar;


namespace MySelf.PMS.Server.Service
{
    public class FileService : IFileService
    {
        ISqlSugarClient _sugarClient;
        public FileService(ISqlSugarClient sugarClient)
        {
            _sugarClient = sugarClient;
        }

        public int AddOrUpdate(UpgradeFileEntity entity)
        {
            // 判断具体是新增还是更新
            // 以文件名称进行区分
            int count = 0;
            var file = _sugarClient.Queryable<UpgradeFileEntity>()
                .First(f => f.FileName == entity.FileName);
            if (file == null)
            {
                // 新增
                count = _sugarClient.Insertable(entity).ExecuteCommand();
            }
            else
            {
                file.FileMd5 = entity.FileMd5;
                file.FilePath = entity.FilePath;
                file.UploadTime = entity.UploadTime;
                file.Length = entity.Length;
                // 更新
                count = _sugarClient.Updateable(file).ExecuteCommand();
            }

            return count;
        }

        public int Delete(string file_name)
        {
            return _sugarClient.Deleteable<UpgradeFileEntity>()
               .Where(f => f.FileName == file_name).ExecuteCommand();
        }

        public UpgradeFileEntity GetFileByName(string file_name)
        {
            return _sugarClient.Queryable<UpgradeFileEntity>()
               .First(f => f.FileName == file_name);
        }

       

        public IEnumerable<UpgradeFileEntity> GetUpgradeFiles(string key)
        {
            return _sugarClient.Queryable<UpgradeFileEntity>()
                      .Where(f => string.IsNullOrEmpty(key) ||
                                  f.FileName.Contains(key))
                      .ToList();
        }
    }
}
