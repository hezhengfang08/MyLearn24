using Myself.PMS.Client.Entities;
using Myself.PMS.Client.IDAL;
using System.IO;

namespace Myself.PMS.Client.DAL
{
    public class FileAccess : WebAccess, IFileAccess
    {
        public FileAccess(GlobalValues globalValues) : base(globalValues)
        {
        }

        public string GetUpgradeFiles()
        {
            string uri = "/api/File";

            return this.Get(uri);
        }
    }
}
