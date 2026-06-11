using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Myself.WinAlarmApp
{
    public partial class FrmAlarmList : Form
    {
        public FrmAlarmList()
        {
            InitializeComponent();
        }

        private void FrmAlarmList_Load(object sender, EventArgs e)
        {
            dgvAlarmList.AutoGenerateColumns = false;
            CommonClass.UpdateAlarmLogList += CommonClass_UpdateAlarmLogList;
            UpdageDgvDataSource();
        }

        private void CommonClass_UpdateAlarmLogList()
        {
            dgvAlarmList.Invoke(new Action(() =>
            {
                //刷新列表页----重新绑定一下
                UpdageDgvDataSource();
            }));
        }

        /// <summary>
        /// 绑定预警记录列表到dgv中
        /// </summary>
        private void UpdageDgvDataSource()
        {
            dgvAlarmList.DataSource = null;
            if (CommonClass.logList.Count > 0)
                dgvAlarmList.DataSource = CommonClass.logList;
        }
    }
}
