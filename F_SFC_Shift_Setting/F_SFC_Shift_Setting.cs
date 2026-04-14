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

namespace F_SFC_Shift_Setting
{
    public partial class F_SFC_Shift_Setting : MaterialForm
    {
        public F_SFC_Shift_Setting()
        {
            InitializeComponent();

            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);

        }

        private void Shift_SettingForm_Load(object sender, EventArgs e)
        {
            LoadQueryItem();
            query();
        }
        public void LoadQueryItem()
        {
            var items1 = new List<AutocompleteItem>();//organize
            string ret1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.SFC_Shift_SettingServer", "LoadOrg", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject("noParam"));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                items1.Add(new MulticolumnAutocompleteItem(new[] { "" }, ""));
                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {
                    items1.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i - 1]["org_code"].ToString()}, dtJson.Rows[i - 1]["org_code"].ToString() ));
                }
            }
            comboBox1.DataSource = items1;

            var items2 = new List<AutocompleteItem>();//factory
            string ret2 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.SFC_Shift_SettingServer", "LoadPlant", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject("noParam"));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                items2.Add(new MulticolumnAutocompleteItem(new[] { "" }, ""));
                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {
                    items2.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i - 1]["code"].ToString()}, dtJson.Rows[i - 1]["code"].ToString() ));
                }
            }
            comboBox2.DataSource = items2;
           
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Shift_Setting_AddShift frm = new Shift_Setting_AddShift();
            frm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //int index = dataGridView1.CurrentRow.Index;
            //string Org_code = dataGridView1.Rows[index].Cells[0].Value == null ? "" : dataGridView1.Rows[index].Cells[0].Value.ToString();
            //string Org_name = dataGridView1.Rows[index].Cells[1].Value == null ? "" : dataGridView1.Rows[index].Cells[1].Value.ToString();
            //string Plant_code = dataGridView1.Rows[index].Cells[2].Value == null ? "" : dataGridView1.Rows[index].Cells[2].Value.ToString();
            //string Plant_name = dataGridView1.Rows[index].Cells[3].Value == null ? "" : dataGridView1.Rows[index].Cells[3].Value.ToString();
            Shift_Setting_AddShift frm = new Shift_Setting_AddShift();
            frm.ShowDialog();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            
            query();
            
        }
        public void query()
        {
            Font a = new Font("宋体", 15);
            dataGridView1.Font = a;//font
            dataGridView2.Font = a;
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView2.AutoGenerateColumns = false;//Get only the columns you need
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vOrg_code", comboBox1.Text);
            p.Add("vPlant_code", comboBox2.Text);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL,
                "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.SFC_Shift_SettingServer", "query",
            Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dtJson.Rows.Count > 0)
                {

                    dataGridView1.DataSource = dtJson;
                    dataGridView1.Rows[0].Selected = false;
                    dataGridView2.DataSource = dtJson;
                    dataGridView2.Rows[0].Selected = false;
                    // dataGridView2.Sort(dataGridView2.Columns[0], ListSortDirection.Ascending);//排序

                }
                else
                {
                   
                    MessageBox.Show("No such data");
                }
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    if (dtJson.Rows[i]["status"].ToString() == "Y")
                    {
                        dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.MediumAquamarine;//color management
                    }
                    else
                    {
                        //dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    }
                }
                
            }
            else
            {
                MessageBox.Show("No such data");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Edit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Edit();
        }
        public void Edit()
        {
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index > -1 && dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.CurrentRow.Index;
                string Org_code = dataGridView1.Rows[index].Cells[0].Value == null ? "" : dataGridView1.Rows[index].Cells[0].Value.ToString();
                string Plant_code = dataGridView1.Rows[index].Cells[2].Value == null ? "" : dataGridView1.Rows[index].Cells[2].Value.ToString();

                string Shift_code = dataGridView2.Rows[index].Cells[0].Value == null ? "" : dataGridView2.Rows[index].Cells[0].Value.ToString();
                string status = dataGridView2.Rows[index].Cells[6].Value == null ? "" : dataGridView2.Rows[index].Cells[6].Value.ToString();
                string def = dataGridView2.Rows[index].Cells[7].Value == null ? "" : dataGridView2.Rows[index].Cells[7].Value.ToString();

                Shift_Setting_Edit frm = new Shift_Setting_Edit(Org_code, Plant_code,  Shift_code, status, def);
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    this.btnSelect.PerformClick();//look up again 
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please select an item to edit！");
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            Del();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Del();
        }
        public void Del()
        {
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index > -1 && dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to delete this data", "hint", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    int index = dataGridView1.CurrentRow.Index;
                    string Org_code = dataGridView1.Rows[index].Cells[0].Value == null ? "" : dataGridView1.Rows[index].Cells[0].Value.ToString();
                    string Plant_code = dataGridView1.Rows[index].Cells[2].Value == null ? "" : dataGridView1.Rows[index].Cells[2].Value.ToString();
                    string Shift_code = dataGridView2.Rows[index].Cells[0].Value == null ? "" : dataGridView2.Rows[index].Cells[0].Value.ToString();

                    Dictionary<string, Object> p = new Dictionary<string, object>();

                    p.Add("data1", Org_code);
                    p.Add("data2", Plant_code);
                    p.Add("data3", Shift_code);

                    string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL,
                        "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.SFC_Shift_SettingServer", "Del",
                        Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

                    if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                    {
                        SJeMES_Control_Library.MessageHelper.ShowOK(this, "successfully deleted！");
                        this.btnSelect.PerformClick();//look up again 
                    }
                    else
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                    }
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please select an item to delete！");
            }
        }
    }
}
