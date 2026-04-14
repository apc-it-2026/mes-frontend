using System;
using System.Data;
using System.Drawing;
using System.Linq;
using MaterialSkin.Controls;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Reflection;
using WinFormLib;
using SJeMES_Framework.WebAPI;
using Newtonsoft.Json;
using NewExportExcels;
using SJeMES_Control_Library;
using SJeMES_Framework.Common;

namespace F_WOO_WareHouseReport
{
    public partial class F_WOO_WareHouseReportForm : MaterialForm
    {
        private int temp = 0;
        public F_WOO_WareHouseReportForm()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);
            LoadWakerOrder();  //Load ticket number
            GetWareHouse();//load warehouse
            GetUnit();//load unit
            //LoadOrderNO();
        }
        #region common
        private void LoadOrderNO()
        {
            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_WOO_WareHouseReportServer", "LoadCombNo", Program.client.UserToken, JsonConvert.SerializeObject(string.Empty));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {
                    collection.Add(dtJson.Rows[i - 1]["SE_ID"].ToString());
                }
                txtCombNo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtCombNo.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtCombNo.AutoCompleteCustomSource = collection;

                txtCombNo2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtCombNo2.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtCombNo2.AutoCompleteCustomSource = collection;

                txtCombNo3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtCombNo3.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtCombNo3.AutoCompleteCustomSource = collection;

            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        //Load ticket number
        private void LoadWakerOrder()
        {
            var orderSource = new AutoCompleteStringCollection();   //Store database query results
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_BarcodePrinting_WakeOrderServer", "LoadWakerOrder", Program.client.UserToken, JsonConvert.SerializeObject(string.Empty));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {
                    orderSource.Add(dtJson.Rows[i - 1]["ORDER_NO"].ToString());
                }
                //The total supporting report work order number is bound to the data source
                textArt.AutoCompleteCustomSource = orderSource;   //bind data source
                textArt.AutoCompleteMode = AutoCompleteMode.Suggest;    //Show related dropdown
                textArt.AutoCompleteSource = AutoCompleteSource.CustomSource;   //set properties
                //The main work order binding data source of the daily supporting report
                textArtDay.AutoCompleteCustomSource = orderSource;   //bind data source 
                textArtDay.AutoCompleteMode = AutoCompleteMode.Suggest;    //Show related dropdown
                textArtDay.AutoCompleteSource = AutoCompleteSource.CustomSource;   //set properties
                //The master work order binding data source of the detailed table is the main work order binding data source of the detailed table
                textBox3.AutoCompleteCustomSource = orderSource;   //bind data source
                textBox3.AutoCompleteMode = AutoCompleteMode.Suggest;    //Show related dropdown
                textBox3.AutoCompleteSource = AutoCompleteSource.CustomSource;   //set properties


            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

        }
        /// <summary>
        /// Feeding total supporting report
        /// </summary>
        /// <param name="parts">datatable with part numbers and part names</param>
        public void Receipt_total_supporting_report(DataTable partDt, string InaSingleNo, string CombNo, string salesOrder)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("vPartDt", partDt);
            if (string.IsNullOrEmpty(InaSingleNo))
            {
                p.Add("vOrder_No", textArt.Text);
            }
            else
            {
                p.Add("vOrder_No", CombNo);
            }
            p.Add("InaSingleNo", InaSingleNo);
            p.Add("salesOrder", salesOrder);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_WOO_WareHouseReportServer", "ReceiptTotalSupportingReport", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                DataRow totaldgvRow2 = TotalRow.GetTotalRow(dataTable: dtJson) ?? throw new ArgumentNullException(paramName: "TotalRow.GetTotalRow(dataTablePoint)");
                totaldgvRow2[column: dtJson.Columns[name: "size"]] = "Total";
                dtJson.Rows.Add(row: totaldgvRow2);
                dataGridView1.DataSource = dtJson;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        /// <summary>
        /// Return material total supporting report
        /// </summary>
        /// <param name="partDt"></param>
        public void Return_material_supporting_report(DataTable partDt, string InaSingleNo, string CombNo, string salesOrder)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("vPartDt", partDt);
            if (string.IsNullOrEmpty(InaSingleNo))
            {
                p.Add("vOrder_No", textArt.Text);
            }
            else
            {
                p.Add("vOrder_No", CombNo);
            }
            p.Add("InaSingleNo", InaSingleNo);
            p.Add("salesOrder", salesOrder);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_WOO_WareHouseReportServer", "ReturnMaterialSupportingReport", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                DataRow totaldgvRow2 = TotalRow.GetTotalRow(dataTable: dtJson) ?? throw new ArgumentNullException(paramName: "TotalRow.GetTotalRow(dataTablePoint)");
                totaldgvRow2[column: dtJson.Columns[name: "size"]] = "total";
                dtJson.Rows.Add(row: totaldgvRow2);
                dataGridView1.DataSource = dtJson;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        public DataTable GetArtPart(string order_no)
        {
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("vOrder_No", order_no);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_WOO_WareHouseReportServer", "QyeryArtPart", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                return dtJson;

            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                return null;
            }
        }
        //Delivery day supporting report
        public void Receipt_Day_Supprting_Report(DataTable partDt, string txtCombNo2, string txtInaSingleNo2, string salesOrder)
        {
            string firsttime = FristTimeDay.Text;
            string lasttime = LastTimeDay.Text;
            string order_no = textArtDay.Text;
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("vPartDt", partDt);
            p.Add("vFirstTime", firsttime);
            p.Add("vLastTime", lasttime);
            p.Add("salesOrder", salesOrder);
            if (string.IsNullOrEmpty(txtInaSingleNo2))
            {
                p.Add("vOrder_No", order_no);
            }
            else
            {
                p.Add("vOrder_No", txtCombNo2);
            }
            p.Add("txtInaSingleNo2", txtInaSingleNo2);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_WOO_WareHouseReportServer", "ReceiptDaySupprtingReport", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                DataRow totaldgvRow2 = TotalRow.GetTotalRow(dataTable: dtJson) ?? throw new ArgumentNullException(paramName: "TotalRow.GetTotalRow(dataTablePoint)");
                totaldgvRow2[column: dtJson.Columns[name: "size"]] = "total";
                dtJson.Rows.Add(row: totaldgvRow2);

                dataGridView4.DataSource = dtJson;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        /// <summary>
        /// Return material day supporting report
        /// </summary>
        /// <param name="partDt"></param>
        public void Return_Day_supporting_report(DataTable partDt, string txtCombNo2, string txtInaSingleNo2, string salesOrder)
        {
            string firsttime = FristTimeDay.Text;
            string lasttime = LastTimeDay.Text;
            string order_no = textArtDay.Text;
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("vPartDt", partDt);
            p.Add("vFirstTime", firsttime);
            p.Add("vLastTime", lasttime);
            p.Add("salesOrder", salesOrder);
            if (string.IsNullOrEmpty(txtInaSingleNo2))
            {
                p.Add("vOrder_No", order_no);
            }
            else
            {
                p.Add("vOrder_No", txtCombNo2);
            }
            p.Add("InaSingleNo2", txtInaSingleNo2);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_WOO_WareHouseReportServer", "ReturnDaySupportingReport", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                DataRow totaldgvRow2 = TotalRow.GetTotalRow(dataTable: dtJson) ?? throw new ArgumentNullException(paramName: "TotalRow.GetTotalRow(dataTablePoint)");
                totaldgvRow2[column: dtJson.Columns[name: "size"]] = "total";
                dtJson.Rows.Add(row: totaldgvRow2);
                dataGridView4.DataSource = dtJson;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        //get warehouse
        public void GetWareHouse()
        {
            var source = new AutoCompleteStringCollection();   //Store database query results
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_WOO_WareHouseReportServer", "GetWareHouse", Program.client.UserToken, string.Empty);
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {
                    source.Add(dtJson.Rows[i - 1]["managementarea_no"].ToString() + "  " + dtJson.Rows[i - 1]["managementarea_memo"].ToString());
                }
                textBox6.AutoCompleteCustomSource = source;
                textBox6.AutoCompleteMode = AutoCompleteMode.Suggest;
                textBox6.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        //get unit
        public void GetUnit()
        {
            var source = new AutoCompleteStringCollection();   //Store database query results
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_WOO_WareHouseReportServer", "GetUnit", Program.client.UserToken, string.Empty);
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {
                    source.Add(dtJson.Rows[i - 1]["department_code"].ToString() + "  " + dtJson.Rows[i - 1]["department_name"].ToString());
                }
                textBox7.AutoCompleteCustomSource = source;
                textBox7.AutoCompleteMode = AutoCompleteMode.Suggest;
                textBox7.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        //Load shoe type
        public string GetShoeTyesName(string art)
        {
            string getShoedTypedName = "";
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("art", art);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_CutRounds_WarkOrderServer", "LoadShoeType", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                getShoedTypedName = json;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            return getShoedTypedName;
        }
        //load manufacturer
        public string GetProcessingUnit(string order_id)
        {
            string getUnits = "";
            string returnUnits = "";
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("order_id", order_id);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_WOO_WareHouseReportServer", "GetProcessingUnit", Program.client.UserToken, JsonConvert.SerializeObject(p));
            //  MessageBox.Show(ret);
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();

                DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    if (i == dtJson.Rows.Count - 1)
                    {
                        getUnits += dtJson.Rows[i][0].ToString();
                    }
                    else
                    {
                        getUnits += dtJson.Rows[i][0].ToString() + ",";
                    }
                }
                string[] s = getUnits.Split(',');
                //Console.WriteLine(s[0]);
                Dictionary<string, string> unit = new Dictionary<string, string>();
                for (int i = 0; i < s.Length; i++)
                {
                    string[] temp = s[i].Split('-');
                    if (!unit.ContainsKey(temp[1]))
                    {
                        unit.Add(temp[1], temp[0]);
                    }
                }
                foreach (string key in unit.Keys)
                {
                    returnUnits += key + "  ";
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            return returnUnits;
        }
        public void ExtelMyself(string fileName, DataGridView myDGV, string art, string shoeTypeName, string processing_unit, string date, string order_no, string sales_order)
        {
            try
            {
                //string temp_path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);//生成的文件存放路径  存在电脑的文档里边
                string temp_path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);//desktop
                ExcelOperate excelOperate = new ExcelOperate();
                //Create a new process of Excel.Application
                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                if (app == null)
                {
                    return;
                }
                app.Visible = false;
                app.UserControl = true;
                Microsoft.Office.Interop.Excel.Workbooks workbooks = app.Workbooks;
                Microsoft.Office.Interop.Excel._Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);//The parameters in the Add method here are equivalent to inheriting an empty template (for now, let's understand it like this)
                Microsoft.Office.Interop.Excel.Sheets sheets = workbook.Worksheets;
                Microsoft.Office.Interop.Excel._Worksheet worksheet = (Microsoft.Office.Interop.Excel._Worksheet)sheets.get_Item(1);
                if (worksheet == null)
                {
                    return;
                }

                worksheet.Rows.NumberFormatLocal = "@"; //Format all cells as text  
                                                        //first line title  
                worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, myDGV.Columns.Count]].Merge(Missing.Value); //Horizontal merge  
                //The file name is the same as the title name, this is the input title name
                worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, 1]].Value2 = fileName;
                //format  
                excelOperate.SetHAlignCenter(worksheet, worksheet.Cells[1, 1], worksheet.Cells[1, 1]);//Centered  
                excelOperate.SetFontSize(worksheet, worksheet.Cells[1, 1], worksheet.Cells[1, 1], 18);//font size  
                worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, myDGV.Columns.Count]].Borders.Color = ColorTranslator.ToOle(Color.Black);//black continuous border  

                Dictionary<string, string> temp = new Dictionary<string, string>();
                temp.Add("Master work order： ", order_no);
                temp.Add("shoe name:  ", shoeTypeName);
                temp.Add("Art: ", art);
                temp.Add("sales order:", sales_order);
                temp.Add("Manufacturer： ", processing_unit);
                temp.Add("date： ", date);
                for (int i = 2; i < 7; i++)
                {
                    worksheet.Range[worksheet.Cells[i, 1], worksheet.Cells[i, myDGV.Columns.Count]].Merge(Missing.Value); //Horizontal merge  
                                                                                                                          //The file name is the same as the title name, this is the input title name
                    string s123 = temp.ElementAt(i - 2).Key + temp.ElementAt(i - 2).Value;
                    string s456 = temp.ElementAt(i - 2).ToString();
                    worksheet.Range[worksheet.Cells[i, 1], worksheet.Cells[i, 1]].Value2 = s123;
                    //format  
                    excelOperate.SetHAlignCenter(worksheet, worksheet.Cells[i, 1], worksheet.Cells[i, 1]);//Centered  
                    excelOperate.SetFontSize(worksheet, worksheet.Cells[i, 1], worksheet.Cells[i, 1], 12);//font size  
                    worksheet.Range[worksheet.Cells[i, 1], worksheet.Cells[i, myDGV.Columns.Count]].Borders.Color = ColorTranslator.ToOle(Color.Black);//black continuous border  
                }


                //title
                for (int i = 0; i < myDGV.Columns.Count; i++)
                {
                    worksheet.Cells[7, i + 1] = myDGV.Columns[i].HeaderText.ToString();
                    excelOperate.SetFontSize(worksheet, worksheet.Cells[3, i + 1], worksheet.Cells[7, i + 1], 9);//font size  
                    excelOperate.SetBold(worksheet, worksheet.Cells[3, i + 1], worksheet.Cells[7, i + 1]); //black body  
                }

                worksheet.Range[worksheet.Cells[7, 1], worksheet.Cells[7, myDGV.Columns.Count]].Borders.Color = ColorTranslator.ToOle(Color.Black);
                for (int i = 0; i < myDGV.Rows.Count; i++)
                {
                    for (int j = 0; j < myDGV.Columns.Count; j++)
                    {
                        // myDGV.Rows[r].Cells[i].Value
                        string data = myDGV.Rows[i].Cells[j].Value.ToString();
                        worksheet.Cells[8 + i, j + 1] = data;
                        excelOperate.SetFontSize(worksheet, worksheet.Cells[8 + i, j + 1], worksheet.Cells[8 + i, j + 1], 9);//font size  
                                                                                                                             //if (j == 6)   
                                                                                                                             //{   
                                                                                                                             //    worksheet.Cells[4 + i, j + 1].  
                                                                                                                             //}  
                    }

                    //  worksheet.Range[worksheet.Cells[4 + i, 1], worksheet.Cells[4 + i, 10]].Borders.Color = ColorTranslator.ToOle(Color.Black);//设置边框颜色，不然打印预览，会非常不雅观  

                }
                worksheet.Name = fileName;

                worksheet.Columns.EntireColumn.AutoFit();//Column width adaptation  

                // String tick = DateTime.Now.ToString().Replace("-", "").Replace(":", "").Replace(" ", "") + ".xls";//excel文件名称  
                // string tick = "日配套报表导出";
                //String save_path = temp_path + "\\" + fileName + ".xls";
                String save_path = temp_path + "\\" + fileName + ".xlsx";
                // saveDialog.ShowDialog();
                workbook.SaveAs(save_path, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                excelOperate.Dispose(worksheet, workbook, app);//Close the Excel process
                MessageBox.Show("Export was successful");
            }
            catch (Exception e)
            {
                MessageBox.Show("export failed！");

            }
        }
        public void GetPart(DataTable order)
        {
            ProcessCB.Controls.Clear();
            dataGridView3.DataSource = null;
            DataTable dtJson = GetParts(order);
            if (dtJson == null || dtJson.Rows.Count <= 0)
            {
                MessageHelper.ShowErr(this, "The part name or part number of this work order is empty");
                return;
            }
            for (int i = 0; i < dtJson.Rows.Count; i++)
            {
                CheckBox processCB = new CheckBox();
                processCB.Text = dtJson.Rows[i]["PART_NAME"].ToString();
                processCB.Tag = dtJson.Rows[i]["PART_NO"].ToString();
                processCB.AutoSize = true;
                processCB.Font = new Font("微软雅黑", 13F);
                processCB.Margin = new Padding(20, 2, 0, 0);
                this.ProcessCB.Controls.Add(processCB);
            }
        }
        public DataTable GetParts(DataTable order_no)
        {
            DataTable TailorCheckDt = order_no;//package
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("data", TailorCheckDt);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_WOO_WareHouseReportServer", "GetParts", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                return dtJson;

            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                return null;
            }

        }
        /// <summary>
        /// Multiple Work Order Feeding Supporting Report
        /// </summary>
        /// <param name="parts">存了部件编号和部件名称的datatable</param>
        public void ReceiptMultiWorkOrderReport(DataTable partDt)
        {
            DataTable vOrder_No = (DataTable)dataGridView5.DataSource;//package
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("vPartDt", partDt);
            p.Add("vOrder_No", vOrder_No);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_WOO_WareHouseReportServer", "ReceiptMultiWorkOrderReport", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                DataRow totaldgvRow2 = TotalRow.GetTotalRow(dataTable: dtJson) ?? throw new ArgumentNullException(paramName: "TotalRow.GetTotalRow(dataTablePoint)");
                totaldgvRow2[column: dtJson.Columns[name: "size"]] = "total";
                dtJson.Rows.Add(row: totaldgvRow2);
                dataGridView3.DataSource = dtJson;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        /// <summary>
        /// Multiple Work Order Issue Supporting Report
        /// </summary>
        /// <param name="partDt"></param>
        public void ReturnMultiWorkOrderReport(DataTable partDt)
        {
            DataTable vOrder_No = (DataTable)dataGridView5.DataSource;
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("vPartDt", partDt);
            p.Add("vOrder_No", vOrder_No);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_WOO_WareHouseReportServer", "ReturnMultiWorkOrderReport", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                DataRow totaldgvRow2 = TotalRow.GetTotalRow(dataTable: dtJson) ?? throw new ArgumentNullException(paramName: "TotalRow.GetTotalRow(dataTablePoint)");
                totaldgvRow2[column: dtJson.Columns[name: "size"]] = "total";
                dtJson.Rows.Add(row: totaldgvRow2);
                dataGridView3.DataSource = dtJson;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        public void SetDefault(string Art, string Type, DataTable Order)
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vArt", Art);
            p.Add("vType", Type);
            p.Add("vOrder", Order);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_WOO_WareHouseReportServer", "SetDefault", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                MessageHelper.ShowSuccess(this, "Successfully saved！");
            }
            else
            {
                MessageHelper.ShowErr(this, "Failed to save！" + JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        public DataTable GetDefault(string Art, string Type)
        {
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("vArt", Art);
            p.Add("vType", Type);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_WOO_WareHouseReportServer", "GetDefault", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                return dtJson;

            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                return null;
            }
        }
        #endregion

        public void DataChanged_order(object sender, OrderDaChangeEventArgs args)
        {
            dataGridView5.DataSource = args.dt;


        }

        #region tabPage Total supporting report
        //Query based on a single work order
        private void button3_Click(object sender, EventArgs e)
        {
            txtCombNo.Text = "";
            textOrder.Text = "";
            comboBox2.Text = "";
            txtInaSingleNo.Text = "";
            txtSalesOrder1.Text = "";
            ProcessPanel.Controls.Clear();
            dataGridView1.DataSource = null;
            string order_no = textArt.Text;
            DataTable dtJson = GetArtPart(order_no);
            if (dtJson == null || dtJson.Rows.Count <= 0)
            {
                MessageHelper.ShowErr(this, "Part of the part name or part number or ART of the work order is empty");
                return;
            }
            textOrder.Text = dtJson.Rows[0]["ART_NO"].ToString();  //ART
            for (int i = 0; i < dtJson.Rows.Count; i++)
            {
                CheckBox processCB = new CheckBox();
                processCB.Text = dtJson.Rows[i]["PART_NAME"].ToString();
                processCB.Tag = dtJson.Rows[i]["PART_NO"].ToString();
                processCB.AutoSize = true;
                processCB.Font = new Font("微软雅黑", 13F);
                processCB.Margin = new Padding(20, 2, 0, 0);
                this.ProcessPanel.Controls.Add(processCB);
            }
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("vOrder_No", textArt.Text);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_WOO_WareHouseReportServer", "SelectSalesOrderByComnNo", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                if (!string.IsNullOrEmpty(json))
                {
                    txtSalesOrder1.Text = json;
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());

            }
        }
        //Query based on combined work order
        private void btnSelectByCombNo_Click(object sender, EventArgs e)
        {
            textArt.Text = "";
            textOrder.Text = "";
            comboBox2.Text = "";
            txtInaSingleNo.Text = "";
            txtSalesOrder1.Text = "";
            ProcessPanel.Controls.Clear();
            dataGridView1.DataSource = null;
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("vOrder_No", txtCombNo.Text);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_WOO_WareHouseReportServer", "SelectByCombNo", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                Dictionary<object, object> result = JsonConvert.DeserializeObject<Dictionary<object, object>>(json);
                string hedan = result["hedan"].ToString();
                string dt = result["dt"].ToString();
                DataTable dtResult = JsonHelper.GetDataTableByJson(dt);
                if (dtResult == null || dtResult.Rows.Count <= 0)
                {
                    MessageHelper.ShowErr(this, "Part of the part name or part number or ART of the work order is empty");
                    return;
                }
                textOrder.Text = dtResult.Rows[0]["ART_NO"].ToString();  //ART
                txtInaSingleNo.Text = hedan;
                for (int i = 0; i < dtResult.Rows.Count; i++)
                {
                    CheckBox processCB = new CheckBox();
                    processCB.Text = dtResult.Rows[i]["PART_NAME"].ToString();
                    processCB.Tag = dtResult.Rows[i]["PART_NO"].ToString();
                    processCB.AutoSize = true;
                    processCB.Font = new Font("微软雅黑", 13F);
                    processCB.Margin = new Padding(20, 2, 0, 0);
                    ProcessPanel.Controls.Add(processCB);
                }
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("vOrder_No", txtInaSingleNo.Text);
                string ret2 = WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_WOO_WareHouseReportServer", "SelectSalesOrderByComnNo", Program.client.UserToken, JsonConvert.SerializeObject(dic));
                if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["IsSuccess"]))
                {
                    string json2 = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["RetData"].ToString();
                    if (!string.IsNullOrEmpty(json2))
                    {
                        txtSalesOrder1.Text = json2;
                    }
                }
                else
                {
                    MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["ErrMsg"].ToString());

                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());

            }
        }
        //Matching query
        private void Query1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            DataTable partsDt = new DataTable();
            DataColumn dc1 = new DataColumn("part_no", Type.GetType("System.String"));
            DataColumn dc2 = new DataColumn("part_name", Type.GetType("System.String"));
            partsDt.Columns.Add(dc1);
            partsDt.Columns.Add(dc2);
            foreach (Control c in this.ProcessPanel.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox ck = c as CheckBox;
                    if (ck.Checked)
                    {
                        DataRow dr = partsDt.NewRow();
                        dr["part_no"] = ck.Tag.ToString();
                        dr["part_name"] = "\"" + ck.Text.ToString() + "\"";
                        partsDt.Rows.Add(dr);
                    }
                }
            }
            if (partsDt.Rows.Count <= 0)
            {
                MessageBox.Show("Please select a part！");
                return;
            }

            string type = comboBox2.Text;
            if (type.Equals("feed"))
            {
                Receipt_total_supporting_report(partsDt, txtInaSingleNo.Text, txtCombNo.Text, textBox8.Text);
            }
            else if (type.Equals("Return material"))
            {
                Return_material_supporting_report(partsDt, txtInaSingleNo.Text, txtCombNo.Text, textBox8.Text);
            }
            else
            {
                MessageBox.Show("Please select the correct report format");
            }
        }
        //Supporting report export
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                //single number
                //string order_id = textArt.Text.ToString();
                string order_id = "";
                string orderNo = "";
                if (string.IsNullOrEmpty(txtInaSingleNo.Text))
                {
                    order_id = textArt.Text.ToString();
                    orderNo = textArt.Text.ToString();
                }
                else
                {
                    order_id = txtInaSingleNo.Text;
                    orderNo = txtCombNo.Text;
                }
                //get art
                string art = textOrder.Text.Trim();
                //get shoe name
                string shoeTypeName = GetShoeTyesName(art);
                //Get the manufacturer
                string processing_unit = GetProcessingUnit(order_id);
                string type = comboBox2.Text.ToString();
                //Time to get the supporting report
                string date = DateTime.Now.ToLocalTime().ToString();
                if (type.Equals("feed"))
                {
                    ExtelMyself("Feeding total supporting report", dataGridView1, art, shoeTypeName, processing_unit, date, orderNo, txtSalesOrder1.Text);
                }
                else if (type.Equals("Return material"))
                {
                    ExtelMyself("Return material total supporting report", dataGridView1, art, shoeTypeName, processing_unit, date, orderNo, txtSalesOrder1.Text);
                }
                else
                {
                    MessageBox.Show("Unable to specify supporting report type");
                }
            }
        }
        //set as default widget
        private void button16_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textArt.Text))
            {
                MessageBox.Show("Please enter a master ticket！");
            }
            if (string.IsNullOrEmpty(textOrder.Text))
            {
                MessageBox.Show("ART cannot be empty！");
            }
            if (string.IsNullOrEmpty(comboBox2.Text))
            {
                MessageBox.Show("Please select feed/return");
            }
            DataTable partsDt = new DataTable();
            DataColumn dc1 = new DataColumn("part_no", Type.GetType("System.String"));
            DataColumn dc2 = new DataColumn("part_name", Type.GetType("System.String"));
            partsDt.Columns.Add(dc1);
            partsDt.Columns.Add(dc2);
            foreach (Control c in this.ProcessPanel.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox ck = c as CheckBox;
                    if (ck.Checked)
                    {
                        DataRow dr = partsDt.NewRow();
                        dr["part_no"] = ck.Tag.ToString();
                        dr["part_name"] = "" + ck.Text.ToString() + "";
                        partsDt.Rows.Add(dr);
                    }
                }
            }
            if (partsDt.Rows.Count <= 0)
            {
                MessageBox.Show("Please select a part！");
                return;
            }
            SetDefault(textOrder.Text, comboBox2.Text, partsDt);
        }
        //select all
        private void button7_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.ProcessPanel.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox ck = c as CheckBox;
                    ck.Checked = true;
                }
            }
        }
        //deselect all
        private void button8_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.ProcessPanel.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox ck = c as CheckBox;
                    ck.Checked = false;
                }
            }
        }
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > 1)
            {
                if (e.Value == null)
                {
                    return;
                }
                if (decimal.Parse(e.Value.ToString()) < 0)
                {
                    e.CellStyle.ForeColor = Color.Red;
                }
            }
        }
        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            foreach (Control c in this.ProcessPanel.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox ck = c as CheckBox;
                    ck.Checked = false;
                }
            }
            DataTable dtJson = GetDefault(textOrder.Text, comboBox2.Text);
            if (dtJson.Rows.Count > 0)
            {
                string part_name = dtJson.Rows[0]["part_name"].ToString();
                DataTable partsDt = new DataTable();
                DataColumn dc1 = new DataColumn("part_no", Type.GetType("System.String"));
                DataColumn dc2 = new DataColumn("part_name", Type.GetType("System.String"));
                partsDt.Columns.Add(dc1);
                partsDt.Columns.Add(dc2);
                foreach (Control c in this.ProcessPanel.Controls)
                {
                    if (c is CheckBox)
                    {
                        CheckBox ck = c as CheckBox;
                        if (part_name.Contains(ck.Text))
                        {
                            ck.Checked = true;
                        }
                    }
                }
            }
        }
        #endregion

        #region tabPage Daily supporting report
        //Query based on a single work order
        private void button6_Click(object sender, EventArgs e)
        {
            txtCombNo2.Text = "";
            txtInaSingleNo2.Text = "";
            ProcessPanelDay.Controls.Clear();
            string order_no = textArtDay.Text;
            dataGridView4.DataSource = null;
            DataTable dtJson = GetArtPart(order_no);

            if (dtJson == null || dtJson.Rows.Count <= 0)
            {
                MessageHelper.ShowErr(this, "Part of the part name or part number or ART of the work order is empty");
                return;
            }
            textOrderDay.Text = dtJson.Rows[0]["ART_NO"].ToString();  //ART
            for (int i = 0; i < dtJson.Rows.Count; i++)
            {
                CheckBox processCB = new CheckBox();
                processCB.Text = dtJson.Rows[i]["PART_NAME"].ToString();
                processCB.Tag = dtJson.Rows[i]["PART_NO"].ToString();
                processCB.AutoSize = true;
                processCB.Font = new Font("微软雅黑", 13F);
                processCB.Margin = new Padding(20, 2, 0, 0);
                this.ProcessPanelDay.Controls.Add(processCB);
            }
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("vOrder_No", textArtDay.Text);
            string ret2 = WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_WOO_WareHouseReportServer", "SelectSalesOrderByComnNo", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["IsSuccess"]))
            {
                string json2 = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["RetData"].ToString();
                if (!string.IsNullOrEmpty(json2))
                {
                    txtSalesOrder2.Text = json2;
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["ErrMsg"].ToString());
                txtSalesOrder2.Text = "";
            }
        }
        //Query based on combined work order
        private void btnSelectByCombNo2_Click(object sender, EventArgs e)
        {
            textArtDay.Text = "";
            txtInaSingleNo2.Text = "";
            ProcessPanelDay.Controls.Clear();
            dataGridView1.DataSource = null;
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("vOrder_No", txtCombNo2.Text);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_WOO_WareHouseReportServer", "SelectByCombNo", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                Dictionary<object, object> result = JsonConvert.DeserializeObject<Dictionary<object, object>>(json);
                string hedan = result["hedan"].ToString();
                string dt = result["dt"].ToString();
                DataTable dtResult = JsonHelper.GetDataTableByJson(dt);
                if (dtResult == null || dtResult.Rows.Count <= 0)
                {
                    MessageHelper.ShowErr(this, "Part of the part name or part number or ART of the work order is empty (check if it has been printed)");
                    return;
                }
                textOrderDay.Text = dtResult.Rows[0]["ART_NO"].ToString();  //ART
                txtInaSingleNo2.Text = hedan;
                for (int i = 0; i < dtResult.Rows.Count; i++)
                {
                    CheckBox processCB = new CheckBox();
                    processCB.Text = dtResult.Rows[i]["PART_NAME"].ToString();
                    processCB.Tag = dtResult.Rows[i]["PART_NO"].ToString();
                    processCB.AutoSize = true;
                    processCB.Font = new Font("微软雅黑", 13F);
                    processCB.Margin = new Padding(20, 2, 0, 0);
                    ProcessPanelDay.Controls.Add(processCB);
                }
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("vOrder_No", txtInaSingleNo2.Text);
                string ret2 = WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_WOO_WareHouseReportServer", "SelectSalesOrderByComnNo", Program.client.UserToken, JsonConvert.SerializeObject(dic));
                if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["IsSuccess"]))
                {
                    string json2 = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["RetData"].ToString();
                    if (!string.IsNullOrEmpty(json2))
                    {
                        txtSalesOrder2.Text = json2;
                    }
                }
                else
                {
                    MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["ErrMsg"].ToString());
                    txtSalesOrder2.Text = "";

                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());

            }
        }
        //Day Package Inquiry
        private void QueryDay_Click(object sender, EventArgs e)
        {
            dataGridView4.DataSource = null;
            DataTable partsDt = new DataTable();
            DataColumn dc1 = new DataColumn("part_no", Type.GetType("System.String"));
            DataColumn dc2 = new DataColumn("part_name", Type.GetType("System.String"));
            partsDt.Columns.Add(dc1);
            partsDt.Columns.Add(dc2);
            foreach (Control c in this.ProcessPanelDay.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox ck = c as CheckBox;
                    if (ck.Checked)
                    {
                        DataRow dr = partsDt.NewRow();
                        dr["part_no"] = ck.Tag.ToString();
                        dr["part_name"] = "\"" + ck.Text.ToString() + "\"";
                        partsDt.Rows.Add(dr);
                    }
                }
            }
            if (partsDt.Rows.Count <= 0)
            {
                MessageBox.Show("Please select a part！");
                return;
            }

            string type = comboBox3.Text;
            if (type.Equals("feed"))
            {
                //Delivery day supporting report
                Receipt_Day_Supprting_Report(partsDt, txtCombNo2.Text, txtInaSingleNo2.Text, textBox10.Text);
            }
            else if (type.Equals("Return material"))
            {
                //Return material day supporting report
                Return_Day_supporting_report(partsDt, txtCombNo2.Text, txtInaSingleNo2.Text, textBox10.Text);
            }
            else
            {
                MessageBox.Show("Please select the correct report format");
            }
        }
        //export
        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView4.Rows.Count > 0)
            {
                //single number
                string order_id = "";
                string orderNo = "";
                order_id = textArtDay.Text.ToString();
                orderNo = textArtDay.Text.ToString();
                if (string.IsNullOrEmpty(order_id))
                {
                    order_id = txtInaSingleNo2.Text;
                    orderNo = txtCombNo2.Text;
                }
                //get art
                string art = textOrderDay.Text.Trim();
                //get shoe name
                string shoeTypeName = GetShoeTyesName(art);
                //Get the manufacturer
                string processing_unit = GetProcessingUnit(order_id);


                string Ldate = LastTimeDay.Text.Trim().ToString();
                string Fdate = FristTimeDay.Text.Trim().ToString();
                string date = Fdate + " - " + Ldate;
                string type = comboBox3.Text.ToString();
                if (type.Equals("feed"))
                {
                    ExtelMyself("Delivery day report", dataGridView4, art, shoeTypeName, processing_unit, date, orderNo, txtSalesOrder2.Text);
                }
                else if (type.Equals("Return material"))
                {
                    ExtelMyself("Return material day report", dataGridView4, art, shoeTypeName, processing_unit, date, orderNo, txtSalesOrder2.Text);
                }
                else
                {
                    MessageBox.Show("Unable to specify supporting report type");
                }
            }
        }
        //set as default widget
        private void button17_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textArtDay.Text))
            {
                MessageBox.Show("Please enter a master ticket！");
            }
            if (string.IsNullOrEmpty(textOrderDay.Text))
            {
                MessageBox.Show("ART cannot be empty！");
            }
            if (string.IsNullOrEmpty(comboBox3.Text))
            {
                MessageBox.Show("Please select feed/return");
            }
            DataTable partsDt = new DataTable();
            DataColumn dc1 = new DataColumn("part_no", Type.GetType("System.String"));
            DataColumn dc2 = new DataColumn("part_name", Type.GetType("System.String"));
            partsDt.Columns.Add(dc1);
            partsDt.Columns.Add(dc2);
            foreach (Control c in this.ProcessPanelDay.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox ck = c as CheckBox;
                    if (ck.Checked)
                    {
                        DataRow dr = partsDt.NewRow();
                        dr["part_no"] = ck.Tag.ToString();
                        dr["part_name"] = "" + ck.Text.ToString() + "";
                        partsDt.Rows.Add(dr);
                    }
                }
            }
            if (partsDt.Rows.Count <= 0)
            {
                MessageBox.Show("Please select a part！");
                return;
            }
            SetDefault(textOrderDay.Text, comboBox3.Text, partsDt);
        }
        //select all
        private void button5_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.ProcessPanelDay.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox ck = c as CheckBox;
                    ck.Checked = true;
                }
            }
        }
        //deselect all
        private void button9_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.ProcessPanelDay.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox ck = c as CheckBox;
                    ck.Checked = false;
                }
            }
        }
        private void comboBox3_TextChanged(object sender, EventArgs e)
        {
            foreach (Control c in this.ProcessPanelDay.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox ck = c as CheckBox;
                    ck.Checked = false;
                }
            }
            DataTable dtJson = GetDefault(textOrderDay.Text, comboBox3.Text);
            if (dtJson.Rows.Count > 0)
            {
                string part_name = dtJson.Rows[0]["part_name"].ToString();
                DataTable partsDt = new DataTable();
                DataColumn dc1 = new DataColumn("part_no", Type.GetType("System.String"));
                DataColumn dc2 = new DataColumn("part_name", Type.GetType("System.String"));
                partsDt.Columns.Add(dc1);
                partsDt.Columns.Add(dc2);
                foreach (Control c in this.ProcessPanelDay.Controls)
                {
                    if (c is CheckBox)
                    {
                        CheckBox ck = c as CheckBox;
                        if (part_name.Contains(ck.Text))
                        {
                            ck.Checked = true;
                        }
                    }
                }
            }
        }
        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            FristTimeDay.Text = dateTimePicker3.Value.ToString("yyyy/MM/dd");
        }
        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            FristTimeDay.Text = dateTimePicker3.Value.ToString("yyyy/MM/dd");
        }
        #endregion

        #region tabPage 明细表
        private void button1_Click(object sender, EventArgs e)
        {
            txtCombNo3.Text = "";
            txtInaSingleNo3.Text = "";
            textBox1.Text = "";
            Dictionary<string, string> p = new Dictionary<string, string>();
            string order_no = textBox3.Text;
            p.Add("vOrder_No", order_no);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_WOO_WareHouseReportServer", "GetArt", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                textBox1.Text = json;
                string ret2 = WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_WOO_WareHouseReportServer", "SelectSalesOrderByComnNo", Program.client.UserToken, JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["IsSuccess"]))
                {
                    string json2 = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["RetData"].ToString();
                    if (!string.IsNullOrEmpty(json2))
                    {
                        txtSalesOrder3.Text = json2;
                    }
                }
                else
                {
                    MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["ErrMsg"].ToString());
                    txtSalesOrder3.Text = "";
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void btnSelectByCombNo3_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            Dictionary<string, string> p = new Dictionary<string, string>();
            string order_no = txtCombNo3.Text;
            p.Add("vOrder_No", order_no);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_WOO_WareHouseReportServer", "GetArtInaSingleNo", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dt = JsonHelper.GetDataTableByJson(json);
                if (dt.Rows.Count > 0)
                {
                    txtInaSingleNo3.Text = dt.Rows[0][1].ToString();
                    textBox1.Text = dt.Rows[0][0].ToString();
                }
                else
                {
                    MessageHelper.ShowErr(this, "No related information found");
                }
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("vOrder_No", txtInaSingleNo3.Text);
                string ret2 = WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_WOO_WareHouseReportServer", "SelectSalesOrderByComnNo", Program.client.UserToken, JsonConvert.SerializeObject(dic));
                if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["IsSuccess"]))
                {
                    string json2 = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["RetData"].ToString();
                    if (!string.IsNullOrEmpty(json2))
                    {
                        txtSalesOrder3.Text = json2;
                    }
                }
                else
                {
                    MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["ErrMsg"].ToString());

                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = null;
            string order_no = textBox3.Text;
            string first_time = textBox4.Text;
            string last_time = textBox5.Text;
            string warehouse = Regex.Split(textBox6.Text, "\\s+", RegexOptions.IgnoreCase)[0];
            string type = Regex.Split(comboBox1.Text, "\\s+", RegexOptions.IgnoreCase)[0];
            string unit = Regex.Split(textBox7.Text, "\\s+", RegexOptions.IgnoreCase)[0];
            string train = textBox2.Text;
            Dictionary<string, object> p = new Dictionary<string, object>();
            if (string.IsNullOrEmpty(txtInaSingleNo3.Text))
            {
                p.Add("order_no", order_no);
            }
            else
            {
                p.Add("order_no", txtCombNo3.Text);
            }
            p.Add("first_time", first_time);
            p.Add("last_time", last_time);
            p.Add("unit", unit);
            p.Add("ware_house", warehouse);
            p.Add("type", type);
            p.Add("train", train);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_WOO_WareHouseReportServer", "QueryDetailList", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable jsonDt = JsonHelper.GetDataTableByJson(json);
                DataRow totaldgvRow2 = TotalRow.GetTotalRow(dataTable: jsonDt) ?? throw new ArgumentNullException(paramName: "TotalRow.GetTotalRow(dataTablePoint)");
                totaldgvRow2[column: jsonDt.Columns[name: "size_no"]] = "total";
                jsonDt.Rows.Add(row: totaldgvRow2);

                dataGridView2.DataSource = jsonDt;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void DetailExport_Click(object sender, EventArgs e)
        {
            //single number
            string order_id = "";
            string orderNo = "";
            order_id = textBox3.Text.ToString();
            orderNo = textBox3.Text.ToString();
            if (!string.IsNullOrEmpty(txtInaSingleNo3.Text))
            {
                order_id = txtInaSingleNo3.Text.ToString();
                orderNo = txtCombNo3.Text;
            }
            //get art
            string art = textBox1.Text.Trim();
            //get shoe name
            string shoeTypeName = GetShoeTyesName(art);
            //Get the manufacturer
            string processing_unit = GetProcessingUnit(order_id);
            //Time to get the supporting report
            string date = DateTime.Now.ToLocalTime().ToString();

            ExtelMyself("list", dataGridView2, art, shoeTypeName, processing_unit, date, orderNo, txtSalesOrder3.Text);
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            textBox4.Text = dateTimePicker1.Value.ToString("yyyy/MM/dd");
        }
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            textBox5.Text = dateTimePicker2.Value.ToString("yyyy/MM/dd");
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
                e.Handled = true;
        }
        #endregion

        #region Multiple work order report
        private void button12_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox9.Text))
            {
                string ART = textBox9.Text;
                WareHouseReport_LoadOrder frm = new WareHouseReport_LoadOrder(ART);
                frm.DataChange += new WareHouseReport_LoadOrder.DataChangeHandler(DataChanged_order);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please enter ART!");
            }
        }
        private void button15_Click(object sender, EventArgs e)
        {
            DataTable TailorCheckDt = (DataTable)dataGridView5.DataSource;//unpack
            GetPart(TailorCheckDt);
        }
        private void button14_Click(object sender, EventArgs e)
        {
            dataGridView3.DataSource = null;
            DataTable partsDt = new DataTable();
            DataColumn dc1 = new DataColumn("part_no", Type.GetType("System.String"));
            DataColumn dc2 = new DataColumn("part_name", Type.GetType("System.String"));
            partsDt.Columns.Add(dc1);
            partsDt.Columns.Add(dc2);
            foreach (Control c in this.ProcessCB.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox ck = c as CheckBox;
                    if (ck.Checked)
                    {
                        DataRow dr = partsDt.NewRow();
                        dr["part_no"] = ck.Tag.ToString();
                        dr["part_name"] = "\"" + ck.Text.ToString() + "\"";
                        partsDt.Rows.Add(dr);
                    }
                }
            }
            if (partsDt.Rows.Count <= 0)
            {
                MessageBox.Show("Please select a part！");
                return;
            }

            string type = comboBox4.Text;
            if (type.Equals("feed"))
            {
                ReceiptMultiWorkOrderReport(partsDt);
            }
            else if (type.Equals("Return material"))
            {
                ReturnMultiWorkOrderReport(partsDt);
            }
            else
            {
                MessageBox.Show("Please select the correct report format");
            }
        }
        private void button13_Click(object sender, EventArgs e)
        {
            string a = "Supporting report.xls";
            ExportExcels.Export(a, dataGridView3);
        }
        private void button18_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(textArtDay.Text))
            //{
            //    MessageBox.Show("请输入主工单！");
            //}
            if (string.IsNullOrEmpty(textBox9.Text))
            {
                MessageBox.Show("ART cannot be empty！");
            }
            if (string.IsNullOrEmpty(comboBox4.Text))
            {
                MessageBox.Show("Please select feed/return");
            }
            DataTable partsDt = new DataTable();
            DataColumn dc1 = new DataColumn("part_no", Type.GetType("System.String"));
            DataColumn dc2 = new DataColumn("part_name", Type.GetType("System.String"));
            partsDt.Columns.Add(dc1);
            partsDt.Columns.Add(dc2);
            foreach (Control c in this.ProcessCB.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox ck = c as CheckBox;
                    if (ck.Checked)
                    {
                        DataRow dr = partsDt.NewRow();
                        dr["part_no"] = ck.Tag.ToString();
                        dr["part_name"] = "" + ck.Text.ToString() + "";
                        partsDt.Rows.Add(dr);
                    }
                }
            }
            if (partsDt.Rows.Count <= 0)
            {
                MessageBox.Show("Please select a part！");
                return;
            }
            SetDefault(textBox9.Text, comboBox4.Text, partsDt);
        }
        private void button11_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.ProcessCB.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox ck = c as CheckBox;
                    ck.Checked = true;
                }
            }
        }
        private void button10_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.ProcessCB.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox ck = c as CheckBox;
                    ck.Checked = false;
                }
            }
        }
        private void comboBox4_TextChanged(object sender, EventArgs e)
        {
            foreach (Control c in this.ProcessCB.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox ck = c as CheckBox;
                    ck.Checked = false;
                }
            }
            DataTable dtJson = GetDefault(textBox9.Text, comboBox4.Text);
            if (dtJson.Rows.Count > 0)
            {
                string part_name = dtJson.Rows[0]["part_name"].ToString();
                DataTable partsDt = new DataTable();
                DataColumn dc1 = new DataColumn("part_no", Type.GetType("System.String"));
                DataColumn dc2 = new DataColumn("part_name", Type.GetType("System.String"));
                partsDt.Columns.Add(dc1);
                partsDt.Columns.Add(dc2);
                foreach (Control c in this.ProcessCB.Controls)
                {
                    if (c is CheckBox)
                    {
                        CheckBox ck = c as CheckBox;
                        if (part_name.Contains(ck.Text))
                        {
                            ck.Checked = true;
                        }
                    }
                }
            }
        }



        #endregion
        #region  Total supporting report
        private void BindList(DataTable dt)
        {
            listBox1.Items.Clear();
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listBox1.Items.Add(dt.Rows[i]["se_id"].ToString());
                }
                if (dt.Rows.Count < 5)
                {
                    this.listBox1.Height = 30 * dt.Rows.Count + 30;
                }
                else
                {
                    this.listBox1.Height = 300;
                }
                this.listBox1.Visible = true;
            }

        }
        private DataTable getDataTable(string s)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("s", s);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_WOO_WareHouseReportServer", "LoadCombNotest", Program.client.UserToken, JsonConvert.SerializeObject(dic));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                return dtJson;

            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                return null;
            }
        }
        private int GetItemAt(ListBox listBox, int X, int Y)
        {
            int index = -1;
            for (int i = 0; i < listBox.Items.Count; i++)
            {
                System.Drawing.Rectangle r = listBox.GetItemRectangle(i);
                if (r.Contains(new Point(X, Y)))
                {
                    index = i;
                    break;
                }
            }
            return index;
        }
        private void F_WOO_WareHouseReportForm_Load(object sender, EventArgs e)
        {
            listBox1.DrawMode = DrawMode.OwnerDrawFixed;
            listBox1.ItemHeight = 30;
            listBox1.Visible = false;
            listBox2.DrawMode = DrawMode.OwnerDrawFixed;
            listBox2.ItemHeight = 30;
            listBox2.Visible = false;
            listBox3.DrawMode = DrawMode.OwnerDrawFixed;
            listBox3.ItemHeight = 30;
            listBox3.Visible = false;
        }
        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                this.txtCombNo.Text = listBox1.SelectedItem.ToString();
                listBox1.Visible = false;
            }
        }
        private void listBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Point m = new Point(e.X, e.Y);
            int index = GetItemAt(this.listBox1, e.X, e.Y);
            if (this.listBox1.SelectedItems.Count > 0 && this.listBox1.SelectedIndex != index)
            {
                this.listBox1.SetSelected(this.listBox1.SelectedIndex, false);
            }
            if (index != -1 && this.listBox1.SelectedIndex != index)
            {
                this.listBox1.SetSelected(index, true);
            }
        }
        private void listBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.listBox1.SelectedItems.Count > 0)
            {
                this.listBox1.Text = this.listBox1.SelectedItems[0].ToString();
                this.listBox1.Visible = false;
                this.txtCombNo.Focus();
            }
        }
        private void txtCombNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down && this.listBox1.Visible)
            {
                this.listBox1.Focus();
                if (this.listBox1.SelectedItems.Count > 0)
                {
                    this.listBox1.SetSelected(this.listBox1.SelectedIndex, false);
                }
                if (this.listBox1.Items.Count > 0)
                {
                    this.listBox1.SetSelected(0, true);
                }
            }
        }
        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && this.listBox1.Visible && this.listBox1.SelectedItems.Count > 0)
            {
                this.txtCombNo.Text = this.listBox1.SelectedItems[0].ToString();
                this.listBox1.Visible = false;
                this.txtCombNo.Focus();
            }
        }
        private void txtCombNo_TextChanged(object sender, EventArgs e)
        {
            if (this.txtCombNo.Text.Trim() != "")
            {
                DataTable dt = getDataTable(this.txtCombNo.Text.Trim());
                BindList(dt);
                this.listBox1.Visible = true;
            }
            else
            {
                this.listBox1.Items.Clear();
                this.listBox1.Visible = false;
            }
        }
        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            listBox1.DrawMode = DrawMode.OwnerDrawFixed;
            e.DrawBackground();
            Brush myBrush = Brushes.Black;
            FontFamily fontFamily = new FontFamily("宋体");
            System.Drawing.Font myFont = new Font(fontFamily, 10);
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                if (e.Index > -1)
                {
                    e.Graphics.DrawString(listBox1.Items[e.Index].ToString(), myFont, Brushes.White, e.Bounds, StringFormat.GenericDefault);
                }
            }
            else
            {
                if (e.Index > -1)
                {
                    e.Graphics.DrawString(listBox1.Items[e.Index].ToString(), myFont, myBrush, e.Bounds, StringFormat.GenericDefault);
                }
            }
            e.DrawFocusRectangle();
        }
        #endregion

        #region Daily supporting report
        private void BindList2(DataTable dt)
        {
            listBox2.Items.Clear();
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listBox2.Items.Add(dt.Rows[i]["se_id"].ToString());
                }
                if (dt.Rows.Count < 5)
                {
                    this.listBox2.Height = 30 * dt.Rows.Count + 30;
                }
                else
                {
                    this.listBox2.Height = 300;
                }
                this.listBox2.Visible = true;
            }
        }
        #endregion
        private void BindList3(DataTable dt)
        {
            listBox3.Items.Clear();
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listBox3.Items.Add(dt.Rows[i]["se_id"].ToString());
                }
                if (dt.Rows.Count < 5)
                {
                    this.listBox3.Height = 30 * dt.Rows.Count + 30;
                }
                else
                {
                    this.listBox3.Height = 300;
                }
                this.listBox3.Visible = true;
            }
        }
        private void listBox2_DrawItem(object sender, DrawItemEventArgs e)
        {
            listBox2.DrawMode = DrawMode.OwnerDrawFixed;
            e.DrawBackground();
            Brush myBrush = Brushes.Black;
            FontFamily fontFamily = new FontFamily("宋体");
            System.Drawing.Font myFont = new Font(fontFamily, 10);
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                if (e.Index > -1)
                {
                    e.Graphics.DrawString(listBox2.Items[e.Index].ToString(), myFont, Brushes.White, e.Bounds, StringFormat.GenericDefault);
                }
            }
            else
            {
                if (e.Index > -1)
                {
                    e.Graphics.DrawString(listBox2.Items[e.Index].ToString(), myFont, myBrush, e.Bounds, StringFormat.GenericDefault);
                }
            }
            e.DrawFocusRectangle();
        }

        private void listBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && this.listBox2.Visible && this.listBox2.SelectedItems.Count > 0)
            {
                this.txtCombNo2.Text = this.listBox2.SelectedItems[0].ToString();
                this.listBox2.Visible = false;
                this.txtCombNo2.Focus();
            }
        }

        private void listBox2_MouseMove(object sender, MouseEventArgs e)
        {
            Point m = new Point(e.X, e.Y);
            int index = GetItemAt(this.listBox2, e.X, e.Y);
            if (this.listBox2.SelectedItems.Count > 0 && this.listBox2.SelectedIndex != index)
            {
                this.listBox2.SetSelected(this.listBox2.SelectedIndex, false);
            }
            if (index != -1 && this.listBox2.SelectedIndex != index)
            {
                this.listBox2.SetSelected(index, true);
            }
        }

        private void listBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                this.txtCombNo2.Text = listBox2.SelectedItem.ToString();
                listBox2.Visible = false;
            }
        }

        private void listBox2_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.listBox2.SelectedItems.Count > 0)
            {
                this.listBox2.Text = this.listBox2.SelectedItems[0].ToString();
                this.listBox2.Visible = false;
                this.txtCombNo2.Focus();
            }
        }

        private void txtCombNo2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down && this.listBox2.Visible)
            {
                this.listBox2.Focus();
                if (this.listBox2.SelectedItems.Count > 0)
                {
                    this.listBox2.SetSelected(this.listBox2.SelectedIndex, false);
                }
                if (this.listBox2.Items.Count > 0)
                {
                    this.listBox2.SetSelected(0, true);
                }
            }
        }

        private void txtCombNo2_TextChanged(object sender, EventArgs e)
        {
            if (this.txtCombNo2.Text.Trim() != "")
            {
                DataTable dt = getDataTable(this.txtCombNo2.Text.Trim());
                BindList2(dt);
                this.listBox2.Visible = true;
            }
            else
            {
                this.listBox2.Items.Clear();
                this.listBox2.Visible = false;
            }
        }

        private void dataGridView4_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > 1)
            {
                if (e.Value == null)
                {
                    return;
                }
                if (decimal.Parse(e.Value.ToString()) < 0)
                {
                    e.CellStyle.ForeColor = Color.Red;
                }
            }
        }

        private void dataGridView3_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > 1)
            {
                if (e.Value == null)
                {
                    return;
                }
                if (decimal.Parse(e.Value.ToString()) < 0)
                {
                    e.CellStyle.ForeColor = Color.Red;
                }
            }
        }

        private void listBox3_DrawItem(object sender, DrawItemEventArgs e)
        {
            listBox3.DrawMode = DrawMode.OwnerDrawFixed;
            e.DrawBackground();
            Brush myBrush = Brushes.Black;
            FontFamily fontFamily = new FontFamily("宋体");
            System.Drawing.Font myFont = new Font(fontFamily, 10);
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                if (e.Index > -1)
                {
                    e.Graphics.DrawString(listBox3.Items[e.Index].ToString(), myFont, Brushes.White, e.Bounds, StringFormat.GenericDefault);
                }
            }
            else
            {
                if (e.Index > -1)
                {
                    e.Graphics.DrawString(listBox3.Items[e.Index].ToString(), myFont, myBrush, e.Bounds, StringFormat.GenericDefault);
                }
            }
            e.DrawFocusRectangle();
        }

        private void listBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && this.listBox3.Visible && this.listBox3.SelectedItems.Count > 0)
            {
                this.txtCombNo3.Text = this.listBox3.SelectedItems[0].ToString();
                this.listBox3.Visible = false;
                this.txtCombNo3.Focus();
            }
        }

        private void listBox3_MouseClick(object sender, MouseEventArgs e)
        {
            if (listBox3.SelectedItem != null)
            {
                this.txtCombNo3.Text = listBox3.SelectedItem.ToString();
                listBox3.Visible = false;
            }
        }

        private void listBox3_MouseMove(object sender, MouseEventArgs e)
        {
            Point m = new Point(e.X, e.Y);
            int index = GetItemAt(this.listBox3, e.X, e.Y);
            if (this.listBox3.SelectedItems.Count > 0 && this.listBox3.SelectedIndex != index)
            {
                this.listBox3.SetSelected(this.listBox3.SelectedIndex, false);
            }
            if (index != -1 && this.listBox3.SelectedIndex != index)
            {
                this.listBox3.SetSelected(index, true);
            }
        }

        private void listBox3_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.listBox3.SelectedItems.Count > 0)
            {
                this.listBox3.Text = this.listBox3.SelectedItems[0].ToString();
                this.listBox3.Visible = false;
                this.txtCombNo3.Focus();
            }
        }

        private void txtCombNo3_TextChanged(object sender, EventArgs e)
        {
            if (this.txtCombNo3.Text.Trim() != "")
            {
                DataTable dt = getDataTable(this.txtCombNo3.Text.Trim());
                BindList3(dt);
                this.txtCombNo3.Visible = true;
            }
            else
            {
                this.listBox3.Items.Clear();
                this.listBox3.Visible = false;
            }
        }

        private void txtCombNo3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down && this.listBox3.Visible)
            {
                this.listBox3.Focus();
                if (this.listBox3.SelectedItems.Count > 0)
                {
                    this.listBox3.SetSelected(this.listBox3.SelectedIndex, false);
                }
                if (this.listBox3.Items.Count > 0)
                {
                    this.listBox3.SetSelected(0, true);
                }
            }
        }
    }
}
