using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Myself.WinAlarmApp
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {

            ushort addr2 = 3;
            // BitConverter      /256    %256
            var tt = (byte)(addr2 / 256);// BitConverter.GetBytes(addr)[1];
            var tt2 = addr2 % 256;// BitConverter.GetBytes(addr)[0]; ushort addr = 3;
            // BitConverter      /256    %256
            addr2 = 278;
            var tt3 = (byte)(addr2 / 256);// BitConverter.GetBytes(addr)[1];
            var tt4 = (byte)addr2 % 256;// BitConverter.GetBytes(addr)[0];
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }
    }
}
