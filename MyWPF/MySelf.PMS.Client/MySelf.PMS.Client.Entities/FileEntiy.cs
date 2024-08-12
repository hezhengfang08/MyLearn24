using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.PMS.Client.Entities
{
    public class FileEntiy
    {
        public int fileId { get; set; }
        public string fileName { get; set; }
        public string fileMd5 { get; set; }
        public string filePath { get; set; }
        public DateTime uploadTime { get; set; }
        public int state { get; set; }
        public int length { get; set; }
    }
}
