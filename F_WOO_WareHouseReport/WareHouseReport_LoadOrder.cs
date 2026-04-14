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

namespace F_WOO_WareHouseReport
{
    public partial class WareHouseReport_LoadOrder : MaterialForm
    {
        public WareHouseReport_LoadOrder()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);
        }
        public delegate void DataChangeHandler(object sender, OrderDaChangeEventArgs args);
        public event DataChangeHandler DataChange;
        public WareHouseReport_LoadOrder(string ART)
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);
            textBox9.Text = ART;
            GetOrder(ART);
            DataTable dtJson = GetOrder(ART);
            if (dtJson == null || dtJson.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "This ART has no master ticket");
                return;
            }
            for (int i = 0; i < dtJson.Rows.Count; i++)
            {
                CheckBox processCB = new CheckBox();
                processCB.Text = dtJson.Rows[i]["order_no"].ToString() + "—" + dtJson.Rows[i]["sales_order"].ToString();
                processCB.AutoSize = true;
                processCB.Font = new Font("微软雅黑", 13F);
                processCB.Margin = new Padding(20, 2, 0, 0);
                this.flowLayoutPanel1.Controls.Add(processCB);
            }
        }
        public DataTable GetOrder(string ART)
        {
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("vART", ART);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_WOO_WareHouseReportServer", "QyeryOrder", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                return dtJson;

            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                return null;
            }
        }
        public void OnDataChange(object sender, OrderDaChangeEventArgs args)
        {
            DataChange?.Invoke(this, args);
        }
        
        private void button11_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.flowLayoutPanel1.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox ck = c as CheckBox;
                    ck.Checked = true;
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.flowLayoutPanel1.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox ck = c as CheckBox;
                    ck.Checked = false;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable partsDt = new DataTable();
            DataColumn dc1 = new DataColumn("order_no", Type.GetType("System.String"));
            partsDt.Columns.Add(dc1);
            foreach (Control c in this.flowLayoutPanel1.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox ck = c as CheckBox;
                    if (ck.Checked)
                    {
                        DataRow dr = partsDt.NewRow();
                        dr["order_no"] = ck.Text.ToString();
                        partsDt.Rows.Add(dr);
                    }
                }
            }
            if (partsDt.Rows.Count <= 0)
            {
                MessageBox.Show("Please select a master ticket！");
                return;
            }

            //F_WOO_WareHouseReportForm frm = new F_WOO_WareHouseReportForm(partsDt);
            //frm.ShowDialog(); 
            DataChange?.Invoke(this,  new  OrderDaChangeEventArgs(partsDt));
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            string ART = textBox9.Text;
            GetOrder(ART);
            DataTable dtJson = GetOrder(ART);
            if (dtJson == null || dtJson.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "This ART has no master ticket");
                return;
            }
            for (int i = 0; i < dtJson.Rows.Count; i++)
            {
                CheckBox processCB = new CheckBox();
                processCB.Text = dtJson.Rows[i]["order_no"].ToString() + "—" + dtJson.Rows[i]["sales_order"].ToString();
                processCB.AutoSize = true;
                processCB.Font = new Font("微软雅黑", 13F);
                processCB.Margin = new Padding(20, 2, 0, 0);
                this.flowLayoutPanel1.Controls.Add(processCB);
            }
        }
    }

    public class OrderDaChangeEventArgs
    {
          public  DataTable dt { get; set; }

       public OrderDaChangeEventArgs(DataTable dt) {
            this.dt = dt;
        }
    }
}
