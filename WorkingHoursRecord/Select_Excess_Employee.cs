using Newtonsoft.Json;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms; 


namespace WorkingHoursRecord
{
    public partial class Select_Excess_Employee : Form
    {
        private FlowLayoutPanel flowLayoutPanel1;
        string ProdLine;
        string Barcode;
        public string Result { get; private set; }
        public Select_Excess_Employee(string barcode, string prodline)
        {
             ProdLine = prodline;
             Barcode = barcode;
            InitializeComponent(); 
            txtdept.Text = ProdLine;
            txtbcode.Text = Barcode;
            flowLayoutPanel1 = new FlowLayoutPanel
            {
                Location = new Point(200, 250),
                Size = new Size(600, 389),
            };

            this.Controls.Add(flowLayoutPanel1);
           

            this.ClientSize = new Size(816, 489); // Set form size
            this.StartPosition = FormStartPosition.CenterParent; // Center relative to parent
            this.FormBorderStyle = FormBorderStyle.FixedDialog; // Optional: fixed dialog style
            this.MaximizeBox = false; // Disable maximize button
            this.MinimizeBox = false; // Disable minimize button
            txtuser.Focus();
        }
        private void LoadDataAndCreateButtons(string Barcode)
        {

            DataTable dataTable = GetSkillsList1(Barcode);

            foreach (DataRow row in dataTable.Rows)
            {
                string buttonText = row["skill_name"].ToString();
                Button button = new Button
                {
                    Text = buttonText,
                    AutoSize = true,
                    BackColor = Color.Aqua,
                    ForeColor = Color.White, 
                    Font = new Font("Times New Roman", 16, FontStyle.Bold), 
                    Size = new Size(150, 50) 

                };
                button.Click += (sender, e) =>
                {
                    if(string.IsNullOrEmpty(txtuser.Text))
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
                   int status = SaveExcessWorkingSkill(Barcode, ProdLine,buttonText, txtuser.Text);
                    if(status==2)
                    {
                        Result = status.ToString(); 
                        this.Close(); 
                    }
                    else if(status == 1)
                    {
                        Result = status.ToString();
                        this.Close();
                    }
                    else
                    {
                        this.Close();
                    }
                };
                flowLayoutPanel1.Controls.Add(button);
            }
            txtuser.Focus();
        }
        public DataTable GetSkillsList1(string Barcode)
        {
            DataTable dt = new DataTable();
            try
            {
                Dictionary<string, object> retData = new Dictionary<string, object>();
                retData.Add("Barcode", Barcode);
                string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder",
                                            "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer",
                                            "GetSkillsList",
                    Program.client.UserToken,
                    Newtonsoft.Json.JsonConvert.SerializeObject(retData)
                );
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (ret.IsSuccess)
                {
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());

                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                }
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
            return dt;
        }

        public int SaveExcessWorkingSkill(string Barcode, string ProdLine, string SkillName,string Approver)
        {
            int status = 0;
            DataTable dt = new DataTable();
            try
            {
                Dictionary<string, object> retData = new Dictionary<string, object>();
                retData.Add("Barcode", Barcode);
                retData.Add("ProdLine", ProdLine);
                retData.Add("SkillName", SkillName); 
                retData.Add("Approver", Approver); 
                string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder",
                                            "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer",
                                            "SaveExcessWorkingSkill",
                    Program.client.UserToken,
                    Newtonsoft.Json.JsonConvert.SerializeObject(retData)
                );
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (ret.IsSuccess)
                {
                   status= Convert.ToInt32(ret.RetData);
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                }
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }

            return status;
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
            LoadDataAndCreateButtons(Barcode);
            
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
    }
}
