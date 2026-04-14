namespace Aql_Inspection
{
    partial class AQL_Inspection
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Upload_btn = new Button();
            dataGridView1 = new DataGridView();
            Check_btn = new Button();
            Submit_btn = new Button();
            panel1 = new Panel();
            Submit_button = new Button();
            panel2 = new Panel();
            panel3 = new Panel();
            panel4 = new Panel();
            template_btn = new Button();
            CRD_TO_label = new Label();
            dateTimePicker2 = new DateTimePicker();
            CRD_From_label = new Label();
            dateTimePicker1 = new DateTimePicker();
            panel5 = new Panel();
            Search_btn = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            SuspendLayout();
            // 
            // Upload_btn
            // 
            Upload_btn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Upload_btn.BackColor = Color.WhiteSmoke;
            Upload_btn.BackgroundImageLayout = ImageLayout.None;
            Upload_btn.FlatStyle = FlatStyle.Flat;
            Upload_btn.Font = new Font("Lucida Bright", 9.75F, FontStyle.Bold);
            Upload_btn.Location = new Point(-3, -3);
            Upload_btn.Name = "Upload_btn";
            Upload_btn.Size = new Size(98, 43);
            Upload_btn.TabIndex = 0;
            Upload_btn.Text = "Upload";
            Upload_btn.UseVisualStyleBackColor = false;
            Upload_btn.Click += Upload_btn_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.BackgroundColor = Color.WhiteSmoke;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.GridColor = SystemColors.ControlText;
            dataGridView1.Location = new Point(55, 140);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(1126, 493);
            dataGridView1.TabIndex = 1;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // Check_btn
            // 
            Check_btn.BackColor = Color.DarkSlateGray;
            Check_btn.BackgroundImageLayout = ImageLayout.None;
            Check_btn.FlatStyle = FlatStyle.Flat;
            Check_btn.Font = new Font("Lucida Bright", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Check_btn.ForeColor = Color.White;
            Check_btn.Location = new Point(-9, -8);
            Check_btn.Name = "Check_btn";
            Check_btn.Size = new Size(139, 53);
            Check_btn.TabIndex = 2;
            Check_btn.Text = "Check all Po's";
            Check_btn.UseVisualStyleBackColor = false;
            Check_btn.Visible = false;
            Check_btn.Click += Check_btn_Click;
            // 
            // Submit_btn
            // 
            Submit_btn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Submit_btn.BackColor = Color.DarkSlateGray;
            Submit_btn.BackgroundImageLayout = ImageLayout.None;
            Submit_btn.FlatStyle = FlatStyle.Flat;
            Submit_btn.Font = new Font("Lucida Bright", 9F, FontStyle.Bold);
            Submit_btn.ForeColor = Color.White;
            Submit_btn.Location = new Point(0, -7);
            Submit_btn.Name = "Submit_btn";
            Submit_btn.Size = new Size(98, 53);
            Submit_btn.TabIndex = 3;
            Submit_btn.Text = "Submit";
            Submit_btn.UseVisualStyleBackColor = false;
            Submit_btn.Click += Submit_btn_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panel1.Controls.Add(Submit_button);
            panel1.Controls.Add(Submit_btn);
            panel1.Location = new Point(1088, 82);
            panel1.Name = "panel1";
            panel1.Size = new Size(93, 38);
            panel1.TabIndex = 12;
            // 
            // Submit_button
            // 
            Submit_button.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Submit_button.BackColor = Color.DarkSlateGray;
            Submit_button.BackgroundImageLayout = ImageLayout.None;
            Submit_button.FlatStyle = FlatStyle.Flat;
            Submit_button.Font = new Font("Lucida Bright", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Submit_button.ForeColor = Color.White;
            Submit_button.Location = new Point(-2, -7);
            Submit_button.Name = "Submit_button";
            Submit_button.Size = new Size(98, 53);
            Submit_button.TabIndex = 4;
            Submit_button.Text = "Submit";
            Submit_button.UseVisualStyleBackColor = false;
            Submit_button.Click += Submit_button_Click;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panel2.Controls.Add(Upload_btn);
            panel2.Location = new Point(983, 82);
            panel2.Name = "panel2";
            panel2.Size = new Size(93, 38);
            panel2.TabIndex = 13;
            // 
            // panel3
            // 
            panel3.Controls.Add(Check_btn);
            panel3.Location = new Point(675, 84);
            panel3.Name = "panel3";
            panel3.Size = new Size(120, 38);
            panel3.TabIndex = 14;
            // 
            // panel4
            // 
            panel4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panel4.Controls.Add(template_btn);
            panel4.Location = new Point(862, 82);
            panel4.Name = "panel4";
            panel4.Size = new Size(93, 38);
            panel4.TabIndex = 15;
            // 
            // template_btn
            // 
            template_btn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            template_btn.BackColor = Color.WhiteSmoke;
            template_btn.BackgroundImageLayout = ImageLayout.None;
            template_btn.FlatStyle = FlatStyle.Flat;
            template_btn.Font = new Font("Lucida Bright", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            template_btn.Location = new Point(-8, -3);
            template_btn.Name = "template_btn";
            template_btn.Size = new Size(115, 43);
            template_btn.TabIndex = 16;
            template_btn.Text = "Template";
            template_btn.UseVisualStyleBackColor = false;
            template_btn.Click += template_btn_Click;
            // 
            // CRD_TO_label
            // 
            CRD_TO_label.AutoSize = true;
            CRD_TO_label.BackColor = Color.White;
            CRD_TO_label.Font = new Font("Lucida Bright", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            CRD_TO_label.Location = new Point(290, 92);
            CRD_TO_label.Name = "CRD_TO_label";
            CRD_TO_label.Size = new Size(82, 18);
            CRD_TO_label.TabIndex = 19;
            CRD_TO_label.Text = "To Date :";
            // 
            // dateTimePicker2
            // 
            dateTimePicker2.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dateTimePicker2.Location = new Point(389, 88);
            dateTimePicker2.Name = "dateTimePicker2";
            dateTimePicker2.Size = new Size(103, 25);
            dateTimePicker2.TabIndex = 18;
            dateTimePicker2.ValueChanged += dateTimePicker2_ValueChanged;
            // 
            // CRD_From_label
            // 
            CRD_From_label.AutoSize = true;
            CRD_From_label.BackColor = Color.White;
            CRD_From_label.Font = new Font("Lucida Bright", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            CRD_From_label.Location = new Point(57, 92);
            CRD_From_label.Name = "CRD_From_label";
            CRD_From_label.Size = new Size(102, 18);
            CRD_From_label.TabIndex = 17;
            CRD_From_label.Text = "From Date :";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dateTimePicker1.Location = new Point(165, 88);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(103, 25);
            dateTimePicker1.TabIndex = 16;
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // panel5
            // 
            panel5.Controls.Add(Search_btn);
            panel5.Location = new Point(525, 85);
            panel5.Name = "panel5";
            panel5.Size = new Size(76, 31);
            panel5.TabIndex = 20;
            // 
            // Search_btn
            // 
            Search_btn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Search_btn.BackColor = Color.DarkSlateGray;
            Search_btn.BackgroundImageLayout = ImageLayout.None;
            Search_btn.FlatStyle = FlatStyle.Flat;
            Search_btn.Font = new Font("Lucida Bright", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Search_btn.ForeColor = Color.White;
            Search_btn.Location = new Point(-10, -12);
            Search_btn.Name = "Search_btn";
            Search_btn.Size = new Size(98, 53);
            Search_btn.TabIndex = 5;
            Search_btn.Text = "Search";
            Search_btn.UseVisualStyleBackColor = false;
            Search_btn.Click += Search_btn_Click;
            // 
            // AQL_Inspection
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1246, 677);
            Controls.Add(panel5);
            Controls.Add(CRD_TO_label);
            Controls.Add(dateTimePicker2);
            Controls.Add(CRD_From_label);
            Controls.Add(dateTimePicker1);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(dataGridView1);
            Name = "AQL_Inspection";
            Text = "AQL Inspection PO's";
            Load += AQL_Inspection_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel5.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Upload_btn;
        private DataGridView dataGridView1;
        private Button Check_btn;
        private Button Submit_btn;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private Button template_btn;
        private Label CRD_TO_label;
        private DateTimePicker dateTimePicker2;
        private Label CRD_From_label;
        private DateTimePicker dateTimePicker1;
        private Panel panel5;
        private Button Submit_button;
        private Button Search_btn;
    }
}
