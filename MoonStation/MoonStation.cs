using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;
using System.IO;

namespace MoonStation
{
    public partial class MoonStation : MaterialForm
    {
        public Boolean isTitle = false;
        IList<object[]> data = null;
        private ExcelProcessor _currentExcelProcessor = null;
        public MoonStation()
        {
            InitializeComponent();

            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);

            this.WindowState = FormWindowState.Maximized;
            tabControl1.Height = Screen.GetBounds(this).Height - 70;
        }

        private void butFile_Click(object sender, EventArgs e)
        {
             void GetExcelData(string fileName)
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
                    MessageBox.Show(this, ex.Message);
                }
            }

            progressBar1.Value = 0;
            isTitle = true;
            string errs = "";
            this.dataGridView2.AutoGenerateColumns = false;
            if (dataGridView2 != null)
            {
                this.dataGridView2.Columns.Clear();
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
                        GetExcelData(Path.GetFullPath(filename));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, ex.Message);
                    }
                }
                dataGridView2.AllowUserToAddRows = false;
                if (data != null && data.Count > 0)
                {
                    int colNum = data[0].Length;
                    for (int i = 0; i < colNum; i++)
                    {
                        DataGridViewTextBoxColumn acCode = new DataGridViewTextBoxColumn();
                        if (i > 0 && i < colNum)
                        {
                            try
                            {
                                //data[0][i] = Convert.ToDateTime(data[0][i].ToString()).ToShortDateString();
                                acCode.Width = 100;
                            }
                            catch (Exception ex)
                            {
                                errs += "column:" + (i + 1) + "," + ex.Message + "\n";
                            }
                        }
                        acCode.Name = data[0][i].ToString();
                        acCode.HeaderText = data[0][i].ToString();
                        dataGridView2.Columns.Add(acCode);
                    }
                    dataGridView2.Rows.Add(data[1]);
                    for (int i = 2; i < data.Count; i++)
                    {
                        dataGridView2.Rows.Add(data[i]);
                        dataGridView2.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dataGridView_RowPostPaint);
                        //try
                        //{
                        //    if (!ValiDept(data[i][0].ToString().Trim().ToUpper()))
                        //    {
                        //        errs += "row:" + i + ",error department" + "\n";
                        //    }
                        //    else
                        //    {
                        //        data[i][0] = data[i][0].ToString().Trim().ToUpper();
                        //    }
                        //}
                        //catch(Exception)
                        //{
                        //    errs += "row:" + i + ",error department" + "\n";
                        //}

                        /*for (int j = 1; j < colNum; j++)
                        {
                            if (data[i][j] != null && data[i][j].ToString().Trim() != "")
                            {
                                try
                                {
                                    int qty = int.Parse(data[i][j].ToString().Trim());
                                }
                                catch (Exception ex)
                                {
                                    errs += "row:" + i + ",cloumn:" + (j + 1) + "," + ex.Message + "\n";
                                }
                            }
                            else
                            {
                                data[i][j] = "";
                            }
                        }*/
                    }
                }
            }
            if (errs != "")
            {
                MessageBox.Show(errs);
            }
        }

        private void butImport_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            isTitle = true;
            //btnFolder.Enabled = false;
            butFile.Enabled = false;
            butImport.Enabled = false;
            if (this.dataGridView2.Rows.Count >= 1)
            {
                try
                {
                    update_db();
                    butFile.Enabled = true;
                    butImport.Enabled = true;
                }
                catch (Exception ex)
                {
                    butFile.Enabled = true;
                    butImport.Enabled = true;
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
                }
            }
            else
            {
                butFile.Enabled = true;
                butImport.Enabled = true;
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("err-00001", Program.client, "", Program.client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void update_db()
        {
            string errs = "";
            DataTable tab = new DataTable();
            object[] cols = data[0];
            for (int i = 0; i < cols.Length; i++)
            {
                tab.Columns.Add(cols[i].ToString());
            }
            for (int i = 1; i < data.Count; i++)
            {
                DataRow dr = tab.NewRow();
                /* try
                 {
                     if (!ValiDept(data[i][0].ToString().Trim().ToUpper()))
                     {
                         errs += "row:" + i + ",error department" + "\n";
                     }
                     else
                     {
                         dr[0] = data[i][0].ToString().Trim().ToUpper();
                     }
                 }
                 catch (Exception)
                 {
                     errs += "row:" + i + ",error department" + "\n";
                 }*/
                progressBar1.Step = progressBar1.Maximum / cols.Length;
                for (int j = 0; j < cols.Length; j++)
                {
                    if (data[i][j] != null && data[i][j].ToString().Trim() != "")
                    {
                        try
                        {
                            //int qty = int.Parse(data[i][j].ToString().Trim());
                            dr[j] = data[i][j].ToString().Trim();
                            progressBar1.PerformStep();
                        }
                        catch (Exception ex)
                        {
                            errs += "row:" + i + ",cloumn:" + (j + 1) + "," + ex.Message + "\n";
                        }
                    }
                    else
                    {
                        dr[j] = "";
                    }
                }
                tab.Rows.Add(dr);
            }
            if (errs == "")
            {
                try
                {
                    UpLoad(tab);
                }
                catch (Exception ex)
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
                }
            }
            else
            {
                MessageBox.Show(errs);
            }
        }

        private void UpLoad(DataTable tab)
        {
            Dictionary<string, Object> d = new Dictionary<string, object>();
            d.Add("data", tab);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.SFC_MoonStationServer", "UpLoad",
                            Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(d));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("导入成功！", Program.client, "", Program.client.Language);
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
            }
            else
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("导入失败！", Program.client, "", Program.client.Language);
                string exceptionMsg = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString();
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg + exceptionMsg);
            }
        }

        /*private bool ValiDept(string d_dept)
        {
            bool isOk = false;
            Dictionary<string, Object> d = new Dictionary<string, object>();
            d.Add("vDDept", d_dept);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.DayTargetsServer", "ValiDept",
                            Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(d));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                isOk = bool.Parse(json);
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            return isOk;
        }*/

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

        private void butquery_Click(object sender, EventArgs e)
        {
            try
            {
                string sDept = tbDept.Text.Trim();
                string sPONumber = tbPONumber.Text.Trim();
                string sYear = dtpYear.Text;
                string sMoon = cbMoon.Text;

                Dictionary<string, Object> p = new Dictionary<string, object>();
                p.Add("vDept", sDept);
                p.Add("vPONumber", sPONumber);
                p.Add("vYear", sYear);
                p.Add("vMoon", sMoon);

                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.SFC_MoonStationServer", "Query", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    DataTable dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                    dataGridView3.DataSource = dt.DefaultView;
                    dataGridView3.Update();
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
}
