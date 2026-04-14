using AutocompleteMenuNS;
using CCWin.SkinControl;
using GDSJ_Framework.DBHelper;
using MaterialSkin.Controls;
using MiniExcelLibs;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using SJeMES_Control_Library.Controls;
using SJeMES_Control_Library.Forms;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace WorkingIEAchievementInput
{
    public partial class IERateForm : MaterialForm
    {
        public IERateForm()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);
            dataGridView2.AutoGenerateColumns = false;
        }
        #region 全局变量
        DataTable dtdept;
        DataTable dtArt;
        DataTable dtProcess;
        DataTable dtOrg;
        #endregion
        #region 公共初始化数据
        private void GetAllRoutNo()
        {
            List<ValueText> processlist = new List<ValueText>();
            //制程
            string ret6 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.ProductionDashBoardServer", "LoadRoutNo", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject("noParam"));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret6)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret6)["RetData"].ToString();
                dtProcess = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                dtProcess.Rows.Add("", "");
                for (int i = 1; i <= dtProcess.Rows.Count; i++)
                {
                    processlist.Add(new ValueText() { code = dtProcess.Rows[i - 1]["rout_no"].ToString(), name = dtProcess.Rows[i - 1]["rout_no"].ToString()+"|" + dtProcess.Rows[i - 1]["rout_name_z"].ToString() });
                }
            }
            this.scomProcess.DataSource = processlist;
            this.scomProcess.ValueMember = "code";
            this.scomProcess.DisplayMember = "name";

            List<ValueText> orglist = new List<ValueText>();
            //工厂
            var items1 = new List<AutocompleteItem>();
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WMSAPI", "KZ_WMSAPI.Controllers.F_WMS_Miscellaneous_Server", "LoadOrg", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject("noParam"));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {

                string Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dtOrg = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(Data);
                dtOrg.Rows.Add("ZAPE", "全厂");
                dtOrg.Rows.Add("", "");
                for (int i = 0; i < dtOrg.Rows.Count; i++)
                {
                    orglist.Add(new ValueText() { code = dtOrg.Rows[i]["org_code"].ToString(), name = dtOrg.Rows[i]["org_code"].ToString() + "|" + dtOrg.Rows[i]["org_name"].ToString() });

                }
                this.scomOrg.DataSource = orglist;
                this.scomOrg.ValueMember = "code";
                this.scomOrg.DisplayMember = "name";

            }
        }
        private void LoadArtInfo()
        {
            List<string> stringList = new List<string>();
            AutoCompleteStringCollection autoComplete1 = new AutoCompleteStringCollection();


            Dictionary<string, object> parm = new Dictionary<string, object>();
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.IE_AchievementServer", "getAllArt", Program.client.UserToken, JsonConvert.SerializeObject(parm));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dtArt = JsonConvert.DeserializeObject<DataTable>(json);               
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void LoadDept()
        {
            List<string> stringList = new List<string>();
            AutoCompleteStringCollection autoComplete1 = new AutoCompleteStringCollection();


            Dictionary<string, object> parm = new Dictionary<string, object>();
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "GetAllDepts", Program.client.UserToken, JsonConvert.SerializeObject(parm));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dtdept = JsonConvert.DeserializeObject<DataTable>(json);
                if (dtdept != null && dtdept.Rows.Count > 0)
                {
                    foreach (DataRow item in dtdept.Rows)
                    {
                        stringList.Add(item["department_code"].ToString() + "|" + item["department_name"]);
                    }
                }              
                autoComplete1.AddRange(stringList.ToArray());
                txtDeptNo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtDeptNo.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtDeptNo.AutoCompleteCustomSource = autoComplete1;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            List<ValueText> typelist = new List<ValueText>();
            typelist.Add(new ValueText() { code = "", name = "" });
            typelist.Add(new ValueText() { code = "NARTIE", name = "新ART" });
            typelist.Add(new ValueText() { code = "NWCIE", name = "新工作中心" });
            List<ValueText> comlist = new List<ValueText>();
            comlist.Add(new ValueText() { code = "", name = "" });
            comlist.Add(new ValueText() { code = "NARTIE", name = "新ART" });
            comlist.Add(new ValueText() { code = "NWCIE", name = "新工作中心" });
            this.Coltype.DataSource = typelist;
            this.Coltype.ValueMember = "code";
            this.Coltype.DisplayMember = "name";
            this.scomtype.DataSource = typelist;
            this.scomtype.ValueMember = "code";
            this.scomtype.DisplayMember = "name";
            this.comboBox1.DataSource = comlist;
            this.comboBox1.ValueMember = "code";
            this.comboBox1.DisplayMember = "name";
        }
        private void IERateForm_Load(object sender, EventArgs e)
        {
            LoadDept();
            LoadArtInfo();
            GetAllRoutNo();
            utbMth.Text = DateTime.Now.ToString("yyyyMM");

        }
        #endregion

        public void Updategridview1(DataTable dt)
        {
            dataGridView1.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataGridView1.Rows.Add(1);
                dataGridView1.Rows[i].Cells["ColNO"].Value = dt.Rows[i]["rownum"];
                dataGridView1.Rows[i].Cells["ColMth"].Value = dt.Rows[i]["Mth"];
                dataGridView1.Rows[i].Cells["ColIEID"].Value = dt.Rows[i]["IEID"];
                dataGridView1.Rows[i].Cells["Coltype"].Value = dt.Rows[i]["I_TYPE"].ToString();
                dataGridView1.Rows[i].Cells["Colartno"].Value = dt.Rows[i]["art_no"];
                dataGridView1.Rows[i].Cells["Colorg"].Value = dt.Rows[i]["org_id"];
                dataGridView1.Rows[i].Cells["Coldeptno"].Value = dt.Rows[i]["dept_no"];
                dataGridView1.Rows[i].Cells["CollastDate"].Value = dt.Rows[i]["last_date"];
                dataGridView1.Rows[i].Cells["CollastUser"].Value = dt.Rows[i]["last_user"];
                dataGridView1.Rows[i].Cells["Colprocess"].Value = dt.Rows[i]["Process_no"];
                dataGridView1.Rows[i].Cells["ColIErate"].Value = dt.Rows[i]["IE_rate"];
                dataGridView1.Rows[i].Cells["ColinsertUser"].Value = dt.Rows[i]["insert_user"];
                dataGridView1.Rows[i].Cells["ColinsertDate"].Value = dt.Rows[i]["insert_date"];
            }
        }
        public DataTable GetIERatebyManual()
        {
            DataTable dt = new DataTable();
            try
            {
                Dictionary<string, object> parm = new Dictionary<string, object>();
                parm.Add("VartNo", txtART.Text);
                parm.Add("VdeptNo", txtDeptNo.Text.Split('|')[0]);
                parm.Add("Vtype", comboBox1.SelectedValue);
                string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.IE_AchievementServer", "getIERatebyManual", Program.client.UserToken, JsonConvert.SerializeObject(parm));
                if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    dt = JsonConvert.DeserializeObject<DataTable>(json);

                }
                else
                {
                    MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowErr(this, ex.Message.ToString());
            }
            return dt;
        }

        private void scomtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            //scomOrg.SelectedValue = "";
            //scomProcess.SelectedValue = "";
            if (scomtype.SelectedValue.ToString() == "NARTIE")
            {
                lbArt.Visible = true;
                utbArt.Visible = true;
                lbdept.Visible = false;
                utbdeptno.Visible = false;
                scomOrg.Enabled = true;
                scomProcess.Enabled = true;

            }
            if (scomtype.SelectedValue.ToString() == "NWCIE")
            {
                lbArt.Visible = false;
                utbArt.Visible = false;
                utbdeptno.Visible = true;
                lbdept.Visible = true;
                scomOrg.Enabled = false;
                scomProcess.Enabled = false;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            utbId.Text = dataGridView1.Rows[rowIndex].Cells["ColIEid"].Value.ToString();
            utbNo.Text = dataGridView1.Rows[rowIndex].Cells["ColNo"].Value.ToString();
            utbMth.Text = dataGridView1.Rows[rowIndex].Cells["ColMth"].Value.ToString();
            utbArt.Text = dataGridView1.Rows[rowIndex].Cells["ColartNo"].Value.ToString();
            utbdeptno.Text = dataGridView1.Rows[rowIndex].Cells["ColdeptNo"].Value.ToString();
            scomProcess.SelectedValue = dataGridView1.Rows[rowIndex].Cells["Colprocess"].Value.ToString();
            scomOrg.SelectedValue = dataGridView1.Rows[rowIndex].Cells["Colorg"].Value.ToString();
            scomtype.SelectedValue = dataGridView1.Rows[rowIndex].Cells["Coltype"].Value.ToString();
            utbrate.InputText = dataGridView1.Rows[rowIndex].Cells["ColIErate"].Value.ToString();
        }

 

        private void utbArt_DoubleClick(object sender, EventArgs e)
        {
            string sql = $@"select  PROD_NO 代号 from bdm_rd_prod group by PROD_NO";

            FrmSelectData frmData = new FrmSelectData(sql, true, Program.client);
            frmData.ShowDialog();
            if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
            {
                utbArt.Text = frmData.RetData.Rows[0]["代号"].ToString();
            }
          
        }

        private void utbdeptno_DoubleClick(object sender, EventArgs e)
        {
            string sql = $@"select DEPARTMENT_CODE 代号,DEPARTMENT_NAME 名称,FACTORY_SAP 工厂,UDF01 制程 from base005m";

            FrmSelectData frmData = new FrmSelectData(sql, true, Program.client);
            frmData.ShowDialog();
            if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
            {
                utbdeptno.Text = frmData.RetData.Rows[0]["代号"].ToString();
                scomOrg.SelectedValue = frmData.RetData.Rows[0]["工厂"].ToString();
                scomProcess.SelectedValue= frmData.RetData.Rows[0]["制程"].ToString();
            }
        }


        public bool updateIERatebyManual(Dictionary<string, object> parm)
        {
            bool Is_true = false;
            try
            {
                string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.IE_AchievementServer", "updateIERatebyManual", Program.client.UserToken, JsonConvert.SerializeObject(parm));
                if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    MessageHelper.ShowSuccess(this, "保存成功！");
                    //skinButton5_Click(new object(), new EventArgs());
                    Is_true = true;
                }
                else
                {
                    MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowErr(this, ex.Message.ToString());
            }
            return Is_true;
        }

  
        public DataTable GetIErateDay(Dictionary<string,object> p)
        {
            DataTable dt = new DataTable();
            try
            {
                string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.IE_AchievementServer", "getIErateDay", Program.client.UserToken, JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    dt = JsonConvert.DeserializeObject<DataTable>(json);
                    if(dt.Rows.Count ==0)
                    {
                        MessageBox.Show("查无数据");
                    }
                }
                else
                {
                    MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowErr(this, ex.Message.ToString());
            }
            return dt;
        }
        Thread thread = null;
        int val = 0;
        public void Go()
        {
            //while (true)
            //{
            ShowProcess(val, 100, "计算中，请耐心等待···");
            if (checkBox2.Checked)
                {
                    string d1 = DateTime.Now.AddDays(-int.Parse(textBox3.Text)).ToShortDateString();
                    string d2 = DateTime.Now.AddDays(-1).ToShortDateString();
                    string sql = $@"SELECT to_char(WORK_DAY,'yyyy/mm/dd') as WORK_DAY FROM T_IE_ACHIEVEMENT_DAY where SCAN_HOUR=0 and WORK_DAY between to_date('{d1}','yyyy/mm/dd') and to_date('{d2}','yyyy/mm/dd') group by WORK_DAY order by WORK_DAY";
                    DataTable dt = Program.client.GetDT(sql);
                if(dt.Rows.Count > 0)
                {
                    int i = 0;
                    int count = dt.Rows.Count;
                    foreach (DataRow dr in dt.Rows)
                    {
                        i++;
                        int val = i * 100 / count;
                        //ShowProcess(a,0);
                        Dictionary<string, object> p = new Dictionary<string, object>();
                        p.Add("work_day", dr[0]);
                        updateIERateDay(p);
                        //ShowProcess(a, 1);
                        //val = a;
                        ShowProcess(val, 100, "计算中，请耐心等待···");
                    } 
                }
                }
                else
                {
                    for (int i = 1; i <= int.Parse(textBox3.Text); i++)
                    {
                        int val = i * 100 / int.Parse(textBox3.Text);
                        //ShowProcess(a, 0);
                        // this.Invoke(new ShowProgressDelegate(ShowProgress), new object[] { a, 0 });
                        Dictionary<string, object> p = new Dictionary<string, object>();
                        p.Add("work_day", DateTime.Now.AddDays(-i).ToShortDateString());
                        updateIERateDay(p);
                    
                    ShowProcess(val, 100, "计算中，请耐心等待···");
                    }                   
                }
                ShowProcess(100, 100, "计算中，请耐心等待···");
                thread.Abort();
               

            //}
        
        }
       
        private void ShowProcess(int i, int total,string str)
        {

            //定义委托
            Action t = () =>
            {
                if (total == i)
                {
                    MessageHelper.ShowSuccess(this, "计算完成···");
                    button3.Enabled = true;
                    str = "计算完成···";
                    //thread.Abort();
                }
                ucProcessWave1.Value = i;

                label14.Text = str;
               
            };

            //跨线程操作
            this.BeginInvoke(t);
            //Thread.Sleep(100);//【注意】:触发事件后的方法要执行完毕，才会退出while中的触发事件代码
        }
    
        public void updateIERateDay(Dictionary<string, object> p)
        {
            string ret1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI", " KZ_SFCAPI.Controllers.IE_AchievementServer", "updateIErateDAY", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
            {
                //MessageHelper.ShowSuccess(this,"计算成功！");
            }
            else
            {
                string msg = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["ErrMsg"].ToString();
                if(msg != "无数据滚动")
                {
                    MessageHelper.ShowErr(this, msg);
                }


            }
        }

      
     

        private void IERateForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (thread != null)
            {
                thread.Abort();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView4.DataSource = null;
            richTextBox1.Text = "";
            dataGridView1.Visible = true;
            dataGridView4.Visible = false;
            DataTable dt = GetIERatebyManual();
            //dataGridView1.DataSource = dt;
            if (dt.Rows.Count <= 0)
            {
                MessageHelper.ShowErr(this, "查无数据！");
            }
            Updategridview1(dt);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtART.Text = "";
            txtDeptNo.Text = "";
            dataGridView1.Rows.Clear();
            button1_Click(sender, e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("Vstartdate", DateTime.Now.AddDays(-int.Parse(textBox3.Text)).ToShortDateString());
            p.Add("Venddate", DateTime.Now.AddDays(-1).ToShortDateString());
            p.Add("Visabnormal", checkBox1.Checked);
            dt = GetIErateDay(p);
            dataGridView3.DataSource = dt;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //label14.Text = "计算中，请耐心等待···";
            try
            {
                button3.Enabled = false;
                ucProcessWave1.Visible = true;
                thread = new Thread(new ThreadStart(Go));
                thread.Start();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            label9.Text = "计算中···";
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("work_Mth", DateTime.Now.ToShortDateString());
            p.Add("Dqty", textBox1.Text.Trim());
            string ret1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI", " KZ_SFCAPI.Controllers.IE_AchievementServer", "updateIErateMth", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["RetData"].ToString();
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(json);
                dataGridView2.DataSource = dt;
                MessageHelper.ShowSuccess(this, "计算完成···");
            }
            else
            {
                string msg = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["ErrMsg"].ToString();
                MessageHelper.ShowErr(this, msg);
            }
            label9.Text = "";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string d1 = DateTime.Now.AddDays(-int.Parse(textBox3.Text)).ToShortDateString();
            string d2 = DateTime.Now.AddDays(-1).ToShortDateString();
            string sql = $@"select*from( SELECT  to_char(WORK_DAY,'yyyy/mm/dd') 日期,DEPT_NO 部门,SCAN_HOUR 总工时 FROM T_IE_ACHIEVEMENT_DAY where (SCAN_HOUR=0 or SCAN_HOUR is null) and WORK_DAY between to_date('{d1}','yyyy/mm/dd') and to_date('{d2}','yyyy/mm/dd') group by WORK_DAY,DEPT_NO,SCAN_HOUR) order by 日期";
            DataTable dt = Program.client.GetDT(sql);
            BadDeptForm deptForm = new BadDeptForm(dt);
            deptForm.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string d1 = DateTime.Now.AddDays(-int.Parse(textBox3.Text)).ToShortDateString();
            string d2 = DateTime.Now.AddDays(-1).ToShortDateString();
            string sql = $@"select PRODUCTION_ORDER as 工单 ,process_no as 制程,art_no from  
                           T_IE_ACHIEVEMENT_DAY where (THT=0 or THT is null) and WORK_DAY between to_date('{d1}','yyyy/mm/dd') and to_date('{d2}','yyyy/mm/dd') group by PRODUCTION_ORDER,process_no,art_no";
            DataTable dt = Program.client.GetDT(sql);
            BadProFprm badPro = new BadProFprm(dt);
            badPro.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string vorg = scomOrg.SelectedValue.ToString();
            string vprocess = scomProcess.SelectedValue.ToString();
            string vrate = utbrate.InputText;
            string vtype = scomtype.SelectedValue.ToString();
            string vart = utbArt.Text;
            string vdept_no = utbdeptno.Text;
            string vieid = utbId.Text;
            if (string.IsNullOrEmpty(vrate))
            {
                MessageBox.Show("IE达成率不能为空");
                return;
            }
            if (string.IsNullOrEmpty(vorg) || string.IsNullOrEmpty(vprocess) || string.IsNullOrEmpty(vtype))
            {
                MessageBox.Show("工厂|制程|类型不能为空");
                return;
            }
            if (vtype == "0" && string.IsNullOrEmpty(vart))
            {
                MessageBox.Show("Art不能为空");
                return;
            }
            if (vtype == "1" && string.IsNullOrEmpty(vdept_no))
            {
                MessageBox.Show("部门不能为空");
                return;
            }

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("VIEID", vieid);
            param.Add("Vmth", utbMth.Text);
            param.Add("Vorg", vorg);
            param.Add("Vprocess", vprocess);
            param.Add("Vrate", vrate);
            param.Add("Vart", vart);
            param.Add("Vdept_no", vdept_no);
            param.Add("Vtype", vtype);

            if (updateIERatebyManual(param))
            {
                DataTable dt = GetIERatebyManual();
                Updategridview1(dt);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            utbId.Text = string.Empty;
            utbNo.Text = string.Empty;
            utbMth.Text = DateTime.Now.ToString("yyyyMM"); ;
            utbArt.Text = string.Empty;
            utbdeptno.Text = string.Empty;
            scomProcess.SelectedValue = "";
            scomOrg.SelectedValue = "";
            scomtype.Text = string.Empty;
            utbrate.InputText = string.Empty;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string path = Application.StartupPath + @"\导入模板" + "\\IE达成率导入模板.xlsx";
            string saveFileName = "";
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xlsx";
            saveDialog.Filter = "Excel文件|*.xlsx";
            saveDialog.FileName = "IE达成率导入模板.xlsx";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                saveFileName = saveDialog.FileName;
                File.Copy(path, saveFileName, true);
                System.Diagnostics.Process.Start(saveFileName);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView4.Visible = true;
                dataGridView1.Visible = false;
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "(excel文件)|*.xls;*.xlsx";
                //DialogResult dialogResult = openFileDialog.ShowDialog();
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string file = openFileDialog.FileName;
                    richTextBox1.Text = file;
                    var rows = MiniExcel.Query<importDto>(file).ToList();

                    List<DataDTO> dts = new List<DataDTO>();
                    int i = 1;
                    foreach (var item in rows)
                    {
                        DataDTO dataDTO = new DataDTO();
                        dataDTO.id = i;
                        dataDTO.I_type = string.IsNullOrEmpty(item.类型) ? "" : item.类型;
                        dataDTO.art_no = string.IsNullOrEmpty(item.ART) ? "" : item.ART;
                        dataDTO.dept_no = string.IsNullOrEmpty(item.工作中心) ? "" : item.工作中心;
                        dataDTO.org_id = string.IsNullOrEmpty(item.工厂) ? "" : item.工厂;
                        dataDTO.process_no = string.IsNullOrEmpty(item.制程) ? "" : item.制程;
                        dataDTO.IE_rate = item.IE达成率;
                        dts.Add(dataDTO);
                        i++;
                    }
                    dataGridView4.DataSource = dts;

                    MessageBox.Show("已选择文件:" + file, "选择文件提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView4.Rows.Count <= 0)
                {
                    MessageHelper.ShowErr(this,"请先选择数据再导入。");
                    return;
                }
                string err_msg = "";
                foreach (DataGridViewRow row in dataGridView4.Rows)
                {
                    string id = row.Cells["id"].Value.ToString();
                    string I_TYPE = row.Cells["I_type"].Value.ToString();
                    string dept_no = row.Cells["dept_no"].Value.ToString();
                    string art_no = row.Cells["art_no"].Value.ToString();
                    string org_id = row.Cells["org_id"].Value.ToString();
                    string process_no = row.Cells["process_no"].Value.ToString();
                    if (I_TYPE != "NWCIE" && I_TYPE != "NARTIE")
                    {
                        err_msg += $@"第{id}行数据类型有误，请检查！";
                    }
                    else
                    {
                        if (org_id == "")
                        {
                            err_msg += $@"第{id}行工厂不能为空，请检查！";
                            continue;
                        }
                        DataRow[] dataRows1 = dtOrg.Select($@"org_code ='{org_id}'");
                        if (dataRows1.Length <= 0)
                        {
                            err_msg += $@"第{id}行工作中心不存在，请检查！";
                            continue;
                        }
                        //
                        if (process_no == "")
                        {
                            err_msg += $@"第{id}行制程不能为空，请检查！";
                            continue;
                        }
                        DataRow[] dataRows2 = dtProcess.Select($@"rout_no ='{process_no}'");
                        if (dataRows2.Length <= 0)
                        {
                            err_msg += $@"第{id}行制程不存在，请检查！";
                            continue;
                        }

                        if (I_TYPE == "NWCIE")
                        {
                            if (dept_no == "")
                            {
                                err_msg += $@"第{id}行工作中心不能为空，请检查！";
                                continue;
                            }
                            DataRow[] dataRows = dtdept.Select($@"department_code ='{dept_no}'");
                            if (dataRows.Length <= 0)
                            {
                                err_msg += $@"第{id}行工作中心不存在，请检查！";
                                continue;
                            }
                        }
                        if (I_TYPE == "NARTIE")
                        {
                            if (art_no == "")
                            {
                                err_msg += $@"第{id}行工作中心不能为空，请检查！";
                                continue;
                            }
                            DataRow[] dataRows = dtArt.Select($@"PROD_NO ='{art_no}'");
                            if (dataRows.Length <= 0)
                            {
                                err_msg += $@"第{id}行ART不存在，请检查！";
                                continue;
                            }
                        }

                    }

                }
                if (err_msg != "")
                {
                    MessageBox.Show(err_msg);
                    return;
                }
                DialogResult dialogResult = MessageBox.Show("是否导入？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    List<DataDTO> dt = dataGridView4.DataSource as List<DataDTO>;
                    Dictionary<string, object> parm = new Dictionary<string, object>();
                    parm.Add("Vdata", dt);
                    string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.IE_AchievementServer", "importIERate", Program.client.UserToken, JsonConvert.SerializeObject(parm));
                    if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                    {
                        MessageHelper.ShowSuccess(this,"上传成功！");
                        dataGridView4.DataSource = null;
                        richTextBox1.Text = "";
                    }
                    else
                    {
                        MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            string sql = $@"SELECT rownum 序号,MTH 统计月份,b.CODE_NAME 类型,ORG_ID 工厂,DEPT_NO 工作中心,ART_NO,PROCESS_NO 制程,IE_RATE IE达成率,INSERT_DATE 创建时间,
INSERT_USER 创建人 FROM T_IE_ACHIEVEMENT_MANUAL a,base098m b where a.I_TYPE=b.CODE_NO and b.rule_no='2001' and a.D_TYPE=2 and a.MTH=(select max(MTH) from T_IE_ACHIEVEMENT_MANUAL )";
            DataTable dt = Program.client.GetDT(sql);
            NewExportExcels.ExportExcels.Export("月达成率导出", dt);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string sql = $@"SELECT rownum 序号,
to_char(WORK_DAY,'yyyy/mm/dd') 日期,ORG_ID 工厂,PROCESS_NO 工作中心,DEPT_NO 部门,ART_NO ART,THT,PRODUCTION_ORDER 工单,
FINISH_QTY 产量,EARN_HOUR 赚取工时,EH_SUM 合计赚取工时,EH_RATE 赚取工时占比,SCAN_HOUR 实际工时合计,
ACTUAL_HOUR 实际工时,IE_RATE IE达成率,INSERT_DATE 创建时间,INSERT_USER 创建用户
FROM T_IE_ACHIEVEMENT_DAY where WORK_DAY<=(sysdate-1) and WORK_DAY>=(sysdate-1 -{textBox3.Text})";
            DataTable dt = Program.client.GetDT(sql);
            NewExportExcels.ExportExcels.Export("日达成率导出", dt);
        }

        private void utbArt_TextChanged(object sender, EventArgs e)
        {

        }
    }
    public class ValueText
    {
        public string name { get; set; }
        public string code { get; set; }
    }
}
