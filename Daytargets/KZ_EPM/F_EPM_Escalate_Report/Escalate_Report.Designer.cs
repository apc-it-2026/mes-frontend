
namespace F_EPM_Escalate_Report
{
    partial class Escalate_Report
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.FA = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Escalatelb1 = new System.Windows.Forms.Label();
            this.Submitbtn = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Faultreporttxt = new System.Windows.Forms.TextBox();
            this.Storagepostxt = new System.Windows.Forms.TextBox();
            this.DeviceModeltxt = new System.Windows.Forms.TextBox();
            this.DeviceNametxt = new System.Windows.Forms.TextBox();
            this.DeviceFAtxt = new System.Windows.Forms.TextBox();
            this.Faultreportlbl = new System.Windows.Forms.Label();
            this.Storageposilbl = new System.Windows.Forms.Label();
            this.DeviceModellbl = new System.Windows.Forms.Label();
            this.DeviceNamelb = new System.Windows.Forms.Label();
            this.DeviceFAlbl = new System.Windows.Forms.Label();
            this.autocompleteMenu1 = new AutocompleteMenuNS.AutocompleteMenu();
            this.Whometolbl = new System.Windows.Forms.Label();
            this.Escalationtext = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Pressbtn = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.Escalationtext);
            this.panel1.Controls.Add(this.Whometolbl);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.Submitbtn);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.Faultreporttxt);
            this.panel1.Controls.Add(this.Storagepostxt);
            this.panel1.Controls.Add(this.DeviceModeltxt);
            this.panel1.Controls.Add(this.DeviceNametxt);
            this.panel1.Controls.Add(this.DeviceFAtxt);
            this.panel1.Controls.Add(this.Faultreportlbl);
            this.panel1.Controls.Add(this.Storageposilbl);
            this.panel1.Controls.Add(this.DeviceModellbl);
            this.panel1.Controls.Add(this.DeviceNamelb);
            this.panel1.Controls.Add(this.DeviceFAlbl);
            this.panel1.Location = new System.Drawing.Point(1, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(663, 663);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LightCyan;
            this.panel3.Controls.Add(this.Pressbtn);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.FA);
            this.panel3.Location = new System.Drawing.Point(3, 71);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(657, 44);
            this.panel3.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(73, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 19);
            this.label2.TabIndex = 13;
            this.label2.Text = "Enter FA Number";
            // 
            // FA
            // 
            this.autocompleteMenu1.SetAutocompleteMenu(this.FA, null);
            this.FA.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FA.Location = new System.Drawing.Point(328, 14);
            this.FA.Name = "FA";
            this.FA.Size = new System.Drawing.Size(227, 24);
            this.FA.TabIndex = 14;
            this.FA.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FA_KeyDown);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Info;
            this.panel2.Controls.Add(this.Escalatelb1);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(657, 70);
            this.panel2.TabIndex = 16;
            // 
            // Escalatelb1
            // 
            this.Escalatelb1.AutoSize = true;
            this.Escalatelb1.Font = new System.Drawing.Font("Rockwell", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Escalatelb1.ForeColor = System.Drawing.Color.Indigo;
            this.Escalatelb1.Location = new System.Drawing.Point(210, 19);
            this.Escalatelb1.Name = "Escalatelb1";
            this.Escalatelb1.Size = new System.Drawing.Size(256, 33);
            this.Escalatelb1.TabIndex = 0;
            this.Escalatelb1.Text = "Escalate the Issue";
            // 
            // Submitbtn
            // 
            this.Submitbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Submitbtn.ForeColor = System.Drawing.Color.Green;
            this.Submitbtn.Location = new System.Drawing.Point(289, 629);
            this.Submitbtn.Name = "Submitbtn";
            this.Submitbtn.Size = new System.Drawing.Size(89, 31);
            this.Submitbtn.TabIndex = 15;
            this.Submitbtn.Text = "Submit";
            this.Submitbtn.UseVisualStyleBackColor = true;
            this.Submitbtn.Click += new System.EventHandler(this.Submitbtn_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "General Faults",
            "Oil Leakage",
            "Noise Issue",
            "Lubrication Issues",
            "Quality-Related Faults",
            "Tooling and Fixture Faults",
            "Electrical and Controls Faults",
            "Material-related Faults",
            "Maintenance-Related Issues"});
            this.comboBox1.Location = new System.Drawing.Point(331, 543);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(227, 26);
            this.comboBox1.TabIndex = 12;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(76, 543);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(252, 19);
            this.label1.TabIndex = 11;
            this.label1.Text = "Select Fault Reporting Content";
            // 
            // Faultreporttxt
            // 
            this.autocompleteMenu1.SetAutocompleteMenu(this.Faultreporttxt, null);
            this.Faultreporttxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Faultreporttxt.Location = new System.Drawing.Point(331, 389);
            this.Faultreporttxt.Name = "Faultreporttxt";
            this.Faultreporttxt.Size = new System.Drawing.Size(227, 24);
            this.Faultreporttxt.TabIndex = 10;
            // 
            // Storagepostxt
            // 
            this.autocompleteMenu1.SetAutocompleteMenu(this.Storagepostxt, null);
            this.Storagepostxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Storagepostxt.Location = new System.Drawing.Point(331, 325);
            this.Storagepostxt.Name = "Storagepostxt";
            this.Storagepostxt.Size = new System.Drawing.Size(227, 24);
            this.Storagepostxt.TabIndex = 9;
            // 
            // DeviceModeltxt
            // 
            this.autocompleteMenu1.SetAutocompleteMenu(this.DeviceModeltxt, null);
            this.DeviceModeltxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeviceModeltxt.Location = new System.Drawing.Point(331, 268);
            this.DeviceModeltxt.Name = "DeviceModeltxt";
            this.DeviceModeltxt.Size = new System.Drawing.Size(227, 24);
            this.DeviceModeltxt.TabIndex = 8;
            // 
            // DeviceNametxt
            // 
            this.autocompleteMenu1.SetAutocompleteMenu(this.DeviceNametxt, null);
            this.DeviceNametxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeviceNametxt.Location = new System.Drawing.Point(331, 206);
            this.DeviceNametxt.Name = "DeviceNametxt";
            this.DeviceNametxt.Size = new System.Drawing.Size(227, 24);
            this.DeviceNametxt.TabIndex = 7;
            // 
            // DeviceFAtxt
            // 
            this.autocompleteMenu1.SetAutocompleteMenu(this.DeviceFAtxt, null);
            this.DeviceFAtxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeviceFAtxt.Location = new System.Drawing.Point(331, 157);
            this.DeviceFAtxt.Name = "DeviceFAtxt";
            this.DeviceFAtxt.Size = new System.Drawing.Size(227, 24);
            this.DeviceFAtxt.TabIndex = 6;
            // 
            // Faultreportlbl
            // 
            this.Faultreportlbl.AutoSize = true;
            this.Faultreportlbl.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Faultreportlbl.Location = new System.Drawing.Point(76, 392);
            this.Faultreportlbl.Name = "Faultreportlbl";
            this.Faultreportlbl.Size = new System.Drawing.Size(191, 19);
            this.Faultreportlbl.TabIndex = 5;
            this.Faultreportlbl.Text = "Fault Reporting Person";
            // 
            // Storageposilbl
            // 
            this.Storageposilbl.AutoSize = true;
            this.Storageposilbl.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Storageposilbl.Location = new System.Drawing.Point(76, 330);
            this.Storageposilbl.Name = "Storageposilbl";
            this.Storageposilbl.Size = new System.Drawing.Size(136, 19);
            this.Storageposilbl.TabIndex = 4;
            this.Storageposilbl.Text = "Device Location";
            // 
            // DeviceModellbl
            // 
            this.DeviceModellbl.AutoSize = true;
            this.DeviceModellbl.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeviceModellbl.Location = new System.Drawing.Point(76, 273);
            this.DeviceModellbl.Name = "DeviceModellbl";
            this.DeviceModellbl.Size = new System.Drawing.Size(119, 19);
            this.DeviceModellbl.TabIndex = 3;
            this.DeviceModellbl.Text = "Device Model";
            // 
            // DeviceNamelb
            // 
            this.DeviceNamelb.AutoSize = true;
            this.DeviceNamelb.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeviceNamelb.Location = new System.Drawing.Point(76, 209);
            this.DeviceNamelb.Name = "DeviceNamelb";
            this.DeviceNamelb.Size = new System.Drawing.Size(115, 19);
            this.DeviceNamelb.TabIndex = 2;
            this.DeviceNamelb.Text = "Device Name";
            // 
            // DeviceFAlbl
            // 
            this.DeviceFAlbl.AutoSize = true;
            this.DeviceFAlbl.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeviceFAlbl.Location = new System.Drawing.Point(76, 157);
            this.DeviceFAlbl.Name = "DeviceFAlbl";
            this.DeviceFAlbl.Size = new System.Drawing.Size(158, 19);
            this.DeviceFAlbl.TabIndex = 1;
            this.DeviceFAlbl.Text = "Device FA Number";
            // 
            // autocompleteMenu1
            // 
            this.autocompleteMenu1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.autocompleteMenu1.ImageList = null;
            this.autocompleteMenu1.Items = new string[0];
            this.autocompleteMenu1.TargetControlWrapper = null;
            // 
            // Whometolbl
            // 
            this.Whometolbl.AutoSize = true;
            this.Whometolbl.BackColor = System.Drawing.Color.White;
            this.Whometolbl.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Whometolbl.ForeColor = System.Drawing.SystemColors.Highlight;
            this.Whometolbl.Location = new System.Drawing.Point(19, 457);
            this.Whometolbl.Name = "Whometolbl";
            this.Whometolbl.Size = new System.Drawing.Size(255, 19);
            this.Whometolbl.TabIndex = 18;
            this.Whometolbl.Text = "Who Should know the Escaltion";
            // 
            // Escalationtext
            // 
            this.autocompleteMenu1.SetAutocompleteMenu(this.Escalationtext, null);
            this.Escalationtext.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Escalationtext.Location = new System.Drawing.Point(331, 444);
            this.Escalationtext.Multiline = true;
            this.Escalationtext.Name = "Escalationtext";
            this.Escalationtext.Size = new System.Drawing.Size(227, 63);
            this.Escalationtext.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label3.Location = new System.Drawing.Point(19, 476);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(309, 19);
            this.label3.TabIndex = 20;
            this.label3.Text = " Enter Barcodes separate with Comma";
            // 
            // Pressbtn
            // 
            this.Pressbtn.BackColor = System.Drawing.Color.CadetBlue;
            this.Pressbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Pressbtn.ForeColor = System.Drawing.Color.Red;
            this.Pressbtn.Location = new System.Drawing.Point(561, 13);
            this.Pressbtn.Name = "Pressbtn";
            this.Pressbtn.Size = new System.Drawing.Size(75, 25);
            this.Pressbtn.TabIndex = 15;
            this.Pressbtn.Text = "Press";
            this.Pressbtn.UseVisualStyleBackColor = false;
            this.Pressbtn.Click += new System.EventHandler(this.Pressbtn_Click);
            // 
            // Escalate_Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 663);
            this.Controls.Add(this.panel1);
            this.Name = "Escalate_Report";
            this.Text = "Escalate_Report";
            this.Load += new System.EventHandler(this.Escalate_Report_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox FA;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Faultreporttxt;
        private System.Windows.Forms.TextBox Storagepostxt;
        private System.Windows.Forms.TextBox DeviceModeltxt;
        private System.Windows.Forms.TextBox DeviceNametxt;
        private System.Windows.Forms.TextBox DeviceFAtxt;
        private System.Windows.Forms.Label Faultreportlbl;
        private System.Windows.Forms.Label Storageposilbl;
        private System.Windows.Forms.Label DeviceModellbl;
        private System.Windows.Forms.Label DeviceNamelb;
        private System.Windows.Forms.Label DeviceFAlbl;
        private System.Windows.Forms.Label Escalatelb1;
        private System.Windows.Forms.Button Submitbtn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private AutocompleteMenuNS.AutocompleteMenu autocompleteMenu1;
        private System.Windows.Forms.Label Whometolbl;
        private System.Windows.Forms.TextBox Escalationtext;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Pressbtn;
    }
}

