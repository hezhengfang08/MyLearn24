using QRCoder;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
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
using static System.Net.Mime.MediaTypeNames;

namespace Mysefl.SmartParking.Simulation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
    
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
        public ImageBrush QRImage { get; set; }
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
            this.InitServer(server_1, Port1, "client_1"); //出口  
            this.InitServer(server_2, Port2, "client_2"); //入口
            this.InitServer(server_3, Port3, "client_3"); //二维码提示
        }
        private void InitServer(Socket server, int port, string key)
        {
            server = new Socket(SocketType.Stream, ProtocolType.Tcp);
            server.SendBufferSize= 1024*1024*1024;
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
                    Task.Run(() =>
                    {
                        while (true)
                        {
                            try
                            {
                                byte[] all_header_bytes = new byte[8];
                                client.Receive(all_header_bytes);
                                // 后续所有子包的字节
                                int len = BitConverter.ToInt32(all_header_bytes, 4);

                                // 将所子包的字节获取到
                                byte[] bytes = new byte[len];
                                int count = client.Receive(bytes);
                                if (count == 0) break;

                                if (bytes[2] == 0x0A)
                                {
                                    if ((client.LocalEndPoint as IPEndPoint).Port == 9090)
                                    {
                                        // 对入口进行抬杆操作
                                        //this.rt1.Angle = -90;
                                        // 触发一个动画，第一步先将杆抬起来   2秒后自动落下
                                        // 关键帧动画
                                        this.Dispatcher.Invoke(() =>
                                        {
                                            VisualStateManager.GoToElementState(this, "EntranceCloseState", false);
                                            VisualStateManager.GoToElementState(this, "EntranceOpenState", false);
                                        });
                                    }
                                    else if ((client.LocalEndPoint as IPEndPoint).Port == 9090)
                                    {
                                        // 对出口进行抬杆操作
                                        // 关键帧动画
                                        this.Dispatcher.Invoke(() =>
                                        {
                                            VisualStateManager.GoToElementState(this, "ExitCloseState", false);
                                            VisualStateManager.GoToElementState(this, "ExitOpenState", false);
                                        });
                                    }
                                }
                                if (bytes[2] == 0x20)
                                {
                                    // 应用系统发送了一个支付链接
                                    // 利用这个链接生成一个二维码图片
                                    // 最终显示在界面
                                    byte[] bytes_len = new byte[] {
                                        bytes[4],
                                        bytes[5],
                                        bytes[6],
                                        bytes[7],
                                    };
                                    len = BitConverter.ToInt32(bytes_len, 0);
                                    byte[] str_bytes = bytes.ToList().GetRange(8, len).ToArray();
                                    // 字节数组-》转成字符串
                                    string url = Encoding.UTF8.GetString(str_bytes);


                                    // 用二维码的方式进行显示
                                    QRCodeGenerator qrGenerator = new QRCoder.QRCodeGenerator();
                                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);

                                    QRCode qrcode = new QRCode(qrCodeData);
                                    Bitmap qrCodeImage = qrcode.GetGraphic(5, System.Drawing.Color.Black, System.Drawing.Color.White, null, 15, 6, false);
                                    // 将Bitmap显示在界面上   转成ImageBursh
                                    this.Dispatcher.Invoke(() =>
                                    {
                                        QRImage = new ImageBrush();

                                        IntPtr hBitmap = qrCodeImage.GetHbitmap();// 需要释放
                                        ImageSource imageSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

                                        QRImage.ImageSource = imageSource;
                                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("QRImage"));
                                    });
                                }
                            }
                            catch { break; }
                        }
                    });
                
            }
            });
        }

        private List<byte> GetSendBytes()
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
            DateTime dt = DateTime.Now;
            li.rec_time = new RecTime
            {
                y = dt.Year,
                m = dt.Month,
                d = dt.Day,
                hh = dt.Hour,
                mm = dt.Minute,
                ss = dt.Second
            };
            li.item = new List<ItemInfo>()
            {
                new ItemInfo{
                    license="苏E05EV8",
                    color="1",
                    nType=0,
                    nConfidence=95,
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
            byte[] file_Bytes = File.ReadAllBytes("imgs/logo_64.png");
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
            file_Bytes = File.ReadAllBytes("imgs/yzm.png");
            small_bytes.AddRange(BitConverter.GetBytes(file_Bytes.Length));
            small_bytes.AddRange(file_Bytes);


            all_bytes.AddRange(BitConverter.GetBytes(info_bytes.Count() + full_bytes.Count() + small_bytes.Count()));
            all_bytes.AddRange(info_bytes);
            all_bytes.AddRange(full_bytes);
            all_bytes.AddRange(small_bytes);
            return all_bytes;
        }
       

        private void ButtonEnter_Click(object sender, RoutedEventArgs e)
        {
            List<byte> all_bytes = GetSendBytes();

            // 利用对应的客户端对接进行发送
            clients["client_1"].Send(all_bytes.ToArray());
        }
        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            List<byte> all_bytes = GetSendBytes();

            // 利用对应的客户端对接进行发送
            clients["client_2"].Send(all_bytes.ToArray());
        }
    }
}