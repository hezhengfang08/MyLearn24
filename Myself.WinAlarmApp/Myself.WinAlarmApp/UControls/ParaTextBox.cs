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
    public partial class ParaTextBox : UserControl
    {
        public ParaTextBox()
        {
            InitializeComponent();
        }
        private string _dataValue;
        public string DataValue
        {
            get { return _dataValue; }
            set
            {
                _dataValue = value;
                lblText.Text = _dataValue + " " + _unit;
            }
        }
        private string _unit;
        public string Unit
        {
            get { return _unit; }
            set
            {
                _unit = value;
                lblText.Text = _dataValue + " " + _unit;
            }
        }
        private string _varName;
        /// <summary>
        /// 参数名
        /// </summary>
        public string VarName
        {
            get { return _varName; }
            set
            {
                _varName = value;
            }
        }
    }
}
