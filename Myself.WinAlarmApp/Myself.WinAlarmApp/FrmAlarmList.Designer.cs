namespace Myself.WinAlarmApp
{
    partial class FrmAlarmList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvAlarmList = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAlarmTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAlarmState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAlarmNote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlarmList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAlarmList
            // 
            this.dgvAlarmList.AllowUserToAddRows = false;
            this.dgvAlarmList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAlarmList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAlarmList.BackgroundColor = System.Drawing.Color.White;
            this.dgvAlarmList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.DeepSkyBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAlarmList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAlarmList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAlarmList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colAlarmTime,
            this.colType,
            this.colAlarmState,
            this.colValue,
            this.colAlarmNote});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAlarmList.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAlarmList.EnableHeadersVisualStyles = false;
            this.dgvAlarmList.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvAlarmList.Location = new System.Drawing.Point(29, 12);
            this.dgvAlarmList.Name = "dgvAlarmList";
            this.dgvAlarmList.ReadOnly = true;
            this.dgvAlarmList.RowHeadersWidth = 30;
            this.dgvAlarmList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvAlarmList.RowTemplate.Height = 27;
            this.dgvAlarmList.Size = new System.Drawing.Size(1058, 693);
            this.dgvAlarmList.TabIndex = 0;
            // 
            // colId
            // 
            this.colId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colId.DataPropertyName = "Id";
            this.colId.FillWeight = 50F;
            this.colId.Frozen = true;
            this.colId.HeaderText = "编号";
            this.colId.MinimumWidth = 6;
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            this.colId.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colId.Width = 257;
            // 
            // colAlarmTime
            // 
            this.colAlarmTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colAlarmTime.FillWeight = 150F;
            this.colAlarmTime.Frozen = true;
            this.colAlarmTime.HeaderText = "报警时间";
            this.colAlarmTime.MinimumWidth = 6;
            this.colAlarmTime.Name = "colAlarmTime";
            this.colAlarmTime.ReadOnly = true;
            this.colAlarmTime.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colAlarmTime.Width = 125;
            // 
            // colType
            // 
            this.colType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colType.DataPropertyName = "AlarmType";
            this.colType.Frozen = true;
            this.colType.HeaderText = "报警类型";
            this.colType.MinimumWidth = 6;
            this.colType.Name = "colType";
            this.colType.ReadOnly = true;
            this.colType.Width = 125;
            // 
            // colAlarmState
            // 
            this.colAlarmState.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colAlarmState.DataPropertyName = "AlarmState";
            this.colAlarmState.Frozen = true;
            this.colAlarmState.HeaderText = "报警状态";
            this.colAlarmState.MinimumWidth = 6;
            this.colAlarmState.Name = "colAlarmState";
            this.colAlarmState.ReadOnly = true;
            this.colAlarmState.Width = 125;
            // 
            // colValue
            // 
            this.colValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colValue.DataPropertyName = "AlarmNote";
            this.colValue.FillWeight = 150F;
            this.colValue.Frozen = true;
            this.colValue.HeaderText = "当前值";
            this.colValue.MinimumWidth = 6;
            this.colValue.Name = "colValue";
            this.colValue.ReadOnly = true;
            this.colValue.Width = 125;
            // 
            // colAlarmNote
            // 
            this.colAlarmNote.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colAlarmNote.DataPropertyName = "AlarmNote";
            this.colAlarmNote.FillWeight = 150F;
            this.colAlarmNote.HeaderText = "报警描述";
            this.colAlarmNote.MinimumWidth = 6;
            this.colAlarmNote.Name = "colAlarmNote";
            this.colAlarmNote.ReadOnly = true;
            this.colAlarmNote.Width = 125;
            // 
            // FrmAlarmList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1109, 743);
            this.Controls.Add(this.dgvAlarmList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmAlarmList";
            this.Text = "预警记录列表";
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlarmList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAlarmList;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAlarmTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAlarmState;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAlarmNote;
    }
}