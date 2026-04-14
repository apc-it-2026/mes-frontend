using AutocompleteMenuNS;
using MaterialSkin.Controls;
using SJeMES_Control_Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace F_TMS_TierMeeting3_Main
{
    public partial class ThirdEfficiencyForm : MaterialForm
    {
        DataTable dtJson = new DataTable();


        public ThirdEfficiencyForm(string dept,string time)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.dataGridView1.AutoGenerateColumns = false;
            textBox1.Text = dept;
            dateTimePicker1.Text = time;
        }

        private void ThirdEfficiencyForm_Load(object sender, EventArgs e)
        {
            SetUI();
            LoadQueryItem();

            //if (!string.IsNullOrEmpty(Interface.plant))
            //{
            //    textBox1.Text = Interface.plant;
            //    dateTimePicker1.Text = Interface.date;
            //}
            //else
            //{
            //    try
            //    {
            //        string ret1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.Efficiency_KanbanServer", "QueryPlant", Program.client.UserToken, string.Empty);
            //        if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
            //        {
            //            string plant = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["RetData"].ToString();
            //            textBox1.Text = plant;
            //        }
            //        else
            //        {
            //            SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["ErrMsg"].ToString());
            //        }
            //        string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.Efficiency_KanbanServer", "QueryWorkDate", Program.client.UserToken, string.Empty);
            //        if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            //        {
            //            string date = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
            //            dateTimePicker1.Text = date;
            //        }
            //        else
            //        {
            //            SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            //        }
                  
            //    }
            //    catch (Exception ex)
            //    {
            //        SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            //    }
            //}
            GetData(dateTimePicker1.Text, textBox1.Text);
        }
        private void LoadQueryItem()
        {
            var items1 = new List<AutocompleteItem>();
            string ret1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.ProductionDashBoardServer", "LoadOrg", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject("noParam"));
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
            comboBox3.DataSource = items1;

            var items2 = new List<AutocompleteItem>();
            string ret2 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.Efficiency_KanbanServer", "Load_Dept", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject("noParam"));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                items2.Add(new MulticolumnAutocompleteItem(new[] { "" }, ""));
                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {
                    items2.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i - 1]["UDF07"].ToString() }, dtJson.Rows[i - 1]["UDF07"].ToString()));
                }
            }
            comboBox2.DataSource = items2;

            var items4 = new List<AutocompleteItem>();
            string ret4 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.ProductionDashBoardServer", "LoadRoutNo", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject("noParam"));
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
            comboBox1.DataSource = items4;

        }

       
        private void GetData(string  date,string plant)
        {
            try
            {
                Dictionary<string, Object> p = new Dictionary<string, object>();
                p.Add("date",date);
                p.Add("plant", plant);
                p.Add("org_id", string.IsNullOrWhiteSpace(comboBox3.Text) ? comboBox3.Text : comboBox3.Text.Split('|')[0]);
                p.Add("process_no", string.IsNullOrWhiteSpace(comboBox1.Text) ? comboBox1.Text : comboBox1.Text.Split('|')[0]);
                p.Add("DEPT", string.IsNullOrWhiteSpace(comboBox2.Text) ? comboBox2.Text : comboBox2.Text.Split('|')[0]);
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "TierMeeting", "TierMeeting.Controllers.TierMeetingServer", "ThreeEfficiency_Query", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                    decimal sum1 = 0;
                    decimal sum2 = 0;
                    decimal sum3 = 0;
                    decimal sum4 = 0;
                    decimal sum5 = 0;
                    decimal sum6 = 0;
                    decimal sum7 = 0;
                    decimal sum8 = 0;
                    decimal sum9 = 0;
                    decimal avg10 = 0;
                    decimal avg11 = 0;
                    decimal avg12 = 0;
                    for (int i = 0; i < dtJson.Rows.Count; i++)
                    {
                        decimal label_qty = string.IsNullOrEmpty(dtJson.Rows[i]["label_qty"].ToString()) ? 0 : decimal.Parse(dtJson.Rows[i]["label_qty"].ToString());
                        decimal work_qty = string.IsNullOrEmpty(dtJson.Rows[i]["WORK_QTY"].ToString()) ? 0 : decimal.Parse(dtJson.Rows[i]["WORK_QTY"].ToString());
                        decimal JOCKEY_QTY = string.IsNullOrEmpty(dtJson.Rows[i]["JOCKEY_QTY"].ToString()) ? 0 : decimal.Parse(dtJson.Rows[i]["JOCKEY_QTY"].ToString());
                        decimal PLURIPOTENT_WORKER = string.IsNullOrEmpty(dtJson.Rows[i]["PLURIPOTENT_WORKER"].ToString()) ? 0 : decimal.Parse(dtJson.Rows[i]["PLURIPOTENT_WORKER"].ToString());
                        decimal OMNIPOTENT_WORKER = string.IsNullOrEmpty(dtJson.Rows[i]["OMNIPOTENT_WORKER"].ToString()) ? 0 : decimal.Parse(dtJson.Rows[i]["OMNIPOTENT_WORKER"].ToString());
                        decimal Water_Spider = string.IsNullOrEmpty(dtJson.Rows[i]["Water_Spider"].ToString()) ? 0 : decimal.Parse(dtJson.Rows[i]["Water_Spider"].ToString());
                        decimal sum_hours = string.IsNullOrEmpty(dtJson.Rows[i]["sum_hours"].ToString()) ? 0 : decimal.Parse(dtJson.Rows[i]["sum_hours"].ToString());
                        decimal TRANIN_HOURS = string.IsNullOrEmpty(dtJson.Rows[i]["TRANIN_HOURS"].ToString()) ? 0 : decimal.Parse(dtJson.Rows[i]["TRANIN_HOURS"].ToString());
                        decimal TRANOUT_HOURS = string.IsNullOrEmpty(dtJson.Rows[i]["TRANOUT_HOURS"].ToString()) ? 0 : decimal.Parse(dtJson.Rows[i]["TRANOUT_HOURS"].ToString());
                        sum1 += label_qty;
                        sum2 += work_qty;
                        sum3 += JOCKEY_QTY;
                        sum4 += PLURIPOTENT_WORKER;
                        sum5 += OMNIPOTENT_WORKER;
                        sum6 += Water_Spider;
                        sum7 += sum_hours;
                        sum8 += TRANIN_HOURS;
                        sum9 += TRANOUT_HOURS;
                        if (i>= dtJson.Rows.Count-1)
                        {
                            if (sum2!=0&&sum7!=0&&sum6!=0&&sum3!=0)
                            {
                                avg10 = decimal.Round((sum2/ (sum7)), 2);
                            }
                            if (sum1 != 0 && sum7 != 0 && sum6 != 0 && sum3 != 0)
                            {
                                avg11 = decimal.Round((sum1 / (sum7)), 2);
                            }
                            if (sum2!=0)
                            {
                                avg12 = decimal.Round(avg11/avg10*100, 2);
                            }
                            
                        }
                    }
                    //排序
                    DataTable dd = dtJson.Clone();
                    dd = dtJson.Rows.Cast<DataRow>().OrderBy(r => r["PPHPersen"].ToDecimal()).CopyToDataTable();

                    DataRow row = dd.NewRow();
                    row["label_qty"] = sum1;
                    row["WORK_QTY"] = sum2;
                    row["JOCKEY_QTY"] = sum3;
                    row["PLURIPOTENT_WORKER"] = sum4;
                    row["OMNIPOTENT_WORKER"] = sum5;
                    row["Water_Spider"] = sum6;
                    row["sum_hours"] = sum7;
                    row["TRANIN_HOURS"] = sum8;
                    row["TRANOUT_HOURS"] = sum9;
                    row["TARGETPPH"] = avg10;
                    row["PPH"] = avg11;
                    if (sum2 != 0)
                    {
                        row["PPHPersen"] = avg12;
                    }
                    else
                    {
                        row["PPHPersen"] =string.Empty;
                    }
                    dd.Rows.Add(row);
                    dtJson = dd;
                    dataGridView1.DataSource = dd;
                    dataGridView1.Update();
                    dataGridView1.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dataGridView1_RowPostPaint);
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

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            for (int i = 0; i < dtJson.Rows.Count; i++)
            {
                decimal acheivementQty = 0;
                string work_qty = string.IsNullOrEmpty(dtJson.Rows[i]["work_qty"].ToString()) ? "0" : dtJson.Rows[i]["work_qty"].ToString();
                string label_qty = string.IsNullOrEmpty(dtJson.Rows[i]["label_qty"].ToString()) ? "0" : dtJson.Rows[i]["label_qty"].ToString();
                decimal result;
                if (!work_qty.Equals("0") && decimal.TryParse(work_qty, out result) && decimal.TryParse(label_qty, out result))
                {
                    acheivementQty = decimal.Parse(label_qty) / decimal.Parse(work_qty) * 100;
                    if (acheivementQty >= 100)
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LawnGreen;
                    }
                    else if (acheivementQty >= 95 && acheivementQty < 100)
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;

                    }
                    else if (acheivementQty < 95)
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    }
                }
                else
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Aqua;
                }

            }

        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    GetData(dateTimePicker1.Text,textBox1.Text);
        //}

        /// <summary>
        /// this is call other dll form demo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void button2_Click(object sender, EventArgs e)
        //{        
        //    Assembly assembly = null;
        //    string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6);
        //    assembly = Assembly.LoadFrom(path + @"\" + "ThirdEfficiency_Kanban" + ".dll");
        //    Type type = assembly.GetType("ThirdEfficiency_Kanban.Interface");
        //    object instance = Activator.CreateInstance(type);
        //    MethodInfo mi = type.GetMethod("RunApp");
        //    object[] args = new object[1];
        //     args[0] = Program.client;
        //    object obj = mi.Invoke(instance, args);
        //}


        private void SetUI()
        {
            int fromHeight = this.Height;
            dataGridView1.ColumnHeadersHeight= Convert.ToInt32(fromHeight / 25);
            dataGridView1.RowTemplate.Height = Convert.ToInt32(fromHeight / 25);//datagridview行高
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("宋体", (float)fromHeight / 60, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //datagridview字体高度
            dataGridView1.DefaultCellStyle.Font = new System.Drawing.Font("宋体", (float)fromHeight / 55, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            GetData(dateTimePicker1.Text, textBox1.Text);
        }

        //private void materialRaisedButton2_Click_1(object sender, EventArgs e)
        //{
        //    GetData(dateTimePicker1.Text, textBox1.Text);
        //}

        //private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    int index = dataGridView1.CurrentRow.Index;
        //    if (index > -1 && dataGridView1.Rows[index].Cells[0].Value != null)
        //    {
        //        Assembly assembly = null;
        //        string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6);
        //        assembly = Assembly.LoadFrom(path + @"\" + "Production_Kanban" + ".dll");
        //        Type type = assembly.GetType("Production_Kanban.Interface");
        //        object instance = Activator.CreateInstance(type);
        //        MethodInfo mi = type.GetMethod("RunCustomize");
        //        object[] args = new object[3];
        //        args[0] = Program.client;
        //        args[1] = this.dataGridView1.Rows[index].Cells["Area"].Value;
        //        args[2] = dateTimePicker1.Text;
        //        object obj = mi.Invoke(instance, args);
        //    }
        //}

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            string a = "日产量报表.xls";
            ExportExcels(a, dataGridView1);
        }


        private void ExportExcels(string fileName, DataGridView myDGV)
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
                MessageBox.Show("无法创建Excel对象，可能您的机子未安装Excel");
                return;
            }
            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];//取得sheet1
                                                                                                                                  //写入标题
            for (int i = 0; i < myDGV.ColumnCount; i++)
            {
                worksheet.Cells[1, i + 1] = "'" + myDGV.Columns[i].HeaderText.ToString();
            }
            //写入数值
            for (int r = 0; r < myDGV.Rows.Count; r++)
            {
                for (int i = 0; i < myDGV.ColumnCount; i++)
                {
                    worksheet.Cells[r + 2, i + 1].NumberFormatLocal = "@";
                    worksheet.Cells[r + 2, i + 1] = myDGV.Rows[r].Cells[i].Value;
                }
                System.Windows.Forms.Application.DoEvents();
            }
            worksheet.Columns.EntireColumn.AutoFit();//列宽自适应
            if (saveFileName != "")
            {
                try
                {
                    workbook.Saved = true;
                    workbook.SaveCopyAs(saveFileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("导出文件时出错,文件可能正被打开！\n" + ex.Message);
                }
            }
            xlApp.Quit();
            GC.Collect();//强行销毁
            MessageBox.Show("保存成功", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

       
    }
}
