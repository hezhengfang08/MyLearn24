using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MySelf.QOSM.QuickOrderAPP.Utils
{
    public class WindowManager
    {
        private static Hashtable registerT = new Hashtable();
        /// <summary>
        /// 泛型注册
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        public static void Register<T>(string key)
        {
            if (!registerT.ContainsKey(key))
            {
                registerT.Add(key, typeof(T));
            }
        }
        /// <summary>
        /// 非泛型注册
        /// </summary>
        /// <param name="key"></param>
        /// <param name="type"></param>
        public static void Regiseter(string key ,Type type)
        {
            if (!registerT.ContainsKey(key)) { 
            registerT.Add(key, type);   
            }
        }
        public static  void ShowWindow(string key, string objVM, bool isDialog)
        {
            if(!registerT.ContainsKey(key))
            {
                MessageBox.Show("对象没有注册");
                return;
            }
           var popWind = (Window) Activator.CreateInstance((Type)registerT[key]);
            if(popWind != null)
            {
                if(objVM == null)
                {
                    popWind.DataContext = objVM;
                }
                popWind.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                if (isDialog)
                {
                   popWind.ShowDialog();
                }
                else
                {
                    popWind.Show(); 
                }
            }
            else
            {
                MessageBox.Show("该窗口类型不存在");
            }
        }
        public static void CloseWindow(object win)
        {
            if(win != null) { 
                var window = win as Window;
                window.Close();
            }
        }
    }
}
