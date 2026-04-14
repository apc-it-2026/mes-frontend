using AutocompleteMenuNS;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KBN_SFC_DayLine_TrackOut_List
{
    public partial class DayLine_TrackOut_List_halftime : MaterialForm
    {

        private MaterialSkinManager materialSkinManager;

        public delegate void MyDelegate(string pageName);

        DataTable dtJson;

        Timer timer1 = new Timer();
        //You generate and run it
        private void MyMehod(string pageName)
        {
            Type type;
            Object obj;
            string className = this.GetType().FullName;
            type = Type.GetType(className);
            obj = System.Activator.CreateInstance(type);
            MethodInfo method = type.GetMethod(pageName, new Type[] { });
            object[] parameters = null;
            method.Invoke(obj, parameters);
        }

        public DayLine_TrackOut_List_halftime()
        {
            InitializeComponent();

            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

            this.WindowState = FormWindowState.Maximized;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void DashBoardForm_Load(object sender, EventArgs e)
        {
            this.tabPage4.Parent = null;
            this.tabPage3.Parent = null;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            LoadQueryItem();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(MaterialSkinManager.Themes.LIGHT, materialSkinManager, this);
            tabPage1_query();
            if (string.IsNullOrWhiteSpace(txtTimer.Text))
            {
                txtTimer.Text = "600";
            }
            timer1.Interval = int.Parse(txtTimer.Text.ToString()) * 1000;
            timer1.Enabled = true;
            timer1.Tick += new EventHandler(timer1_Tick);//Add event
        }

        private  void LoadQueryItem()
        {
            var items1 = new List<AutocompleteItem>();
            string ret1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.ProductionDashBoardServer", "LoadOrg", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject("noParam"));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                items1.Add(new MulticolumnAutocompleteItem(new[]{""},""));
                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {
                    items1.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i - 1]["company"].ToString()}, dtJson.Rows[i - 1]["company"].ToString()));
                }
            }
            comboBox1.DataSource = items1;

            var items2 = new List<AutocompleteItem>();
            string ret2 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.ProductionDashBoardServer", "LoadPlant", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject("noParam"));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                items2.Add(new MulticolumnAutocompleteItem(new[] { "" }, ""));
                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {
                    items2.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i - 1]["code"].ToString(), dtJson.Rows[i - 1]["org"].ToString() }, dtJson.Rows[i - 1]["code"].ToString()+"|"+ dtJson.Rows[i - 1]["org"].ToString()));
                }
            }
            comboBox2.DataSource = items2;



            var items3 = new List<AutocompleteItem>();
            string ret3 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.ProductionTargetsServer", "LoadSeDept", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject("noParam"));
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
            autocompleteMenu3.SetAutocompleteItems(items3);


            var items4 = new List<AutocompleteItem>();
            string ret4 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.ProductionDashBoardServer", "LoadRoutNo", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject("noParam"));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret4)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret4)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                items4.Add(new MulticolumnAutocompleteItem(new[] { "" }, ""));
                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {
                    items4.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i - 1]["rout_no"].ToString(), dtJson.Rows[i - 1]["rout_name_z"].ToString()}, dtJson.Rows[i - 1]["rout_no"].ToString()+"|"+ dtJson.Rows[i - 1]["rout_name_z"].ToString()));
                }
            }
            comboBox3.DataSource = items4;

        }


        private void timer1_Tick(object sender, EventArgs e)
        {

            tabPage1_query();

            //int index = tabControl1.SelectedIndex;
            //if (index == tabControl1.TabCount - 2)
            //{
            //    index = 0;
            //}
            //else
            //{
            //    index++;
            //}
            //this.tabControl1.SelectedIndex = index;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MyDelegate myDelegate = MyMehod;
            //string pageName = this.tabControl1.SelectedTab.Name + "_query";
            //myDelegate(pageName);
            //tabPage1_query();
        }



        public void tabPage1_query()
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView1.AutoGenerateColumns = false;
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
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vCompany", comboBox1.Text);
            p.Add("vPlant", string.IsNullOrWhiteSpace(comboBox2.Text) ? comboBox2.Text : comboBox2.Text.Split('|')[0]);
            p.Add("vDept", textBox3.Text);
            p.Add("vRountNo", string.IsNullOrWhiteSpace(comboBox3.Text) ? comboBox3.Text : comboBox3.Text.Split('|')[0]);
            p.Add("vType","V2");
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.ProductionDashBoardServer", "Query", Program.Client.UserToken,Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                dataGridView1.DataSource = dtJson.DefaultView;
                dataGridView1.Update();
                dataGridView1.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dataGridView1_RowPostPaint);
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }


        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            for (int i=0;i<dtJson.Rows.Count;i++)
            {
                decimal acheivementQty = 0;
                string str = dtJson.Rows[i]["Acheivement"].ToString().IndexOf("%") > 0 ?
                    dtJson.Rows[i]["Acheivement"].ToString().Substring(0, dtJson.Rows[i]["Acheivement"].ToString().IndexOf("%")) :
                    dtJson.Rows[i]["Acheivement"].ToString();
                decimal result;
                if (decimal.TryParse(str, out result))
                {
                    string total = string.IsNullOrEmpty(dtJson.Rows[i]["Total"].ToString())? "0":dtJson.Rows[i]["Total"].ToString();
                    decimal finishTotal = decimal.Parse(total);//* 100 * 100
                    decimal finishTarget = decimal.Parse(dtJson.Rows[i]["Target"].ToString()) * decimal.Parse(dtJson.Rows[i]["Ccheivement"].ToString());
                    acheivementQty = decimal.Round(finishTotal*100*100/ finishTarget,1);
                    dataGridView1.Rows[i].Cells["CurAcheivement"].Value = acheivementQty + "%";
                    if (acheivementQty >= 100)
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(184,250,64);

                    }
                    else if (acheivementQty > 90 && acheivementQty <= 99)
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.White;

                    }
                    else if (acheivementQty > 85 && acheivementQty <= 89)
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(252, 251, 62);

                    }
                    else if (acheivementQty <= 84)
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(254,205,208); 
                    }
                }
                else
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Aqua;
                }
               
            }
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

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            string a = "Daily hourly production report.xls";
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
            if (saveFileName.IndexOf(":") < 0) return; //was clicked to cancel
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            if (xlApp == null)
            {
                MessageBox.Show("Unable to create Excel object, maybe your machine does not have Excel installed");
                return;
            }
            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];//get sheet1
                                                                                                                                  //write title
            for (int i = 0; i < myDGV.ColumnCount; i++)
            {
                worksheet.Cells[1, i + 1] = "'"+myDGV.Columns[i].HeaderText.ToString();
            }
            //write value
            for (int r = 0; r < myDGV.Rows.Count; r++)
            {
                for (int i = 0; i < myDGV.ColumnCount; i++)
                {
                    worksheet.Cells[r + 2, i + 1].NumberFormatLocal = "@";
                    worksheet.Cells[r + 2, i + 1] = myDGV.Rows[r].Cells[i].Value;
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
                    MessageBox.Show("There was an error exporting the file, the file may be being opened！\n" + ex.Message);
                }
            }
            xlApp.Quit();
            GC.Collect();//forcibly destroyed
            MessageBox.Show("Successfully saved", "message notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            timer1_Tick(sender,e);
        }

        private void DashBoardForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Enabled = false;
            //timer1.Tick -= new EventHandler(timer1_Tick);//Add event
            timer1.Tick -= timer1_Tick;//Add event
        }

        /// <summary>
        /// search the line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void materialRaisedButton4_Click(object sender, EventArgs e)
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vCompany", comboBox1.Text);
            p.Add("vPlant", string.IsNullOrWhiteSpace(comboBox2.Text) ? comboBox2.Text : comboBox2.Text.Split('|')[0]);
            p.Add("vDept", textBox3.Text);
            p.Add("vRountNo", string.IsNullOrWhiteSpace(comboBox3.Text) ? comboBox3.Text : comboBox3.Text.Split('|')[0]);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.ProductionDashBoardServer", "QueryDayYield", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                dataGridView2.DataSource = dtJson.DefaultView;
                dataGridView2.Update();
                //dataGridView2.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dataGridView1_RowPostPaint);
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex > -1)
            {

            }
            else
            {

            }
        }

       
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dataGridView1.CurrentRow.Index;
            if (index > -1 && dataGridView1.Rows[index].Cells[0].Value != null)
            {
                Assembly assembly = null;
                string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6);
                assembly = Assembly.LoadFrom(path + @"\" + "Production_Kanban" + ".dll");
                Type type = assembly.GetType("Production_Kanban.Interface");
                object instance = Activator.CreateInstance(type);
                MethodInfo mi = type.GetMethod("RunCustomize");
                object[] args = new object[3];
                args[0] = Program.Client;
                args[1] = this.dataGridView1.Rows[index].Cells["scan_detpt"].Value;
                args[2] = string.Format(DateTime.Now.ToShortDateString(), "yyyy/MM/dd");
                object obj = mi.Invoke(instance, args);
            }
        }

        private void materialRaisedButton6_Click(object sender, EventArgs e)
        {
            this.listBox2.Items.Clear();
            string vPlantNo = string.IsNullOrWhiteSpace(comboBox5.Text.ToString()) ? comboBox5.Text.ToString() : comboBox5.Text.ToString().Split('|')[0];
            string vRoutNo = string.IsNullOrWhiteSpace(comboBox6.Text.ToString()) ? comboBox6.Text.ToString() : comboBox6.Text.ToString().Split('|')[0];
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vOrgNo", comboBox4.Text.ToString());
            p.Add("vPlantNo", vPlantNo);
            p.Add("vDeptNo", textBox1.Text.ToString());
            p.Add("vRoutNo", vRoutNo);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.WorkingHoursServer", "Query", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    this.listBox2.Items.Add(dtJson.Rows[i]["department_code"].ToString());
                }
                //dataGridView1.DataSource = dtJson.DefaultView;
                //dataGridView1.Update();
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
    }
}
