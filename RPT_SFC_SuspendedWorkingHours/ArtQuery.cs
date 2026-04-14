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
    public partial class ArtQuery : MaterialForm
    {
        public ArtQuery()
        {
            InitializeComponent();
        }

        

        private void Query_Click(object sender, EventArgs e)
        {
            string art = this.ART.Text.ToString();
            string name_t = this.Name_T.Text.ToString();
            string mold_no = this.mold_no.Text.ToString();
         Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("prod_no", art);
            p.Add("name_t", name_t);
            p.Add("mold_no", mold_no);
            DataTable dataTable = new DataTable();
            try
            {
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.SuspendedWorkingHoursServer", "GetArt", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
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
        public delegate void DataChangeHandler(object sender, DataChangeEventArgs args);
        //define a delegated event
        public event DataChangeHandler DataChange;


        /// If the delegated event (datachange) is not empty, the delegated event (invoke()) is called
        public void OnDataChange(object sender, DataChangeEventArgs args)
        {
            //If DataChange is not empty, call the Invoke() method
            DataChange?.Invoke(this, args);
        }

        /// Define the parameter types required by a delegate
        /// </summary>
        public class DataChangeEventArgs : EventArgs
        {
            public string prod_no { get; set; }
            public string name_t { get; set; }
            public string mold_no { get; set; }

            public DataChangeEventArgs(string s1, string s2,string s3)
            {
                prod_no = s1;
                name_t = s2;
                mold_no = s3;
            }
        }


        //echo the queried data
        private void ReturnArt(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            if (i > -1)
            {
                //echo data
                string prod_no = dgv_art.Rows[i].Cells[0].Value.ToString();
                string name_t = dgv_art.Rows[i].Cells[1].Value.ToString();
                string mold_no = dgv_art.Rows[i].Cells[2].Value.ToString();

                OnDataChange(this, new DataChangeEventArgs(prod_no, name_t, mold_no));

                this.Close();
            }
        }






    }
}
