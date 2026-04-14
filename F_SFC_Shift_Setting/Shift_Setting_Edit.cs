using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_SFC_Shift_Setting
{
    public partial class Shift_Setting_Edit : MaterialForm
    {
        public Shift_Setting_Edit(string Org_code,string Plant_code, string Shift_code, string status,string def)
        {
            InitializeComponent();

            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);


            textBox1.Text = Org_code;
            textBox2.Text = Plant_code;
            textBox3.Text = Shift_code;
            comboBox2.Text = status;
            comboBox1.Text = def;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string Org_code = textBox1.Text;
            string Plant_code = textBox2.Text;
            string Shift_code = textBox3.Text;
            string def = comboBox1.Text;

            DialogResult dr = MessageBox.Show("Are you sure to submit data?", "hint", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                Dictionary<string, Object> p = new Dictionary<string, object>();
                p.Add("vOrg_code", Org_code);
                p.Add("vPlant_code", Plant_code);
                p.Add("vShift_code", Shift_code);
                p.Add("vstatus", comboBox2.Text);
                p.Add("vdef", def);

                if (def == "Y"&& QueryDef(Org_code, Plant_code, Shift_code) == true)
                {
                     MessageBox.Show("Default item selected,please change first!"); 
                }
                else
                {
                    string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.SFC_Shift_SettingServer", "Edit", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                    if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                    {
                        DialogResult result = MessageBox.Show("Successfully modified！", "hint");
                        if (result == DialogResult.OK)
                        {
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    }
                    else
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                    }
                }

                
            }
        }

        public bool QueryDef(string Org_code, string Plant_code, string Shift_code)
        {
            bool isOK = false;
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vOrg_code", Org_code);
            p.Add("vPlant_code", Plant_code);
            p.Add("vShift_code", Shift_code);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.SFC_Shift_SettingServer", "QueryDef", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                isOK = Convert.ToBoolean(json);
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            return isOK;
        }
    }
}
