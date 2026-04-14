using AutocompleteMenuNS;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace WorkingCalendarReport
{
    public partial class WorkingCalendarForm : MaterialForm
    {
        public WorkingCalendarForm()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);
        }
        #region 全局变量
        int firstloading = 0;
        int rowIndex = -1;
        string fcid;
        DataTable factoryCalendarList;
        #endregion

        private void WorkingCalendarForm_Load(object sender, EventArgs e)
        {
            // textBox1.MaxLength = 500;
            dgv_Calendar.AutoGenerateColumns = false;
            dgv_Calendar.EnableHeadersVisualStyles = false;
            dgv_Calendar.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgv_Calendar.RowTemplate.Height = 40;
            //字体居中
            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
            headerStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgv_Calendar.ColumnHeadersDefaultCellStyle = headerStyle;
            this.dgv_Calendar.RowsDefaultCellStyle = headerStyle;
            //加载年份
            cboxYear.Text = DateTime.Now.Year.ToString();
            //加载月份
            cboxMonth.Text = DateTime.Now.Month.ToString();
            loadYear();
            loadCalendar();
            LoadDept();
            GetAllRoutNo();
            DateTime startMonth = DateTime.Parse(DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/01"); //本月月初
            DateTime endMonth = startMonth.AddMonths(1).AddDays(-1); //本月月末
            dateTimePicker1.Value = startMonth;
            dateTimePicker2.Value = endMonth;
            firstloading = 1;
            // ucBtnExt1.BtnText = "确定";
        }

        private void GetAllRoutNo()
        {
            //制程
            List<AutocompleteItem> items6 = new List<AutocompleteItem>();
            List<AutocompleteItem> items5 = new List<AutocompleteItem>();
            string ret6 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.ProductionDashBoardServer", "LoadRoutNo", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject("noParam"));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret6)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret6)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                items6.Add(new MulticolumnAutocompleteItem(new[] { "" }, ""));
                items5.Add(new MulticolumnAutocompleteItem(new[] { "" }, ""));

                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {
                    items6.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i - 1]["rout_no"].ToString(), dtJson.Rows[i - 1]["rout_name_z"].ToString() }, dtJson.Rows[i - 1]["rout_no"].ToString() + "|" + dtJson.Rows[i - 1]["rout_name_z"].ToString()));
                    items5.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i - 1]["rout_no"].ToString(), dtJson.Rows[i - 1]["rout_name_z"].ToString() }, dtJson.Rows[i - 1]["rout_no"].ToString() + "|" + dtJson.Rows[i - 1]["rout_name_z"].ToString()));

                }
            }
            comroute.DataSource = items6;
            comroute2.DataSource = items5;

            //工厂
            var items1 = new List<AutocompleteItem>();
            var items3 = new List<AutocompleteItem>();
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WMSAPI", "KZ_WMSAPI.Controllers.F_WMS_Miscellaneous_Server", "LoadOrg", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject("noParam"));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(Data);
                items1.Add(new MulticolumnAutocompleteItem(new[] { "" }, ""));
                items3.Add(new MulticolumnAutocompleteItem(new[] { "" }, ""));
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    items1.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i]["org_code"].ToString(), dtJson.Rows[i]["org_name"].ToString() }, dtJson.Rows[i]["org_code"].ToString() + "|" + dtJson.Rows[i]["org_name"].ToString()));
                    items3.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i]["org_code"].ToString(), dtJson.Rows[i]["org_name"].ToString() }, dtJson.Rows[i]["org_code"].ToString() + "|" + dtJson.Rows[i]["org_name"].ToString()));


                }
                combOrg.DataSource = items1;
                combOrg2.DataSource = items3;
            }
            //加载厂区
            List<AutocompleteItem> item1 = LoadPlant(string.Empty);
            List<AutocompleteItem> item11 = item1;
            List<AutocompleteItem> item12 = item1;
            combcq.DataSource = item11;
            combcq2.DataSource = item12;
        }
        private List<AutocompleteItem> LoadPlant(string org)
        {
            //厂区
            List<AutocompleteItem> items2 = new List<AutocompleteItem>();
            Dictionary<string, object> parm = new Dictionary<string, object>();
            parm.Add("vCompany", org);
            string ret2 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.ProductionDashBoardServer", "LoadPlant", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(parm));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                DataRow drw = dtJson.NewRow();
                drw[0] = "";
                drw[1] = "全部";
                dtJson.Rows.InsertAt(drw, 0);
                items2.Add(new MulticolumnAutocompleteItem(new[] { "" }, ""));
                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {
                    items2.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i - 1]["code"].ToString(), dtJson.Rows[i - 1]["org"].ToString() }, dtJson.Rows[i - 1]["code"].ToString() + "|" + dtJson.Rows[i - 1]["org"].ToString()));
                }
            }
            return items2;
        }
        private void LoadDept()
        {
            List<string> stringList = new List<string>();
            AutoCompleteStringCollection autoComplete1 = new AutoCompleteStringCollection();


            Dictionary<string, object> parm = new Dictionary<string, object>();
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "GetAllDepts", Program.client.UserToken, JsonConvert.SerializeObject(parm));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtdept = JsonConvert.DeserializeObject<DataTable>(json);
                if (dtdept != null && dtdept.Rows.Count > 0)
                {
                    foreach (DataRow item in dtdept.Rows)
                    {
                        stringList.Add(item["department_code"].ToString() + "|" + item["department_name"]);
                    }
                }

                autoComplete1.AddRange(stringList.ToArray());
                txtDeptNo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtDeptNo.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtDeptNo.AutoCompleteCustomSource = autoComplete1;
                txtDeptNo2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtDeptNo2.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtDeptNo2.AutoCompleteCustomSource = autoComplete1;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        public void loadYear()
        {
            cboxYear.Items.Clear();
            int year1 = DateTime.Now.Year;
            for (int i = year1 - 10; i < year1 + 10; i++)
            {
                cboxYear.Items.Add(i.ToString());

            }

        }
        public void loadCalendar()
        {
            dgv_Calendar.DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue;
            dgv_Calendar.DefaultCellStyle.SelectionForeColor = Color.White;
            DataTable dtfreeday = null;
            //获取节假日
            if (skinCheckBox1.Checked)
            {
                dtfreeday = Getfreeday(cboxYear.Text + (cboxMonth.Text.Length == 1 ? "0" + cboxMonth.Text : cboxMonth.Text));
            }
            DateTime startMonth = DateTime.Parse(cboxYear.Text + "/" + cboxMonth.Text + "/01"); //本月月初
            DateTime endMonth = startMonth.AddMonths(1).AddDays(-1); //本月月末
            //判断是周几
            int weekInt = (int)startMonth.DayOfWeek - 1;
            if (weekInt == -1)//周日
            {
                weekInt = 6;
            }
            dgv_Calendar.Rows.Clear();
            int endnum = (endMonth.Day - startMonth.Day) + 1 + weekInt;
            int rowcount = endnum % 7 == 0 ? endnum / 7 : endnum / 7 + 1;
            int colcount = 7;
            int k = 1;
            //偶数为日期，奇数为工时
            for (int i = 0; i < rowcount; i++)
            {
                dgv_Calendar.Rows.Add(2);
                dgv_Calendar.Rows[2 * i].ReadOnly = true;
                for (int j = 0; j < colcount; j++)
                {
                    //判断从第一行的第几列开始
                    if (i == 0 && j < weekInt)
                    {
                        continue;
                    }
                    dgv_Calendar.Rows[2 * i].Cells[j].Value = k;
                    dgv_Calendar.Rows[2 * i].Cells[j].Style.BackColor = Color.YellowGreen;
                    dgv_Calendar.Rows[2 * i].Cells[j].Style.ForeColor = Color.White;

                    //去除节假日
                    if (skinCheckBox1.Checked)
                    {
                        foreach (DataRow row in dtfreeday.Rows)
                        {
                            if (int.Parse(row[0].ToString().Split('-')[2]) == k)
                            {
                                dgv_Calendar.Rows[2 * i].Cells[j].Style.BackColor = Color.White;
                                dgv_Calendar.Rows[2 * i].Cells[j].Style.ForeColor = Color.Black;
                            }
                        }

                    }
                    //赋值工时
                    dgv_Calendar.Rows[2 * i + 1].Cells[j].Value = "8";
                    k++;
                    if (k > endnum - weekInt)
                    {
                        break;
                    }

                }
                //增加工时行
                dgv_Calendar.Rows[2 * i + 1].Height = 30;
                dgv_Calendar.Rows[2 * i + 1].DefaultCellStyle.BackColor = Color.LightGray;
                dgv_Calendar.Rows[2 * i + 1].DefaultCellStyle.ForeColor = Color.White;

            }
            //动态设置日期的行高和宽度
            //dgv_Calendar.Width = 50 * 7 + 3;
            dgv_Calendar.Height = 40 * (rowcount + 1) + 3 + 30 * (rowcount);

        }
        public DataTable Getfreeday(string yearAndMonth)
        {
            DataTable dt = new DataTable();
            try
            {
                Dictionary<string, object> parm = new Dictionary<string, object>();
                parm.Add("yearAndMonth", yearAndMonth);
                string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.FactoryCalendarServer", "GetfreeDay", Program.client.UserToken, JsonConvert.SerializeObject(parm));
                if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    dt = JsonConvert.DeserializeObject<DataTable>(json);

                }
                else
                {
                    MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowErr(this, ex.Message.ToString());
            }
            return dt;
        }

        private void dgv_Calendar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            int rowIndex = e.RowIndex;
            int colIndex = e.ColumnIndex;
            //dgv_Calendar.DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue;
            //dgv_Calendar.DefaultCellStyle.SelectionForeColor = Color.White;
            if (dgv_Calendar.Rows[rowIndex].Cells[colIndex].Style.BackColor == Color.YellowGreen)
            {
                dgv_Calendar.Rows[rowIndex].Cells[colIndex].Style.BackColor = Color.White;
                dgv_Calendar.Rows[rowIndex].Cells[colIndex].Style.ForeColor = Color.Black;
                dgv_Calendar.DefaultCellStyle.SelectionBackColor = Color.White;
                dgv_Calendar.DefaultCellStyle.SelectionForeColor = Color.Black;

            }
            else //if(dgv_Calendar.Rows[rowIndex].Cells[colIndex].Style.BackColor == Color.White)
            {
                if (dgv_Calendar.Rows[rowIndex].DefaultCellStyle.BackColor == Color.LightGray)
                {
                    dgv_Calendar.DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue;
                    dgv_Calendar.DefaultCellStyle.SelectionForeColor = Color.White;

                }
                else
                {
                    if (rowIndex % 2 == 0 && dgv_Calendar.Rows[rowIndex].Cells[colIndex].Value != null)
                    {
                        dgv_Calendar.Rows[rowIndex].Cells[colIndex].Style.BackColor = Color.YellowGreen;
                        dgv_Calendar.Rows[rowIndex].Cells[colIndex].Style.ForeColor = Color.White;
                        dgv_Calendar.DefaultCellStyle.SelectionBackColor = Color.YellowGreen;
                        dgv_Calendar.DefaultCellStyle.SelectionForeColor = Color.White;
                    }
                    else
                    {
                        //dgv_Calendar.Rows[rowIndex].Cells[colIndex].Style.BackColor = Color.White;
                        //dgv_Calendar.Rows[rowIndex].Cells[colIndex].Style.ForeColor = Color.Black;
                        dgv_Calendar.DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue;
                        dgv_Calendar.DefaultCellStyle.SelectionForeColor = Color.White;
                    }
                }
            }
        }

        private void cboxMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadCalendar();
        }
        private void cboxYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (firstloading == 1)
            {
                loadCalendar();
            }
        }

        private void skinButton2_Click(object sender, EventArgs e)
        {

        }
        public DataTable GetdeptList()
        {
            DataTable dt = new DataTable();
            try
            {
                Dictionary<string, object> parm = new Dictionary<string, object>();
                parm.Add("vOrgId", combOrg.Text.Split('|')[0]);
                parm.Add("vFactoryNo", combcq.Text.Split('|')[0]);
                parm.Add("vRouteNo", comroute.Text.Split('|')[0]);
                parm.Add("vDeptNo", txtDeptNo.Text.Split('|')[0]);
                string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.FactoryCalendarServer", "GetDeptInfo", Program.client.UserToken, JsonConvert.SerializeObject(parm));
                if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    dt = JsonConvert.DeserializeObject<DataTable>(json);

                }
                else
                {
                    MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowErr(this, ex.Message.ToString());
            }
            return dt;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dgv_dept.Rows.Count; i++)
            {
                dgv_dept.Rows[i].Cells[0].Value = checkBox1.Checked;
            }
        }

        private void skinButton3_Click(object sender, EventArgs e)
        {

        }
        public void SaveDate(DataTable D, DataTable C)
        {
            try
            {
                Dictionary<string, object> parm = new Dictionary<string, object>();
                parm.Add("vdtdept", D);
                parm.Add("vdtcalendar", C);
                string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.FactoryCalendarServer", "SaveFactoryCalendarDate", Program.client.UserToken, JsonConvert.SerializeObject(parm));
                if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    MessageHelper.ShowSuccess(this, "Saved Successfully！");

                }
                else
                {
                    string errmsg = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString();
                    //if(errmsg== "ORA-00001: 违反唯一约束条件 (MES00.INDEX_FACTORY_CALENDAR_LIST)") //
                    if (errmsg == "ORA-00001: unique constraint (MES00.INDEX_FACTORY_CALENDAR_LIST) violated")
                    {
                        errmsg = "The data already exists and cannot be added again!";
                    }
                    MessageHelper.ShowErr(this, errmsg);
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowErr(this, ex.Message.ToString());
            }
        }

        private void skinButton1_Click_1(object sender, EventArgs e)
        {
            string str = textBox2.Text;
            for (int i = 0; i < dgv_Calendar.Rows.Count; i++)
            {
                if (i % 2 != 0)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        if (dgv_Calendar.Rows[i - 1].Cells[j].Value != null)
                        {
                            dgv_Calendar.Rows[i].Cells[j].Value = str;
                        }

                    }

                }
            }
        }

        private void skinCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            //获取选中的日历和工时
            DataTable vdtcalendar = new DataTable();
            vdtcalendar.Columns.Add("vDate", typeof(string));
            vdtcalendar.Columns.Add("vhour", typeof(decimal));
            for (int i = 0; i < dgv_Calendar.Rows.Count; i++)
            {
                if (i % 2 == 0)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        if (dgv_Calendar.Rows[i].Cells[j].Value != null)
                        {
                            if (skinCheckBox2.Checked)
                            {
                                dgv_Calendar.Rows[i].Cells[j].Style.BackColor = Color.YellowGreen;
                                dgv_Calendar.Rows[i].Cells[j].Style.ForeColor = Color.White;
                                dgv_Calendar.DefaultCellStyle.SelectionBackColor = Color.YellowGreen;
                                dgv_Calendar.DefaultCellStyle.SelectionForeColor = Color.White;
                            }
                            else
                            {
                                dgv_Calendar.Rows[i].Cells[j].Style.BackColor = Color.White;
                                dgv_Calendar.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                                dgv_Calendar.DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue;
                                dgv_Calendar.DefaultCellStyle.SelectionForeColor = Color.White;
                            }
                        }
                    }
                }
            }
            dgv_Calendar.DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue;
            dgv_Calendar.DefaultCellStyle.SelectionForeColor = Color.White;
        }
        private void skinCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            loadCalendar();
        }
        public void Updategridview1(DataTable dt)
        {
            dataGridView1.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataGridView1.Rows.Add(1);
                dataGridView1.Rows[i].Cells["coldeptName2"].Value = dt.Rows[i]["DEPARTMENT_NAME"];
                dataGridView1.Rows[i].Cells["coldeptNo2"].Value = dt.Rows[i]["DEPT_NO"];
                dataGridView1.Rows[i].Cells["colWorkday2"].Value = DateTime.Parse(dt.Rows[i]["WORK_DAY"].ToString()).ToShortDateString();
                dataGridView1.Rows[i].Cells["colqty2"].Value = dt.Rows[i]["SCHEDULING_QTY"];
                dataGridView1.Rows[i].Cells["colallhours2"].Value = dt.Rows[i]["ALL_HOURS"];
                dataGridView1.Rows[i].Cells["colhour2"].Value = dt.Rows[i]["WORK_HOUR"];
                dataGridView1.Rows[i].Cells["colInsertuser2"].Value = dt.Rows[i]["INSERT_USER"];
                dataGridView1.Rows[i].Cells["colInsertdate2"].Value = dt.Rows[i]["INSERT_DATE"];
                dataGridView1.Rows[i].Cells["collastuser2"].Value = dt.Rows[i]["LAST_USER"];
                dataGridView1.Rows[i].Cells["collastdate2"].Value = dt.Rows[i]["LAST_DATE"];
                dataGridView1.Rows[i].Cells["colroute2"].Value = dt.Rows[i]["udf01"];
                dataGridView1.Rows[i].Cells["colcq2"].Value = dt.Rows[i]["udf05"];
                dataGridView1.Rows[i].Cells["colorg2"].Value = dt.Rows[i]["org_name"];
                dataGridView1.Rows[i].Cells["colfcid"].Value = dt.Rows[i]["fcid"];
                dataGridView1.Rows[i].ReadOnly = true;
                dataGridView1.Rows[i].Cells[0].ReadOnly = false;
            }
            checkBox2.Checked = false;
        }

        public DataTable GetdateDetail()
        {
            DataTable dt = new DataTable();
            try
            {
                Dictionary<string, object> parm = new Dictionary<string, object>();
                parm.Add("vstartDate", dateTimePicker1.Value.ToShortDateString());
                parm.Add("vendDate", dateTimePicker2.Value.ToShortDateString());
                parm.Add("vOrgId", combOrg2.Text.Split('|')[0]);
                parm.Add("vFactoryNo", combcq2.Text.Split('|')[0]);
                parm.Add("vRouteNo", comroute2.Text.Split('|')[0]);
                parm.Add("vDeptNo", txtDeptNo2.Text.Split('|')[0]);
                string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.FactoryCalendarServer", "GetfactoryCalendarInfo", Program.client.UserToken, JsonConvert.SerializeObject(parm));
                if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    dt = JsonConvert.DeserializeObject<DataTable>(json);
                    factoryCalendarList = dt;
                }
                else
                {
                    MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowErr(this, ex.Message.ToString());
            }
            return dt;
        }



        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            rowIndex = e.RowIndex;
            if (dataGridView1.Columns[e.ColumnIndex].Name.ToString() == "colqty2")
            {
                new ControlMoveResize(grpeopleqty, this);
                grpeopleqty.Visible = true;
                txtuppeopleqty.Focus();
                fcid = dataGridView1.Rows[e.RowIndex].Cells["colfcid"].Value.ToString();
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name.ToString() == "colhour2")
            {
                new ControlMoveResize(grhour, this);
                grhour.Visible = true;
                txtuphour.Focus();
                fcid = dataGridView1.Rows[e.RowIndex].Cells["colfcid"].Value.ToString();
            }
        }

        public bool updateHourByfcid(List<string> list, string hour, string peopleqty)
        {
            bool Is_true = false;
            try
            {
                Dictionary<string, object> parm = new Dictionary<string, object>();
                parm.Add("fcidlist", list);
                parm.Add("vhour", hour);
                parm.Add("vpeopleqty", peopleqty);
                string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.FactoryCalendarServer", "UpdateFactoryCalendarDate", Program.client.UserToken, JsonConvert.SerializeObject(parm));
                if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    MessageHelper.ShowSuccess(this, "Saved Successfully!");
                    //skinButton5_Click(new object(), new EventArgs());
                    Is_true = true;
                }
                else
                {
                    MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowErr(this, ex.Message.ToString());
            }
            return Is_true;
        }
        public bool deleteFactoryCalendarByfcid(List<string> list)
        {
            bool Is_true = false;
            try
            {
                Dictionary<string, object> parm = new Dictionary<string, object>();
                parm.Add("fcidlist", list);
                string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.FactoryCalendarServer", "DeleteFactoryCalendarDate", Program.client.UserToken, JsonConvert.SerializeObject(parm));
                if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    MessageHelper.ShowSuccess(this, "Saved Successfully!");
                    //skinButton5_Click(new object(), new EventArgs());
                    Is_true = true;
                }
                else
                {
                    MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowErr(this, ex.Message.ToString());
            }
            return Is_true;
        }


        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = checkBox2.Checked;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[1];
            }
        }

        private void dgv_dept_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                dgv_dept.CurrentCell = dgv_dept.Rows[e.RowIndex].Cells[1];
            }
        }

        private void dgv_Calendar_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            //int cellvalue = 0;
            //string cellContent=e.Value as string;
            //if(!string.IsNullOrEmpty(cellContent) )
            //{
            //    int.TryParse(cellContent, out cellvalue);
            //}
            //e.Value = cellvalue;
            //e.ParsingApplied = true;
        }


        private void combOrg_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (firstloading == 0)
            {
                return;
            }
            combcq.DataSource = LoadPlant(combOrg.Text.Split('|')[0]);
        }

        private void combOrg2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (firstloading == 0)
            {
                return;
            }
            combcq2.DataSource = LoadPlant(combOrg2.Text.Split('|')[0]);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dgv_dept.Rows.Clear();
            DataTable dt = GetdeptList();
            if (dt.Rows.Count <= 0)
            {
                MessageHelper.ShowErr(this, "No data found!");
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgv_dept.Rows.Add(1);
                dgv_dept.Rows[i].Cells["colDeptNo"].Value = dt.Rows[i]["DEPARTMENT_CODE"];
                dgv_dept.Rows[i].Cells["colDeptName"].Value = dt.Rows[i]["DEPARTMENT_NAME"];
                dgv_dept.Rows[i].Cells["colbzqty"].Value = dt.Rows[i]["qty"];
                dgv_dept.Rows[i].Cells["colpbqty"].Value = dt.Rows[i]["qty"];
            }
            checkBox1.Checked = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //获取选中的日历和工时
            DataTable vdtcalendar = new DataTable();
            vdtcalendar.Columns.Add("vDate", typeof(string));
            vdtcalendar.Columns.Add("vhour", typeof(decimal));
            for (int i = 0; i < dgv_Calendar.Rows.Count; i++)
            {
                if (i % 2 == 0)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        if (dgv_Calendar.Rows[i].Cells[j].Style.BackColor == Color.YellowGreen)
                        {
                            vdtcalendar.Rows.Add(cboxYear.Text + "/" + cboxMonth.Text + "/" + dgv_Calendar.Rows[i].Cells[j].Value.ToString(), dgv_Calendar.Rows[i + 1].Cells[j].Value.ToString());
                        }
                    }
                }
            }
            //获取排班部门
            DataTable dtdept = new DataTable();
            dtdept.Columns.Add("vDeptNo", typeof(string));
            dtdept.Columns.Add("vDeptName", typeof(string));
            dtdept.Columns.Add("vqty", typeof(decimal));
            for (int i = 0; i < dgv_dept.Rows.Count; i++)
            {
                if (dgv_dept.Rows[i].Cells[0].Value != null && bool.Parse(dgv_dept.Rows[i].Cells[0].Value.ToString()))
                {
                    string deptNo = dgv_dept.Rows[i].Cells["colDeptNo"].Value.ToString();
                    string deptName = dgv_dept.Rows[i].Cells["colDeptName"].Value.ToString();
                    string qty = dgv_dept.Rows[i].Cells["colpbqty"].Value.ToString();
                    dtdept.Rows.Add(deptNo, deptName, qty);
                }
            }
            if (dtdept.Rows.Count > 0 && vdtcalendar.Rows.Count > 0)
            {
                SaveDate(dtdept, vdtcalendar);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dt = GetdateDetail();
            if (dt.Rows.Count <= 0)
            {
                MessageHelper.ShowErr(this, "no data!");
            }
            Updategridview1(dt);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value != null && bool.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString()))
                {
                    list.Add(dataGridView1.Rows[i].Cells["colfcid"].Value.ToString());
                }
            }
            if (list.Count <= 0)
            {
                MessageHelper.ShowErr(this, "No row selected!");
                return;
            }
            if (MessageBox.Show("You sure you want to delete it?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            if (deleteFactoryCalendarByfcid(list))
            {
                DataTable dt = GetdateDetail();
                Updategridview1(dt);

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            NewExportExcels.ExportExcels.Export("Factory Calendar Report", dataGridView1);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            combcq2.Text = string.Empty;
            combOrg2.Text = string.Empty;
            comroute2.Text = string.Empty;
            txtDeptNo2.Text = string.Empty;
            DateTime startMonth = DateTime.Parse(DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/01"); //本月月初 //本月月初
            DateTime endMonth = startMonth.AddMonths(1).AddDays(-1); //本月月末
            dateTimePicker1.Value = startMonth;
            dateTimePicker2.Value = endMonth;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value != null && bool.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString()))
                {
                    list.Add(dataGridView1.Rows[i].Cells["colfcid"].Value.ToString());
                }
            }
            if (list.Count <= 0)
            {
                MessageHelper.ShowErr(this, "No row selected!");
                return;
            }
            if (updateHourByfcid(list, ucTextBoxEx3.InputText, string.Empty))
            {
                grhour.Visible = false;
                //dataGridView1.Rows[rowIndex].Cells["colhour2"].Value = txtuphour.InputText;
                button2_Click(new object(), new EventArgs());
                ucTextBoxEx3.InputText = "";

            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            grhour.Visible = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            list.Add(fcid);
            if (updateHourByfcid(list, txtuphour.InputText, string.Empty))
            {
                grhour.Visible = false;
                DataTable dt = GetdateDetail();
                Updategridview1(dt);
                txtuphour.InputText = "";
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            grpeopleqty.Visible = false;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            list.Add(fcid);
            if (updateHourByfcid(list, string.Empty, txtuppeopleqty.InputText))
            {
                grpeopleqty.Visible = false;
                DataTable dt = GetdateDetail();
                Updategridview1(dt);
                txtuppeopleqty.InputText = "";
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            combOrg2.Text = combOrg.Text;
            txtDeptNo2.Text = txtDeptNo.Text;
            comroute2.Text = comroute.Text;
            combcq2.Text = combcq.Text;

            DateTime startMonth = DateTime.Parse(cboxYear.Text + "/" + cboxMonth.Text + "/01"); //本月月初
            DateTime endMonth = startMonth.AddMonths(1).AddDays(-1); //本月月末
            dateTimePicker1.Value = startMonth;
            dateTimePicker2.Value = endMonth;

            DataTable dt = GetdateDetail();
            if (dt.Rows.Count <= 0)
            {
                MessageHelper.ShowErr(this, "no data!");
            }
            Updategridview1(dt);
            tabControl1.SelectedTab = tabPage2;
        }
    }
}
