using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.PMS.Client.IDAL
{
    public interface IFileAccess : IWebAccess
    {
        string GetUpgradeFiles(string key);

        void UploadFile(string file, string save_path, Action<int> progress, Action completed);
    }
}
