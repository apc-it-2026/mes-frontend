using F_EPM_Maintennace_Plan.bean;
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

namespace F_EPM_Maintennace_Plan
{

    public delegate void onSelectedProjectResult(List<MaintenanceType> maintenanceTypes);

    public partial class SelectMaintenanceProject : MaterialForm
    {

        public event onSelectedProjectResult mOnSelectedProjectResult;

        public List<MaintenanceType> maintenanceTypes;
        public string planId;
        public string deviceNo;
        public bool isAddType;

        public SelectMaintenanceProject()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void SelectMaintenanceProject_Load(object sender, EventArgs e)
        {
             QueryMaintenanceTypeList();
        }


        //todo delete
        private void getMaintennaceTypeList()
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("plan_id", planId);
            p.Add("device_no", deviceNo);
            p.Add("like", textBox1.Text);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MaintenancePlanServer", "QueryUnableMaintenanceTypeList", Program.Client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json);
                dataGridView1.Rows.Clear();
                if (dtJson.Rows.Count == 0)
                {
                    MessageBox.Show("No Data！");
                }
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add(1);

                    dataGridView1.Rows[i].Cells["by_no"].Value = dtJson.Rows[i]["by_no"].ToString();
                    dataGridView1.Rows[i].Cells["part_body"].Value = dtJson.Rows[i]["body_part"].ToString();
                    dataGridView1.Rows[i].Cells["item"].Value = dtJson.Rows[i]["item"].ToString();
                    dataGridView1.Rows[i].Cells["standard"].Value = dtJson.Rows[i]["standard"].ToString();
                    dataGridView1.Rows[i].Cells["level"].Value = dtJson.Rows[i]["level_name"].ToString();

                    foreach (MaintenanceType maintenanceType in maintenanceTypes) {
                        if (maintenanceType.byNo == dtJson.Rows[i]["by_no"].ToString()) {
                            dataGridView1.Rows[i].Cells["cl_cb"].Value = maintenanceType.subIsCheck;
                        }
                    }
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void QueryMaintenanceTypeList()
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("device_no", deviceNo);
            p.Add("like", textBox1.Text);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MaintenanceAddPlanServer", "QueryMaintenanceTypeList", Program.Client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json);
                dataGridView1.Rows.Clear();
                if (dtJson.Rows.Count == 0)
                {
                    MessageBox.Show("No Data！");
                }
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add(1);

                    dataGridView1.Rows[i].Cells["by_no"].Value = dtJson.Rows[i]["by_no"].ToString();
                    dataGridView1.Rows[i].Cells["part_body"].Value = dtJson.Rows[i]["body_part"].ToString();
                    dataGridView1.Rows[i].Cells["item"].Value = dtJson.Rows[i]["item"].ToString();
                    dataGridView1.Rows[i].Cells["standard"].Value = dtJson.Rows[i]["standard"].ToString();
                    dataGridView1.Rows[i].Cells["level"].Value = dtJson.Rows[i]["level_name"].ToString();

                    foreach (MaintenanceType maintenanceType in maintenanceTypes)
                    {
                        if (maintenanceType.byNo == dtJson.Rows[i]["by_no"].ToString())
                        {
                            dataGridView1.Rows[i].Cells["cl_cb"].Value = maintenanceType.subIsCheck;
                        }
                    }
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            addOrRomoveSelectItemIntoList();
            if (mOnSelectedProjectResult!=null) {
                mOnSelectedProjectResult(maintenanceTypes);
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
          QueryMaintenanceTypeList();
        }


        public void addOrRomoveSelectItemIntoList()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewCell o = dataGridView1.Rows[i].Cells["cl_cb"];
                string byNo= dataGridView1.Rows[i].Cells["by_no"].Value.ToString();
                int index = hasByNoIntoMaintenanceTypeList(byNo);
                if (o != null && o.Value != null)
                {
                    bool result = bool.Parse(o.Value.ToString());
                    if (result && index == -1) {
                        MaintenanceType maintenanceType = new MaintenanceType();
                        maintenanceType.subIsCheck = true;
                        maintenanceType.byNo = dataGridView1.Rows[i].Cells["by_no"].Value.ToString();
                        maintenanceType.bodyPart = dataGridView1.Rows[i].Cells["part_body"].Value.ToString();
                        maintenanceType.item = dataGridView1.Rows[i].Cells["item"].Value.ToString();
                        maintenanceType.standard = dataGridView1.Rows[i].Cells["standard"].Value.ToString();
                        maintenanceType.levelName = dataGridView1.Rows[i].Cells["level"].Value.ToString();
                        maintenanceTypes.Add(maintenanceType);
                    } else if (!result && index>-1 && maintenanceTypes[index].enable!="Y") {
                        maintenanceTypes.RemoveAt(index);
                    }
                }
               
            }

        }

        private int hasByNoIntoMaintenanceTypeList(string byNo) {
            for (int i=0;i<maintenanceTypes.Count;i++) {
                MaintenanceType maintenanceType = maintenanceTypes[i];
                if (maintenanceType.byNo == byNo) {
                    return i;
                }
            }
            return -1;
        }

     

        private void button4_Click(object sender, EventArgs e)
        {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Cells["cl_cb"].Value = true;
                }
        }

        private void button3_Click(object sender, EventArgs e)
        {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Cells["cl_cb"].Value = false;
                }
        }
    }
}
