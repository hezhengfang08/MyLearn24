using Myself.WinAlarmApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.WinAlarmApp
{
    public class CommonClass
    {
        public static List<AlarmLogInfo> logList = new List<AlarmLogInfo>();//存储预警记录信息

        public static event Action UpdateAlarmLogList;//更新预警列表
        //调用更新事件
        public static void UpdateDgvAlarmList()
        {
            UpdateAlarmLogList?.Invoke();
        }
    }
}