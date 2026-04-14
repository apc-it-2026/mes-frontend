using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;
using AutocompleteMenuNS;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using SJeMES_Framework.WebAPI;

namespace CuttingAllocation
{
    public partial class CuttingAllocation : MaterialForm
    {
        private List<int> rowIndexList = new List<int>();

        public CuttingAllocation()
        {
            InitializeComponent();

            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);

        }

        private void CuttingAllocationQueryForm_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            DataTable dtJson = GetOrg();

            if (dtJson != null && dtJson.Rows.Count > 0)
            {
                DataRow row = dtJson.NewRow();
                row["org_name"] = "All";
                dtJson.Rows.InsertAt(row, 0);
                comboBoxFactory.DisplayMember = "org_name";
                comboBoxFactory.ValueMember = "org_id";
                comboBoxFactory.DataSource = dtJson;
                comboBoxFactory.SelectedIndex = 0; //Default is the first item
            }
            txtWorkCenter.AutoCompleteCustomSource = null;
            txtWorkCenter.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtWorkCenter.AutoCompleteMode = AutoCompleteMode.Suggest;

             //txtMainOrderNoAutoComleteCustom(txtMainOrderNo);  commented by Ashok on 2025/04/18
            //txtWorkCenterAutoComleteCustom(txtWorkCenter);commented by Ashok on 2025/04/18

            txtWorkCenterAutoComleteCustom();
            autocompleteMenu1.SetAutocompleteMenu(txtWorkCenter, autocompleteMenu1);
        }
        #region commented by Ashok on 2025/04/18
        //private void txtMainOrderNoAutoComleteCustom(TextBox textBoxName)
        //{
        //    Dictionary<string, object> parm = new Dictionary<string, object>();
        //    List<string> strList = new List<string>();

        //    string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.StitchingInOutServer", "GetMainOrder", Program.client.UserToken, JsonConvert.SerializeObject(parm));
        //    var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
        //    if (Convert.ToBoolean(retJson["IsSuccess"]))
        //    {
        //        string json = retJson["RetData"].ToString();
        //        DataTable dtJson = JsonConvert.DeserializeObject<DataTable>(json);

        //        if (dtJson.Rows.Count > 0)
        //        {
        //            foreach (DataRow row in dtJson.Rows)
        //            {
        //                strList.Add(row["udf01"].ToString());
        //            }

        //            //The overall assignment to AutoCompleteCustomSource 
        //            textBoxName.AutoCompleteCustomSource.AddRange(strList.ToArray());
        //        }
        //        else
        //        {
        //            MessageHelper.ShowErr(this, "Automatically prompt error, no data");
        //        }
        //    }
        //    else
        //    {
        //        MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
        //    }
        //}
        #endregion
        #region Commented by Ashok on 2025/04/18
        //private void txtWorkCenterAutoComleteCustom(TextBox textBoxName)
        //{
        //    Dictionary<string, object> parm = new Dictionary<string, object>();
        //    List<string> strList = new List<string>();

        //    string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "GetAllDepts", Program.client.UserToken, JsonConvert.SerializeObject(parm));
        //    var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
        //    if (Convert.ToBoolean(retJson["IsSuccess"]))
        //    {
        //        string json = retJson["RetData"].ToString();
        //        DataTable dtJson = JsonConvert.DeserializeObject<DataTable>(json);

        //        if (dtJson.Rows.Count > 0)
        //        {
        //            foreach (DataRow row in dtJson.Rows)
        //            {
        //                strList.Add(row["DEPARTMENT_CODE"].ToString());
        //            }

        //            //The overall assignment to AutoCompleteCustomSource 
        //            textBoxName.AutoCompleteCustomSource.AddRange(strList.ToArray());
        //        }
        //        else
        //        {
        //            MessageHelper.ShowErr(this, "automatic error message,no data");
        //        }
        //    }
        //    else
        //    {
        //        MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
        //    }
        //}
        #endregion

        private void txtWorkCenterAutoComleteCustom()
        {
            autocompleteMenu1.Items = null;
            autocompleteMenu1.MaximumSize = new System.Drawing.Size(350, 350);
            var columnWidth = new int[] { 50, 250 };
            Dictionary<string, object> parm = new Dictionary<string, object>();
            List<string> strList = new List<string>();
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "GetAllDepts", Program.client.UserToken, JsonConvert.SerializeObject(parm));
            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                string json = retJson["RetData"].ToString();
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(json);
                int n = 1;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        autocompleteMenu2.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dt.Rows[i]["DEPARTMENT_CODE"].ToString() + " " + dt.Rows[i]["DEPARTMENT_CODE"].ToString() }, dt.Rows[i]["DEPARTMENT_CODE"].ToString()) { ColumnWidth = columnWidth, ImageIndex = n });
                        n++;
                    }
                  
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

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMainOrderNo.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Master PO!!");
                return;
            }
            if (txtMainOrderNo.Text.Length != 12)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Check Master PO!!");
                return;
            }
            string mainProductionOrder = txtMainOrderNo.Text;
            if (mainProductionOrder.Length == 12)
            {
                DataTable CheckCuttingLine = CheckMasterPOCancelledOrNot(mainProductionOrder);

                if (CheckCuttingLine.Rows.Count > 0 && CheckCuttingLine != null)
                {
                    if (CheckCuttingLine.Rows[0]["STATUS"].ToString() == "99")
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "Your Not Allowed to share. SO hasbeen Cancelled or Rejected");
                        return;
                    }
                }
            }
            dataGridView1.DataSource = null;
            if (!checkmainworkorder(txtMainOrderNo.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Only Upper Master PO allowed to Share the data!!");
                return;
            }
            BindData();
        }
        #region Newly Added by Shyam 2025.04.16 
        //private bool CheckMasterPO(string MasterPO)
        //{
        //    DataTable dt = new DataTable();

        //    Dictionary<string, object> p = new Dictionary<string, object>();

        //    p.Add("MasterPO", MasterPO);

        //    string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTLAPI", "KZ_RTLAPI.Controllers.GeneralServer", "CheckPO_Rejected_Or_Not", Program.client.UserToken, JsonConvert.SerializeObject(p));
        //    bool isOK = Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]);
        //    if (!isOK)
        //    {
        //        MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
        //        return false;
        //    }

        //    return true;


        //}


        private DataTable CheckMasterPOCancelledOrNot(string MasterPO)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("MasterPO", MasterPO);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_RTLAPI", "KZ_RTLAPI.Controllers.GeneralServer", "CheckMasterPOCancelledOrNot", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(retJson["RetData"].ToString());
                return dt;
            }
            else
            {
                MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
                return null;
            }

        }
        #endregion


        private bool checkmainworkorder(string MasterPO)
        {
            Dictionary<string, object> parm = new Dictionary<string, object>();
            parm.Add("MasterPO", MasterPO);

            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTLAPI", "KZ_RTLAPI.Controllers.CuttingLabelServer", "CheckUpperMasterPO", Program.client.UserToken, JsonConvert.SerializeObject(parm));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                bool isExisted = JsonConvert.DeserializeObject<bool>(json);
                return isExisted;
            }
            else
            {
                //MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                MessageHelper.ShowErr(this, "No Data Found !!");
                return false;
            }

        }
        /// <summary>
        ///     bind data
        /// </summary>
        private void BindData()
        {
            Cursor Cursor = Cursors.WaitCursor;    
            Dictionary<string, object> parm = new Dictionary<string, object>();
            parm.Add("udf01", txtMainOrderNo.Text);
            if (comboBoxFactory.Text != "All")
            {
                parm.Add("org", comboBoxFactory.SelectedValue.ToString());
            }
            parm.Add("cutting_dept", txtWorkCenter.Text);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "CuttingAllocationQuery", Program.client.UserToken, JsonConvert.SerializeObject(parm));
            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                string json = retJson["RetData"].ToString();
                DataTable dtJson = JsonConvert.DeserializeObject<DataTable>(json);
                if (dtJson.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dtJson;
                    dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter; 
                }
                else
                {
                    dataGridView1.DataSource = null;
                    MessageHelper.ShowErr(this, "No Data Found");
                }
            }
            else
            {
                MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int totalCount = 0;

            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                string main_production_order = item.Cells["主订单号"].FormattedValue.ToString();
                string cutting_dept_name = item.Cells["裁剪单位"].FormattedValue.ToString();
                string sticking_dept_name = item.Cells["针车单位"].FormattedValue.ToString();

                int number = int.Parse(item.Cells["序号"].FormattedValue.ToString());
                if (string.IsNullOrWhiteSpace(cutting_dept_name))
                {
                    MessageHelper.ShowErr(this, $"Cutting unit at serial number {number} is cannot be empty！");
                    return;
                }

                if (string.IsNullOrWhiteSpace(sticking_dept_name))
                {
                    MessageHelper.ShowErr(this, $"Stitching unit at serial number {number} is cannot be empty！");
                    return;
                }

                Dictionary<string, object> parm = new Dictionary<string, object>();
                parm.Add("main_production_order", main_production_order);
                parm.Add("cutting_dept_name", cutting_dept_name);
                parm.Add("sticking_dept_name", sticking_dept_name);

                //Determine whether to add the generated serial number
                if (rowIndexList.Contains(number))
                {
                    bool isExistAllocationSubProductionOrder = IsExisted(parm);

                    if (isExistAllocationSubProductionOrder)
                    {
                        MessageHelper.ShowErr(this, $"serial number is{number}Master order number and cutting unit and sewing unit already exist！");
                        return;
                    }
                }
            }

            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                string main_production_order = item.Cells["主订单号"].FormattedValue.ToString();
                string cutting_dept_name = item.Cells["裁剪单位"].FormattedValue.ToString();
                string sticking_dept_name = item.Cells["针车单位"].FormattedValue.ToString();
                string Org = item.Cells["工厂"].FormattedValue.ToString();
                int number = int.Parse(item.Cells["序号"].FormattedValue.ToString());
                if (!IsExistProdLinesbyOrg(Org, cutting_dept_name, sticking_dept_name))  //Added by Ashok on 2025/04/21
                {
                    MessageHelper.ShowErr(this, $@"Cutting or Stitching line at {number} does not belongs to {Org}");
                    return;
                }
                Dictionary<string, object> parm = new Dictionary<string, object>();
                parm.Add("main_production_order", main_production_order);
                parm.Add("cutting_dept_name", cutting_dept_name);
                parm.Add("sticking_dept_name", sticking_dept_name);

                string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "InsertAllocationQuery", Program.client.UserToken, JsonConvert.SerializeObject(parm));

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
                MessageHelper.ShowSuccess(this, $"successfully saved{totalCount}Records！");
                rowIndexList.Clear();
                BindData();
            }
        }


        #region Added by Ashok on 2025/04/21
        private bool IsExistProdLinesbyOrg(string Org,string cutting_dept_name,string sticking_dept_name)
        {   DataTable dt = new DataTable();
            Dictionary<string, object> parm = new Dictionary<string, object>();
            parm.Add("Org", Org);
            parm.Add("cutting_dept_name", cutting_dept_name);
            parm.Add("sticking_dept_name", sticking_dept_name);
            string isExistedRet = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTLAPI", "KZ_RTLAPI.Controllers.CuttingLabelServer", "IsExistProdLinesbyOrg", Program.client.UserToken, JsonConvert.SerializeObject(parm));
            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(isExistedRet);
            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                string json = retJson["RetData"].ToString();
                bool isExisted = bool.Parse(json);
                return isExisted;
            }
            else
            {
                MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
                return false;
            }
        }
        #endregion


        private bool IsExisted(Dictionary<string, object> parm)
        {
            string isExistedRet = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "IsExistAllocationSubProductionOrder", Program.client.UserToken, JsonConvert.SerializeObject(parm));
            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(isExistedRet);
            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                string json = retJson["RetData"].ToString();
                bool isExisted = bool.Parse(json);
                return isExisted;
            }
            else
            {
                MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
                return false;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtMainOrderNo.Text = "";
            comboBoxFactory.SelectedText = "";
            txtWorkCenter.Text = "";
            dataGridView1.DataSource = null;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DataTable dt = dataGridView1.DataSource as DataTable;
            if (dt != null)
            {
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Selected = false;
                DataRow row = dt.NewRow();
                int number = int.Parse(dt.Rows[dt.Rows.Count - 1]["NO"].ToString()) + 1;
                row["NO"] = number.ToString();
                row["main_production_order"] = dt.Rows[dt.Rows.Count - 1]["main_production_order"].ToString();
                row["org_name"] = dt.Rows[dt.Rows.Count - 1]["org_name"].ToString();
                row["qty"] = dt.Rows[dt.Rows.Count - 1]["qty"].ToString();
                dt.Rows.InsertAt(row, dataGridView1.Rows.Count);

                dataGridView1.DataSource = dt;
                rowIndexList.Add(number);
                //The next two lines set the cursor position。
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Selected = true;
                dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0];
            }
            else
            {
                MessageHelper.ShowErr(this, "Add failed！You must have a query record and select it before you can add it");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    string id = dataGridView1.CurrentRow.Cells["Id"].Value.ToString();
                    string main_production_order = dataGridView1.CurrentRow.Cells["主订单号"].Value.ToString();
                    if (string.IsNullOrEmpty(id))
                    {
                        DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                        dataGridView1.Rows.Remove(selectedRow);
                        return;
                    }
                    Dictionary<string, object> parm = new Dictionary<string, object>();
                    parm.Add("id", id);
                    parm.Add("main_production_order", main_production_order);

                    string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "DeleteAllocation", Program.client.UserToken, JsonConvert.SerializeObject(parm));
                    var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
                    if (Convert.ToBoolean(retJson["IsSuccess"]))
                    {
                        string json = retJson["RetData"].ToString();
                        bool isDeleted = bool.Parse(json);
                        if (isDeleted)
                        {
                            MessageHelper.ShowSuccess(this, "successfully deleted！");
                            //No need to rebind data
                            //dataGridView1.Rows.Remove(dataGridView1.CurrentRow); //This is to delete the row where the cursor is located, but does not delete the database.
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

        /// <summary>
        ///     Used for events that occur when a cell is edited
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox tb = e.Control as TextBox;
            DataGridView dgv = (DataGridView)sender;

            if (tb != null)
            {
                tb.AutoCompleteCustomSource = null;
                tb.AutoCompleteSource = AutoCompleteSource.CustomSource;
                tb.AutoCompleteMode = AutoCompleteMode.Suggest;

                List<string> strList = new List<string>();

                //get the displayed control
                var tbec = e.Control as DataGridViewTextBoxEditingControl;
                if (tbec != null)
                {
                    //Check the corresponding column
                    if (dgv.CurrentCell.OwningColumn.Name == "工厂")
                    {
                        //DataTable dtJson= GetOrg();
                        //if (dtJson.Rows.Count > 0)
                        //{
                        //    foreach (DataRow row in dtJson.Rows)
                        //    {
                        //        strList.Add(row["org_name"].ToString());
                        //    }

                        //    //整体赋值给AutoCompleteCustomSource 
                        //    tb.AutoCompleteCustomSource.AddRange(strList.ToArray());
                        //}
                        //else
                        //{
                        //    MessageHelper.ShowErr(this, "自动提示出错,没有数据");
                        //}
                    }
                    else
                    {
                        //S:针车、C：裁剪
                        Dictionary<string, object> parm = new Dictionary<string, object>();

                        if (dgv.CurrentCell.OwningColumn.Name == "裁剪单位")
                        {
                            parm.Add("udf01", "C");
                        }

                        if (dgv.CurrentCell.OwningColumn.Name == "针车单位")
                        {
                            parm.Add("udf01", "S");
                        }
                        string MaProdOrd = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[dgv.Columns["主订单号"].Index].Value?.ToString();
                        //if (MaProdOrd != null)
                        //{
                        //    parm.Add("MaProdOrd", MaProdOrd);
                        //}
                        string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "GetAllDeptsByUDF01", Program.client.UserToken, JsonConvert.SerializeObject(parm));
                        var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
                        if (Convert.ToBoolean(retJson["IsSuccess"]))
                        {
                            string json = retJson["RetData"].ToString();
                            DataTable dtJson = JsonConvert.DeserializeObject<DataTable>(json);

                            if (dtJson != null && dtJson.Rows.Count > 0)
                            {
                                foreach (DataRow row in dtJson.Rows)
                                {
                                    strList.Add(row["Department_Name"].ToString());
                                }

                                //The overall assignment to AutoCompleteCustomSource 
                                tb.AutoCompleteCustomSource.AddRange(strList.ToArray());
                            }
                            else
                            {
                                MessageHelper.ShowErr(this, "automatic error message,no data");
                            }
                        }
                        else
                        {
                            MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
                        }
                    }
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

            #region Old Code 

            //int width = panel1.Width;
            //int height = panel1.Height;

            //Point startpoint = new Point(0, 0);
            //Point endpoint = new Point(width, height);


            ////// Create the LinearGradientBrush with dynamic dimensions
            ////using (LinearGradientBrush lgb = new LinearGradientBrush(startpoint, endpoint, Color.FromArgb(255, 255, 0, 0), Color.FromArgb(255, 255, 255, 0)))
            ////{
            ////    // Fill the panel with the gradient
            ////    e.Graphics.FillRectangle(lgb, 0, 0, width, height);
            ////}


            //Color color1 = ColorTranslator.FromHtml("#EEAECA");
            //Color color2 = ColorTranslator.FromHtml("#1dfd64");
            //Color color3 = ColorTranslator.FromHtml("#3a8ab4");

            //using (LinearGradientBrush lBrush = new LinearGradientBrush(startpoint, endpoint, color1, color2))
            //{
            //    // Define a ColorBlend to handle multiple colors
            //    ColorBlend colorBlend = new ColorBlend();

            //    // Set the positions where the colors should be blended (0 to 1)
            //    colorBlend.Positions = new float[] { 0.0f, 0.5f, 1.0f };

            //    // Set the colors that should appear at the corresponding positions
            //    colorBlend.Colors = new Color[] { color1, color2, color3 };

            //    // Apply the ColorBlend to the brush
            //    lBrush.InterpolationColors = colorBlend;

            //    e.Graphics.FillRectangle(lBrush, 0, 0, width, height);
            //}

            #endregion

            #region

            int width = panel1.Width;
            int height = panel1.Height;

            Rectangle rect = new Rectangle(0, 0, width, height);

            // Gradient direction: top-left to bottom-right
            Point startpoint = new Point(rect.Left, rect.Top);
            Point endpoint = new Point(rect.Right, rect.Bottom);

            Color color1 = ColorTranslator.FromHtml("#EEAECA");
            Color color2 = ColorTranslator.FromHtml("#C2EEAE");
            Color color3 = ColorTranslator.FromHtml("#3a8ab4");

            using (LinearGradientBrush lBrush = new LinearGradientBrush(startpoint, endpoint, color1, color2))
            {
                ColorBlend colorBlend = new ColorBlend
                {
                    Positions = new float[] { 0.0f, 0.33f, 0.80f, 1.0f },
                    Colors = new Color[] { color1, color2, color3, color1 } // Optional: Loop back to color1 for full wrap 
                };

                lBrush.InterpolationColors = colorBlend;

                e.Graphics.FillRectangle(lBrush, rect);
            }

            #endregion


        }

        private void txtMainOrderNo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}