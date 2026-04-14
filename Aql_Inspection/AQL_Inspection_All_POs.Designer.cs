namespace Aql_Inspection
{
    partial class AQL_Inspection_All_POs
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
            SO_comboBox = new ComboBox();
            SO_label = new Label();
            dateTimePicker1 = new DateTimePicker();
            CRD_From_label = new Label();
            CRD_TO_label = new Label();
            dateTimePicker2 = new DateTimePicker();
            dataGridView1 = new DataGridView();
            Search_btn = new Button();
            Clear_btn = new Button();
            panel1 = new Panel();
            panel2 = new Panel();
            label1 = new Label();
            Status_comboBox = new ComboBox();
            Destination_label = new Label();
            Destination_comboBox = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // SO_comboBox
            // 
            SO_comboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            SO_comboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            SO_comboBox.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            SO_comboBox.FormattingEnabled = true;
            SO_comboBox.Location = new Point(115, 87);
            SO_comboBox.Name = "SO_comboBox";
            SO_comboBox.Size = new Size(137, 25);
            SO_comboBox.TabIndex = 0;
            SO_comboBox.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // SO_label
            // 
            SO_label.AutoSize = true;
            SO_label.BackColor = Color.White;
            SO_label.Font = new Font("Lucida Bright", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            SO_label.Location = new Point(70, 89);
            SO_label.Name = "SO_label";
            SO_label.Size = new Size(39, 18);
            SO_label.TabIndex = 1;
            SO_label.Text = "SO :";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dateTimePicker1.Location = new Point(776, 87);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(104, 25);
            dateTimePicker1.TabIndex = 4;
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // CRD_From_label
            // 
            CRD_From_label.AutoSize = true;
            CRD_From_label.BackColor = Color.White;
            CRD_From_label.Font = new Font("Lucida Bright", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            CRD_From_label.Location = new Point(668, 89);
            CRD_From_label.Name = "CRD_From_label";
            CRD_From_label.Size = new Size(102, 18);
            CRD_From_label.TabIndex = 5;
            CRD_From_label.Text = "From Date :";
            // 
            // CRD_TO_label
            // 
            CRD_TO_label.AutoSize = true;
            CRD_TO_label.BackColor = Color.White;
            CRD_TO_label.Font = new Font("Lucida Bright", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            CRD_TO_label.Location = new Point(905, 89);
            CRD_TO_label.Name = "CRD_TO_label";
            CRD_TO_label.Size = new Size(82, 18);
            CRD_TO_label.TabIndex = 7;
            CRD_TO_label.Text = "To Date :";
            // 
            // dateTimePicker2
            // 
            dateTimePicker2.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dateTimePicker2.Location = new Point(993, 85);
            dateTimePicker2.Name = "dateTimePicker2";
            dateTimePicker2.Size = new Size(103, 25);
            dateTimePicker2.TabIndex = 6;
            dateTimePicker2.ValueChanged += dateTimePicker2_ValueChanged;
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.BackgroundColor = SystemColors.ButtonFace;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(51, 137);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(1353, 469);
            dataGridView1.TabIndex = 8;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // Search_btn
            // 
            Search_btn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Search_btn.BackColor = Color.DarkSlateGray;
            Search_btn.BackgroundImageLayout = ImageLayout.None;
            Search_btn.FlatStyle = FlatStyle.Flat;
            Search_btn.Font = new Font("Lucida Bright", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Search_btn.ForeColor = Color.Transparent;
            Search_btn.Location = new Point(-15, -18);
            Search_btn.Name = "Search_btn";
            Search_btn.Size = new Size(126, 74);
            Search_btn.TabIndex = 9;
            Search_btn.Text = "Search";
            Search_btn.UseVisualStyleBackColor = false;
            Search_btn.Click += Search_btn_Click;
            // 
            // Clear_btn
            // 
            Clear_btn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Clear_btn.BackColor = Color.WhiteSmoke;
            Clear_btn.BackgroundImageLayout = ImageLayout.None;
            Clear_btn.FlatStyle = FlatStyle.Flat;
            Clear_btn.Font = new Font("Lucida Bright", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Clear_btn.ForeColor = Color.Black;
            Clear_btn.Location = new Point(-8, -7);
            Clear_btn.Name = "Clear_btn";
            Clear_btn.Padding = new Padding(3);
            Clear_btn.Size = new Size(108, 53);
            Clear_btn.TabIndex = 10;
            Clear_btn.Text = "Clear";
            Clear_btn.UseVisualStyleBackColor = false;
            Clear_btn.Click += Clear_btn_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panel1.Controls.Add(Clear_btn);
            panel1.Location = new Point(1131, 82);
            panel1.Name = "panel1";
            panel1.Size = new Size(93, 38);
            panel1.TabIndex = 11;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panel2.Controls.Add(Search_btn);
            panel2.Location = new Point(1289, 82);
            panel2.Name = "panel2";
            panel2.Size = new Size(93, 38);
            panel2.TabIndex = 12;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.White;
            label1.Font = new Font("Lucida Bright", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(491, 89);
            label1.Name = "label1";
            label1.Size = new Size(67, 18);
            label1.TabIndex = 14;
            label1.Text = "Status :";
            // 
            // Status_comboBox
            // 
            Status_comboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            Status_comboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            Status_comboBox.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Status_comboBox.FormattingEnabled = true;
            Status_comboBox.Location = new Point(564, 87);
            Status_comboBox.Name = "Status_comboBox";
            Status_comboBox.Size = new Size(88, 25);
            Status_comboBox.TabIndex = 13;
            // 
            // Destination_label
            // 
            Destination_label.AutoSize = true;
            Destination_label.BackColor = Color.White;
            Destination_label.Font = new Font("Lucida Bright", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Destination_label.Location = new Point(264, 90);
            Destination_label.Name = "Destination_label";
            Destination_label.Size = new Size(107, 18);
            Destination_label.TabIndex = 16;
            Destination_label.Text = "Destination :";
            // 
            // Destination_comboBox
            // 
            Destination_comboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            Destination_comboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            Destination_comboBox.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Destination_comboBox.FormattingEnabled = true;
            Destination_comboBox.Location = new Point(373, 88);
            Destination_comboBox.Name = "Destination_comboBox";
            Destination_comboBox.Size = new Size(102, 25);
            Destination_comboBox.TabIndex = 15;
            // 
            // AQL_Inspection_All_POs
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1450, 663);
            Controls.Add(Destination_label);
            Controls.Add(Destination_comboBox);
            Controls.Add(label1);
            Controls.Add(Status_comboBox);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(dataGridView1);
            Controls.Add(CRD_TO_label);
            Controls.Add(dateTimePicker2);
            Controls.Add(CRD_From_label);
            Controls.Add(dateTimePicker1);
            Controls.Add(SO_label);
            Controls.Add(SO_comboBox);
            Name = "AQL_Inspection_All_POs";
            Text = "AQL Inspection All PO's";
            Load += AQL_Inspection_All_POs_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox SO_comboBox;
        private Label SO_label;
        private DateTimePicker dateTimePicker1;
        private Label CRD_From_label;
        private Label CRD_TO_label;
        private DateTimePicker dateTimePicker2;
        private DataGridView dataGridView1;
        private Button Search_btn;
        private Button Clear_btn;
        private Panel panel1;
        private Panel panel2;
        private Label label1;
        private ComboBox Status_comboBox;
        private Label Destination_label;
        private ComboBox Destination_comboBox;
    }
}