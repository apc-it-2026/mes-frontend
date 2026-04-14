namespace SJeMES_Framework.WinForm.CommonForm
{
    partial class frmSearchData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSearchData));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Advanced = new System.Windows.Forms.Button();
            this.cbWhereKey = new System.Windows.Forms.ComboBox();
            this.txtWhereKey = new System.Windows.Forms.TextBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnOk = new System.Windows.Forms.Button();
            this.labPageNum = new System.Windows.Forms.Label();
            this.labDataCount = new System.Windows.Forms.Label();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btn_BodyQuery = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_BodyQuery);
            this.panel1.Controls.Add(this.btn_Advanced);
            this.panel1.Controls.Add(this.cbWhereKey);
            this.panel1.Controls.Add(this.txtWhereKey);
            this.panel1.Controls.Add(this.btnSelect);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(597, 75);
            this.panel1.TabIndex = 0;
            // 
            // btn_Advanced
            // 
            this.btn_Advanced.Location = new System.Drawing.Point(396, 17);
            this.btn_Advanced.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Advanced.Name = "btn_Advanced";
            this.btn_Advanced.Size = new System.Drawing.Size(89, 29);
            this.btn_Advanced.TabIndex = 4;
            this.btn_Advanced.Text = "高级查询";
            this.btn_Advanced.UseVisualStyleBackColor = true;
            this.btn_Advanced.Click += new System.EventHandler(this.btn_Advanced_Click);
            // 
            // cbWhereKey
            // 
            this.cbWhereKey.FormattingEnabled = true;
            this.cbWhereKey.Location = new System.Drawing.Point(21, 23);
            this.cbWhereKey.Margin = new System.Windows.Forms.Padding(2);
            this.cbWhereKey.Name = "cbWhereKey";
            this.cbWhereKey.Size = new System.Drawing.Size(99, 20);
            this.cbWhereKey.TabIndex = 2;
            // 
            // txtWhereKey
            // 
            this.txtWhereKey.Location = new System.Drawing.Point(126, 22);
            this.txtWhereKey.Margin = new System.Windows.Forms.Padding(2);
            this.txtWhereKey.Name = "txtWhereKey";
            this.txtWhereKey.Size = new System.Drawing.Size(143, 21);
            this.txtWhereKey.TabIndex = 3;
            this.txtWhereKey.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtWhereKey_KeyPress);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(295, 17);
            this.btnSelect.Margin = new System.Windows.Forms.Padding(2);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(89, 29);
            this.btnSelect.TabIndex = 1;
            this.btnSelect.Text = "查询";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnOk);
            this.panel2.Controls.Add(this.labPageNum);
            this.panel2.Controls.Add(this.labDataCount);
            this.panel2.Controls.Add(this.btnLast);
            this.panel2.Controls.Add(this.btnNext);
            this.panel2.Controls.Add(this.btnBack);
            this.panel2.Controls.Add(this.btnFirst);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 418);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(597, 67);
            this.panel2.TabIndex = 0;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(477, 16);
            this.btnOk.Margin = new System.Windows.Forms.Padding(2);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(98, 29);
            this.btnOk.TabIndex = 15;
            this.btnOk.Text = "确认";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // labPageNum
            // 
            this.labPageNum.AutoSize = true;
            this.labPageNum.Location = new System.Drawing.Point(9, 35);
            this.labPageNum.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labPageNum.Name = "labPageNum";
            this.labPageNum.Size = new System.Drawing.Size(0, 12);
            this.labPageNum.TabIndex = 14;
            // 
            // labDataCount
            // 
            this.labDataCount.AutoSize = true;
            this.labDataCount.Location = new System.Drawing.Point(9, 16);
            this.labDataCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labDataCount.Name = "labDataCount";
            this.labDataCount.Size = new System.Drawing.Size(0, 12);
            this.labDataCount.TabIndex = 13;
            // 
            // btnLast
            // 
            this.btnLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLast.Location = new System.Drawing.Point(369, 16);
            this.btnLast.Margin = new System.Windows.Forms.Padding(2);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(61, 29);
            this.btnLast.TabIndex = 12;
            this.btnLast.Text = "尾页";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.Location = new System.Drawing.Point(294, 16);
            this.btnNext.Margin = new System.Windows.Forms.Padding(2);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(61, 29);
            this.btnNext.TabIndex = 11;
            this.btnNext.Text = "下一页";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnBack
            // 
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBack.Location = new System.Drawing.Point(217, 16);
            this.btnBack.Margin = new System.Windows.Forms.Padding(2);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(61, 29);
            this.btnBack.TabIndex = 10;
            this.btnBack.Text = "上一页";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFirst.Location = new System.Drawing.Point(140, 16);
            this.btnFirst.Margin = new System.Windows.Forms.Padding(2);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(61, 29);
            this.btnFirst.TabIndex = 9;
            this.btnFirst.Text = "首页";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 75);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(597, 343);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            this.dataGridView1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDoubleClick);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(5, 79);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // btn_BodyQuery
            // 
            this.btn_BodyQuery.Location = new System.Drawing.Point(495, 17);
            this.btn_BodyQuery.Margin = new System.Windows.Forms.Padding(2);
            this.btn_BodyQuery.Name = "btn_BodyQuery";
            this.btn_BodyQuery.Size = new System.Drawing.Size(89, 29);
            this.btn_BodyQuery.TabIndex = 5;
            this.btn_BodyQuery.Text = "表身查询";
            this.btn_BodyQuery.UseVisualStyleBackColor = true;
            this.btn_BodyQuery.Click += new System.EventHandler(this.btn_BodyQuery_Click);
            // 
            // frmSearchData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 485);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmSearchData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "选择数据";
            this.Load += new System.EventHandler(this.frmSearchData_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.DataGridView dataGridView1;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.Label labPageNum;
    private System.Windows.Forms.Label labDataCount;
    private System.Windows.Forms.Button btnLast;
    private System.Windows.Forms.Button btnNext;
    private System.Windows.Forms.Button btnBack;
    private System.Windows.Forms.Button btnFirst;
    private System.Windows.Forms.Button btnSelect;
    private System.Windows.Forms.ComboBox cbWhereKey;
    private System.Windows.Forms.TextBox txtWhereKey;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button btn_Advanced;
        private System.Windows.Forms.Button btn_BodyQuery;
    }
}