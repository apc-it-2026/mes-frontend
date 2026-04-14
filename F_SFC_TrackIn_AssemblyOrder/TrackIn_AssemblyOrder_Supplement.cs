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

namespace F_SFC_TrackIn_AssemblyOrder
{
    public partial class TrackIn_AssemblyOrder_Supplement : MaterialForm
    {

        DataTable workDayDt = null;
        DataTable wipWarehouseDt = null;
        string vOrgId = "";
        string vSeId = "";
        decimal vQty = 0;
        string vProdOrder = "";
        string vMainProdOrder = "";
        string vStock_orgId = "";
        string vItemNo = "";
        string vBatchNo = "";
        string vMaterialNO = "";

        public TrackIn_AssemblyOrder_Supplement()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        public TrackIn_AssemblyOrder_Supplement(string dept_no)
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            textBoxDDept.Text = dept_no;
            textBox3.Text = "1";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SupplementForm_Load(object sender, EventArgs e)
        {
            LoadSeId();
            LoadStock();
        }

        private void LoadSeId()
        {
            autocompleteMenu1.Items = null;
            autocompleteMenu1.MaximumSize = new System.Drawing.Size(400, 400);
            var columnWidth = new int[] { 50, 400 };
            workDayDt = GetSeId();
            int n = 1;
            for (int i = 0; i < workDayDt.Rows.Count; i++)
            {
                autocompleteMenu1.AddItem(new MulticolumnAutocompleteItem(
                    new[] { n + "", workDayDt.Rows[i]["MAIN_PROD_ORDER"].ToString() + " " + workDayDt.Rows[i]["SE_ID"].ToString() + " " + workDayDt.Rows[i]["PO"].ToString() }, workDayDt.Rows[i]["MAIN_PROD_ORDER"].ToString() + "|" + workDayDt.Rows[i]["SE_ID"].ToString() + "|" + workDayDt.Rows[i]["PO"].ToString() + "|" + workDayDt.Rows[i]["ArtNo"].ToString())
                { ColumnWidth = columnWidth, ImageIndex = n });
                n++;
            }
        }

        private DataTable GetSeId()
        {
            DataTable dt = new DataTable();
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("DeptNo", textBoxDDept.Text);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralOrderServer", "GetSeOrderByDeptNo", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                //dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json);
                if(dt.Rows.Count > 0)
                {
                    vOrgId = dt.Rows[0]["ORG_ID"].ToString();
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            return dt;
        }

        private void LoadStock()
        {
            autocompleteMenu2.Items = null;
            autocompleteMenu2.MaximumSize = new System.Drawing.Size(400, 400);
            var columnWidth = new int[] { 50, 400 };
            wipWarehouseDt = GetStock();
            int n = 1;
            for (int i = 0; i < wipWarehouseDt.Rows.Count; i++)
            {
                autocompleteMenu2.AddItem(new MulticolumnAutocompleteItem(
                    new[] { n + "", wipWarehouseDt.Rows[i]["WAREHOUSE_CODE"].ToString() + " " + wipWarehouseDt.Rows[i]["WAREHOUSE_NAME"].ToString() + " " + wipWarehouseDt.Rows[i]["ORG_ID"].ToString() }, wipWarehouseDt.Rows[i]["WAREHOUSE_CODE"].ToString() + "|" + wipWarehouseDt.Rows[i]["WAREHOUSE_NAME"].ToString() + "|" + wipWarehouseDt.Rows[i]["ORG_ID"].ToString())
                { ColumnWidth = columnWidth, ImageIndex = n });
                n++;
            }
        }

        private DataTable GetStock()
        {
            DataTable dt = new DataTable();
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vOrgId", vOrgId);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralOrderServer", "GetWIPWarehouse", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBoxButtons mess = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要提交资料吗？", "vQty", mess);
                if (dr == DialogResult.OK)
                {
                    if (string.IsNullOrEmpty(textBoxDDept.Text))
                    {
                        return;
                    }
                    if (string.IsNullOrEmpty(textBoxMainProdOrder.Text) && string.IsNullOrEmpty(textBoxPO.Text) && string.IsNullOrEmpty(textBoxArtNo.Text))
                    {
                        return;
                    }
                    if (string.IsNullOrEmpty(comboBoxSizeNo.Text))
                    {
                        return;
                    }
                    if (string.IsNullOrEmpty(textStockCode.Text))
                    {
                        return;
                    }
                    int leftqty = int.Parse(textBoxLeftQty.Text);
                    int rightqty = int.Parse(textBoxRightQty.Text);

                    vQty = (decimal.Parse(textBoxLeftQty.Text) + decimal.Parse(textBoxRightQty.Text)) / 2;
                    //// if (vQty <= GetStockRemainingQty(vStock_orgId, textStockCode.Text, vItemNo)) //报工数量大于库存数量时，不允许报工
                    //if (GetStockRemainingQty(vStock_orgId, textStockCode.Text, vItemNo, vBatchNo, vQty) == 1)
                    if (true)
                    {
                        Dictionary<string, Object> p = new Dictionary<string, object>();
                        p.Add("vOrgId", vOrgId);
                        p.Add("vDDept", textBoxDDept.Text);
                        p.Add("vSeId", vSeId);
                        p.Add("vSeSeq", int.Parse(textBox3.Text));
                        p.Add("vSizeNo", comboBoxSizeNo.Text);
                        p.Add("vQty", vQty);
                        p.Add("vLeftQty", leftqty);
                        p.Add("vRightQty", rightqty);
                        p.Add("vInOut", "IN");
                        p.Add("vArtNo", textBoxArtNo.Text);
                        p.Add("vPO", textBoxPO.Text);
                        p.Add("vSizeSeq", int.Parse(textBox8.Text));
                        p.Add("vRountNo", "C");
                        p.Add("vProdOrder", vProdOrder);
                        p.Add("vMainProdOrder", vMainProdOrder);
                        p.Add("vStock_orgId", vStock_orgId);
                        p.Add("vStockNo", textStockCode.Text);
                        p.Add("vItemNo", vItemNo);
                        p.Add("vTransType", "Z61");
                        p.Add("vBatchNo", vBatchNo);
                        p.Add("vShelfNo", "ALL");
                        p.Add("vOperateType", "C");
                        string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.SupplementServer", "updateStock", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                        if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                        {
                            SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Success");
                        }
                        else
                        {
                            SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                        }
                    }
                    else
                    {
                        MessageBox.Show("库存数量不足！");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxMainProdOrder.Text) && textBoxMainProdOrder.Text.Split('|').Length == 4)
            {
                vSeId = textBoxMainProdOrder.Text.Split('|')[1].ToString();
                textBoxPO.Text = textBoxMainProdOrder.Text.Split('|')[2].ToString();
                textBoxArtNo.Text = textBoxMainProdOrder.Text.Split('|')[3].ToString();
                textBoxMainProdOrder.Text = textBoxMainProdOrder.Text.Split('|')[0].ToString();
            }
            LoadSeSize();
        }


        DataTable sizeDatatable;
        private void LoadSeSize()
        {
           comboBoxSizeNo.Items.Clear();
           var columnWidth = new int[] { 50, 400 };
           sizeDatatable = new DataTable();
            Dictionary<string, Object> p = new Dictionary<string, object>();
            //获取工厂编号
            string org = (textBoxDDept.Text).Substring(0, 4);
            p.Add("mainid", textBoxMainProdOrder.Text);
            p.Add("org", org);//工厂编号
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.SupplementServer", "GetSeOrderSize", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                sizeDatatable = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                int n = 1;
                for (int i = 0; i < sizeDatatable.Rows.Count; i++)
                {
                    comboBoxSizeNo.Items.Add(sizeDatatable.Rows[i]["MATERIAL_SPECIFICATIONS"].ToString());
                    n++;
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sizeDatatable.Rows.Count>0)
            {
                textBox8.Text = "0";
                vProdOrder = sizeDatatable.Rows[comboBoxSizeNo.SelectedIndex]["PRODUCTION_ORDER"].ToString();
                vMainProdOrder = sizeDatatable.Rows[comboBoxSizeNo.SelectedIndex]["MAIN_PROD_ORDER"].ToString();
                vMaterialNO = sizeDatatable.Rows[comboBoxSizeNo.SelectedIndex]["MATERIAL_NO"].ToString();
                vItemNo = GetItemNo(sizeDatatable.Rows[comboBoxSizeNo.SelectedIndex]["PRODUCTION_ORDER"].ToString());
                vBatchNo = sizeDatatable.Rows[comboBoxSizeNo.SelectedIndex]["BATCH_NO"].ToString();
            }
        }

        private string GetItemNo(string vProdOrder)
        {
            string itemNo = "";
            DataTable dt = new DataTable();
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vProdOrder", vProdOrder);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.ProductionInputOrderTwoServer", "GetItemNo", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                //dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                itemNo = json;
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            return itemNo;
        }

        private decimal GetStockRemainingQty(string vStock_orgId,string vStock, string vItemNo, string vBatchNo, decimal vQty)
        {
            decimal qty = 0;
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vStock_orgId", vStock_orgId);
            p.Add("vStock", vStock);
            p.Add("vItemNo", vItemNo);
            p.Add("vBatchNo", vBatchNo);
            p.Add("vShelfNo", "ALL");
            p.Add("vQty", vQty);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.ProductionInputOrderTwoServer", "GetStockRemainingQty", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                qty = decimal.Parse(json);
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            return qty;
        }

        private void textStockCode_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textStockCode.Text) && textStockCode.Text.Split('|').Length == 3)
            {
                vStock_orgId = textStockCode.Text.Trim().ToString().ToUpper().Split(new Char[] { '|' })[2];
                textStockName.Text = textStockCode.Text.Trim().ToString().ToUpper().Split(new Char[] { '|' })[1];
                textStockCode.Text = textStockCode.Text.Trim().ToString().ToUpper().Split(new Char[] { '|' })[0];
            }
        }
    }
}
