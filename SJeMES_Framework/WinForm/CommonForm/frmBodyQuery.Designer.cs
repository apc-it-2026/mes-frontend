namespace SJeMES_Framework.WinForm.CommonForm
{
    partial class frmBodyQuery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBodyQuery));
            this.cbo_PageName = new System.Windows.Forms.ComboBox();
            this.cbo_FieldName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_Where = new System.Windows.Forms.TextBox();
            this.btn_Clear = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdb_OR = new System.Windows.Forms.RadioButton();
            this.rdb_AND = new System.Windows.Forms.RadioButton();
            this.txt_Content = new System.Windows.Forms.TextBox();
            this.cbo_Where = new System.Windows.Forms.ComboBox();
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbo_PageName
            // 
            this.cbo_PageName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_PageName.FormattingEnabled = true;
            this.cbo_PageName.Location = new System.Drawing.Point(72, 13);
            this.cbo_PageName.Margin = new System.Windows.Forms.Padding(4);
            this.cbo_PageName.Name = "cbo_PageName";
            this.cbo_PageName.Size = new System.Drawing.Size(201, 24);
            this.cbo_PageName.TabIndex = 0;
            this.cbo_PageName.SelectedIndexChanged += new System.EventHandler(this.cbo_PageName_SelectedIndexChanged);
            // 
            // cbo_FieldName
            // 
            this.cbo_FieldName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_FieldName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbo_FieldName.FormattingEnabled = true;
            this.cbo_FieldName.Location = new System.Drawing.Point(13, 26);
            this.cbo_FieldName.Margin = new System.Windows.Forms.Padding(4);
            this.cbo_FieldName.Name = "cbo_FieldName";
            this.cbo_FieldName.Size = new System.Drawing.Size(172, 20);
            this.cbo_FieldName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "页签";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_Where);
            this.groupBox1.Controls.Add(this.btn_Clear);
            this.groupBox1.Controls.Add(this.btn_Add);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.txt_Content);
            this.groupBox1.Controls.Add(this.cbo_Where);
            this.groupBox1.Controls.Add(this.cbo_FieldName);
            this.groupBox1.Location = new System.Drawing.Point(4, 44);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(416, 247);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "条件";
            // 
            // txt_Where
            // 
            this.txt_Where.Location = new System.Drawing.Point(13, 102);
            this.txt_Where.Multiline = true;
            this.txt_Where.Name = "txt_Where";
            this.txt_Where.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_Where.Size = new System.Drawing.Size(397, 139);
            this.txt_Where.TabIndex = 7;
            // 
            // btn_Clear
            // 
            this.btn_Clear.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Clear.Location = new System.Drawing.Point(233, 64);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(75, 29);
            this.btn_Clear.TabIndex = 6;
            this.btn_Clear.Text = "清除";
            this.btn_Clear.UseVisualStyleBackColor = true;
            this.btn_Clear.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Add.Location = new System.Drawing.Point(331, 64);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(75, 29);
            this.btn_Add.TabIndex = 5;
            this.btn_Add.Text = "增加";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdb_OR);
            this.groupBox2.Controls.Add(this.rdb_AND);
            this.groupBox2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(13, 54);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(156, 41);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "条件关系";
            // 
            // rdb_OR
            // 
            this.rdb_OR.AutoSize = true;
            this.rdb_OR.Location = new System.Drawing.Point(86, 19);
            this.rdb_OR.Name = "rdb_OR";
            this.rdb_OR.Size = new System.Drawing.Size(35, 16);
            this.rdb_OR.TabIndex = 1;
            this.rdb_OR.TabStop = true;
            this.rdb_OR.Text = "OR";
            this.rdb_OR.UseVisualStyleBackColor = true;
            // 
            // rdb_AND
            // 
            this.rdb_AND.AutoSize = true;
            this.rdb_AND.Location = new System.Drawing.Point(20, 19);
            this.rdb_AND.Name = "rdb_AND";
            this.rdb_AND.Size = new System.Drawing.Size(41, 16);
            this.rdb_AND.TabIndex = 0;
            this.rdb_AND.TabStop = true;
            this.rdb_AND.Text = "AND";
            this.rdb_AND.UseVisualStyleBackColor = true;
            // 
            // txt_Content
            // 
            this.txt_Content.Location = new System.Drawing.Point(288, 23);
            this.txt_Content.Name = "txt_Content";
            this.txt_Content.Size = new System.Drawing.Size(122, 26);
            this.txt_Content.TabIndex = 3;
            // 
            // cbo_Where
            // 
            this.cbo_Where.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_Where.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbo_Where.FormattingEnabled = true;
            this.cbo_Where.Items.AddRange(new object[] {
            "等于",
            "模糊包含",
            "不等于",
            "大于",
            "小于",
            "大于等于",
            "小于等于",
            "开始于",
            "结束于",
            "不开始于",
            "不结束于"});
            this.cbo_Where.Location = new System.Drawing.Point(193, 26);
            this.cbo_Where.Margin = new System.Windows.Forms.Padding(4);
            this.cbo_Where.Name = "cbo_Where";
            this.cbo_Where.Size = new System.Drawing.Size(87, 20);
            this.cbo_Where.TabIndex = 2;
            // 
            // btn_OK
            // 
            this.btn_OK.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_OK.Location = new System.Drawing.Point(251, 292);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(75, 29);
            this.btn_OK.TabIndex = 7;
            this.btn_OK.Text = "确定";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Close.Location = new System.Drawing.Point(332, 292);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 29);
            this.btn_Close.TabIndex = 8;
            this.btn_Close.Text = "关闭";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // frmBodyQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 325);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbo_PageName);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBodyQuery";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "表身查询";
            this.Load += new System.EventHandler(this.frmBodyQuery_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbo_PageName;
        private System.Windows.Forms.ComboBox cbo_FieldName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_Content;
        private System.Windows.Forms.ComboBox cbo_Where;
        private System.Windows.Forms.Button btn_Clear;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdb_OR;
        private System.Windows.Forms.RadioButton rdb_AND;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.TextBox txt_Where;
    }
}