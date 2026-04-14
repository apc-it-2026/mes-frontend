using MaterialSkin.Controls;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkingHoursStandard
{
    public partial class WorkingHoursStandard_Add : MaterialForm
    {
        public WorkingHoursStandard_Add()
        {
            InitializeComponent();

            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);

        }
        public WorkingHoursStandard_Add(string art)
        {
            InitializeComponent();
            art_no.Text = art;
        }
        public void GetData()
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("ART", ART.Text);
            string ret = WebAPIHelper.Post(Program.client.APIURL,
                "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.WorkingHoursStandardServer", "GetDataOne", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                for (int i = 0; i < dtJson.Rows.Count;)
                {
                    SHOE_NAME.Text = dtJson.Rows[0]["NAME_T"].ToString();
                    mold_no.Text = dtJson.Rows[0]["mold_no"].ToString();
                    break;
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

            Font a = new Font("宋体", 13);
            dataGridView1.Font = a;//字体
            //dataGridView1.DataSource = null;
            //dataGridView1.AutoGenerateColumns = false;//只获取自己需要的列


            string ret1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL,
                "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.WorkingHoursStandardServer", "GetDataTwo",
            Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dtJson.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dtJson;
                    dataGridView1.Rows[0].Selected = false;
                    //dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);//排序
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

        private void AddFrom_Load(object sender, EventArgs e)
        {
            GetAllData();
            if (string.IsNullOrEmpty(ART.Text))
            {
                art_no.Visible = false;
                btnYES.Visible = false;
                ART.Visible = true;
                btnOK.Visible = true;
                groupBox1.Visible = false;
                groupBox3.Location = new Point(43, 23);

            }
            if (!string.IsNullOrEmpty(art_no.Text))
            {
                ART.Visible = false;
                btnOK.Visible = false;
                art_no.Visible = true;
                btnYES.Visible = true;
                groupBox1.Visible = true;
                groupBox3.Location = new Point(33, 0);

            }
        }
        public void GetAllData()
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("ART", art_no.Text);
            string ret = WebAPIHelper.Post(Program.client.APIURL,
                "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.WorkingHoursStandardServer", "GetAllDataOne", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                for (int i = 0; i < dtJson.Rows.Count;)
                {
                    SHOE_NAME.Text = dtJson.Rows[0]["mold_no"].ToString(); 
                    mold_no.Text = dtJson.Rows[0]["NAME_T"].ToString();
                    label11.Text = dtJson.Rows[0]["CREATEBY"].ToString();
                    label9.Text = dtJson.Rows[0]["CREATETIME"].ToString();
                    label10.Text = dtJson.Rows[0]["MODIFYBY"].ToString();
                    label8.Text = dtJson.Rows[0]["MODIFYTIME"].ToString();
                    break;
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

            Font a = new Font("宋体", 13);
            dataGridView1.Font = a;//font
            string ret1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL,
                "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.WorkingHoursStandardServer", "GetAllDataTwo",
            Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dtJson.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dtJson;
                    dataGridView1.Rows[0].Selected = false;
                }
                else
                {

                }
            }
            else
            {
                MessageBox.Show("No such data");
            }
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)//automatic sorting
        {

            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,e.RowBounds.Location.Y,dataGridView1.RowHeadersWidth - 4,e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics,(e.RowIndex + 1).ToString(), dataGridView1.RowHeadersDefaultCellStyle.Font, rectangle,dataGridView1.RowHeadersDefaultCellStyle.ForeColor,TextFormatFlags.VerticalCenter | TextFormatFlags.Right);

        }

        private void ART_MouseLeave_1(object sender, EventArgs e)
        {
            GetData();
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ART.Text))
            {
                DataTable Dt = GetDgvToTable(dataGridView1);
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("ART", ART.Text);
                p.Add("SHOE_NAME", SHOE_NAME.Text);
                p.Add("mold_no", mold_no.Text);
                p.Add("data", Dt);
                string ret = WebAPIHelper.Post(Program.client.APIURL,
                    "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.WorkingHoursStandardServer", "SaveData"
                    , Program.client.UserToken, JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    MessageBox.Show("Successfully saved！！！");
                    this.Close();
                }
                else
                {
                    MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }
            else
            {
                MessageBox.Show("Please enter ART");
            }
            
        }
        private DataTable GetDgvToTable(DataGridView dgv)
        {
            DataTable dt = new DataTable();

            for (int count = 0; count < dgv.Columns.Count; count++)
            {
                DataColumn dc = new DataColumn(dgv.Columns[count].Name);
                dt.Columns.Add(dc);
            }

            for (int count = 0; count < dgv.Rows.Count; count++)
            {
                DataRow dr = dt.NewRow();
                if (!string.IsNullOrEmpty(Convert.ToString(dgv.Rows[count].Cells[0].Value)))
                {
                    continue;
                }
                for (int countsub = 0; countsub < dgv.Columns.Count; countsub++)
                {
                    dr[countsub] = Convert.ToString(dgv.Rows[count].Cells[countsub].Value);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        private void btnYES_Click(object sender, EventArgs e)
        {
            DataTable Dt = GetDgvToTable(dataGridView1);
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("ART", art_no.Text);
            p.Add("SHOE_NAME", SHOE_NAME.Text);
            p.Add("mold_no", mold_no.Text);
            p.Add("data", Dt);
            string ret = WebAPIHelper.Post(Program.client.APIURL,
                "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.WorkingHoursStandardServer", "SaveNewData"
                , Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                MessageBox.Show("Successfully saved！！！");
                this.Close();
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
    }
}
