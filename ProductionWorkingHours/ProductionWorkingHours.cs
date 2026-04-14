using AutocompleteMenuNS;
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

namespace ProductionWorkingHours
{
    public partial class ProductionWorkingHours : MaterialForm
    {
        public ProductionWorkingHours()
        {
            InitializeComponent();

            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        private void WorkingHoursForm_Load(object sender, EventArgs e)
        {

            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            //permission
            String RET = SJeMES_Framework.Common.UIHelper.UIVisiable(this, "Work calendar maintenance", Program.Client);
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView2.AutoGenerateColumns = false;
            LoadQueryItem();
        }

        private void LoadQueryItem()
        {
            var items1 = new List<AutocompleteItem>();
            string ret1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.ProductionDashBoardServer", "LoadOrg", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject("noParam"));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                items1.Add(new MulticolumnAutocompleteItem(new[] { "" }, ""));
                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {
                    items1.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i - 1]["company"].ToString() }, dtJson.Rows[i - 1]["company"].ToString()));
                }
            }
            comboBox1.DataSource = items1;
            comboBox4.DataSource = items1;

            var items2 = new List<AutocompleteItem>();
            string ret2 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.ProductionDashBoardServer", "LoadPlant", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject("noParam"));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                items2.Add(new MulticolumnAutocompleteItem(new[] { "" }, ""));
                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {
                    items2.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i - 1]["code"].ToString(), dtJson.Rows[i - 1]["org"].ToString() }, dtJson.Rows[i - 1]["code"].ToString() + "|" + dtJson.Rows[i - 1]["org"].ToString()));
                }
            }
            comboBox2.DataSource = items2;
            comboBox5.DataSource = items2;



            var items3 = new List<AutocompleteItem>();
            string ret3 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.ProductionTargetsServer", "LoadSeDept", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject("noParam"));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret3)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret3)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {
                    items3.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i - 1]["DEPARTMENT_CODE"].ToString(), dtJson.Rows[i - 1]["DEPARTMENT_NAME"].ToString() }, dtJson.Rows[i - 1]["DEPARTMENT_CODE"].ToString()));
                }
            }
            AutocompleteMenuNS.AutocompleteMenu autocompleteMenu3 = new AutocompleteMenuNS.AutocompleteMenu();
            autocompleteMenu3.MaximumSize = new Size(350, 350);
            autocompleteMenu3.SetAutocompleteMenu(textBox3, autocompleteMenu3);
            autocompleteMenu3.SetAutocompleteMenu(textBox30, autocompleteMenu3);
            autocompleteMenu3.SetAutocompleteItems(items3);


            var items4 = new List<AutocompleteItem>();
            string ret4 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.ProductionDashBoardServer", "LoadRoutNo", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject("noParam"));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret4)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret4)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                items4.Add(new MulticolumnAutocompleteItem(new[] { "" }, ""));
                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {
                    items4.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i - 1]["rout_no"].ToString(), dtJson.Rows[i - 1]["rout_name_z"].ToString() }, dtJson.Rows[i - 1]["rout_no"].ToString() + "|" + dtJson.Rows[i - 1]["rout_name_z"].ToString()));
                }
            }
            comboBox3.DataSource = items4;
            comboBox6.DataSource = items4;
        }

        private void btn_query_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = null;
            try
            {
                GetData();
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }

        private void GetData()
        {
            string vPlantNo = string.IsNullOrWhiteSpace(comboBox2.Text.ToString()) ? comboBox2.Text.ToString() : comboBox2.Text.ToString().Split('|')[0];
            string vRoutNo = string.IsNullOrWhiteSpace(comboBox3.Text.ToString()) ? comboBox3.Text.ToString() : comboBox3.Text.ToString().Split('|')[0];
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vOrgNo", comboBox1.Text.ToString());
            p.Add("vPlantNo", vPlantNo);
            p.Add("vDeptNo", textBox3.Text.ToString());
            p.Add("vRoutNo", vRoutNo);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.WorkingHoursServer", "Query", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                dataGridView1.DataSource = dtJson.DefaultView;
                dataGridView1.Update();
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }


        private void btn_save_Click(object sender, EventArgs e)
        {
            if(GetDgvToTable(dataGridView1).Rows.Count == 0)
            {
                return;
            }
            if (Vali())
            {
                Dictionary<string, Object> p = new Dictionary<string, object>();
                p.Add("data", GetDgvToTable(dataGridView1));
                p.Add("from", dateTimePicker1.Value.ToString("yyyy/MM/dd"));
                p.Add("to", dateTimePicker2.Value.ToString("yyyy/MM/dd"));
                //Monday
                p.Add("mon_am_from", textBox1.Text.ToString());
                p.Add("mon_am_to", textBox2.Text.ToString());
                p.Add("mon_pm_from", textBox4.Text.ToString());
                p.Add("mon_pm_to", textBox5.Text.ToString());
                //Tuesday
                p.Add("tue_am_from", textBox9.Text.ToString());
                p.Add("tue_am_to", textBox8.Text.ToString());
                p.Add("tue_pm_from", textBox7.Text.ToString());
                p.Add("tue_pm_to", textBox6.Text.ToString());
                //Wednesday
                p.Add("wed_am_from", textBox13.Text.ToString());
                p.Add("wed_am_to", textBox12.Text.ToString());
                p.Add("wed_pm_from", textBox11.Text.ToString());
                p.Add("wed_pm_to", textBox10.Text.ToString());
                //Thursday
                p.Add("thu_am_from", textBox17.Text.ToString());
                p.Add("thu_am_to", textBox16.Text.ToString());
                p.Add("thu_pm_from", textBox15.Text.ToString());
                p.Add("thu_pm_to", textBox14.Text.ToString());
                //Friday
                p.Add("fri_am_from", textBox21.Text.ToString());
                p.Add("fri_am_to", textBox20.Text.ToString());
                p.Add("fri_pm_from", textBox19.Text.ToString());
                p.Add("fri_pm_to", textBox18.Text.ToString());
                //Saturday
                p.Add("sat_am_from", textBox25.Text.ToString());
                p.Add("sat_am_to", textBox24.Text.ToString());
                p.Add("sat_pm_from", textBox23.Text.ToString());
                p.Add("sat_pm_to", textBox22.Text.ToString());
                //Sunday
                p.Add("sun_am_from", textBox29.Text.ToString());
                p.Add("sun_am_to", textBox28.Text.ToString());
                p.Add("sun_pm_from", textBox27.Text.ToString());
                p.Add("sun_pm_to", textBox26.Text.ToString());


                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.WorkingHoursServer", "Save", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"].ToString());
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }
        }

        private bool Vali()
        {
            //Monday am
            if (string.IsNullOrEmpty(textBox1.Text.ToString())&& !string.IsNullOrEmpty(textBox2.Text.ToString()))
            {
                 return  false;
            }
            if (!string.IsNullOrEmpty(textBox1.Text.ToString()) && string.IsNullOrEmpty(textBox2.Text.ToString()))
            {
                return false;
            }
            //Monday pm
            if (string.IsNullOrEmpty(textBox4.Text.ToString()) && !string.IsNullOrEmpty(textBox5.Text.ToString()))
            {
                return false;
            }
            if (!string.IsNullOrEmpty(textBox4.Text.ToString()) && string.IsNullOrEmpty(textBox5.Text.ToString()))
            {
                return false;
            }


            //Tuesday am
            if (string.IsNullOrEmpty(textBox9.Text.ToString()) && !string.IsNullOrEmpty(textBox8.Text.ToString()))
            {
                return false;
            }
            if (!string.IsNullOrEmpty(textBox9.Text.ToString()) && string.IsNullOrEmpty(textBox8.Text.ToString()))
            {
                return false;
            }
            //Tuesday pm
            if (string.IsNullOrEmpty(textBox7.Text.ToString()) && !string.IsNullOrEmpty(textBox6.Text.ToString()))
            {
                return false;
            }
            if (!string.IsNullOrEmpty(textBox7.Text.ToString()) && string.IsNullOrEmpty(textBox6.Text.ToString()))
            {
                return false;
            }


            //Wednesday am
            if (string.IsNullOrEmpty(textBox13.Text.ToString()) && !string.IsNullOrEmpty(textBox12.Text.ToString()))
            {
                return false;
            }
            if (!string.IsNullOrEmpty(textBox13.Text.ToString()) && string.IsNullOrEmpty(textBox12.Text.ToString()))
            {
                return false;
            }
            //Wednesday pm
            if (string.IsNullOrEmpty(textBox11.Text.ToString()) && !string.IsNullOrEmpty(textBox10.Text.ToString()))
            {
                return false;
            }
            if (!string.IsNullOrEmpty(textBox11.Text.ToString()) && string.IsNullOrEmpty(textBox10.Text.ToString()))
            {
                return false;
            }

            //Thursday am
            if (string.IsNullOrEmpty(textBox17.Text.ToString()) && !string.IsNullOrEmpty(textBox16.Text.ToString()))
            {
                return false;
            }
            if (!string.IsNullOrEmpty(textBox17.Text.ToString()) && string.IsNullOrEmpty(textBox16.Text.ToString()))
            {
                return false;
            }
            //Thursday pm
            if (string.IsNullOrEmpty(textBox15.Text.ToString()) && !string.IsNullOrEmpty(textBox14.Text.ToString()))
            {
                return false;
            }
            if (!string.IsNullOrEmpty(textBox15.Text.ToString()) && string.IsNullOrEmpty(textBox14.Text.ToString()))
            {
                return false;
            }

            //Friday am
            if (string.IsNullOrEmpty(textBox21.Text.ToString()) && !string.IsNullOrEmpty(textBox20.Text.ToString()))
            {
                return false;
            }
            if (!string.IsNullOrEmpty(textBox21.Text.ToString()) && string.IsNullOrEmpty(textBox20.Text.ToString()))
            {
                return false;
            }
            //Friday pm
            if (string.IsNullOrEmpty(textBox19.Text.ToString()) && !string.IsNullOrEmpty(textBox18.Text.ToString()))
            {
                return false;
            }
            if (!string.IsNullOrEmpty(textBox19.Text.ToString()) && string.IsNullOrEmpty(textBox18.Text.ToString()))
            {
                return false;
            }

            //Saturday am
            if (string.IsNullOrEmpty(textBox25.Text.ToString()) && !string.IsNullOrEmpty(textBox24.Text.ToString()))
            {
                return false;
            }
            if (!string.IsNullOrEmpty(textBox25.Text.ToString()) && string.IsNullOrEmpty(textBox24.Text.ToString()))
            {
                return false;
            }
            //Saturday pm
            if (string.IsNullOrEmpty(textBox23.Text.ToString()) && !string.IsNullOrEmpty(textBox22.Text.ToString()))
            {
                return false;
            }
            if (!string.IsNullOrEmpty(textBox23.Text.ToString()) && string.IsNullOrEmpty(textBox22.Text.ToString()))
            {
                return false;
            }


            //Sunday am
            if (string.IsNullOrEmpty(textBox29.Text.ToString()) && !string.IsNullOrEmpty(textBox28.Text.ToString()))
            {
                return false;
            }
            if (!string.IsNullOrEmpty(textBox29.Text.ToString()) && string.IsNullOrEmpty(textBox28.Text.ToString()))
            {
                return false;
            }
            //Sunday pm
            if (string.IsNullOrEmpty(textBox27.Text.ToString()) && !string.IsNullOrEmpty(textBox26.Text.ToString()))
            {
                return false;
            }
            if (!string.IsNullOrEmpty(textBox27.Text.ToString()) && string.IsNullOrEmpty(textBox26.Text.ToString()))
            {
                return false;
            }
            return true;
        }
        public DataTable GetDgvToTable(DataGridView dgv)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("department_no");
            dt.Columns.Add("department_name");
            // Column coercion
            for (int count = 0; count < dgv.Rows.Count; count++)
            {
                DataRow dr = dt.NewRow();
                if (dgv.Rows[count].Cells["isCheck"].Value.ToString().Equals("Y"))
                {
                    dr[0] = dgv.Rows[count].Cells[1].Value.ToString();
                    dr[1] = dgv.Rows[count].Cells[2].Value.ToString();
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
        /// <summary>
        /// select all
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            for (int count = 0; count < dataGridView1.Rows.Count; count++)
            {
                dataGridView1.Rows[count].Cells[0].Value = "Y";
            }
            dataGridView1.Update();
        }
        /// <summary>
        ///Reverse election
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            for (int count = 0; count < dataGridView1.Rows.Count; count++)
            {
                if (dataGridView1.Rows[count].Cells[0].Value.Equals("Y"))
                {
                    dataGridView1.Rows[count].Cells[0].Value = "N";
                }
                else
                {
                    dataGridView1.Rows[count].Cells[0].Value = "Y";
                }
            }
            dataGridView1.Update();
        }

        /// <summary>
        /// Copy template
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("data", GetDgvToTable(dataGridView1));
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.WorkingHoursServer", "CopyTemplates", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                p.Add("from", dateTimePicker1.Text.ToString());
                p.Add("to", dateTimePicker2.Text.ToString());
                //Monday
                textBox1.Text = "mon_am_from";
                textBox2.Text = "mon_am_to";
                textBox4.Text = "mon_pm_from";
                textBox5.Text = "mon_pm_to";
                //Tuesday
                textBox9.Text = "tue_am_from";
                textBox8.Text = "tue_am_to";
                textBox7.Text = "tue_pm_from";
                textBox6.Text = "tue_pm_to";
                //Wednesday
                textBox13.Text = "wed_am_from";
                textBox12.Text = "wed_am_to";
                textBox11.Text = "wed_pm_from";
                textBox10.Text = "wed_pm_to";
                //Thursday
                textBox17.Text = "thu_am_from";
                textBox16.Text = "thu_am_to";
                textBox15.Text = "thu_pm_from";
                textBox14.Text = "thu_pm_to";
                //Friday
                textBox21.Text = "fri_am_from";
                textBox20.Text = "fri_am_to";
                textBox19.Text = "fri_pm_from";
                textBox18.Text = "fri_pm_to";
                //Saturday
                textBox25.Text = "sat_am_from";
                textBox24.Text = "sat_am_to";
                textBox23.Text = "sat_pm_from";
                textBox22.Text = "sat_pm_to";
                //Sunday
                textBox29.Text= "sun_am_from";
                textBox28.Text = "sun_am_to";
                textBox27.Text = "sun_pm_from";
                textBox26.Text = "sun_pm_to";
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        /// <summary>
        /// Work history information query
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            this.dataGridView2.DataSource = null;
            try
            {
                GetWorkingHoursData();
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }

        private void GetWorkingHoursData()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            string vPlantNo = string.IsNullOrWhiteSpace(comboBox5.Text.ToString()) ? comboBox5.Text.ToString() : comboBox5.Text.ToString().Split('|')[0];
            string vRoutNo = string.IsNullOrWhiteSpace(comboBox6.Text.ToString()) ? comboBox6.Text.ToString() : comboBox6.Text.ToString().Split('|')[0];
            p.Add("vOrgNo", comboBox4.Text.ToString());
            p.Add("vPlantNo", vPlantNo);
            p.Add("vDeptNo", textBox30.Text.ToString());
            p.Add("vRoutNo", vRoutNo);
            p.Add("vDate", textBox31.Text.ToString());
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.WorkingHoursServer", "GetWorkingHoursData", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                dataGridView2.DataSource = dtJson.DefaultView;
                dataGridView2.Update();
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        /// <summary>
        /// call out work hours
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int index = dataGridView2.CurrentRow.Index;
            if (index > -1 && dataGridView2.Rows[index].Cells[0].Value != null)
            {
                string department_code = dataGridView2.Rows[index].Cells["department_code"].Value == null ? "" : dataGridView2.Rows[index].Cells["department_code"].Value.ToString();
                string department_name = dataGridView2.Rows[index].Cells["department_name"].Value == null ? "" : dataGridView2.Rows[index].Cells["department_name"].Value.ToString();
                string work_day = dataGridView2.Rows[index].Cells["work_day"].Value == null ? "" : dataGridView2.Rows[index].Cells["work_day"].Value.ToString();
                string rout_no = dataGridView2.Rows[index].Cells["rout_no"].Value == null ? "" : dataGridView2.Rows[index].Cells["rout_no"].Value.ToString();
                ProductionWorkingHours_TransHours frm = new ProductionWorkingHours_TransHours(department_code, department_name, work_day,rout_no);
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.Show();
            }

        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            textBox31.Text = dateTimePicker3.Value.ToString("yyyy/MM/dd");
        }

        private void dateTimePicker3_DropDown(object sender, EventArgs e)
        {
            textBox31.Text = string.Empty;
        }

        private void dateTimePicker3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
