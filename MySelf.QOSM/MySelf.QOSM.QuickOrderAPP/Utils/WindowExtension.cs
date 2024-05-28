using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MySelf.QOSM.QuickOrderAPP.Utils
{
    /// <summary>
    ///  窗口扩展类-----扩展方法-----位置：静态类中，静态方法
    /// </summary>
    public static class WindowExtension
    {
        /// <summary>
        /// 注册页面或者窗体当前是窗体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="window"></param>
        /// <param name="key"></param>
        public static void Register<T>(this Window window, string key)
        {
             WindowManager.Register<T>(key);
        }
        /// <summary>
        /// Page注册
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="page"></param>
        /// <param name="key"></param>
        public static void Register<T>(this Page page, string key)
        {
            WindowManager.Register<T>(key);
        }
        /// <summary>
        /// Window 注册
        /// </summary>
        /// <param name="window"></param>
        /// <param name="key"></param>
        /// <param name="type"></param>
        public static void Register(this Window window ,string key, Type type)
        {
            WindowManager.Regiseter(key, type); 
        }
        /// <summary>
        /// Page  注册
        /// </summary>
        /// <param name="window"></param>
        /// <param name="key"></param>
        /// <param name="type"></param>
        public static void Register(this Page page, string key, Type type)
        {
            WindowManager.Regiseter(key, type);
        }

    }
}
