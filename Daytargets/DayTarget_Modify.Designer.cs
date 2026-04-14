namespace DayTargets
{
    partial class DayTargetModifyForm
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
            this.textDept = new System.Windows.Forms.TextBox();
            this.textWorkDay = new System.Windows.Forms.TextBox();
            this.textWorkQty = new System.Windows.Forms.TextBox();
            this.textNote = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("宋体", 12F);
            this.label1.Location = new System.Drawing.Point(12, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "生产部门：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("宋体", 12F);
            this.label2.Location = new System.Drawing.Point(12, 192);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(173, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "生产时间：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("宋体", 12F);
            this.label3.Location = new System.Drawing.Point(316, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(174, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "备    注：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("宋体", 12F);
            this.label4.Location = new System.Drawing.Point(313, 193);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(177, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "当日目标：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textDept
            // 
            this.textDept.BackColor = System.Drawing.Color.LightGray;
            this.textDept.Font = new System.Drawing.Font("宋体", 13F);
            this.textDept.Location = new System.Drawing.Point(187, 113);
            this.textDept.Name = "textDept";
            this.textDept.ReadOnly = true;
            this.textDept.Size = new System.Drawing.Size(111, 27);
            this.textDept.TabIndex = 3;
            // 
            // textWorkDay
            // 
            this.textWorkDay.BackColor = System.Drawing.Color.LightGray;
            this.textWorkDay.Font = new System.Drawing.Font("宋体", 13F);
            this.textWorkDay.Location = new System.Drawing.Point(187, 187);
            this.textWorkDay.Name = "textWorkDay";
            this.textWorkDay.ReadOnly = true;
            this.textWorkDay.Size = new System.Drawing.Size(111, 27);
            this.textWorkDay.TabIndex = 3;
            // 
            // textWorkQty
            // 
            this.textWorkQty.Font = new System.Drawing.Font("宋体", 13F);
            this.textWorkQty.Location = new System.Drawing.Point(492, 188);
            this.textWorkQty.Name = "textWorkQty";
            this.textWorkQty.Size = new System.Drawing.Size(124, 27);
            this.textWorkQty.TabIndex = 1;
            this.textWorkQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textWorkQty_KeyPress);
            // 
            // textNote
            // 
            this.textNote.Font = new System.Drawing.Font("宋体", 13F);
            this.textNote.Location = new System.Drawing.Point(492, 113);
            this.textNote.Name = "textNote";
            this.textNote.Size = new System.Drawing.Size(267, 27);
            this.textNote.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 12F);
            this.button1.Location = new System.Drawing.Point(277, 306);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 29);
            this.button1.TabIndex = 2;
            this.button1.Text = "取消";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("宋体", 12F);
            this.button2.Location = new System.Drawing.Point(427, 306);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(83, 29);
            this.button2.TabIndex = 2;
            this.button2.Text = "提交";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // DayTargetModifyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 422);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textNote);
            this.Controls.Add(this.textWorkQty);
            this.Controls.Add(this.textWorkDay);
            this.Controls.Add(this.textDept);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Name = "DayTarget_Modify";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DayTarget_Modify";
            this.Load += new System.EventHandler(this.DayTargetModifyForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textDept;
        private System.Windows.Forms.TextBox textWorkDay;
        private System.Windows.Forms.TextBox textWorkQty;
        private System.Windows.Forms.TextBox textNote;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}