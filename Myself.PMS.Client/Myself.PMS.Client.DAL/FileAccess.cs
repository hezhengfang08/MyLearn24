using Myself.PMS.Client.IDAL;
using System.IO;

namespace Myself.PMS.Client.DAL
{
    public class FileAccess : WebAccess, IFileAccess
    {
        public string GetUpgradeFiles()
        {
            string uri = "/api/File";

            return this.Get(uri);
        }
    }
}
