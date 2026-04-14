using AutocompleteMenuNS;
using MaterialSkin.Controls;
using Microsoft.Office.Interop.Excel;
using NewExportExcels;
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
using System.Drawing.Drawing2D;

namespace IE_Efficiency_Eval
{
    public partial class IE_Efficiency_Evaluation : MaterialForm
    {
        public Boolean isTitle = false;
        IList<object[]> data = null;
        private ExcelProcessor _currentExcelProcessor = null;
        DataTable dtJson1 = null;

        DataTable dtJson = null;

        public IE_Efficiency_Evaluation()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            lbl_total_records.Visible = false;

        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            query();
        }

        public void query()
        {
            //System.Drawing.Font a = new System.Drawing.Font("宋体", 12);
            //dgv_Summary.Font = a;//font

            Dictionary<string, Object> p = new Dictionary<string, object>();

            //p.Add("vOrg_code", comboBox1.Text.Trim().ToString().ToUpper().Split('|')[0]);//.Trim().ToString().ToUpper().Split('|')[0]
            //p.Add("vPlant_code", comboBox2.Text.Trim().ToString().ToUpper().Split('|')[0]);
            //p.Add("vDept_code", textBox1.Text);
            p.Add("vProcess", cbxPross.Text.ToString().ToUpper().Split('|')[0].Trim());
            p.Add("vLine", txtsearchLine.Text);
            p.Add("vBeginTime", dateTimePicker1.Text);
            p.Add("vEndTime", dateTimePicker2.Text);
            Cursor.Current = Cursors.WaitCursor;
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL,
                "KZ_RTLAPI", "KZ_RTLAPI.Controllers.IE_Final_BonusServer", "QueryIEEfficiencyReport", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p)); ;

            //string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL,
            //    "KZ_EDMAPI", "KZ_EDMAPI.Controllers.KPI_Criteria_Server", "LoadKPICriteria",

            Cursor.Current = Cursors.Default;
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dtJson.Rows.Count > 0)
                {

                    dgv_Summary.DataSource = dtJson;
                    AutosizeColumnChange();
                    lbl_total_records.Visible = true;
                    lbl_total_records.Text = $"Total Records : {dgv_Summary.RowCount}";

                }
                else
                {
                    dgv_Summary.Columns.Clear();
                    MessageBox.Show("No such data");
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

        }



        private void butIETargetFile_Click(object sender, EventArgs e)
        {
            isTitle = true;
            string errs = "";
            this.dgv_IETargets.AutoGenerateColumns = false;
            if (dgv_IETargets != null)
            {
                this.dgv_IETargets.Columns.Clear();
            }
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter = "EXCEL|*.xls*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (dgv_ImportList != null)
                {
                    data = new List<object[]>();
                    this.dgv_ImportList.Rows.Clear();
                }
                foreach (string filename in ofd.FileNames)
                {
                    try
                    {
                        this.dgv_ImportList.Rows.Add(filename);
                        dgv_ImportList.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dataGridView_RowPostPaint);
                        this.GetExcelData(Path.GetFullPath(filename));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, ex.Message);
                    }
                }
                dgv_IETargets.AllowUserToAddRows = false;
                if (data != null && data.Count > 0)
                {
                    int colNum = data[0].Length;
                    for (int i = 0; i < colNum; i++)
                    {
                        DataGridViewTextBoxColumn acCode = new DataGridViewTextBoxColumn();
                        //if (i > 0 && i < colNum)
                        //{
                        //    try
                        //    {
                        //        //data[0][i] = Convert.ToDateTime(data[0][i].ToString()).ToShortDateString();
                        //        //data[0][i] = Convert.ToDateTime(data[0][i].ToString()).ToString("yyyy/MM/dd");
                        //        acCode.Width = 68;
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        errs += "column:" + (i + 1) + "," + ex.Message + "\n";
                        //    }
                        //}
                        acCode.Name = data[0][i].ToString();
                        acCode.HeaderText = data[0][i].ToString();
                        dgv_IETargets.Columns.Add(acCode);
                    }
                    dgv_IETargets.Rows.Add(data[1]);
                    for (int i = 2; i < data.Count; i++)
                    {
                        dgv_IETargets.Rows.Add(data[i]);
                        dgv_IETargets.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dataGridView_RowPostPaint);


                        int len = data.Count;
                        for (int d = 1; d < len; d++)
                        {
                            if (data[d][2] != null && data[d][2].ToString().Trim() != "")
                            {
                                try
                                {
                                    decimal target = decimal.Parse(data[d][2].ToString().Trim());
                                }
                                catch (Exception ex)
                                {
                                    errs += "row:" + d + ",cloumn: 2," + ex.Message + "\n";
                                }
                            }
                            else
                            {
                                data[d][2] = "";
                            }
                        }


                    }
                }
            }
            if (errs != "")
            {
                MessageBox.Show(errs);
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
            System.Drawing.Rectangle headerBounds = new System.Drawing.Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
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
                    for (int i = 0; i < list.Count; i++)
                    {
                        string chk = "";
                        for (int j = 0; j < list[i].Length; j++)
                        {

                            object vl = list[i][j];

                            if (j == 0)
                                chk = list[i][j] == null ? "" : list[i][j].ToString();
                        }

                        //int chk = 0;
                        //foreach (string item in list[i])
                        //{

                        //}

                        if (chk != "")
                            data.Add(list[i]);


                    }

                    //data = list;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }

        private void butImport_Click(object sender, EventArgs e)
        {
            isTitle = true;
            //btnFolder.Enabled = false;
            butIETargetFile.Enabled = false;
            //butImport.Enabled = false;
            if (this.dgv_IETargets.Rows.Count >= 2)
            {
                try
                {
                    update_db();
                }
                catch (Exception ex)
                {
                    butIETargetFile.Enabled = true;
                    //butImport.Enabled = true;
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
                }
            }
            else
            {
                butIETargetFile.Enabled = true;
                //butImport.Enabled = true;
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("err-00001", Program.client, "", Program.client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
        private void UpLoad(System.Data.DataTable tab)
        {
            Dictionary<string, Object> d = new Dictionary<string, object>();
            d.Add("data", tab);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.IE_BonusEvalServer", "UpLoad",
                            Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(d));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("导入成功！", Program.client, "", Program.client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
            else
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("导入失败！", Program.client, "", Program.client.Language);
                string exceptionMsg = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString();
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg + exceptionMsg);
            }
        }
        private void update_db()
        {

            System.Data.DataTable dt = new System.Data.DataTable();
            foreach (DataGridViewColumn col in dgv_IETargets.Columns)
            {
                dt.Columns.Add(col.Name);
            }

            foreach (DataGridViewRow row in dgv_IETargets.Rows)
            {
                DataRow dRow = dt.NewRow();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dRow[cell.ColumnIndex] = cell.Value;
                }
                dt.Rows.Add(dRow);
            }

            try
            {

                UpLoad(StripEmptyRows(dt));
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }

        }

        private System.Data.DataTable StripEmptyRows(System.Data.DataTable dt)
        {
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                if (dt.Rows[i][1] == DBNull.Value || dt.Rows[i][1].ToString() == "")
                {
                    dt.Rows[i].Delete();
                }
            }
            dt.AcceptChanges();
            return dt;
        }

        private void btnSearchIE_Click(object sender, EventArgs e)
        {
            if (dgv_IETargets != null)
            {
                this.dgv_IETargets.Columns.Clear();
            }

            System.Drawing.Font a = new System.Drawing.Font("宋体", 12);
            dgv_Summary.Font = a;//font

            Dictionary<string, Object> p = new Dictionary<string, object>();

            p.Add("vIEtargetsearch", txtIEtargetsearch.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL,
                "KZ_MESAPI", "KZ_MESAPI.Controllers.IE_BonusEvalServer", "QueryIEtargetsearch",
            Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                System.Data.DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dtJson.Rows.Count > 0)
                {

                    dgv_IETargets.DataSource = dtJson;
                    AutosizeColumnChange();

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

        private void AutosizeColumnChange()
        {
            if (dgv_IETargets.Columns.Contains("MODEL"))   //  dgv_IEEffDetails
            {
                dgv_IETargets.Columns["MODEL"].Width = 200;
            }
            if (dgv_Summary.Columns.Contains("MODEL"))
            {
                dgv_Summary.Columns["MODEL"].Width = 220;
            }
            if (dgv_IEEffDetails.Columns.Contains("MODEL"))
            {
                dgv_IEEffDetails.Columns["MODEL"].Width = 220;
            }
            foreach(DataGridViewColumn Col in dgv_IEEffDetails.Columns)
            {
                Col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        #region Model Change

        private void butIEModelChangeFile_Click(object sender, EventArgs e)
        {

            isTitle = true;
            string errs = "";
            this.dgv_IEModelChange.AutoGenerateColumns = false;
            if (dgv_IEModelChange != null)
            {
                this.dgv_IEModelChange.Columns.Clear();
            }
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter = "EXCEL|*.xls*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (dgv_ImportModelList != null)
                {
                    data = new List<object[]>();
                    this.dgv_ImportModelList.Rows.Clear();
                }
                foreach (string filename in ofd.FileNames)
                {
                    try
                    {
                        this.dgv_ImportModelList.Rows.Add(filename);
                        dgv_ImportModelList.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dataGridView_RowPostPaint);
                        this.GetExcelData(Path.GetFullPath(filename));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, ex.Message);
                    }
                }
                dgv_IEModelChange.AllowUserToAddRows = false;
                if (data != null && data.Count > 0)
                {
                    int colNum = data[0].Length;
                    for (int i = 0; i < colNum; i++)
                    {
                        DataGridViewTextBoxColumn acCode = new DataGridViewTextBoxColumn();
                        //if (i > 0 && i < colNum)
                        //{
                        //    try
                        //    {
                        //        //data[0][i] = Convert.ToDateTime(data[0][i].ToString()).ToShortDateString();
                        //        //data[0][i] = Convert.ToDateTime(data[0][i].ToString()).ToString("yyyy/MM/dd");
                        //        acCode.Width = 68;
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        errs += "column:" + (i + 1) + "," + ex.Message + "\n";
                        //    }
                        //}
                        acCode.Name = data[0][i].ToString();
                        acCode.HeaderText = data[0][i].ToString();
                        dgv_IEModelChange.Columns.Add(acCode);
                    }
                    dgv_IEModelChange.Rows.Add(data[1]);
                    for (int i = 2; i < data.Count; i++)
                    {

                        if (data[i][0].ToString() == "")
                        {

                        }
                        else
                        {

                            dgv_IEModelChange.Rows.Add(data[i]);
                            dgv_IEModelChange.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dataGridView_RowPostPaint);


                            //int len = data.Count;
                            //for (int d = 1; d < len; d++)
                            //{
                            //    if (data[d][2] != null && data[d][2].ToString().Trim() != "")
                            //    {
                            //        try
                            //        {
                            //            decimal target = decimal.Parse(data[d][2].ToString().Trim());
                            //        }
                            //        catch (Exception ex)
                            //        {
                            //            errs += "row:" + d + ",cloumn: 2," + ex.Message + "\n";
                            //        }
                            //    }
                            //    else
                            //    {
                            //        data[d][2] = "";
                            //    }
                            //}

                        }



                    }
                    lbl_modelchange_lines.Text = $"Total Model Change Lines : {dgv_IEModelChange.RowCount}";
                }
            }
            if (errs != "")
            {
                MessageBox.Show(errs);
            }
        }


        private void butModelImport_Click(object sender, EventArgs e)
        {
            isTitle = true;
            //btnFolder.Enabled = false;
            butIEModelChangeFile.Enabled = false;
            //butImport.Enabled = false;
            if (this.dgv_IEModelChange.Rows.Count >= 2)
            {
                try
                {
                    updateModelchange_db();
                }
                catch (Exception ex)
                {
                    butIEModelChangeFile.Enabled = true;
                    //butImport.Enabled = true;
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
                }
            }
            else
            {
                butIEModelChangeFile.Enabled = true;
                //butImport.Enabled = true;
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("err-00001", Program.client, "", Program.client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
        private void UpLoadModelChange(System.Data.DataTable tab)
        {
            Dictionary<string, Object> d = new Dictionary<string, object>();
            d.Add("data", tab);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.IE_BonusEvalServer", "UpLoadModelChange",
                            Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(d));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("导入成功！", Program.client, "", Program.client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
            else
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("导入失败！", Program.client, "", Program.client.Language);
                string exceptionMsg = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString();
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg + exceptionMsg);
            }
        }
        private void updateModelchange_db()
        {

            System.Data.DataTable dt = new System.Data.DataTable();
            foreach (DataGridViewColumn col in dgv_IEModelChange.Columns)
            {
                dt.Columns.Add(col.Name);
            }

            foreach (DataGridViewRow row in dgv_IEModelChange.Rows)
            {
                DataRow dRow = dt.NewRow();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dRow[cell.ColumnIndex] = cell.Value;
                }
                dt.Rows.Add(dRow);
            }

            try
            {

                UpLoadModelChange(StripEmptyRows(dt));
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }

        }

        private void btnSearchIEModel_Click(object sender, EventArgs e)
        {
            if (dgv_IETargets != null)
            {
                this.dgv_IEModelChange.Columns.Clear();
            }

            System.Drawing.Font a = new System.Drawing.Font("宋体", 12);
            dgv_IEModelChange.Font = a;//font

            Dictionary<string, Object> p = new Dictionary<string, object>();

            p.Add("vIEModelsearch", txtIEModelsearch.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL,
                "KZ_MESAPI", "KZ_MESAPI.Controllers.IE_BonusEvalServer", "QueryIEModelsearch",
            Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                System.Data.DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dtJson.Rows.Count > 0)
                {

                    dgv_IEModelChange.DataSource = dtJson;

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

        #endregion

        #region MP

        private void btn_FSLS_Click(object sender, EventArgs e)
        {
            isTitle = true;
            string errs = "";
            this.dgv_LSDetails.AutoGenerateColumns = false;
            if (dgv_LSDetails != null)
            {
                this.dgv_LSDetails.Columns.Clear();
            }
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter = "EXCEL|*.xls*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (dgv_ImportLS != null)
                {
                    data = new List<object[]>();
                    this.dgv_ImportLS.Rows.Clear();
                }
                foreach (string filename in ofd.FileNames)
                {
                    try
                    {
                        this.dgv_ImportLS.Rows.Add(filename);
                        dgv_ImportLS.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dataGridView_RowPostPaint);
                        this.GetExcelData(Path.GetFullPath(filename));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, ex.Message);
                    }
                }
                dgv_LSDetails.AllowUserToAddRows = false;
                if (data != null && data.Count > 0)
                {
                    int colNum = data[0].Length;
                    for (int i = 0; i < colNum; i++)
                    {
                        DataGridViewTextBoxColumn acCode = new DataGridViewTextBoxColumn();
                        //if (i > 0 && i < colNum)
                        //{
                        //    try
                        //    {
                        //        //data[0][i] = Convert.ToDateTime(data[0][i].ToString()).ToShortDateString();
                        //        //data[0][i] = Convert.ToDateTime(data[0][i].ToString()).ToString("yyyy/MM/dd");
                        //        acCode.Width = 68;
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        errs += "column:" + (i + 1) + "," + ex.Message + "\n";
                        //    }
                        //}
                        acCode.Name = data[0][i].ToString();
                        acCode.HeaderText = data[0][i].ToString();
                        dgv_LSDetails.Columns.Add(acCode);
                    }
                    dgv_LSDetails.Rows.Add(data[1]);
                    for (int i = 2; i < data.Count; i++)
                    {
                        dgv_LSDetails.Rows.Add(data[i]);
                        dgv_LSDetails.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dataGridView_RowPostPaint);


                        int len = data.Count;
                        for (int d = 1; d < len; d++)
                        {
                            if (data[d][1] != null && data[d][1].ToString().Trim() != "")
                            {
                                try
                                {
                                    decimal target = decimal.Parse(data[d][2].ToString().Trim());
                                }
                                catch (Exception ex)
                                {
                                    errs += "row:" + d + ",cloumn: 2," + ex.Message + "\n";
                                }
                            }
                            else
                            {
                                data[d][2] = "";
                            }
                        }


                    }
                }
            }
            if (errs != "")
            {
                MessageBox.Show(errs);
            }
        }

        private void btn_ImportLS_Click(object sender, EventArgs e)
        {
            isTitle = true;
            //btnFolder.Enabled = false;
            btn_FSLS.Enabled = false;
            //butImport.Enabled = false;
            if (this.dgv_LSDetails.Rows.Count >= 2)
            {
                try
                {
                    updateLS_db();
                }
                catch (Exception ex)
                {
                    btn_FSLS.Enabled = true;
                    //butImport.Enabled = true;
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
                }
            }
            else
            {
                btn_FSLS.Enabled = true;
                //butImport.Enabled = true;
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("err-00001", Program.client, "", Program.client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }


        private void updateLS_db()
        {

            System.Data.DataTable dt = new System.Data.DataTable();
            foreach (DataGridViewColumn col in dgv_LSDetails.Columns)
            {
                dt.Columns.Add(col.Name);
            }

            foreach (DataGridViewRow row in dgv_LSDetails.Rows)
            {
                DataRow dRow = dt.NewRow();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dRow[cell.ColumnIndex] = cell.Value;
                }
                dt.Rows.Add(dRow);
            }

            try
            {

                UpLoadLS(StripEmptyRows(dt));
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }

        }

        private void UpLoadLS(System.Data.DataTable tab)
        {
            Dictionary<string, Object> d = new Dictionary<string, object>();
            d.Add("outputdate", dtp_ImportMP.Text);
            d.Add("data", tab);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.IE_BonusEvalServer", "UpLoadLCManpower",
                            Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(d));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("导入成功！", Program.client, "", Program.client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
            else
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("导入失败！", Program.client, "", Program.client.Language);
                string exceptionMsg = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString();
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg + exceptionMsg);
            }
        }

        private void btn_SerachLS_Click(object sender, EventArgs e)
        {
            if (dgv_LSDetails != null)
            {
                this.dgv_LSDetails.Columns.Clear();
            }

            System.Drawing.Font a = new System.Drawing.Font("宋体", 12);
            dgv_Summary.Font = a;//font

            Dictionary<string, Object> p = new Dictionary<string, object>();

            p.Add("vSearchMPDate", dtp_SearchMP.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL,
                "KZ_MESAPI", "KZ_MESAPI.Controllers.IE_BonusEvalServer", "QueryLCManpowerDetails",
            Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                System.Data.DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dtJson.Rows.Count > 0)
                {

                    dgv_LSDetails.DataSource = dtJson;

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


        #endregion

        #region AO

        private void btn_FSAO_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            isTitle = true;
            string errs = "";
            this.dgv_AODetails.AutoGenerateColumns = false;
            if (dgv_AODetails != null)
            {
                this.dgv_AODetails.Columns.Clear();
            }
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter = "EXCEL|*.xls*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (dgv_ImportAO != null)
                {
                    data = new List<object[]>();
                    this.dgv_ImportAO.Rows.Clear();
                }
                foreach (string filename in ofd.FileNames)
                {
                    try
                    {
                        this.dgv_ImportAO.Rows.Add(filename);
                        dgv_ImportAO.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dataGridView_RowPostPaint);
                        this.GetExcelData(Path.GetFullPath(filename));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, ex.Message);
                    }
                }
                dgv_AODetails.AllowUserToAddRows = false;
                if (data != null && data.Count > 0)
                {
                    int colNum = data[0].Length;
                    for (int i = 0; i < colNum; i++)
                    {
                        DataGridViewTextBoxColumn acCode = new DataGridViewTextBoxColumn();
                        acCode.Name = data[0][i].ToString();
                        acCode.HeaderText = data[0][i].ToString();
                        dgv_AODetails.Columns.Add(acCode);
                    }
                    dgv_AODetails.Rows.Add(data[1]);
                    for (int i = 2; i < data.Count; i++)
                    {
                        dgv_AODetails.Rows.Add(data[i]);
                        dgv_AODetails.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dataGridView_RowPostPaint);


                        int len = data.Count;
                        for (int d = 1; d < len; d++)
                        {
                            if (data[d][1] != null && data[d][1].ToString().Trim() != "")
                            {
                                try
                                {
                                    decimal target = decimal.Parse(data[d][2].ToString().Trim());
                                }
                                catch (Exception ex)
                                {
                                    errs += "row:" + d + ",cloumn: 2," + ex.Message + "\n";
                                }
                            }
                            else
                            {
                                data[d][2] = "";
                            }
                        }


                    }
                    lbl_Output_Rows.Text = $"Total Adjusted Lines : {dgv_AODetails.RowCount}";
                }
            }
            if (errs != "")
            {
                MessageBox.Show(errs);
            }
            Cursor.Current = Cursors.Default;
        }

        private void btn_ImportAO_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            isTitle = true;
            //btnFolder.Enabled = false;
            btn_FSAO.Enabled = false;
            //butImport.Enabled = false;
            if (this.dgv_AODetails.Rows.Count >= 2)
            {
                try
                {
                    updateAO_db();
                }
                catch (Exception ex)
                {
                    btn_FSAO.Enabled = true;
                    //butImport.Enabled = true;
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
                }
            }
            else
            {
                btn_FSAO.Enabled = true;
                //butImport.Enabled = true;
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("err-00001", Program.client, "", Program.client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
            Cursor.Current = Cursors.Default;
        }


        private void updateAO_db()
        {
            Cursor.Current = Cursors.WaitCursor;

            System.Data.DataTable dt = new System.Data.DataTable();
            foreach (DataGridViewColumn col in dgv_AODetails.Columns)
            {
                dt.Columns.Add(col.Name);
            }

            foreach (DataGridViewRow row in dgv_AODetails.Rows)
            {
                DataRow dRow = dt.NewRow();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dRow[cell.ColumnIndex] = cell.Value;
                }
                dt.Rows.Add(dRow);
            }

            try
            {

                UpLoadAO(StripEmptyRows(dt));
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
            Cursor.Current = Cursors.Default;

        }

        private void UpLoadAO(System.Data.DataTable tab)
        {
            Cursor.Current = Cursors.WaitCursor;
            Dictionary<string, Object> d = new Dictionary<string, object>();
            d.Add("outputdate", dtp_ImportAO.Text);
            d.Add("data", tab);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.IE_BonusEvalServer", "UpLoadAdjustOutput",
                            Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(d));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("导入成功！", Program.client, "", Program.client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
            else
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("导入失败！", Program.client, "", Program.client.Language);
                string exceptionMsg = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString();
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg + exceptionMsg);
            }
            Cursor.Current = Cursors.Default;
        }

        private void btn_SerachAO_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (dgv_AODetails != null)
            {
                this.dgv_AODetails.Columns.Clear();
            }

            System.Drawing.Font a = new System.Drawing.Font("宋体", 12);
            dgv_Summary.Font = a;//font

            Dictionary<string, Object> p = new Dictionary<string, object>();

            p.Add("vSearchAODate", dtp_SearchAO.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL,
                "KZ_MESAPI", "KZ_MESAPI.Controllers.IE_BonusEvalServer", "QueryAdjustOutput",
            Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                System.Data.DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dtJson.Rows.Count > 0)
                {

                    dgv_AODetails.DataSource = dtJson;

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
            Cursor.Current = Cursors.Default;
        }

        #endregion


        #region AM

        private void btn_FSAM_Click(object sender, EventArgs e)
        {

            #region Old Code 
            //Cursor.Current = Cursors.WaitCursor;
            //isTitle = true;
            //string errs = "";
            //this.dgv_AMDetails.AutoGenerateColumns = false;
            //if (dgv_AMDetails != null)
            //{
            //    this.dgv_AMDetails.Columns.Clear();
            //}
            //OpenFileDialog ofd = new OpenFileDialog();
            //ofd.Multiselect = true;
            //ofd.Filter = "EXCEL|*.xls*";
            //if (ofd.ShowDialog() == DialogResult.OK)
            //{
            //    if (dgv_ImportAM != null)
            //    {
            //        data = new List<object[]>();
            //        this.dgv_ImportAM.Rows.Clear();
            //    }
            //    foreach (string filename in ofd.FileNames)
            //    {
            //        try
            //        {
            //            this.dgv_ImportAM.Rows.Add(filename);
            //            dgv_ImportAM.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dataGridView_RowPostPaint);
            //            this.GetExcelData(Path.GetFullPath(filename));
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show(this, ex.Message);
            //        }
            //    }
            //    dgv_AMDetails.AllowUserToAddRows = false;
            //    if (data != null && data.Count > 0)
            //    {
            //        int colNum = data[0].Length;
            //        for (int i = 0; i < colNum; i++)
            //        {
            //            DataGridViewTextBoxColumn acCode = new DataGridViewTextBoxColumn();
            //            acCode.Name = data[0][i].ToString();
            //            acCode.HeaderText = data[0][i].ToString();
            //            dgv_AMDetails.Columns.Add(acCode);
            //        }
            //        dgv_AMDetails.Rows.Add(data[1]);
            //        for (int i = 2; i < data.Count; i++)
            //        //for (int i = 1; i < data.Count; i++)
            //        {
            //            dgv_AMDetails.Rows.Add(data[i]);
            //            dgv_AMDetails.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dataGridView_RowPostPaint);


            //            int len = data.Count;
            //            for (int d = 1; d < len; d++)
            //            {
            //                if (data[d][1] != null && data[d][1].ToString().Trim() != "")
            //                {
            //                    try
            //                    {
            //                        decimal target = decimal.Parse(data[d][2].ToString().Trim());
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        errs += "row:" + d + ",cloumn: 2," + ex.Message + "\n";
            //                    }
            //                }
            //                else
            //                {
            //                    data[d][2] = "";
            //                }
            //            }




            //        }
            //    }
            //}
            //if (errs != "")
            //{
            //    MessageBox.Show(errs);
            //}
            //Cursor.Current = Cursors.Default;
            #endregion



            #region

            isTitle = true;
            string errs = "";
            dgv_AMDetails.AutoGenerateColumns = false;

            dgv_AMDetails?.Columns.Clear();

            OpenFileDialog ofd = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "EXCEL|*.xls*"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                dgv_ImportAM?.Rows.Clear();
                data = new List<object[]>();

                foreach (string filename in ofd.FileNames)
                {
                    try
                    {
                        dgv_ImportAM.Rows.Add(filename);
                        dgv_ImportAM.RowPostPaint += dataGridView_RowPostPaint;
                        GetExcelData(Path.GetFullPath(filename));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, ex.Message);
                    }
                }

                dgv_AMDetails.AllowUserToAddRows = false;

                if (data?.Count > 0)
                {
                    // Add columns based on the first row of data
                    foreach (var colName in data[0])
                    {
                        dgv_AMDetails.Columns.Add(new DataGridViewTextBoxColumn
                        {
                            Name = colName.ToString(),
                            HeaderText = colName.ToString()
                        });
                    }

                    // Add rows from the second row onward
                    for (int i = 1; i < data.Count; i++)
                    {
                        dgv_AMDetails.Rows.Add(data[i]);
                        dgv_AMDetails.RowPostPaint += dataGridView_RowPostPaint;

                        // Validate and parse data in the rows
                        if (data[i].Length > 2)
                        {
                            var value = data[i][1]?.ToString().Trim();
                            if (!string.IsNullOrEmpty(value))
                            {
                                try
                                {
                                    decimal target = decimal.Parse(data[i][2].ToString().Trim());
                                }
                                catch (Exception ex)
                                {
                                    errs += $"row: {i}, column: 2, {ex.Message}\n";
                                }
                            }
                            else
                            {
                                data[i][2] = "";
                            }
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(errs))
            {
                MessageBox.Show(errs);
            }

            Cursor.Current = Cursors.Default;

            #endregion


        }

        private void btn_ImportAM_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            isTitle = true;
            //btnFolder.Enabled = false;
            btn_FSAM.Enabled = false;
            //butImport.Enabled = false;
            if (this.dgv_AMDetails.Rows.Count >= 2)
            {
                try
                {
                    updateAM_db();
                }
                catch (Exception ex)
                {
                    btn_FSAM.Enabled = true;
                    //butImport.Enabled = true;
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
                }
            }
            else
            {
                btn_FSAM.Enabled = true;
                //butImport.Enabled = true;
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("err-00001", Program.client, "", Program.client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
            Cursor.Current = Cursors.Default;
        }


        private void updateAM_db()
        {
            Cursor.Current = Cursors.WaitCursor;
            System.Data.DataTable dt = new System.Data.DataTable();
            foreach (DataGridViewColumn col in dgv_AMDetails.Columns)
            {
                dt.Columns.Add(col.Name);
            }

            foreach (DataGridViewRow row in dgv_AMDetails.Rows)
            {
                DataRow dRow = dt.NewRow();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dRow[cell.ColumnIndex] = cell.Value;
                }
                dt.Rows.Add(dRow);
            }

            try
            {

                UpLoadAM(StripEmptyRows(dt));
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
            Cursor.Current = Cursors.Default;
        }

        private void UpLoadAM(System.Data.DataTable tab)
        {
            string process = this.cbProcess.Text.Trim();

            Cursor.Current = Cursors.WaitCursor;
            Dictionary<string, Object> d = new Dictionary<string, object>();
            d.Add("outputdate", dtp_ImportAM.Text);
            d.Add("category", this.cmb_AMCategory.Text.Trim());
            d.Add("data", tab);
            //string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.IE_BonusEvalServer", "UpLoadAdjustManpower",
            //                Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(d)); 

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_RTLAPI", "KZ_RTLAPI.Controllers.IE_Final_BonusServer", "UpLoadAdjustManpower",
                            Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(d));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("导入成功！", Program.client, "", Program.client.Language);
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
            }
            else
            {
                string msg = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString();
                //string msg = SJeMES_Framework.Common.UIHelper.UImsg("导入失败！", Program.client, "", Program.client.Language);
                // string exceptionMsg = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString();
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
            Cursor.Current = Cursors.Default;
        }

        private void btn_SerachAM_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (dgv_AMDetails != null)
            {
                this.dgv_AMDetails.Columns.Clear();
            }

            System.Drawing.Font a = new System.Drawing.Font("宋体", 12);
            dgv_Summary.Font = a;//font

            Dictionary<string, Object> p = new Dictionary<string, object>();

            p.Add("vSearchAMDate", dtp_SearchAM.Text);
            p.Add("category", this.cmb_AMCategory.Text.Trim());

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL,
                "KZ_MESAPI", "KZ_MESAPI.Controllers.IE_BonusEvalServer", "QueryAdjustManpower",
            Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                System.Data.DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dtJson.Rows.Count > 0)
                {

                    dgv_AMDetails.DataSource = dtJson;

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
            Cursor.Current = Cursors.Default;
        }


        #endregion


        private void btn_QueryIE_Click(object sender, EventArgs e)
        {

            //if (dgv_IEEffDetails != null)
            //{
            //    this.dgv_IEEffDetails.Columns.Clear();
            //}
            if (string.IsNullOrEmpty(cbProcess.Text.ToString()))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Process !");
                return;
            }

            System.Drawing.Font a = new System.Drawing.Font("宋体", 12);
            dgv_Summary.Font = a;//font

            string process = this.cbProcess.Text.Trim();

            Cursor.Current = Cursors.WaitCursor;

            Cls_DBOpertions obj = new Cls_DBOpertions();
            System.Data.DataTable dt = obj.QueryIEEfficiencyDetails(dtp_SearchIE.Text, process);

            Cursor.Current = Cursors.Default;

            if (dt.Rows.Count > 0)
            {
                dgv_IEEffDetails.DataSource = dt;
                AutosizeColumnChange();

                #region

                //for (int i = 0; i < dgv_IEEffDetails.Rows.Count; i++)
                //{
                //    string output;
                //    string headcount;
                //    int adj_output;
                //    int adj_headcount;
                //    //adj_output = string.IsNullOrEmpty(dt.Rows[i]["OUTPUT"].ToString()) ? 0 : int.Parse(dt.Rows[i]["OUTPUT"].ToString());
                //    // adj_headcount = string.IsNullOrEmpty(dt.Rows[i]["FINAL_HC"].ToString()) ? 0 : int.Parse(dt.Rows[i]["FINAL_HC"].ToString());
                //   if (dt.Rows[i]["OUTPUT"].ToString() == "NULL" || dt.Rows[i]["OUTPUT"].ToString() == "0" && dt.Rows[i]["FINAL_HC"].ToString() == "NULL" || dt.Rows[i]["FINAL_HC"].ToString() == "0")
                //    {
                //        dgv_IEEffDetails.Rows[i].DefaultCellStyle.BackColor = Color.DarkOrange;
                //    }

                //    else if (dt.Rows[i]["OUTPUT"].ToString() == "NULL" || dt.Rows[i]["OUTPUT"].ToString() == "" || dt.Rows[i]["OUTPUT"].ToString() =="0")
                //    {
                //        dgv_IEEffDetails.Rows[i].DefaultCellStyle.BackColor = Color.PeachPuff;
                //    }
                //    else if (dt.Rows[i]["FINAL_HC"].ToString() == "NULL" || dt.Rows[i]["FINAL_HC"].ToString() == "" || dt.Rows[i]["FINAL_HC"].ToString() == "0")
                //    {
                //        dgv_IEEffDetails.Rows[i].DefaultCellStyle.BackColor = Color.Violet;
                //    }
                //    else
                //    {
                //        dgv_IEEffDetails.Rows[i].DefaultCellStyle.BackColor = Color.LawnGreen;
                //    }

                //}

                #endregion


                dgv_IEEffDetails.Columns[0].ReadOnly = true;
                dgv_IEEffDetails.Columns[1].ReadOnly = true;
                dgv_IEEffDetails.Columns[2].ReadOnly = true;
                dgv_IEEffDetails.Columns[3].ReadOnly = true;
                dgv_IEEffDetails.Columns[4].ReadOnly = true;
                dgv_IEEffDetails.Columns[5].ReadOnly = true;
                dgv_IEEffDetails.Columns[6].ReadOnly = true;
                dgv_IEEffDetails.Columns[7].ReadOnly = false;
                dgv_IEEffDetails.Columns[8].ReadOnly = true;
                dgv_IEEffDetails.Columns[9].ReadOnly = true;
                dgv_IEEffDetails.Columns[10].ReadOnly = true;
                dgv_IEEffDetails.Columns[11].ReadOnly = true;
                dgv_IEEffDetails.Columns[12].ReadOnly = true;

                #region

                //if(dgv_IEEffDetails.Columns[5]!=null)
                //{
                //    dgv_IEEffDetails.Rows[0].DefaultCellStyle.BackColor = Color.Aqua;
                //    //Color.Yellow;
                //    //Color.Red;
                //    //Color.Aqua;

                //}
                //dgv_IEEffDetails.Columns[5].DefaultCellStyle.BackColor = Color.LawnGreen;

            }

            //added by charan 
            // else   if (dgv_IEEffDetails.Columns[5].ReadOnly)
            //{
            //    dgv_IEEffDetails.Columns[5].DefaultCellStyle.BackColor = Color.LawnGreen;
            //}

            //added by charan 

            #endregion


            else
            {
                MessageBox.Show("No such data");
            }

        }

        private void btn_IEConfirm_Click(object sender, EventArgs e)
        {
            isTitle = true;
            //btnFolder.Enabled = false;
            //btn_FSMP.Enabled = false;
            //butImport.Enabled = false;
            if (this.dgv_IEEffDetails.Rows.Count >= 2)
            {
                try
                {
                    ConfirmIEdetails_db();
                }
                catch (Exception ex)
                {
                    //btn_FSMP.Enabled = true;
                    //butImport.Enabled = true;
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
                }
            }
            else
            {
                //btn_FSMP.Enabled = true;
                //butImport.Enabled = true;
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("err-00001", Program.client, "", Program.client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void ConfirmIEdetails_db()
        {

            System.Data.DataTable dt = new System.Data.DataTable();
            foreach (DataGridViewColumn col in dgv_IEEffDetails.Columns)
            {
                dt.Columns.Add(col.Name);
            }

            foreach (DataGridViewRow row in dgv_IEEffDetails.Rows)
            {
                DataRow dRow = dt.NewRow();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dRow[cell.ColumnIndex] = cell.Value;
                }
                dt.Rows.Add(dRow);
            }

            try
            {
                ConfirmIE(dt);
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }

        }

        private void ConfirmIE(System.Data.DataTable tab)
        {
            Dictionary<string, Object> d = new Dictionary<string, object>();
            d.Add("outputdate", dtp_SearchIE.Text);
            d.Add("data", tab);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL,
                "KZ_RTLAPI", "KZ_RTLAPI.Controllers.IE_Final_BonusServer", "UpLoadIEConfirmDetails", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(d));
            //"KZ_MESAPI", "KZ_MESAPI.Controllers.IE_BonusEvalServer", "UpLoadIEConfirmDetails",


            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("Data Saved Successfully！", Program.client, "", Program.client.Language);
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                dgv_IEEffDetails.DataSource = null;
            }
            else
            {
                //string msg = SJeMES_Framework.Common.UIHelper.UImsg("导入失败！", Program.client, "", Program.client.Language);
                string exceptionMsg = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString();
                SJeMES_Control_Library.MessageHelper.ShowErr(this, exceptionMsg);
                //dgv_IEEffDetails.DataSource = null;
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            if (dgv_Summary.Rows.Count <= 0)                                            // New Code
            {
                //MessageBox.Show("No Data Found");
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {

                string a = "IE bonus Data";
                ExportExcels.Export(a, dgv_Summary);
            }
        }

        private void BtnExport1_Click(object sender, EventArgs e)
        {
            if (dgv_IEEffDetails.Rows.Count <= 0)
            {
                //MessageBox.Show("No Data Found");
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {

                string a = "IE bonus Data";
                ExportExcels.Export(a, dgv_IEEffDetails);
            }
        }

        private void IE_Efficiency_Evaluation_Load(object sender, EventArgs e)
        {
            this.dateTimePicker4.Value = DateTime.Now.AddDays(1 - DateTime.Now.Day);
            this.dateTimePicker1.Value = DateTime.Now.AddDays(1 - DateTime.Now.Day);
            LoadDepts();
            LoadPlant();
           // tabControl1.TabPages.Remove(tabPage7);

            if (tabControl1.TabPages.Contains(tabPage6))
            {
                tabControl1.TabPages.Remove(tabPage6); // Remove tabPage6 from its current position
                tabControl1.TabPages.Insert(1, tabPage6); // Insert tabPage6 at index 2
            }
            if (tabControl1.TabPages.Contains(tabPage7))
            {
                tabControl1.TabPages.Remove(tabPage7); // Remove tabPage6 from its current position
                tabControl1.TabPages.Insert(3, tabPage7); // Insert tabPage6 at index 2
            }


        }
        private void LoadPlant()
        {
            var items1 = new List<AutocompleteItem>();

            string ret1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.MES_Hourly_Management_Server", "LoadPlant", Program.client.UserToken, string.Empty);


            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                items1.Add(new MulticolumnAutocompleteItem(new[] { "" }, ""));
                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {
                    items1.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i - 1]["CODE"].ToString() }, dtJson.Rows[i - 1]["CODE"].ToString()));
                }
            }
            comboBox2.DataSource = items1;


        }
        private void LoadDepts()
        {
            var columnWidth = new int[] { 30, 250 };
            DataTable dt = GetDepts();
            int n = 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                autocompleteMenu1.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dt.Rows[i]["DEPARTMENT_CODE"].ToString() + "   " + dt.Rows[i]["DEPARTMENT_NAME"].ToString() }, dt.Rows[i]["DEPARTMENT_CODE"].ToString()) { ColumnWidth = columnWidth, ImageIndex = n });
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

        private void Btn_Bonus_Select_Click(object sender, EventArgs e)
        {
            advancedDataGridView1.DataSource = null;

            Dictionary<string, Object> p = new Dictionary<string, object>();


            p.Add("vProcess", comboBox1.Text.ToString().ToUpper().Split('|')[0].Trim());
            p.Add("vLine", txt_Line.Text);
            p.Add("vBeginTime", dateTimePicker4.Text);
            p.Add("vEndTime", dateTimePicker3.Text);
            p.Add("vBarcode", txt_Barcode.Text);
            p.Add("vPlant", comboBox2.Text.ToString().ToUpper());

            Cursor.Current = Cursors.WaitCursor;

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL,
                "KZ_MESAPI", "KZ_MESAPI.Controllers.IE_BonusEvalServer", "Bouns_QueryIEEfficiencyReport",
            Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            Cursor.Current = Cursors.Default;
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dtJson1 = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dtJson1.Rows.Count > 0)
                {

                    advancedDataGridView1.DataSource = dtJson1;
                    DataRow row = dtJson1.NewRow();
                    row["EMPNO"] = dtJson1.Rows.Count;
                    dtJson1.Rows.Add(row);
                }
                else
                {
                    advancedDataGridView1.Columns.Clear();
                    MessageBox.Show("No such data");
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void Btn_bonus_reset_Click(object sender, EventArgs e)
        {
            txt_Line.ResetText();
            txt_Barcode.ResetText();
            comboBox2.Text = "";
            advancedDataGridView1.Columns.Clear();
        }

        private void Btn_Export_Excel_Click(object sender, EventArgs e)
        {

            if (advancedDataGridView1.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {
                string a = "IE Bonus Report ";
                ExportExcels.Export(a, advancedDataGridView1);
            }
        }

        private void AdvancedDataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex == 0)
                return;
            if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
            {
                e.Value = "";
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                e.FormattingApplied = true;
            }
        }


        bool IsTheSameCellValue(int column, int row)
        {
            // Specify the column indices for cell 4 and cell 7 and cell 8

            int cell4ColumnIndex = 0; // Assuming cell 4 is at column index 3 (zero-based index)
            int cell7ColumnIndex = 1;
            int cell8ColumnIndex = 2;

            // Check if the current column is either cell 4 or cell 7
            if (column == cell4ColumnIndex || column == cell7ColumnIndex || column == cell8ColumnIndex)
            {
                // Get the current and previous cells for comparison
                DataGridViewCell cell1 = advancedDataGridView1[column, row];
                DataGridViewCell cell2 = advancedDataGridView1[column, row - 1];

                // Check if either cell is null
                if (cell1.Value == null || cell2.Value == null)
                {
                    return false;
                }

                // Compare the values of the current and previous cells
                return cell1.Value.ToString() == cell2.Value.ToString();
            }

            // If the current column is neither cell 4 nor cell 7 and 8, return false
            return false;
        }

        private void AdvancedDataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
            if (e.RowIndex < 1 || e.ColumnIndex < 0)
                return;
            if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
            {
                e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            else
            {
                e.AdvancedBorderStyle.Top = advancedDataGridView1.AdvancedCellBorderStyle.Top;
            }
        }

        private void AdvancedDataGridView1_FilterStringChanged(object sender, EventArgs e)
        {
            dtJson1.DefaultView.RowFilter = advancedDataGridView1.FilterString;
        }

        private void AdvancedDataGridView1_SortStringChanged(object sender, EventArgs e)
        {
            dtJson1.DefaultView.Sort = advancedDataGridView1.SortString;
        }

        private void Dgv_Summary_FilterStringChanged(object sender, EventArgs e)
        {
            dtJson.DefaultView.RowFilter = dgv_Summary.FilterString;
        }

        private void Dgv_Summary_SortStringChanged(object sender, EventArgs e)
        {
            dtJson.DefaultView.Sort = dgv_Summary.SortString; ;
        }

        private void Btn_tct_select_Click(object sender, EventArgs e)
        {

            isTitle = true;
            string errs = "";
            this.dataGridView2.AutoGenerateColumns = false;
            if (dataGridView2 != null)
            {
                //this.dataGridView2.Columns.Clear();
                dataGridView2.DataSource = null;
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
                        //if (i > 0 && i < colNum)
                        //{
                        //    try
                        //    {
                        //        //data[0][i] = Convert.ToDateTime(data[0][i].ToString()).ToShortDateString();
                        //        //data[0][i] = Convert.ToDateTime(data[0][i].ToString()).ToString("yyyy/MM/dd");
                        //        acCode.Width = 68;
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        errs += "column:" + (i + 1) + "," + ex.Message + "\n";
                        //    }
                        //}
                        acCode.Name = data[0][i].ToString();
                        acCode.HeaderText = data[0][i].ToString();
                        dataGridView2.Columns.Add(acCode);
                    }
                    dataGridView2.Rows.Add(data[1]);
                    for (int i = 2; i < data.Count; i++)
                    {
                        dataGridView2.Rows.Add(data[i]);
                        dataGridView2.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dataGridView_RowPostPaint);


                        int len = data.Count;
                        for (int d = 1; d < len; d++)
                        {
                            if (data[d][2] != null && data[d][2].ToString().Trim() != "")
                            {
                                try
                                {
                                    decimal target = decimal.Parse(data[d][2].ToString().Trim());
                                }
                                catch (Exception ex)
                                {
                                    errs += "row:" + d + ",cloumn: 2," + ex.Message + "\n";
                                }
                            }
                            else
                            {
                                data[d][2] = "";
                            }
                        }


                    }
                }
            }
            if (errs != "")
            {
                MessageBox.Show(errs);
            }
        }


        private void Btn_tct_import_Click(object sender, EventArgs e)
        {

            isTitle = true;
            //btnFolder.Enabled = false;
            btn_tct_select.Enabled = false;
            //butImport.Enabled = false;
            if (this.dataGridView2.Rows.Count >= 1)
            {
                try
                {
                    update_TCT_db();
                }
                catch (Exception ex)
                {
                    btn_tct_select.Enabled = true;
                    //butImport.Enabled = true;
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
                }
            }
            else
            {
                btn_tct_select.Enabled = true;
                //butImport.Enabled = true;
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("err-00001", Program.client, "", Program.client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }

        }

        private void update_TCT_db()
        {


            System.Data.DataTable dt = new System.Data.DataTable();
            foreach (DataGridViewColumn col in dataGridView2.Columns)
            {
                dt.Columns.Add(col.Name);
            }

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                DataRow dRow = dt.NewRow();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dRow[cell.ColumnIndex] = cell.Value;
                }
                dt.Rows.Add(dRow);
            }

            try
            {

                // UpLoadTCT(StripEmptyRows(dt));
                UpLoadTCT(dt);
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }

        }

        private void UpLoadTCT(System.Data.DataTable tab)
        {
            Dictionary<string, Object> d = new Dictionary<string, object>();
            d.Add("data", tab);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_RTLAPI", "KZ_RTLAPI.Controllers.IE_Final_BonusServer", "UpLoadTCT",
                            Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(d));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("Import Successful！", Program.client, "", Program.client.Language);
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                btn_tct_select.Enabled = true;
                btn_tct_import.Enabled = true;
                dataGridView2.Columns.Clear();
                dataGridView1.Rows.Clear();

            }
            else
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("Import Failed！", Program.client, "", Program.client.Language);
                string exceptionMsg = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString();
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg + exceptionMsg);
                btn_tct_select.Enabled = true;
                btn_tct_import.Enabled = true;
                dataGridView2.Columns.Clear();
                dataGridView1.Rows.Clear();
            }
        }

        private void Btn_TCT_Article_Search_Click(object sender, EventArgs e)
        {
            //if (dataGridView2 != null)
            //{
            //    this.dataGridView2.Columns.Clear();
            //}

            //System.Drawing.Font a = new System.Drawing.Font("宋体", 12);
            //dgv_Summary.Font = a;//font


            Dictionary<string, Object> p = new Dictionary<string, object>();

            p.Add("vIEtargetsearch", TCT_Article_Search.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_RTLAPI", "KZ_RTLAPI.Controllers.IE_Eval_Server", "QueryArticleTCTsearch", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                dataGridView2.DataSource = null;
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                System.Data.DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dtJson.Rows.Count > 0)
                {
                    dataGridView2.DataSource = dtJson;
                    dataGridView2.Refresh();
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "No such data");
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }


        }

        private void Btn_Clear_Click(object sender, EventArgs e)
        {
            btn_tct_select.Enabled = true;
            btn_tct_import.Enabled = true;
            dataGridView2.Columns.Clear();
            dataGridView1.Rows.Clear();
            TCT_Article_Search.ResetText();
        }

        private void tabPage4_Paint(object sender, PaintEventArgs e)
        {

            #region Old Code 
            //int height = tabPage4.Height;
            //int width = tabPage4.Width;

            //Point startpoint = new Point(0, 0);
            //Point endpoint = new Point(height, width);

            //Color color1 = Color.FromArgb(110, 103, 87, 100);
            //Color color2 = Color.FromArgb(180, 248, 120, 0);

            //using (LinearGradientBrush lgb = new LinearGradientBrush(startpoint, endpoint, color1, color2))
            //{
            //    e.Graphics.FillRectangle(lgb, 0, 0, width, height);
            //}
            #endregion

            #region New Code 

            //// Get the graphics object from the event
            //Graphics g = e.Graphics;

            //// Get the size of the tabPage4 area to draw the gradient
            //Rectangle rect = tabPage4.ClientRectangle;

            //// Define colors similar to the CSS gradient you shared
            //Color centerColor = Color.FromArgb(223, 175, 27); // Center color (rgba(223,175,27,1))
            //Color middleColor = Color.FromArgb(105, 105, 105, 105); // Middle color with alpha (rgba(110, 103, 87, 0.41))
            //Color edgeColor = Color.FromArgb(103, 180, 58); // Edge color (rgba(103,180,58,1))

            //// Create a circular path that covers the tabPage4 area
            //using (GraphicsPath path = new GraphicsPath())
            //{
            //    path.AddEllipse(rect); // Create an ellipse based on the rectangle area

            //    // Create a PathGradientBrush to draw the radial gradient
            //    using (PathGradientBrush brush = new PathGradientBrush(path))
            //    {
            //        // Set the center color of the gradient
            //        brush.CenterColor = centerColor;

            //        // Set the surrounding colors of the gradient
            //        brush.SurroundColors = new Color[] { edgeColor };

            //        // Set up blending to simulate the gradient stops (positions) from your CSS
            //        brush.Blend = new Blend
            //        {
            //            Positions = new float[] { 0f, 0.74f, 1f }, // Corresponds to 9%, 74%, and 100% stops
            //            Factors = new float[] { 1f, 0.41f, 1f } // Alpha transparency levels
            //        };

            //        // Fill the rectangle area of the tabPage4 with the radial gradient
            //        g.FillRectangle(brush, rect);
            //    }
            //}

            #endregion


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgv_IETargets.Rows.Count <= 0)
            {
                //MessageBox.Show("No Data Found");
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {

                string a = "IE bonus Data";
                ExportExcels.Export(a, dgv_IETargets);
            }
        }
    }
}



