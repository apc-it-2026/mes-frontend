namespace F_TMS_TierMeeting2_Main
{
    partial class DetailKaizenForm
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
            this.btnSearch = new MaterialSkin.Controls.MaterialRaisedButton();
            this.lblMonth = new System.Windows.Forms.Label();
            this.cbxMonth = new System.Windows.Forms.ComboBox();
            this.gridData = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblDepartmentText = new System.Windows.Forms.Label();
            this.lblReceivedText = new System.Windows.Forms.Label();
            this.lblAcceptedText = new System.Windows.Forms.Label();
            this.lblReceivedPersonText = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridData)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.AutoSize = true;
            this.btnSearch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSearch.Depth = 0;
            this.btnSearch.Icon = null;
            this.btnSearch.Location = new System.Drawing.Point(370, 28);
            this.btnSearch.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Primary = true;
            this.btnSearch.Size = new System.Drawing.Size(64, 36);
            this.btnSearch.TabIndex = 54;
            this.btnSearch.Text = "query";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lblMonth
            // 
            this.lblMonth.AutoSize = true;
            this.lblMonth.Location = new System.Drawing.Point(54, 31);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(37, 13);
            this.lblMonth.TabIndex = 47;
            this.lblMonth.Text = "Month";
            // 
            // cbxMonth
            // 
            this.cbxMonth.FormattingEnabled = true;
            this.cbxMonth.Location = new System.Drawing.Point(156, 28);
            this.cbxMonth.Name = "cbxMonth";
            this.cbxMonth.Size = new System.Drawing.Size(121, 21);
            this.cbxMonth.TabIndex = 46;
            this.cbxMonth.TextChanged += new System.EventHandler(this.cbxMonth_TextChanged);
            // 
            // gridData
            // 
            this.gridData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridData.Location = new System.Drawing.Point(3, 103);
            this.gridData.Name = "gridData";
            this.gridData.ReadOnly = true;
            this.gridData.RowTemplate.Height = 40;
            this.gridData.Size = new System.Drawing.Size(1910, 887);
            this.gridData.TabIndex = 55;
            this.gridData.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridData_CellDoubleClick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.gridData, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 67);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1916, 1013);
            this.tableLayoutPanel1.TabIndex = 56;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.lblReceivedPersonText);
            this.panel1.Controls.Add(this.lblAcceptedText);
            this.panel1.Controls.Add(this.lblReceivedText);
            this.panel1.Controls.Add(this.lblDepartmentText);
            this.panel1.Controls.Add(this.lblHeader);
            this.panel1.Controls.Add(this.cbxMonth);
            this.panel1.Controls.Add(this.lblMonth);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1910, 94);
            this.panel1.TabIndex = 0;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(718, 24);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(327, 31);
            this.lblHeader.TabIndex = 55;
            this.lblHeader.Text = "DETAIL INFORMATION";
            // 
            // lblDepartmentText
            // 
            this.lblDepartmentText.AutoSize = true;
            this.lblDepartmentText.Location = new System.Drawing.Point(497, 0);
            this.lblDepartmentText.Name = "lblDepartmentText";
            this.lblDepartmentText.Size = new System.Drawing.Size(62, 13);
            this.lblDepartmentText.TabIndex = 56;
            this.lblDepartmentText.Text = "Department";
            this.lblDepartmentText.Visible = false;
            // 
            // lblReceivedText
            // 
            this.lblReceivedText.AutoSize = true;
            this.lblReceivedText.Location = new System.Drawing.Point(565, 0);
            this.lblReceivedText.Name = "lblReceivedText";
            this.lblReceivedText.Size = new System.Drawing.Size(53, 13);
            this.lblReceivedText.TabIndex = 57;
            this.lblReceivedText.Text = "Received";
            this.lblReceivedText.Visible = false;
            // 
            // lblAcceptedText
            // 
            this.lblAcceptedText.AutoSize = true;
            this.lblAcceptedText.Location = new System.Drawing.Point(644, 0);
            this.lblAcceptedText.Name = "lblAcceptedText";
            this.lblAcceptedText.Size = new System.Drawing.Size(53, 13);
            this.lblAcceptedText.TabIndex = 58;
            this.lblAcceptedText.Text = "Accepted";
            this.lblAcceptedText.Visible = false;
            // 
            // lblReceivedPersonText
            // 
            this.lblReceivedPersonText.AutoSize = true;
            this.lblReceivedPersonText.Location = new System.Drawing.Point(712, -3);
            this.lblReceivedPersonText.Name = "lblReceivedPersonText";
            this.lblReceivedPersonText.Size = new System.Drawing.Size(91, 13);
            this.lblReceivedPersonText.TabIndex = 59;
            this.lblReceivedPersonText.Text = "Received/Person";
            this.lblReceivedPersonText.Visible = false;
            // 
            // DetailKaizenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "DetailKaizenForm";
            this.Text = "Detail Kaizen";
            this.Load += new System.EventHandler(this.DetailKaizenForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridData)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblMonth;
        private System.Windows.Forms.ComboBox cbxMonth;
        private System.Windows.Forms.DataGridView gridData;
        private MaterialSkin.Controls.MaterialRaisedButton btnSearch;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblReceivedPersonText;
        private System.Windows.Forms.Label lblAcceptedText;
        private System.Windows.Forms.Label lblReceivedText;
        private System.Windows.Forms.Label lblDepartmentText;
    }
}