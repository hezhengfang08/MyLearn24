using System.IO.Ports;

namespace My.SerialCommuniTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            // 这是个静态方法，获取程序运行环境下的所有串口名称，供选择打开连接
            string[] ports = SerialPort.GetPortNames();
            // 默认使用过程
            SerialPort serialPort = new SerialPort();
            // 作用：打开串口，基于打开的串口进行收发数据
            // 指定打开的串口
            serialPort.PortName = "Com1";
            // 波特率：每秒钟传输的位数量   101001010  101010110010101001
            // 以设备为准
            serialPort.BaudRate = 9600;
            // 数据位：以多少个位为一个数据
            serialPort.DataBits = 8;
            // 校验位：针对传输数据被干扰时的对应，奇   偶
            serialPort.Parity = Parity.None;
            serialPort.StopBits = StopBits.One;
            #region 属性
            serialPort.WriteBufferSize = 5100;
            serialPort.ReadBufferSize = 4096;
            serialPort.NewLine = "\n";
            //serialPort.ReadTimeout = 5000;
            #endregion
            //bool state =  serialPort.IsOpen ;
            serialPort.Open();
            //state = serialPort.IsOpen;

            #region 关于发送的方法调用
            // 发送
            //** public void Write(byte[] buffer, int offset, int count)
            //public void Write(char[] buffer, int offset, int count)
            //public void Write(string text)
            byte[] buffer = new byte[] { 1, 2, 125, 255 };
            serialPort.Write(buffer, 0, buffer.Length);
            #endregion
            //serialPort.Open();
            Console.WriteLine(ports.Length);

            #region 关于打开串口时可能出现的异常
            // 打开时，可能出现异常情况：
            // 1、设置了不存在的串口名称
            //    异常：System.IO.FileNotFoundException:“Could not find file 'COM10'.”

            // 2、重复打开串口
            //    异常：System.InvalidOperationException:“The port is already open.”

            // 3、串口独占：串口已经被其他程序打开过
            //    异常：System.UnauthorizedAccessException:“Access to the path 'COM1' is denied.”
            #endregion

            #region 关于接收的方法调用
            // 涉及到一个接口缓存  区
            //serialPort.WriteBufferSize
            // 关于缓冲区问题，下次课单独演示

            // 接收
            // ** public int Read(byte[] buffer, int offset, int count)
            // public int Read(char[] buffer, int offset, int count)
            //byte[] bytes = new byte[10];
            //serialPort.Read(bytes, 0, 10);// 卡线程
            //Console.WriteLine(bytes[1]);
            byte[] bytes = new byte[serialPort.BytesToRead];
            serialPort.Read(bytes, 0, bytes.Length);// 不卡线程 
            Console.WriteLine(bytes.Length);
            #endregion
        }
    }
}
