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
using System.Windows.Documents;
using System.Windows.Forms;
using static MaterialSkin.Controls.MaterialForm;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace F_EPM_Base_List
{
    public partial class devicetypeForm : MaterialForm
    {
        public devicetypeForm()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }
        DataTable dtprocess;
        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void devicetypeForm_Load(object sender, EventArgs e)
        {
            load_date();
            dataGridView1.AutoGenerateColumns = false;
            Is_administrators is_Administrators = new Is_administrators();
            bool isMangager = is_Administrators.IsMangager();
            if (isMangager)
            {
                button5.Enabled = true;
                
            }
            else
            {
                button5.Enabled = false;
                button5.BackColor = SystemColors.Control;
            }
            new ControlMoveResize(groupBox1, this);
        }
        private void load_date()
        {
            //Process
            List<AutocompleteItem> items4 = new List<AutocompleteItem>();
            string ret4 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.ProductionDashBoardServer", "LoadRoutNo", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject("noParam"));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret4)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret4)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                //dtJson.Rows.Add("全部", "", "", "");
                items4.Add(new MulticolumnAutocompleteItem(new[] { "" }, ""));

                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {
                    items4.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i - 1]["rout_no"].ToString(), dtJson.Rows[i - 1]["rout_name_z"].ToString() }, dtJson.Rows[i - 1]["rout_no"].ToString() + "|" + dtJson.Rows[i - 1]["rout_name_z"].ToString()));

                }
                process.DataSource = items4;
                checkedListBox1.DataSource = dtJson;
                checkedListBox1.DisplayMember = "rout_no";
                checkedListBox1.ValueMember = "rout_no";
                dtprocess = dtJson;
            }

            //comboBox3.DataSource = items4;

            //factory
            List<AutocompleteItem> items3 = new List<AutocompleteItem>();
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_WMSAPI", "KZ_WMSAPI.Controllers.F_WMS_Miscellaneous_Server", "LoadOrg", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject("noParam"));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(Data);
                //dtJson.Rows.Add("全部","");

                items3.Add(new MulticolumnAutocompleteItem(new[] { "" }, ""));

                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    items3.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i]["org_code"].ToString(), dtJson.Rows[i]["org_name"].ToString() }, dtJson.Rows[i]["org_code"].ToString() + "|" + dtJson.Rows[i]["org_name"].ToString()));

                }
                this.org_id.DataSource = dtJson;
                this.org_id.DisplayMember = "org_name";
                this.org_id.ValueMember = "org_code";
                comboBox1.DataSource = items3;

            }

          

        }
        private DataTable Getalldevicetype(string org_id, string process_no, string dev_no,string dev_name, string enable)
        {
            DataTable dataTable = null;
            Dictionary<string, object> p = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(org_id)) { p.Add("org_id", org_id); }
            if (!string.IsNullOrEmpty(process_no)) { p.Add("rout_no", process_no); }
            if (!string.IsNullOrEmpty(dev_no)) { p.Add("dev_no", dev_no); }
            if (!string.IsNullOrEmpty(dev_name)) { p.Add("dev_name", dev_name); }
            if (!string.IsNullOrEmpty(enable)) { p.Add("enable", enable); }
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineBasicdateServer", "GetallDeviceType", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dataTable = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(Data);

            }
            return dataTable;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dtJson = Getalldevicetype(comboBox1.Text.Split('|')[0], process.Text.Split('|')[0], textBox3.Text, textBox1.Text, comboBox2.Text);
            dataGridView1.Rows.Clear();
            for (int i = 0; i < dtJson.Rows.Count; i++)
            {
                dataGridView1.Rows.Add(1);
                dataGridView1.Rows[i].Cells["device_no"].Value = dtJson.Rows[i]["device_no"].ToString();
                dataGridView1.Rows[i].Cells["device_name"].Value = dtJson.Rows[i]["device_name"].ToString();
                dataGridView1.Rows[i].Cells["process_no"].Value = dtJson.Rows[i]["process_no"].ToString();
                dataGridView1.Rows[i].Cells["org_id"].Value = dtJson.Rows[i]["org_id"].ToString();
                dataGridView1.Rows[i].Cells["PROCESS_NAME"].Value = dtJson.Rows[i]["PROCESS_NAME"].ToString();
                ((DataGridViewComboBoxCell)dataGridView1.Rows[i].Cells["ENABLE"]).Value = (dtJson.Rows[i]["ENABLE"].ToString());
                dataGridView1.Rows[i].ReadOnly = true;
                dataGridView1.Rows[i].Cells[0].ReadOnly = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(1);
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].ReadOnly = false;
            DataGridViewCheckBoxCell check = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0] as
                DataGridViewCheckBoxCell;
            check.Value = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int a = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
              
                DataGridViewCheckBoxCell check = dataGridView1.Rows[i].Cells[0] as DataGridViewCheckBoxCell;
                if (check.Value != null)
                {
                    if ((bool)check.Value)//when selected
                    {   
                        a = 1;
                        dataGridView1.Rows[i].ReadOnly = false;
                    }
                  
                }
            }
            if (a == 1)
            {
                SJeMES_Control_Library.MessageHelper.ShowOK(this, "Editable state is turned on。");
            }
            dataGridView1.Columns["device_no"].ReadOnly = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.SelectedCells[0].RowIndex;
            if (MessageBox.Show("Are you sure to delete the device type" + (dataGridView1.Rows[index].Cells["device_no"].Value), "Hint", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }
            if (dataGridView1.Rows[index].Cells["device_no"].Value == null)
            {
                dataGridView1.Rows.RemoveAt(index);
                return;
            }
            try
            {
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("dev_no", dataGridView1.Rows[index].Cells["device_no"].Value);
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineBasicdateServer", "deleteDeviceType", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Successfully deleted！");
                    dataGridView1.Rows.RemoveAt(index);
                }
                else
                {
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString();
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, json);
                   // MessageBox.Show(json);
                }
            }
            catch (Exception ex)
            {

                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
               // MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            baocun(dataGridView1,0);
        }

        private void baocun(DataGridView dg,int index)
        {
            //save
            DataTable dt = new DataTable();
            dt.Columns.Add("device_no", typeof(string));
            dt.Columns.Add("device_name", typeof(string));
            dt.Columns.Add("process_no", typeof(string));
            dt.Columns.Add("process_name", typeof(string));
            dt.Columns.Add("org_id", typeof(string));
            dt.Columns.Add("ENABLE", typeof(string));

            for (int i = 0; i < dg.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell check = dg.Rows[i].Cells[0] as DataGridViewCheckBoxCell;
                if (check.Value != null)
                {
                    if ((bool)check.Value)//when selected
                    {

                        DataRow dr = dt.NewRow();
                        if (dataGridView1.Rows[i].Cells["device_name"].Value == null)
                        {
                            MessageBox.Show("Device type name cannot be empty！");
                            return;
                        }
                        if (dataGridView1.Rows[i].Cells["org_id"].Value == null)
                        {
                            MessageBox.Show("Factory cannot be empty！");
                            return;
                        }
                        if (dataGridView1.Rows[i].Cells["ENABLE"].Value == null)
                        {
                            MessageBox.Show("Please choose whether to enable！");
                            return;
                        }

                        dr["device_no"] = dg.Rows[i].Cells["device_no"].Value;
                        dr["device_name"] = dg.Rows[i].Cells["device_name"].Value;
                        dr["process_no"] = dg.Rows[i].Cells["process_no"].Value;
                        dr["process_name"] = dg.Rows[i].Cells["process_name"].Value;
                        dr["org_id"] = dg.Rows[i].Cells["org_id"].Value;                      
                        dr["ENABLE"] = dg.Rows[i].Cells["ENABLE"].Value;

                        dt.Rows.Add(dr);
                    }
                }
            }

            if (dt.Rows.Count > 0)
            {
                try
                {
                    Dictionary<string, object> p = new Dictionary<string, object>();
                    p.Add("datamodel", dt);
                    string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineBasicdateServer", "updateDeviceType", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                    if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                    {
                        SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Saved successfully！");
                        //dataGridView1.ReadOnly = true;
                    }
                    else
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
           for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell check = dataGridView1.Rows[i].Cells[0] as DataGridViewCheckBoxCell;
                    dataGridView1.Rows[i].ReadOnly = false;

                    check.Value = true;
                }
           
              
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell check = dataGridView1.Rows[i].Cells[0] as DataGridViewCheckBoxCell;
                check.Value = false;
                dataGridView1.Rows[i].ReadOnly = false;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.SelectedCells[0].RowIndex;
            if (MessageBox.Show("Are you sure to delete the except id number" + (dataGridView1.Rows[index].Cells[0].Value), "Hint", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }
            dataGridView1.Rows.RemoveAt(index);
        }
        //private void Dcf_setFormTextValue(string textValue)
        //{
        //    this.process.Text = textValue;
        //}
      

        //private void process_DoubleClick(object sender, EventArgs e)
        //{
        //    ProcessForm f2 = new ProcessForm(dtprocess);
        //    f2.setFormTextValue += Dcf_setFormTextValue;
        //    f2.Show();
        //}

        int selectindex;
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           bool s= dataGridView1.Rows[e.RowIndex].ReadOnly;
            if (e.ColumnIndex == 3 && (dataGridView1.Rows[e.RowIndex].ReadOnly==false))
            {
                groupBox1.Visible = true;
                //groupBox1.SendToBack();
                selectindex = e.RowIndex;
            }
        }


        private void button11_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            string str = GeCheckBoxList(this.checkedListBox1);
            str = str.TrimEnd(',');            
            dataGridView1.Rows[selectindex].Cells[3].Value = str;
            DataView dv = dtprocess.AsDataView();
            string[] s = str.Split(',');
            string name="";
            foreach (string i in s)
            {
                dv.RowFilter = "rout_no='" + i + "'";
                DataTable dt = dv.ToTable();
                name += dt.Rows[0]["rout_name_z"].ToString()+"，";
            }
            name = name.TrimEnd('，');
            dataGridView1.Rows[selectindex].Cells[4].Value = name;
        }
        public static string GeCheckBoxList(CheckedListBox cbl)
        {
            string SelectValues = "";

            for (int i = 0; i < cbl.Items.Count; i++)
            {
                if (cbl.GetItemChecked(i))
                {
                    //method one
                    cbl.SetSelected(i, true);
                    SelectValues += cbl.SelectedValue + ",";

                    ////方法二
                    //DataRowView rv = (DataRowView)cbl.Items[i];

                    //Console.WriteLine(rv.Row[1]);
                }
            }
            return SelectValues;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
        }
    }
}
