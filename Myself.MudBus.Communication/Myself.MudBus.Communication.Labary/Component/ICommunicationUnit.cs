using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.MudBus.Communication.Labary.Component
{
    public interface ICommunicationUnit
    {
        /// <summary>
        /// 连接超时时间
        /// </summary>
        public int ConnectTimeout { get; set; }

        /// <summary>
        /// 打开动作
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public Result<bool> Open(int timeout);
        /// <summary>
        /// 关闭
        /// </summary>
        public void Close();

        /// <summary>
        ///  发送与接收报文
        /// </summary>
        /// <param name="req">请求报文</param>
        /// <param name="receiveLen">接收长度</param>
        /// <param name="errorLen">错误长度</param>
        /// <returns></returns>
        public Result<byte> SendAndReceive(List<byte> req, int receiveLen, int errorLen);
    }
}
