using MySelf.PMS.Client.Entities;
using System.ComponentModel;
namespace MySelf.PMS.Client.IBLL
{
    public interface IFileService
    {
        IEnumerable<FileEntiy> GetUpgradeFiles(string key = "");

        void UploadFile(string file, string filePath, Action<int> prograssChanged, Action<AsyncCompletedEventArgs> completed);

        int DeleteFile(string fileName);
    }
}
