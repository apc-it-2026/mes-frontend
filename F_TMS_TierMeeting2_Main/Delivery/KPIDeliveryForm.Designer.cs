namespace F_TMS_TierMeeting2_Main
{
    partial class KPIDeliveryForm
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
            this.lblHeader = new CusContorl.CusLabel();
            this.btnUpload = new MaterialSkin.Controls.MaterialRaisedButton();
            this.gridData = new System.Windows.Forms.DataGridView();
            this.btnMDP = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnPDP = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnSDP = new MaterialSkin.Controls.MaterialRaisedButton();
            this.tableLayoutPanel1 = new CusContorl.CusTableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.gridData)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(644, 32);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(358, 31);
            this.lblHeader.TabIndex = 14;
            this.lblHeader.Text = "DELIVERY EACH MONTH";
            // 
            // btnUpload
            // 
            this.btnUpload.AutoSize = true;
            this.btnUpload.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnUpload.Depth = 0;
            this.btnUpload.Icon = null;
            this.btnUpload.Location = new System.Drawing.Point(1718, 87);
            this.btnUpload.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Primary = true;
            this.btnUpload.Size = new System.Drawing.Size(104, 36);
            this.btnUpload.TabIndex = 16;
            this.btnUpload.Text = "Upload file";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // gridData
            // 
            this.gridData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridData.ColumnHeadersHeight = 36;
            this.gridData.Location = new System.Drawing.Point(3, 103);
            this.gridData.Name = "gridData";
            this.gridData.ReadOnly = true;
            this.gridData.RowTemplate.Height = 40;
            this.gridData.Size = new System.Drawing.Size(1912, 886);
            this.gridData.TabIndex = 17;
            // 
            // btnMDP
            // 
            this.btnMDP.AutoSize = true;
            this.btnMDP.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnMDP.Depth = 0;
            this.btnMDP.Icon = null;
            this.btnMDP.Location = new System.Drawing.Point(3, 3);
            this.btnMDP.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnMDP.Name = "btnMDP";
            this.btnMDP.Primary = true;
            this.btnMDP.Size = new System.Drawing.Size(51, 36);
            this.btnMDP.TabIndex = 18;
            this.btnMDP.Text = "MDP";
            this.btnMDP.UseVisualStyleBackColor = true;
            this.btnMDP.Click += new System.EventHandler(this.btnMDP_Click);
            // 
            // btnPDP
            // 
            this.btnPDP.AutoSize = true;
            this.btnPDP.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnPDP.Depth = 0;
            this.btnPDP.Icon = null;
            this.btnPDP.Location = new System.Drawing.Point(60, 3);
            this.btnPDP.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnPDP.Name = "btnPDP";
            this.btnPDP.Primary = true;
            this.btnPDP.Size = new System.Drawing.Size(47, 36);
            this.btnPDP.TabIndex = 19;
            this.btnPDP.Text = "PDP";
            this.btnPDP.UseVisualStyleBackColor = true;
            this.btnPDP.Click += new System.EventHandler(this.btnPDP_Click);
            // 
            // btnSDP
            // 
            this.btnSDP.AutoSize = true;
            this.btnSDP.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSDP.Depth = 0;
            this.btnSDP.Icon = null;
            this.btnSDP.Location = new System.Drawing.Point(113, 3);
            this.btnSDP.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnSDP.Name = "btnSDP";
            this.btnSDP.Primary = true;
            this.btnSDP.Size = new System.Drawing.Size(47, 36);
            this.btnSDP.TabIndex = 20;
            this.btnSDP.Text = "SDP";
            this.btnSDP.UseVisualStyleBackColor = true;
            this.btnSDP.Click += new System.EventHandler(this.btnSDP_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.gridData, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 67);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1918, 1012);
            this.tableLayoutPanel1.TabIndex = 21;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.btnPDP);
            this.panel1.Controls.Add(this.btnSDP);
            this.panel1.Controls.Add(this.lblHeader);
            this.panel1.Controls.Add(this.btnMDP);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1912, 94);
            this.panel1.TabIndex = 18;
            // 
            // KPIDeliveryForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btnUpload);
            this.Name = "KPIDeliveryForm";
            this.Text = "Delivery";
            this.Load += new System.EventHandler(this.DetailDeliveryForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridData)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CusContorl.CusLabel lblHeader;
        private System.Windows.Forms.DataGridView gridData;
        private MaterialSkin.Controls.MaterialRaisedButton btnUpload;
        private MaterialSkin.Controls.MaterialRaisedButton btnMDP;
        private MaterialSkin.Controls.MaterialRaisedButton btnPDP;
        private MaterialSkin.Controls.MaterialRaisedButton btnSDP;
        private CusContorl.CusTableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
    }
}