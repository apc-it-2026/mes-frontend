using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using SJeMES_Framework.WebAPI;

namespace ProcessAllocation
{
    public partial class ProcessAllocation : Form
    {
        //Add a record collection
        private List<int> rowIndexList = new List<int>();
        private DataTable depart = null;
        public ProcessAllocation()
        {
            InitializeComponent();

            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);
        }

        private void ProcessAllocationForm_Load(object sender, EventArgs e)
        {
            FormLoad();
            //Activate the OnPageChanged event  
            //pagerControl1.OnPageChanged += new EventHandler(pagerControl1_OnPageChanged);
        }

        private void FormLoad()
        {
            dataGridView1.AutoGenerateColumns = false; //Disable auto-generated columns
            DataTable dtJson = GetOrg();
            if (dtJson != null && dtJson != null && dtJson.Rows.Count > 0)
            {
                DataRow row = dtJson.NewRow();
                row["org_name"] = "All";
                dtJson.Rows.InsertAt(row, 0);
                comboBoxOrgId.DisplayMember = "org_name";
                comboBoxOrgId.ValueMember = "org_id";
                comboBoxOrgId.DataSource = dtJson;
                comboBoxOrgId.SelectedIndex = 0; //Default is the first item
            }

            DataTable dtJsonRoutNo = GetRoutNo();

            if (dtJsonRoutNo != null && dtJsonRoutNo.Rows.Count > 0)
            {
                DataRow rowRoutNo = dtJsonRoutNo.NewRow();
                rowRoutNo["rout_name_z"] = "All";
                dtJsonRoutNo.Rows.InsertAt(rowRoutNo, 0);
                comboBoxRoutNo.DisplayMember = "rout_name_z";
                comboBoxRoutNo.ValueMember = "rout_no";
                comboBoxRoutNo.DataSource = dtJsonRoutNo;
                comboBoxRoutNo.SelectedIndex = 0; //Default is the first item
            }

            //Add smart search
            txtWorkCenter.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtWorkCenter.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtWorkCenterAutoComleteCustom(txtWorkCenter);
        }

        private void pagerControl1_OnPageChanged(object sender, EventArgs e)
        {
            BindData();

            //pagerControl1.DrawControl(count);
        }

        /// <summary>
        ///     Get all process records
        /// </summary>
        /// <returns></returns>
        private DataTable GetRoutNo()
        {
            Dictionary<string, object> parm = new Dictionary<string, object>();
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "GetRoutNo", Program.client.UserToken, JsonConvert.SerializeObject(parm));
            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                string json = retJson["RetData"].ToString();
                DataTable dtJson = JsonConvert.DeserializeObject<DataTable>(json);
                return dtJson;
            }
            else
            {
                MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
                return null;
            }
        }

        /// <summary>
        ///     Get all factory records
        /// </summary>
        /// <returns></returns>
        private DataTable GetOrg()
        {
            Dictionary<string, object> parm = new Dictionary<string, object>();
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "GetOrg", Program.client.UserToken, JsonConvert.SerializeObject(parm));

            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                string json = retJson["RetData"].ToString();
                DataTable dtJson = JsonConvert.DeserializeObject<DataTable>(json);
                return dtJson;
            }
            else
            {
                MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
                return null;
            }
        }

        private void txtWorkCenterAutoComleteCustom(TextBox textBoxName)
        {
            Dictionary<string, object> parm = new Dictionary<string, object>();
            List<string> strList = new List<string>();

            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "GetAllDepts", Program.client.UserToken, JsonConvert.SerializeObject(parm));

            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                string json = retJson["RetData"].ToString();
                DataTable dtJson = JsonConvert.DeserializeObject<DataTable>(json);
                depart = dtJson;
               
                if (dtJson.Rows.Count > 0)
                {
                    foreach (DataRow row in dtJson.Rows)
                    {
                        strList.Add(row["DEPARTMENT_CODE"].ToString());
                    }

                    //The overall assignment to AutoCompleteCustomSource 
                    textBoxName.AutoCompleteCustomSource.AddRange(strList.ToArray());
                }
                else
                {
                    MessageHelper.ShowErr(this, "Automatically prompt error, no data");
                }
            }
            else
            {
                MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            BindData();
        }

        /// <summary>
        ///     bind data
        /// </summary>
        private void BindData()
        {
            int count = 0;

            //pagerControl1.PageSize = 30;//Set the number of records to display

            Dictionary<string, object> parm = new Dictionary<string, object>();
            parm.Add("mainOrder", txtMainOrder.Text);
            parm.Add("subOrderNo", txtSubOrderNo.Text);

            if (comboBoxRoutNo.Text != "All")
            {
                parm.Add("routNo", comboBoxRoutNo.SelectedValue.ToString());
            }

            parm.Add("salesOrder", txtSalesOrder.Text);
            parm.Add("dept", txtWorkCenter.Text);

            if (comboBoxOrgId.Text != "All")
            {
                parm.Add("orgId", comboBoxOrgId.SelectedValue.ToString());
            }

            if (string.IsNullOrWhiteSpace(txtMainOrder.Text))
            {
                MessageHelper.ShowErr(this, "Master work order number cannot be empty");
                return;
            }

            //if (string.IsNullOrWhiteSpace(txtSubOrderNo.Text))
            //{
            //    MessageHelper.ShowErr(this, "子工单编号不能为空");
            //    return;
            //}

            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "ProcessAllocationQuery", Program.client.UserToken, JsonConvert.SerializeObject(parm));

            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                string json = retJson["RetData"].ToString();
                DataTable dtJson = JsonConvert.DeserializeObject<DataTable>(json);
                if (dtJson.Rows.Count > 0)
                {
                    count = dtJson.Rows.Count;
                    dataGridView1.DataSource = dtJson;
                    dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter; //Click the cell
                }
                else
                {
                    dataGridView1.DataSource = null;
                }
            }
            else
            {
                MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
                dataGridView1.DataSource = null;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            DataTable dt = dataGridView1.DataSource as DataTable;
            if (dt != null)
            {
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Selected = false;
                DataRow row = dt.NewRow();
                int number = int.Parse(dt.Rows[dt.Rows.Count - 1]["NO"].ToString()) + 1;
                row["NO"] = number.ToString();
                row["main_production_order"] = dt.Rows[dt.Rows.Count - 1]["main_production_order"].ToString();
                row["production_order"] = dt.Rows[dt.Rows.Count - 1]["production_order"].ToString();
                row["MATERIAL_SPECIFICATIONS"] = dt.Rows[dt.Rows.Count - 1]["MATERIAL_SPECIFICATIONS"].ToString();
                row["QTY"] = dt.Rows[dt.Rows.Count - 1]["QTY"].ToString();

                dt.Rows.InsertAt(row, dataGridView1.Rows.Count);
                dataGridView1.DataSource = dt;
                rowIndexList.Add(number);
                //The next two lines set the cursor position。
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Selected = true;
                dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0];
            }
            else
            {
                MessageHelper.ShowErr(this, "Add failed! You must have a query record and select it before you can add it");
            }
        }

        //Determine if the factory exists
        public bool Isorgid(string orgid)
        {
            
            bool Istrue = false;
            DataTable dtJson = GetOrg();
            foreach (DataRow dr in dtJson.Rows)
            {
                if (dr["ORG_ID"].ToString() == orgid)
                {
                    Istrue = true;
                }
            }
            return Istrue;
        }

        //Determining the existence of Chinese and Western at work
        public bool Isdept(string dept)
        {

            bool Istrue = false;           
            foreach (DataRow dr in depart.Rows)
            {
                if (dr["DEPARTMENT_CODE"].ToString() == dept)
                {
                    Istrue = true;
                }
            }
            return Istrue;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            int totalCount = 0;
            //remaining quantity
            int remainQty = 0;
            int tempNewProductionlineQty = 0;
            //The loop first judges whether there is an error that does not meet the requirements
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                string production_order = item.Cells["子工单号"].FormattedValue.ToString();
                string orgId = item.Cells["工厂"].FormattedValue.ToString();
                string productionline_no = item.Cells["工作中心"].FormattedValue.ToString();
                string qtyStr = item.Cells["工单数量"].FormattedValue.ToString();
                object effectivedateObj = item.Cells["生效日期"].FormattedValue;
                DateTime effectivedate = new DateTime();
                if (!string.IsNullOrWhiteSpace(effectivedateObj.ToString()))
                {
                    effectivedate = DateTime.Parse(effectivedateObj.ToString());
                }

                string mes010a10Id = item.Cells["调拨记录表ID"].FormattedValue.ToString();
                int number = int.Parse(item.Cells["序号"].FormattedValue.ToString());
                if (string.IsNullOrWhiteSpace(orgId))
                {
                    MessageHelper.ShowErr(this, $"serial number is{number}Factory code cannot be empty！");
                    return;
                }

                if (string.IsNullOrWhiteSpace(productionline_no))
                {
                    MessageHelper.ShowErr(this, $"serial number is{number}Work center cannot be empty！");
                    return;
                }


                if (string.IsNullOrWhiteSpace(item.Cells["新工单数量"].FormattedValue.ToString()))
                {
                    MessageHelper.ShowErr(this, $"serial number is{number}New work order quantity cannot be empty！");
                    return;
                }

                int newProductionlineQty = int.Parse(item.Cells["新工单数量"].FormattedValue.ToString());

                if (effectivedate == DateTime.MinValue)
                {
                    MessageHelper.ShowErr(this, $"serial number is{number}Effective date cannot be empty！");
                    return;
                }

                if (string.IsNullOrWhiteSpace(item.Cells["生效日期"].FormattedValue.ToString()))
                {
                    MessageHelper.ShowErr(this, $"serial number is{number}Effective date cannot be empty, please select it first in the tab!");
                    return;
                }

                if (!string.IsNullOrWhiteSpace(item.Cells["累计投产数量"].FormattedValue.ToString()))
                {
                    int sumInputQty = int.Parse(item.Cells["累计投产数量"].FormattedValue.ToString());

                    if (newProductionlineQty < sumInputQty)
                    {
                        MessageHelper.ShowErr(this, $"serial number is{number}The number of new work orders cannot be less than the cumulative production quantity");
                        return;
                    }
                }

                int qty = 0;
                if (!string.IsNullOrWhiteSpace(qtyStr))
                {
                    qty = int.Parse(qtyStr);
                    if (newProductionlineQty > qty)
                    {
                        MessageHelper.ShowErr(this, $"serial number is{number}The number of new work orders cannot be greater than the number of work orders");
                        return;
                    }
                }

                Dictionary<string, object> parm = new Dictionary<string, object>();
                parm.Add("production_order", production_order); //Sub work order
                parm.Add("newProductionlineQty", newProductionlineQty);
                parm.Add("effectiveDate", effectivedate);
                parm.Add("orgId", orgId); //factory
                parm.Add("productionline_no", productionline_no); //Work Center

                //Determine if the factory exists
                if (Isorgid(orgId) == false)
                {
                    MessageHelper.ShowErr(this, $"serial number is{item.Cells["序号"].FormattedValue}Factory does not exist！");
                    return;
                }
                // MessageBox.Show("123");
                //Determine if a work center exists
                if (Isdept(productionline_no) ==false)
                {
                    MessageHelper.ShowErr(this, $"serial number is{item.Cells["序号"].FormattedValue}work center does not exist！");
                    return;
                }
               // MessageBox.Show("223");
                if (!string.IsNullOrWhiteSpace(mes010a10Id))
                {
                    //Determine whether the saved records exist the same
                    int mes010a10Id2 = IsExistProcessAllocationSubProductionOrder(parm);

                    if (!string.IsNullOrWhiteSpace(mes010a10Id))
                    {
                        bool isExistAllocationSubProductionOrder = mes010a10Id2 > 0 && mes010a10Id != mes010a10Id2.ToString();


                        if (isExistAllocationSubProductionOrder)
                        {
                            MessageHelper.ShowErr(this, $"serial number is{item.Cells["序号"].FormattedValue}Factories and work centers already exist！");
                            return;
                        }
                    }
                   
                }
                else
                {
                    //Determine whether the saved records exist the same
                    int mes010a10Id2 = IsExistProcessAllocationSubProductionOrder2(parm);

                    if (!string.IsNullOrWhiteSpace(mes010a10Id))
                    {
                        bool isExistAllocationSubProductionOrder = mes010a10Id2 > 0 && mes010a10Id != mes010a10Id2.ToString();

                        if (isExistAllocationSubProductionOrder)
                        {
                            MessageHelper.ShowErr(this, $"serial number is{item.Cells["序号"].FormattedValue}Factories and work centers already exist！");
                            return;
                        }
                    }
                   
                }


                //Dictionary<string, object> parmSubOrder = new Dictionary<string, object>();
                //parmSubOrder.Add("production_order", production_order);
                //int totalQty = GetSubOrderQtyList(parmSubOrder);//子工单订单数量
                //remainQty = qty - totalQty - newProductionlineQty;

                tempNewProductionlineQty += newProductionlineQty;
                remainQty = qty - tempNewProductionlineQty;
                if (remainQty < 0)
                {
                    MessageHelper.ShowErr(this, $"serial number is{number}The sum of the quantity of multiple new work orders of the same sub-work order cannot be greater than the original order quantity of this sub-work order");
                    return;
                }
            }

            //loop save data
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                string production_order = item.Cells["子工单号"].FormattedValue.ToString();
                string orgId = item.Cells["工厂"].FormattedValue.ToString();
                string productionline_no = item.Cells["工作中心"].FormattedValue.ToString();
                object effectivedateObj = item.Cells["生效日期"].FormattedValue;
                DateTime effectivedate = new DateTime();
                if (!string.IsNullOrWhiteSpace(effectivedateObj.ToString()))
                {
                    effectivedate = DateTime.Parse(effectivedateObj.ToString());
                }

                string mes010a10Id = item.Cells["调拨记录表ID"].FormattedValue.ToString();
                int newProductionlineQty = int.Parse(item.Cells["新工单数量"].FormattedValue.ToString());
                Dictionary<string, object> parm = new Dictionary<string, object>();
                parm.Add("production_order", production_order); //Sub work order
                parm.Add("newProductionlineQty", newProductionlineQty);
                parm.Add("effectiveDate", effectivedate);
                parm.Add("orgId", orgId); //factory
                parm.Add("productionline_no", productionline_no); //Work Center
                parm.Add("mes010a10Id", mes010a10Id);

                int number = int.Parse(item.Cells["序号"].FormattedValue.ToString());
                if (rowIndexList.Contains(number))
                {
                    parm.Add("isAdd", true);
                }

                string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "InsertProcessAllocationQuery", Program.client.UserToken, JsonConvert.SerializeObject(parm));

                var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
                if (Convert.ToBoolean(retJson["IsSuccess"]))
                {
                    string json = retJson["RetData"].ToString();
                    bool isInserted = bool.Parse(json);
                    if (isInserted)
                    {
                        totalCount += 1;
                    }
                    else
                    {
                        MessageHelper.ShowErr(this, "Failed to save！");
                    }
                }
                else
                {
                    MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
                }
            }

            if (totalCount > 0)
            {
                MessageHelper.ShowSuccess(this, $"successfully saved{totalCount}Records");
                rowIndexList.Clear();
                BindData();
            }
        }

        private int IsExistProcessAllocationSubProductionOrder(Dictionary<string, object> parm)
        {
            string isExistedRet = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "IsExistProcessAllocationSubProductionOrder", Program.client.UserToken, JsonConvert.SerializeObject(parm));

            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(isExistedRet);
            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                string json = retJson["RetData"].ToString();
                int mes010a10Id = int.Parse(json);
                return mes010a10Id;
            }
            else
            {
                MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
                return 0;
            }
        }

        private int IsExistProcessAllocationSubProductionOrder2(Dictionary<string, object> parm)
        {
            string isExistedRet = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "IsExistedProcessAllocation2", Program.client.UserToken, JsonConvert.SerializeObject(parm));

            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(isExistedRet);
            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                string json = retJson["RetData"].ToString();
                int mes010a10Id = int.Parse(json);
                return mes010a10Id;
            }
            else
            {
                MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
                return 0;
            }
        }

        private int GetSubOrderQtyList(Dictionary<string, object> parm)
        {
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "GetSubOrderQtyList", Program.client.UserToken, JsonConvert.SerializeObject(parm));

            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                string json = retJson["RetData"].ToString();
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(json);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[0]["totalQty"].ToString()))
                    {
                        int qty = int.Parse(dt.Rows[0]["totalQty"].ToString());
                        return qty;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
                return 0;
            }
        }

        /// <summary>
        ///     reset
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            txtMainOrder.Text = "";
            txtSalesOrder.Text = "";
            comboBoxRoutNo.SelectedIndex = 0;
            txtSalesOrder.Text = "";
            txtWorkCenter.Text = "";
            comboBoxOrgId.SelectedIndex = 0;
            txtSubOrderNo.Text = "";
        }

        /// <summary>
        ///     closure
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    string id = dataGridView1.CurrentRow.Cells["调拨记录表ID"].Value.ToString();
                    string production_order = dataGridView1.CurrentRow.Cells["子工单号"].Value.ToString();

                    Dictionary<string, object> parm = new Dictionary<string, object>();
                    parm.Add("id", id);
                    parm.Add("production_order", production_order);

                    if (string.IsNullOrWhiteSpace(id))
                    {
                        MessageHelper.ShowErr(this, "Deletion failed, there is no transfer record for this record！");
                        return;
                    }

                    string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "DeleteProcessAllocation", Program.client.UserToken, JsonConvert.SerializeObject(parm));

                    var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
                    if (Convert.ToBoolean(retJson["IsSuccess"]))
                    {
                        string json = retJson["RetData"].ToString();
                        bool isDeleted = JsonConvert.DeserializeObject<bool>(json);
                        if (isDeleted)
                        {
                            MessageHelper.ShowSuccess(this, "successfully deleted！");
                            BindData();
                        }
                        else
                        {
                            MessageHelper.ShowErr(this, "failed to delete！");
                        }
                    }
                    else
                    {
                        MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
                    }
                }
                else
                {
                    MessageHelper.ShowErr(this, "you haven't selected the row！");
                }
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            List<string> strList = new List<string>();
            //get the displayed control
            var tb = e.Control as DataGridViewTextBoxEditingControl;
            if (tb != null)
            {
                tb.AutoCompleteCustomSource = null;
                tb.AutoCompleteSource = AutoCompleteSource.CustomSource;
                tb.AutoCompleteMode = AutoCompleteMode.Suggest;
                //Check the corresponding column
                if (dgv.CurrentCell.OwningColumn.Name == "工厂")
                {
                    DataTable dtJson = GetOrg();
                    if (dtJson.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtJson.Rows)
                        {
                            strList.Add(row["org_id"].ToString());
                        }

                        //The overall assignment to AutoCompleteCustomSource 
                        tb.AutoCompleteCustomSource.AddRange(strList.ToArray());
                    }
                    else
                    {
                        MessageHelper.ShowErr(this, "Automatically prompt error, no data");
                    }
                }
                else if (dgv.CurrentCell.OwningColumn.Name == "工作中心")
                {
                    txtWorkCenterAutoComleteCustom(tb);
                }
            }
        }
    }
}