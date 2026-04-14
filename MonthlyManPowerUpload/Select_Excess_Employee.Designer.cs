
namespace MonthlyManPowerUpload
{
    partial class Select_Excess_Employee
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
            this.lbldept = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtbcode = new System.Windows.Forms.TextBox();
            this.txtdept = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelErrorMsg = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtprocess = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtremarks = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtuser = new System.Windows.Forms.TextBox();
            this.txtpwd = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(88, 34);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dept :";
            // 
            // lbldept
            // 
            this.lbldept.AutoSize = true;
            this.lbldept.Location = new System.Drawing.Point(200, 34);
            this.lbldept.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbldept.Name = "lbldept";
            this.lbldept.Size = new System.Drawing.Size(0, 19);
            this.lbldept.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(76, 89);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 19);
            this.label5.TabIndex = 4;
            this.label5.Text = "Barcode :";
            // 
            // txtbcode
            // 
            this.txtbcode.Location = new System.Drawing.Point(174, 85);
            this.txtbcode.Margin = new System.Windows.Forms.Padding(4);
            this.txtbcode.Name = "txtbcode";
            this.txtbcode.ReadOnly = true;
            this.txtbcode.Size = new System.Drawing.Size(148, 26);
            this.txtbcode.TabIndex = 5;
            this.txtbcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtbcode_KeyDown);
            // 
            // txtdept
            // 
            this.txtdept.Location = new System.Drawing.Point(174, 34);
            this.txtdept.Margin = new System.Windows.Forms.Padding(4);
            this.txtdept.Name = "txtdept";
            this.txtdept.ReadOnly = true;
            this.txtdept.Size = new System.Drawing.Size(148, 26);
            this.txtdept.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelErrorMsg);
            this.panel1.Location = new System.Drawing.Point(441, 72);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(722, 109);
            this.panel1.TabIndex = 9;
            // 
            // labelErrorMsg
            // 
            this.labelErrorMsg.AutoSize = true;
            this.labelErrorMsg.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelErrorMsg.ForeColor = System.Drawing.Color.Red;
            this.labelErrorMsg.Location = new System.Drawing.Point(6, 13);
            this.labelErrorMsg.Name = "labelErrorMsg";
            this.labelErrorMsg.Size = new System.Drawing.Size(691, 62);
            this.labelErrorMsg.TabIndex = 26;
            this.labelErrorMsg.Text = "Please mention the reason to add the employee more than \r\nME Standard";
            this.labelErrorMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(92, 290);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(123, 36);
            this.button1.TabIndex = 10;
            this.button1.Text = "Request";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtprocess
            // 
            this.txtprocess.Location = new System.Drawing.Point(174, 134);
            this.txtprocess.Margin = new System.Windows.Forms.Padding(4);
            this.txtprocess.Name = "txtprocess";
            this.txtprocess.ReadOnly = true;
            this.txtprocess.Size = new System.Drawing.Size(148, 26);
            this.txtprocess.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(76, 138);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 19);
            this.label4.TabIndex = 11;
            this.label4.Text = "Process :";
            // 
            // txtremarks
            // 
            this.txtremarks.Location = new System.Drawing.Point(176, 190);
            this.txtremarks.Margin = new System.Windows.Forms.Padding(4);
            this.txtremarks.Multiline = true;
            this.txtremarks.Name = "txtremarks";
            this.txtremarks.Size = new System.Drawing.Size(200, 67);
            this.txtremarks.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(64, 213);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 19);
            this.label6.TabIndex = 13;
            this.label6.Text = "Remarks :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(753, 363);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "User Name :";
            this.label2.Visible = false;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(760, 417);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 19);
            this.label3.TabIndex = 2;
            this.label3.Text = "PassWord :";
            this.label3.Visible = false;
            // 
            // txtuser
            // 
            this.txtuser.Location = new System.Drawing.Point(869, 363);
            this.txtuser.Margin = new System.Windows.Forms.Padding(4);
            this.txtuser.Name = "txtuser";
            this.txtuser.Size = new System.Drawing.Size(148, 26);
            this.txtuser.TabIndex = 6;
            this.txtuser.Visible = false;
            this.txtuser.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtuser_KeyDown);
            // 
            // txtpwd
            // 
            this.txtpwd.Location = new System.Drawing.Point(869, 417);
            this.txtpwd.Margin = new System.Windows.Forms.Padding(4);
            this.txtpwd.Name = "txtpwd";
            this.txtpwd.Size = new System.Drawing.Size(148, 26);
            this.txtpwd.TabIndex = 7;
            this.txtpwd.Visible = false;
            this.txtpwd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpwd_KeyDown);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label7);
            this.panel2.Location = new System.Drawing.Point(441, 200);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(722, 109);
            this.panel2.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(6, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(679, 62);
            this.label7.TabIndex = 26;
            this.label7.Text = "ME Standard కంటే ఎక్కువ మంది ఉద్యోగులను యాడ్ చేయటానికి\r\nకారణం తెలపండి";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Select_Excess_Employee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(1185, 637);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.txtremarks);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtprocess);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtdept);
            this.Controls.Add(this.txtpwd);
            this.Controls.Add(this.txtuser);
            this.Controls.Add(this.txtbcode);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lbldept);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Select_Excess_Employee";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Select_Excess_Employee";
            this.Load += new System.EventHandler(this.Select_Excess_Employee_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbldept;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtbcode;
        private System.Windows.Forms.TextBox txtdept;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelErrorMsg;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtprocess;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtremarks;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtuser;
        private System.Windows.Forms.TextBox txtpwd;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label7;
    }
}