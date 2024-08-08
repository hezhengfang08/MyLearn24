using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhaoxi.Forum.Domain.Shared
{
    /// <summary>
    /// 接口返回模型，返回值
    /// </summary>
    public class ApiResponse<TResult> : ApiResponse where TResult : class
    {
        public TResult Result { get; set; }

        public void IsSuccess(TResult result, string message = "")
        {
            Code = ApiResponseCode.Succeed;
            Message = message;
            Result = result;
        }
    }
    /// <summary>
    /// 接口返回模型
    /// </summary>
    public class ApiResponse
    {
        public ApiResponseCode Code { get; set; }

        public string Message { get; set; } = string.Empty;

        public bool Success => Code == ApiResponseCode.Succeed;

        public void IsSuccess(string message = "")
        {
            Code = ApiResponseCode.Succeed;
            Message = message;
        }

        public void IsFailed(string message = "")
        {
            Code = ApiResponseCode.Failed;
            Message = message;
        }

        public void IsFailed(Exception exception)
        {
            Code = ApiResponseCode.Failed;
            Message = exception?.InnerException?.StackTrace;
        }
    }
    public enum ApiResponseCode : int
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
