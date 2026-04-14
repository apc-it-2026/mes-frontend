using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using SJeMES_Framework.Common;
using SJeMES_Framework.WebAPI;

namespace StitchingOutput2
{
    public partial class StitchingOutput2_SelectIn : MaterialForm
    {
        public string deptNo = "";

        public StitchingOutput2_SelectIn()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);
        }

        private void GetIninformation(string warehouse_code = "")
        {
            DataTable dt = new DataTable();
            Dictionary<string, object> p = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(textSectionCode.Text))
            {
                warehouse_code = textSectionCode.Text.ToUpper();
                p.Add("warehouse_code", warehouse_code);
            }

            dataGridView1.DataSource = null;
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.TrackIn_BottomServer", "getIninformation", Program.client.UserToken, JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonConvert.DeserializeObject<DataTable>(json);
                if (dtJson != null && dtJson.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dtJson;
                    dataGridView1.Rows[0].Selected = false;
                }
                else
                {
                    MessageBox.Show("No such data");
                }
            }
            else
            {
                MessageBox.Show("No such data");
            }
        }


        private void btnSelect_Click(object sender, EventArgs e)
        {
            GetIninformation(textSectionCode.Text.Trim());
        }

        private void StitchingOutput2_SelectIn_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            GetIninformation();
        }

        /// <summary>
        ///     warehousing operation
        /// </summary>
        /// <param name="ORG_ID"></param>
        /// <param name="WAREHOUSE_NO"></param>
        /// <param name="LABEL_TYPE"></param>
        /// <param name="SE_SEQ"></param>
        /// <param name="qty"></param>
        /// <param name="SE_ID"></param>
        /// <param name="MATERIAL_NO"></param>
        /// <param name="IntoWH_Mark"></param>
        /// <param name="PROCESS_NO"></param>
        /// <param name="ORG_CODE"></param>
        /// <param name="SCAN_DETPT"></param>
        /// <param name="LABEL_ID"></param>
        /// <param name="SCAN_NAME"></param>
        /// <param name="SCAN_DATE"></param>
        /// <param name="vProductionOrder"></param>
        public void UpdateStockInByProd(string ORG_ID, string WAREHOUSE_NO, string LABEL_TYPE, string SE_SEQ, int qty, string SE_ID, string MATERIAL_NO, string IntoWH_Mark, string PROCESS_NO, string ORG_CODE, string SCAN_DETPT, string LABEL_ID, string SCAN_NAME, string SCAN_DATE, string vProductionOrder)
        {
            DataTable dt = new DataTable();
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("vOrgId", ORG_ID);
            p.Add("vStocNo", WAREHOUSE_NO); //storehouse
            p.Add("vDpetNo", SCAN_DETPT); //Department/Manufacturer Code
            p.Add("vProductionOrder", vProductionOrder); //Sub work order number
            p.Add("vItemNo", MATERIAL_NO); //Part No
            p.Add("vTransType", "101"); //Transaction type
            p.Add("vBatchNo", SE_ID); //batch = order id
            p.Add("vShelfNo", "ALL"); //Storage spaces
            p.Add("qty", qty); ////Inventory quantity
            p.Add("vOperateType", "C"); //Operation type A-scanning warehousing B-manual warehousing C-reporting for work warehousing
            p.Add("vLABEL_TYPE", LABEL_TYPE);
            p.Add("vLABEL_ID", LABEL_ID);
            p.Add("vPROCESS_NO", PROCESS_NO);
            p.Add("vSCAN_NAME", SCAN_NAME);
            p.Add("vSCAN_DETPT", SCAN_DETPT);
            p.Add("vSCAN_DATE", SCAN_DATE);
            p.Add("vID", ORG_CODE);
            p.Add("vse_seq", SE_SEQ);
            p.Add("IntoWH_Mark", IntoWH_Mark);

            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.TrackIn_BottomServer", "UpdateStockInByProd", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonHelper.GetDataTableByJson(json);
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                MessageBox.Show("Inventory failed! ! !");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1 != null && dataGridView1.Rows.Count > 0)
            {
                DataTable dt = dataGridView1.DataSource as DataTable;
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string ORG_ID = row["ORG_ID"].ToString();
                        string SIZE_NO = row["SIZE_NO"].ToString();
                        string SE_SEQ = row["SE_SEQ"].ToString();
                        string SE_ID = row["SE_ID"].ToString();
                        string ART_NO = row["art_no"].ToString();
                        string PO_NO = row["po_no"].ToString();
                        string IntoWH_Mark = row["IntoWH_Mark"].ToString();
                        string PROCESS_NO = row["PROCESS_NO"].ToString();
                        string ORG_CODE = row["ORG_ID"].ToString();
                        string SCAN_DETPT = row["SCAN_DETPT"].ToString();
                        string LABEL_ID = row["LABEL_ID"].ToString();
                        string SCAN_NAME = row["SCAN_NAME"].ToString();
                        string LAST_USER = row["LAST_USER"].ToString();
                        string SCAN_DATE = row["SCAN_DATE"].ToString();
                        string vProductionOrder = row["PRODUCTION_ORDER"].ToString();
                        string WAREHOUSE_NO = row["WAREHOUSE_NO"].ToString();
                        string LABEL_TYPE = row["LABEL_TYPE"].ToString();
                        int qty = int.Parse(row["LABEL_QTY"].ToString());
                        string MATERIAL_NO = row["MATERIAL_NO"].ToString(); //Part No
                        if (!string.IsNullOrWhiteSpace(MATERIAL_NO)) //Item number cannot be empty
                        {
                            UpdateStockInByProd(ORG_ID, WAREHOUSE_NO, LABEL_TYPE, SE_SEQ, qty, SE_ID, MATERIAL_NO, IntoWH_Mark, PROCESS_NO, ORG_CODE, SCAN_DETPT, LABEL_ID, SCAN_NAME, SCAN_DATE, vProductionOrder);
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(textSectionCode.Text))
                {
                    GetIninformation(textSectionCode.Text.Trim());
                }
                else
                {
                    GetIninformation();
                }
            }
            else
            {
                MessageBox.Show("no data available！");
            }
        }
    }
}