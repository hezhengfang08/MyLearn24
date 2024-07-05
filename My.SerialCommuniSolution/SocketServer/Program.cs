using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketServer
{
    public class Program
    {
        static Socket socketServer;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            SetListen();
        }
        static void SetListen()
        {
            socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                //这里最好改成IPAddress.Any, 这样放到服务器上就会同时监听所有网卡上的端口，比如有内外双网的服务器
                socketServer.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888));
                socketServer.Listen(2);
                Thread thread = new Thread(Listen);
                thread.Start(socketServer);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void Listen(object so)
        {
            Socket socket = so as Socket;
            while (true)
            {
                try
                {
                    Socket clientStoke = socket.Accept();//接受客户端接入
                                                         // 获取链接IP地址
                    string clientPoint = clientStoke.RemoteEndPoint.ToString();
                    //开启新线程来不停接受信息
                    Thread src = new Thread(Receive);
                    src.Start(clientStoke);
                    //开启新线程来不停发送信息
                    Thread send = new Thread(Send);
                    send.Start(clientStoke);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message, ex);
                    break;
                }
            }
            socket.Close();
        }
        static void Receive(object so)
        {
            Socket socket = so as Socket;
            string clientPoint = socket.RemoteEndPoint.ToString();
            while (true)
            {
                try
                {
                    byte[] buffer = new byte[1024];
                    int len = socket.Receive(buffer);
                    if (len == 0)
                    {
                        break;
                    }
                    string msg = Encoding.UTF8.GetString(buffer, 0, len);
                    Console.WriteLine("客户端说：" + msg);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
        static void Send(object so)
        {
            Socket socket = so as Socket;
            while (true)
            {
                string input = Console.ReadLine();
                byte[] msg = Encoding.UTF8.GetBytes(input);
                socket.Send(msg);
            }

        }
    }
}
