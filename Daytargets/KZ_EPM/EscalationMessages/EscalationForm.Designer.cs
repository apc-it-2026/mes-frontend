
namespace EscalationMessages
{
    partial class EscalationForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtsubject = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtbody = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.userListtxt = new System.Windows.Forms.TextBox();
            this.Requstclick = new System.Windows.Forms.Button();
            this.Responcetxt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Trajan Pro", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.DarkViolet;
            this.label1.Location = new System.Drawing.Point(276, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter Title";
            // 
            // txtsubject
            // 
            this.txtsubject.Location = new System.Drawing.Point(436, 117);
            this.txtsubject.Name = "txtsubject";
            this.txtsubject.Size = new System.Drawing.Size(197, 23);
            this.txtsubject.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Trajan Pro", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.DarkViolet;
            this.label2.Location = new System.Drawing.Point(276, 178);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Enter Body";
            // 
            // txtbody
            // 
            this.txtbody.Location = new System.Drawing.Point(436, 178);
            this.txtbody.Name = "txtbody";
            this.txtbody.Size = new System.Drawing.Size(197, 23);
            this.txtbody.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Trajan Pro", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.DarkViolet;
            this.label3.Location = new System.Drawing.Point(276, 239);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 19);
            this.label3.TabIndex = 4;
            this.label3.Text = "Enter userList";
            // 
            // userListtxt
            // 
            this.userListtxt.Location = new System.Drawing.Point(436, 239);
            this.userListtxt.Name = "userListtxt";
            this.userListtxt.Size = new System.Drawing.Size(197, 23);
            this.userListtxt.TabIndex = 5;
            // 
            // Requstclick
            // 
            this.Requstclick.Font = new System.Drawing.Font("Trajan Pro", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Requstclick.ForeColor = System.Drawing.Color.Cyan;
            this.Requstclick.Location = new System.Drawing.Point(436, 388);
            this.Requstclick.Name = "Requstclick";
            this.Requstclick.Size = new System.Drawing.Size(106, 33);
            this.Requstclick.TabIndex = 6;
            this.Requstclick.Text = "Submit";
            this.Requstclick.UseVisualStyleBackColor = true;
            this.Requstclick.Click += new System.EventHandler(this.Requstclick_Click);
            // 
            // Responcetxt
            // 
            this.Responcetxt.Location = new System.Drawing.Point(436, 309);
            this.Responcetxt.Name = "Responcetxt";
            this.Responcetxt.Size = new System.Drawing.Size(197, 23);
            this.Responcetxt.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Trajan Pro", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.Color.Crimson;
            this.label4.Location = new System.Drawing.Point(276, 309);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 19);
            this.label4.TabIndex = 8;
            this.label4.Text = "Responce";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.MintCream;
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(933, 79);
            this.panel1.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label5.Font = new System.Drawing.Font("Trajan Pro", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(332, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(288, 36);
            this.label5.TabIndex = 0;
            this.label5.Text = "Escalation Form";
            // 
            // EscalationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(941, 539);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Responcetxt);
            this.Controls.Add(this.Requstclick);
            this.Controls.Add(this.userListtxt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtbody);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtsubject);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.DarkViolet;
            this.Name = "EscalationForm";
            this.Text = "EscalationForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtsubject;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtbody;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox userListtxt;
        private System.Windows.Forms.Button Requstclick;
        private System.Windows.Forms.TextBox Responcetxt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
    }
}

