using MySelf.QOSM.QuickOrderAPP.Utils;
using NPOI.OpenXmlFormats.Dml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MySelf.QOSM.QuickOrderAPP.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        /// <summary>
        /// 对话框
        /// </summary>
        /// <param name="key"></param>
        /// <param name="vmObj"></param>
        public void ShowDialog(string key, object vmObj)
        {
            WindowManager.ShowWindow(key, vmObj, true);
        }
        /// <summary>
        /// 打开窗口
        /// </summary>
        /// <param name="key"></param>
        /// <param name="vmObj"></param>
        public void ShowWindow(string key, object vmObj)
        {
            WindowManager.ShowWindow(key, vmObj, false);
        }
        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="win"></param>
        public void CloseWindow(object win)
        {
            WindowManager.CloseWindow(win);
        }
        /// <summary>
        /// 成功提示
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="title"></param>
        /// <param name="button"></param>
        public void ShowSuccess(string msg, string title = "成功提示", MessageBoxButton button = MessageBoxButton.OK)
        {
            HandyControl.Controls.MessageBox.Success(msg, title);
        }
        /// <summary>
        /// 错误提示
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="title"></param>
        /// <param name="button"></param>
        public void ShowError(string msg, string title = "错误提示", MessageBoxButton button = MessageBoxButton.OK)
        {
            HandyControl.Controls.MessageBox.Error(msg, title);
        }
        public MessageBoxResult ShowQuestion(string msg, string title = "询问提示", MessageBoxButton button = MessageBoxButton.OKCancel)
        {
            return HandyControl.Controls.MessageBox.Ask(msg, title);

        }
        public ICommand MinWindowCmd { get; set; }
        public ICommand MoveWindowCmd { get; set; } 
        public ICommand CloseWindowCmd { get; set; }

        public RelayCommand CreateCloseWindowCmd()
        {
            return new RelayCommand(win =>
            {
                CloseWindow(win);
            });
        }
        /// <summary>
        /// 拖动
        /// </summary>
        /// <returns></returns>
        public RelayCommand CreateMoveWindowCmd()
        {
            return new RelayCommand(win =>
           {
               Window window = win as Window;
               if (window != null)
               {
                   if (window.WindowState == WindowState.Maximized)
                   {
                       window.WindowState = WindowState.Normal;
                   }
               }
               window?.DragMove();
           });
        }
       /// <summary>
       /// 最小化
       /// </summary>
       /// <returns></returns>
        public RelayCommand CreateMinWindowCmd()
        {
            return new RelayCommand(win =>
            {
                Window window = win as Window;
                window.WindowState = WindowState.Minimized;
            });
        }
        /// <summary>
        /// 转换字符图标代码
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GetIconCode(string code)
        {
            if (!string.IsNullOrEmpty(code))
                return ((char)int.Parse(code, NumberStyles.HexNumber)).ToString();
            return null;
        }
    }
}
