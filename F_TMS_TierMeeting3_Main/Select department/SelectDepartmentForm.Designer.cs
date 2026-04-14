namespace F_TMS_TierMeeting3_Main
{
    partial class SelectDepartmentForm
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
            this.tableLayoutPanel1 = new CusContorl.CusTableLayoutPanel();
            this.gridPlant = new System.Windows.Forms.DataGridView();
            this.gridSection = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblPlant = new CusContorl.CusLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblSection = new CusContorl.CusLabel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblLine = new CusContorl.CusLabel();
            this.gridLine = new System.Windows.Forms.DataGridView();
            this.btnAll = new MaterialSkin.Controls.MaterialRaisedButton();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPlant)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSection)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLine)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.Controls.Add(this.gridPlant, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.gridSection, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.gridLine, 2, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 107);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(453, 364);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // gridPlant
            // 
            this.gridPlant.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridPlant.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridPlant.Location = new System.Drawing.Point(3, 53);
            this.gridPlant.Name = "gridPlant";
            this.gridPlant.RowHeadersWidth = 51;
            this.gridPlant.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridPlant.Size = new System.Drawing.Size(144, 288);
            this.gridPlant.TabIndex = 0;
            this.gridPlant.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridPlant_CellClick);
            this.gridPlant.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridPlant_CellDoubleClick);
            // 
            // gridSection
            // 
            this.gridSection.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridSection.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSection.Location = new System.Drawing.Point(153, 53);
            this.gridSection.Name = "gridSection";
            this.gridSection.RowHeadersWidth = 51;
            this.gridSection.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridSection.Size = new System.Drawing.Size(145, 288);
            this.gridSection.TabIndex = 1;
            this.gridSection.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridSection_CellClick);
            this.gridSection.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridSection_CellDoubleClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblPlant);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(144, 44);
            this.panel1.TabIndex = 3;
            // 
            // lblPlant
            // 
            this.lblPlant.AutoSize = true;
            this.lblPlant.Location = new System.Drawing.Point(3, 13);
            this.lblPlant.Name = "lblPlant";
            this.lblPlant.Size = new System.Drawing.Size(47, 15);
            this.lblPlant.TabIndex = 0;
            this.lblPlant.Text = "Plant";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblSection);
            this.panel2.Location = new System.Drawing.Point(153, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(145, 44);
            this.panel2.TabIndex = 4;
            // 
            // lblSection
            // 
            this.lblSection.AutoSize = true;
            this.lblSection.Location = new System.Drawing.Point(4, 14);
            this.lblSection.Name = "lblSection";
            this.lblSection.Size = new System.Drawing.Size(63, 15);
            this.lblSection.TabIndex = 0;
            this.lblSection.Text = "Section";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblLine);
            this.panel3.Location = new System.Drawing.Point(304, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(146, 44);
            this.panel3.TabIndex = 5;
            // 
            // lblLine
            // 
            this.lblLine.AutoSize = true;
            this.lblLine.Location = new System.Drawing.Point(4, 13);
            this.lblLine.Name = "lblLine";
            this.lblLine.Size = new System.Drawing.Size(39, 15);
            this.lblLine.TabIndex = 0;
            this.lblLine.Text = "Line";
            // 
            // gridLine
            // 
            this.gridLine.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridLine.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridLine.Location = new System.Drawing.Point(304, 53);
            this.gridLine.Name = "gridLine";
            this.gridLine.RowHeadersWidth = 51;
            this.gridLine.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridLine.Size = new System.Drawing.Size(146, 288);
            this.gridLine.TabIndex = 2;
            this.gridLine.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridLine_CellDoubleClick);
            // 
            // btnAll
            // 
            this.btnAll.AutoSize = true;
            this.btnAll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAll.Depth = 0;
            this.btnAll.Icon = null;
            this.btnAll.Location = new System.Drawing.Point(206, 65);
            this.btnAll.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnAll.Name = "btnAll";
            this.btnAll.Primary = true;
            this.btnAll.Size = new System.Drawing.Size(52, 36);
            this.btnAll.TabIndex = 2;
            this.btnAll.Text = "All";
            this.btnAll.UseVisualStyleBackColor = true;
            this.btnAll.Visible = false;
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // SelectDepartmentForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(460, 475);
            this.Controls.Add(this.btnAll);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SelectDepartmentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select department";
            this.Load += new System.EventHandler(this.SelectLineForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridPlant)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSection)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLine)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CusContorl.CusTableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView gridPlant;
        private System.Windows.Forms.DataGridView gridSection;
        private System.Windows.Forms.DataGridView gridLine;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblPlant;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblSection;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblLine;
        private MaterialSkin.Controls.MaterialRaisedButton btnAll;
    }
}