using MaterialSkin.Controls;
using RPT_SFC_PO_Tracking_List.DoubleClickForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static RPT_SFC_PO_Tracking_List.Bulk_SalesOrders;
using TExcel = Microsoft.Office.Interop.Excel;

namespace RPT_SFC_PO_Tracking_List
{
    public partial class RPT_SFC_PO_Tracking_List : MaterialForm
    {
        Dictionary<int, string> CusFormIndex = new Dictionary<int, string>();
        public RPT_SFC_PO_Tracking_List()
        {
            InitializeComponent();

            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);

            this.WindowState = FormWindowState.Maximized;

            // p.Add(dataGridView1.Columns["outSoleQty"].Index, "");
            //configuration process
            CusFormIndex.Add(dataGridView1.Columns["packingQty"].Index, "A");
            CusFormIndex.Add(dataGridView1.Columns["assmeblyQty"].Index, "L");
            CusFormIndex.Add(dataGridView1.Columns["cutQty"].Index, "C");
            CusFormIndex.Add(dataGridView1.Columns["stitchingQty"].Index, "S");
        }

        private void F_BDM_Task_List_Load(object sender, EventArgs e)
        {
           // Item_no_List.Enabled = false;

            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView2.AutoGenerateColumns = false;
            this.dataGridView3.AutoGenerateColumns = false;
            this.dateTimePicker1.Value = DateTime.Now.AddDays(1 - DateTime.Now.Day);
            this.dateTimePicker2.Value = DateTime.Now.AddDays(0);
            this.dateTimePicker3.Value = DateTime.Now.AddDays(1 - DateTime.Now.Day);
            this.dateTimePicker4.Value = DateTime.Now.AddDays(0);
            GetDepts();
        }

        private DataTable GetDepts()    //Get department information
        {
            var orderSource = new AutoCompleteStringCollection();   //Store database query results
            DataTable dt = null;
            Dictionary<string, Object> p = new Dictionary<string, object>();
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.GeneralServer", "GetAllDepts", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    orderSource.Add(dt.Rows[i]["DEPARTMENT_CODE"].ToString() + "|" + dt.Rows[i]["DEPARTMENT_NAME"].ToString());
                }
                //【Production department】Bind data source
                textBox_DeptNo.AutoCompleteCustomSource = orderSource;   //bind data source
                textBox_DeptNo.AutoCompleteMode = AutoCompleteMode.Suggest;    //Show related dropdown
                textBox_DeptNo.AutoCompleteSource = AutoCompleteSource.CustomSource;   //set properties
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            return dt;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            #region Added by Shyam 2025.08.07  

            if (string.IsNullOrEmpty(textBox_PO.Text) && string.IsNullOrEmpty(textBox_SeId.Text) && (checkBox_LPD.Checked == false) && (checkBox_CRD.Checked == false) && (string.IsNullOrEmpty(richTextBox1.Text)))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Any One Condition PO or SO or CRD or LPD or Bulk SO List !! ");
                return;
            }
            if (!Validate_CRD_Date())
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, " CRD Range Must Not Exceed 3 Months!");
                return;
            }
            if (!Validate_LPD_Date())
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, " LPD Range Must Not Exceed 3 Months!");
                return;
            }
          
            #endregion

            if (tabControl1.SelectedIndex == 0) //Query tab: Query by order size
            {
                dataGridView1.DataSource = null;

                //if (string.IsNullOrEmpty(textBox1.Text))
                //{
                //    return;
                //}


                 Cursor.Current = Cursors.WaitCursor;

               

                Dictionary<string, Object> p = new Dictionary<string, object>();
                p.Add("vSeId", textBox_SeId.Text);
                p.Add("vPO", textBox_PO.Text);
                // p.Add("SeIdList", Item_no_List.Text.Trim().ToString());                // Newly Added Line by shyam 01 March 2023               
                p.Add("SeIdList", richTextBox1.Text.Trim().ToString());                   // Newly Added Line by shyam 09 March 2024              
                p.Add("vCheckShip", checkBox1.Checked);
                p.Add("vCheckCRD", checkBox_CRD.Checked);
                p.Add("vBeginDate", dateTimePicker1.Value.ToShortDateString());
                p.Add("vEndDate", dateTimePicker2.Value.ToShortDateString());
                p.Add("vCheckLPD", checkBox_LPD.Checked);
                p.Add("vLPDBeginDate", dateTimePicker3.Value.ToShortDateString());
                p.Add("vLPDEndDate", dateTimePicker4.Value.ToShortDateString());
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.SFC_Po_Tracking_ListServer", "GetData", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                Cursor.Current = Cursors.Default;
               
              
                
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                    if (dtJson.Rows.Count == 0)
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "No such data");
                    }
                    else
                    {
                        #region  Old Code Commented on 2025.08.19

                        //DataRow TotaldgvRow1 = WinFormLib.TotalRow.GetTotalRow(dtJson);        //return DataRow
                        //TotaldgvRow1[dtJson.Columns["se_id"]] = string.Empty;
                        //TotaldgvRow1[dtJson.Columns["mer_po"]] = string.Empty;
                        //TotaldgvRow1[dtJson.Columns["prod_no"]] = string.Empty;
                        //TotaldgvRow1[dtJson.Columns["colorway"]] = string.Empty;
                        //TotaldgvRow1[dtJson.Columns["cr_reqdate"]] = string.Empty;
                        //TotaldgvRow1[dtJson.Columns["lpd"]] = string.Empty;
                        //TotaldgvRow1[dtJson.Columns["psdd"]] = string.Empty;
                        //TotaldgvRow1[dtJson.Columns["podd"]] = string.Empty;
                        //TotaldgvRow1[dtJson.Columns["mold_no"]] = string.Empty;
                        //TotaldgvRow1[dtJson.Columns["size_no"]] = "Total";
                        //dtJson.Rows.Add(TotaldgvRow1);

                        #endregion

                        #region New Code by Manohar 2025.08.19
                        // Helper to safely convert to int
                        int ToIntSafe(object value)
                        {
                            if (value == null) return 0;
                            return int.TryParse(value.ToString(), out int result) ? result : 0;
                        }

                        // Group by se_id
                        var seIds = dtJson.AsEnumerable()
                                          .Select(r => r.Field<string>("se_id"))
                                          .Distinct()
                                          .ToList();



                        // Add Total_By_SO rows (your existing code is fine)
                        foreach (var seId in seIds)
                        {
                            var rows = dtJson.AsEnumerable()
                                             .Where(r => r.Field<string>("se_id") == seId &&
                                                         r.Field<string>("size_no") != "Total_By_SO" &&
                                                         r.Field<string>("size_no") != "Total")
                                             .ToList();

                            if (rows.Any())
                            {
                                DataRow totalBySoRow = dtJson.NewRow();

                                totalBySoRow["se_id"] = string.Empty;
                                totalBySoRow["mer_po"] = string.Empty;
                                totalBySoRow["prod_no"] = string.Empty;
                                totalBySoRow["colorway"] = string.Empty;
                                totalBySoRow["cr_reqdate"] = string.Empty;
                                totalBySoRow["lpd"] = string.Empty;
                                totalBySoRow["psdd"] = string.Empty;
                                totalBySoRow["podd"] = string.Empty;
                                totalBySoRow["mold_no"] = "Total";
                                totalBySoRow["size_no"] = string.Empty;

                                totalBySoRow["se_qty"] = rows.Sum(r => ToIntSafe(r["se_qty"]));
                                totalBySoRow["cutQty"] = rows.Sum(r => ToIntSafe(r["cutQty"]));
                                totalBySoRow["stitchingQty"] = rows.Sum(r => ToIntSafe(r["stitchingQty"]));
                                totalBySoRow["assmeblyQty"] = rows.Sum(r => ToIntSafe(r["assmeblyQty"]));
                                totalBySoRow["packingQty"] = rows.Sum(r => ToIntSafe(r["packingQty"]));
                                totalBySoRow["inStock_qty"] = rows.Sum(r => ToIntSafe(r["inStock_qty"]));
                                totalBySoRow["outSoleQty"] = rows.Sum(r => ToIntSafe(r["outSoleQty"]));
                                totalBySoRow["shipping_qty"] = rows.Sum(r => ToIntSafe(r["shipping_qty"]));

                                int insertIndex = dtJson.Rows.IndexOf(rows.Last());
                                dtJson.Rows.InsertAt(totalBySoRow, insertIndex + 1);
                            }
                        }

                        // Now build the FINAL "Total" row excluding all "Total_By_SO" & "Total" rows
                        var realRows = dtJson.AsEnumerable()
                                             .Where(r => r.Field<string>("mold_no") != "Total" &&
                                                         r.Field<string>("size_no") != "Total");

                        DataRow totalRow = dtJson.NewRow();
                        totalRow["se_id"] = string.Empty;
                        totalRow["mer_po"] = string.Empty;
                        totalRow["prod_no"] = string.Empty;
                        totalRow["colorway"] = string.Empty;
                        totalRow["cr_reqdate"] = string.Empty;
                        totalRow["lpd"] = string.Empty;
                        totalRow["psdd"] = string.Empty;
                        totalRow["podd"] = string.Empty;
                        totalRow["mold_no"] = string.Empty;
                        totalRow["size_no"] = "Total";

                        // Sum only real data rows
                        totalRow["se_qty"] = realRows.Sum(r => ToIntSafe(r["se_qty"]));
                        totalRow["cutQty"] = realRows.Sum(r => ToIntSafe(r["cutQty"]));
                        totalRow["stitchingQty"] = realRows.Sum(r => ToIntSafe(r["stitchingQty"]));
                        totalRow["assmeblyQty"] = realRows.Sum(r => ToIntSafe(r["assmeblyQty"]));
                        totalRow["packingQty"] = realRows.Sum(r => ToIntSafe(r["packingQty"]));
                        totalRow["inStock_qty"] = realRows.Sum(r => ToIntSafe(r["inStock_qty"]));
                        totalRow["outSoleQty"] = realRows.Sum(r => ToIntSafe(r["outSoleQty"]));
                        totalRow["shipping_qty"] = realRows.Sum(r => ToIntSafe(r["shipping_qty"]));

                        dtJson.Rows.Add(totalRow);
                        #endregion 


                    }
                    dataGridView1.DataSource = dtJson.DefaultView;

                    dataGridView1.Update();


                    #region Old Code Commented on 2025.08.19 
                    //Cell background color settings
                    //for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    //{
                    //    //Number of cut jobs
                    //    if (int.Parse(dataGridView1.Rows[i].Cells["cutQty"].Value.ToString()) == int.Parse(dataGridView1.Rows[i].Cells["se_qty"].Value.ToString()))
                    //    {
                    //        dataGridView1.Rows[i].Cells["cutQty"].Style.BackColor = Color.PaleGreen;
                    //    }
                    //    else if
                    //        (
                    //            int.Parse(dataGridView1.Rows[i].Cells["cutQty"].Value.ToString()) < int.Parse(dataGridView1.Rows[i].Cells["se_qty"].Value.ToString()) &&
                    //            int.Parse(dataGridView1.Rows[i].Cells["inStock_qty"].Value.ToString()) < int.Parse(dataGridView1.Rows[i].Cells["se_qty"].Value.ToString()) &&
                    //            int.Parse(dataGridView1.Rows[i].Cells["cutQty"].Value.ToString()) >= int.Parse(dataGridView1.Rows[i].Cells["stitchingQty"].Value.ToString()) &&
                    //            int.Parse(dataGridView1.Rows[i].Cells["cutQty"].Value.ToString()) >= int.Parse(dataGridView1.Rows[i].Cells["assmeblyQty"].Value.ToString()) &&
                    //            int.Parse(dataGridView1.Rows[i].Cells["cutQty"].Value.ToString()) >= int.Parse(dataGridView1.Rows[i].Cells["packingQty"].Value.ToString()) &&
                    //            int.Parse(dataGridView1.Rows[i].Cells["cutQty"].Value.ToString()) >= int.Parse(dataGridView1.Rows[i].Cells["inStock_qty"].Value.ToString()) &&
                    //            int.Parse(dataGridView1.Rows[i].Cells["inStock_qty"].Value.ToString()) >= int.Parse(dataGridView1.Rows[i].Cells["shipping_qty"].Value.ToString())
                    //         )
                    //    {
                    //        dataGridView1.Rows[i].Cells["cutQty"].Style.BackColor = Color.Yellow;
                    //    }
                    //    else
                    //    {
                    //        dataGridView1.Rows[i].Cells["cutQty"].Style.BackColor = Color.LightCoral;
                    //    }

                    //    //Number of sewing machines
                    //    if (int.Parse(dataGridView1.Rows[i].Cells["stitchingQty"].Value.ToString()) == int.Parse(dataGridView1.Rows[i].Cells["se_qty"].Value.ToString()))
                    //    {
                    //        dataGridView1.Rows[i].Cells["stitchingQty"].Style.BackColor = Color.PaleGreen;
                    //    }
                    //    else if
                    //       (
                    //           int.Parse(dataGridView1.Rows[i].Cells["stitchingQty"].Value.ToString()) < int.Parse(dataGridView1.Rows[i].Cells["se_qty"].Value.ToString()) &&
                    //           int.Parse(dataGridView1.Rows[i].Cells["inStock_qty"].Value.ToString()) < int.Parse(dataGridView1.Rows[i].Cells["se_qty"].Value.ToString()) &&
                    //           int.Parse(dataGridView1.Rows[i].Cells["stitchingQty"].Value.ToString()) >= int.Parse(dataGridView1.Rows[i].Cells["assmeblyQty"].Value.ToString()) &&
                    //           int.Parse(dataGridView1.Rows[i].Cells["stitchingQty"].Value.ToString()) >= int.Parse(dataGridView1.Rows[i].Cells["packingQty"].Value.ToString()) &&
                    //           int.Parse(dataGridView1.Rows[i].Cells["stitchingQty"].Value.ToString()) >= int.Parse(dataGridView1.Rows[i].Cells["inStock_qty"].Value.ToString()) &&
                    //           int.Parse(dataGridView1.Rows[i].Cells["inStock_qty"].Value.ToString()) >= int.Parse(dataGridView1.Rows[i].Cells["shipping_qty"].Value.ToString())
                    //        )
                    //    {
                    //        dataGridView1.Rows[i].Cells["stitchingQty"].Style.BackColor = Color.Yellow;
                    //    }
                    //    else
                    //    {
                    //        dataGridView1.Rows[i].Cells["stitchingQty"].Style.BackColor = Color.LightCoral;
                    //    }

                    //    //Background Inventory Number
                    //    if (int.Parse(dataGridView1.Rows[i].Cells["outSoleQty"].Value.ToString()) == int.Parse(dataGridView1.Rows[i].Cells["se_qty"].Value.ToString()))
                    //    {
                    //        dataGridView1.Rows[i].Cells["outSoleQty"].Style.BackColor = Color.PaleGreen;
                    //    }
                    //    else if
                    //       (
                    //           int.Parse(dataGridView1.Rows[i].Cells["outSoleQty"].Value.ToString()) < int.Parse(dataGridView1.Rows[i].Cells["se_qty"].Value.ToString()) &&
                    //           int.Parse(dataGridView1.Rows[i].Cells["inStock_qty"].Value.ToString()) < int.Parse(dataGridView1.Rows[i].Cells["se_qty"].Value.ToString()) &&
                    //           int.Parse(dataGridView1.Rows[i].Cells["outSoleQty"].Value.ToString()) >= int.Parse(dataGridView1.Rows[i].Cells["assmeblyQty"].Value.ToString()) &&
                    //           int.Parse(dataGridView1.Rows[i].Cells["outSoleQty"].Value.ToString()) >= int.Parse(dataGridView1.Rows[i].Cells["packingQty"].Value.ToString()) &&
                    //           int.Parse(dataGridView1.Rows[i].Cells["outSoleQty"].Value.ToString()) >= int.Parse(dataGridView1.Rows[i].Cells["inStock_qty"].Value.ToString()) &&
                    //           int.Parse(dataGridView1.Rows[i].Cells["inStock_qty"].Value.ToString()) >= int.Parse(dataGridView1.Rows[i].Cells["shipping_qty"].Value.ToString())
                    //        )
                    //    {
                    //        dataGridView1.Rows[i].Cells["outSoleQty"].Style.BackColor = Color.Yellow;
                    //    }
                    //    else
                    //    {
                    //        dataGridView1.Rows[i].Cells["outSoleQty"].Style.BackColor = Color.LightCoral;
                    //    }

                    //    //Number of processing and production
                    //    if (int.Parse(dataGridView1.Rows[i].Cells["assmeblyQty"].Value.ToString()) == int.Parse(dataGridView1.Rows[i].Cells["se_qty"].Value.ToString()))
                    //    {
                    //        dataGridView1.Rows[i].Cells["assmeblyQty"].Style.BackColor = Color.PaleGreen;
                    //    }
                    //    else if
                    //       (
                    //           int.Parse(dataGridView1.Rows[i].Cells["assmeblyQty"].Value.ToString()) < int.Parse(dataGridView1.Rows[i].Cells["se_qty"].Value.ToString()) &&
                    //           int.Parse(dataGridView1.Rows[i].Cells["inStock_qty"].Value.ToString()) < int.Parse(dataGridView1.Rows[i].Cells["se_qty"].Value.ToString()) &&
                    //           int.Parse(dataGridView1.Rows[i].Cells["assmeblyQty"].Value.ToString()) >= int.Parse(dataGridView1.Rows[i].Cells["packingQty"].Value.ToString()) &&
                    //           int.Parse(dataGridView1.Rows[i].Cells["assmeblyQty"].Value.ToString()) >= int.Parse(dataGridView1.Rows[i].Cells["inStock_qty"].Value.ToString()) &&
                    //           int.Parse(dataGridView1.Rows[i].Cells["inStock_qty"].Value.ToString()) >= int.Parse(dataGridView1.Rows[i].Cells["shipping_qty"].Value.ToString())
                    //        )
                    //    {
                    //        dataGridView1.Rows[i].Cells["assmeblyQty"].Style.BackColor = Color.Yellow;
                    //    }
                    //    else
                    //    {
                    //        dataGridView1.Rows[i].Cells["assmeblyQty"].Style.BackColor = Color.LightCoral;
                    //    }

                    //    //Number of packing reports
                    //    if (int.Parse(dataGridView1.Rows[i].Cells["packingQty"].Value.ToString()) == int.Parse(dataGridView1.Rows[i].Cells["se_qty"].Value.ToString()))
                    //    {
                    //        dataGridView1.Rows[i].Cells["packingQty"].Style.BackColor = Color.PaleGreen;
                    //    }
                    //    else if
                    //       (
                    //           int.Parse(dataGridView1.Rows[i].Cells["packingQty"].Value.ToString()) < int.Parse(dataGridView1.Rows[i].Cells["se_qty"].Value.ToString()) &&
                    //           int.Parse(dataGridView1.Rows[i].Cells["inStock_qty"].Value.ToString()) < int.Parse(dataGridView1.Rows[i].Cells["se_qty"].Value.ToString()) &&
                    //           int.Parse(dataGridView1.Rows[i].Cells["packingQty"].Value.ToString()) >= int.Parse(dataGridView1.Rows[i].Cells["inStock_qty"].Value.ToString()) &&
                    //           int.Parse(dataGridView1.Rows[i].Cells["inStock_qty"].Value.ToString()) >= int.Parse(dataGridView1.Rows[i].Cells["shipping_qty"].Value.ToString())
                    //        )
                    //    {
                    //        dataGridView1.Rows[i].Cells["packingQty"].Style.BackColor = Color.Yellow;
                    //    }
                    //    else
                    //    {
                    //        dataGridView1.Rows[i].Cells["packingQty"].Style.BackColor = Color.LightCoral;
                    //    }

                    //    //Number of warehousing
                    //    if (int.Parse(dataGridView1.Rows[i].Cells["inStock_qty"].Value.ToString()) == int.Parse(dataGridView1.Rows[i].Cells["se_qty"].Value.ToString()))
                    //    {
                    //        dataGridView1.Rows[i].Cells["inStock_qty"].Style.BackColor = Color.PaleGreen;
                    //    }
                    //    else if
                    //       (
                    //           int.Parse(dataGridView1.Rows[i].Cells["inStock_qty"].Value.ToString()) < int.Parse(dataGridView1.Rows[i].Cells["se_qty"].Value.ToString()) &&
                    //           int.Parse(dataGridView1.Rows[i].Cells["inStock_qty"].Value.ToString()) >= int.Parse(dataGridView1.Rows[i].Cells["shipping_qty"].Value.ToString())
                    //        )
                    //    {
                    //        dataGridView1.Rows[i].Cells["inStock_qty"].Style.BackColor = Color.Yellow;
                    //    }
                    //    else
                    //    {
                    //        dataGridView1.Rows[i].Cells["inStock_qty"].Style.BackColor = Color.LightCoral;
                    //    }

                    //    //Shipments
                    //    if (int.Parse(dataGridView1.Rows[i].Cells["shipping_qty"].Value.ToString()) == int.Parse(dataGridView1.Rows[i].Cells["se_qty"].Value.ToString()))
                    //    {
                    //        dataGridView1.Rows[i].Cells["shipping_qty"].Style.BackColor = Color.PaleGreen;
                    //    }
                    //    else if
                    //       (
                    //           int.Parse(dataGridView1.Rows[i].Cells["shipping_qty"].Value.ToString()) < int.Parse(dataGridView1.Rows[i].Cells["se_qty"].Value.ToString()) &&
                    //           int.Parse(dataGridView1.Rows[i].Cells["shipping_qty"].Value.ToString()) <= int.Parse(dataGridView1.Rows[i].Cells["inStock_qty"].Value.ToString())
                    //        )
                    //    {
                    //        dataGridView1.Rows[i].Cells["shipping_qty"].Style.BackColor = Color.Yellow;
                    //    }
                    //    else
                    //    {
                    //        dataGridView1.Rows[i].Cells["shipping_qty"].Style.BackColor = Color.LightCoral;
                    //    }
                    //}




                    //dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[9].Style.ForeColor = Color.White;
                    //dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[9].Style.Font = new Font("Times New Roman", 10, FontStyle.Bold);
                    //dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[10].Style.Font = new Font("Times New Roman", 10, FontStyle.Bold);
                    //dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[11].Style.Font = new Font("Times New Roman", 10, FontStyle.Bold);
                    //dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[12].Style.Font = new Font("Times New Roman", 10, FontStyle.Bold);
                    //dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[13].Style.Font = new Font("Times New Roman", 10, FontStyle.Bold);
                    //dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[14].Style.Font = new Font("Times New Roman", 10, FontStyle.Bold);
                    //dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[15].Style.Font = new Font("Times New Roman", 10, FontStyle.Bold);
                    //dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[16].Style.Font = new Font("Times New Roman", 10, FontStyle.Bold);
                    //dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[17].Style.Font = new Font("Times New Roman", 10, FontStyle.Bold);
                    //dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[9].Style.BackColor = Color.DodgerBlue;

                    #endregion

                    #region New Code by Manohar 2025.08.19 

                    //Cell background color settings
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {


                        string cutQtystr = dataGridView1.Rows[i].Cells["cutQty"].Value?.ToString();
                        string seqtystr = dataGridView1.Rows[i].Cells["se_qty"].Value?.ToString();
                        string instockqty = dataGridView1.Rows[i].Cells["inStock_qty"].Value?.ToString();
                        string stitchingqty = dataGridView1.Rows[i].Cells["stitchingQty"].Value?.ToString();
                        string assemblyqty = dataGridView1.Rows[i].Cells["assmeblyQty"].Value?.ToString();
                        string packingqty = dataGridView1.Rows[i].Cells["packingQty"].Value?.ToString();
                        string shippingqty = dataGridView1.Rows[i].Cells["shipping_qty"].Value?.ToString();
                        string outsoleqty = dataGridView1.Rows[i].Cells["outSoleQty"].Value?.ToString();

                        int cutQty = 0;
                        int seQty = 0;
                        int insqty = 0;
                        int stitqty = 0;
                        int asseqty = 0;
                        int packqty = 0;
                        int shipqty = 0;
                        int outqty = 0;
                        int.TryParse(cutQtystr, out cutQty);
                        int.TryParse(seqtystr, out seQty);
                        int.TryParse(instockqty, out insqty);
                        int.TryParse(stitchingqty, out stitqty);
                        int.TryParse(assemblyqty, out asseqty);
                        int.TryParse(packingqty, out packqty);
                        int.TryParse(shippingqty, out shipqty);
                        int.TryParse(outsoleqty, out outqty);
                        if (cutQty == seQty)
                        {
                            dataGridView1.Rows[i].Cells["cutQty"].Style.BackColor = Color.PaleGreen;
                        }
                        /*
                                                 if (int.Parse(dataGridView1.Rows[i].Cells["cutQty"].Value.ToString()) == 
                                                    int.Parse(dataGridView1.Rows[i].Cells["se_qty"].Value.ToString()))
                                                {
                                                    dataGridView1.Rows[i].Cells["cutQty"].Style.BackColor = Color.PaleGreen;
                                                }*/
                        else if (cutQty < seQty && insqty < seQty && cutQty >= stitqty
                            && cutQty >= asseqty && cutQty >= packqty && cutQty >= insqty
                            && insqty >= shipqty
                            )
                        {
                            dataGridView1.Rows[i].Cells["cutQty"].Style.BackColor = Color.Yellow;
                        }

                        /*else if 
                            ( 
                                int.Parse(dataGridView1.Rows[i].Cells["cutQty"].Value.ToString()) 
                                < int.Parse(dataGridView1.Rows[i].Cells["se_qty"].Value.ToString()) &&
                                int.Parse(dataGridView1.Rows[i].Cells["inStock_qty"].Value.ToString()) 
                                < int.Parse(dataGridView1.Rows[i].Cells["se_qty"].Value.ToString()) &&
                                int.Parse(dataGridView1.Rows[i].Cells["cutQty"].Value.ToString()) 
                                >= int.Parse(dataGridView1.Rows[i].Cells["stitchingQty"].Value.ToString()) &&
                                int.Parse(dataGridView1.Rows[i].Cells["cutQty"].Value.ToString()) 
                                >= int.Parse(dataGridView1.Rows[i].Cells["assmeblyQty"].Value.ToString()) &&
                                int.Parse(dataGridView1.Rows[i].Cells["cutQty"].Value.ToString()) 
                                >= int.Parse(dataGridView1.Rows[i].Cells["packingQty"].Value.ToString()) &&
                                int.Parse(dataGridView1.Rows[i].Cells["cutQty"].Value.ToString()) 
                                >= int.Parse(dataGridView1.Rows[i].Cells["inStock_qty"].Value.ToString()) &&
                                int.Parse(dataGridView1.Rows[i].Cells["inStock_qty"].Value.ToString())
                                >= int.Parse(dataGridView1.Rows[i].Cells["shipping_qty"].Value.ToString())
                             )
                        {
                            dataGridView1.Rows[i].Cells["cutQty"].Style.BackColor = Color.Yellow;
                        }*/

                        else
                        {
                            dataGridView1.Rows[i].Cells["cutQty"].Style.BackColor = Color.LightCoral;
                        }

                        //Number of sewing machines
                        if (stitqty == seQty)
                        /* (int.Parse(dataGridView1.Rows[i].Cells["stitchingQty"].Value.ToString()) 
                         == int.Parse(dataGridView1.Rows[i].Cells["se_qty"].Value.ToString()))*/

                        {
                            dataGridView1.Rows[i].Cells["stitchingQty"].Style.BackColor = Color.PaleGreen;
                        }

                        else if
                            (stitqty < seQty && insqty < seQty && stitqty >= asseqty && stitqty >= packqty && stitqty >= insqty
                            && insqty >= shipqty
                            )
                        /*(
                            int.Parse(dataGridView1.Rows[i].Cells["stitchingQty"].Value.ToString()) 
                            < int.Parse(dataGridView1.Rows[i].Cells["se_qty"].Value.ToString()) &&
                            int.Parse(dataGridView1.Rows[i].Cells["inStock_qty"].Value.ToString()) 
                            < int.Parse(dataGridView1.Rows[i].Cells["se_qty"].Value.ToString()) &&
                            int.Parse(dataGridView1.Rows[i].Cells["stitchingQty"].Value.ToString()) 
                            >= int.Parse(dataGridView1.Rows[i].Cells["assmeblyQty"].Value.ToString()) &&
                            int.Parse(dataGridView1.Rows[i].Cells["stitchingQty"].Value.ToString()) 
                            >= int.Parse(dataGridView1.Rows[i].Cells["packingQty"].Value.ToString()) &&
                            int.Parse(dataGridView1.Rows[i].Cells["stitchingQty"].Value.ToString()) 
                            >= int.Parse(dataGridView1.Rows[i].Cells["inStock_qty"].Value.ToString()) &&
                            int.Parse(dataGridView1.Rows[i].Cells["inStock_qty"].Value.ToString()) 
                            >= int.Parse(dataGridView1.Rows[i].Cells["shipping_qty"].Value.ToString())
                         )*/
                        {
                            dataGridView1.Rows[i].Cells["stitchingQty"].Style.BackColor = Color.Yellow;
                        }
                        else
                        {
                            dataGridView1.Rows[i].Cells["stitchingQty"].Style.BackColor = Color.LightCoral;
                        }

                        //Background Inventory Number
                        if
                            (
                            outqty == seQty
                            )
                        /* (int.Parse(dataGridView1.Rows[i].Cells["outSoleQty"].Value.ToString()) 
                         == int.Parse(dataGridView1.Rows[i].Cells["se_qty"].Value.ToString()))*/

                        {
                            dataGridView1.Rows[i].Cells["outSoleQty"].Style.BackColor = Color.PaleGreen;
                        }
                        else if
                            (
                            outqty < seQty && insqty < seQty && outqty >= asseqty && outqty >= packqty && outqty >= insqty && insqty >= shipqty
                            )
                        /*  (
                              int.Parse(dataGridView1.Rows[i].Cells["outSoleQty"].Value.ToString()) 
                              < int.Parse(dataGridView1.Rows[i].Cells["se_qty"].Value.ToString()) &&
                              int.Parse(dataGridView1.Rows[i].Cells["inStock_qty"].Value.ToString()) 
                              < int.Parse(dataGridView1.Rows[i].Cells["se_qty"].Value.ToString()) &&
                              int.Parse(dataGridView1.Rows[i].Cells["outSoleQty"].Value.ToString()) 
                              >= int.Parse(dataGridView1.Rows[i].Cells["assmeblyQty"].Value.ToString()) &&
                              int.Parse(dataGridView1.Rows[i].Cells["outSoleQty"].Value.ToString()) 
                              >= int.Parse(dataGridView1.Rows[i].Cells["packingQty"].Value.ToString()) &&
                              int.Parse(dataGridView1.Rows[i].Cells["outSoleQty"].Value.ToString()) 
                              >= int.Parse(dataGridView1.Rows[i].Cells["inStock_qty"].Value.ToString()) &&
                              int.Parse(dataGridView1.Rows[i].Cells["inStock_qty"].Value.ToString()) 
                              >= int.Parse(dataGridView1.Rows[i].Cells["shipping_qty"].Value.ToString())
                           )*/
                        {
                            dataGridView1.Rows[i].Cells["outSoleQty"].Style.BackColor = Color.Yellow;
                        }
                        else
                        {
                            dataGridView1.Rows[i].Cells["outSoleQty"].Style.BackColor = Color.LightCoral;
                        }

                        //Number of processing and production
                        if
                           /* (int.Parse(dataGridView1.Rows[i].Cells["assmeblyQty"].Value.ToString()) 
                            == int.Parse(dataGridView1.Rows[i].Cells["se_qty"].Value.ToString()))*/
                           (
                           asseqty == seQty
                           )

                        {
                            dataGridView1.Rows[i].Cells["assmeblyQty"].Style.BackColor = Color.PaleGreen;
                        }
                        else if
                            (
                            asseqty < seQty && insqty < seQty && asseqty >= packqty && insqty >= shipqty
                            )

                        /* (
                             int.Parse(dataGridView1.Rows[i].Cells["assmeblyQty"].Value.ToString()) 
                             < int.Parse(dataGridView1.Rows[i].Cells["se_qty"].Value.ToString()) &&
                             int.Parse(dataGridView1.Rows[i].Cells["inStock_qty"].Value.ToString()) 
                             < int.Parse(dataGridView1.Rows[i].Cells["se_qty"].Value.ToString()) &&
                             int.Parse(dataGridView1.Rows[i].Cells["assmeblyQty"].Value.ToString()) 
                             >= int.Parse(dataGridView1.Rows[i].Cells["packingQty"].Value.ToString()) &&
                             int.Parse(dataGridView1.Rows[i].Cells["assmeblyQty"].Value.ToString()) 
                             >= int.Parse(dataGridView1.Rows[i].Cells["inStock_qty"].Value.ToString()) &&
                             int.Parse(dataGridView1.Rows[i].Cells["inStock_qty"].Value.ToString()) 
                             >= int.Parse(dataGridView1.Rows[i].Cells["shipping_qty"].Value.ToString())
                          )*/

                        {
                            dataGridView1.Rows[i].Cells["assmeblyQty"].Style.BackColor = Color.Yellow;
                        }
                        else
                        {
                            dataGridView1.Rows[i].Cells["assmeblyQty"].Style.BackColor = Color.LightCoral;
                        }

                        //Number of packing reports
                        if
                            (
                            packqty == seQty
                            )
                        /*(int.Parse(dataGridView1.Rows[i].Cells["packingQty"].Value.ToString()) 
                        == int.Parse(dataGridView1.Rows[i].Cells["se_qty"].Value.ToString()))*/

                        {
                            dataGridView1.Rows[i].Cells["packingQty"].Style.BackColor = Color.PaleGreen;
                        }
                        else if
                            (
                            packqty < seQty && insqty < seQty && packqty >= insqty && insqty >= shipqty
                            )
                        /* ( 
                             int.Parse(dataGridView1.Rows[i].Cells["packingQty"].Value.ToString()) 
                             < int.Parse(dataGridView1.Rows[i].Cells["se_qty"].Value.ToString()) &&
                             int.Parse(dataGridView1.Rows[i].Cells["inStock_qty"].Value.ToString()) 
                             < int.Parse(dataGridView1.Rows[i].Cells["se_qty"].Value.ToString()) &&
                             int.Parse(dataGridView1.Rows[i].Cells["packingQty"].Value.ToString()) 
                             >= int.Parse(dataGridView1.Rows[i].Cells["inStock_qty"].Value.ToString()) &&
                             int.Parse(dataGridView1.Rows[i].Cells["inStock_qty"].Value.ToString()) 
                             >= int.Parse(dataGridView1.Rows[i].Cells["shipping_qty"].Value.ToString())
                          ) */
                        {
                            dataGridView1.Rows[i].Cells["packingQty"].Style.BackColor = Color.Yellow;
                        }
                        else
                        {
                            dataGridView1.Rows[i].Cells["packingQty"].Style.BackColor = Color.LightCoral;
                        }

                        //Number of warehousing
                        if
                            (
                            insqty == seQty
                            )
                        /*(int.Parse(dataGridView1.Rows[i].Cells["inStock_qty"].Value.ToString()) 
                        == int.Parse(dataGridView1.Rows[i].Cells["se_qty"].Value.ToString()))*/

                        {
                            dataGridView1.Rows[i].Cells["inStock_qty"].Style.BackColor = Color.PaleGreen;
                        }
                        else if
                            (
                            insqty < seQty && insqty >= shipqty
                            )
                        /* (
                             int.Parse(dataGridView1.Rows[i].Cells["inStock_qty"].Value.ToString()) 
                             < int.Parse(dataGridView1.Rows[i].Cells["se_qty"].Value.ToString()) &&
                             int.Parse(dataGridView1.Rows[i].Cells["inStock_qty"].Value.ToString()) 
                             >= int.Parse(dataGridView1.Rows[i].Cells["shipping_qty"].Value.ToString())
                          )*/
                        {
                            dataGridView1.Rows[i].Cells["inStock_qty"].Style.BackColor = Color.Yellow;
                        }
                        else
                        {
                            dataGridView1.Rows[i].Cells["inStock_qty"].Style.BackColor = Color.LightCoral;
                        }

                        //Shipments
                        if
                           /* (int.Parse(dataGridView1.Rows[i].Cells["shipping_qty"].Value.ToString()) 
                            == int.Parse(dataGridView1.Rows[i].Cells["se_qty"].Value.ToString()))*/
                           (
                           shipqty == seQty
                           )

                        {
                            dataGridView1.Rows[i].Cells["shipping_qty"].Style.BackColor = Color.PaleGreen;
                        }
                        else if
                            (
                            shipqty < seQty && shipqty <= insqty
                            )
                        /* (
                             int.Parse(dataGridView1.Rows[i].Cells["shipping_qty"].Value.ToString()) 
                             < int.Parse(dataGridView1.Rows[i].Cells["se_qty"].Value.ToString()) &&
                             int.Parse(dataGridView1.Rows[i].Cells["shipping_qty"].Value.ToString()) 
                             <= int.Parse(dataGridView1.Rows[i].Cells["inStock_qty"].Value.ToString())
                          )*/
                        {
                            dataGridView1.Rows[i].Cells["shipping_qty"].Style.BackColor = Color.Yellow;
                        }
                        else
                        {
                            dataGridView1.Rows[i].Cells["shipping_qty"].Style.BackColor = Color.LightCoral;
                        }
                    }

                    #endregion


                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }

            if (tabControl1.SelectedIndex == 1) //Query Tab: Summary by Sales Order
            {
                //if (dataGridView2.DataSource != null)
                //{
                //    DataTable dt = (DataTable)dataGridView2.DataSource;
                //    dt.Rows.Clear();
                //    dataGridView2.DataSource = dt;
                //}

                Dictionary<string, Object> p = new Dictionary<string, object>();
                p.Add("vSeId", textBox_SeId.Text);
                p.Add("vPO", textBox_PO.Text);
                p.Add("vCheckShip", checkBox1.Checked);
                p.Add("vCheckCRD", checkBox_CRD.Checked);
                p.Add("vBeginDate", dateTimePicker1.Value.ToShortDateString());
                p.Add("vEndDate", dateTimePicker2.Value.ToShortDateString());
                p.Add("vCheckLPD", checkBox_LPD.Checked);
                p.Add("vLPDBeginDate", dateTimePicker3.Value.ToShortDateString());
                p.Add("vLPDEndDate", dateTimePicker4.Value.ToShortDateString());
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.SFC_Po_Tracking_ListServer", "GetTotalDataBySeId", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                    if (dtJson.Rows.Count == 0)
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "No such data");
                    }
                    else
                    {
                        DataRow TotaldgvRow2 = WinFormLib.TotalRow.GetTotalRow(dtJson);        //return DataRow
                        TotaldgvRow2[dtJson.Columns["se_id"]] = string.Empty;
                        TotaldgvRow2[dtJson.Columns["mer_po"]] = string.Empty;
                        TotaldgvRow2[dtJson.Columns["prod_no"]] = string.Empty;
                        TotaldgvRow2[dtJson.Columns["colorway"]] = string.Empty;
                        TotaldgvRow2[dtJson.Columns["cr_reqdate"]] = string.Empty;
                        TotaldgvRow2[dtJson.Columns["lpd"]] = string.Empty;
                        TotaldgvRow2[dtJson.Columns["psdd"]] = string.Empty;
                        TotaldgvRow2[dtJson.Columns["podd"]] = string.Empty;
                        TotaldgvRow2[dtJson.Columns["mold_no"]] = "Total";
                        dtJson.Rows.Add(TotaldgvRow2);
                    }
                    dataGridView2.DataSource = dtJson.DefaultView;
                    dataGridView2.Update();

                    //Cell background color settings
                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        //Number of cut jobs
                        if (int.Parse(dataGridView2.Rows[i].Cells["cutQty_2"].Value.ToString()) == int.Parse(dataGridView2.Rows[i].Cells["se_qty_2"].Value.ToString()))
                        {
                            dataGridView2.Rows[i].Cells["cutQty_2"].Style.BackColor = Color.PaleGreen;
                        }
                        else if
                            (
                                int.Parse(dataGridView2.Rows[i].Cells["cutQty_2"].Value.ToString()) < int.Parse(dataGridView2.Rows[i].Cells["se_qty_2"].Value.ToString()) &&
                                int.Parse(dataGridView2.Rows[i].Cells["inStock_qty_2"].Value.ToString()) < int.Parse(dataGridView2.Rows[i].Cells["se_qty_2"].Value.ToString()) &&
                                int.Parse(dataGridView2.Rows[i].Cells["cutQty_2"].Value.ToString()) >= int.Parse(dataGridView2.Rows[i].Cells["stitchingQty_2"].Value.ToString()) &&
                                int.Parse(dataGridView2.Rows[i].Cells["cutQty_2"].Value.ToString()) >= int.Parse(dataGridView2.Rows[i].Cells["assmeblyQty_2"].Value.ToString()) &&
                                int.Parse(dataGridView2.Rows[i].Cells["cutQty_2"].Value.ToString()) >= int.Parse(dataGridView2.Rows[i].Cells["packingQty_2"].Value.ToString()) &&
                                int.Parse(dataGridView2.Rows[i].Cells["cutQty_2"].Value.ToString()) >= int.Parse(dataGridView2.Rows[i].Cells["inStock_qty_2"].Value.ToString()) &&
                                int.Parse(dataGridView2.Rows[i].Cells["inStock_qty_2"].Value.ToString()) >= int.Parse(dataGridView2.Rows[i].Cells["shipping_qty_2"].Value.ToString())
                             )
                        {
                            dataGridView2.Rows[i].Cells["cutQty_2"].Style.BackColor = Color.Yellow;
                        }
                        else
                        {
                            dataGridView2.Rows[i].Cells["cutQty_2"].Style.BackColor = Color.LightCoral;
                        }

                        //Number of sewing machines
                        if (int.Parse(dataGridView2.Rows[i].Cells["stitchingQty_2"].Value.ToString()) == int.Parse(dataGridView2.Rows[i].Cells["se_qty_2"].Value.ToString()))
                        {
                            dataGridView2.Rows[i].Cells["stitchingQty_2"].Style.BackColor = Color.PaleGreen;
                        }
                        else if
                           (
                               int.Parse(dataGridView2.Rows[i].Cells["stitchingQty_2"].Value.ToString()) < int.Parse(dataGridView2.Rows[i].Cells["se_qty_2"].Value.ToString()) &&
                               int.Parse(dataGridView2.Rows[i].Cells["inStock_qty_2"].Value.ToString()) < int.Parse(dataGridView2.Rows[i].Cells["se_qty_2"].Value.ToString()) &&
                               int.Parse(dataGridView2.Rows[i].Cells["stitchingQty_2"].Value.ToString()) >= int.Parse(dataGridView2.Rows[i].Cells["assmeblyQty_2"].Value.ToString()) &&
                               int.Parse(dataGridView2.Rows[i].Cells["stitchingQty_2"].Value.ToString()) >= int.Parse(dataGridView2.Rows[i].Cells["packingQty_2"].Value.ToString()) &&
                               int.Parse(dataGridView2.Rows[i].Cells["stitchingQty_2"].Value.ToString()) >= int.Parse(dataGridView2.Rows[i].Cells["inStock_qty_2"].Value.ToString()) &&
                               int.Parse(dataGridView2.Rows[i].Cells["inStock_qty_2"].Value.ToString()) >= int.Parse(dataGridView2.Rows[i].Cells["shipping_qty_2"].Value.ToString())
                            )
                        {
                            dataGridView2.Rows[i].Cells["stitchingQty_2"].Style.BackColor = Color.Yellow;
                        }
                        else
                        {
                            dataGridView2.Rows[i].Cells["stitchingQty_2"].Style.BackColor = Color.LightCoral;
                        }

                        //Background Inventory Number
                        if (int.Parse(dataGridView2.Rows[i].Cells["outSoleQty_2"].Value.ToString()) == int.Parse(dataGridView2.Rows[i].Cells["se_qty_2"].Value.ToString()))
                        {
                            dataGridView2.Rows[i].Cells["outSoleQty_2"].Style.BackColor = Color.PaleGreen;
                        }
                        else if
                           (
                               int.Parse(dataGridView2.Rows[i].Cells["outSoleQty_2"].Value.ToString()) < int.Parse(dataGridView2.Rows[i].Cells["se_qty_2"].Value.ToString()) &&
                               int.Parse(dataGridView2.Rows[i].Cells["inStock_qty_2"].Value.ToString()) < int.Parse(dataGridView2.Rows[i].Cells["se_qty_2"].Value.ToString()) &&
                               int.Parse(dataGridView2.Rows[i].Cells["outSoleQty_2"].Value.ToString()) >= int.Parse(dataGridView2.Rows[i].Cells["assmeblyQty_2"].Value.ToString()) &&
                               int.Parse(dataGridView2.Rows[i].Cells["outSoleQty_2"].Value.ToString()) >= int.Parse(dataGridView2.Rows[i].Cells["packingQty_2"].Value.ToString()) &&
                               int.Parse(dataGridView2.Rows[i].Cells["outSoleQty_2"].Value.ToString()) >= int.Parse(dataGridView2.Rows[i].Cells["inStock_qty_2"].Value.ToString()) &&
                               int.Parse(dataGridView2.Rows[i].Cells["inStock_qty_2"].Value.ToString()) >= int.Parse(dataGridView2.Rows[i].Cells["shipping_qty_2"].Value.ToString())
                            )
                        {
                            dataGridView2.Rows[i].Cells["outSoleQty_2"].Style.BackColor = Color.Yellow;
                        }
                        else
                        {
                            dataGridView2.Rows[i].Cells["outSoleQty_2"].Style.BackColor = Color.LightCoral;
                        }

                        //Number of processing and production
                        if (int.Parse(dataGridView2.Rows[i].Cells["assmeblyQty_2"].Value.ToString()) == int.Parse(dataGridView2.Rows[i].Cells["se_qty_2"].Value.ToString()))
                        {
                            dataGridView2.Rows[i].Cells["assmeblyQty_2"].Style.BackColor = Color.PaleGreen;
                        }
                        else if
                           (
                               int.Parse(dataGridView2.Rows[i].Cells["assmeblyQty_2"].Value.ToString()) < int.Parse(dataGridView2.Rows[i].Cells["se_qty_2"].Value.ToString()) &&
                               int.Parse(dataGridView2.Rows[i].Cells["inStock_qty_2"].Value.ToString()) < int.Parse(dataGridView2.Rows[i].Cells["se_qty_2"].Value.ToString()) &&
                               int.Parse(dataGridView2.Rows[i].Cells["assmeblyQty_2"].Value.ToString()) >= int.Parse(dataGridView2.Rows[i].Cells["packingQty_2"].Value.ToString()) &&
                               int.Parse(dataGridView2.Rows[i].Cells["assmeblyQty_2"].Value.ToString()) >= int.Parse(dataGridView2.Rows[i].Cells["inStock_qty_2"].Value.ToString()) &&
                               int.Parse(dataGridView2.Rows[i].Cells["inStock_qty_2"].Value.ToString()) >= int.Parse(dataGridView2.Rows[i].Cells["shipping_qty_2"].Value.ToString())
                            )
                        {
                            dataGridView2.Rows[i].Cells["assmeblyQty_2"].Style.BackColor = Color.Yellow;
                        }
                        else
                        {
                            dataGridView2.Rows[i].Cells["assmeblyQty_2"].Style.BackColor = Color.LightCoral;
                        }

                        //Number of packing reports
                        if (int.Parse(dataGridView2.Rows[i].Cells["packingQty_2"].Value.ToString()) == int.Parse(dataGridView2.Rows[i].Cells["se_qty_2"].Value.ToString()))
                        {
                            dataGridView2.Rows[i].Cells["packingQty_2"].Style.BackColor = Color.PaleGreen;
                        }
                        else if
                           (
                               int.Parse(dataGridView2.Rows[i].Cells["packingQty_2"].Value.ToString()) < int.Parse(dataGridView2.Rows[i].Cells["se_qty_2"].Value.ToString()) &&
                               int.Parse(dataGridView2.Rows[i].Cells["inStock_qty_2"].Value.ToString()) < int.Parse(dataGridView2.Rows[i].Cells["se_qty_2"].Value.ToString()) &&
                               int.Parse(dataGridView2.Rows[i].Cells["packingQty_2"].Value.ToString()) >= int.Parse(dataGridView2.Rows[i].Cells["inStock_qty_2"].Value.ToString()) &&
                               int.Parse(dataGridView2.Rows[i].Cells["inStock_qty_2"].Value.ToString()) >= int.Parse(dataGridView2.Rows[i].Cells["shipping_qty_2"].Value.ToString())
                            )
                        {
                            dataGridView2.Rows[i].Cells["packingQty_2"].Style.BackColor = Color.Yellow;
                        }
                        else
                        {
                            dataGridView2.Rows[i].Cells["packingQty_2"].Style.BackColor = Color.LightCoral;
                        }

                        //Number of warehousing
                        if (int.Parse(dataGridView2.Rows[i].Cells["inStock_qty_2"].Value.ToString()) == int.Parse(dataGridView2.Rows[i].Cells["se_qty_2"].Value.ToString()))
                        {
                            dataGridView2.Rows[i].Cells["inStock_qty_2"].Style.BackColor = Color.PaleGreen;
                        }
                        else if
                           (
                               int.Parse(dataGridView2.Rows[i].Cells["inStock_qty_2"].Value.ToString()) < int.Parse(dataGridView2.Rows[i].Cells["se_qty_2"].Value.ToString()) &&
                               int.Parse(dataGridView2.Rows[i].Cells["inStock_qty_2"].Value.ToString()) >= int.Parse(dataGridView2.Rows[i].Cells["shipping_qty_2"].Value.ToString())
                            )
                        {
                            dataGridView2.Rows[i].Cells["inStock_qty_2"].Style.BackColor = Color.Yellow;
                        }
                        else
                        {
                            dataGridView2.Rows[i].Cells["inStock_qty_2"].Style.BackColor = Color.LightCoral;
                        }

                        //Shipments
                        if (int.Parse(dataGridView2.Rows[i].Cells["shipping_qty_2"].Value.ToString()) == int.Parse(dataGridView2.Rows[i].Cells["se_qty_2"].Value.ToString()))
                        {
                            dataGridView2.Rows[i].Cells["shipping_qty_2"].Style.BackColor = Color.PaleGreen;
                        }
                        else if
                           (
                               int.Parse(dataGridView2.Rows[i].Cells["shipping_qty_2"].Value.ToString()) < int.Parse(dataGridView2.Rows[i].Cells["se_qty_2"].Value.ToString()) &&
                               int.Parse(dataGridView2.Rows[i].Cells["shipping_qty_2"].Value.ToString()) <= int.Parse(dataGridView2.Rows[i].Cells["inStock_qty_2"].Value.ToString())
                            )
                        {
                            dataGridView2.Rows[i].Cells["shipping_qty_2"].Style.BackColor = Color.Yellow;
                        }
                        else
                        {
                            dataGridView2.Rows[i].Cells["shipping_qty_2"].Style.BackColor = Color.LightCoral;
                        }
                    }
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }

            if (tabControl1.SelectedIndex == 2) //Query tab: query by work center
            {
                //DataTable dt = (DataTable)dataGridView3.DataSource;
                //dt.Rows.Clear();
                //dataGridView3.DataSource = dt;

                if (string.IsNullOrEmpty(textBox_DeptNo.Text))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Production sector not entered！");
                    return;
                }
                Dictionary<string, Object> p = new Dictionary<string, object>();
                p.Add("vSeId", textBox_SeId.Text);
                p.Add("vPO", textBox_PO.Text);
                p.Add("vDeptNo", textBox_DeptNo.Text.ToString().ToUpper().Trim().Split(new Char[] { '|' })[0]);
                p.Add("vCheckShip", checkBox1.Checked);
                p.Add("vCheckCRD", checkBox_CRD.Checked);
                p.Add("vBeginDate", dateTimePicker1.Value.ToShortDateString());
                p.Add("vEndDate", dateTimePicker2.Value.ToShortDateString());
                p.Add("vCheckLPD", checkBox_LPD.Checked);
                p.Add("vLPDBeginDate", dateTimePicker3.Value.ToShortDateString());
                p.Add("vLPDEndDate", dateTimePicker4.Value.ToShortDateString());
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.SFC_Po_Tracking_ListServer", "GetDataByDeptNo", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                    if (dtJson.Rows.Count == 0)
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "No such data");
                    }
                    else
                    {
                        DataRow TotaldgvRow3 = WinFormLib.TotalRow.GetTotalRow(dtJson);        //return DataRow
                        TotaldgvRow3[dtJson.Columns["se_id"]] = string.Empty;
                        TotaldgvRow3[dtJson.Columns["mer_po"]] = string.Empty;
                        TotaldgvRow3[dtJson.Columns["prod_no"]] = string.Empty;
                        TotaldgvRow3[dtJson.Columns["colorway"]] = string.Empty;
                        TotaldgvRow3[dtJson.Columns["cr_reqdate"]] = string.Empty;
                        TotaldgvRow3[dtJson.Columns["lpd"]] = string.Empty;
                        TotaldgvRow3[dtJson.Columns["psdd"]] = string.Empty;
                        TotaldgvRow3[dtJson.Columns["podd"]] = string.Empty;
                        TotaldgvRow3[dtJson.Columns["mold_no"]] = string.Empty;
                        TotaldgvRow3[dtJson.Columns["size_no"]] = "Total";
                        dtJson.Rows.Add(TotaldgvRow3);
                    }
                    dataGridView3.DataSource = dtJson.DefaultView;
                    dataGridView3.Update();

                    //Cell background color settings
                    for (int i = 0; i < dataGridView3.Rows.Count; i++)
                    {
                        //Number of cut jobs
                        if (int.Parse(dataGridView3.Rows[i].Cells["cutQty_3"].Value.ToString()) == int.Parse(dataGridView3.Rows[i].Cells["se_qty_3"].Value.ToString()))
                        {
                            dataGridView3.Rows[i].Cells["cutQty_3"].Style.BackColor = Color.PaleGreen;
                        }
                        else if
                            (
                                int.Parse(dataGridView3.Rows[i].Cells["cutQty_3"].Value.ToString()) < int.Parse(dataGridView3.Rows[i].Cells["se_qty_3"].Value.ToString()) &&
                                int.Parse(dataGridView3.Rows[i].Cells["inStock_qty_3"].Value.ToString()) < int.Parse(dataGridView3.Rows[i].Cells["se_qty_3"].Value.ToString()) &&
                                int.Parse(dataGridView3.Rows[i].Cells["cutQty_3"].Value.ToString()) >= int.Parse(dataGridView3.Rows[i].Cells["stitchingQty_3"].Value.ToString()) &&
                                int.Parse(dataGridView3.Rows[i].Cells["cutQty_3"].Value.ToString()) >= int.Parse(dataGridView3.Rows[i].Cells["assmeblyQty_3"].Value.ToString()) &&
                                int.Parse(dataGridView3.Rows[i].Cells["cutQty_3"].Value.ToString()) >= int.Parse(dataGridView3.Rows[i].Cells["packingQty_3"].Value.ToString()) &&
                                int.Parse(dataGridView3.Rows[i].Cells["cutQty_3"].Value.ToString()) >= int.Parse(dataGridView3.Rows[i].Cells["inStock_qty_3"].Value.ToString()) &&
                                int.Parse(dataGridView3.Rows[i].Cells["inStock_qty_3"].Value.ToString()) >= int.Parse(dataGridView3.Rows[i].Cells["shipping_qty_3"].Value.ToString())
                             )
                        {
                            dataGridView3.Rows[i].Cells["cutQty_3"].Style.BackColor = Color.Yellow;
                        }
                        else
                        {
                            dataGridView3.Rows[i].Cells["cutQty_3"].Style.BackColor = Color.LightCoral;
                        }

                        //Number of sewing machines
                        if (int.Parse(dataGridView3.Rows[i].Cells["stitchingQty_3"].Value.ToString()) == int.Parse(dataGridView3.Rows[i].Cells["se_qty_3"].Value.ToString()))
                        {
                            dataGridView3.Rows[i].Cells["stitchingQty_3"].Style.BackColor = Color.PaleGreen;
                        }
                        else if
                           (
                               int.Parse(dataGridView3.Rows[i].Cells["stitchingQty_3"].Value.ToString()) < int.Parse(dataGridView3.Rows[i].Cells["se_qty_3"].Value.ToString()) &&
                               int.Parse(dataGridView3.Rows[i].Cells["inStock_qty_3"].Value.ToString()) < int.Parse(dataGridView3.Rows[i].Cells["se_qty_3"].Value.ToString()) &&
                               int.Parse(dataGridView3.Rows[i].Cells["stitchingQty_3"].Value.ToString()) >= int.Parse(dataGridView3.Rows[i].Cells["assmeblyQty_3"].Value.ToString()) &&
                               int.Parse(dataGridView3.Rows[i].Cells["stitchingQty_3"].Value.ToString()) >= int.Parse(dataGridView3.Rows[i].Cells["packingQty_3"].Value.ToString()) &&
                               int.Parse(dataGridView3.Rows[i].Cells["stitchingQty_3"].Value.ToString()) >= int.Parse(dataGridView3.Rows[i].Cells["inStock_qty_3"].Value.ToString()) &&
                               int.Parse(dataGridView3.Rows[i].Cells["inStock_qty_3"].Value.ToString()) >= int.Parse(dataGridView3.Rows[i].Cells["shipping_qty_3"].Value.ToString())
                            )
                        {
                            dataGridView3.Rows[i].Cells["stitchingQty_3"].Style.BackColor = Color.Yellow;
                        }
                        else
                        {
                            dataGridView3.Rows[i].Cells["stitchingQty_3"].Style.BackColor = Color.LightCoral;
                        }

                        //Background Inventory Number
                        if (int.Parse(dataGridView3.Rows[i].Cells["outSoleQty_3"].Value.ToString()) == int.Parse(dataGridView3.Rows[i].Cells["se_qty_3"].Value.ToString()))
                        {
                            dataGridView3.Rows[i].Cells["outSoleQty_3"].Style.BackColor = Color.PaleGreen;
                        }
                        else if
                           (
                               int.Parse(dataGridView3.Rows[i].Cells["outSoleQty_3"].Value.ToString()) < int.Parse(dataGridView3.Rows[i].Cells["se_qty_3"].Value.ToString()) &&
                               int.Parse(dataGridView3.Rows[i].Cells["inStock_qty_3"].Value.ToString()) < int.Parse(dataGridView3.Rows[i].Cells["se_qty_3"].Value.ToString()) &&
                               int.Parse(dataGridView3.Rows[i].Cells["outSoleQty_3"].Value.ToString()) >= int.Parse(dataGridView3.Rows[i].Cells["assmeblyQty_3"].Value.ToString()) &&
                               int.Parse(dataGridView3.Rows[i].Cells["outSoleQty_3"].Value.ToString()) >= int.Parse(dataGridView3.Rows[i].Cells["packingQty_3"].Value.ToString()) &&
                               int.Parse(dataGridView3.Rows[i].Cells["outSoleQty_3"].Value.ToString()) >= int.Parse(dataGridView3.Rows[i].Cells["inStock_qty_3"].Value.ToString()) &&
                               int.Parse(dataGridView3.Rows[i].Cells["inStock_qty_3"].Value.ToString()) >= int.Parse(dataGridView3.Rows[i].Cells["shipping_qty_3"].Value.ToString())
                            )
                        {
                            dataGridView3.Rows[i].Cells["outSoleQty_3"].Style.BackColor = Color.Yellow;
                        }
                        else
                        {
                            dataGridView3.Rows[i].Cells["outSoleQty_3"].Style.BackColor = Color.LightCoral;
                        }

                        //Number of processing and production
                        if (int.Parse(dataGridView3.Rows[i].Cells["assmeblyQty_3"].Value.ToString()) == int.Parse(dataGridView3.Rows[i].Cells["se_qty_3"].Value.ToString()))
                        {
                            dataGridView3.Rows[i].Cells["assmeblyQty_3"].Style.BackColor = Color.PaleGreen;
                        }
                        else if
                           (
                               int.Parse(dataGridView3.Rows[i].Cells["assmeblyQty_3"].Value.ToString()) < int.Parse(dataGridView3.Rows[i].Cells["se_qty_3"].Value.ToString()) &&
                               int.Parse(dataGridView3.Rows[i].Cells["inStock_qty_3"].Value.ToString()) < int.Parse(dataGridView3.Rows[i].Cells["se_qty_3"].Value.ToString()) &&
                               int.Parse(dataGridView3.Rows[i].Cells["assmeblyQty_3"].Value.ToString()) >= int.Parse(dataGridView3.Rows[i].Cells["packingQty_3"].Value.ToString()) &&
                               int.Parse(dataGridView3.Rows[i].Cells["assmeblyQty_3"].Value.ToString()) >= int.Parse(dataGridView3.Rows[i].Cells["inStock_qty_3"].Value.ToString()) &&
                               int.Parse(dataGridView3.Rows[i].Cells["inStock_qty_3"].Value.ToString()) >= int.Parse(dataGridView3.Rows[i].Cells["shipping_qty_3"].Value.ToString())
                            )
                        {
                            dataGridView3.Rows[i].Cells["assmeblyQty_3"].Style.BackColor = Color.Yellow;
                        }
                        else
                        {
                            dataGridView3.Rows[i].Cells["assmeblyQty_3"].Style.BackColor = Color.LightCoral;
                        }

                        //Number of packing reports
                        if (int.Parse(dataGridView3.Rows[i].Cells["packingQty_3"].Value.ToString()) == int.Parse(dataGridView3.Rows[i].Cells["se_qty_3"].Value.ToString()))
                        {
                            dataGridView3.Rows[i].Cells["packingQty_3"].Style.BackColor = Color.PaleGreen;
                        }
                        else if
                           (
                               int.Parse(dataGridView3.Rows[i].Cells["packingQty_3"].Value.ToString()) < int.Parse(dataGridView3.Rows[i].Cells["se_qty_3"].Value.ToString()) &&
                               int.Parse(dataGridView3.Rows[i].Cells["inStock_qty_3"].Value.ToString()) < int.Parse(dataGridView3.Rows[i].Cells["se_qty_3"].Value.ToString()) &&
                               int.Parse(dataGridView3.Rows[i].Cells["packingQty_3"].Value.ToString()) >= int.Parse(dataGridView3.Rows[i].Cells["inStock_qty_3"].Value.ToString()) &&
                               int.Parse(dataGridView3.Rows[i].Cells["inStock_qty_3"].Value.ToString()) >= int.Parse(dataGridView3.Rows[i].Cells["shipping_qty_3"].Value.ToString())
                            )
                        {
                            dataGridView3.Rows[i].Cells["packingQty_3"].Style.BackColor = Color.Yellow;
                        }
                        else
                        {
                            dataGridView3.Rows[i].Cells["packingQty_3"].Style.BackColor = Color.LightCoral;
                        }

                        //Number of warehousing
                        if (int.Parse(dataGridView3.Rows[i].Cells["instock_qty_3"].Value.ToString()) == int.Parse(dataGridView3.Rows[i].Cells["se_qty_3"].Value.ToString()))
                        {
                            dataGridView3.Rows[i].Cells["instock_qty_3"].Style.BackColor = Color.PaleGreen;
                        }
                        else if
                           (
                               int.Parse(dataGridView3.Rows[i].Cells["instock_qty_3"].Value.ToString()) < int.Parse(dataGridView3.Rows[i].Cells["se_qty_3"].Value.ToString()) &&
                               int.Parse(dataGridView3.Rows[i].Cells["instock_qty_3"].Value.ToString()) >= int.Parse(dataGridView3.Rows[i].Cells["shipping_qty_3"].Value.ToString())
                            )
                        {
                            dataGridView3.Rows[i].Cells["instock_qty_3"].Style.BackColor = Color.Yellow;
                        }
                        else
                        {
                            dataGridView3.Rows[i].Cells["instock_qty_3"].Style.BackColor = Color.LightCoral;
                        }

                        //Shipments
                        if (int.Parse(dataGridView3.Rows[i].Cells["shipping_qty_3"].Value.ToString()) == int.Parse(dataGridView3.Rows[i].Cells["se_qty_3"].Value.ToString()))
                        {
                            dataGridView3.Rows[i].Cells["shipping_qty_3"].Style.BackColor = Color.PaleGreen;
                        }
                        else if
                           (
                               int.Parse(dataGridView3.Rows[i].Cells["shipping_qty_3"].Value.ToString()) < int.Parse(dataGridView3.Rows[i].Cells["se_qty_3"].Value.ToString()) &&
                               int.Parse(dataGridView3.Rows[i].Cells["shipping_qty_3"].Value.ToString()) <= int.Parse(dataGridView3.Rows[i].Cells["instock_qty_3"].Value.ToString())
                            )
                        {
                            dataGridView3.Rows[i].Cells["shipping_qty_3"].Style.BackColor = Color.Yellow;
                        }
                        else
                        {
                            dataGridView3.Rows[i].Cells["shipping_qty_3"].Style.BackColor = Color.LightCoral;
                        }
                    }
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }
        }

        private bool Validate_CRD_Date()
        {
            DateTime fromDate = dateTimePicker1.Value.Date;
            DateTime toDate = dateTimePicker2.Value.Date;

            DateTime maxAllowedDate = fromDate.AddMonths(3); // strictly 3 calendar months

            if (toDate > maxAllowedDate)
            {
                return false;
            }
            return true;
        }

        private bool Validate_LPD_Date()
        {
            DateTime fromDate = dateTimePicker3.Value.Date;
            DateTime toDate = dateTimePicker4.Value.Date;

            DateTime maxAllowedDate = fromDate.AddMonths(3); // strictly 3 calendar months

            if (toDate > maxAllowedDate)
            {
                return false;
            }
            return true;
        }

        private Dictionary<string, DataTable> GetAssmeblyQtyDetail(string se_id, string size_no, string vProcessNo)
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();

            p.Add("vSeId", se_id);
            p.Add("vSizeNo", size_no);
            p.Add("vProcessNo", vProcessNo);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.SFC_Po_Tracking_ListServer", "CusGetFinishQtyDetail", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                Dictionary<string, DataTable> jarr = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, DataTable>>(json);

                return jarr;
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                return null;
            }
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int ColumnIndex = e.ColumnIndex;


            if (CusFormIndex.ContainsKey(ColumnIndex))  //Open the [Number of Work Reports] viewing interface
            {

                DataGridView dgv = (DataGridView)sender;
                string se_id = dgv.Rows[e.RowIndex].Cells["se_id"].Value.ToString();
                string size_no = dgv.Rows[e.RowIndex].Cells["size_no"].Value.ToString();
                if (string.IsNullOrEmpty(se_id) || string.IsNullOrEmpty(size_no))
                {
                    return;
                }
                CusAssmeblyQtyForm cusAssmebly = new CusAssmeblyQtyForm(dgv.Columns[ColumnIndex].HeaderText, GetAssmeblyQtyDetail(se_id, size_no, CusFormIndex[ColumnIndex]));
                cusAssmebly.ShowDialog();

            }



            if (ColumnIndex == dataGridView1.Columns["outSoleQty"].Index)  //Open the [Background Inventory] data viewing interface
            {
                DataGridView dgv = (DataGridView)sender;
                OutSoleQtyForm frm = new OutSoleQtyForm(dgv.Rows[e.RowIndex].Cells["se_id"].Value.ToString(), dgv.Rows[e.RowIndex].Cells["size_no"].Value.ToString());
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }


            if (ColumnIndex == dataGridView1.Columns["inStock_qty"].Index)  //Open the [Inbound Quantity] viewing interface
            {
                DataGridView dgv = (DataGridView)sender;
                InStockQtyForm frm = new InStockQtyForm(dgv.Rows[e.RowIndex].Cells["se_id"].Value.ToString(), dgv.Rows[e.RowIndex].Cells["size_no"].Value.ToString());
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }

            if (ColumnIndex == dataGridView1.Columns["shipping_qty"].Index)  //Open the [Shipment Quantity] view interface
            {
                DataGridView dgv = (DataGridView)sender;
                ShippingQtyForm frm = new ShippingQtyForm(dgv.Rows[e.RowIndex].Cells["se_id"].Value.ToString(), dgv.Rows[e.RowIndex].Cells["size_no"].Value.ToString());
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == dataGridView2.Columns["cutQty_2"].Index)  //打开【裁剪报工数】查看界面
            //{
            //    DataGridView dgv = (DataGridView)sender;
            //    CutQtyForm frm = new CutQtyForm(dgv.Rows[e.RowIndex].Cells["se_id_2"].Value.ToString(), dgv.Rows[e.RowIndex].Cells["size_no_2"].Value.ToString());
            //    frm.StartPosition = FormStartPosition.CenterParent;
            //    frm.ShowDialog();
            //}

            //if (e.ColumnIndex == dataGridView2.Columns["stitchingQty_2"].Index)  //打开【针车报工数】查看界面
            //{
            //    DataGridView dgv = (DataGridView)sender;
            //    StitchingQtyForm frm = new StitchingQtyForm(dgv.Rows[e.RowIndex].Cells["se_id_2"].Value.ToString(), dgv.Rows[e.RowIndex].Cells["size_no_2"].Value.ToString());
            //    frm.StartPosition = FormStartPosition.CenterParent;
            //    frm.ShowDialog();
            //}

            //if (e.ColumnIndex == dataGridView2.Columns["outSoleQty_2"].Index)  //打开【本底入库】数查看界面
            //{
            //    DataGridView dgv = (DataGridView)sender;
            //    OutSoleQtyForm frm = new OutSoleQtyForm(dgv.Rows[e.RowIndex].Cells["se_id_2"].Value.ToString(), dgv.Rows[e.RowIndex].Cells["size_no_2"].Value.ToString());
            //    frm.StartPosition = FormStartPosition.CenterParent;
            //    frm.ShowDialog();
            //}

            //if (e.ColumnIndex == dataGridView2.Columns["assmeblyQty_2"].Index)  //打开【加工投产数】查看界面
            //{
            //    DataGridView dgv = (DataGridView)sender;
            //    AssmeblyQtyForm frm = new AssmeblyQtyForm(dgv.Rows[e.RowIndex].Cells["se_id_2"].Value.ToString(), dgv.Rows[e.RowIndex].Cells["size_no_2"].Value.ToString());
            //    frm.StartPosition = FormStartPosition.CenterParent;
            //    frm.ShowDialog();
            //}

            //if (e.ColumnIndex == dataGridView2.Columns["packingQty_2"].Index)  //打开【包装加工数】查看界面
            //{
            //    DataGridView dgv = (DataGridView)sender;
            //    PackingQtyForm frm = new PackingQtyForm(dgv.Rows[e.RowIndex].Cells["se_id_2"].Value.ToString(), dgv.Rows[e.RowIndex].Cells["size_no_2"].Value.ToString());
            //    frm.StartPosition = FormStartPosition.CenterParent;
            //    frm.ShowDialog();
            //}

            //if (e.ColumnIndex == dataGridView2.Columns["inStock_qty_2"].Index)  //打开【入库数】查看界面
            //{
            //    DataGridView dgv = (DataGridView)sender;
            //    InStockQtyForm frm = new InStockQtyForm(dgv.Rows[e.RowIndex].Cells["se_id_2"].Value.ToString(), dgv.Rows[e.RowIndex].Cells["size_no_2"].Value.ToString());
            //    frm.StartPosition = FormStartPosition.CenterParent;
            //    frm.ShowDialog();
            //}

            //if (e.ColumnIndex == dataGridView2.Columns["shipping_qty_2"].Index)  //打开【出货数】查看界面
            //{
            //    DataGridView dgv = (DataGridView)sender;
            //    ShippingQtyForm frm = new ShippingQtyForm(dgv.Rows[e.RowIndex].Cells["se_id_2"].Value.ToString(), dgv.Rows[e.RowIndex].Cells["size_no_2"].Value.ToString());
            //    frm.StartPosition = FormStartPosition.CenterParent;
            //    frm.ShowDialog();
            //}
        }

        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView3.Columns["cutQty_3"].Index)  //Open the view interface of [Number of Cuts and Reports]
            {
                DataGridView dgv = (DataGridView)sender;
                CutQtyForm frm = new CutQtyForm(dgv.Rows[e.RowIndex].Cells["se_id_3"].Value.ToString(), dgv.Rows[e.RowIndex].Cells["size_no_3"].Value.ToString());
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }

            if (e.ColumnIndex == dataGridView3.Columns["stitchingQty_3"].Index)  //Open the [Number of Needle Car Reports] view interface
            {
                DataGridView dgv = (DataGridView)sender;
                StitchingQtyForm frm = new StitchingQtyForm(dgv.Rows[e.RowIndex].Cells["se_id_3"].Value.ToString(), dgv.Rows[e.RowIndex].Cells["size_no_3"].Value.ToString());
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }

            if (e.ColumnIndex == dataGridView3.Columns["outSoleQty_3"].Index)  //Open the [Background Inventory] data viewing interface
            {
                DataGridView dgv = (DataGridView)sender;
                OutSoleQtyForm frm = new OutSoleQtyForm(dgv.Rows[e.RowIndex].Cells["se_id_3"].Value.ToString(), dgv.Rows[e.RowIndex].Cells["size_no_3"].Value.ToString());
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }

            if (e.ColumnIndex == dataGridView3.Columns["assmeblyQty_3"].Index)  //Open the [Processing and Production Quantity] view interface
            {
                DataGridView dgv = (DataGridView)sender;
                AssmeblyQtyForm frm = new AssmeblyQtyForm(dgv.Rows[e.RowIndex].Cells["se_id_3"].Value.ToString(), dgv.Rows[e.RowIndex].Cells["size_no_3"].Value.ToString());
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }

            if (e.ColumnIndex == dataGridView3.Columns["packingQty_3"].Index)  //Open the 【Package Processing Quantity】view interface
            {
                DataGridView dgv = (DataGridView)sender;
                PackingQtyForm frm = new PackingQtyForm(dgv.Rows[e.RowIndex].Cells["se_id_3"].Value.ToString(), dgv.Rows[e.RowIndex].Cells["size_no_3"].Value.ToString());
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }

            if (e.ColumnIndex == dataGridView3.Columns["inStock_qty_3"].Index)  //Open the [Inbound Quantity] viewing interface
            {
                DataGridView dgv = (DataGridView)sender;
                InStockQtyForm frm = new InStockQtyForm(dgv.Rows[e.RowIndex].Cells["se_id_3"].Value.ToString(), dgv.Rows[e.RowIndex].Cells["size_no_3"].Value.ToString());
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }

            if (e.ColumnIndex == dataGridView3.Columns["shipping_qty_3"].Index)  //Open the [Shipment Quantity] view interface
            {
                DataGridView dgv = (DataGridView)sender;
                ShippingQtyForm frm = new ShippingQtyForm(dgv.Rows[e.RowIndex].Cells["se_id_3"].Value.ToString(), dgv.Rows[e.RowIndex].Cells["size_no_3"].Value.ToString());
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }
        }

        private void Export_Click(object sender, EventArgs e)
        {
            //string a = "PO跟踪表.xls";
            string a = "PO Tracking.xls";
            ExportExcels(a, dataGridView1);
        }

        private void ExportExcels(string fileName, DataGridView myDGV) //export method
        {

            // System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

            //stopwatch.Start();
            TExcel.Range rg;
            string saveFileName = "";
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xlsx";
            saveDialog.Filter = "Excel文件|*.xlsx";
            saveDialog.FileName = fileName;
            saveDialog.ShowDialog();
            saveFileName = saveDialog.FileName;
            if (saveFileName.IndexOf(":") < 0) return; //was clicked to cancel
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            if (xlApp == null)
            {
                MessageBox.Show("Unable to create Excel object，Maybe your machine does not have Excel installed");
                return;
            }
            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];//get sheet1
            TExcel.Worksheet excelSheet = null;

            excelSheet = (TExcel.Worksheet)workbook.ActiveSheet;         //write title
            for (int i = 0; i < myDGV.ColumnCount; i++)
            {
                excelSheet.Cells[1, i + 1] = "'" + myDGV.Columns[i].HeaderText.ToString();
            }
            //write value

            try
            {
                object[,] objData = new object[myDGV.Rows.Count, myDGV.Columns.Count];

                for (int i = 0; i < myDGV.Rows.Count; i++)
                {
                    for (int j = 0; j < myDGV.Columns.Count; j++)
                    {
                        objData[i, j] = myDGV.Rows[i].Cells[j].FormattedValue.ToString();
                    }
                }

                // excelSheet.get_Range(worksheet.Cells[1, 1], worksheet.Cells[myDGV.Rows.Count + 1, myDGV.Columns.Count]).Value2 = objData;
                rg = excelSheet.Range[worksheet.Cells[2, 1], worksheet.Cells[myDGV.Rows.Count + 1, myDGV.Columns.Count]];
                rg.Value2 = objData;
                excelSheet = rg.Worksheet;

                //rg.RowHeight = 18;    
                //rg.HorizontalAlignment = TExcel.XlHAlign.xlHAlignCenter;
                //rg.VerticalAlignment = TExcel.XlHAlign.xlHAlignCenter;



                excelSheet.Columns.EntireColumn.AutoFit();//Column width adaptation
                excelSheet.SaveAs(saveFileName);
                workbook.Save();
                xlApp.Quit();
                GC.Collect();//forcibly destroyed

                MessageBox.Show("Successfully saved", "message notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //stopwatch.Stop();

                //Console.WriteLine(stopwatch.Elapsed.TotalSeconds);//这里是输出的总运行秒数,精确到毫秒的
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                //PublicLibrary.PublicLibrary.Log("prj_002_WeightRandomReport", "Export_Report", "Error", ex.ToString());
            }

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 2)
            {
                textBox_DeptNo.Enabled = true;
            }
            else
            {
                textBox_DeptNo.Enabled = false;
            }
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    DataTable dt = LoadSalesorders(Item_no_List.Text.ToString().Trim());
        //    if (dt == null || dt.Rows.Count <= 0)
        //    {
        //        string msg = SJeMES_Framework.Common.UIHelper.UImsg("No Data Found！", Program.client, "", Program.client.Language);
        //        MessageBox.Show(msg);
        //        return;
        //    }
        //    Bulk_SalesOrders frm = new Bulk_SalesOrders(dt, "Sales_Order");
        //    //frm.DataChange += new Bulk_SalesOrders.DataChangeHandler(DataChanged_Item_no_List);
        //    frm.DataChange += new Bulk_SalesOrders.DataChangeHandler(DataChanged_Item_no_List);
        //    frm.ShowDialog();
        //}

        //public void DataChanged_Item_no_List(object sender, CheckDataChangeEventArgs args)
        //{
        //    Item_no_List.Text = args.value1;
        //}

        private DataTable LoadSalesorders(string SO)
        {
            DataTable dt = null;
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("SALESORDER", SO);
            //p.Add("vStoc_no", stoc_no);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.SFC_Po_Tracking_ListServer", "GetSalesOrderList", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
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

        private void button2_Click(object sender, EventArgs e)
        {
            textBox_PO.Text = "";
            textBox_SeId.Text = "";
            // Item_no_List.Text = "";
            richTextBox1.Text = "";
        }

        private void RichTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (!(sender is RichTextBox)) { return; }
            RichTextBox richText = sender as RichTextBox;
            if (richText.Text.Contains("\n"))
            {
                ExcelFormat(richText);
            }
        }

        private void ExcelFormat(RichTextBox richText)
        {
            string[] str = richText.Text.Split(new string[] { "\t\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (str.Length <= 1)
                str = richText.Text.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            string se_id = "";
            for (int i = 0; i < str.Length; i++)
            {
                if (se_id.Length > 0)
                {
                    se_id += ",";
                }
                se_id += str[i];

            }
            richText.Text = se_id;
            richText.Font = new System.Drawing.Font("宋体", 9F);

        }


        #region Dummy Code 2025.08.07
        //private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        //{
        //  //  ValidateDateDifference();
        //}
        //private void ValidateDateDifference()
        //{
        //    DateTime startDate = dateTimePicker1.Value.Date;
        //    DateTime endDate = dateTimePicker2.Value.Date;

        //    // Ensure startDate is always less than or equal to endDate
        //    if (startDate > endDate)
        //    {
        //        var temp = startDate;
        //        startDate = endDate;
        //        endDate = temp;
        //    }



        //    // Calculate total month difference
        //    int monthDiff = ((endDate.Year - startDate.Year) * 12) + endDate.Month - startDate.Month;

        //    if (endDate.Day < startDate.Day)
        //    {
        //        monthDiff--; // adjust if the day isn't complete in the last month
        //    }

        //    if (Math.Abs(monthDiff) > 4)
        //    {
        //        // MessageBox.Show("The difference between the dates must not exceed 4 months.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
        //        SJeMES_Control_Library.MessageHelper.ShowErr(this, "The difference between the dates must not exceed 4 months !!");
        //    }
        //    else
        //    {
        //        // MessageBox.Show("Date range is valid.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Success !!");
        //    }
        //}

        #endregion 



        //if (string.IsNullOrEmpty(textBox1.Text))
        //{
        //    return;
        //}

        //string so_list = richTextBox1.Text.Trim();
        //string[] seidList = so_list.Split(',');
        //string seids = "'" + seidList[0].Trim() + "'";
        //for (int i = 1; i<seidList.Length; i++)
        //{
        //    seids = seids + ",'" + seidList[i].Trim() + "'";
        //}












    }
}
