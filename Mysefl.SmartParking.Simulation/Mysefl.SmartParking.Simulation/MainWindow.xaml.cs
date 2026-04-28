using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mysefl.SmartParking.Simulation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Socket server_1; //入口
        Socket server_2; //出口
        Socket server_3; //提示

        //Socket client_1;// 入口
        //Socket client_2;// 出口
        //Socket client_3;// 提示
        Dictionary<string, Socket> clients = new Dictionary<string, Socket>();

        public int Port1 { get; set; } = 9090;
        public int Port2 { get; set; } = 9091;
        public int Port3 { get; set; } = 9092;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            // server_1 = new Socket(SocketType.Stream, ProtocolType.Tcp);
            //server_1.Bind(new IPEndPoint(IPAddress.Any, Port1));
            //server_1.Listen();

            //Task.Run(() =>
            //{
            //    client_1 = server_1.Accept();
            //});

            //server_2 = new Socket(SocketType.Stream, ProtocolType.Tcp);
            //server_2.Bind(new IPEndPoint(IPAddress.Any, Port2));
            //server_2.Listen();

            //client_2 = server_2.Accept();

            //server_3 = new Socket(SocketType.Stream, ProtocolType.Tcp);
            //server_3.Bind(new IPEndPoint(IPAddress.Any, Port3));
            //server_3.Listen();

            //client_3 = server_3.Accept();
            this.InitServer(server_1, Port1, "client_1");
            this.InitServer(server_2, Port2, "client_2");
            this.InitServer(server_3, Port3, "client_3");
        }
        private void InitServer(Socket server, int port, string key)
        {
            server = new Socket(SocketType.Stream, ProtocolType.Tcp);
            server.Bind(new IPEndPoint(IPAddress.Any, port));
            server.Listen();

            Task.Run(() =>
            {
                while (true)
                {
                    var client = server.Accept();
                    Debug.WriteLine((client.RemoteEndPoint as IPEndPoint).Port);

                    if (clients.ContainsKey(key)) clients.Remove(key);
                    clients.Add(key, client);

                    // 这里是接收从监控程序到这个模拟程序的消息 
                    //client.Receive
                }
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonEnter_Click(object sender, RoutedEventArgs e)
        {
            // 总包头
            List<byte> all_bytes = new List<byte>()
            {
                (byte)'E',(byte)'P',0x00,0x00
            };

            // 发送入口识别结果
            // 组装需要发送的信息
            // 子包头--识别信息
            List<byte> info_bytes = new List<byte>
            {
                (byte)'E',(byte)'P',0x01,0x00
            };

            LicenseInfo li = new LicenseInfo();
            li.id = 123213;
            li.count = 1;// 识别了多少个车牌
            li.rec_time = new RecTime { y = 2026, m = 4, d = 28, hh = 12, mm = 1, ss = 12 };
            li.item = new List<ItemInfo>()
            {
                new ItemInfo{
                    license="苏E05EV8",
                    color="1",
                    nType=0,
                    nConfidence=99,
                    nTime=1
                }
            };
            string json_str = System.Text.Json.JsonSerializer.Serialize(li);
            byte[] info_data_bytes = Encoding.UTF8.GetBytes(json_str);
            // 这里是子包里的数据字节长度--4个字节表示
            info_bytes.AddRange(BitConverter.GetBytes(info_data_bytes.Length));
            info_bytes.AddRange(info_data_bytes);

            // 子包头--全图
            List<byte> full_bytes = new List<byte>
            {
                (byte)'E',(byte)'P',0x02,0x00
            };
            byte[] file_Bytes = File.ReadAllBytes("imgs/sua.png");
            full_bytes.AddRange(BitConverter.GetBytes(file_Bytes.Length));
            full_bytes.AddRange(file_Bytes);

            //using (FileStream fs = new FileStream("imgs/sua.png", FileMode.Open))
            //{
            //    byte[] file_bytes = new byte[fs.Length];
            //    fs.Read(file_bytes, 0, file_bytes.Length);
            //    // 这里是子包里的数据字节长度--4个字节表示
            //    full_bytes.AddRange(BitConverter.GetBytes(fs.Length));
            //    full_bytes.AddRange(file_bytes);
            //}


            // 子包头--全图
            List<byte> small_bytes = new List<byte>
            {
                (byte)'E',(byte)'P',0x03,0x00
            };
            var small_file_Bytes = File.ReadAllBytes("imgs/sua-01.jpg");
            small_bytes.AddRange(BitConverter.GetBytes(small_file_Bytes.Length));
            small_bytes.AddRange(small_file_Bytes);


            all_bytes.AddRange(BitConverter.GetBytes(info_bytes.Count() + full_bytes.Count() + small_bytes.Count()));
            all_bytes.AddRange(info_bytes);
            all_bytes.AddRange(full_bytes);
            all_bytes.AddRange(small_bytes);

            // 利用对应的客户端对接进行发送
            clients["client_1"].Send(all_bytes.ToArray());
        }
    }
}