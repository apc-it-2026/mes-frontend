namespace F_SFC_TrackIn_AssemblyOrder
{
    partial class TrackIn_AssemblyOrder_Supplement
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
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxDDept = new System.Windows.Forms.TextBox();
            this.textBoxMainProdOrder = new System.Windows.Forms.TextBox();
            this.textBoxLeftQty = new System.Windows.Forms.TextBox();
            this.textBoxRightQty = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.autocompleteMenu1 = new AutocompleteMenuNS.AutocompleteMenu();
            this.textBoxPO = new System.Windows.Forms.TextBox();
            this.textBoxArtNo = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textStockName = new System.Windows.Forms.TextBox();
            this.textStockCode = new System.Windows.Forms.TextBox();
            this.autocompleteMenu2 = new AutocompleteMenuNS.AutocompleteMenu();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxSizeNo = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 18F);
            this.label1.Location = new System.Drawing.Point(192, 186);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(193, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "主工单号(*):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 18F);
            this.label2.Location = new System.Drawing.Point(256, 349);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 30);
            this.label2.TabIndex = 1;
            this.label2.Text = "码数(*):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 18F);
            this.label3.Location = new System.Drawing.Point(192, 510);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(193, 30);
            this.label3.TabIndex = 2;
            this.label3.Text = "左脚数量(*):";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 18F);
            this.label4.Location = new System.Drawing.Point(192, 560);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(193, 30);
            this.label4.TabIndex = 3;
            this.label4.Text = "右脚数量(*):";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 18F);
            this.label5.Location = new System.Drawing.Point(256, 129);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 30);
            this.label5.TabIndex = 4;
            this.label5.Text = "部门(*):";
            // 
            // textBoxDDept
            // 
            this.autocompleteMenu1.SetAutocompleteMenu(this.textBoxDDept, null);
            this.textBoxDDept.Location = new System.Drawing.Point(423, 132);
            this.textBoxDDept.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxDDept.Name = "textBoxDDept";
            this.textBoxDDept.ReadOnly = true;
            this.textBoxDDept.Size = new System.Drawing.Size(299, 25);
            this.textBoxDDept.TabIndex = 5;
            // 
            // textBoxMainProdOrder
            // 
            this.autocompleteMenu1.SetAutocompleteMenu(this.textBoxMainProdOrder, this.autocompleteMenu1);
            this.textBoxMainProdOrder.Location = new System.Drawing.Point(423, 190);
            this.textBoxMainProdOrder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxMainProdOrder.Name = "textBoxMainProdOrder";
            this.textBoxMainProdOrder.Size = new System.Drawing.Size(299, 25);
            this.textBoxMainProdOrder.TabIndex = 6;
            this.textBoxMainProdOrder.Leave += new System.EventHandler(this.textBox2_Leave);
            // 
            // textBoxLeftQty
            // 
            this.autocompleteMenu1.SetAutocompleteMenu(this.textBoxLeftQty, null);
            this.textBoxLeftQty.Location = new System.Drawing.Point(421, 510);
            this.textBoxLeftQty.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxLeftQty.Name = "textBoxLeftQty";
            this.textBoxLeftQty.Size = new System.Drawing.Size(299, 25);
            this.textBoxLeftQty.TabIndex = 8;
            // 
            // textBoxRightQty
            // 
            this.autocompleteMenu1.SetAutocompleteMenu(this.textBoxRightQty, null);
            this.textBoxRightQty.Location = new System.Drawing.Point(421, 560);
            this.textBoxRightQty.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxRightQty.Name = "textBoxRightQty";
            this.textBoxRightQty.Size = new System.Drawing.Size(299, 25);
            this.textBoxRightQty.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 18F);
            this.button1.Location = new System.Drawing.Point(427, 624);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 48);
            this.button1.TabIndex = 10;
            this.button1.Text = "提交";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("宋体", 18F);
            this.button2.Location = new System.Drawing.Point(605, 624);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(117, 48);
            this.button2.TabIndex = 11;
            this.button2.Text = "退出";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // autocompleteMenu1
            // 
            this.autocompleteMenu1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.autocompleteMenu1.ImageList = null;
            this.autocompleteMenu1.Items = new string[0];
            this.autocompleteMenu1.TargetControlWrapper = null;
            // 
            // textBoxPO
            // 
            this.autocompleteMenu1.SetAutocompleteMenu(this.textBoxPO, null);
            this.textBoxPO.Location = new System.Drawing.Point(423, 250);
            this.textBoxPO.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxPO.Name = "textBoxPO";
            this.textBoxPO.ReadOnly = true;
            this.textBoxPO.Size = new System.Drawing.Size(299, 25);
            this.textBoxPO.TabIndex = 13;
            // 
            // textBoxArtNo
            // 
            this.autocompleteMenu1.SetAutocompleteMenu(this.textBoxArtNo, null);
            this.textBoxArtNo.Location = new System.Drawing.Point(423, 302);
            this.textBoxArtNo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxArtNo.Name = "textBoxArtNo";
            this.textBoxArtNo.ReadOnly = true;
            this.textBoxArtNo.Size = new System.Drawing.Size(299, 25);
            this.textBoxArtNo.TabIndex = 15;
            // 
            // textBox3
            // 
            this.autocompleteMenu1.SetAutocompleteMenu(this.textBox3, null);
            this.textBox3.Location = new System.Drawing.Point(731, 190);
            this.textBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(100, 25);
            this.textBox3.TabIndex = 17;
            // 
            // textBox8
            // 
            this.autocompleteMenu1.SetAutocompleteMenu(this.textBox8, null);
            this.textBox8.Location = new System.Drawing.Point(732, 359);
            this.textBox8.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox8.Name = "textBox8";
            this.textBox8.ReadOnly = true;
            this.textBox8.Size = new System.Drawing.Size(100, 25);
            this.textBox8.TabIndex = 18;
            // 
            // textStockName
            // 
            this.autocompleteMenu1.SetAutocompleteMenu(this.textStockName, null);
            this.textStockName.Location = new System.Drawing.Point(421, 455);
            this.textStockName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textStockName.Name = "textStockName";
            this.textStockName.ReadOnly = true;
            this.textStockName.Size = new System.Drawing.Size(299, 25);
            this.textStockName.TabIndex = 22;
            // 
            // textStockCode
            // 
            this.autocompleteMenu1.SetAutocompleteMenu(this.textStockCode, this.autocompleteMenu2);
            this.textStockCode.Location = new System.Drawing.Point(421, 404);
            this.textStockCode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textStockCode.Name = "textStockCode";
            this.textStockCode.Size = new System.Drawing.Size(299, 25);
            this.textStockCode.TabIndex = 21;
            this.textStockCode.Leave += new System.EventHandler(this.textStockCode_Leave);
            // 
            // autocompleteMenu2
            // 
            this.autocompleteMenu2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.autocompleteMenu2.ImageList = null;
            this.autocompleteMenu2.Items = new string[0];
            this.autocompleteMenu2.TargetControlWrapper = null;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 18F);
            this.label6.Location = new System.Drawing.Point(288, 244);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 30);
            this.label6.TabIndex = 12;
            this.label6.Text = "PO(*):";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 18F);
            this.label7.Location = new System.Drawing.Point(240, 298);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(148, 30);
            this.label7.TabIndex = 14;
            this.label7.Text = "ArtNo(*):";
            // 
            // comboBoxSizeNo
            // 
            this.comboBoxSizeNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSizeNo.FormattingEnabled = true;
            this.comboBoxSizeNo.Location = new System.Drawing.Point(423, 359);
            this.comboBoxSizeNo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxSizeNo.Name = "comboBoxSizeNo";
            this.comboBoxSizeNo.Size = new System.Drawing.Size(297, 23);
            this.comboBoxSizeNo.TabIndex = 16;
            this.comboBoxSizeNo.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 18F);
            this.label8.Location = new System.Drawing.Point(192, 455);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(193, 30);
            this.label8.TabIndex = 20;
            this.label8.Text = "仓库名称(*):";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 18F);
            this.label9.Location = new System.Drawing.Point(192, 404);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(193, 30);
            this.label9.TabIndex = 19;
            this.label9.Text = "仓库编码(*):";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(729, 519);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 15);
            this.label10.TabIndex = 23;
            this.label10.Text = "(单位/只)";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(729, 567);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(76, 15);
            this.label11.TabIndex = 23;
            this.label11.Text = "(单位/只)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.Blue;
            this.label12.Location = new System.Drawing.Point(418, 599);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(195, 15);
            this.label12.TabIndex = 24;
            this.label12.Text = "注：左右脚数量不能为小数~";
            // 
            // TrackIn_AssemblyOrder_Supplement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 738);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textStockName);
            this.Controls.Add(this.textStockCode);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.comboBoxSizeNo);
            this.Controls.Add(this.textBoxArtNo);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxPO);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoxRightQty);
            this.Controls.Add(this.textBoxLeftQty);
            this.Controls.Add(this.textBoxMainProdOrder);
            this.Controls.Add(this.textBoxDDept);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "TrackIn_AssemblyOrder_Supplement";
            this.Text = "TrackIn_AssemblyOrder_Supplement";
            this.Load += new System.EventHandler(this.SupplementForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxDDept;
        private System.Windows.Forms.TextBox textBoxMainProdOrder;
        private System.Windows.Forms.TextBox textBoxLeftQty;
        private System.Windows.Forms.TextBox textBoxRightQty;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private AutocompleteMenuNS.AutocompleteMenu autocompleteMenu1;
        private System.Windows.Forms.TextBox textBoxPO;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxArtNo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBoxSizeNo;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox textStockName;
        private System.Windows.Forms.TextBox textStockCode;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private AutocompleteMenuNS.AutocompleteMenu autocompleteMenu2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
    }
}