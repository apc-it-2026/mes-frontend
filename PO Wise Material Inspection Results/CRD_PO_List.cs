using MaterialSkin.Controls;
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

namespace PO_Wise_Material_Inspection_Results
{
    public partial class CRD_PO_List : MaterialForm
    {
        public CRD_PO_List()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Get_CRDWise_PO();
        }

        public void Get_CRDWise_PO()
        {
            Dictionary<string, object> Data = new Dictionary<string, object>();
            Data.Add("FromDate", dateTimePicker1.Text);
            Data.Add("ToDate", dateTimePicker2.Text);
            Data.Add("PO", textBox1.Text);
            Cursor.Current = Cursors.WaitCursor;
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_RTDMAPI",
                     "KZ_RTDMAPI.Controllers.PO_Wise_MaterialresultServer",
                     "Get_CRDWise_PO", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(Data));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

            if (ret.IsSuccess)
            {
                //Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                DataTable dtJson1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);

                if (dtJson1!=null && dtJson1.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dtJson1;
                }
                else
                {
                    dataGridView1.DataSource = null;
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found");
                }
            }
            else
            {
                dataGridView1.DataSource = null;
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found");

            }
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    return;
                }
                if (e.ColumnIndex > -1 && e.RowIndex > -1)
                {
                    switch (dataGridView1.Columns[e.ColumnIndex].Name)
                    {
                        case "view_result":
                            string SO= dataGridView1.CurrentRow.Cells["SO"].Value.ToString();
                            string PO= dataGridView1.CurrentRow.Cells["PO"].Value.ToString();
                            using (PO_Wise_Material_Inspection_Results aa = new PO_Wise_Material_Inspection_Results(SO,PO))
                            {
                                aa.ShowDialog();
                            }
                            break;
                    }


                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
    }
}
