using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace My.CommuniTest
{
    public class SocketTest
    {
        Socket client;
        public void Test()
        {
            StartTcpServicer();
        }
        private void StartTcpServicer()
        {
            Socket server;
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9090));
            server.Listen(1);
            // 同步执行    卡线程
            var ccc = server.Accept();

        }
    }
}
