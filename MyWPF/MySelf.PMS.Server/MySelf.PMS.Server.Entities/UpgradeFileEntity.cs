using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.PMS.Server.Entities
{
    [SugarTable("upgrade_files")]
    public class UpgradeFileEntity
    {
        [SugarColumn(ColumnName = "file_id",IsPrimaryKey =true,IsIdentity =true)]
        public int FileId { get; set; }
        [SugarColumn(ColumnName = "file_name")]
        public string FileName { get; set; }
        [SugarColumn(ColumnName = "file_md5")]
        public string FileMd5 { get; set; }
        [SugarColumn(ColumnName = "file_path")]
        public string FilePath { get; set; }
        [SugarColumn(ColumnName = "upload_time")]
        public DateTime UploadTime { get; set; }
        [SugarColumn(ColumnName = "state")]
        public int state { get; set; }
        [SugarColumn(ColumnName = "length")]
        public long Length { get; set; }
    }
}

