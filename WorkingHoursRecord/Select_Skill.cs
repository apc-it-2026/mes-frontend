using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms; 


namespace WorkingHoursRecord
{
    public partial class Select_Skill : Form
    {
        private FlowLayoutPanel flowLayoutPanel;
        string Barcode;
        string ProdLine;
        string ModelName;
        public string Result { get; private set; }
        public Select_Skill(string barcode,string prodline,string modelname)
        {
             Barcode = barcode;
             ProdLine = prodline;
             ModelName = modelname;
            InitializeComponent();
            flowLayoutPanel = new FlowLayoutPanel
            {
                Location = new Point(10, 10),
                Size = new Size(816, 489),
            };

            this.Controls.Add(flowLayoutPanel);
            LoadDataAndCreateButtons();

            this.ClientSize = new Size(816, 489); // Set form size
            this.StartPosition = FormStartPosition.CenterParent; // Center relative to parent
            this.FormBorderStyle = FormBorderStyle.FixedDialog; // Optional: fixed dialog style
            this.MaximizeBox = false; // Disable maximize button
            this.MinimizeBox = false; // Disable minimize button

        }
        private void LoadDataAndCreateButtons()
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
                    ForeColor = Color.Black, 
                    Font = new Font("Times New Roman", 16, FontStyle.Bold), 
                    Size = new Size(150, 50) 

                };
                button.Click += (sender, e) =>
                {
                   int status = SaveWorkingSkill(Barcode, ProdLine,buttonText, ModelName);
                    if(status==2)
                    {
                        Result = status.ToString(); 
                        this.Close(); 
                    }
                    else if (status == 1)
                    {
                        Result = status.ToString();
                        this.Close();
                    }
                    else
                    {
                        this.Close();
                    }
                };
                flowLayoutPanel.Controls.Add(button);
            }

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

        public int SaveWorkingSkill(string Barcode, string ProdLine, string SkillName,string ModelName)
        {
            int status = 0;  
            DataTable dt = new DataTable();
            try
            {
                Dictionary<string, object> retData = new Dictionary<string, object>();
                retData.Add("Barcode", Barcode);
                retData.Add("ProdLine", ProdLine);
                retData.Add("SkillName", SkillName); 
                retData.Add("ModelName", ModelName); 
                string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder",
                                            "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer",
                                            "SaveWorkingSkill",
                    Program.client.UserToken,
                    Newtonsoft.Json.JsonConvert.SerializeObject(retData)
                );
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (ret.IsSuccess)
                {
                    if(ret.RetData=="2")
                    {
                        status = 2;  //WorkingSkill not inserted
                    }
                    else
                    {
                        status = 1;  //WorkingSkill inserted
                        SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Added Successfully");
                    }
                    
                    
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
    }
}
