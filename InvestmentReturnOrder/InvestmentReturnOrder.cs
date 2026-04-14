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
using AutocompleteMenuNS;
using Newtonsoft.Json;
using WinFormLib;
using Mes.Util;
using System.IO;

namespace InvestmentReturnOrder
{
    public partial class InvestmentReturnOrder : MaterialForm
    {
        DataTable dtReturn = null;
        DataTable dtTransferDept = null;

        public InvestmentReturnOrder()
        {
            InitializeComponent();

            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);

            tabControl1.Height = System.Windows.Forms.Screen.GetWorkingArea(this).Height - 30;

        }

        private void InvestmentReturnOrderForm_Load(object sender, EventArgs e)
        {
            GetAllOrgInfo();
            GetAllRoutNo();
        }

        private void SetTextBox(AutocompleteMenuNS.AutocompleteMenu menu, string returnValue, string returnTable)
        {
            try
            {
                menu.Items = null;
                menu.MaximumSize = new System.Drawing.Size(350, 350);
                var columnWidth = new int[] { 50, 250 };

                Dictionary<string, Object> p = new Dictionary<string, object>();
                p.Add("returnValue", returnValue.Trim());


                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", returnTable, Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    if (ret.Contains("null"))
                        ret = ret.Replace("null", "123456");

                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();

                    DataTable dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);


                    if (returnValue == "d.production_order")
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            menu.AddItem(new MulticolumnAutocompleteItem(new[] { (i + 1) + "", dt.Rows[i]["production_order"].ToString() }, dt.Rows[i]["production_order"].ToString()) { ColumnWidth = columnWidth, ImageIndex = i + 1 });
                        }
                    }
                    else if (returnValue == "a.se_id")
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            menu.AddItem(new MulticolumnAutocompleteItem(new[] { (i + 1) + "", dt.Rows[i]["se_id"].ToString() }, dt.Rows[i]["se_id"].ToString()) { ColumnWidth = columnWidth, ImageIndex = i + 1 });
                        }
                    }
                    else if (returnValue == "c.po")
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            menu.AddItem(new MulticolumnAutocompleteItem(new[] { (i + 1) + "", dt.Rows[i]["po"].ToString() }, dt.Rows[i]["po"].ToString()) { ColumnWidth = columnWidth, ImageIndex = i + 1 });
                        }
                    }
                    else if (returnValue == "a.d_dept,e.department_name")
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            menu.AddItem(new MulticolumnAutocompleteItem(new[] { (i + 1) + "", dt.Rows[i]["d_dept"].ToString() + " " + dt.Rows[i]["DEPARTMENT_NAME"].ToString() }, dt.Rows[i]["d_dept"].ToString() + "|" + dt.Rows[i]["DEPARTMENT_NAME"].ToString()) { ColumnWidth = columnWidth, ImageIndex = i + 1 });
                        }
                    }

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

        private void GetAllOrgInfo()
        {
            try
            {
                comboBox_OrgId.Items.Clear();
                Dictionary<string, Object> p = new Dictionary<string, object>();

                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralOrderServer", "GetAllOrgInfo", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    DataTable dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                    foreach (DataRow row in dt.Rows)
                    {
                        comboBox_OrgId.Items.Add(row["ORG_CODE"].ToString());
                    }
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

        private void GetAllRoutNo()
        {
            try
            {
                comboBox_RoutNo.Items.Clear();
                Dictionary<string, Object> p = new Dictionary<string, object>();

                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralOrderServer", "GetAllRoutNo", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    DataTable dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                    foreach (DataRow row in dt.Rows)
                    {
                        comboBox_RoutNo.Items.Add(row["rout_no"].ToString());
                    }
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

        private void GetTransferDept(string vOrgId, string vRoutNo)
        {
            try
            {
                TransferDept.Items.Clear();
                Dictionary<string, Object> p = new Dictionary<string, object>();
                p.Add("vOrgId", vOrgId);
                p.Add("vRoutNo", vRoutNo);
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralOrderServer", "GetTransferDept", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    dtTransferDept = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json);

                    foreach (DataRow row in dtTransferDept.Rows)
                    {
                        TransferDept.Items.Add(row["TransferDeptNo"].ToString());
                    }
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

        private bool Vail()
        {
            bool resultValue = false;
            if (
                !(string.IsNullOrEmpty(comboBox_OrgId.Text) || string.IsNullOrWhiteSpace(comboBox_OrgId.Text)) &&
                !(string.IsNullOrEmpty(comboBox_RoutNo.Text) || string.IsNullOrWhiteSpace(comboBox_RoutNo.Text))
                )
            {
                resultValue = true;
            }
            //else if (!(string.IsNullOrEmpty(comboBox_RoutNo.Text) || string.IsNullOrWhiteSpace(comboBox_RoutNo.Text)))
            //{
            //    resultValue = true;
            //}
            //else if (!(string.IsNullOrEmpty(textBox_SalesOrder.Text) || string.IsNullOrWhiteSpace(textBox_SalesOrder.Text)))
            //{
            //    resultValue = true;
            //}
            return resultValue;

        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Vail())
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Factory code and routing not entered！");
                    return;
                }

                GetTransferDept(comboBox_OrgId.Text.Trim(), comboBox_RoutNo.Text.Trim());

                Dictionary<string, Object> p = new Dictionary<string, object>();
                p.Add("vMainProdOrder", textBox_MainProdOrder.Text.Trim());
                p.Add("vSubProdOrder", textBox_SubProdOrder.Text.Trim());
                p.Add("vRoutNo", comboBox_RoutNo.Text.Trim());
                p.Add("vSeId", textBox_SalesOrder.Text.Trim());
                p.Add("vDeptNo", textBox_DeptNo.Text.Trim());
                p.Add("vOrgId", comboBox_OrgId.Text.Trim());

                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralOrderServer", "GetInvestmentReturn", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    dtReturn = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json);

                    if (dtReturn.Rows.Count > 0)
                    {
                        //Generate "total" row
                        DataRow TotaldgvRow1 = WinFormLib.TotalRow.GetTotalRow(dtReturn);        //return DataRow
                        TotaldgvRow1[dtReturn.Columns["SE_ID"]] = string.Empty;
                        TotaldgvRow1[dtReturn.Columns["main_prod_order"]] = string.Empty;
                        TotaldgvRow1[dtReturn.Columns["PRODUCTION_ORDER"]] = string.Empty;
                        TotaldgvRow1[dtReturn.Columns["SIZE_NO"]] = string.Empty;
                        TotaldgvRow1[dtReturn.Columns["ROUT_NO"]] = string.Empty;
                        TotaldgvRow1[dtReturn.Columns["D_DEPT"]] = "Total";
                        //TotaldgvRow1[dtReturn.Columns["TransferDept"]] = string.Empty;
                        dtReturn.Rows.Add(TotaldgvRow1);

                        dgvIntoProduction.AutoGenerateColumns = false;
                        dgvIntoProduction.DataSource = dtReturn.DefaultView;
                    }
                    else
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "No corresponding results were found！");
                        dtReturn.Rows.Clear();
                        dgvIntoProduction.DataSource = dtReturn.DefaultView;
                    }
                    dgvIntoProduction.Update();
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            string vIP = IPUtil.GetIpAddress();
            string vQrcodeId = "";

            if (dtReturn.Rows.Count > 1)
            {
                for (int i = 0; i < dtReturn.Rows.Count - 1; i++)
                {
                    if ((dgvIntoProduction.Rows[i].Cells["This_Qty"].Value != null) && (int.Parse(dgvIntoProduction.Rows[i].Cells["This_Qty"].Value.ToString()) > 0))
                    {
                        //Determine whether the transfer department is selected
                        if ((dgvIntoProduction.Rows[i].Cells["TransferDept"].Value == null))
                        {
                            SJeMES_Control_Library.MessageHelper.ShowErr(this, "No transfer department selected！");
                            break;
                        }
                        //Update the number of transfers
                        try
                        {
                            if (UpdateReturnQty(dtReturn.Rows[i]["ORG_ID"].ToString(), dtReturn.Rows[i]["D_DEPT"].ToString(), dgvIntoProduction.Rows[i].Cells["TransferDept"].Value.ToString(),
                                        decimal.Parse(dgvIntoProduction.Rows[i].Cells["This_Qty"].Value.ToString()), dtReturn.Rows[i]["SE_ID"].ToString(), dtReturn.Rows[i]["SE_SEQ"].ToString(),
                                        dtReturn.Rows[i]["SIZE_NO"].ToString(), dtReturn.Rows[i]["PO"].ToString(), dtReturn.Rows[i]["ART_NO"].ToString(), dtReturn.Rows[i]["ROUT_NO"].ToString(),
                                        dtReturn.Rows[i]["PRODUCTION_ORDER"].ToString(), dtReturn.Rows[i]["MAIN_PROD_ORDER"].ToString(), dtReturn.Rows[i]["SE_DAY"].ToString(), vIP, vQrcodeId)
                                )
                            {
                                //SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "保存成功！");
                            }
                            else
                            {
                                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Failed to update allocation quantity！");
                                break;
                            }
                        }
                        catch (Exception ex)
                        {
                            SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
                            break;
                        }

                        //save new table
                        try
                        {
                            if (UpdateSizeReturnQuery(dtReturn.Rows[i]["SE_ID"].ToString(), dtReturn.Rows[i]["WORK_DAY"].ToString(), dtReturn.Rows[i]["D_DEPT"].ToString(),
                                dtReturn.Rows[i]["PRODUCTION_ORDER"].ToString(), dtReturn.Rows[i]["MAIN_PROD_ORDER"].ToString(),
                                dtReturn.Rows[i]["ART_NO"].ToString(), dtReturn.Rows[i]["ART_NAME"].ToString(), dtReturn.Rows[i]["PO"].ToString(), dtReturn.Rows[i]["SIZE_NO"].ToString(),
                                decimal.Parse(dtReturn.Rows[i]["QTY"].ToString()), decimal.Parse(dtReturn.Rows[i]["FINISH_QTY"].ToString()),
                                decimal.Parse(dtReturn.Rows[i]["IN_QTY"].ToString()), decimal.Parse(dtReturn.Rows[i]["OUT_QTY"].ToString()),
                                decimal.Parse(dtReturn.Rows[i]["TRANSFER_QTY"].ToString()), decimal.Parse(dgvIntoProduction.Rows[i].Cells["This_Qty"].Value.ToString()),
                                dtReturn.Rows[i]["ROUT_NO"].ToString(), dtReturn.Rows[i]["ORG_ID"].ToString(), dtReturn.Rows[i]["SIZE_SEQ"].ToString()))

                            {
                                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Save the allocated number successfully！");
                            }
                            else
                            {
                                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Failed to save the allocated amount！");
                            }
                        }
                        catch (Exception ex)
                        {
                            SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
                        }
                    }
                }
                //query again
                dtReturn.Rows.Clear();
                dgvIntoProduction.DataSource = dtReturn;
                dgvIntoProduction.Update();

                btnQuery_Click(sender, e);
            }
        }

        private void ExportGridToFile(DataGridView dataGridView, string filter, string fileName)
        {
            if (!(dataGridView.Rows.Count > 0))
            {
                return;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = filter; //"Excel files (*.xls)|*.xls";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "Export Excel file to";
            saveFileDialog.FileName = fileName;//"Transfer record_" + DateTime.Now.ToString("yyyy/MM/dd");

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Stream myStream;
                    myStream = saveFileDialog.OpenFile();
                    StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding("gb2312"));
                    //Sheet.RepeatingRows = new CellRangeAddress(0, 5, 0, 5);
                    string str = "";

                    //write a title  
                    for (int i = 0; i < dataGridView.ColumnCount; i++)
                    {
                        if (i > 0)
                        {
                            str += "\t";
                        }
                        str += dataGridView.Columns[i].HeaderText;
                    }

                    sw.WriteLine(str);
                    //Write content from line 0 needs to be decremented by one
                    for (int j = 0; j < dataGridView.Rows.Count; j++)
                    {
                        string tempStr = "";
                        for (int k = 0; k < dataGridView.Columns.Count; k++)
                        {
                            if (k > 0)
                            {
                                tempStr += "\t";
                            }
                            string cell = "";
                            if (dataGridView.Rows[j].Cells[k].Value != null)
                            {
                                cell = dataGridView.Rows[j].Cells[k].Value.ToString();
                            }

                            cell = cell.Replace(" ", "").Replace("\r", "").Replace("\n", "").Replace("\r\n", "");
                            tempStr += cell;
                        }
                        sw.WriteLine(tempStr);
                    }

                    //Add a new line to sum

                    MessageBox.Show("Export was successful", "Hint", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    sw.Close();
                    myStream.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    //sw.Close();
                    //myStream.Close();
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportGridToFile(dgvIntoProduction, "Excel files (*.xls)|*.xls", "调拨记录_" + DateTime.Now.ToString("yyyyMMdd"));
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            textBox_MainProdOrder.Clear();
            textBox_SalesOrder.Clear();
            textBox_SubProdOrder.Clear();
            textBox_DeptNo.Clear();
            comboBox_RoutNo.Items.Clear();
            comboBox_RoutNo.Text = "";
            comboBox_OrgId.Items.Clear();
            comboBox_OrgId.Text = "";

            dtReturn.Rows.Clear();
            dgvIntoProduction.DataSource = dtReturn;
            dgvIntoProduction.Update();

            GetAllOrgInfo();
            GetAllRoutNo();
        }

        private bool UpdateReturnQty(string vOrgId, string vDept, string vTransferDept, decimal vQty, string vSeId, string vSeSeq, string vSizeNo, string vPO, string vArtNo, string vRoutNo, string vProdOrder, string vMainProdOrder, string vSeDay, string vIP, string vQrcodeId)
        {
            DataTable dt = new DataTable();
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vOrgId", vOrgId);
            p.Add("vDept", vDept);
            p.Add("vTransferDept", vTransferDept);
            p.Add("vQty", vQty);
            p.Add("vSeId", vSeId);
            p.Add("vSeSeq", vSeSeq);
            p.Add("vSizeNo", vSizeNo);
            p.Add("vPO", vPO);
            p.Add("vArtNo", vArtNo);
            p.Add("vRoutNo", vRoutNo);
            p.Add("vProdOrder", vProdOrder);
            p.Add("vMainProdOrder", vMainProdOrder);
            p.Add("vSeDay", vSeDay);
            p.Add("vIP", vIP);
            p.Add("vQrcodeId", vQrcodeId);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralOrderServer", "UpdateReturnQty", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            bool isOK = Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]);
            if (!isOK)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            return isOK;
        }

        private bool UpdateSizeReturnQuery(string vSeId, string vWorkDate, string vDept, string vProdOrder, string vMainProdOrder, string vArtNo, string vArtName, string vPo, string vSizeNo,
               decimal vSeQty, decimal vFinishQty, decimal vSumInFinishQty, decimal vSumOutFinishQty, decimal vReturnQty, decimal vReturnSizeQty, string vUdf01, string vOrgId, string vSizeSeq)
        {
            DataTable dt = new DataTable();
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vSeId", vSeId);
            p.Add("vWorkDate", vWorkDate);
            p.Add("vDept", vDept);
            p.Add("vProdOrder", vProdOrder);
            p.Add("vMainProdOrder", vMainProdOrder);
            p.Add("vArtNo", vArtNo);
            p.Add("vArtName", vArtName);
            p.Add("vPo", vPo);
            p.Add("vSizeNo", vSizeNo);
            p.Add("vSeQty", vSeQty);
            p.Add("vFinishQty", vFinishQty);
            p.Add("vSumInFinishQty", vSumInFinishQty);
            p.Add("vSumOutFinishQty", vSumOutFinishQty);
            p.Add("vReturnQty", vReturnQty);
            p.Add("vReturnSizeQty", vReturnSizeQty);
            p.Add("vUdf01", vUdf01);
            p.Add("vOrgId", vOrgId);
            p.Add("vSizeSeq", vSizeSeq);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralOrderServer", "UpdateSizeReturnQuery", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            bool isOK = Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]);
            if (!isOK)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            return isOK;
        }


        private void dgvIntoProduction_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //Check the input content
            if (dgvIntoProduction.Rows[e.RowIndex].Cells["This_Qty"].Value != null)
            {
                if (int.TryParse(dgvIntoProduction.Rows[e.RowIndex].Cells["This_Qty"].Value.ToString(), out int returnQty))
                {
                    if (returnQty > decimal.Parse(dtReturn.Rows[e.RowIndex]["Transfer_Qty"].ToString()))
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "The quantity to be allocated this time cannot be greater than the quantity that can be dialed！");
                        dgvIntoProduction.Rows[e.RowIndex].Cells["This_Qty"].Value = "0";
                    }
                    if (returnQty < 0)
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "The number of this transfer cannot be less than 0！");
                        dgvIntoProduction.Rows[e.RowIndex].Cells["This_Qty"].Value = "0";
                    }
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please enter the correct number！");
                    dgvIntoProduction.Rows[e.RowIndex].Cells["This_Qty"].Value = "0";
                }
            }

            //total
            if (dgvIntoProduction.Rows.Count > 0)
            {
                int sumReturnQty = 0;
                string sReturnQty = "";
                for (int i = 0; i < (dgvIntoProduction.Rows.Count - 1); i++)
                {
                    if (dgvIntoProduction.Rows[i].Cells["This_Qty"].Value != null)
                    {
                        sReturnQty = dgvIntoProduction.Rows[i].Cells["This_Qty"].Value.ToString();
                    }
                    else
                    {
                        sReturnQty = "";
                    }

                    if (sReturnQty != "")
                    {
                        sumReturnQty += int.Parse(sReturnQty);
                    }
                }
                dgvIntoProduction.Rows[dgvIntoProduction.Rows.Count - 1].Cells["This_Qty"].Value = sumReturnQty;
            }
        }

        private void btnExportReturn_Click(object sender, EventArgs e)
        {
            ExportGridToFile(dgvIntoProduction, "Excel files (*.xls)|*.xls", "调拨记录_" + DateTime.Now.ToString("yyyyMMdd"));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvIntoProduction_SelectionChanged(object sender, EventArgs e)
        {
            //TransferDept.Items.Clear();
            //string vRoutNo = "";
            //if (dgvIntoProduction.Rows.Count <= 1)
            //    vRoutNo = dgvIntoProduction.Rows[0].Cells["ROUT_NO"].Value.ToString();
            //else
            //    vRoutNo = dgvIntoProduction.Rows[dgvIntoProduction.CurrentRow.Index].Cells["ROUT_NO"].Value.ToString();
            //DataRow dataRow = dtTransferDept.Select("ROUT_NO = '" + vRoutNo + "'")[dgvIntoProduction.FirstDisplayedScrollingRowIndex];
            //TransferDept.Items.Add(dataRow["TransferDeptNo"].ToString());
        }
    }
}
