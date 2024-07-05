using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketClient
{
    internal class Program
    {
        static Socket clientSocket;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Connect();
        }
        //连接客户端，连接后开始监听发送过来的消息并且把输入的信息发给服务端
        static void Connect()
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {   //连接
                clientSocket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888));
                //独立线程来接受来自服务端的数据
                Thread receive = new Thread(Receive);
                receive.Start(clientSocket);

                //独立线程来发送数据给服务端
                Thread send = new Thread(Send);
                send.Start(clientSocket);

            }
            catch (Exception ex) { throw ex; }
        }
        static void Receive(object ob)
        {
            Socket client = ob as Socket;
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;  // 修改文字颜色为绿色
                try
                {
                    byte[] buffer = new byte[1024];
                    int len = client.Receive(buffer);
                    if (len > 0)
                    {
                        string msg = Encoding.UTF8.GetString(buffer);
                        Console.WriteLine("服务端说：" + msg);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    break;
                }
            }
            client.Close();
        }
        static void Send(object ob)
        {
            Socket send = ob as Socket;
            while (true) {
                Console.ForegroundColor = ConsoleColor.Red;
                //获取键盘输入
                string read = Console.ReadLine();
                byte[] buffer = Encoding.UTF8.GetBytes(read);
                send.Send(buffer);
            }
        }
    }
}
