using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.PMS.Client.Entities
{
    public class ResultEntiy<T>
    {
        public ResultCode State { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }

    public enum ResultCode {
        Success =200,
        Fail = 400,
        Error =500
    }

}
