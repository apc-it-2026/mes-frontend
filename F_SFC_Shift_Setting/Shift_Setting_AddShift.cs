using AutocompleteMenuNS;
using F_SFC_TrackIn_List;
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
using static F_SFC_TrackIn_List.OrgSelectForm;

namespace F_SFC_Shift_Setting
{
    public partial class Shift_Setting_AddShift : MaterialForm
    {
        string d_Org = "";
        public Shift_Setting_AddShift()
        {
            InitializeComponent();

            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);

        }

        private void Shift_SettingAddForm_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OrgSelectForm frm = new OrgSelectForm();
            frm.DataChange += new OrgSelectForm.DataChangeHandler(DataChanged);
            frm.ShowDialog();
        }
        public void DataChanged(object sender, DataChangeEventArgs args)
        {
            d_Org = args.code;
            txtOrg_id.Text = d_Org;
            txtOrg_name.Text = args.name;          
        }

        public void DataChanged2(object sender, DataChangeEventArgs args)
        {
            d_Org = args.code;
            txtPlant_id.Text = d_Org;
            txtPlant_name.Text = args.name;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PlantSelectForm frm = new PlantSelectForm();
            frm.DataChange += new PlantSelectForm.DataChangeHandler(DataChanged2);
            frm.ShowDialog();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string Org_code = txtOrg_id.Text;
            string Org_name = txtOrg_name.Text;
            string Plant_code = txtPlant_id.Text;
            string Plant_name = txtPlant_name.Text;
            string Shift_code = txtShift_code.Text;
            string Shift_name = txtShift_name.Text;
            string status =comboBox2.Text;
            string def = comboBox1.Text;

            if (string.IsNullOrEmpty(Org_code))
            {
                MessageBox.Show("Please enter factory");
                return;
            }
            if (string.IsNullOrEmpty(Plant_code))
            {
                MessageBox.Show("Please enter the factory");
                return;
            }
            if (string.IsNullOrEmpty(Shift_code))
            {
                MessageBox.Show("Please enter and confirm the shift code");
                return;
            }
            if (string.IsNullOrEmpty(Shift_name))
            {
                MessageBox.Show("Please enter and confirm the shift name");
                return;
            }
            try
            {
                if (def == "Y" && QueryDef(Org_code, Plant_code, Shift_code) == true)
                {
                    MessageBox.Show("The default item has been selected, please change it first!");
                }
                else
                {
                    Dictionary<string, Object> p = new Dictionary<string, object>();
                    p.Add("vOrg_code", Org_code);
                    p.Add("vOrg_name", Org_name);
                    p.Add("vPlant_code", Plant_code);
                    p.Add("vPlant_name", Plant_name);
                    p.Add("vShift_code", Shift_code);
                    p.Add("vShift_name", Shift_name);
                    p.Add("AmFrom", tsAmFrom.getHHmm);
                    p.Add("AmTo", tsAmTo.getHHmm);
                    p.Add("PmFrom", tsPmFrom.getHHmm);
                    p.Add("PmTo", tsPmTo.getHHmm);
                    p.Add("vstatus", status);
                    p.Add("vdef", def);

                    string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.SFC_Shift_SettingServer", "Add", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                    if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                    {
                        SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Added successfully！");

                    }
                    else
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "add failed！" + Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                    }
                }

                
              
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
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

    public class DataChangeEventArgs : EventArgs
    {
        public string code { get; set; }
        public string name { get; set; }

        public DataChangeEventArgs(string s1, string s2)
        {
            code = s1;
            name = s2;
        }
    }
}
