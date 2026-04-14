namespace F_Abnormal_Registration
{
    partial class ItemSelectForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textSelect = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column20 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PROCEDURE_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MATERIAL_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PART_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NAME_T = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Process_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Process_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SUPPLIERS_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SUPPLIES_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SIZE_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QUANTITY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RECEIVED_QUANTITY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.registed_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 23);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(780, 317);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSelect);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textSelect);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(774, 44);
            this.panel1.TabIndex = 0;
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(5, 11);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(65, 23);
            this.btnSelect.TabIndex = 5;
            this.btnSelect.TabStop = false;
            this.btnSelect.Text = "全选";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.Location = new System.Drawing.Point(515, 11);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(74, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.TabStop = false;
            this.btnOK.Text = "确认";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(280, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "筛选";
            // 
            // textSelect
            // 
            this.textSelect.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textSelect.Location = new System.Drawing.Point(320, 11);
            this.textSelect.Name = "textSelect";
            this.textSelect.Size = new System.Drawing.Size(178, 23);
            this.textSelect.TabIndex = 0;
            this.textSelect.TextChanged += new System.EventHandler(this.textSelect_TextChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.LightGray;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column20,
            this.PROCEDURE_NO,
            this.MATERIAL_NO,
            this.PART_NO,
            this.NAME_T,
            this.Process_No,
            this.Process_Name,
            this.SUPPLIERS_CODE,
            this.SUPPLIES_NAME,
            this.SIZE_NO,
            this.QUANTITY,
            this.RECEIVED_QUANTITY,
            this.registed_qty});
            this.dataGridView1.Location = new System.Drawing.Point(3, 53);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(774, 261);
            this.dataGridView1.TabIndex = 2;
            // 
            // Column20
            // 
            this.Column20.HeaderText = "选择";
            this.Column20.Name = "Column20";
            this.Column20.Width = 40;
            // 
            // PROCEDURE_NO
            // 
            this.PROCEDURE_NO.DataPropertyName = "PROCEDURE_NO";
            this.PROCEDURE_NO.HeaderText = "工序";
            this.PROCEDURE_NO.Name = "PROCEDURE_NO";
            // 
            // MATERIAL_NO
            // 
            this.MATERIAL_NO.DataPropertyName = "MATERIAL_NO";
            this.MATERIAL_NO.HeaderText = "物料编号";
            this.MATERIAL_NO.Name = "MATERIAL_NO";
            // 
            // PART_NO
            // 
            this.PART_NO.DataPropertyName = "PART_NO";
            this.PART_NO.HeaderText = "部件代号";
            this.PART_NO.Name = "PART_NO";
            this.PART_NO.ReadOnly = true;
            this.PART_NO.Width = 120;
            // 
            // NAME_T
            // 
            this.NAME_T.DataPropertyName = "NAME_T";
            this.NAME_T.HeaderText = "部件名称";
            this.NAME_T.Name = "NAME_T";
            this.NAME_T.ReadOnly = true;
            // 
            // Process_No
            // 
            this.Process_No.DataPropertyName = "Process_no";
            this.Process_No.HeaderText = "工艺代号";
            this.Process_No.Name = "Process_No";
            this.Process_No.ReadOnly = true;
            // 
            // Process_Name
            // 
            this.Process_Name.DataPropertyName = "Process_name";
            this.Process_Name.HeaderText = "工艺名称";
            this.Process_Name.Name = "Process_Name";
            this.Process_Name.ReadOnly = true;
            // 
            // SUPPLIERS_CODE
            // 
            this.SUPPLIERS_CODE.DataPropertyName = "SUPPLIERS_CODE";
            this.SUPPLIERS_CODE.HeaderText = "厂商代号";
            this.SUPPLIERS_CODE.Name = "SUPPLIERS_CODE";
            this.SUPPLIERS_CODE.ReadOnly = true;
            // 
            // SUPPLIES_NAME
            // 
            this.SUPPLIES_NAME.DataPropertyName = "SUPPLIES_NAME";
            this.SUPPLIES_NAME.HeaderText = "厂商名称";
            this.SUPPLIES_NAME.Name = "SUPPLIES_NAME";
            // 
            // SIZE_NO
            // 
            this.SIZE_NO.DataPropertyName = "SIZE_NO";
            this.SIZE_NO.HeaderText = "码数";
            this.SIZE_NO.Name = "SIZE_NO";
            this.SIZE_NO.ReadOnly = true;
            this.SIZE_NO.Width = 80;
            // 
            // QUANTITY
            // 
            this.QUANTITY.DataPropertyName = "QUANTITY";
            this.QUANTITY.HeaderText = "订单数量";
            this.QUANTITY.Name = "QUANTITY";
            this.QUANTITY.ReadOnly = true;
            this.QUANTITY.Width = 90;
            // 
            // RECEIVED_QUANTITY
            // 
            this.RECEIVED_QUANTITY.DataPropertyName = "RECEIVED_QUANTITY";
            this.RECEIVED_QUANTITY.HeaderText = "收货数量";
            this.RECEIVED_QUANTITY.Name = "RECEIVED_QUANTITY";
            this.RECEIVED_QUANTITY.ReadOnly = true;
            this.RECEIVED_QUANTITY.Width = 80;
            // 
            // registed_qty
            // 
            this.registed_qty.DataPropertyName = "registed_qty";
            this.registed_qty.HeaderText = "登记次数";
            this.registed_qty.Name = "registed_qty";
            this.registed_qty.ReadOnly = true;
            // 
            // ItemSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 341);
            this.Controls.Add(this.tableLayoutPanel1);
            this.KeyPreview = true;
            this.Name = "ItemSelectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ItemSelectForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ItemSelectForm_FormClosing);
            this.Load += new System.EventHandler(this.ItemSelectForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textSelect;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column20;
        private System.Windows.Forms.DataGridViewTextBoxColumn PROCEDURE_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn MATERIAL_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PART_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn NAME_T;
        private System.Windows.Forms.DataGridViewTextBoxColumn Process_No;
        private System.Windows.Forms.DataGridViewTextBoxColumn Process_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn SUPPLIERS_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn SUPPLIES_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn SIZE_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn QUANTITY;
        private System.Windows.Forms.DataGridViewTextBoxColumn RECEIVED_QUANTITY;
        private System.Windows.Forms.DataGridViewTextBoxColumn registed_qty;
    }
}