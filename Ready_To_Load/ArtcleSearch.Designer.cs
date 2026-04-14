namespace Ready_To_Load
{
    partial class ArtcleSearch
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
            this.label3 = new System.Windows.Forms.Label();
            this.txt_article = new System.Windows.Forms.TextBox();
            this.art_btn_search = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.art_btn_confirm = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.art_btn_confirm);
            this.panel1.Controls.Add(this.art_btn_search);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txt_article);
            this.panel1.Location = new System.Drawing.Point(2, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(435, 68);
            this.panel1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Article :";
            // 
            // txt_article
            // 
            this.txt_article.Location = new System.Drawing.Point(111, 22);
            this.txt_article.Name = "txt_article";
            this.txt_article.Size = new System.Drawing.Size(99, 20);
            this.txt_article.TabIndex = 2;
            // 
            // art_btn_search
            // 
            this.art_btn_search.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.art_btn_search.Location = new System.Drawing.Point(229, 20);
            this.art_btn_search.Name = "art_btn_search";
            this.art_btn_search.Size = new System.Drawing.Size(55, 25);
            this.art_btn_search.TabIndex = 4;
            this.art_btn_search.Text = "Search";
            this.art_btn_search.UseVisualStyleBackColor = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.MintCream;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(2, 73);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(435, 448);
            this.dataGridView1.TabIndex = 1;
            // 
            // art_btn_confirm
            // 
            this.art_btn_confirm.BackColor = System.Drawing.Color.DarkGreen;
            this.art_btn_confirm.ForeColor = System.Drawing.SystemColors.Control;
            this.art_btn_confirm.Location = new System.Drawing.Point(325, 15);
            this.art_btn_confirm.Name = "art_btn_confirm";
            this.art_btn_confirm.Size = new System.Drawing.Size(75, 32);
            this.art_btn_confirm.TabIndex = 5;
            this.art_btn_confirm.Text = "Confirm";
            this.art_btn_confirm.UseVisualStyleBackColor = false;
            // 
            // ArtcleSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 527);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Name = "ArtcleSearch";
            this.Text = "ArtcleSearch";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button art_btn_search;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_article;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button art_btn_confirm;
    }
}