using MaterialSkin.Controls;
using NewExportExcels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Domestic_PO_Tracking
{
    public partial class Domestic_PO_Tracking : MaterialForm
    {
        Dictionary<int, string> CusFormIndex = new Dictionary<int, string>();
        public Domestic_PO_Tracking()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            this.WindowState = FormWindowState.Maximized;
            CusFormIndex.Add(dataGridView1.Columns["packingQty"].Index, "A");
            CusFormIndex.Add(dataGridView1.Columns["assmeblyQty"].Index, "L");
            CusFormIndex.Add(dataGridView1.Columns["cutQty"].Index, "C");
            CusFormIndex.Add(dataGridView1.Columns["stitchingQty"].Index, "S");
        }

        private void Domestic_PO_Tracking_Load(object sender, EventArgs e)
        {
            this.dataGridView1.AutoGenerateColumns = false;
            this.dateTimePicker1.Value = DateTime.Now.AddDays(1 - DateTime.Now.Day);
            this.dateTimePicker2.Value = DateTime.Now.AddDays(0);
            this.dateTimePicker3.Value = DateTime.Now.AddDays(1 - DateTime.Now.Day);
            this.dateTimePicker4.Value = DateTime.Now.AddDays(0);
        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(textBox_PO.Text) && string.IsNullOrEmpty(textBox_SeId.Text) && (checkBox_LPD.Checked == false) && (checkBox_CRD.Checked == false))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Any One Condition PO or SO or CRD or LPD or Bulk SO List !! ");
                return;
            }

            if (tabControl1.SelectedIndex == 0) 
            {
                dataGridView1.DataSource = null;
                Cursor.Current = Cursors.WaitCursor;
                Dictionary<string, Object> p = new Dictionary<string, object>();
                p.Add("vSeId", textBox_SeId.Text);
                p.Add("vPO", textBox_PO.Text);         
                p.Add("vCheckShip", checkBox1.Checked);
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_FAST_REPORT_API", "KZ_FAST_REPORT_API.Controllers.Domestic_Transactions_Server", "GetData", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
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
                                //totalBySoRow["inStock_qty"] = rows.Sum(r => ToIntSafe(r["inStock_qty"]));
                                //totalBySoRow["outSoleQty"] = rows.Sum(r => ToIntSafe(r["outSoleQty"]));
                               // totalBySoRow["shipping_qty"] = rows.Sum(r => ToIntSafe(r["shipping_qty"]));

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
                       // totalRow["inStock_qty"] = realRows.Sum(r => ToIntSafe(r["inStock_qty"]));
                        //totalRow["outSoleQty"] = realRows.Sum(r => ToIntSafe(r["outSoleQty"]));
                        //totalRow["shipping_qty"] = realRows.Sum(r => ToIntSafe(r["shipping_qty"]));

                        dtJson.Rows.Add(totalRow);
                        #endregion 


                    }
                    dataGridView1.DataSource = dtJson.DefaultView;

                    dataGridView1.Update();

                    #region New Code by Manohar 2025.08.19 

                    //Cell background color settings
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {


                        string cutQtystr = dataGridView1.Rows[i].Cells["cutQty"].Value?.ToString();
                        string seqtystr = dataGridView1.Rows[i].Cells["se_qty"].Value?.ToString();
                        //string instockqty = dataGridView1.Rows[i].Cells["inStock_qty"].Value?.ToString();
                        string stitchingqty = dataGridView1.Rows[i].Cells["stitchingQty"].Value?.ToString();
                        string assemblyqty = dataGridView1.Rows[i].Cells["assmeblyQty"].Value?.ToString();
                        string packingqty = dataGridView1.Rows[i].Cells["packingQty"].Value?.ToString();
                        // string shippingqty = dataGridView1.Rows[i].Cells["shipping_qty"].Value?.ToString();
                        //string outsoleqty = dataGridView1.Rows[i].Cells["outSoleQty"].Value?.ToString();

                        int cutQty = 0;
                        int seQty = 0;
                        int insqty = 0;
                        int stitqty = 0;
                        int asseqty = 0;
                        int packqty = 0;
                        int shipqty = 0;
                        // int outqty = 0;
                        int.TryParse(cutQtystr, out cutQty);
                        int.TryParse(seqtystr, out seQty);
                        // int.TryParse(instockqty, out insqty);
                        int.TryParse(stitchingqty, out stitqty);
                        int.TryParse(assemblyqty, out asseqty);
                        int.TryParse(packingqty, out packqty);
                        //  int.TryParse(shippingqty, out shipqty);
                        // int.TryParse(outsoleqty, out outqty);
                        if (cutQty == seQty)
                        {
                            dataGridView1.Rows[i].Cells["cutQty"].Style.BackColor = Color.PaleGreen;
                        }

                        else if (cutQty < seQty && insqty < seQty && cutQty >= stitqty
                            && cutQty >= asseqty && cutQty >= packqty && cutQty >= insqty
                            && insqty >= shipqty
                            )
                        {
                            dataGridView1.Rows[i].Cells["cutQty"].Style.BackColor = Color.Yellow;
                        }

                        else
                        {
                            dataGridView1.Rows[i].Cells["cutQty"].Style.BackColor = Color.LightCoral;
                        }

                        //Number of sewing machines
                        if (stitqty == seQty)
                        {
                            dataGridView1.Rows[i].Cells["stitchingQty"].Style.BackColor = Color.PaleGreen;
                        }

                        else if
                            (stitqty < seQty && insqty < seQty && stitqty >= asseqty && stitqty >= packqty && stitqty >= insqty
                            && insqty >= shipqty
                            )

                        {
                            dataGridView1.Rows[i].Cells["stitchingQty"].Style.BackColor = Color.Yellow;
                        }
                        else
                        {
                            dataGridView1.Rows[i].Cells["stitchingQty"].Style.BackColor = Color.LightCoral;
                        }
                        if

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

                        {
                            dataGridView1.Rows[i].Cells["packingQty"].Style.BackColor = Color.PaleGreen;
                        }
                        else if
                            (
                            packqty < seQty && insqty < seQty && packqty >= insqty && insqty >= shipqty
                            )

                        {
                            dataGridView1.Rows[i].Cells["packingQty"].Style.BackColor = Color.Yellow;
                        }
                        else
                        {
                            dataGridView1.Rows[i].Cells["packingQty"].Style.BackColor = Color.LightCoral;
                        }


                        #endregion
                    }

                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }
        }

        private void Export_Click(object sender, EventArgs e)
        {
            string a = "PO Tracking.xls";
            ExportExcels.Export(a, dataGridView1); 
        }

        private void Btn_clear_Click(object sender, EventArgs e)
        {
            textBox_PO.Text = "";
            textBox_SeId.Text = "";
        }
        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int ColumnIndex = e.ColumnIndex;
            string Type = string.Empty;
            if (ColumnIndex == dataGridView1.Columns["cutQty"].Index)  
            {
                Type = "C";
            }
            if (ColumnIndex == dataGridView1.Columns["stitchingQty"].Index)
            {
                Type = "S";
            }
            if (ColumnIndex == dataGridView1.Columns["assmeblyQty"].Index) 
            {
                Type = "L";
            }
            if (ColumnIndex == dataGridView1.Columns["packingQty"].Index)
            {
                Type = "A";
            }
            DataGridView dgv = (DataGridView)sender;
            SizeFinishReport frm = new SizeFinishReport(dgv.Rows[e.RowIndex].Cells["se_id"].Value.ToString(), dgv.Rows[e.RowIndex].Cells["size_no"].Value.ToString(), Type);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }
    }
}
