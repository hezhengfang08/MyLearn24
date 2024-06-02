using ICSharpCode.SharpZipLib.Zip;
using MySelf.QOSM.QuickOrderAPP.ViewModels;
using MySelf.QOSM.QuickOrderAPP.ViewModels.Food;
using MySelf.QOSM.QuickOrderAPP.ViewModels.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MySelf.QOSM.QuickOrderAPP.Utils
{
    public class CommonHelper
    {
        public static int selMainMenuId = 0; //选择的主导菜单的编号
        public static int loginRoleId = 0;//登录的角色编码
        public static LoginInfo loginUser; //保存登录者信息
        public static Action existLogin; //退出登录的委托
        public static List<SelFoodItem> hasSelFoods = new List<SelFoodItem>();//已选点餐列表
        public static List<string> iconList = new List<string>();//字体图标列表
        public static List<PageInfo> pageList = new List<PageInfo>();//页面集合下
        public static string GetDelTypeName(int isDeleted)
        {
            string typeName = "";
            switch (isDeleted)
            {
                case 1:typeName="删除";break;
                case 2:typeName="恢复";break;
                case 3:typeName="移除";break;
                default:break;

            }
            return typeName;
        }
        public static List<string> GetIconList()
        {
            if(iconList.Count == 0)
            {
                iconList = new List<string>()
                {
                     "e6ae",
                    "e6ad",
                    "e613",
                    "e63c",
                    "e606",
                    "e63a",
                    "e667"
                };
            }
            return iconList;
        }
        public static List<PageInfo> GetPageList()
        {
            if(pageList.Count == 0)
            {
                Assembly assembly = Application.ResourceAssembly;
                int length = assembly.GetName().Name.Length;
                List<PageInfo> pages = assembly.GetTypes().Where(t => t.BaseType == typeof(PageInfo)).Select(t => new PageInfo()
                {
                    PageText = GetPageTitle(t),
                    PageURL = t.FullName.Substring(length + 1).Replace('.', '/') + ".xaml"
                }).ToList() ;
                pages.Insert(0, new PageInfo()
                {
                    PageText = "请选择",
                    PageURL = ""
                });
                pageList = pages;

            }
            return pageList;
        }
        // <summary>
        /// 获取页面的标题
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string GetPageTitle(Type type)
        {
            string title = "";
            if (type.BaseType == typeof(Page))
            {
                Page page = (Page)Activator.CreateInstance(type);
                title = page.Title;
            }

            return title;
        }
    }
}
