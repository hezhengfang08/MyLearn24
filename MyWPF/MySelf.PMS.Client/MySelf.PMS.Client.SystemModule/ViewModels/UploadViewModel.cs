using Microsoft.Win32;
using MySelf.PMS.Client.Common;
using MySelf.PMS.Client.IBLL;
using MySelf.PMS.Client.SystemModule.Models;
using Prism.Commands;
using Prism.Regions;
using System.Collections.ObjectModel;
namespace MySelf.PMS.Client.SystemModule.ViewModels
{
    public class UploadViewModel : PageViewModelBase
    {
        public ObservableCollection<FileModel> Files { get; set; } =
            new ObservableCollection<FileModel>();

        public DelegateCommand UploadCommand { get; set; }

        IFileService _fileService;
        public UploadViewModel(IRegionManager regionManager,
            IFileService fileService) : base(regionManager)
        {
            this.PageTitle = "更新文件上传";

            _fileService = fileService;

            UploadCommand = new DelegateCommand(DoUpload);

            this.Refresh();
        }

        public override void Refresh()
        {
            Files.Clear();

            var files = _fileService.GetUpgradeFiles(this.SearchKey).ToList();

            for (int i = 0; i < files.Count(); i++)
            {
                var file = files[i];

                FileModel model = new FileModel();
                model.Index = i + 1;
                model.FileName = file.fileName;
                model.UploadTime = file.uploadTime.ToString("yyyy-MM-dd HH:mm:ss");
                model.FilePath = file.filePath;
                model.State = "0";// 属于是从服务器上获取的原始数据

                Files.Add(model);
            }
        }
        public override void DoModify(object model)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == true)
            {
                // 文件名称
                // aaa.dll
                string[] file_names = openFileDialog.SafeFileNames;
                // d:\\files\aaa.dll
                //openFileDialog.FileNames
                // 文件完整路径
                // 判断名称是否在列表中，如果在，将相关信息设置一下，状态设置待上传
                //    如果没在，将信息添加到列表
                for (int i = 0; i < file_names.Length; i++)
                {
                    var file_model = Files
                                .FirstOrDefault(f => f.FileName.ToLower() == file_names[i].ToLower());
                    if (file_model != null)
                    {
                        file_model.FullPath = openFileDialog.FileNames[i];
                        file_model.State = "1";// 等待上传状态
                    }
                    else
                    {
                        Files.Add(new FileModel
                        {
                            Index = Files.Count + 1,
                            FileName = file_names[i],
                            FullPath = openFileDialog.FileNames[i],
                            FilePath = @".\",
                            State = "1"
                        });
                    }
                }
            }
        }
        public override void DoDelete(object model)
        {
            base.DoDelete(model);
        }
        int index = 0;
        private void DoUpload()
        {
            if (index >= Files.Count) return;

            var file = Files[index];
            try
            {
                if (file.State == "1" || file.State == "-1")
                {
                    file.State = "2";// 状态为正在上传
                    _fileService.UploadFile(file.FullPath, file.FilePath,
                        progress =>
                        {
                            // 上传进度
                            file.ProgressValue = progress;
                        },
                        () =>
                        {
                            // 上传完成
                            file.State = "3";// 上传完成
                            index++;
                            DoUpload();
                        });
                }
                else
                {
                    index++;
                    DoUpload();
                }
            }
            catch (Exception ex)
            {
                file.Error = ex.Message;
                file.State = "-1";
            }

        }
    }
}
