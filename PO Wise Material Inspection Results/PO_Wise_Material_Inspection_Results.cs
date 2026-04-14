using MaterialSkin.Controls;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PO_Wise_Material_Inspection_Results
{
    public partial class PO_Wise_Material_Inspection_Results : MaterialForm
    {
        string Salesorder = string.Empty;
        string PO_Number = string.Empty;
        public PO_Wise_Material_Inspection_Results(string SO,string PO)
        {
            InitializeComponent();
            Salesorder = SO;
            PO_Number = PO;
            txt_PO.Text = PO;
            txt_SO.Text = SO;
        }

        private void PO_Wise_Material_Inspection_Results_Load(object sender, EventArgs e)
        {
            GetPO_Wise_Material_Inspection_Results(Salesorder);
        }

        public void GetPO_Wise_Material_Inspection_Results(string Salesorder)
        {
            Dictionary<string, object> Data = new Dictionary<string, object>();
            Data.Add("Salesorder", Salesorder);
            Cursor.Current = Cursors.WaitCursor;
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_RTDMAPI",
                     "KZ_RTDMAPI.Controllers.PO_Wise_MaterialresultServer",
                     "GetPO_Wise_Material_Inspection_Results", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(Data));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

            if (ret.IsSuccess)
            {
                DataTable dtJson1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                if (dtJson1.Rows.Count > 0)
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

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
