using Myself.MudBus.Communication.Labary;
using Myself.MudBus.Communication.Labary.Modbus;
using System.IO.Ports;
using System.Text;

namespace Mysefl.MudBus.Communication.Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            int flag = 13;
            #region Communication 业务层面读测试 - RTU
            if (flag == 10)
            {
                // 创建通信实例  ModbusRTU  ModbusAscii  ModbusTcp
                // ModbusRtu  master=new ModbusRtu();
                // List<bool> datas = master.ReadCoils(1,1,0,10);

                ModbusRtu modbusRtu = new ModbusRtu("COM1", 9600, 8, Parity.None, StopBits.One);
                modbusRtu.EndianType = EndianType.AB;
                //modbusRtu.ReadCoils();
                //Result<bool> result = modbusRtu.ReadCoils(1, 1, 0, 2000);
                //Result<bool> result = modbusRtu.ReadCoils(1, "00001", 5);
                // 问题:count指的是寄存器数还是模拟量数？
                //Result<ushort> result = modbusRtu.ReadRegisters<ushort>(slaveNum: 1, funcCode: 3, startAddr: 0, dataCount: 200);
                //Result<ushort> result = modbusRtu.ReadRegisters<ushort>(slaveNum: 1, variable: "40001", count: 5);
                Result<float> result = modbusRtu.ReadRegisters<float>(1, 3, 0, dataCount: 1);

                //Result<byte> result = modbusRtu.ReadBytes(1, 3, 0, 2);
                if (result.Status)
                {
                    result.Datas.ForEach(data => Console.WriteLine(data));
                    //Console.WriteLine(Encoding.UTF8.GetString(result.Datas.ToArray()));
                }
                else
                {
                    Console.WriteLine(result.Message);
                }
                //result.Status = false;
                //result.Message = "excption";


            }
            #endregion

            #region Communication 业务层面写测试 - RTU
            if (flag == 11)
            {
                ModbusRtu modbusRtu = new ModbusRtu("COM1", 9600, 8, Parity.None, StopBits.One);
                Result<bool> result = modbusRtu.WriteCoils(1, 1, new List<bool> { true, false, true, true, false, false, true });
                //Result<bool> result = modbusRtu.WriteRegisters<ushort>(1, 1,
                //    new List<ushort> { 11, 22, 33, 44, 55, 66, 77 });
                //    

                byte[] v = Encoding.UTF8.GetBytes("123456");
                // Result<bool> result = modbusRtu.WriteBytes(1, 0, new List<byte>(v));
                if (result.Status)
                {
                    Console.WriteLine("写入成功");

                    Result<byte> r1 = modbusRtu.ReadBytes(1, 3, 0, (ushort)v.Length);
                    if (r1.Status)
                    {
                        //result.Datas.ForEach(data => Console.WriteLine(data));
                        Console.WriteLine(Encoding.UTF8.GetString(r1.Datas.ToArray()));
                    }
                    else
                    {
                        Console.WriteLine(r1.Message);
                    }
                }
                else
                {
                    Console.WriteLine(result.Message);
                }
            }
            #endregion

            #region Communication 业务层面测试 - Ascii
            if (flag == 12)
            {
                ModbusBase master = new ModbusAscii("COM1", 9600, 8, Parity.None, StopBits.One);
                //Result<bool> result = master.ReadCoils(1, "00001", 5);
                Result<float> result = master.ReadRegisters<float>(1, "40001", 5);
                if (result.Status)
                {
                    result.Datas.ForEach(data => Console.WriteLine(data));
                }
                else
                {
                    Console.WriteLine(result.Message);
                }
                //master.WriteCoils(1, 0, new List<bool> { true, true, false, false, true });
                master.WriteRegisters<float>(1, 0, new List<float> { 1.2f, 2.3f, 3.4f, 4.5f });
            }
            #endregion


            #region Communication 业务层面测试 - TCP
            if (flag == 13)
            {

                ModbusTcp master = new ModbusTcp("127.0.0.1");
                //Result<bool> result = master.ReadCoils(1, "00001", 5);
                //while (true)
                //{
                //    // 打开通信链路
                //    Result<bool> result = master.ReadCoils(1, 1, 0, 5);
                //    //Result<float> result = master.ReadRegisters<float>(1, "40001", 4);
                //    if (result.Status)
                //    {
                //        result.Datas.ForEach(data => Console.WriteLine(data));
                //    }
                //    else
                //    {
                //        Console.WriteLine(result.Message);
                //    }
                //}
                //master.WriteCoils(1, 0, new List<bool> { false, true, false, true, true });
                //master.WriteRegisters<float>(1, 0, new List<float> { 1.1f, 2.2f, 3.3f, 4.4f });

                // 关闭通信链路
                //master.Dispose();
                // A  ---   B
                // 
                // 0001 0002   40001 40001    通信异构平台功能

                for (int i = 0; i < 10; i++)
                {
                    master.ReadRegistersAsync<float>(1, 3, 0, 4, result =>
                    {
                        if (result.Status)
                        {
                            result.Datas.ForEach(data => Console.WriteLine(data));
                        }
                        else
                        {
                            Console.WriteLine(result.Message);
                        }

                    });
                }
            }
            #endregion
            Console.ReadLine();
        }
    }
}
