using MonthlyManPowerUpload;
using Newtonsoft.Json;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace MonthlyManPowerUpload
{
    public partial class Select_Excess_Employee : Form
    {
        private FlowLayoutPanel flowLayoutPanel1;
        string ProdLine;
        string Barcode;
        string SkillName;
        public string Result { get; private set; }
        public string Remarks { get; private set; }
        public string User { get; private set; }
        public Select_Excess_Employee(string barcode, string prodline,string skillname)
        {
             ProdLine = prodline;
             Barcode = barcode;
             SkillName = skillname;
            InitializeComponent();
            //SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);
            txtdept.Text = ProdLine;
            txtbcode.Text = Barcode;
            txtprocess.Text = skillname;
            flowLayoutPanel1 = new FlowLayoutPanel
            {
                Location = new Point(200, 250),
                Size = new Size(600, 389),
            };

            this.Controls.Add(flowLayoutPanel1);
           

            this.ClientSize = new Size(1201, 676); // Set form size
            this.StartPosition = FormStartPosition.CenterParent; // Center relative to parent
            this.FormBorderStyle = FormBorderStyle.FixedDialog; // Optional: fixed dialog style
            this.MaximizeBox = false; // Disable maximize button
            this.MinimizeBox = false; // Disable minimize button
            txtuser.Focus();
        }
        private void LoadDataAndCreateButtons(string Barcode)
        {

            //DataTable dataTable = GetSkillsList1(Barcode);

            //foreach (DataRow row in dataTable.Rows)
            //{
                string buttonText = SkillName;
                Button button = new Button
                {
                    Text = buttonText,
                    AutoSize = true,
                    BackColor = Color.Aqua,
                    ForeColor = Color.Black, 
                    Font = new Font("Times New Roman", 16, FontStyle.Bold), 
                    Size = new Size(150, 50) 

                };
                button.Click += (sender, e) =>
                {
                    User = txtuser.Text;
                    if (string.IsNullOrEmpty(User))
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter UserName");
                        return;
                    }
                    if(string.IsNullOrEmpty(txtpwd.Text))
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Password");
                        return;
                    }
                    if(!Checkuser(txtuser.Text, txtpwd.Text))
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "Enter Correct UserName or Password");
                        return;
                    }
                   //int status = SaveExcessWorkingSkill(Barcode, ProdLine,buttonText, txtuser.Text);
                   
                        Result = buttonText.ToString(); 
                        this.Close(); 
                   
                };
                flowLayoutPanel1.Controls.Add(button);
           // }
            txtuser.Focus();
        }
     

       

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtbcode_KeyDown(object sender, KeyEventArgs e)
        {
        }

        public bool Checkuser(string User, string Pwd)
        {
            bool isExists = false;   
            DataTable dt = new DataTable();
            try
            {
                Dictionary<string, object> retData = new Dictionary<string, object>();
                retData.Add("User", User);
                retData.Add("Pwd", Pwd); 
                string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder",
                                            "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer",
                                            "Checkuser",
                    Program.client.UserToken,
                    Newtonsoft.Json.JsonConvert.SerializeObject(retData)
                );
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (ret.IsSuccess)
                {
                    isExists = JsonConvert.DeserializeObject<bool>(ret.RetData.ToString());
                    return isExists;
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                    return isExists;
                }
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }

            return isExists;
        }

        private void Select_Excess_Employee_Load(object sender, EventArgs e)
        {
           // LoadDataAndCreateButtons(Barcode);
            
        }

        private void txtuser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtpwd.Focus();
            }
        }

        private void txtpwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtuser.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //User = txtuser.Text;
            //if (string.IsNullOrEmpty(User))
            //{
            //    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter UserName");
            //    return;
            //}
            //if (string.IsNullOrEmpty(txtpwd.Text))
            //{
            //    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Password");
            //    return;
            //}
            //if (!Checkuser(txtuser.Text, txtpwd.Text))
            //{
            //    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Enter Correct UserName or Password");
            //    return;
            //}

            if (string.IsNullOrEmpty(txtremarks.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please enter the Excess Reason");
                txtremarks.Focus();
                return;
            }
            Result = txtprocess.Text;
            Remarks = txtremarks.Text;
            this.Close();
        }
    }
}
