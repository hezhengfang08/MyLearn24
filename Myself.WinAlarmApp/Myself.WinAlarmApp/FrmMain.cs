using Modbus.Device;
using Myself.WinAlarmApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Myself.WinAlarmApp
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }
        IModbusMaster master = null;//主站设备
        SerialPort serialPort = null;//串口对象
        List<SlaveInfo> slaves = new List<SlaveInfo>();//存储从站信息列表
        List<ParaInfo> paraInfos = new List<ParaInfo>();//参数列表
        List<AlarmInfo> alarmInfos = new List<AlarmInfo>();//存储预警设置列表
        System.Timers.Timer timer = null;//定时器  用来定时采集数据
        private void FrmMain_Load(object sender, EventArgs e)
        {
            //加载从站列表
            LoadSlaveList();
            //加载参数列表
            LoadParaList();
            //加载预警设置列表
            LoadAlarmSetList();

            //创建串口对象及主站设备
            CreateConn();
            //初始定时器
            timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.AutoReset = true;//重复执行
            timer.Elapsed += Timer_Elapsed;
        }
        /// <summary>
        /// 串口对象、主站设备
        /// </summary>
        private void CreateConn()
        {
            serialPort = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One);
            if (serialPort != null)
            {
                try
                {
                    serialPort.Open();
                    if (serialPort.IsOpen)
                    {
                        //创建主站设备
                        master = ModbusSerialMaster.CreateRtu(serialPort);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        /// <summary>
        /// 加载从站列表
        /// </summary>
        private void LoadSlaveList()
        {
            XmlDocument doc = new XmlDocument();
            string path = Application.StartupPath + "/configFiles/XSlave.xml";
            doc.Load(path);
            XmlElement root = doc.DocumentElement;//根节点
            if (root != null)
            {
                foreach (XmlNode node in root.ChildNodes)
                {
                    SlaveInfo slave = new SlaveInfo();
                    slave.SlaveId = byte.Parse(node.SelectSingleNode("SlaveId").InnerText);
                    slave.FuntionCode = byte.Parse(node.SelectSingleNode("FunctionCode").InnerText);
                    slave.StartAddress = ushort.Parse(node.SelectSingleNode("StartAddress").InnerText);
                    slave.Count = ushort.Parse(node.SelectSingleNode("Count").InnerText);
                    slaves.Add(slave);
                }
            }
        }
        /// <summary>
        /// 加载参数列表
        /// </summary>
        private void LoadParaList()
        {
            XmlDocument doc = new XmlDocument();
            string path = Application.StartupPath + "/configFiles/XParas.xml";
            doc.Load(path);
            XmlElement root = doc.DocumentElement;//根节点
            if (root != null)
            {
                foreach (XmlNode node in root.ChildNodes)
                {
                    ParaInfo para = new ParaInfo();
                    para.ParaName = node.SelectSingleNode("ParaName").InnerText;
                    para.SlaveId = byte.Parse(node.SelectSingleNode("SlaveId").InnerText);
                    para.Address = ushort.Parse(node.SelectSingleNode("Address").InnerText);
                    para.DataType = node.SelectSingleNode("DataType").InnerText;
                    para.Note = node.SelectSingleNode("Note").InnerText;
                    paraInfos.Add(para);
                }
            }
        }

        /// <summary>
        /// 加载预警设置列表
        /// </summary>
        private void LoadAlarmSetList()
        {
            XmlDocument doc = new XmlDocument();
            string path = Application.StartupPath + "/configFiles/XAlarmValueSet.xml";
            doc.Load(path);
            XmlElement root = doc.DocumentElement;//根节点
            if (root != null)
            {
                foreach (XmlNode node in root.ChildNodes)
                {
                    AlarmInfo alarm = new AlarmInfo();
                    alarm.ParaType = node.SelectSingleNode("ParaType").InnerText;
                    alarm.AlarmType = int.Parse(node.SelectSingleNode("AlarmType").InnerText);
                    alarm.AlarmValue = node.SelectSingleNode("AlarmValue").InnerText;
                    alarmInfos.Add(alarm);
                }
            }
        }
        /// <summary>
        /// 实时采集与检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //定时读与加载
            ReadAndLoad();
        }
        private void ReadAndLoad()
        {

        }
    }
}
