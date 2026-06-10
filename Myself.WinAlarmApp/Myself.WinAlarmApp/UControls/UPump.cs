using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Myself.WinAlarmApp.UControls
{
    public partial class UPump : UserControl
    {
        public UPump()
        {
            InitializeComponent();
        }
        public event EventHandler ChangedStateClick;//状态改变事件
        private Bitmap lightImg = Properties.Resources.light_off;
        /// <summary>
        /// 水泵状态指示灯图片
        /// </summary>
        public Bitmap LightImg
        {
            get { return lightImg; }
            set
            {
                lightImg = value;
                picLight.Image = lightImg;
            }
        }
        private Color btnBgColor = Color.FromArgb(0, 192, 0);
        /// <summary>
        /// 切换按钮背景色
        /// </summary>
        public Color BtnBgColor
        {
            get { return btnBgColor; }
            set
            {
                btnBgColor = value;
                btnSwitch.BackColor = btnBgColor;
            }
        }
        private string btnText = "OFF";
        /// <summary>
        /// 切换按钮文本
        /// </summary>
        public string BtnText
        {
            get { return btnText; }
            set
            {
                btnText = value;
                btnSwitch.Text = btnText;
            }
        }
        private string pumpStateName;
        /// <summary>
        /// 水泵的状态参数名称
        /// </summary>
        public string PumpStateName
        {
            get { return pumpStateName; }
            set
            {
                pumpStateName = value;
            }
        }
        private bool actualState = false;
        /// <summary>
        /// 水泵的运行状态
        /// </summary>
        public bool ActualState
        {
            get { return actualState; }
            set
            {
                actualState = value;
                if (actualState)
                {
                    LightImg = WinAlarmApp.Properties.Resources.light_on;
                    BtnBgColor = Color.Orange;
                    BtnText = "ON";
                }
                else
                {
                    LightImg = WinAlarmApp.Properties.Resources.light_off;
                    BtnBgColor = Color.FromArgb(0, 192, 0); ;
                    BtnText = "OFF";
                }
            }
        }
        /// <summary>
        /// 切换水泵状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSwitch_Click(object sender, EventArgs e)
        {
            ChangedStateClick?.Invoke(this, new EventArgs());
        }

       
    }
}
