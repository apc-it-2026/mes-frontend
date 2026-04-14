namespace KPIINPUT
{
    partial class Repair
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.PRODLINE = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.REPAIRDATE = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRepairReason = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TOTALRECEIVED = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TOTALREPAIRED = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.REMAININGQTY = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.REPAIRREASON = new System.Windows.Forms.ComboBox();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_submit = new System.Windows.Forms.Button();
            this.btn_clear = new System.Windows.Forms.Button();
            this.btn_update = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.TODATE = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.FROMDATE = new System.Windows.Forms.DateTimePicker();
            this.button2 = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.REASON = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.LINE = new System.Windows.Forms.TextBox();
            this.autocompleteMenu1 = new AutocompleteMenuNS.AutocompleteMenu();
            this.txtupdate = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.EDIT = new System.Windows.Forms.DataGridViewButtonColumn();
            this.DELETE = new System.Windows.Forms.DataGridViewButtonColumn();
            this.LOCK_STATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REPAIR_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PROD_LINE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TOTAL_RECEIVED = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TOTAL_REPAIRED = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REMAINING_QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REPAIR_REASON = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CREATED_BY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CREATED_AT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UPDATE_REASON = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.AllowDrop = true;
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(1, 72);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1383, 546);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel1);
            this.tabPage1.ForeColor = System.Drawing.Color.Black;
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(1375, 516);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "INSERT REPAIR DATA";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.591326F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.9266F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.75897F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.18265F));
            this.tableLayoutPanel1.Controls.Add(this.label12, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtupdate, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.PRODLINE, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.REPAIRDATE, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.TOTALRECEIVED, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.TOTALREPAIRED, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.REMAININGQTY, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label6, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.REPAIRREASON, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.btn_cancel, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.btn_submit, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.btn_clear, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.btn_update, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtRepairReason, 1, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1369, 410);
            this.tableLayoutPanel1.TabIndex = 82;
            // 
            // PRODLINE
            // 
            this.PRODLINE.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.autocompleteMenu1.SetAutocompleteMenu(this.PRODLINE, this.autocompleteMenu1);
            this.PRODLINE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.PRODLINE.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PRODLINE.Location = new System.Drawing.Point(863, 23);
            this.PRODLINE.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.PRODLINE.Name = "PRODLINE";
            this.PRODLINE.Size = new System.Drawing.Size(220, 22);
            this.PRODLINE.TabIndex = 69;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(790, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 17);
            this.label1.TabIndex = 68;
            this.label1.Text = "Prodline";
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(159, 25);
            this.label9.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(91, 17);
            this.label9.TabIndex = 78;
            this.label9.Text = "Repair_Date";
            // 
            // REPAIRDATE
            // 
            this.REPAIRDATE.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.REPAIRDATE.Location = new System.Drawing.Point(258, 22);
            this.REPAIRDATE.Name = "REPAIRDATE";
            this.REPAIRDATE.Size = new System.Drawing.Size(222, 23);
            this.REPAIRDATE.TabIndex = 86;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 221);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(240, 34);
            this.label5.TabIndex = 85;
            this.label5.Text = "Please Enter Other Repair Reason Below";
            this.label5.Visible = false;
            // 
            // txtRepairReason
            // 
            this.txtRepairReason.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.autocompleteMenu1.SetAutocompleteMenu(this.txtRepairReason, null);
            this.txtRepairReason.Location = new System.Drawing.Point(258, 218);
            this.txtRepairReason.Multiline = true;
            this.txtRepairReason.Name = "txtRepairReason";
            this.txtRepairReason.Size = new System.Drawing.Size(222, 40);
            this.txtRepairReason.TabIndex = 84;
            this.txtRepairReason.Visible = false;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(150, 93);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 17);
            this.label2.TabIndex = 70;
            this.label2.Text = "Total_Repairs";
            // 
            // TOTALRECEIVED
            // 
            this.TOTALRECEIVED.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.autocompleteMenu1.SetAutocompleteMenu(this.TOTALRECEIVED, null);
            this.TOTALRECEIVED.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TOTALRECEIVED.Location = new System.Drawing.Point(260, 91);
            this.TOTALRECEIVED.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.TOTALRECEIVED.Name = "TOTALRECEIVED";
            this.TOTALRECEIVED.Size = new System.Drawing.Size(220, 22);
            this.TOTALRECEIVED.TabIndex = 71;
            this.TOTALRECEIVED.TextChanged += new System.EventHandler(this.TOTALRECEIVED_TextChanged);
            this.TOTALRECEIVED.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TOTALRECEIVED_KeyPress);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(769, 93);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 17);
            this.label3.TabIndex = 72;
            this.label3.Text = "Repair_Qty";
            // 
            // TOTALREPAIRED
            // 
            this.TOTALREPAIRED.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.autocompleteMenu1.SetAutocompleteMenu(this.TOTALREPAIRED, null);
            this.TOTALREPAIRED.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TOTALREPAIRED.Location = new System.Drawing.Point(863, 91);
            this.TOTALREPAIRED.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.TOTALREPAIRED.Name = "TOTALREPAIRED";
            this.TOTALREPAIRED.Size = new System.Drawing.Size(220, 22);
            this.TOTALREPAIRED.TabIndex = 73;
            this.TOTALREPAIRED.TextChanged += new System.EventHandler(this.TOTALREPAIRED_TextChanged);
            this.TOTALREPAIRED.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TOTALREPAIRED_KeyPress);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(147, 161);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 17);
            this.label4.TabIndex = 74;
            this.label4.Text = "UnRepair_Qty";
            // 
            // REMAININGQTY
            // 
            this.REMAININGQTY.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.autocompleteMenu1.SetAutocompleteMenu(this.REMAININGQTY, null);
            this.REMAININGQTY.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.REMAININGQTY.Location = new System.Drawing.Point(260, 159);
            this.REMAININGQTY.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.REMAININGQTY.Name = "REMAININGQTY";
            this.REMAININGQTY.ReadOnly = true;
            this.REMAININGQTY.Size = new System.Drawing.Size(220, 22);
            this.REMAININGQTY.TabIndex = 75;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(744, 161);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(109, 17);
            this.label6.TabIndex = 76;
            this.label6.Text = "Repair_Reason";
            // 
            // REPAIRREASON
            // 
            this.REPAIRREASON.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.REPAIRREASON.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.REPAIRREASON.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.REPAIRREASON.FormattingEnabled = true;
            this.REPAIRREASON.Location = new System.Drawing.Point(863, 159);
            this.REPAIRREASON.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.REPAIRREASON.Name = "REPAIRREASON";
            this.REPAIRREASON.Size = new System.Drawing.Size(220, 22);
            this.REPAIRREASON.TabIndex = 80;
            this.REPAIRREASON.SelectedIndexChanged += new System.EventHandler(this.REPAIRREASON_SelectedIndexChanged);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cancel.ForeColor = System.Drawing.Color.Transparent;
            this.btn_cancel.Location = new System.Drawing.Point(138, 288);
            this.btn_cancel.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(112, 35);
            this.btn_cancel.TabIndex = 78;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = false;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_submit
            // 
            this.btn_submit.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btn_submit.BackColor = System.Drawing.Color.DodgerBlue;
            this.btn_submit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_submit.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_submit.ForeColor = System.Drawing.Color.Transparent;
            this.btn_submit.Location = new System.Drawing.Point(863, 288);
            this.btn_submit.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btn_submit.Name = "btn_submit";
            this.btn_submit.Size = new System.Drawing.Size(112, 35);
            this.btn_submit.TabIndex = 77;
            this.btn_submit.Text = "Submit";
            this.btn_submit.UseVisualStyleBackColor = false;
            this.btn_submit.Click += new System.EventHandler(this.btn_submit_Click);
            // 
            // btn_clear
            // 
            this.btn_clear.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btn_clear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_clear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_clear.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_clear.ForeColor = System.Drawing.Color.Transparent;
            this.btn_clear.Location = new System.Drawing.Point(260, 288);
            this.btn_clear.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(112, 35);
            this.btn_clear.TabIndex = 87;
            this.btn_clear.Text = "Clear";
            this.btn_clear.UseVisualStyleBackColor = false;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // btn_update
            // 
            this.btn_update.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_update.BackColor = System.Drawing.Color.DodgerBlue;
            this.btn_update.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_update.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_update.ForeColor = System.Drawing.Color.Transparent;
            this.btn_update.Location = new System.Drawing.Point(741, 288);
            this.btn_update.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btn_update.Name = "btn_update";
            this.btn_update.Size = new System.Drawing.Size(112, 35);
            this.btn_update.TabIndex = 88;
            this.btn_update.Text = "Update";
            this.btn_update.UseVisualStyleBackColor = false;
            this.btn_update.Click += new System.EventHandler(this.btn_update_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer1);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(1375, 516);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "VIEW REPAIR DATA";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView2.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle25.BackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle25.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle25.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle25.SelectionBackColor = System.Drawing.Color.Aqua;
            dataGridViewCellStyle25.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle25.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle25;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EDIT,
            this.DELETE,
            this.LOCK_STATUS,
            this.REPAIR_DATE,
            this.PROD_LINE,
            this.TOTAL_RECEIVED,
            this.TOTAL_REPAIRED,
            this.REMAINING_QTY,
            this.REPAIR_REASON,
            this.CREATED_BY,
            this.CREATED_AT,
            this.UPDATE_REASON});
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle26.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle26.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle26.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle26.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle26.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle26.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView2.DefaultCellStyle = dataGridViewCellStyle26;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.GridColor = System.Drawing.Color.Maroon;
            this.dataGridView2.Location = new System.Drawing.Point(0, 0);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle27.BackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle27.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle27.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle27.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle27.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle27.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.RowHeadersDefaultCellStyle = dataGridViewCellStyle27;
            dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle28.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle28.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle28.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle28.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
            this.dataGridView2.RowsDefaultCellStyle = dataGridViewCellStyle28;
            this.dataGridView2.Size = new System.Drawing.Size(1361, 421);
            this.dataGridView2.TabIndex = 6;
            this.dataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellClick);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tableLayoutPanel3.ColumnCount = 9;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.334303F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.6686F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.194024F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.25931F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.335816F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.42069F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.652047F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.33009F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.805118F));
            this.tableLayoutPanel3.Controls.Add(this.TODATE, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.FROMDATE, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.button2, 8, 0);
            this.tableLayoutPanel3.Controls.Add(this.label11, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.label10, 6, 0);
            this.tableLayoutPanel3.Controls.Add(this.REASON, 7, 0);
            this.tableLayoutPanel3.Controls.Add(this.label8, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.LINE, 5, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 4);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1355, 74);
            this.tableLayoutPanel3.TabIndex = 5;
            // 
            // TODATE
            // 
            this.TODATE.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.TODATE.Location = new System.Drawing.Point(423, 25);
            this.TODATE.Name = "TODATE";
            this.TODATE.Size = new System.Drawing.Size(200, 23);
            this.TODATE.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(24, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 19);
            this.label7.TabIndex = 0;
            this.label7.Text = "From_Date";
            // 
            // FROMDATE
            // 
            this.FROMDATE.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.FROMDATE.Location = new System.Drawing.Point(115, 25);
            this.FROMDATE.Name = "FROMDATE";
            this.FROMDATE.Size = new System.Drawing.Size(200, 23);
            this.FROMDATE.TabIndex = 8;
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button2.BackColor = System.Drawing.Color.Goldenrod;
            this.button2.Location = new System.Drawing.Point(1261, 15);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(91, 44);
            this.button2.TabIndex = 3;
            this.button2.Text = "Search";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(656, 27);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(39, 19);
            this.label11.TabIndex = 4;
            this.label11.Text = "Line";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(947, 27);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 19);
            this.label10.TabIndex = 2;
            this.label10.Text = "Reason";
            // 
            // REASON
            // 
            this.REASON.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.REASON.FormattingEnabled = true;
            this.REASON.Location = new System.Drawing.Point(1013, 26);
            this.REASON.Name = "REASON";
            this.REASON.Size = new System.Drawing.Size(242, 25);
            this.REASON.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(350, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 19);
            this.label8.TabIndex = 1;
            this.label8.Text = "To_Date";
            // 
            // LINE
            // 
            this.LINE.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.autocompleteMenu1.SetAutocompleteMenu(this.LINE, this.autocompleteMenu1);
            this.LINE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.LINE.Location = new System.Drawing.Point(701, 25);
            this.LINE.Name = "LINE";
            this.LINE.Size = new System.Drawing.Size(210, 23);
            this.LINE.TabIndex = 9;
            // 
            // autocompleteMenu1
            // 
            this.autocompleteMenu1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.autocompleteMenu1.ImageList = null;
            this.autocompleteMenu1.Items = new string[0];
            this.autocompleteMenu1.TargetControlWrapper = null;
            // 
            // txtupdate
            // 
            this.txtupdate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.autocompleteMenu1.SetAutocompleteMenu(this.txtupdate, null);
            this.txtupdate.Location = new System.Drawing.Point(861, 218);
            this.txtupdate.Multiline = true;
            this.txtupdate.Name = "txtupdate";
            this.txtupdate.Size = new System.Drawing.Size(222, 40);
            this.txtupdate.TabIndex = 85;
            this.txtupdate.Visible = false;
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(741, 229);
            this.label12.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(112, 17);
            this.label12.TabIndex = 83;
            this.label12.Text = "Update_Reason";
            // 
            // EDIT
            // 
            this.EDIT.HeaderText = "EDIT";
            this.EDIT.Name = "EDIT";
            this.EDIT.ReadOnly = true;
            this.EDIT.Text = "EDIT";
            this.EDIT.UseColumnTextForButtonValue = true;
            this.EDIT.Width = 43;
            // 
            // DELETE
            // 
            this.DELETE.HeaderText = "DELETE";
            this.DELETE.Name = "DELETE";
            this.DELETE.ReadOnly = true;
            this.DELETE.Text = "DELETE";
            this.DELETE.UseColumnTextForButtonValue = true;
            this.DELETE.Width = 60;
            // 
            // LOCK_STATUS
            // 
            this.LOCK_STATUS.DataPropertyName = "LOCK_STATUS";
            this.LOCK_STATUS.HeaderText = "LOCK_STATUS";
            this.LOCK_STATUS.Name = "LOCK_STATUS";
            this.LOCK_STATUS.ReadOnly = true;
            this.LOCK_STATUS.Width = 119;
            // 
            // REPAIR_DATE
            // 
            this.REPAIR_DATE.DataPropertyName = "REPAIR_DATE";
            this.REPAIR_DATE.HeaderText = "REPAIR_DATE";
            this.REPAIR_DATE.Name = "REPAIR_DATE";
            this.REPAIR_DATE.ReadOnly = true;
            this.REPAIR_DATE.Width = 116;
            // 
            // PROD_LINE
            // 
            this.PROD_LINE.DataPropertyName = "PROD_LINE";
            this.PROD_LINE.HeaderText = "PROD_LINE";
            this.PROD_LINE.Name = "PROD_LINE";
            this.PROD_LINE.ReadOnly = true;
            this.PROD_LINE.Width = 102;
            // 
            // TOTAL_RECEIVED
            // 
            this.TOTAL_RECEIVED.DataPropertyName = "TOTAL_RECEIVED";
            this.TOTAL_RECEIVED.HeaderText = "TOTAL_RECEIVED";
            this.TOTAL_RECEIVED.Name = "TOTAL_RECEIVED";
            this.TOTAL_RECEIVED.ReadOnly = true;
            this.TOTAL_RECEIVED.Width = 140;
            // 
            // TOTAL_REPAIRED
            // 
            this.TOTAL_REPAIRED.DataPropertyName = "TOTAL_REPAIRED";
            this.TOTAL_REPAIRED.HeaderText = "TOTAL_REPAIRED";
            this.TOTAL_REPAIRED.Name = "TOTAL_REPAIRED";
            this.TOTAL_REPAIRED.ReadOnly = true;
            this.TOTAL_REPAIRED.Width = 141;
            // 
            // REMAINING_QTY
            // 
            this.REMAINING_QTY.DataPropertyName = "REMAINING_QTY";
            this.REMAINING_QTY.HeaderText = "REMAINING_QTY";
            this.REMAINING_QTY.Name = "REMAINING_QTY";
            this.REMAINING_QTY.ReadOnly = true;
            this.REMAINING_QTY.Width = 137;
            // 
            // REPAIR_REASON
            // 
            this.REPAIR_REASON.DataPropertyName = "REPAIR_REASON";
            this.REPAIR_REASON.HeaderText = "REPAIR_REASON";
            this.REPAIR_REASON.Name = "REPAIR_REASON";
            this.REPAIR_REASON.ReadOnly = true;
            this.REPAIR_REASON.Width = 133;
            // 
            // CREATED_BY
            // 
            this.CREATED_BY.DataPropertyName = "CREATED_BY";
            this.CREATED_BY.HeaderText = "CREATED_BY";
            this.CREATED_BY.Name = "CREATED_BY";
            this.CREATED_BY.ReadOnly = true;
            this.CREATED_BY.Width = 111;
            // 
            // CREATED_AT
            // 
            this.CREATED_AT.DataPropertyName = "CREATED_AT";
            this.CREATED_AT.HeaderText = "CREATED_AT";
            this.CREATED_AT.Name = "CREATED_AT";
            this.CREATED_AT.ReadOnly = true;
            this.CREATED_AT.Width = 112;
            // 
            // UPDATE_REASON
            // 
            this.UPDATE_REASON.DataPropertyName = "UPDATE_REASON";
            this.UPDATE_REASON.HeaderText = "UPDATE_REASON";
            this.UPDATE_REASON.Name = "UPDATE_REASON";
            this.UPDATE_REASON.ReadOnly = true;
            this.UPDATE_REASON.Width = 140;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(7, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView2);
            this.splitContainer1.Size = new System.Drawing.Size(1361, 506);
            this.splitContainer1.SplitterDistance = 81;
            this.splitContainer1.TabIndex = 7;
            // 
            // Repair
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1397, 631);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Repair";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "REPAIR_ENTRY";
            this.Load += new System.EventHandler(this.Repair_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btn_submit;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox REASON;
        private System.Windows.Forms.TextBox LINE;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button btn_cancel;
        private AutocompleteMenuNS.AutocompleteMenu autocompleteMenu1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox REPAIRREASON;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox REMAININGQTY;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TOTALRECEIVED;
        private System.Windows.Forms.TextBox TOTALREPAIRED;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtRepairReason;
        private System.Windows.Forms.TextBox PRODLINE;
        private System.Windows.Forms.DateTimePicker REPAIRDATE;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.Button btn_update;
        private System.Windows.Forms.DateTimePicker TODATE;
        private System.Windows.Forms.DateTimePicker FROMDATE;
        private System.Windows.Forms.TextBox txtupdate;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DataGridViewButtonColumn EDIT;
        private System.Windows.Forms.DataGridViewButtonColumn DELETE;
        private System.Windows.Forms.DataGridViewTextBoxColumn LOCK_STATUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn REPAIR_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn PROD_LINE;
        private System.Windows.Forms.DataGridViewTextBoxColumn TOTAL_RECEIVED;
        private System.Windows.Forms.DataGridViewTextBoxColumn TOTAL_REPAIRED;
        private System.Windows.Forms.DataGridViewTextBoxColumn REMAINING_QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn REPAIR_REASON;
        private System.Windows.Forms.DataGridViewTextBoxColumn CREATED_BY;
        private System.Windows.Forms.DataGridViewTextBoxColumn CREATED_AT;
        private System.Windows.Forms.DataGridViewTextBoxColumn UPDATE_REASON;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}