using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TierAudits_New
{
    public partial class LoginSixS : Form
    {
        static int attempt = 2;
        public LoginSixS()
        {
            InitializeComponent();
            timer1.Start();
            MaximizeBox = false;
            textBox2.PasswordChar = '*';
        }

        async void DisableButtons(int seconds)
        {
            await Task.Delay(1000 * seconds);

        }

        private void eyeIcon_Click(object sender, EventArgs e)
        {
            // If the PasswordChar is '*', then reveal the password.
            if (textBox2.PasswordChar == '*')
            {
                textBox2.PasswordChar = '\0'; // Null character to reveal the password.
            }
            else
            {
                // If the PasswordChar is not '*', then mask the password.
                textBox2.PasswordChar = '*';
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string username = textBox1.Text;
            string password = textBox2.Text;
            
            

            if ((this.textBox1.Text == "Admin") && (this.textBox2.Text == "admin"))
            {
                attempt = 0;
                //pictureBox1.Image = new Bitmap(@"E:\MES_500\KZ_MES\mano\1u.png‪");
                MessageBox.Show("you are granted with access");

                //Manoj f1 = new Manoj();
                //f1.Show();

                this.Close();

                PlantSixS newForm = new PlantSixS(); 
                newForm.Show();



            }


            else if (attempt > 0)
            {
                //pictureBox1.Image = new Bitmap(@"E:\MES_500\KZ_MES\mano\2u.png");
                label4.Text = ("You Have Only " + Convert.ToString(attempt) + " Attempt Left To Try");
                --attempt;
            }

            else if (this.textBox1.Text == null) 
            {
                label5.Text = ("user Id should not be empty");

            }

            else if  (this.textBox2.Text == null)
            {
                //MessageBox.Show("Password should not be empty.");
                label6.Text = ("Password should not be empty");
            }

            else if ((this.textBox1.Text == null) && (this.textBox2.Text == null))
            {
                //MessageBox.Show("Please enter user id & password");
                label5.Text = ("user Id should not be empty");
                label6.Text = ("Password should not be empty");
            }

            else
            {

                //pictureBox1.Image = new Bitmap(@"H:\Tier audits final code\TierAudits_New\TierAudits_New\Resources\user (1).png");
                MessageBox.Show("you are not granted with access");


                button1.Hide();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            this.time_lbl.Text = dateTime.ToString("hh:mm");
            this.label7.Text = dateTime.ToString(":ss");
            this.label8.Text = dateTime.ToString("tt");
        }

        private void time_lbl_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            this.time_lbl.Text = dateTime.ToString("hh:mm");
            this.label7.Text = dateTime.ToString(":ss");
            this.label8.Text = dateTime.ToString("tt");
        }
    }
}
