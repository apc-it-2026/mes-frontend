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
    public partial class TrackIn_AssemblyOrder_InOutDetail : MaterialForm
    {
        string d_dept = "";

        public TrackIn_AssemblyOrder_InOutDetail()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        public TrackIn_AssemblyOrder_InOutDetail(string dept)
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            d_dept = dept;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            d_dept = text_dept.Text;
            string se_id = textSeId.Text.Trim();
            string po = textPo.Text.Trim();

            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DataSource = null;

            GetInOutDetail(d_dept, se_id, po);
            //dataGridView1.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dataGridView_RowPostPaint);
        }

        private void TrackIn_AssemblyOrder_InOutDetail_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            text_dept.Text = d_dept;
            textPo.Focus();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            this.dataGridView1.DataError += delegate (object obj, DataGridViewDataErrorEventArgs eve) { };
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GetInOutDetail(string d_dept, string se_id, string po)
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vDDept", d_dept);
            p.Add("vSeId", se_id);
            p.Add("vPo", po);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.GeneralServer", "GetInOutDetail", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dt.Rows.Count > 0)
                {
                    dataGridView1.Columns["ColPlanQty"].DefaultCellStyle.Format = "0";
                    dataGridView1.Columns["ColInQty"].DefaultCellStyle.Format = "0";
                    dataGridView1.Columns["ColOutQty"].DefaultCellStyle.Format = "0";
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("查无此数据！", Program.Client, "", Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
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
            Rectangle headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }
    }
}
