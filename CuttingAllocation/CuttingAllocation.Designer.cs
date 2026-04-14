
namespace CuttingAllocation
{
    partial class CuttingAllocation
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.labelMainOrderNo = new System.Windows.Forms.Label();
            this.txtMainOrderNo = new System.Windows.Forms.TextBox();
            this.labelFactory = new System.Windows.Forms.Label();
            this.labelWorkCenter = new System.Windows.Forms.Label();
            this.txtWorkCenter = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.序号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.主订单号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.工厂 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.裁剪单位 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.针车单位 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.主工单数量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cutting_dept = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sticking_dept = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comboBoxFactory = new System.Windows.Forms.ComboBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.autocompleteMenu2 = new AutocompleteMenuNS.AutocompleteMenu();
            this.autocompleteMenu1 = new AutocompleteMenuNS.AutocompleteMenu();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnQuery
            // 
            this.btnQuery.BackColor = System.Drawing.Color.Gray;
            this.btnQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuery.Font = new System.Drawing.Font("Candara", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuery.ForeColor = System.Drawing.Color.White;
            this.btnQuery.Location = new System.Drawing.Point(45, 12);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 33);
            this.btnQuery.TabIndex = 0;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = false;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Green;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Candara", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(268, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(79, 33);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("Candara", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.ForeColor = System.Drawing.Color.White;
            this.btnReset.Location = new System.Drawing.Point(492, 12);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(84, 33);
            this.btnReset.TabIndex = 3;
            this.btnReset.Text = "重置";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Gainsboro;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Candara", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(609, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(83, 33);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // labelMainOrderNo
            // 
            this.labelMainOrderNo.AutoSize = true;
            this.labelMainOrderNo.BackColor = System.Drawing.Color.Transparent;
            this.labelMainOrderNo.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMainOrderNo.ForeColor = System.Drawing.Color.Navy;
            this.labelMainOrderNo.Location = new System.Drawing.Point(43, 67);
            this.labelMainOrderNo.Name = "labelMainOrderNo";
            this.labelMainOrderNo.Size = new System.Drawing.Size(83, 18);
            this.labelMainOrderNo.TabIndex = 5;
            this.labelMainOrderNo.Text = "主工单编号";
            // 
            // txtMainOrderNo
            // 
            this.autocompleteMenu1.SetAutocompleteMenu(this.txtMainOrderNo, null);
            this.txtMainOrderNo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMainOrderNo.Location = new System.Drawing.Point(151, 64);
            this.txtMainOrderNo.Name = "txtMainOrderNo";
            this.txtMainOrderNo.Size = new System.Drawing.Size(141, 23);
            this.txtMainOrderNo.TabIndex = 6;
            this.txtMainOrderNo.TextChanged += new System.EventHandler(this.txtMainOrderNo_TextChanged);
            // 
            // labelFactory
            // 
            this.labelFactory.AutoSize = true;
            this.labelFactory.BackColor = System.Drawing.Color.Transparent;
            this.labelFactory.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFactory.ForeColor = System.Drawing.Color.Navy;
            this.labelFactory.Location = new System.Drawing.Point(311, 66);
            this.labelFactory.Name = "labelFactory";
            this.labelFactory.Size = new System.Drawing.Size(38, 18);
            this.labelFactory.TabIndex = 7;
            this.labelFactory.Text = "工厂";
            // 
            // labelWorkCenter
            // 
            this.labelWorkCenter.AutoSize = true;
            this.labelWorkCenter.BackColor = System.Drawing.Color.Transparent;
            this.labelWorkCenter.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWorkCenter.ForeColor = System.Drawing.Color.Navy;
            this.labelWorkCenter.Location = new System.Drawing.Point(523, 65);
            this.labelWorkCenter.Name = "labelWorkCenter";
            this.labelWorkCenter.Size = new System.Drawing.Size(68, 18);
            this.labelWorkCenter.TabIndex = 9;
            this.labelWorkCenter.Text = "工作中心";
            // 
            // txtWorkCenter
            // 
            this.autocompleteMenu1.SetAutocompleteMenu(this.txtWorkCenter, null);
            this.txtWorkCenter.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWorkCenter.Location = new System.Drawing.Point(609, 62);
            this.txtWorkCenter.Name = "txtWorkCenter";
            this.txtWorkCenter.Size = new System.Drawing.Size(146, 23);
            this.txtWorkCenter.TabIndex = 10;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.Azure;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.DarkSlateGray;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Candara", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView1.ColumnHeadersHeight = 30;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.序号,
            this.主订单号,
            this.工厂,
            this.裁剪单位,
            this.针车单位,
            this.主工单数量,
            this.Id,
            this.cutting_dept,
            this.sticking_dept});
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.Green;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(1138, 478);
            this.dataGridView1.TabIndex = 11;
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            // 
            // 序号
            // 
            this.序号.DataPropertyName = "NO";
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.Honeydew;
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.DarkBlue;
            this.序号.DefaultCellStyle = dataGridViewCellStyle8;
            this.序号.HeaderText = "序号";
            this.序号.Name = "序号";
            this.序号.ReadOnly = true;
            // 
            // 主订单号
            // 
            this.主订单号.DataPropertyName = "main_production_order";
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.Honeydew;
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.DarkBlue;
            this.主订单号.DefaultCellStyle = dataGridViewCellStyle9;
            this.主订单号.HeaderText = "主订单号";
            this.主订单号.Name = "主订单号";
            this.主订单号.ReadOnly = true;
            this.主订单号.Width = 200;
            // 
            // 工厂
            // 
            this.工厂.DataPropertyName = "org_name";
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.Honeydew;
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.DarkBlue;
            this.工厂.DefaultCellStyle = dataGridViewCellStyle10;
            this.工厂.HeaderText = "工厂";
            this.工厂.Name = "工厂";
            this.工厂.ReadOnly = true;
            // 
            // 裁剪单位
            // 
            this.裁剪单位.DataPropertyName = "cutting_dept_name";
            this.裁剪单位.HeaderText = "裁剪单位";
            this.裁剪单位.Name = "裁剪单位";
            this.裁剪单位.Width = 150;
            // 
            // 针车单位
            // 
            this.针车单位.DataPropertyName = "sticking_dept_name";
            this.针车单位.HeaderText = "针车单位";
            this.针车单位.Name = "针车单位";
            this.针车单位.Width = 150;
            // 
            // 主工单数量
            // 
            this.主工单数量.DataPropertyName = "qty";
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.Honeydew;
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.DarkBlue;
            this.主工单数量.DefaultCellStyle = dataGridViewCellStyle11;
            this.主工单数量.HeaderText = "主工单数量";
            this.主工单数量.Name = "主工单数量";
            this.主工单数量.ReadOnly = true;
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.Visible = false;
            // 
            // cutting_dept
            // 
            this.cutting_dept.DataPropertyName = "cutting_dept";
            this.cutting_dept.HeaderText = "cutting_dept";
            this.cutting_dept.Name = "cutting_dept";
            this.cutting_dept.Visible = false;
            // 
            // sticking_dept
            // 
            this.sticking_dept.DataPropertyName = "sticking_dept";
            this.sticking_dept.HeaderText = "sticking_dept";
            this.sticking_dept.Name = "sticking_dept";
            this.sticking_dept.Visible = false;
            // 
            // comboBoxFactory
            // 
            this.comboBoxFactory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFactory.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxFactory.FormattingEnabled = true;
            this.comboBoxFactory.Location = new System.Drawing.Point(359, 62);
            this.comboBoxFactory.Name = "comboBoxFactory";
            this.comboBoxFactory.Size = new System.Drawing.Size(156, 24);
            this.comboBoxFactory.TabIndex = 12;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Candara", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(380, 12);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 33);
            this.btnDelete.TabIndex = 13;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Candara", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(151, 12);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(78, 33);
            this.btnAdd.TabIndex = 14;
            this.btnAdd.Text = "新增";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.btnQuery);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnReset);
            this.panel1.Controls.Add(this.comboBoxFactory);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.labelMainOrderNo);
            this.panel1.Controls.Add(this.txtWorkCenter);
            this.panel1.Controls.Add(this.txtMainOrderNo);
            this.panel1.Controls.Add(this.labelWorkCenter);
            this.panel1.Controls.Add(this.labelFactory);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1138, 95);
            this.panel1.TabIndex = 15;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Azure;
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1138, 478);
            this.panel2.TabIndex = 16;
            // 
            // autocompleteMenu2
            // 
            this.autocompleteMenu2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.autocompleteMenu2.ImageList = null;
            this.autocompleteMenu2.Items = new string[0];
            this.autocompleteMenu2.MaximumSize = new System.Drawing.Size(300, 200);
            this.autocompleteMenu2.TargetControlWrapper = null;
            // 
            // autocompleteMenu1
            // 
            this.autocompleteMenu1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.autocompleteMenu1.ImageList = null;
            this.autocompleteMenu1.Items = new string[0];
            this.autocompleteMenu1.MaximumSize = new System.Drawing.Size(300, 200);
            this.autocompleteMenu1.TargetControlWrapper = null;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 66);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Size = new System.Drawing.Size(1138, 577);
            this.splitContainer1.SplitterDistance = 95;
            this.splitContainer1.TabIndex = 17;
            // 
            // CuttingAllocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1139, 646);
            this.Controls.Add(this.splitContainer1);
            this.Name = "CuttingAllocation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "裁剪生产调拨";
            this.Load += new System.EventHandler(this.CuttingAllocationQueryForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label labelMainOrderNo;
        private System.Windows.Forms.TextBox txtMainOrderNo;
        private System.Windows.Forms.Label labelFactory;
        private System.Windows.Forms.Label labelWorkCenter;
        private System.Windows.Forms.TextBox txtWorkCenter;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox comboBoxFactory;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private AutocompleteMenuNS.AutocompleteMenu autocompleteMenu2;
        private System.Windows.Forms.DataGridViewTextBoxColumn 序号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 主订单号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 工厂;
        private System.Windows.Forms.DataGridViewTextBoxColumn 裁剪单位;
        private System.Windows.Forms.DataGridViewTextBoxColumn 针车单位;
        private System.Windows.Forms.DataGridViewTextBoxColumn 主工单数量;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn cutting_dept;
        private System.Windows.Forms.DataGridViewTextBoxColumn sticking_dept;
        private AutocompleteMenuNS.AutocompleteMenu autocompleteMenu1;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}

