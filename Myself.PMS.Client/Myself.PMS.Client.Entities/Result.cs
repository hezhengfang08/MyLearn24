using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.Entities
{
   /// <summary>
   /// 客户端接收服务端数据后，做反序列化时的结构
   /// </summary>   
    public class Result<T>
    {
        public int State { get; set; }
        public string ExceptionMessage { get; set; }
        public T Data { get; set; }
    }
}
