using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;

namespace InvestmentReturnOrder
{
    public delegate void GetArtNoDelegate(string artNo);

    public partial class InvestmentReturnOrder_QueryArtNo : MaterialForm
    {

        public event GetArtNoDelegate GetArtNoEvent;


        public InvestmentReturnOrder_QueryArtNo(Color backColor)
        {
            InitializeComponent();

            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);

            btnQuery.BackColor = backColor;
            dgvArtNo.ColumnHeadersDefaultCellStyle.BackColor = backColor;
        }

        private void dgvArtNo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //artNo = dgvArtNo.SelectedRows[0].Cells["PROD_NO"].ToString();
            if (GetArtNoEvent != null)
            {
                if (dgvArtNo.RowCount > 0)
                {
                    GetArtNoEvent(dgvArtNo.Rows[dgvArtNo.CurrentRow.Index].Cells[0].Value.ToString());
                }
                else
                {
                    GetArtNoEvent(tbArtNo.Text);
                }
            }

            this.Close();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("ArtNo", tbArtNo.Text.Trim());
            p.Add("shoeName", tbShoeName.Text.Trim());
            p.Add("mold_no", tbMoldNo.Text.Trim());

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "GetArtNo", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                dgvArtNo.DataSource = dt.DefaultView;
                dgvArtNo.Update();
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void QueryArtNoForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (GetArtNoEvent != null)
            {
                if (dgvArtNo.RowCount > 0)
                {
                    GetArtNoEvent(dgvArtNo.Rows[dgvArtNo.CurrentRow.Index].Cells[0].Value.ToString());
                }
                else
                {
                    GetArtNoEvent(tbArtNo.Text);
                }
            }
        }
    }
}
