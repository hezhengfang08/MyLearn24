using Prism.Mvvm;


namespace MySelf.PMS.Client.SystemModule.Models
{
    public class FileModel : BindableBase
    {
        public int Index { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string UploadTime { get; set; }
        public string FullPath { get; set; }
        private int _progressValue;
        public int ProgressValue
        {
            get { return _progressValue; }
            set { SetProperty<int>(ref _progressValue, value); }
        }
        private string _state;
        public string State
        {
            get { return _state; }
            set { SetProperty(ref _state, value); }
        }
        private string _error;

        public string Error
        {
            get { return _error; }
            set { SetProperty<string>(ref _error, value); }
        }
    }
}

