using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zero.Domain.Shared
{
    public enum ApiResponseCode:int
    {
        /// <summary>
        /// 成功
        /// </summary>
        Succeed = 0,

        /// <summary>
        /// 失败
        /// </summary>
        Failed = 1
    }
}
