using AutocompleteMenuNS;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SJeMES_Control_Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.CheckedListBox;

namespace F_EPM_Index_Summary
{
    public partial class Index_SummaryMainForm : MaterialForm
    {
        private Point point;
        private List<string> months = new List<string>();
        private List<string> yearMonths = new List<string>();
        private List<string> orgCode = new List<string>();
        private List<string> orgInfo = new List<string>();
        private List<string> storey = new List<string>();
        private List<string> addressCodes = new List<string>();

        private Dictionary<string, List<string>> hoildays = new Dictionary<string, List<string>>();
        private Dictionary<string, Dictionary<string, double>> dataSummary = new Dictionary<string, Dictionary<string, double>>();

        public Index_SummaryMainForm()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            dataGridView1.EnableHeadersVisualStyles = false;
        }

        private void Index_SummaryMainForm_Load(object sender, EventArgs e)
        {
            int year = DateTime.Now.Year;
            point = panel4.Location;
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, true);
                months.Add(checkedListBox1.Items[i].ToString());
                yearMonths.Add($"{year}/{int.Parse(checkedListBox1.Items[i].ToString()):00}");
            }

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy";
            dateTimePicker1.ShowUpDown = true;

            addMonthLabel();

            GetHolidayDate();
            LoadOrg();
            GetSjqdmsOrgInfo();
        }

        private void LoadOrg()
        {
            orgCode.Clear();
            //工厂
            List<AutocompleteItem> items3 = new List<AutocompleteItem>();
            List<string> stringList = new List<string>();
            AutoCompleteStringCollection autoComplete1 = new AutoCompleteStringCollection();

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_WMSAPI", "KZ_WMSAPI.Controllers.F_WMS_Miscellaneous_Server", "LoadOrg", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject("noParam"));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string Data = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(Data);
                //dtJson.Rows.Add("全部","");

                items3.Add(new MulticolumnAutocompleteItem(new[] { "", "" }, "All"));
                stringList.Add("All");
                orgCode.Add("");

                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    orgCode.Add(dtJson.Rows[i]["org_code"].ToString());
                    stringList.Add(dtJson.Rows[i]["org_name"].ToString());
                    items3.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i]["org_code"].ToString(), dtJson.Rows[i]["org_name"].ToString() }, dtJson.Rows[i]["org_name"].ToString()));
                }
                comboBox1.DataSource = items3;
                autoComplete1.AddRange(stringList.ToArray());
                comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
                comboBox1.AutoCompleteCustomSource = autoComplete1;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void GetSjqdmsOrgInfo()
        {
            orgInfo.Clear();

            List<AutocompleteItem> items3 = new List<AutocompleteItem>();
            List<string> stringList = new List<string>();
            AutoCompleteStringCollection autoComplete1 = new AutoCompleteStringCollection();

            Dictionary<string, object> p = new Dictionary<string, object>();
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineManagerServer", "GetSjqdmsOrgInfo", Program.Client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonConvert.DeserializeObject<DataTable>(Data);
                //dtJson.Rows.Add("全部","");

                items3.Add(new MulticolumnAutocompleteItem(new[] { "", "" }, "All"));
                orgInfo.Add("");
                stringList.Add("All");

                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    orgInfo.Add(dtJson.Rows[i]["code"].ToString());
                    stringList.Add(dtJson.Rows[i]["org"].ToString());
                    items3.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i]["code"].ToString(), dtJson.Rows[i]["org"].ToString() }, dtJson.Rows[i]["org"].ToString()));
                }
                comboBox2.DataSource = items3;
                autoComplete1.AddRange(stringList.ToArray());
                comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;
                comboBox2.AutoCompleteCustomSource = autoComplete1;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

        }


        private void GetHolidayDate()
        {
            int year = DateTime.Now.Year;
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("datetype", "1");
            p.Add("date_start", $"{year}/1/1");
            p.Add("date_end", $"{year}/12/31");
            p.Add("org_id", "");
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineBasicdateServer", "Get_HolidayDate", Program.Client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonConvert.DeserializeObject<DataTable>(Data);
                //dtJson.Rows.Add("全部","");
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    string orgId = dtJson.Rows[i]["org_id"].ToString();
                    string day = Convert.ToDateTime(dtJson.Rows[i]["day_code"].ToString()).ToString("yyyy/MM/dd");
                    List<string> days;
                    if (hoildays.ContainsKey(orgId))
                    {
                        days = hoildays[orgId];
                        days.Add(day);
                    }
                    else
                    {
                        days = new List<string>();
                        days.Add(day);
                        hoildays.Add(orgId, days);
                    }
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

        }

        private int counterHoildays(string orgId, string month)
        {
            int count = 0;
            if (hoildays.ContainsKey(orgId))
            {
                List<string> days = hoildays[orgId];
                foreach (string day in days)
                {
                    if (day.Contains(month))
                    {
                        count++;
                    }
                }

            }
            return count;
        }

        private int getWorkDays(string orgId, string month)
        {
            int day = 0;
            int m = Convert.ToDateTime(month).Month;
            int y = Convert.ToDateTime(month).Year;
            if (m == 1 || m == 3 || m == 5 || m == 7 || m == 8 || m == 10 || m == 12)
            {
                day = 31;
            }
            else
            {
                day = 30;
            }
            if (m == 2 && y % 4 == 0)
            {
                day = 29;
            }
            else if (m == 2)
            {
                day = 28;
            }
            day -= counterHoildays(orgId, month);

            return day;
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            if (months.Count == 0)
            {
                MessageBox.Show("Please select the month of inquiry！");
                return;
            }
            addMonthLabel();
            addHeader();

            QueryDeviceRepairTime();
        }

        private void QueryDeviceRepairTime()
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("org_code", orgCode[comboBox1.SelectedIndex]);
            p.Add("code", orgInfo[comboBox2.SelectedIndex]);
            p.Add("storey_code", storey[comboBox4.SelectedIndex]);
            p.Add("address_code", addressCodes[comboBox3.SelectedIndex]);
            p.Add("months", JArray.Parse(JsonConvert.SerializeObject(yearMonths)));
            p.Add("year", dateTimePicker1.Value.ToString("yyyy"));
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineManagerServer", "QueryDeviceRepairTime", Program.Client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonConvert.DeserializeObject<DataTable>(Data);
                if (dtJson.Rows.Count == 0)
                {
                    MessageBox.Show("No Data！");
                    return;
                }
                dataSummary.Clear();
                dataGridView1.Rows.Clear();
                int i = 0;
                Dictionary<string, Dictionary<string, object>> data = mergeData(dtJson);
                foreach (string key in data.Keys)
                {
                    dataGridView1.Rows.Add(1);

                    Dictionary<string, object> subData = data[key];
                    dataGridView1.Rows[i].Cells["address"].Value = key;
                    dataGridView1.Rows[i].Cells["cl_cb"].Value = false;

                    foreach (string subKey in subData.Keys)
                    {
                        int startOffset = 2;
                        if (subKey.IndexOf("count") != -1 || subKey.IndexOf("orgId") != -1)
                        {
                            continue;
                        }
                        //保存月份的总数据
                        Dictionary<string, double> sum;

                        if (dataSummary.ContainsKey(subKey))
                        {
                            sum = dataSummary[subKey];
                        }
                        else
                        {
                            sum = new Dictionary<string, double>();
                            sum.Add("mttr", 0);
                            sum.Add("mtbf", 0);
                            sum.Add("count", 0);//Valuable quantity of factory area in month
                            dataSummary.Add(subKey, sum);
                        }

                        int index = yearMonths.IndexOf(subKey);
                        //if (index > -1)
                        //{
                        //    startOffset += index * 2;
                        //    int count = int.Parse(subData[$"{subKey}_count"].ToString());
                        //    double workTime = 8 * getWorkDays(subData["orgId"].ToString(), subKey);
                        //    double mttr = Math.Round(Double.Parse(subData[subKey].ToString()) / count, 3);
                        //    dataGridView1.Rows[i].Cells[startOffset].Value = mttr;
                        //    dataGridView1.Rows[i].Cells[startOffset + 1].Value = Math.Round(workTime, 1);
                        //    dataGridView1.Rows[i].Cells[startOffset + 1].Style.BackColor = Color.WhiteSmoke;

                        //    sum["mttr"] += mttr;
                        //    sum["mtbf"] += workTime;
                        //    sum["count"] += 1;
                        //}
                        if (index > -1)
                        {
                            startOffset += index * 2;
                            int count = int.Parse(subData[$"{subKey}_count"].ToString());
                            double workTime = 8 * getWorkDays(subData["orgId"].ToString(), subKey);
                            double mttr = Math.Round(Double.Parse(subData[subKey].ToString()) / count, 3);
                            double mtbf= Math.Round(workTime / count, 3);
                            dataGridView1.Rows[i].Cells[startOffset].Value = mttr;
                            dataGridView1.Rows[i].Cells[startOffset + 1].Value = Math.Round(mtbf, 1);
                            dataGridView1.Rows[i].Cells[startOffset + 1].Style.BackColor = Color.WhiteSmoke;

                            sum["mttr"] += mttr;
                            sum["mtbf"] += mtbf;
                            sum["count"] += 1;
                        }
                    }

                    i++;
                }
                if (dataSummary.Count > 0)
                {

                    dataGridView1.Rows.Add(1);
                    dataGridView1.Rows[i].Cells[0].Value = false;
                    dataGridView1.Rows[i].Cells[1].Value = "Average_value";
                    foreach (string key in dataSummary.Keys)
                    {
                        int size = (int)dataSummary[key]["count"];

                        int position = 2;
                        int index = yearMonths.IndexOf(key);
                        if (index > -1)
                        {
                            position += index * 2;
                            dataGridView1.Rows[i].Cells[position].Value = Math.Round(dataSummary[key]["mttr"] / size, 3);
                            dataGridView1.Rows[i].Cells[position + 1].Value = Math.Round(dataSummary[key]["mtbf"] / size, 3);
                        }
                    }

                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private Dictionary<string, Dictionary<string, object>> mergeData(DataTable dtJson)
        {
            //key：楼层，value：
            Dictionary<string, Dictionary<string, object>> data = new Dictionary<string, Dictionary<string, object>>();
            Dictionary<string, object> subDate;
            for (int i = 0; i < dtJson.Rows.Count; i++)
            {
                string storeyName = dtJson.Rows[i]["storey_name"].ToString();
                string orgId = dtJson.Rows[i]["org_id"].ToString();
                double rt = Double.Parse(dtJson.Rows[i]["rt"].ToString());
                string month = dtJson.Rows[i]["month"].ToString();
                if (data.ContainsKey(storeyName))
                {
                    subDate = data[storeyName];
                    if (subDate.ContainsKey(month))
                    {
                        subDate[month] = double.Parse(subDate[month].ToString()) + rt;
                        //月份厂区数量
                        subDate[$"{month}_count"] = int.Parse(subDate[$"{month}_count"].ToString()) + 1;
                    }
                    else
                    {
                        subDate.Add(month, rt);
                        subDate[$"{month}_count"] = 1;
                    }
                }
                else
                {
                    subDate = new Dictionary<string, object>();
                    subDate.Add(month, rt);
                    subDate.Add($"{month}_count", 1);//月份个数
                    subDate.Add("orgId", orgId);
                    data.Add(storeyName, subDate);
                }
            }

            return data;
        }

        private void addHeader()
        {
            dataGridView1.Columns.Clear();

            //包含Header所有的单元格的背景色为黄色
            DataGridViewColumn dataGridViewColumn = new DataGridViewColumn();
            dataGridViewColumn.Width = 50;
            dataGridViewColumn.HeaderText = " ";
            dataGridViewColumn.Name = "cl_cb";
            dataGridViewColumn.CellTemplate = new DataGridViewCheckBoxCell();
            dataGridView1.Columns.Add(dataGridViewColumn);
            //dataGridView1.Columns.Add(initDataGridViewColumn(100, "指标（H）", "address"));
            dataGridView1.Columns.Add(initDataGridViewColumn(100, "Index（H）", "address"));

            for (int i = 0; i < months.Count; i++)
            {
                dataGridView1.Columns.Add(initDataGridViewColumn(60, "MTTR", "mttr" + months[i]));
                dataGridView1.Columns.Add(initDataGridViewColumn(60, "MTBF", "mtbf" + months[i]));
            }
        }

        private DataGridViewColumn initDataGridViewColumn(int w, string text, string name)
        {
            DataGridViewColumn dataGridViewColumn = new DataGridViewColumn();
            dataGridViewColumn.Width = w;
            dataGridViewColumn.HeaderText = text;
            dataGridViewColumn.Name = name;
            dataGridViewColumn.CellTemplate = new DataGridViewTextBoxCell();
            dataGridViewColumn.ReadOnly = true;
            return dataGridViewColumn;
        }

        private void addMonthLabel()
        {
            int childCount = panel3.Controls.Count;
            for (int i = childCount - 1; i > 0; i--)
            {
                panel3.Controls.RemoveAt(i);
            }

            for (int i = 0; i < months.Count; i++)
            {
                Label label = new System.Windows.Forms.Label();

                label.AutoSize = true;
                label.Font = new System.Drawing.Font("宋体", 14F);
                label.Location = new System.Drawing.Point(36, 14);
                label.Name = "l" + i;
                label.Size = new System.Drawing.Size(38, 19);
                label.TabIndex = 0;
                //label.Text = months[i] + "月";
                label.Text = months[i] + " Month";

                Panel panel = new System.Windows.Forms.Panel();
                panel.SuspendLayout();
                panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                panel.Controls.Add(label);
                panel.Location = new System.Drawing.Point(71 + 120 * (i + 1), -1);
                panel.Name = "p" + i;
                panel.Size = new System.Drawing.Size(120, 47);
                panel.TabIndex = 26;

                panel3.Controls.Add(panel);
                panel.ResumeLayout(false);
            }

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            //paintline(e, "厂区", "月份");
            paintline(e, "Factory area","Month");
        }

        private int w;
        private void paintline(PaintEventArgs e, string name1, string name2)
        {
            Rectangle rec = panel4.DisplayRectangle;
            Graphics gp = e.Graphics;
            gp.SmoothingMode = SmoothingMode.HighQuality;
            Brush gridBrush = new SolidBrush(Color.Black);
            Pen gridLinePen = new Pen(gridBrush);
            if (rec.Width !=w && w!=0) {
                return;
            }
            w = rec.Width;
            gp.DrawLine(gridLinePen, new Point(rec.X, rec.Y), new Point(rec.X + rec.Width, rec.Y + rec.Height));
            gp.DrawString(name1, new Font("SimSun", 9F), Brushes.Black, new Point(rec.X, rec.Y + rec.Height - 20));
            gp.DrawString(name2, new Font("SimSun", 9F), Brushes.Black, new Point(rec.X + rec.Width - 30, rec.Y + 5));
        }

        private void panel3_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.NewValue <= point.X)
            {
                panel4.Location = point;
            }
            Point p = panel4.Location;
            p.X -= e.NewValue;
            panel4.Location = p;
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            checkedListBox1.Visible = !checkedListBox1.Visible;
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateQueryDay();
        }

        private void updateQueryDay()
        {
            CheckedItemCollection items = checkedListBox1.CheckedItems;
            textBox1.Text = "";
            months.Clear();
            yearMonths.Clear();
            int year = dateTimePicker1.Value.Year;
            foreach (string item in items)
            {

                textBox1.Text += item;
                if (items.IndexOf(item) != items.Count - 1)
                {
                    textBox1.Text += ",";
                }
                months.Add(item);
                yearMonths.Add($"{year}/{int.Parse(item):00}");
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAddressInfoByOrgId();
        }

        private void GetAddressInfoByOrgId()
        {
            storey.Clear();
            addressCodes.Clear();

            List<AutocompleteItem> autoAddress = new List<AutocompleteItem>();
            List<string> addressStr = new List<string>();
            AutoCompleteStringCollection autoCompleteAddress = new AutoCompleteStringCollection();

            List<AutocompleteItem> autoStorey = new List<AutocompleteItem>();
            List<string> storeyStr = new List<string>();
            AutoCompleteStringCollection autoComplete1Storey = new AutoCompleteStringCollection();
            List<string> distinct = new List<string>();

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("org_code", orgCode[comboBox1.SelectedIndex]);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineManagerServer", "GetAddressInfoByOrgId", Program.Client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson =JsonConvert.DeserializeObject<DataTable>(Data);
                //dtJson.Rows.Add("全部","");

                autoAddress.Add(new MulticolumnAutocompleteItem(new[] { "", "" }, "All"));
                autoStorey.Add(new MulticolumnAutocompleteItem(new[] { "", "" }, "All"));
                storey.Add("");
                addressCodes.Add("");

                for (int i = 0; i < dtJson.Rows.Count; i++)
                {

                    string storeyCode = dtJson.Rows[i]["storey_code"].ToString();
                    string storeyName = dtJson.Rows[i]["storey_name"].ToString();

                    if (!distinct.Contains(storeyCode + "|" + storeyName))
                    {
                        storey.Add(storeyCode);
                        storeyStr.Add(storeyName);
                        autoStorey.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i]["storey_code"].ToString(), dtJson.Rows[i]["storey_name"].ToString() }, dtJson.Rows[i]["storey_name"].ToString() + "|" + dtJson.Rows[i]["storey_code"].ToString()));
                        distinct.Add(storeyCode + "|" + storeyName);
                    }

                    string addressCode = dtJson.Rows[i]["address_code"].ToString();
                    string addressName = dtJson.Rows[i]["address_name"].ToString();
                    addressCodes.Add(addressCode);
                    addressStr.Add(addressName);
                    autoAddress.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i]["address_code"].ToString(), dtJson.Rows[i]["address_name"].ToString() }, dtJson.Rows[i]["address_name"].ToString()));

                }

                comboBox4.DataSource = autoStorey;
                autoComplete1Storey.AddRange(storeyStr.ToArray());
                comboBox4.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox4.AutoCompleteSource = AutoCompleteSource.CustomSource;
                comboBox4.AutoCompleteCustomSource = autoComplete1Storey;

                comboBox3.DataSource = autoAddress;
                autoCompleteAddress.AddRange(addressStr.ToArray());
                comboBox3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox3.AutoCompleteSource = AutoCompleteSource.CustomSource;
                comboBox3.AutoCompleteCustomSource = autoCompleteAddress;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Boolean isExcute = false;
            //DataTable dt = new DataTable();
            //dt.Columns.Add(@"Factory Area\Month");
            //for (int i = 0; i < months.Count; i++)
            //{
            //    dt.Columns.Add(months[i] + "月");
            //    dt.Columns.Add(months[i] + "月");
            //}
            Boolean isExcute = false;
            DataTable dt = new DataTable();
            dt.Columns.Add(@"Factory Area\Month");
            for (int i = 0; i < months.Count; i++)
            {
                dt.Columns.Add(months[i] + "M");
                dt.Columns.Add(months[i] + "MM");
            }
            DataRow dr = dt.NewRow();
            dr[0] = "Index(H)";
            for (int i = 0; i < months.Count; i++)
            {
                dr[i * 2 + 1] = "MTTR";
                dr[i * 2 + 2] = "MTBF";
            }
            dt.Rows.Add(dr);

            isExcute = true;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewCell o = dataGridView1.Rows[i].Cells["cl_cb"];
                if (o != null && o.Value != null && bool.Parse(o.Value.ToString()))
                {
                    isExcute = true;
                    dr = dt.NewRow();
                    for (int j = 1; j < dataGridView1.Rows[i].Cells.Count; j++)
                    {
                        DataGridViewCell d = dataGridView1.Rows[i].Cells[j];
                        if (d == null || d.Value == null)
                        {
                            dr[j - 1] = "";
                        }
                        else
                        {
                            dr[j - 1] = d.Value.ToString();
                        }
                    }
                    dt.Rows.Add(dr);
                }
            }
            if (isExcute)
            {
                NewExportExcels.ExportExcels.Export("Summary of TPM key indicators (details)", dt);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            updateQueryDay();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Index_SummaryMainForm f=  new Index_SummaryMainForm();
            f.Close();
        }
    }
}
