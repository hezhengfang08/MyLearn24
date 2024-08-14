using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.PMS.Client.Entities
{
    public class ResultEntiy<T>
    {
        public ResultCode RCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }

    public enum ResultCode {
        Success =1,
        Fail = 2,
        Error =3
    }

}
