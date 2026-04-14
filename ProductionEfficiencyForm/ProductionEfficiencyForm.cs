using AutocompleteMenuNS;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductionEfficiencyForm
{
    public partial class ProductionEfficiencyForm : MaterialForm
    {
        public Boolean isTitle = false;
        IList<object[]> data = null;
        private ExcelProcessor _currentExcelProcessor = null;

        public ProductionEfficiencyForm()
        {
            InitializeComponent();

            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);

            var frmtype = this.GetType();
        }

        private void ProductionEfficiencyForm_Load(object sender, EventArgs e)
        {
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);
            autocompleteMenu1.Items = null;
            autocompleteMenu1.MaximumSize = new System.Drawing.Size(350, 350);
            //Departmental information reminder
            LoadDepts();
            ////初始化DataTimePicker为空
            //this.dtpWorkDay.CustomFormat = " ";
            //this.dtpWorkDay.Format = DateTimePickerFormat.Custom;
            //this.dtpWorkDay.Checked = false;
        }

        private void LoadDepts()
        {
            var columnWidth = new int[] { 50, 250 };
            DataTable dt = GetDepts();
            int n = 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                autocompleteMenu1.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dt.Rows[i]["DEPARTMENT_CODE"].ToString() + " " + dt.Rows[i]["DEPARTMENT_NAME"].ToString() }, dt.Rows[i]["DEPARTMENT_CODE"].ToString()) { ColumnWidth = columnWidth, ImageIndex = n });
                n++;
            }
        }

        private DataTable GetDepts()
        {
            DataTable dt = null;
            Dictionary<string, Object> p = new Dictionary<string, object>();
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.GeneralServer", "GetAllDepts", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            return dt;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            isTitle = true;
            this.dataGridView.AutoGenerateColumns = false;
            if (dataGridView != null)
            {
                this.dataGridView.Rows.Clear();
            }

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter = "EXCEL|*.xls*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (dataGridView1 != null)
                {
                    data = new List<object[]>();
                    this.dataGridView1.Rows.Clear();
                }
                foreach (string filename in ofd.FileNames)
                {
                    try
                    {
                        this.dataGridView1.Rows.Add(filename);
                        dataGridView1.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dataGridView_RowPostPaint);
                        this.GetExcelData(Path.GetFullPath(filename));
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message, resources.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                //dataGridView2.Name = resources.GetString("ImportData");
                dataGridView.AllowUserToAddRows = false;
                Boolean isTitle = true;
                if (data != null && data.Count > 0)
                {
                    foreach (object[] row in data)
                    {
                        if (!isTitle)
                        {
                            try
                            {
                                row[5] = Convert.ToDateTime(row[5]?.ToString()).ToShortDateString();
                            }
                            catch (Exception)
                            {
                                //MessageBox.Show(resources.GetString("DataError"), resources.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            dataGridView.Rows.Add(row);
                        }
                        else
                        {
                            isTitle = false;
                        }
                    }
                }
                dataGridView.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dataGridView_RowPostPaint);
            }
        }

        private void GetExcelData(string fileName)
        {
            try
            {
                this._currentExcelProcessor = new ExcelProcessor(fileName);
                IList<object[]> list = this._currentExcelProcessor.GetSheetData(0);
                if (data != null && data.Count > 0)
                {
                    for (int i = 1; i < list.Count; i++)
                    {
                        data.Add(list[i]);
                    }
                }
                else
                {
                    data = list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            StringFormat centerFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            Rectangle headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            btnOpen.Enabled = false;
            btnAdd.Enabled = false;
            if (this.dataGridView1.Rows.Count >= 1)
            {
                insert_db();
            }
            else
            {
                //file cannot be empty
                String message1 = SJeMES_Framework.Common.UIHelper.UImsg("err-00001", Program.client, "", Program.client.Language);
                //mistake
                String message2 = SJeMES_Framework.Common.UIHelper.UImsg("err-00003", Program.client, "", Program.client.Language);
                MessageBox.Show(message1, message2, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            btnOpen.Enabled = true;
            btnAdd.Enabled = true;
        }

        private void insert_db()
        {
            string ret = "";
            try
            {
                //Boolean isValiDate = true;
                Byte isValiDate = 1;
                isTitle = true;
                DataTable tab = new DataTable();
                tab.Columns.Add("D_DEPT");
                tab.Columns.Add("Operator");
                tab.Columns.Add("Multi_skill");
                tab.Columns.Add("All_rounder");
                tab.Columns.Add("Mobile_Worker");
                tab.Columns.Add("Work_Day");

                foreach (var o in data)
                {                 
                    if (!isTitle)
                    {
                        DateTime workDate = DateTime.Parse(o[5].ToString());
                        DateTime nowDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                        if (workDate < nowDate)
                        {
                            isValiDate = 0;
                            break;
                        }
                        if (workDate > nowDate)
                        {
                            isValiDate = 2;
                        }
                        DataRow dr = tab.NewRow();
                        dr[0] = o[0].ToString();
                        dr[1] = o[1].ToString();
                        dr[2] = o[2].ToString();
                        dr[3] = o[3].ToString();
                        dr[4] = o[4].ToString();
                        dr[5] = o[5].ToString();
                        tab.Rows.Add(dr);                       
                    }
                    else
                    {
                        isTitle = false;
                    }
                }
                switch (isValiDate)
                {
                    case 0:
                        //Date cannot be less than today
                        String message = SJeMES_Framework.Common.UIHelper.UImsg("err-WorkDay", Program.client, "", Program.client.Language);
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, message);
                        break;
                    //date equals today    
                    case 1:
                        upload(tab);
                        break; 
                    case 2:
                        //The date contains more than today. Are you sure you need to import?
                        string SureClose = SJeMES_Framework.Common.UIHelper.UImsg("SureUpload", Program.client, "", Program.client.Language);
                        string Tips = SJeMES_Framework.Common.UIHelper.UImsg("Tips", Program.client, "", Program.client.Language);
                        DialogResult dr = MessageBox.Show(SureClose, Tips, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            upload(tab);
                        }                   
                        break;
                }
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void upload(DataTable tab)
        {
            //string ret = "";
            Dictionary<string, Object> d = new Dictionary<string, object>();
            d.Add("data", tab);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.ProductionEfficiencyServer", "UpLoad", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(d));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            else
            {
                //string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                //DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                //MessageForm frm=new MessageForm(dtJson);
                //frm.ShowDialog();
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void btn_query_Click(object sender, EventArgs e)
        {
            this.dataGridView2.DataSource = null;
            this.dataGridView2.AutoGenerateColumns = false;

            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vDept", textDDept.Text.ToString().ToUpper()); 
            p.Add("vWrokDay", textBox1.Text.ToString());
            //p.Add("vWrokDay", dtpWorkDay.Text.ToString());

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.ProductionEfficiencyServer", "QueryNumber", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                //string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                //DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                //dataGridView2.DataSource = dtJson.DefaultView;
                //dataGridView2.Update();

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

        private void dtpWorkDay_ValueChanged(object sender, EventArgs e)
        {
            //this.dtpWorkDay.Format = DateTimePickerFormat.Long;
            //this.dtpWorkDay.CustomFormat = null;
            textBox1.Text = dtpWorkDay.Value.ToString("yyyy-MM-dd");
        }

        private void dtpWorkDay_DropDown(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
        }

        private void dtpWorkDay_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
