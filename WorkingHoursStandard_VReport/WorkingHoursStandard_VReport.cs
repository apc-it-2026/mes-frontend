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

namespace WorkingHoursStandard_VReport
{
    public partial class WorkingHoursStandard_VReport : MaterialForm
    {
        private int temp = 0;
        public static DataRow drTemp = null;
        //private int _sorterOrder;   //1表示升序，0表示降序
        //private int _previousIndex = -1;    //记录前一次点击的列索引

        public WorkingHoursStandard_VReport()
        {
            InitializeComponent();

            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);

            //this.WindowState = FormWindowState.Maximized;

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

            p.Add("vOrg_code", comboBox1.Text.Trim().ToString().ToUpper().Split('|')[0]);//.Trim().ToString().ToUpper().Split('|')[0]
            p.Add("vPlant_code", comboBox2.Text.Trim().ToString().ToUpper().Split('|')[0]);
            p.Add("vDept_code", textBox1.Text);
            p.Add("vPross", cbxPross.Text.Trim().ToString().ToUpper().Split('|')[0]);
            p.Add("vDateType", cbxData.Text);
            p.Add("vBeginTime", dateTimePicker1.Text);
            p.Add("vEndTime", dateTimePicker2.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL,
                "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.SFC_Shift_SettingServer", "QueryReport",
            Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dtJson.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dtJson;
                    dataGridView1.Rows[0].Selected = false;
                    this.dataGridView1.Sort(this.dataGridView1.Columns["Column13"], ListSortDirection.Ascending);
                    //1
                    //if (cbxData.Text == "月")
                    //{
                    //    DataView dj = dtJson.DefaultView;
                    //    DataTable dataTable1 = new DataTable();

                    //    for (var i = 0; i < dataGridView1.RowCount; i++)
                    //    {
                    //        if ((temp < dataGridView1.Rows.Count - 1))
                    //        {
                    //            var dr = dataTable1.NewRow();
                    //            for (var j = 0; j < dataGridView1.ColumnCount; j++)
                    //            {
                    //                if (i == 0)
                    //                {
                    //                    var dc = new DataColumn(dataGridView1.Columns[j].Name);
                    //                    dataTable1.Columns.Add(dc);
                    //                }
                    //                dr[j] = dataGridView1[j, i].Value;
                    //            }
                    //            dataTable1.Rows.Add(dr);
                    //        }
                    //    }
                    //    var query1 = from g in dataTable1.AsEnumerable()

                    //                 group g by new
                    //                 {
                    //                     t1 = g.Field<string>("Column13"),
                    //                     t2 = g.Field<string>("Column7"),
                    //                     t9 = g.Field<string>("Column5"),
                    //                     t10 = g.Field<string>("Column1"),
                    //                     t11 = g.Field<string>("Column4"),
                    //                     t12 = g.Field<string>("Column2"),
                    //                     t13 = g.Field<string>("Column3"),

                    //                 } into products

                    //                 select new
                    //                 {
                    //                     scan_date = products.Key.t1,
                    //                     Scan_detpt = products.Key.t2,
                    //                     Act_WORK_houser = products.Sum(n => Convert.ToDecimal(n.Field<string>("Column6"))),
                    //                     qty = products.Sum(n => Convert.ToDecimal(n.Field<string>("Column8"))),
                    //                     qty_percent = 1.00,
                    //                     Tar_WORK_houser = products.Sum(n => Convert.ToDecimal(n.Field<string>("Column10"))),
                    //                     PPH = products.Average(n => Convert.ToDecimal(n.Field<string>("Column11"))).ToString("0.00"),
                    //                     efficiency = products.Average(n => Convert.ToDecimal(n.Field<string>("Column12"))).ToString("0.00"),
                    //                     ART_NO = products.Key.t9,
                    //                     NAME_T = products.Key.t10,
                    //                     mold_no = products.Key.t11,
                    //                     Process_no = products.Key.t12,
                    //                     THT = products.Key.t13
                    //                 };
                    //    this.dataGridView1.DataSource = query1.ToList();
                    //    dataGridView1.Rows[0].Selected = false;
                    //}

                    ////日
                    //DataView dv = dtJson.DefaultView;
                    //DataTable dataTable = new DataTable();

                    //for (var i = 0; i < dataGridView1.RowCount; i++)
                    //{
                    //    if ((temp < dataGridView1.Rows.Count - 1))
                    //    {
                    //        var dr = dataTable.NewRow();
                    //        for (var j = 0; j < dataGridView1.ColumnCount; j++)
                    //        {
                    //            if (i == 0)
                    //            {
                    //                var dc = new DataColumn(dataGridView1.Columns[j].Name);
                    //                dataTable.Columns.Add(dc);
                    //            }
                    //            dr[j] = dataGridView1[j, i].Value;
                    //        }
                    //        dataTable.Rows.Add(dr);
                    //    }
                    //}
                    //var query = from g in dataTable.AsEnumerable()
                    //            group g by new
                    //            {
                    //                t1 = g.Field<string>("Column13"),
                    //                t2 = g.Field<string>("Column7"),

                    //            } into products

                    //            select new
                    //            {
                    //                t1 = products.Key.t1,
                    //                t2 = products.Key.t2,
                    //                t3 = products.Sum(n => Convert.ToDecimal(n.Field<string>("Column6"))),
                    //                t4 = products.Sum(n => Convert.ToDecimal(n.Field<string>("Column8"))),
                    //                t5 = 1.00,
                    //                t6 = products.Sum(n => Convert.ToDecimal(n.Field<string>("Column10"))),
                    //                t7 = products.Average(n => Convert.ToDecimal(n.Field<string>("Column11"))).ToString("0.00"),
                    //                t8 = products.Sum(n => Convert.ToDecimal(n.Field<string>("Column6"))) == 0 ? "0" : ((products.Sum(n => Convert.ToDecimal(n.Field<string>("Column10"))) / products.Sum(n => Convert.ToDecimal(n.Field<string>("Column6")))) * 100).ToString("0.00")
                    //                //t8 = products.Average(n => Convert.ToDecimal(n.Field<string>("Column12"))).ToString("0.00")
                    //            };
                    //this.dataGridView2.DataSource = query.ToList();
                    //dataGridView2.Rows[0].Selected = false;
                    //this.dataGridView2.Sort(this.dataGridView2.Columns["t1"], ListSortDirection.Ascending);
                }
                else
                {
                    MessageBox.Show("No such data");
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

        }

        private void StandardVolumeReportForm_Load(object sender, EventArgs e)
        {
            LoadQueryItem();
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
                    items1.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i - 1]["org_code"].ToString(), dtJson.Rows[i - 1]["org_name"].ToString() }, dtJson.Rows[i - 1]["org_code"].ToString() + "|" + dtJson.Rows[i - 1]["org_name"].ToString()));
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
                    items2.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i - 1]["code"].ToString(), dtJson.Rows[i - 1]["org"].ToString() }, dtJson.Rows[i - 1]["code"].ToString() + "|" + dtJson.Rows[i - 1]["org"].ToString()));
                }
            }
            comboBox2.DataSource = items2;

            var items3 = new List<AutocompleteItem>();//group
            string ret3 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.ProductionTargetsServer", "LoadSeDept", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject("noParam"));
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
            autocompleteMenu3.SetAutocompleteMenu(textBox1, autocompleteMenu3);
            autocompleteMenu3.SetAutocompleteItems(items3);

        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            cbxPross.Text = "";
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            query();
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            string a = "工时标准量.xls";
            ExportExcels(a, dataGridView1,dataGridView2);
        }
        private void ExportExcels(string fileName, DataGridView myDGV, DataGridView myDGV2)
        {
            string saveFileName = "";
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xls";
            saveDialog.Filter = "Excel文件|*.xls";
            saveDialog.FileName = fileName;
            saveDialog.ShowDialog();
            saveFileName = saveDialog.FileName;
            if (saveFileName.IndexOf(":") < 0) return; //被点了取消
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            if (xlApp == null)
            {
                MessageBox.Show("Unable to create Excel object，Maybe your computer does not have Excel installed");
                return;
            }
            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];//get sheet1
                                                                                                                                  //write title
            for (int i = 0; i < myDGV.ColumnCount; i++)
            {
                worksheet.Cells[1, i + 1] = "'" + myDGV.Columns[i].HeaderText.ToString();
            }
            //write value
            for (int r = 0; r < myDGV.Rows.Count; r++)
            {
                for (int i = 0; i < myDGV.ColumnCount; i++)
                {
                    worksheet.Cells[r + 2, i + 1].NumberFormatLocal = "@";
                    worksheet.Cells[r + 2, i + 1] = myDGV.Rows[r].Cells[i].Value;
                }
                //System.Windows.Forms.Application.DoEvents();
            }



            //write value
            worksheet.Cells[1 + myDGV.Rows.Count + 5, 1] = "Total：";
            for (int i = 0; i < myDGV2.ColumnCount; i++)
            {
                worksheet.Cells[1+ myDGV.Rows.Count + 6, i + 1] = "'" + myDGV2.Columns[i].HeaderText.ToString();
            }
            for (int r = 0; r < myDGV2.Rows.Count; r++)
            {
                for (int i = 0; i < myDGV2.ColumnCount; i++)
                {
                    worksheet.Cells[r + myDGV.Rows.Count+8, i + 1].NumberFormatLocal = "@";
                    worksheet.Cells[r + myDGV.Rows.Count+8, i + 1] = myDGV2.Rows[r].Cells[i].Value;
                }
                System.Windows.Forms.Application.DoEvents();
            }
            worksheet.Columns.EntireColumn.AutoFit();//Column width adaptation
            if (saveFileName != "")
            {
                try
                {
                    workbook.Saved = true;
                    workbook.SaveCopyAs(saveFileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error exporting file,file may be being opened！\n" + ex.Message);
                }
            }
            xlApp.Quit();
            GC.Collect();//forcibly destroyed
            MessageBox.Show("Successfully saved", "message notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridView2_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
        //    //判断列标头是否被连续点击，是则改变上次排序规则，否则按升序排序
        //    if (this._previousIndex == e.ColumnIndex)
        //    {
        //        this._sorterOrder = -this._sorterOrder;
        //    }
        //    else
        //    {
        //        this._sorterOrder = 1;
        //    }
        //    this._previousIndex = e.ColumnIndex;
        //    ListComparison(dataGridView2.Columns[e.ColumnIndex].DataPropertyName);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetAdd(dataGridView1);
        }
        private void GetAdd( DataGridView dtJson)
        {
            if (dtJson.Rows.Count > 0)
            {
                
                //1
                if (cbxData.Text == "Month")
                {
                    //DataView dj = dtJson.DefaultView;
                    DataTable dataTable1 = new DataTable();

                    for (var i = 0; i < dataGridView1.RowCount; i++)
                    {
                        if ((temp < dataGridView1.Rows.Count - 1))
                        {
                            var dr = dataTable1.NewRow();
                            for (var j = 0; j < dataGridView1.ColumnCount; j++)
                            {
                                if (i == 0)
                                {
                                    var dc = new DataColumn(dataGridView1.Columns[j].Name);
                                    dataTable1.Columns.Add(dc);
                                }
                                dr[j] = dataGridView1[j, i].Value;
                            }
                            dataTable1.Rows.Add(dr);
                        }
                    }
                    var query1 = from g in dataTable1.AsEnumerable()

                                 group g by new
                                 {
                                     t1 = g.Field<string>("Column13"),
                                     t2 = g.Field<string>("Column7"),
                                     t9 = g.Field<string>("Column5"),
                                     t10 = g.Field<string>("Column1"),
                                     t11 = g.Field<string>("Column4"),
                                     t12 = g.Field<string>("Column2"),
                                     t13 = g.Field<string>("Column3"),

                                 } into products

                                 select new
                                 {
                                     scan_date = products.Key.t1,
                                     Scan_detpt = products.Key.t2,
                                     Act_WORK_houser = products.Sum(n => Convert.ToDecimal(n.Field<string>("Column6"))),
                                     qty = products.Sum(n => Convert.ToDecimal(n.Field<string>("Column8"))),
                                     qty_percent = 1.00,
                                     Tar_WORK_houser = products.Sum(n => Convert.ToDecimal(n.Field<string>("Column10"))),
                                     PPH = products.Average(n => Convert.ToDecimal(n.Field<string>("Column11"))).ToString("0.00"),
                                     efficiency = products.Average(n => Convert.ToDecimal(n.Field<string>("Column12"))).ToString("0.00"),
                                     ART_NO = products.Key.t9,
                                     NAME_T = products.Key.t10,
                                     mold_no = products.Key.t11,
                                     Process_no = products.Key.t12,
                                     THT = products.Key.t13
                                 };
                    this.dataGridView1.DataSource = query1.ToList();
                    dataGridView1.Rows[0].Selected = false;
                }

                //日
                //DataView dv = dtJson.DefaultView;
                DataTable dataTable = new DataTable();

                for (var i = 0; i < dataGridView1.RowCount; i++)
                {
                    if ((temp < dataGridView1.Rows.Count - 1))
                    {
                        var dr = dataTable.NewRow();
                        for (var j = 0; j < dataGridView1.ColumnCount; j++)
                        {
                            if (i == 0)
                            {
                                var dc = new DataColumn(dataGridView1.Columns[j].Name);
                                dataTable.Columns.Add(dc);
                            }
                            dr[j] = dataGridView1[j, i].Value;
                        }
                        dataTable.Rows.Add(dr);
                    }
                }
                var query = from g in dataTable.AsEnumerable()
                            group g by new
                            {
                                t1 = g.Field<string>("Column13"),
                                t2 = g.Field<string>("Column7"),

                            } into products

                            select new
                            {
                                t1 = products.Key.t1,
                                t2 = products.Key.t2,
                                t3 = products.Sum(n => Convert.ToDecimal(n.Field<string>("Column6"))),
                                t4 = products.Sum(n => Convert.ToDecimal(n.Field<string>("Column8"))),
                                t5 = 1.00,
                                t6 = products.Sum(n => Convert.ToDecimal(n.Field<string>("Column10"))),
                                t7 = products.Average(n => Convert.ToDecimal(n.Field<string>("Column11"))).ToString("0.00"),
                                t8 = products.Sum(n => Convert.ToDecimal(n.Field<string>("Column6"))) == 0 ? "0" : ((products.Sum(n => Convert.ToDecimal(n.Field<string>("Column10"))) / products.Sum(n => Convert.ToDecimal(n.Field<string>("Column6")))) * 100).ToString("0.00")
                                //t8 = products.Average(n => Convert.ToDecimal(n.Field<string>("Column12"))).ToString("0.00")
                            };
                this.dataGridView2.DataSource = query.ToList();
                dataGridView2.Rows[0].Selected = false;
                //this.dataGridView2.Sort(this.dataGridView2.Columns["t1"], ListSortDirection.Ascending);
            }
            else
            {
                MessageBox.Show("查无此数据");
            }
        }
        ///// <summary>
        ///// List集合比较器
        ///// </summary>
        ///// <param name="propName">属性名</param>
        //private void ListComparison(string propName)
        //{
        //    Type type = typeof(MyModel);
        //    PropertyInfo property = type.GetProperty(propName);
        //    List<MyModel> modelList = dataGridView2.DataSource as List<MyModel>;
        //    modelList.Sort((x, y) =>
        //    {
        //        string value1 = property.GetValue(x, null).ToString();
        //        string value2 = property.GetValue(y, null).ToString();
        //        double number1, number2;
        //        //如果属性值为数字则转换为浮点数进行比较，否则按照字符串比较
        //        if (double.TryParse(value1, out number1) && double.TryParse(value2, out number2))
        //        {
        //            return this._sorterOrder == 1 ? number1.CompareTo(number2) : number2.CompareTo(number1);
        //        }

        //        return this._sorterOrder == 1 ? value1.CompareTo(value2) : value2.CompareTo(value1);
        //    });
        //}
    }
}
