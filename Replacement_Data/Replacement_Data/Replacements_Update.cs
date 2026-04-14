using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Replacement_Data
{
    public partial class Replacements_Update : Form
    {
        public event Action<string> UpdateSuccess;
        public Replacements_Update()
        {
            InitializeComponent();
        }
        public Replacements_Update(string replaceDate, string salesOrder, string replaceWorkcentre, string responsibleDept, string textBox5Value, string textBox6Value, string Code_Replaceworkcentre, string component)
        {
            InitializeComponent();

            // Set the values in the textboxes
            Replace_Date.Text = replaceDate;
            Replace_Date.Enabled = false;
            Sales_Order.Text = salesOrder;
            Replace_Workcentre.Text = replaceWorkcentre;
            Responsible_Dept.Text = responsibleDept;
            textBox5.Text = textBox5Value;
            textBox6.Text = textBox6Value;
            textBox2.Text = Code_Replaceworkcentre;
            textBox1.Text = component;
        }
        public void cleardata()
        {
            Sales_Order.Text = "";
            Replace_Workcentre.Text = "";
            Responsible_Dept.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox2.Text = "";
            textBox1.Text = "";
            textBox3.Text = "";
            Replace_Date.Value = DateTime.Now;
            this.Close();
        }



        private void TableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
       

        private void Replacements_Update_Load(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {



            cleardata();





        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox5.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Amount.If Amount Is 0 Please Enter 0");
                return;
            }
            if (string.IsNullOrEmpty(textBox6.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Loss.If Loss Is 0 Please Enter 0");
                return;
            }
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Update Reason.");
                return;
            }
            if (string.IsNullOrEmpty(Replace_Workcentre.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Amount.If Amount Is 0 Please Enter 0");
                return;
            }
            if (string.IsNullOrEmpty(Responsible_Dept.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Amount.If Amount Is 0 Please Enter 0");
                return;
            }
            Dictionary<string, object> retData = new Dictionary<string, object>();
            retData.Add("Replace_Date", Replace_Date.Text);
            retData.Add("Sales_Order", Sales_Order.Text);
            retData.Add("Replace_Workcentre", Replace_Workcentre.Text);
            retData.Add("Responsible_Dept", Responsible_Dept.Text);
            retData.Add("Code_Replaceworkcentre", textBox2.Text);
            retData.Add("Component", textBox1.Text);
            retData.Add("Amount", textBox5.Text);
            retData.Add("Loss", textBox6.Text);
            retData.Add("Update_Reason", textBox3.Text);

            string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_PRODKPIAPI",
                     "KZ_PRODKPIAPI.Controllers.ReplacementsServer",
                     "UpdateReplacementsData",
    Program.client.UserToken,
    Newtonsoft.Json.JsonConvert.SerializeObject(retData)
    );
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["IsSuccess"]))
            {

                
                UpdateSuccess?.Invoke("Data updated successfully");
                this.Close();
               
    }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Data Updation failed");
            }

        }

        private void TextBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label8_Click(object sender, EventArgs e)
        {

        }
    }
}
