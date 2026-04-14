namespace AssemblyOutput_Domestic
{
    partial class TrackOut_AssemblyOrder_InOutDetail
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.text_dept = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textSeId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textPo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.ColSeId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColPo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColSizeSeq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDept = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColOrderQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColPlanQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColInQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColOutQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Location = new System.Drawing.Point(0, 66);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(764, 501);
            this.panel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(762, 499);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.text_dept);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.textSeId);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.textPo);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(756, 48);
            this.panel2.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.MediumAquamarine;
            this.button1.Font = new System.Drawing.Font("SimSun", 12F);
            this.button1.Location = new System.Drawing.Point(631, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 31);
            this.button1.TabIndex = 3;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // text_dept
            // 
            this.text_dept.BackColor = System.Drawing.Color.White;
            this.text_dept.Font = new System.Drawing.Font("SimSun", 11F);
            this.text_dept.Location = new System.Drawing.Point(495, 11);
            this.text_dept.Name = "text_dept";
            this.text_dept.Size = new System.Drawing.Size(88, 24);
            this.text_dept.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("SimSun", 11F);
            this.label3.Location = new System.Drawing.Point(389, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "组别:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textSeId
            // 
            this.textSeId.Font = new System.Drawing.Font("SimSun", 11F);
            this.textSeId.Location = new System.Drawing.Point(277, 11);
            this.textSeId.Name = "textSeId";
            this.textSeId.Size = new System.Drawing.Size(91, 24);
            this.textSeId.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("SimSun", 11F);
            this.label2.Location = new System.Drawing.Point(151, 15);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(124, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "订单号:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textPo
            // 
            this.textPo.Font = new System.Drawing.Font("SimSun", 11F);
            this.textPo.Location = new System.Drawing.Point(45, 11);
            this.textPo.Name = "textPo";
            this.textPo.Size = new System.Drawing.Size(89, 24);
            this.textPo.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SimSun", 11F);
            this.label1.Location = new System.Drawing.Point(10, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "PO:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColSeId,
            this.ColPo,
            this.ColSize,
            this.ColSizeSeq,
            this.ColDept,
            this.ColOrderQty,
            this.ColPlanQty,
            this.ColInQty,
            this.ColOutQty});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 57);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(756, 385);
            this.dataGridView1.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.panel3.Controls.Add(this.button2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 445);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(762, 54);
            this.panel3.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.Font = new System.Drawing.Font("SimSun", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(326, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 41);
            this.button2.TabIndex = 0;
            this.button2.Text = "确定";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ColSeId
            // 
            this.ColSeId.DataPropertyName = "SE_ID";
            this.ColSeId.HeaderText = "订单号";
            this.ColSeId.Name = "ColSeId";
            this.ColSeId.ReadOnly = true;
            // 
            // ColPo
            // 
            this.ColPo.DataPropertyName = "PO";
            this.ColPo.HeaderText = "PO";
            this.ColPo.Name = "ColPo";
            this.ColPo.ReadOnly = true;
            // 
            // ColSize
            // 
            this.ColSize.DataPropertyName = "SIZE_NO";
            this.ColSize.HeaderText = "SIZE";
            this.ColSize.Name = "ColSize";
            this.ColSize.ReadOnly = true;
            this.ColSize.Width = 80;
            // 
            // ColSizeSeq
            // 
            this.ColSizeSeq.DataPropertyName = "SIZE_SEQ";
            this.ColSizeSeq.HeaderText = "SIZE_SEQ";
            this.ColSizeSeq.Name = "ColSizeSeq";
            this.ColSizeSeq.ReadOnly = true;
            this.ColSizeSeq.Visible = false;
            this.ColSizeSeq.Width = 20;
            // 
            // ColDept
            // 
            this.ColDept.DataPropertyName = "D_DEPT";
            this.ColDept.HeaderText = "组别";
            this.ColDept.Name = "ColDept";
            this.ColDept.ReadOnly = true;
            // 
            // ColOrderQty
            // 
            this.ColOrderQty.DataPropertyName = "ORDER_QTY";
            this.ColOrderQty.HeaderText = "訂購數量";
            this.ColOrderQty.Name = "ColOrderQty";
            this.ColOrderQty.ReadOnly = true;
            this.ColOrderQty.Width = 80;
            // 
            // ColPlanQty
            // 
            this.ColPlanQty.DataPropertyName = "IN_QTY";
            this.ColPlanQty.HeaderText = "生管排产数量";
            this.ColPlanQty.Name = "ColPlanQty";
            this.ColPlanQty.ReadOnly = true;
            // 
            // ColInQty
            // 
            this.ColInQty.DataPropertyName = "PLAN_QTY";
            this.ColInQty.HeaderText = "加工投入数量";
            this.ColInQty.Name = "ColInQty";
            this.ColInQty.ReadOnly = true;
            // 
            // ColOutQty
            // 
            this.ColOutQty.DataPropertyName = "OUT_QTY";
            this.ColOutQty.HeaderText = "包装扫描数量";
            this.ColOutQty.Name = "ColOutQty";
            this.ColOutQty.ReadOnly = true;
            // 
            // TrackOut_AssemblyOrder_InOutDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 568);
            this.Controls.Add(this.panel1);
            this.MinimizeBox = false;
            this.Name = "TrackOut_AssemblyOrder_InOutDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "投入扫描数量对比";
            this.Load += new System.EventHandler(this.InOutDetailForm_Load);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox text_dept;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textSeId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textPo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColSeId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColSizeSeq;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDept;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColOrderQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPlanQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColInQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColOutQty;
    }
}