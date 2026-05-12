using System.ComponentModel;

namespace Myself.PMS.Client.IDAL
{
    public interface IFileAccess : IWebAccess
    {
        string GetUpgradeFiles(string key);

        void UploadFile(string file, string save_path, Action<int> progress, Action<AsyncCompletedEventArgs> completed);
        string DeleteFile(string file_name);
    }
}
