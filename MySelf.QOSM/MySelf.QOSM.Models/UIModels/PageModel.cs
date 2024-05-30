using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.QOSM.Models.UIModels
{
    /// <summary>
    /// 分页查询结果类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageModel<T>
    {
        public int TotalCount { get; set; } 
        public List<T>DataList { get; set; }= new List<T>();    

    }
}
