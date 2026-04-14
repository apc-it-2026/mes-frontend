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

namespace RPT_SFC_SuspendedWorkingHours
{
    public partial class ArtQueryQuery : MaterialForm
    {
        public ArtQueryQuery()
        {
            InitializeComponent();
        }

        private void Query_Click(object sender, EventArgs e)
        {
            string art = this.ART.Text.ToString();
            
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("prod_no", art);
            DataTable dataTable = new DataTable();
            try
            {
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.SuspendedWorkingHoursServer", "GetQueryArt", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {

                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    dataTable = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                    dgv_art.DataSource = dataTable.DefaultView;

                    dgv_art.Update();
                }
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }

        //Define a delegate with two parameters
        // DataChangeEventArgs is an object with fields for shoe number and name
        public delegate void DataChangeHandler2(object sender, DataChangeEventArgs2 args);
        //define a delegated event
        public event DataChangeHandler2 DataChange2;


        /// If the delegated event (datachange) is not empty, the delegated event (invoke()) is called
        public void OnDataChange2(object sender, DataChangeEventArgs2 args)
        {
            //If DataChange is not empty, call the Invoke() method
            DataChange2?.Invoke(this, args);
        }

        /// Define the parameter types required by a delegate
        /// </summary>
        public class DataChangeEventArgs2 : EventArgs
        {
            public string prod_no { get; set; }
          

            public DataChangeEventArgs2(string s1)
            {
                prod_no = s1;
               
            }
        }
        private void ReturnArt(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            if (i > -1)
            {
                //echo data
                string prod_no = dgv_art.Rows[i].Cells[0].Value.ToString();
                OnDataChange2(this, new DataChangeEventArgs2(prod_no));

                this.Close();
            }
        }

       
    }
}
