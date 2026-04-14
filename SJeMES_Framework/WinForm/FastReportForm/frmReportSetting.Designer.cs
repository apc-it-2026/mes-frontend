namespace SJeMES_Framework.WinForm.FastReportForm
{
    partial class frmReportSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReportSetting));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_Pre = new System.Windows.Forms.Button();
            this.txt_ReportCode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_Primarykey = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_ReportName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_TestSQL = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.btn_SelectReport = new System.Windows.Forms.Button();
            this.txt_ReportPath = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btn_Pre);
            this.groupBox1.Controls.Add(this.txt_ReportCode);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txt_Primarykey);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txt_ReportName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btn_TestSQL);
            this.groupBox1.Controls.Add(this.btn_Save);
            this.groupBox1.Controls.Add(this.btn_SelectReport);
            this.groupBox1.Controls.Add(this.txt_ReportPath);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(796, 98);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择报表文件";
            // 
            // btn_Pre
            // 
            this.btn_Pre.Location = new System.Drawing.Point(715, 19);
            this.btn_Pre.Name = "btn_Pre";
            this.btn_Pre.Size = new System.Drawing.Size(75, 23);
            this.btn_Pre.TabIndex = 12;
            this.btn_Pre.Text = "预览报表";
            this.btn_Pre.UseVisualStyleBackColor = true;
            this.btn_Pre.Click += new System.EventHandler(this.btn_Pre_Click);
            // 
            // txt_ReportCode
            // 
            this.txt_ReportCode.Location = new System.Drawing.Point(67, 56);
            this.txt_ReportCode.Name = "txt_ReportCode";
            this.txt_ReportCode.Size = new System.Drawing.Size(133, 21);
            this.txt_ReportCode.TabIndex = 11;
            this.txt_ReportCode.Text = "Report_";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "报表代号";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "true",
            "false"});
            this.comboBox1.Location = new System.Drawing.Point(654, 57);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(566, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "开始显示数据";
            // 
            // txt_Primarykey
            // 
            this.txt_Primarykey.Location = new System.Drawing.Point(437, 56);
            this.txt_Primarykey.Name = "txt_Primarykey";
            this.txt_Primarykey.Size = new System.Drawing.Size(123, 21);
            this.txt_Primarykey.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(390, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "关联键";
            // 
            // txt_ReportName
            // 
            this.txt_ReportName.Location = new System.Drawing.Point(265, 56);
            this.txt_ReportName.Name = "txt_ReportName";
            this.txt_ReportName.Size = new System.Drawing.Size(119, 21);
            this.txt_ReportName.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(206, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "报表名称";
            // 
            // btn_TestSQL
            // 
            this.btn_TestSQL.Location = new System.Drawing.Point(552, 19);
            this.btn_TestSQL.Name = "btn_TestSQL";
            this.btn_TestSQL.Size = new System.Drawing.Size(75, 23);
            this.btn_TestSQL.TabIndex = 3;
            this.btn_TestSQL.Text = "测试SQL";
            this.btn_TestSQL.UseVisualStyleBackColor = true;
            this.btn_TestSQL.Click += new System.EventHandler(this.btn_TestSQL_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(634, 19);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(75, 23);
            this.btn_Save.TabIndex = 2;
            this.btn_Save.Text = "保 存";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // btn_SelectReport
            // 
            this.btn_SelectReport.Location = new System.Drawing.Point(469, 20);
            this.btn_SelectReport.Name = "btn_SelectReport";
            this.btn_SelectReport.Size = new System.Drawing.Size(75, 23);
            this.btn_SelectReport.TabIndex = 1;
            this.btn_SelectReport.Text = "选择报表";
            this.btn_SelectReport.UseVisualStyleBackColor = true;
            this.btn_SelectReport.Click += new System.EventHandler(this.btn_SelectReport_Click);
            // 
            // txt_ReportPath
            // 
            this.txt_ReportPath.Location = new System.Drawing.Point(13, 21);
            this.txt_ReportPath.Name = "txt_ReportPath";
            this.txt_ReportPath.ReadOnly = true;
            this.txt_ReportPath.Size = new System.Drawing.Size(448, 21);
            this.txt_ReportPath.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Location = new System.Drawing.Point(3, 108);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(796, 443);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "设置数据源";
            // 
            // frmReportSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 556);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmReportSetting";
            this.Text = "设置报表";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_Pre;
        private System.Windows.Forms.TextBox txt_ReportCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_Primarykey;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_ReportName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_TestSQL;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Button btn_SelectReport;
        private System.Windows.Forms.TextBox txt_ReportPath;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}