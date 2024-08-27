using MySelf.PMS.Client.Entities;
using MySelf.PMS.Client.IBLL;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MySelf.PMS.Client.Start.ViewModels
{
    public class LoginViewModel : BindableBase, IDialogAware
    {
        public string Title => "登录";
        public event Action<IDialogResult> RequestClose;
        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {

        }



        public string UserName { get; set; } = "admin";
        public string Password { get; set; } = "123456";
        private bool _state;

        public bool State
        {
            get { return _state; }
            set { SetProperty<bool>(ref _state, value); }
        }

        private string _errorInfo;

        public string ErrorInfo
        {
            get { return _errorInfo; }
            set { SetProperty<string>(ref _errorInfo, value); }
        }


        public DelegateCommand LoginCommand { get; set; }

        public DelegateCommand<object> LoadedCommand { get; set; }


        IUserService _userService;
        GlobalValues _globalValues;
        public LoginViewModel(IUserService userService,
            IFileService fileService, GlobalValues globalValues)
        {
            _userService = userService;
            _globalValues = globalValues;

            LoginCommand = new DelegateCommand(DoLogin);
            LoadedCommand = new DelegateCommand<object>(obj =>
            {
                Task.Run(async () =>
                {
                    FrameworkElement element = obj as FrameworkElement;
                    if (element == null) return;
                    // 检查应用更新
                    //1、获取最新文件列表
                    // 
                    var files_server = fileService.GetUpgradeFiles().ToList();
                    //files_server.Clear();// 临时测试用   正常逻辑不要这个

                    // 2、文件判断，新增的直接下载；更新的直接下载；删除的直接删除
                    //    客户端本地需要一个记录，最后更新的记录（）

                    // 拿到本地文件列表 （Json文件  当前客户端的文件相关信息 MD5）
                    string path_temp = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
                    path_temp = Path.Combine(path_temp, "Zhaoxi.PMS", "upgrade_temp.json");
                    string json_str = "[]";
                    if (File.Exists(path_temp))
                        json_str = File.ReadAllText(path_temp);

                    List<FileEntiy> files_local = System.Text.Json
                        .JsonSerializer.Deserialize<List<FileEntiy>>(json_str);
                    // 本地数据与服务器数据的对比，根据名称
                    // (1) 本地列表中有，并且服务列表中也有：需要比对MD5值，如果不一致，说明这个文件需要更新
                    // (2) 本地列表中有，并且服务列表中没有：删除本地文件（可参照、选做）
                    // (3) 本地列表中没有，并且服务列表中有：服务列表中的文件直接下载

                    // 以客户端文件列表为基础进行文件删除判断
                    foreach (var fl in files_local)
                    {
                        if (!files_server.Exists(f => f.fileName == fl.fileName))
                            File.Delete(fl.filePath + "/" + fl.fileName);
                    }
                    // 以服务端文件列表为基础进行文件判断
                    List<string> update_file = new List<string>();
                    foreach (var sf in files_server)
                    {
                        if (files_local.Exists(f => f.fileName == sf.fileName && f.fileMd5 != sf.fileMd5) ||
                            !files_local.Exists(f => f.fileName == sf.fileName))
                        {
                            // 这个文件是需要下载的(名称、路径、大小)
                            // Zhaoxi.PMS.Client.BLL.dll|UpgradeFiles|100
                            update_file.Add(sf.fileName + "|" + sf.filePath + "|" + sf.length + "|" + sf.fileMd5);
                        }
                    }
                    // 服务器拿到文件列表中的文件都得下载
                    if (update_file.Count > 0)
                    {
                        // 启动更新程序，并且将更新文件列表传给它
                        Process.Start("Zhaoxi.PMS.Client.Upgrade.exe", update_file);
                        // 

                        // 下载完成进行服务文件列表的保存（Json序列化）
                        // 这个逻辑需要在Upgrade.exe进程里处理，判断正常更新完成后写入
                        //json_str = System.Text.Json.JsonSerializer.Serialize(update_file);
                        //File.WriteAllText("upgrade_temp.json", json_str);

                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            //Application.Current.Shutdown();
                            System.Environment.Exit(0);// 结束进程
                        }));
                    }


                    // pied pi :  提示一个Bug：
                    // 如果更新途中部分文件失败时，然后直接关闭更新窗口
                    // 下次再重新打开主程序有可能不会在摄氏打不开的情况
                    // 【下次课分享方案】
                    // 1、zip文件整体压缩，下载到临时目录；
                    // 2、文件的依赖关系；
                    // 3、每次更新，将历史文件临时保存到指定目录，回退（推荐）     【下一期进行升级】

                    // 这个等待只做调试使用，没有具体含义【建议使用时直接删除，不用管】
                    await Task.Delay(2000);

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        // 检查完成后，没有发现需要更新的文件，呈现登录输入区域
                        // ShowLoginState
                        VisualStateManager.GoToElementState(element, "ShowLoginState", true);
                    });
                });
            });


        }


        private void DoLogin()
        {
            // 将用户名和密码提交到WebApi，检查状态，将状态写入State属性
            // 需要返回用户相关信息  Token
            try
            {
                var user = _userService.Login(UserName, Password);
                // 登录成功

                _globalValues.Token = user.Token;

                   DialogParameters dps = new DialogParameters();
                dps.Add("user", user);
                RequestClose?.Invoke(new DialogResult(ButtonResult.OK, dps));
            }
            catch (Exception ex)
            {
                this.ErrorInfo = ex.Message;
            }
        }


    }
}

