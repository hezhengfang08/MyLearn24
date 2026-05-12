using Myself.PMS.Client.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.IBLL
{
    public interface IFileService
    {
        IEnumerable<FileEntiy> GetUpgradeFiles(string key = "");

        void UploadFile(string file, string filePath, Action<int> prograssChanged, Action<AsyncCompletedEventArgs> completed);

        int DeleteFile(string fileName);
    }
}
