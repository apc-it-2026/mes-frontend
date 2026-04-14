namespace SJeMES_Framework.Printer
{
    partial class frmBarCodePrinter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBarCodePrinter));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.cbo_Print = new System.Windows.Forms.ComboBox();
            this.btn_NotAll = new System.Windows.Forms.Button();
            this.btn_RevSel = new System.Windows.Forms.Button();
            this.btn_SelALL = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.check_OpenModel = new System.Windows.Forms.CheckBox();
            this.txt_Qty = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_BarCodeE = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_BarCodeS = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.list_Model = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.list_Model.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.cbo_Print);
            this.panel1.Controls.Add(this.btn_NotAll);
            this.panel1.Controls.Add(this.btn_RevSel);
            this.panel1.Controls.Add(this.btn_SelALL);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.check_OpenModel);
            this.panel1.Controls.Add(this.txt_Qty);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txt_BarCodeE);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txt_BarCodeS);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(866, 665);
            this.panel1.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(463, 639);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 17);
            this.label7.TabIndex = 22;
            this.label7.Text = "当前第1页/共1页";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(375, 636);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 21;
            this.button4.Text = "尾 页";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(294, 636);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 20;
            this.button3.Text = "下一页";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(213, 637);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 19;
            this.button2.Text = "上一页";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(132, 637);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 18;
            this.button1.Text = "首 页";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "10",
            "30",
            "50",
            "100",
            "200",
            "500",
            "1000"});
            this.comboBox1.Location = new System.Drawing.Point(5, 638);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 17;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // cbo_Print
            // 
            this.cbo_Print.FormattingEnabled = true;
            this.cbo_Print.Location = new System.Drawing.Point(324, 51);
            this.cbo_Print.Name = "cbo_Print";
            this.cbo_Print.Size = new System.Drawing.Size(182, 20);
            this.cbo_Print.TabIndex = 16;
            // 
            // btn_NotAll
            // 
            this.btn_NotAll.Location = new System.Drawing.Point(180, 80);
            this.btn_NotAll.Name = "btn_NotAll";
            this.btn_NotAll.Size = new System.Drawing.Size(51, 23);
            this.btn_NotAll.TabIndex = 15;
            this.btn_NotAll.Text = "全不选";
            this.btn_NotAll.UseVisualStyleBackColor = true;
            this.btn_NotAll.Click += new System.EventHandler(this.btn_NotAll_Click);
            // 
            // btn_RevSel
            // 
            this.btn_RevSel.Location = new System.Drawing.Point(123, 80);
            this.btn_RevSel.Name = "btn_RevSel";
            this.btn_RevSel.Size = new System.Drawing.Size(51, 23);
            this.btn_RevSel.TabIndex = 14;
            this.btn_RevSel.Text = "反选";
            this.btn_RevSel.UseVisualStyleBackColor = true;
            this.btn_RevSel.Click += new System.EventHandler(this.btn_RevSel_Click);
            // 
            // btn_SelALL
            // 
            this.btn_SelALL.Location = new System.Drawing.Point(66, 79);
            this.btn_SelALL.Name = "btn_SelALL";
            this.btn_SelALL.Size = new System.Drawing.Size(51, 23);
            this.btn_SelALL.TabIndex = 13;
            this.btn_SelALL.Text = "全选";
            this.btn_SelALL.UseVisualStyleBackColor = true;
            this.btn_SelALL.Click += new System.EventHandler(this.btn_SelALL_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "条码明细";
            // 
            // check_OpenModel
            // 
            this.check_OpenModel.AutoSize = true;
            this.check_OpenModel.Location = new System.Drawing.Point(512, 53);
            this.check_OpenModel.Name = "check_OpenModel";
            this.check_OpenModel.Size = new System.Drawing.Size(72, 16);
            this.check_OpenModel.TabIndex = 9;
            this.check_OpenModel.Text = "打开模板";
            this.check_OpenModel.UseVisualStyleBackColor = true;
            // 
            // txt_Qty
            // 
            this.txt_Qty.BackColor = System.Drawing.Color.White;
            this.txt_Qty.Location = new System.Drawing.Point(69, 51);
            this.txt_Qty.Name = "txt_Qty";
            this.txt_Qty.Size = new System.Drawing.Size(182, 21);
            this.txt_Qty.TabIndex = 7;
            this.txt_Qty.Text = "1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "打印份数";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(276, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "打印机";
            // 
            // txt_BarCodeE
            // 
            this.txt_BarCodeE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txt_BarCodeE.Location = new System.Drawing.Point(324, 18);
            this.txt_BarCodeE.Name = "txt_BarCodeE";
            this.txt_BarCodeE.ReadOnly = true;
            this.txt_BarCodeE.Size = new System.Drawing.Size(182, 21);
            this.txt_BarCodeE.TabIndex = 3;
            this.txt_BarCodeE.TextChanged += new System.EventHandler(this.txt_BarCodeE_TextChanged);
            this.txt_BarCodeE.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txt_BarCodeE_MouseDoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(265, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "结束条码";
            // 
            // txt_BarCodeS
            // 
            this.txt_BarCodeS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txt_BarCodeS.Location = new System.Drawing.Point(69, 18);
            this.txt_BarCodeS.Name = "txt_BarCodeS";
            this.txt_BarCodeS.ReadOnly = true;
            this.txt_BarCodeS.Size = new System.Drawing.Size(182, 21);
            this.txt_BarCodeS.TabIndex = 1;
            this.txt_BarCodeS.TextChanged += new System.EventHandler(this.txt_BarCodeS_TextChanged);
            this.txt_BarCodeS.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txt_BarCodeS_MouseDoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "起始条码";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.dataGridView1.Location = new System.Drawing.Point(4, 113);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(861, 517);
            this.dataGridView1.TabIndex = 23;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = " ";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 17;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 22);
            this.label1.TabIndex = 5;
            this.label1.Text = "打印";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // list_Model
            // 
            this.list_Model.AutoScroll = true;
            this.list_Model.BackColor = System.Drawing.Color.White;
            this.list_Model.Controls.Add(this.label1);
            this.list_Model.Dock = System.Windows.Forms.DockStyle.Fill;
            this.list_Model.Location = new System.Drawing.Point(0, 0);
            this.list_Model.Name = "list_Model";
            this.list_Model.Size = new System.Drawing.Size(176, 665);
            this.list_Model.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Location = new System.Drawing.Point(1, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(866, 665);
            this.panel2.TabIndex = 9;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.list_Model);
            this.panel3.Location = new System.Drawing.Point(873, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(176, 665);
            this.panel3.TabIndex = 10;
            // 
            // frmBarCodePrinter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 665);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBarCodePrinter";
            this.Text = "条码打印";
            this.Load += new System.EventHandler(this.frmBarCodePrinter_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.list_Model.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txt_Qty;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_BarCodeE;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_BarCodeS;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox check_OpenModel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_NotAll;
        private System.Windows.Forms.Button btn_RevSel;
        private System.Windows.Forms.Button btn_SelALL;
        private System.Windows.Forms.ComboBox cbo_Print;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel list_Model;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
    }
}