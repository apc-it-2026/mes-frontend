namespace F_TMS_TierMeeting3_Main
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnUpload = new MaterialSkin.Controls.MaterialRaisedButton();
            this.gridData = new System.Windows.Forms.DataGridView();
            this.btnMDP = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnPDP = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnSDP = new MaterialSkin.Controls.MaterialRaisedButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridData)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(737, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(358, 31);
            this.label1.TabIndex = 14;
            this.label1.Text = "DELIVERY EACH MONTH";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(91, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 31);
            this.label2.TabIndex = 13;
            this.label2.Text = "Delivery";
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
            this.gridData.Location = new System.Drawing.Point(90, 197);
            this.gridData.Name = "gridData";
            this.gridData.ReadOnly = true;
            this.gridData.RowTemplate.Height = 40;
            this.gridData.Size = new System.Drawing.Size(1732, 785);
            this.gridData.TabIndex = 17;
            // 
            // btnMDP
            // 
            this.btnMDP.AutoSize = true;
            this.btnMDP.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnMDP.Depth = 0;
            this.btnMDP.Icon = null;
            this.btnMDP.Location = new System.Drawing.Point(340, 87);
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
            this.btnPDP.Location = new System.Drawing.Point(443, 87);
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
            this.btnSDP.Location = new System.Drawing.Point(548, 87);
            this.btnSDP.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnSDP.Name = "btnSDP";
            this.btnSDP.Primary = true;
            this.btnSDP.Size = new System.Drawing.Size(47, 36);
            this.btnSDP.TabIndex = 20;
            this.btnSDP.Text = "SDP";
            this.btnSDP.UseVisualStyleBackColor = true;
            this.btnSDP.Click += new System.EventHandler(this.btnSDP_Click);
            // 
            // KPIDeliveryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.btnSDP);
            this.Controls.Add(this.btnPDP);
            this.Controls.Add(this.btnMDP);
            this.Controls.Add(this.gridData);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Name = "KPIDeliveryForm";
            this.Text = "Delivery";
            this.Load += new System.EventHandler(this.DetailDeliveryForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView gridData;
        private MaterialSkin.Controls.MaterialRaisedButton btnUpload;
        private MaterialSkin.Controls.MaterialRaisedButton btnMDP;
        private MaterialSkin.Controls.MaterialRaisedButton btnPDP;
        private MaterialSkin.Controls.MaterialRaisedButton btnSDP;
    }
}