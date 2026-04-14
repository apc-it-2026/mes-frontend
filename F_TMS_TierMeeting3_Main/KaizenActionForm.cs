using MaterialSkin.Controls;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_TMS_TierMeeting3_Main
{
    public partial class KaizenActionForm : MaterialForm
    {
        private int tempColIndex = 0;
        private int tempRowIndex = 0;
        private Rectangle rectangle;
        string date = "";
        string txtAll = "All";
        string dept = "";
        string plant = "";
        string txtNothingToSave = "Nothing to save";
        string txtNothingToUpgrade = "Nothing to upgrade";
        string txtNothingToClose = "Nothing to close";
        string txtCreatedDateNotNull = "Created date cannot be null";
        int type = 0;
        public KaizenActionForm(string date, string plant)
        {
            this.date = date;
            this.plant = plant;
            InitializeComponent();
        }
        private void GetData()
        {
            GetTypeAndDept();
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("from", dtpFrom.Text);
            p.Add("to", dtpTo.Text);
            p.Add("type", type);
            p.Add("dept", dept);
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "GetKaizenAction",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                gridData.DataSource = dtJson;
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    ReadOnlyCell(i, true);
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void KaizenAction_Load(object sender, EventArgs e)
        {
            InitUI();
            InitPlantCombobox();

        }

        private void InitUI()
        {
            int height = base.Height;
            gridData.ColumnHeadersHeight = Convert.ToInt32(height / 25);
            gridData.RowTemplate.Height = Convert.ToInt32(height / 25);
            gridData.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", (float)height / 60f, FontStyle.Regular, GraphicsUnit.Point, 134);
            gridData.DefaultCellStyle.Font = new Font("宋体", (float)height / 55f, FontStyle.Regular, GraphicsUnit.Point, 134);
            this.WindowState = FormWindowState.Maximized;
            dtpFrom.Size = new Size(173, 35);
            dtpFrom.Font = new Font("宋体", 18f, FontStyle.Regular, GraphicsUnit.Point, 134);
            dtpFrom.Format = DateTimePickerFormat.Custom;
            dtpFrom.CustomFormat = "yyyy/MM/dd";
            string temp = date.ToDate().Year.ToString() + "/" + date.ToDate().Month.ToString() + "/" + "01"; ;
            dtpFrom.Text = temp;
            dtpTo.Size = new Size(173, 35);
            dtpTo.Font = new Font("宋体", 18f, FontStyle.Regular, GraphicsUnit.Point, 134);
            dtpTo.Format = DateTimePickerFormat.Custom;
            dtpTo.CustomFormat = "yyyy/MM/dd";
            dtpTo.Text = date;
            dateTimePicker2.Size = new Size(173, 35);
            dateTimePicker2.Font = new Font("宋体", 18f, FontStyle.Regular, GraphicsUnit.Point, 134);
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "yyyy/MM/dd";
            dateTimePicker2.Visible = false;
            dateTimePicker2.CloseUp += new EventHandler(dtp_TextChange);
            gridData.Rows[0].Height = Convert.ToInt32(height / 25);
            cbxLine.Size = new Size(173, 35);
            cbxPlant.Size = new Size(173, 35);
            cbxSection.Size = new Size(173, 35);
        }
        private void dtp_TextChange(object sender, EventArgs e)
        {
            gridData.Rows[tempRowIndex].Cells[tempColIndex].Value = dateTimePicker2.Text.ToString();
            dateTimePicker2.Visible = false;
        }

        private void gridData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = this.gridData.CurrentRow.Index;
            if (e.ColumnIndex < 0) return;
            if (e.RowIndex < 0) return;
            bool flag = index > -1 && this.gridData.Rows[index].Cells["G_ITIME"].ReadOnly == false;
            if (flag)
            {
                var colName = gridData.Columns[e.ColumnIndex].Name;
                    if (colName == "G_GREATEDDATE" //created date 
                        || colName == "G_PLANDATE" //plan date
                        || colName == "G_FINISHDATE") //finish date
                    {
                        rectangle = gridData.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true); //  
                        dateTimePicker2.Size = new Size(rectangle.Width, rectangle.Height);
                        dateTimePicker2.Location = new Point(rectangle.X + 5, rectangle.Y + 135);
                        dateTimePicker2.Visible = true;
                        tempColIndex = e.ColumnIndex;
                        tempRowIndex = e.RowIndex;
                    }
                    else
                    {
                        dateTimePicker2.Visible = false;
                    }
            }
        }

        private void gridData_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            dateTimePicker2.Visible = false;
        }

        private void gridData_Scroll(object sender, ScrollEventArgs e)
        {
            //dtp.Visible = false;
        }

        private void gridData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0) return;
            if (e.RowIndex < 0) return;
            var colName = gridData.Columns[e.ColumnIndex].Name;
            switch (colName)
            {
                case "EDIT": //btnEdit
                    ReadOnlyCell(e.RowIndex, false);
                    break;
                case "SAVE": //btnSave
                    if (gridData.Rows[e.RowIndex].Cells["G_ITIME"].ReadOnly == false)
                    {
                        SaveAction(sender, e);
                        //ReadOnlyCell(e.RowIndex, true);
                    }
                    else
                    {
                        MessageHelper.ShowWarning(this, txtNothingToSave);
                        return;
                    }
                    break;
                case "UPGRADE2": //btnUpgrade
                    UpdateStatus(sender, e, "N");
                    break;
                case "CLOSE": //btnClose
                    UpdateStatus(sender, e, "C");
                    break;
                default:
                    break;
            }
        }
        private void SaveAction(object sender, DataGridViewCellEventArgs e)
        {
            GetTypeAndDept();
            int id = gridData.Rows[e.RowIndex].Cells["ID"].Value.ToInt();

            string item = gridData.Rows[e.RowIndex].Cells["G_ITIME"].Value == null ? "" : gridData.Rows[e.RowIndex].Cells["G_ITIME"].Value.ToString();
            if (item == "")
            {
                MessageHelper.ShowWarning(this, txtNothingToSave);
                return;
            }
            string department = gridData.Rows[e.RowIndex].Cells["G_DEPTCODE"].Value == null ? "" : gridData.Rows[e.RowIndex].Cells["G_DEPTCODE"].Value.ToString().ToUpper();
            if (department == "")
            {
                MessageHelper.ShowWarning(this, txtNothingToSave);
                return;
            }
            string finder = gridData.Rows[e.RowIndex].Cells["G_FINDER"].Value == null ? "" : gridData.Rows[e.RowIndex].Cells["G_FINDER"].Value.ToString();
            string problem = gridData.Rows[e.RowIndex].Cells["G_PROBLEMPOINT"].Value == null ? "" : gridData.Rows[e.RowIndex].Cells["G_PROBLEMPOINT"].Value.ToString();
            string measure = gridData.Rows[e.RowIndex].Cells["G_MEASURE"].Value == null ? "" : gridData.Rows[e.RowIndex].Cells["G_MEASURE"].Value.ToString();
            string principal = gridData.Rows[e.RowIndex].Cells["G_PRINCTIPAL"].Value == null ? "" : gridData.Rows[e.RowIndex].Cells["G_PRINCTIPAL"].Value.ToString();
            string remark = gridData.Rows[e.RowIndex].Cells["G_REMARK"].Value == null ? "" : gridData.Rows[e.RowIndex].Cells["G_REMARK"].Value.ToString();
            if (gridData.Rows[e.RowIndex].Cells["G_GREATEDDATE"].Value == null)
            {
                MessageHelper.ShowWarning(this, txtCreatedDateNotNull);
                return;
            }
            string createdDate = gridData.Rows[e.RowIndex].Cells["G_GREATEDDATE"].Value.ToString();
            if (gridData.Rows[e.RowIndex].Cells["G_PLANDATE"].Value == null) gridData.Rows[e.RowIndex].Cells["G_PLANDATE"].Value = "";
            string planDate = gridData.Rows[e.RowIndex].Cells["G_PLANDATE"].Value.ToString();
            if (gridData.Rows[e.RowIndex].Cells["G_FINISHDATE"].Value == null) gridData.Rows[e.RowIndex].Cells["G_FINISHDATE"].Value = "";
            string finishDate = gridData.Rows[e.RowIndex].Cells["G_FINISHDATE"].Value.ToString();
            Dictionary<string, Object> p = new Dictionary<string, object>(); p.Add("id", id);
            p.Add("item", item);
            p.Add("department", department);
            p.Add("finder", finder);
            p.Add("problem", problem);
            p.Add("measure", measure);
            p.Add("principal", principal);
            p.Add("remark", remark);
            p.Add("createdDate", createdDate);
            p.Add("planDate", planDate);
            p.Add("finishDate", finishDate);
            if (id != 0) //edit
            {
                string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "EditKaizenAction",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    GetData();
                }
                else
                {
                    MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }
            else
            { //create
                switch (type)
                {
                    case 0:
                        p.Add("T1", "");
                        p.Add("T2", "");
                        p.Add("T3", "");
                        p.Add("T4", "P");
                        break;
                    case 1:
                        p.Add("T1", "");
                        p.Add("T2", "");
                        p.Add("T3", "P");
                        p.Add("T4", "");
                        break;
                    case 2:
                        p.Add("T1", "");
                        p.Add("T2", "P");
                        p.Add("T3", "");
                        p.Add("T4", "");
                        break;
                    case 3:
                        p.Add("T1", "P");
                        p.Add("T2", "");
                        p.Add("T3", "");
                        p.Add("T4", "");
                        break;
                    default:
                        break;
                }
                string ret =
                    SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                        Program.client.APIURL,
                        "TierMeeting",
                        "TierMeeting.Controllers.TierMeetingServer",
                                            "CreateKaizenAction",
                        Program.client.UserToken,
                        JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    GetData();
                }
                else
                {
                    MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }
        }
        private void UpdateStatus(object sender, DataGridViewCellEventArgs e, string action)
        {
            int id = gridData.Rows[e.RowIndex].Cells["ID"].Value.ToInt();
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("id", id);
            if (action == "C")
            {
                if (id == 0)
                {
                    MessageHelper.ShowWarning(this, txtNothingToClose);
                    return;
                }
                string T1 = gridData.Rows[e.RowIndex].Cells["G_T1"].Value.ToString();
                string T2 = gridData.Rows[e.RowIndex].Cells["G_T2"].Value.ToString();
                string T3 = gridData.Rows[e.RowIndex].Cells["G_T3"].Value.ToString();
                string T4 = gridData.Rows[e.RowIndex].Cells["G_T4"].Value.ToString();
                if (T1 == "C" || T2 == "C" || T3 == "C" || T4 == "C")
                {
                    //closed
                    MessageHelper.ShowWarning(this, txtNothingToClose);
                    return;
                }
                T1 = T1 != "" ? "C" : "";
                T2 = T2 != "" ? "C" : "";
                T3 = T3 != "" ? "C" : "";
                T4 = T4 != "" ? "C" : "";
                p.Add("T1", T1);
                p.Add("T2", T2);
                p.Add("T3", T3);
                p.Add("T4", T4);
            }
            else if (action == "N")
            {
                if (id == 0)
                {
                    MessageHelper.ShowWarning(this, txtNothingToUpgrade);
                    return;
                }
                string T1 = gridData.Rows[e.RowIndex].Cells["G_T1"].Value.ToString();
                string T2 = gridData.Rows[e.RowIndex].Cells["G_T2"].Value.ToString();
                string T3 = gridData.Rows[e.RowIndex].Cells["G_T3"].Value.ToString();
                string T4 = gridData.Rows[e.RowIndex].Cells["G_T4"].Value.ToString();
                if (T4 == "P") {
                    //Tier 4
                    MessageHelper.ShowWarning(this, txtNothingToUpgrade);
                    return;
                }
                if (T1 == "C" || T2 == "C" || T3 == "C" || T4 == "C")
                {
                    //closed
                    MessageHelper.ShowWarning(this, txtNothingToUpgrade);
                    return;
                }
                else
                {
                    if (T1 == "P" || T1 == "N")
                    {
                        T1 = "N";
                    }
                    else {
                        T1 = "";
                    }
                    if (T2 == "P" || T2 == "N")
                    {
                        T2 = "N";
                    }
                    else
                    {
                        T2 = "";
                    }
                    if (T3 == "P" || T3 == "N")
                    {
                        T3 = "N";
                    }
                    else
                    {
                        T3 = "";
                    }
                    string[] arr = new string[4];
                    arr[0] = T1;
                    arr[1] = T2;
                    arr[2] = T3;
                    arr[3] = T4;
                    int i = 2;
                    do
                    {
                        if (arr[i] == "N")
                        {
                            arr[i + 1] = "P";
                            break;
                        }
                        i--;
                    }
                    while (i >= 0);
                    T1 = arr[0];
                    T2 = arr[1];
                    T3 = arr[2];
                    T4 = arr[3];
                    //for (var i = 2; i >= 0; i--) {
                    //    if (arr[i] == "N") {
                    //        arr[i + 1] = "P";
                    //        break;
                    //    }
                    //}


                    //if (T1 == "N" && T2 == "")
                    //{
                    //    T2 = "P";
                    //}
                    //if (T2 == "N" && T3 == "")
                    //{
                    //    T3 = "P";
                    //}
                    //if (T3 == "N" && T4 == "")
                    //{
                    //    T4 = "P";
                    //}
                    p.Add("T1", T1);
                    p.Add("T2", T2);
                    p.Add("T3", T3);
                    p.Add("T4", T4);
                }
            }
            string ret =
                    SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                        Program.client.APIURL,
                        "TierMeeting",
                        "TierMeeting.Controllers.TierMeetingServer",
                                            "UpdateStatus",
                        Program.client.UserToken,
                        JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {

                GetData();
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void ReadOnlyCell(int rowIndex, bool flag)
        {
            gridData.Rows[rowIndex].Cells[5].ReadOnly = flag; //item
            gridData.Rows[rowIndex].Cells[6].ReadOnly = flag; //department
            gridData.Rows[rowIndex].Cells[8].ReadOnly = flag; //finder
            gridData.Rows[rowIndex].Cells[9].ReadOnly = flag; //problem point
            gridData.Rows[rowIndex].Cells[10].ReadOnly = flag; //measure
            gridData.Rows[rowIndex].Cells[11].ReadOnly = flag; //principal
            gridData.Rows[rowIndex].Cells[14].ReadOnly = flag; //remark
            if (!flag)
            {
                gridData.Rows[rowIndex].DefaultCellStyle.BackColor = Color.Aquamarine;
            }
            else
            {
                gridData.Rows[rowIndex].DefaultCellStyle.BackColor = Color.White;
            }
        }
        private void btnQuery_Click(object sender, EventArgs e)
        {
            GetData();
        }
        private void InitPlantCombobox()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("parent", "is null");
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "GetDepartmentList",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                cbxPlant.Items.Add(txtAll);
                for (var i = 0; i < dtJson.Rows.Count; i++)
                {
                    cbxPlant.Items.Add(dtJson.Rows[i][0]);
                }
                EnableSectionCombobox(false);
                EnableLineCombobox(false);
                cbxPlant.Text = plant;
                GetData();
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void EnableSectionCombobox(bool temp)
        {
            cbxSection.Enabled = temp;
        }
        private void EnableLineCombobox(bool temp)
        {
            cbxLine.Enabled = temp;
        }
        private void FilterSection(string plant)
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("parent", "= '" + plant + "'");
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "GetDepartmentList",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                cbxSection.Items.Clear();
                cbxSection.Items.Add(txtAll);
                for (var i = 0; i < dtJson.Rows.Count; i++)
                {
                    cbxSection.Items.Add(dtJson.Rows[i][0]);
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void FilterLine(string section)
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("parent", "= '" + section + "'");
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "GetDepartmentList",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                cbxLine.Items.Clear();
                cbxLine.Items.Add(txtAll);
                for (var i = 0; i < dtJson.Rows.Count; i++)
                {
                    cbxLine.Items.Add(dtJson.Rows[i][0]);
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void cbxPlant_TextChanged(object sender, EventArgs e)
        {
            if (cbxPlant.Text == txtAll)
            {
                EnableSectionCombobox(false);
                EnableLineCombobox(false);
                cbxSection.Text = txtAll;
                cbxLine.Text = txtAll;
            }
            else
            {
                FilterSection(cbxPlant.Text);
                EnableSectionCombobox(true);
                cbxSection.Text = txtAll;
            }
        }
        private void cbxSection_TextChanged(object sender, EventArgs e)
        {
            if (cbxSection.Text == txtAll)
            {
                EnableLineCombobox(false);
                cbxLine.Text = txtAll;
            }
            else
            {
                FilterLine(cbxSection.Text);
                EnableLineCombobox(true);
                cbxLine.Text = txtAll;
            }
        }
        private void GetTypeAndDept()
        {
            type = 0;
            if (cbxPlant.Text != txtAll)
            {
                if (cbxSection.Text != txtAll)
                {
                    if (cbxLine.Text != txtAll)
                    {
                        type = 3; //get by line
                        dept = cbxLine.Text;
                    }
                    else
                    {
                        type = 2; //get by section
                        dept = cbxSection.Text;
                    }
                }
                else
                {
                    type = 1; //get by plant
                    dept = cbxPlant.Text;
                }
            }
        }

        private void gridData_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            GetTypeAndDept();
            e.Row.Cells["G_GREATEDDATE"].Value = DateTime.Now.ToString("yyyy/MM/dd");
            e.Row.Cells["G_DEPTCODE"].Value = dept;
        }

        private void gridData_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            return;
        }
    }
}
