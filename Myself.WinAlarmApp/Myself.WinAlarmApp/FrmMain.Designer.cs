namespace Myself.WinAlarmApp
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblState = new System.Windows.Forms.Label();
            this.btnAlarmList = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.txtPump03Fre = new Myself.WinAlarmApp.UControls.ParaTextBox();
            this.txtPump03Power = new Myself.WinAlarmApp.UControls.ParaTextBox();
            this.txtPump02Fre = new Myself.WinAlarmApp.UControls.ParaTextBox();
            this.txtPump02Power = new Myself.WinAlarmApp.UControls.ParaTextBox();
            this.lightPump03Power = new Myself.WinAlarmApp.UControls.UCAlarmControl();
            this.lightPump02Power = new Myself.WinAlarmApp.UControls.UCAlarmControl();
            this.lightPump01Power = new Myself.WinAlarmApp.UControls.UCAlarmControl();
            this.lightPump03Fre = new Myself.WinAlarmApp.UControls.UCAlarmControl();
            this.lightPump02Fre = new Myself.WinAlarmApp.UControls.UCAlarmControl();
            this.lightPump01Fre = new Myself.WinAlarmApp.UControls.UCAlarmControl();
            this.txtPump01Power = new Myself.WinAlarmApp.UControls.ParaTextBox();
            this.txtPump01Fre = new Myself.WinAlarmApp.UControls.ParaTextBox();
            this.uPump2 = new Myself.WinAlarmApp.UControls.UPump();
            this.uPump3 = new Myself.WinAlarmApp.UControls.UPump();
            this.uPump1 = new Myself.WinAlarmApp.UControls.UPump();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(668, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 19);
            this.label4.TabIndex = 7;
            this.label4.Text = "3#水泵";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(388, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 19);
            this.label3.TabIndex = 8;
            this.label3.Text = "2#水泵";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(613, 401);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(39, 19);
            this.label10.TabIndex = 9;
            this.label10.Text = "功率";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(614, 359);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(39, 19);
            this.label9.TabIndex = 10;
            this.label9.Text = "频率";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(323, 401);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 19);
            this.label8.TabIndex = 11;
            this.label8.Text = "功率";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(323, 359);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 19);
            this.label7.TabIndex = 12;
            this.label7.Text = "频率";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(57, 401);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 19);
            this.label6.TabIndex = 13;
            this.label6.Text = "功率";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(57, 359);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 19);
            this.label5.TabIndex = 14;
            this.label5.Text = "频率";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(91, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 19);
            this.label2.TabIndex = 15;
            this.label2.Text = "1#水泵";
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblState.Location = new System.Drawing.Point(207, 45);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(54, 20);
            this.lblState.TabIndex = 16;
            this.lblState.Text = "未启动";
            // 
            // btnAlarmList
            // 
            this.btnAlarmList.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAlarmList.ForeColor = System.Drawing.Color.Brown;
            this.btnAlarmList.Location = new System.Drawing.Point(655, 34);
            this.btnAlarmList.Name = "btnAlarmList";
            this.btnAlarmList.Size = new System.Drawing.Size(88, 42);
            this.btnAlarmList.TabIndex = 5;
            this.btnAlarmList.Text = "预警列表";
            this.btnAlarmList.UseVisualStyleBackColor = true;
            this.btnAlarmList.Click += new System.EventHandler(this.btnAlarmList_Click);
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStart.ForeColor = System.Drawing.Color.Blue;
            this.btnStart.Location = new System.Drawing.Point(76, 31);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(88, 42);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "启动";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtPump03Fre
            // 
            this.txtPump03Fre.DataValue = "50";
            this.txtPump03Fre.Location = new System.Drawing.Point(667, 357);
            this.txtPump03Fre.Name = "txtPump03Fre";
            this.txtPump03Fre.Size = new System.Drawing.Size(70, 20);
            this.txtPump03Fre.TabIndex = 21;
            this.txtPump03Fre.Unit = "HZ";
            this.txtPump03Fre.VarName = "Pump03Fre";
            // 
            // txtPump03Power
            // 
            this.txtPump03Power.DataValue = "30";
            this.txtPump03Power.Location = new System.Drawing.Point(667, 399);
            this.txtPump03Power.Name = "txtPump03Power";
            this.txtPump03Power.Size = new System.Drawing.Size(70, 20);
            this.txtPump03Power.TabIndex = 20;
            this.txtPump03Power.Unit = "KW";
            this.txtPump03Power.VarName = "Pump03Power";
            // 
            // txtPump02Fre
            // 
            this.txtPump02Fre.DataValue = "50";
            this.txtPump02Fre.Location = new System.Drawing.Point(376, 358);
            this.txtPump02Fre.Name = "txtPump02Fre";
            this.txtPump02Fre.Size = new System.Drawing.Size(70, 20);
            this.txtPump02Fre.TabIndex = 21;
            this.txtPump02Fre.Unit = "HZ";
            this.txtPump02Fre.VarName = "Pump02Fre";
            // 
            // txtPump02Power
            // 
            this.txtPump02Power.DataValue = "30";
            this.txtPump02Power.Location = new System.Drawing.Point(376, 400);
            this.txtPump02Power.Name = "txtPump02Power";
            this.txtPump02Power.Size = new System.Drawing.Size(70, 20);
            this.txtPump02Power.TabIndex = 20;
            this.txtPump02Power.Unit = "KW";
            this.txtPump02Power.VarName = "Pump02Power";
            // 
            // lightPump03Power
            // 
            this.lightPump03Power.AlarmLightColors = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192))))),
        System.Drawing.Color.Aqua};
            this.lightPump03Power.IsOn = false;
            this.lightPump03Power.Location = new System.Drawing.Point(748, 386);
            this.lightPump03Power.Name = "lightPump03Power";
            this.lightPump03Power.Size = new System.Drawing.Size(32, 37);
            this.lightPump03Power.TabIndex = 19;
            this.lightPump03Power.TwinkleInterval = 200;
            this.lightPump03Power.VarName = null;
            // 
            // lightPump02Power
            // 
            this.lightPump02Power.AlarmLightColors = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192))))),
        System.Drawing.Color.Aqua};
            this.lightPump02Power.IsOn = false;
            this.lightPump02Power.Location = new System.Drawing.Point(476, 385);
            this.lightPump02Power.Name = "lightPump02Power";
            this.lightPump02Power.Size = new System.Drawing.Size(32, 37);
            this.lightPump02Power.TabIndex = 19;
            this.lightPump02Power.TwinkleInterval = 200;
            this.lightPump02Power.VarName = null;
            // 
            // lightPump01Power
            // 
            this.lightPump01Power.AlarmLightColors = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192))))),
        System.Drawing.Color.Aqua};
            this.lightPump01Power.IsOn = false;
            this.lightPump01Power.Location = new System.Drawing.Point(171, 386);
            this.lightPump01Power.Name = "lightPump01Power";
            this.lightPump01Power.Size = new System.Drawing.Size(32, 37);
            this.lightPump01Power.StandColor = System.Drawing.SystemColors.GrayText;
            this.lightPump01Power.TabIndex = 19;
            this.lightPump01Power.TwinkleInterval = 200;
            this.lightPump01Power.VarName = null;
            // 
            // lightPump03Fre
            // 
            this.lightPump03Fre.AlarmLightColors = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))))};
            this.lightPump03Fre.IsOn = false;
            this.lightPump03Fre.Location = new System.Drawing.Point(748, 343);
            this.lightPump03Fre.Name = "lightPump03Fre";
            this.lightPump03Fre.Size = new System.Drawing.Size(32, 37);
            this.lightPump03Fre.TabIndex = 19;
            this.lightPump03Fre.TwinkleInterval = 200;
            this.lightPump03Fre.VarName = null;
            // 
            // lightPump02Fre
            // 
            this.lightPump02Fre.AlarmLightColors = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))))};
            this.lightPump02Fre.IsOn = false;
            this.lightPump02Fre.Location = new System.Drawing.Point(477, 342);
            this.lightPump02Fre.Name = "lightPump02Fre";
            this.lightPump02Fre.Size = new System.Drawing.Size(32, 37);
            this.lightPump02Fre.TabIndex = 19;
            this.lightPump02Fre.TwinkleInterval = 200;
            this.lightPump02Fre.VarName = null;
            // 
            // lightPump01Fre
            // 
            this.lightPump01Fre.AlarmLightColors = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))))};
            this.lightPump01Fre.IsOn = true;
            this.lightPump01Fre.Location = new System.Drawing.Point(171, 340);
            this.lightPump01Fre.Name = "lightPump01Fre";
            this.lightPump01Fre.Size = new System.Drawing.Size(32, 37);
            this.lightPump01Fre.TabIndex = 19;
            this.lightPump01Fre.TwinkleInterval = 200;
            this.lightPump01Fre.VarName = null;
            // 
            // txtPump01Power
            // 
            this.txtPump01Power.DataValue = "110";
            this.txtPump01Power.Location = new System.Drawing.Point(112, 400);
            this.txtPump01Power.Name = "txtPump01Power";
            this.txtPump01Power.Size = new System.Drawing.Size(70, 20);
            this.txtPump01Power.TabIndex = 18;
            this.txtPump01Power.Unit = "KW";
            this.txtPump01Power.VarName = "Pump01Power";
            // 
            // txtPump01Fre
            // 
            this.txtPump01Fre.DataValue = "150";
            this.txtPump01Fre.Location = new System.Drawing.Point(102, 357);
            this.txtPump01Fre.Name = "txtPump01Fre";
            this.txtPump01Fre.Size = new System.Drawing.Size(70, 20);
            this.txtPump01Fre.TabIndex = 18;
            this.txtPump01Fre.Unit = "HZ";
            this.txtPump01Fre.VarName = "Pump01Fre";
            // 
            // uPump2
            // 
            this.uPump2.ActualState = false;
            this.uPump2.BtnBgColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.uPump2.BtnText = "OFF";
            this.uPump2.LightImg = ((System.Drawing.Bitmap)(resources.GetObject("uPump2.LightImg")));
            this.uPump2.Location = new System.Drawing.Point(363, 170);
            this.uPump2.Name = "uPump2";
            this.uPump2.PumpStateName = "Pump02State";
            this.uPump2.Size = new System.Drawing.Size(119, 151);
            this.uPump2.TabIndex = 17;
            // 
            // uPump3
            // 
            this.uPump3.ActualState = false;
            this.uPump3.BtnBgColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.uPump3.BtnText = "OFF";
            this.uPump3.LightImg = ((System.Drawing.Bitmap)(resources.GetObject("uPump3.LightImg")));
            this.uPump3.Location = new System.Drawing.Point(618, 170);
            this.uPump3.Name = "uPump3";
            this.uPump3.PumpStateName = "Pump03State";
            this.uPump3.Size = new System.Drawing.Size(119, 151);
            this.uPump3.TabIndex = 17;
            // 
            // uPump1
            // 
            this.uPump1.ActualState = false;
            this.uPump1.BtnBgColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.uPump1.BtnText = "OFF";
            this.uPump1.LightImg = ((System.Drawing.Bitmap)(resources.GetObject("uPump1.LightImg")));
            this.uPump1.Location = new System.Drawing.Point(76, 170);
            this.uPump1.Name = "uPump1";
            this.uPump1.PumpStateName = "Pump01State";
            this.uPump1.Size = new System.Drawing.Size(119, 151);
            this.uPump1.TabIndex = 17;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 545);
            this.Controls.Add(this.txtPump03Fre);
            this.Controls.Add(this.txtPump03Power);
            this.Controls.Add(this.txtPump02Fre);
            this.Controls.Add(this.txtPump02Power);
            this.Controls.Add(this.lightPump03Power);
            this.Controls.Add(this.lightPump02Power);
            this.Controls.Add(this.lightPump01Power);
            this.Controls.Add(this.lightPump03Fre);
            this.Controls.Add(this.lightPump02Fre);
            this.Controls.Add(this.lightPump01Fre);
            this.Controls.Add(this.txtPump01Power);
            this.Controls.Add(this.txtPump01Fre);
            this.Controls.Add(this.uPump2);
            this.Controls.Add(this.uPump3);
            this.Controls.Add(this.uPump1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblState);
            this.Controls.Add(this.btnAlarmList);
            this.Controls.Add(this.btnStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmMain";
            this.Text = "预警管理系统";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Button btnAlarmList;
        private System.Windows.Forms.Button btnStart;
        private UControls.UPump uPump1;
        private UControls.ParaTextBox txtPump01Fre;
        private UControls.UCAlarmControl lightPump01Fre;
        private UControls.ParaTextBox txtPump01Power;
        private UControls.ParaTextBox txtPump02Power;
        private UControls.ParaTextBox txtPump02Fre;
        private UControls.UPump uPump3;
        private UControls.UPump uPump2;
        private UControls.UCAlarmControl lightPump01Power;
        private UControls.UCAlarmControl lightPump02Fre;
        private UControls.UCAlarmControl lightPump02Power;
        private UControls.UCAlarmControl lightPump03Fre;
        private UControls.UCAlarmControl lightPump03Power;
        private UControls.ParaTextBox txtPump03Power;
        private UControls.ParaTextBox txtPump03Fre;
    }
}

