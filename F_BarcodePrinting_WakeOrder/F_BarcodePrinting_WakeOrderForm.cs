using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static F_BarcodePrinting_WakeOrder.F_MMS_SelectTurn;

namespace F_BarcodePrinting_WakeOrder
{
    public partial class F_BarcodePrinting_WakeOrderForm : MaterialForm
    {
        List<string> partList = new List<string>();//part
        List<string> partList1 = new List<string>();//New interface loading widget
        List<string> partListOld = new List<string>();//Query Recorded Parts
        List<string> partList2 = new List<string>();//New interface to query records

        // public object SJeMES_Framework { get; private set; }
        // public object SJeMES_Control_Library { get; private set; }

        public F_BarcodePrinting_WakeOrderForm()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);
            tabPage1.Parent = null;
            tabPage2.Parent = null;
            tabPage3.Parent = null;
            LoadUnit(); //Load outgoing units
            LoadWakerOrder();//Load ticket number
            outgoing(); //Load outgoing unit of query print record
            LoadContractNo();//Load the combined order number
                             //  Rectangle rect = new Rectangle();
                             //  rect = Screen.GetWorkingArea(this);
                             //   this.Width = rect.Width;//屏幕宽
                             //   this.Height = rect.Height;//屏幕高
                             //   this.ControlBox = false;   // 设置不出现关闭按钮
                             //   this.FormBorderStyle = FormBorderStyle.None;//无边框

        }
        //Load outgoing units
        private void LoadUnit()
        {
            var orderSource = new AutoCompleteStringCollection();   //Store database query results
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_BarcodePrinting_WakeOrderServer", "LoadUnit", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(string.Empty));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                textBox1.Text = json;
                textBox18.Text = json;
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void LoadWakerOrder()
        {
            var orderSource = new AutoCompleteStringCollection();   //Store database query results
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_BarcodePrinting_WakeOrderServer", "LoadWakerOrder", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(string.Empty));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {
                    orderSource.Add(dtJson.Rows[i - 1]["ORDER_NO"].ToString());
                }
                //Work order number binding data source
                textBox3.AutoCompleteCustomSource = orderSource;   //bind data source
                textBox3.AutoCompleteMode = AutoCompleteMode.Suggest;    //Show related dropdown
                textBox3.AutoCompleteSource = AutoCompleteSource.CustomSource;   //set properties

                textBox14.AutoCompleteCustomSource = orderSource;   //bind data source
                textBox14.AutoCompleteMode = AutoCompleteMode.Suggest;    //Show related dropdown
                textBox14.AutoCompleteSource = AutoCompleteSource.CustomSource;   //set properties
                //The main ticket binding data source of the query function
                textBox11.AutoCompleteCustomSource = orderSource;   //bind data source
                textBox11.AutoCompleteMode = AutoCompleteMode.Suggest;    //Show related dropdown
                textBox11.AutoCompleteSource = AutoCompleteSource.CustomSource;   //Set property textBox11




            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

        }

        private void LoadContractNo()
        {
            var orderSource = new AutoCompleteStringCollection();   //Store database query results
            var orderList = new AutoCompleteStringCollection();
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_Remake_BarcodePringtingService", "LoadContractNo", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(string.Empty));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {
                    orderSource.Add(dtJson.Rows[i - 1]["se_id"].ToString());
                    orderList.Add(dtJson.Rows[i - 1]["order_no"].ToString());
                }

                textBox12.AutoCompleteCustomSource = orderSource;
                textBox12.AutoCompleteMode = AutoCompleteMode.Suggest;    //Show related dropdown
                textBox12.AutoCompleteSource = AutoCompleteSource.CustomSource;   //set properties
                textBox26.AutoCompleteCustomSource = orderSource;
                textBox26.AutoCompleteMode = AutoCompleteMode.Suggest;    //Show related dropdown
                textBox26.AutoCompleteSource = AutoCompleteSource.CustomSource;   //set properties
                textBox27.AutoCompleteCustomSource = orderSource;
                textBox27.AutoCompleteMode = AutoCompleteMode.Suggest;    //Show related dropdown
                textBox27.AutoCompleteSource = AutoCompleteSource.CustomSource;   //set properties
                textBox19.AutoCompleteCustomSource = orderList;
                textBox19.AutoCompleteMode = AutoCompleteMode.Suggest;    //Show related dropdown
                textBox19.AutoCompleteSource = AutoCompleteSource.CustomSource;   //set properties
                textBox23.AutoCompleteCustomSource = orderList;
                textBox23.AutoCompleteMode = AutoCompleteMode.Suggest;    //Show related dropdown
                textBox23.AutoCompleteSource = AutoCompleteSource.CustomSource;   //set properties
                textBox25.AutoCompleteCustomSource = orderList;
                textBox25.AutoCompleteMode = AutoCompleteMode.Suggest;    //Show related dropdown
                textBox25.AutoCompleteSource = AutoCompleteSource.CustomSource;   //set properties
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Clear all data
            textBox4.Text = null;
            //textBox1.Text = null;
            textBox2.Text = null;
            textBox5.Text = null;
            textBox10.Text = null;
            this.comboBox3.SelectedValueChanged -= new System.EventHandler(this.comboBox3_SelectedValueChanged);
            comboBox3.Text = "";
            comboBox3.Items.Clear();
            this.comboBox3.SelectedValueChanged += new System.EventHandler(this.comboBox3_SelectedValueChanged);
            dataGridView1.Columns.Clear();
            dataGridView2.DataSource = null;
            this.comboBox3.Items.Clear();//Empty parts
            //query parts 
            partList.Clear();
            string vOrder = textBox3.Text;
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("vOrder", vOrder);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_BarcodePrinting_WakeOrderServer", "QueryPart", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                partList.Add("");
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    partList.Add(dtJson.Rows[i]["udf01"].ToString().Trim() + "  " + dtJson.Rows[i]["name_t"].ToString().Trim());
                    // this.comboBox3.Items.Add(dtJson.Rows[i]["udf01"].ToString().Trim() + "  " + dtJson.Rows[i]["name_t"].ToString().Trim());
                }
                comboBox3.Items.AddRange(partList.ToArray());
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

            //Query shoe type and art
            string ret1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_BarcodePrinting_WakeOrderServer", "GetArtShoeType", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                textBox2.Text = dtJson.Rows[0][0].ToString();
                textBox4.Text = dtJson.Rows[0][1].ToString();
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        //query process
        private void QueryRouting()
        {
            //get a size

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("size_no");
            dataTable.Columns.Add("production_quantity");
            for (int i = 1; i < dataGridView1.Columns.Count; i++)
            {
                DataRow dataRow = dataTable.NewRow();
                //    dataRow
                string size = dataGridView1.Columns[i].HeaderText.ToString();
                string quantity = dataGridView1.Rows[0].Cells[i].Value == null ? "" : dataGridView1.Rows[0].Cells[i].Value.ToString();
                if (!quantity.Equals("") && !quantity.Equals("0"))
                {
                    dataRow["size_no"] = size;
                    dataRow["production_quantity"] = quantity;
                    dataTable.Rows.Add(dataRow);
                }
            }
            if (dataTable.Rows.Count < 1)
            {
                MessageBox.Show("No data to print");
                return;
            }

            string size_no = dataTable.Rows[0][0].ToString();
            dataGridView2.DataSource = null;
            string vOrder = textBox3.Text;
            string part_no = Regex.Split(comboBox3.Text, "\\s+", RegexOptions.IgnoreCase)[0];
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("vOrder", vOrder);
            p.Add("part_no", part_no);
            p.Add("size_no", size_no);
            if (!string.IsNullOrEmpty(vOrder) && !string.IsNullOrEmpty(part_no))
            {
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_BarcodePrinting_WakeOrderServer", "QueryRouting", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string RetData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    DataTable dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(RetData);
                    dataGridView2.DataSource = dt;
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please enter work order number and part!");
            }


        }




        public void DataChanged(object sender, DataChangeEventArgs args)
        {
            textBox5.Text = args.turn;
        }

        public void DataChanged2(object sender, DataChangeEventArgs args)
        {
            textBox15.Text = args.turn;
        }


        //select round
        private void textBox5_Click(object sender, EventArgs e)
        {

            string vOrder = textBox3.Text;
            string vDept = textBox1.Text;
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("vOrder", vOrder);
            p.Add("vDept", vDept);
            if (!string.IsNullOrEmpty(vOrder) && !string.IsNullOrEmpty(vDept))
            {
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_BarcodePrinting_WakeOrderServer", "GetTruns", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string RetData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    DataTable dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(RetData);
                    DataRow zero = dt.NewRow();
                    zero["TURN_NO"] = "0";
                    zero["ORDER_NO"] = dt.Rows[0][1].ToString();
                    zero["PRODUCTION_LINE"] = dt.Rows[0][2].ToString();
                    dt.Rows.Add(zero);
                    F_MMS_SelectTurn frm = new F_MMS_SelectTurn(dt);
                    frm.datachange += new F_MMS_SelectTurn.DataChangeHandler(DataChanged);
                    frm.ShowDialog();
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "This group has not scheduled this work order!");
                    return;
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please enter ticket number and group!");
                return;
            }


            //Take out quantity
            p.Add("vTurnNo", textBox5.Text);
            //Inquire about size
            string ret1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_BarcodePrinting_WakeOrderServer", "getTurn", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["RetData"].ToString();
                DataTable dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dt.Rows.Count > 0)
                {
                    dataGridView1.Columns.Clear();
                    DataTable tb1 = new DataTable();
                    tb1.Columns.Add("Size");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        tb1.Columns.Add(dt.Rows[i][0].ToString());
                    }
                    DataRow tb1_dr = tb1.NewRow();
                    tb1_dr[0] = "quantity";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        tb1_dr[i + 1] = dt.Rows[i][1].ToString();
                    }
                    tb1.Rows.Add(tb1_dr);
                    dataGridView1.DataSource = tb1;
                    dataGridView1.Update();

                }

            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["ErrMsg"].ToString());
            }
        }

        //Print
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Whether to confirm printing？", "hint", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                button1.Enabled = false;
                if (string.IsNullOrEmpty(textBox10.Text.Trim()) || int.Parse(textBox10.Text.Trim()) < 0)
                {
                    MessageBox.Show("Please fill in the number of tags greater than 0！");
                    button1.Enabled = true;
                    return;
                }
                DataTable processTrainDt = GetDgvToTable(dataGridView2);
                if (dataGridView2.Rows.Count > 0 && !string.IsNullOrEmpty(processTrainDt.Columns[3].ToString()))//
                {
                    if (dataGridView1.Rows.Count > 0)
                    {
                        //Get the value of dataGridView1 as the size and quantity of round 0
                        DataTable dataTable = new DataTable();
                        dataTable.Columns.Add("size_no");
                        dataTable.Columns.Add("production_quantity");
                        for (int i = 1; i < dataGridView1.Columns.Count; i++)
                        {
                            DataRow dataRow = dataTable.NewRow();
                            //    dataRow
                            string size = dataGridView1.Columns[i].HeaderText.ToString();
                            string quantity = dataGridView1.Rows[0].Cells[i].Value == null ? "" : dataGridView1.Rows[0].Cells[i].Value.ToString();
                            if (!quantity.Equals(""))
                            {
                                dataRow["size_no"] = size;
                                dataRow["production_quantity"] = quantity;
                                dataTable.Rows.Add(dataRow);
                            }
                        }

                        Dictionary<string, object> p = new Dictionary<string, object>();
                        p.Add("data", processTrainDt);  //craft
                        p.Add("vOrder", textBox3.Text);
                        p.Add("vArtNo", textBox2.Text);
                        p.Add("vDept", textBox1.Text);
                        // p.Add("vVendNo", Regex.Split(textBox2.Text, "\\s+", RegexOptions.IgnoreCase)[0]);
                        p.Add("vPartNo", Regex.Split(comboBox3.Text, "\\s+", RegexOptions.IgnoreCase)[0]);
                        p.Add("vPartName", Regex.Split(comboBox3.Text, "\\s+", RegexOptions.IgnoreCase)[1]);
                        p.Add("vDeliveryDate", dateTimePicker1.Value.ToString("yyyy/MM/dd"));
                        p.Add("vTurnNo", textBox5.Text);
                        p.Add("vPrintNum", textBox10.Text);
                        p.Add("vType", "1");
                        p.Add("vShoeName", textBox4.Text);
                        p.Add("vdataTable", dataTable);
                        string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_BarcodePrinting_WakeOrderServer", "SavePrintInfo", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                        if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                        {
                            int resultMsg = int.Parse(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString());
                            if (resultMsg == 10)
                            {
                                MessageBox.Show("Failed to generate QR code! The user's organization information is not queried");
                                button1.Enabled = true;
                                return;
                            }
                            else if (resultMsg == 20)
                            {
                                MessageBox.Show("Failed to generate QR code! QR code cannot be generated repeatedly");
                                button1.Enabled = true;
                                return;
                            }
                        }
                        else
                        {
                            SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                            button1.Enabled = true;
                            return;
                        }

                        Dictionary<string, Object> pintDate = new Dictionary<string, object>();
                        pintDate.Add("vOrder", textBox3.Text);
                        pintDate.Add("vDept", textBox1.Text);
                        pintDate.Add("vPartNo", Regex.Split(comboBox3.Text, "\\s+", RegexOptions.IgnoreCase)[0]);
                        pintDate.Add("vTurnNo", textBox5.Text);
                        pintDate.Add("vdataTable", dataTable);
                        string retPrint = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_BarcodePrinting_WakeOrderServer", "GetQrCodeByTurn", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(pintDate));
                        if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retPrint)["IsSuccess"]))
                        {
                            string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retPrint)["RetData"].ToString();
                            DataTable dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                            if (dt.Rows.Count <= 0)
                            {
                                SJeMES_Control_Library.MessageHelper.ShowErr(this, "no print data！");
                                button1.Enabled = true;
                                return;
                            }


                            string path = Application.StartupPath + @"\报表" + "\\中仓二维码.frx";
                            WakeOrder_QrCodePrint frm = new WakeOrder_QrCodePrint(dt, path);
                            frm.Show();
                        }
                        else
                        {
                            SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retPrint)["ErrMsg"].ToString());
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Printing is not allowed~~");
                }
                button1.Enabled = true;
            }

        }

        private void comboBox3_SelectedValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox5.Text.ToString().Trim()))
            {
                MessageBox.Show("Please select the round first");

                this.comboBox3.SelectedValueChanged -= new System.EventHandler(this.comboBox3_SelectedValueChanged);
                comboBox3.Text = "";
                this.comboBox3.SelectedValueChanged += new System.EventHandler(this.comboBox3_SelectedValueChanged);
                return;
            }
            QueryRouting();
        }
        /// <summary>
        /// Convert data in datagridview to data in datatable
        /// </summary>
        /// <param name="dgv"></param>
        /// <returns></returns>
        private DataTable GetDgvToTable(DataGridView dgv)
        {
            DataTable dt = new DataTable();

            for (int count = 0; count < dgv.Columns.Count; count++)
            {
                DataColumn dc = new DataColumn(dgv.Columns[count].Name.ToString());
                dt.Columns.Add(dc);
            }

            for (int count = 0; count < dgv.Rows.Count; count++)
            {
                DataRow dr = dt.NewRow();
                if (string.IsNullOrEmpty(Convert.ToString(dgv.Rows[count].Cells[0].Value)))
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

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.Text.Equals("0"))
            {
                dataGridView1.ReadOnly = false;
            }
            else
            {
                dataGridView1.ReadOnly = true;
            }
        }
        //Query outgoing units   Inquire the outsourcing unit
        private void outgoing()
        {
            var source = new AutoCompleteStringCollection();   //Store database query results
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("s", "C");
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_BarcodePrinting_WakeOrderServer", "OutgoingQuery", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {
                    source.Add(dtJson.Rows[i - 1]["DEPARTMENT_CODE"].ToString() + "  " + dtJson.Rows[i - 1]["DEPARTMENT_NAME"].ToString());
                }
                textBox9.AutoCompleteCustomSource = source;   //bind data source
                textBox9.AutoCompleteMode = AutoCompleteMode.Suggest;    //Show related dropdown
                textBox9.AutoCompleteSource = AutoCompleteSource.CustomSource;   //set properties

            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            this.comboBox1.Items.Clear();//Empty parts
            //query parts 
            string vOrder = textBox11.Text;
            partListOld.Clear();
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("vOrder", vOrder);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_BarcodePrinting_WakeOrderServer", "QueryPart", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                partListOld.Add("");
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    partListOld.Add(dtJson.Rows[i]["udf01"].ToString().Trim() + "  " + dtJson.Rows[i]["name_t"].ToString().Trim());
                    // this.comboBox1.Items.Add(dtJson.Rows[i]["udf01"].ToString().Trim() + "  " + dtJson.Rows[i]["name_t"].ToString().Trim());
                }
                comboBox1.Items.AddRange(partListOld.ToArray());
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

        }
        //Query print records
        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView3.DataSource = null;
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vOrder_no", textBox11.Text);
            p.Add("vDept", Regex.Split(textBox9.Text, "\\s+", RegexOptions.IgnoreCase)[0]);
            p.Add("vPartNo", Regex.Split(comboBox1.Text, "\\s+", RegexOptions.IgnoreCase)[0]);
            p.Add("vPrintTime", textBox7.Text);
            p.Add("vTurnNoBefore", textBox6.Text);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_BarcodePrinting_WakeOrderServer", "GetPrintInfoBySePart", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                dataGridView3.DataSource = dtJson;

            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }



        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            this.textBox7.Text = dateTimePicker2.Value.ToString("yyyy/MM/dd");
        }


        //Show all QR codes
        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            this.dataGridView4.DataSource = null;
            //  if (!dataGridView3.Focused)
            // {
            //  return;
            //  }
            if (dataGridView3.CurrentRow != null && dataGridView3.CurrentRow.Index > -1)
            {
                int index = dataGridView3.CurrentRow.Index;
                string vOrder = dataGridView3.Rows[index].Cells[0].Value == null ? "" : dataGridView3.Rows[index].Cells[0].Value.ToString();
                string vDept = dataGridView3.Rows[index].Cells[1].Value == null ? "" : dataGridView3.Rows[index].Cells[1].Value.ToString();
                string vPartNo = dataGridView3.Rows[index].Cells[3].Value == null ? "" : dataGridView3.Rows[index].Cells[3].Value.ToString();
                string vTurnNo = dataGridView3.Rows[index].Cells[5].Value == null ? "" : dataGridView3.Rows[index].Cells[5].Value.ToString();
                string vPointDate = dataGridView3.Rows[index].Cells[8].Value == null ? "" : dataGridView3.Rows[index].Cells[8].Value.ToString();
                Dictionary<string, Object> p = new Dictionary<string, object>();
                p.Add("vOrder", vOrder);
                p.Add("vDept", vDept);
                p.Add("vPartNo", vPartNo);
                p.Add("vTurnNo", vTurnNo);
                p.Add("vPointDate", vPointDate);
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_BarcodePrinting_WakeOrderServer", "GetPrintInfoByTurn", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                    dataGridView4.DataSource = dtJson.DefaultView;
                    dataGridView4.Rows[0].Selected = false;
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
                e.Handled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataTable print_result = null;
            //Determine whether to hit a two-dimensional code mark or a round of two-dimensional code mark
            //Print a QR code label
            if (dataGridView4.CurrentRow != null && dataGridView4.CurrentRow.Index > -1 && dataGridView4.SelectedRows.Count > 0)
            {
                int index = dataGridView4.CurrentRow.Index;
                string vQrCode = dataGridView4.Rows[index].Cells[0].Value == null ? "" : dataGridView4.Rows[index].Cells[0].Value.ToString();
                string vOrder = dataGridView4.Rows[index].Cells[1].Value == null ? "" : dataGridView4.Rows[index].Cells[1].Value.ToString();
                Dictionary<string, Object> p = new Dictionary<string, object>();
                p.Add("vQrCode", vQrCode);
                p.Add("vOrder", vOrder);
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_BarcodePrinting_WakeOrderServer", "GetQrCodeOne", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    print_result = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }

            }
            //Print a round of QR code labels
            else if (dataGridView3.CurrentRow != null && dataGridView3.CurrentRow.Index > -1 && dataGridView3.SelectedRows.Count > 0)
            {
                int index = dataGridView3.CurrentRow.Index;
                string vOrder = dataGridView3.Rows[index].Cells[0].Value == null ? "" : dataGridView3.Rows[index].Cells[0].Value.ToString();
                string vDept = dataGridView3.Rows[index].Cells[1].Value == null ? "" : dataGridView3.Rows[index].Cells[1].Value.ToString();
                string vPartNo = dataGridView3.Rows[index].Cells[3].Value == null ? "" : dataGridView3.Rows[index].Cells[3].Value.ToString();
                string vTurnNo = dataGridView3.Rows[index].Cells[5].Value == null ? "" : dataGridView3.Rows[index].Cells[5].Value.ToString();
                string vPointDate = dataGridView3.Rows[index].Cells[8].Value == null ? "" : dataGridView3.Rows[index].Cells[8].Value.ToString();
                Dictionary<string, Object> p = new Dictionary<string, object>();
                p.Add("vOrder", vOrder);
                p.Add("vDept", vDept);
                p.Add("vPartNo", vPartNo);
                p.Add("vTurnNo", vTurnNo);
                p.Add("vPointDate", vPointDate);
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_BarcodePrinting_WakeOrderServer", "GetQrCodeByTurnBefore", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    print_result = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please select what to print");
                return;
            }
            for (int i = 0; i < print_result.Rows.Count; i++)
            {
                string str = print_result.Rows[i]["order_no"].ToString();
                string str1 = print_result.Rows[i]["sales_order"].ToString();
                string temp_order = "";
                string temp_sales = "";
                string[] vs = str.Split(',');
                string[] vs1 = str1.Split(',');
                foreach (var s in vs)
                {
                    string t1 = s.Remove(0, 3);
                    //Console.WriteLine(t1);
                    int p = int.Parse(t1);
                    temp_order += p + ",";
                }
                foreach (var s in vs1)
                {
                    string t1 = s.Remove(0, 1);
                    // Console.WriteLine(t1);
                    int p = int.Parse(t1);
                    temp_sales += p + ",";
                }

                temp_order = temp_order.Remove(temp_order.Length - 1, 1);
                temp_sales = temp_sales.Remove(temp_sales.Length - 1, 1);
                print_result.Rows[i]["order_no"] = temp_order;
                print_result.Rows[i]["sales_order"] = temp_sales;
            }

            string path = Application.StartupPath + @"\报表" + "\\中仓二维码.frx";
            WakeOrder_QrCodePrint frm = new WakeOrder_QrCodePrint(print_result, path);
            frm.Show();
        }

        private void comboBox3_TextUpdate(object sender, EventArgs e)
        {
            GetInformationbycombox(comboBox3);
        }
        private void GetInformationbycombox(ComboBox combo)
        {
            string s = combo.Text;
            List<string> strList = new List<string>();   //Store raw data (can be objects, strings...)
            strList.AddRange(partListOld.ToArray());  // List<string> materials
            List<string> strListNew = new List<string>();
            //Empty combobox
            combo.Items.Clear();

            //if (combo.Text == "")
            //{
            //    combo.Items.AddRange(strList.ToArray());
            //}
            ////遍历全部原始数据
            //else
            //{
            foreach (var item in strList)
            {
                // According to the fuzzy query of the input value, the qualified value is stored in the new strListNew collection
                if (item.Contains(s))
                {
                    strListNew.Add(item);
                }
            }
            if (strListNew.Count >= 1) // Eligible content exists
            {
                //Add eligible content to the combobox
                combo.Items.AddRange(strListNew.ToArray());
            }
            else  // When no conditions exist
            {
                // The following code is to add the value entered by itself when the query does not meet the conditions
                combo.Items.Add(combo.Text);
            }
            //}
            //Set the cursor position, if not set: the cursor position is always kept in the first column, resulting in the reverse order of the input keywords
            combo.SelectionStart = combo.Text.Length;  // Set the cursor position, if not set: the cursor position is always kept in the first column, resulting in the reverse order of the input keywords
            Cursor = Cursors.Default; //保持鼠标指针原来状态，有时候鼠标指针会被下拉框覆盖，所以要进行一次设置
            combo.DroppedDown = true; // Automatic pop-up drop-down box
        }
        private void comboBox1_TextUpdate(object sender, EventArgs e)
        {
            GetInformationbycombox(comboBox1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox14.Text == "")
            {
                MessageBox.Show("Please enter the ticket number information！！！");
            }
            else
            {
                //Truncate part number
                string[] partno = (comboBox14.Text).Split();
                dataGridView5.DataSource = null;
                Dictionary<string, Object> p = new Dictionary<string, object>();
                p.Add("order_no", textBox14.Text);
                p.Add("part_no", comboBox14.Text == "" ? "" : partno[0]);
                p.Add("qr_code", textBox13.Text);
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_BarcodePrinting_WakeOrderServer", "GetLbScanningRecord", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                    dataGridView5.DataSource = dtJson;
                    if (dtJson.Rows.Count == 0)
                    {
                        MessageBox.Show("Tagless scan records！");
                    }
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }

            }
        }


        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            this.comboBox14.Items.Clear();//Empty parts
            //query parts 
            string vOrder = textBox14.Text;
            partListOld.Clear();
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("vOrder", vOrder);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_BarcodePrinting_WakeOrderServer", "QueryPart", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                partListOld.Add("");
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    partListOld.Add(dtJson.Rows[i]["udf01"].ToString().Trim() + "  " + dtJson.Rows[i]["name_t"].ToString().Trim());
                    // this.comboBox1.Items.Add(dtJson.Rows[i]["udf01"].ToString().Trim() + "  " + dtJson.Rows[i]["name_t"].ToString().Trim());
                }
                comboBox14.Items.AddRange(partListOld.ToArray());
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void comboBox14_TextUpdate(object sender, EventArgs e)
        {
            GetInformationbycombox(comboBox14);
        }
        //*************************************How to add a new tab*********************************************
        private void button6_Click(object sender, EventArgs e)
        {
            textBox8.Text = "";
            textBox17.Text = "";
            textBox15.Text = "";
            dataGridView6.DataSource = null;
            dataGridView7.DataSource = null;
            string f_order = textBox19.Text.ToString().Trim();
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("f_order", f_order);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_Remake_BarcodePringtingService", "GetPartAndArt", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dtJson.Rows.Count > 0)
                {
                    textBox12.Text = dtJson.Rows[0][0].ToString();
                    textBox8.Text = dtJson.Rows[0][1].ToString();
                    textBox17.Text = dtJson.Rows[0][2].ToString();
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Check no data！");
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                //textBox12.Text = "";
            }
            //query parts 
            comboBox2.Items.Clear();
            partList1.Clear();
            string ContractNo = textBox19.Text;
            Dictionary<string, string> part = new Dictionary<string, string>();
            part.Add("ContractNo", ContractNo);
            string ret1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_Remake_BarcodePringtingService", "GetPart", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(part));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                partList1.Add("");
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    partList1.Add(dtJson.Rows[i]["udf01"].ToString().Trim() + "  " + dtJson.Rows[i]["name_t"].ToString().Trim());
                    // this.comboBox3.Items.Add(dtJson.Rows[i]["udf01"].ToString().Trim() + "  " + dtJson.Rows[i]["name_t"].ToString().Trim());
                }
                comboBox2.Items.AddRange(partList1.ToArray());
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["ErrMsg"].ToString());
            }

        }

        private void textBox15_Click(object sender, EventArgs e)
        {
            string vOrder = textBox19.Text;
            string vDept = textBox18.Text;
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("ContractNo", vOrder);
            p.Add("vDept", vDept);
            if (!string.IsNullOrEmpty(vOrder) && !string.IsNullOrEmpty(vDept))
            {
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_Remake_BarcodePringtingService", "GetTruns", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string RetData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    DataTable dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(RetData);
                    DataRow zero = dt.NewRow();
                    zero["TURN_NO"] = "0";
                    zero["ORDER_NO"] = dt.Rows[0][1].ToString();
                    zero["PRODUCTION_LINE"] = dt.Rows[0][2].ToString();
                    dt.Rows.Add(zero);
                    F_MMS_SelectTurn frm = new F_MMS_SelectTurn(dt);
                    frm.datachange += new F_MMS_SelectTurn.DataChangeHandler(DataChanged2);
                    frm.ShowDialog();
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "This group has not scheduled this work order!");
                    return;
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please enter ticket number and group!");
                return;
            }


            //Take out quantity
            p.Add("vTurnNo", textBox15.Text);
            //Inquire about size
            string ret1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_Remake_BarcodePringtingService", "GetSizeAndNum", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["RetData"].ToString();
                DataTable dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dt.Rows.Count > 0)
                {
                    dataGridView6.Columns.Clear();
                    DataTable tb1 = new DataTable();
                    tb1.Columns.Add("Size");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        tb1.Columns.Add(dt.Rows[i][0].ToString());
                    }
                    DataRow tb1_dr = tb1.NewRow();
                    tb1_dr[0] = "Left";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        tb1_dr[i + 1] = dt.Rows[i][1].ToString();
                    }
                    tb1.Rows.Add(tb1_dr);
                    DataRow tb1_dr2 = tb1.NewRow();
                    tb1_dr2[0] = "Right";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        tb1_dr2[i + 1] = dt.Rows[i][1].ToString();
                    }
                    tb1.Rows.Add(tb1_dr2);
                    DataRow tb1_dr3 = tb1.NewRow();
                    tb1_dr3[0] = "Total";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        tb1_dr3[i + 1] = dt.Rows[i][1].ToString();
                    }
                    tb1.Rows.Add(tb1_dr3);
                    dataGridView6.DataSource = tb1;
                    int index = dataGridView6.Rows.Count;
                    dataGridView6.Rows[index - 1].ReadOnly = true;
                    dataGridView6.Update();
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["ErrMsg"].ToString());
            }
        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {

            if (textBox15.Text.Equals("0"))
            {
                dataGridView6.ReadOnly = false;

            }
            else
            {
                dataGridView6.ReadOnly = true;
            }
        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox15.Text.ToString().Trim()))
            {
                MessageBox.Show("Please select the round first");
                this.comboBox2.SelectedValueChanged -= new System.EventHandler(this.comboBox2_SelectedValueChanged);
                comboBox2.Text = "";
                this.comboBox2.SelectedValueChanged += new System.EventHandler(this.comboBox2_SelectedValueChanged);
                return;
            }
            GetRouting();
        }

        //query process
        private void GetRouting()
        {
            //get a size

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("size_no");
            dataTable.Columns.Add("production_quantity");
            dataTable.Columns.Add("L_quantity");
            dataTable.Columns.Add("R_quantity");
            for (int i = 1; i < dataGridView6.Columns.Count; i++)
            {
                DataRow dataRow = dataTable.NewRow();
                //    dataRow
                string size = dataGridView6.Columns[i].HeaderText.ToString();
                string quantity = dataGridView6.Rows[2].Cells[i].Value == null ? "" : dataGridView6.Rows[0].Cells[i].Value.ToString();
                string L_quantity = dataGridView6.Rows[0].Cells[i].Value == null ? "" : dataGridView6.Rows[0].Cells[i].Value.ToString();
                string R_quantity = dataGridView6.Rows[1].Cells[i].Value == null ? "" : dataGridView6.Rows[0].Cells[i].Value.ToString();
                if (!quantity.Equals("") && !quantity.Equals("0"))
                {
                    dataRow["size_no"] = size;
                    dataRow["production_quantity"] = quantity;
                    dataRow["L_quantity"] = L_quantity;
                    dataRow["R_quantity"] = R_quantity;
                    dataTable.Rows.Add(dataRow);
                }
            }
            if (dataTable.Rows.Count < 1)
            {
                MessageBox.Show("No data to print");
                return;
            }

            string size_no = dataTable.Rows[0][0].ToString();
            dataGridView7.DataSource = null;
            string vOrder = textBox19.Text;
            string part_no = Regex.Split(comboBox2.Text, "\\s+", RegexOptions.IgnoreCase)[0];
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("vOrder", vOrder);
            p.Add("part_no", part_no);
            p.Add("size_no", size_no);
            if (!string.IsNullOrEmpty(vOrder) && !string.IsNullOrEmpty(part_no))
            {
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_Remake_BarcodePringtingService", "GetRound", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string RetData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    DataTable dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(RetData);
                    dataGridView7.AutoGenerateColumns = false;
                    dataGridView7.DataSource = dt;
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please enter work order number and part!");
            }


        }

        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Whether to confirm printing？", "hint", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                button1.Enabled = false;
                if (string.IsNullOrEmpty(textBox16.Text.Trim()) || int.Parse(textBox16.Text.Trim()) < 0)
                {
                    MessageBox.Show("Please fill in the number of tags greater than 0！");
                    button1.Enabled = true;
                    return;
                }
                DataTable processTrainDt = GetDgvToTable(dataGridView7);
                if (dataGridView7.Rows.Count > 0 && !string.IsNullOrEmpty(processTrainDt.Columns[3].ToString()))//
                {
                    if (dataGridView6.Rows.Count > 0)
                    {
                        // Get the value of dataGridView1 as the size and quantity of round 0
                        DataTable dataTable = new DataTable();
                        dataTable.Columns.Add("size_no");
                        dataTable.Columns.Add("production_quantity");
                        dataTable.Columns.Add("r_quantity");
                        dataTable.Columns.Add("l_quantity");
                        for (int i = 1; i < dataGridView6.Columns.Count; i++)
                        {
                            DataRow dataRow = dataTable.NewRow();
                            //    dataRow
                            string size = dataGridView6.Columns[i].HeaderText.ToString();
                            string quantity = dataGridView6.Rows[2].Cells[i].Value == null ? "" : dataGridView6.Rows[2].Cells[i].Value.ToString();
                            string l_quantity = dataGridView6.Rows[0].Cells[i].Value == null ? "" : dataGridView6.Rows[0].Cells[i].Value.ToString();
                            string r_quantity = dataGridView6.Rows[1].Cells[i].Value == null ? "" : dataGridView6.Rows[1].Cells[i].Value.ToString();
                            if (!quantity.Equals("") && !r_quantity.Equals(""))
                            {
                                dataRow["size_no"] = size;
                                dataRow["production_quantity"] = quantity;
                                dataRow["l_quantity"] = l_quantity;
                                dataRow["r_quantity"] = r_quantity;
                                dataTable.Rows.Add(dataRow);
                            }
                        }

                        Dictionary<string, object> p = new Dictionary<string, object>();
                        p.Add("data", processTrainDt);  //craft
                        p.Add("vOrder", textBox19.Text);
                        p.Add("vArtNo", textBox8.Text);
                        p.Add("vDept", textBox18.Text);
                        // p.Add("vVendNo", Regex.Split(textBox2.Text, "\\s+", RegexOptions.IgnoreCase)[0]);
                        p.Add("vPartNo", Regex.Split(comboBox2.Text, "\\s+", RegexOptions.IgnoreCase)[0]);
                        p.Add("vPartName", Regex.Split(comboBox2.Text, "\\s+", RegexOptions.IgnoreCase)[1]);
                        p.Add("vDeliveryDate", dateTimePicker3.Value.ToString("yyyy/MM/dd"));
                        p.Add("vTurnNo", textBox15.Text);
                        p.Add("vPrintNum", textBox16.Text);
                        p.Add("vType", "1");
                        p.Add("vShoeName", textBox17.Text);
                        p.Add("vdataTable", dataTable);
                        string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_Remake_BarcodePringtingService", "SavePrintInfo", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                        if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                        {
                            int resultMsg = int.Parse(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString());
                            if (resultMsg == 10)
                            {
                                MessageBox.Show("Failed to generate QR code! The user's organization information is not queried");
                                button1.Enabled = true;
                                return;
                            }
                            else if (resultMsg == 20)
                            {
                                MessageBox.Show("Failed to generate QR code! QR code cannot be generated repeatedly");
                                button1.Enabled = true;
                                return;
                            }
                        }
                        else
                        {
                            SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                            button1.Enabled = true;
                            return;
                        }

                        Dictionary<string, Object> pintDate = new Dictionary<string, object>();
                        pintDate.Add("vOrder", textBox19.Text);
                        pintDate.Add("vDept", textBox18.Text);
                        pintDate.Add("vPartNo", Regex.Split(comboBox2.Text, "\\s+", RegexOptions.IgnoreCase)[0]);
                        pintDate.Add("vTurnNo", textBox15.Text);
                        pintDate.Add("vdataTable", dataTable);
                        string retPrint = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_Remake_BarcodePringtingService", "GetQrCodeByTurn", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(pintDate));
                        if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retPrint)["IsSuccess"]))
                        {
                            string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retPrint)["RetData"].ToString();
                            DataTable dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                            if (dt.Rows.Count <= 0)
                            {
                                SJeMES_Control_Library.MessageHelper.ShowErr(this, "no print data！");
                                button1.Enabled = true;
                                return;
                            }
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                string str = dt.Rows[i]["order_no"].ToString();
                                string str1 = dt.Rows[i]["sales_order"].ToString();
                                string temp_order = "";
                                string temp_sales = "";
                                string[] vs = str.Split(',');
                                string[] vs1 = str1.Split(',');
                                foreach (var s in vs)
                                {
                                    string t1 = s.Remove(0, 3);
                                    Console.WriteLine(t1);
                                    int kk = int.Parse(t1);
                                    temp_order += kk + ",";
                                }
                                foreach (var s in vs1)
                                {
                                    string t1 = s.Remove(0, 1);
                                    Console.WriteLine(t1);
                                    int kk = int.Parse(t1);
                                    temp_sales += kk + ",";
                                }

                                temp_order = temp_order.Remove(temp_order.Length - 1, 1);
                                temp_sales = temp_sales.Remove(temp_sales.Length - 1, 1);
                                dt.Rows[i]["order_no"] = temp_order;
                                dt.Rows[i]["sales_order"] = temp_sales;
                            }

                            string path = Application.StartupPath + @"\报表" + "\\中仓二维码.frx";
                            WakeOrder_QrCodePrint frm = new WakeOrder_QrCodePrint(dt, path);
                            frm.Show();
                        }
                        else
                        {
                            SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retPrint)["ErrMsg"].ToString());
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Printing is not allowed~~");
                }
                button1.Enabled = true;
            }
        }

        private void dataGridView6_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int RowIndex = this.dataGridView6.CurrentRow.Index;
            int ColIndex = this.dataGridView6.CurrentCell.ColumnIndex;
            int Count = this.dataGridView6.Rows.Count;
            decimal L_Num = decimal.Parse(this.dataGridView6.Rows[0].Cells[ColIndex].Value.ToString());
            decimal R_Num = decimal.Parse(this.dataGridView6.Rows[1].Cells[ColIndex].Value.ToString());

            this.dataGridView6.Rows[Count - 1].Cells[ColIndex].Value = (L_Num + R_Num) / 2;
        }

        private void textBox19_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button6_Click(sender, e);
            }
        }

        private void textBox12_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Enter == e.KeyCode)
            {
                Dictionary<string, string> p = new Dictionary<string, string>();
                p.Add("vOrder", this.textBox12.Text.ToString().Trim());
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_Remake_BarcodePringtingService", "GetOneContractNo", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    this.textBox19.Text = json;
                    this.button6_Click(sender, e);
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                    return;
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            dataGridView9.DataSource = null;
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vOrder_no", textBox23.Text);
            p.Add("vDept", Regex.Split(textBox22.Text, "\\s+", RegexOptions.IgnoreCase)[0]);
            p.Add("vPartNo", Regex.Split(comboBox4.Text, "\\s+", RegexOptions.IgnoreCase)[0]);
            p.Add("vPrintTime", textBox21.Text);
            p.Add("vTurnNoBefore", textBox20.Text);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_Remake_BarcodePringtingService", "GetPrintInfoBySePart", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                dataGridView9.AutoGenerateColumns = false;
                dataGridView9.DataSource = dtJson;

            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                dataGridView8.DataSource = null;
            }
        }

        private void textBox23_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox23_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Enter == e.KeyCode)
            {
                //query parts 
                comboBox4.Items.Clear();
                partList2.Clear();
                string ContractNo = textBox23.Text;
                Dictionary<string, string> part = new Dictionary<string, string>();
                part.Add("ContractNo", ContractNo);
                string ret1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_Remake_BarcodePringtingService", "GetPart", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(part));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
                {
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["RetData"].ToString();
                    DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                    partList2.Add("");
                    for (int i = 0; i < dtJson.Rows.Count; i++)
                    {
                        partList2.Add(dtJson.Rows[i]["udf01"].ToString().Trim() + "  " + dtJson.Rows[i]["name_t"].ToString().Trim());
                        // this.comboBox3.Items.Add(dtJson.Rows[i]["udf01"].ToString().Trim() + "  " + dtJson.Rows[i]["name_t"].ToString().Trim());
                    }
                    comboBox4.Items.AddRange(partList2.ToArray());
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["ErrMsg"].ToString());
                }
            }
        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            this.textBox21.Text = dateTimePicker4.Value.ToString("yyyy/MM/dd");
        }

        private void dataGridView9_SelectionChanged(object sender, EventArgs e)
        {
            this.dataGridView8.DataSource = null;
            //  if (!dataGridView3.Focused)
            // {
            //  return;
            //  }
            if (dataGridView9.CurrentRow != null && dataGridView9.CurrentRow.Index > -1)
            {
                int index = dataGridView9.CurrentRow.Index;
                string vOrder = dataGridView9.Rows[index].Cells[0].Value == null ? "" : dataGridView9.Rows[index].Cells[0].Value.ToString();
                string vDept = dataGridView9.Rows[index].Cells[2].Value == null ? "" : dataGridView9.Rows[index].Cells[2].Value.ToString();
                string vPartNo = dataGridView9.Rows[index].Cells[4].Value == null ? "" : dataGridView9.Rows[index].Cells[4].Value.ToString();
                string vTurnNo = dataGridView9.Rows[index].Cells[6].Value == null ? "" : dataGridView9.Rows[index].Cells[6].Value.ToString();
                string vPointDate = dataGridView9.Rows[index].Cells[9].Value == null ? "" : dataGridView9.Rows[index].Cells[9].Value.ToString();
                Dictionary<string, Object> p = new Dictionary<string, object>();
                p.Add("vOrder", vOrder);
                p.Add("vDept", vDept);
                p.Add("vPartNo", vPartNo);
                p.Add("vTurnNo", vTurnNo);
                p.Add("vPointDate", vPointDate);
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_Remake_BarcodePringtingService", "GetPrintInfoByTurn", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                    dataGridView8.AutoGenerateColumns = false;
                    dataGridView8.DataSource = dtJson.DefaultView;
                    dataGridView8.Rows[0].Selected = false;
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (textBox25.Text == "")
            {
                MessageBox.Show("Please enter the ticket number information！！！");
            }
            else
            {
                //Truncate part number
                string[] partno = (comboBox5.Text).Split();
                dataGridView10.DataSource = null;
                Dictionary<string, Object> p = new Dictionary<string, object>();
                p.Add("order_no", textBox25.Text);
                p.Add("part_no", comboBox5.Text == "" ? "" : partno[0]);
                p.Add("qr_code", textBox24.Text);
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_BarcodePrinting_WakeOrderServer", "GetLbScanningRecord", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                    dataGridView10.DataSource = dtJson;
                    if (dtJson.Rows.Count == 0)
                    {
                        MessageBox.Show("Tagless scan records！");
                    }
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }

            }
        }

        private void textBox25_TextChanged(object sender, EventArgs e)
        {
            partList1.Clear();
            comboBox5.Items.Clear();
            comboBox5.Text = "";
            string ContractNo = textBox25.Text;
            Dictionary<string, string> part = new Dictionary<string, string>();
            part.Add("ContractNo", ContractNo);
            string ret1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_Remake_BarcodePringtingService", "GetPart", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(part));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                partList1.Add("");
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    partList1.Add(dtJson.Rows[i]["udf01"].ToString().Trim() + "  " + dtJson.Rows[i]["name_t"].ToString().Trim());
                    // this.comboBox3.Items.Add(dtJson.Rows[i]["udf01"].ToString().Trim() + "  " + dtJson.Rows[i]["name_t"].ToString().Trim());
                }
                comboBox5.Items.AddRange(partList1.ToArray());
            }
            else
            {
                //SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["ErrMsg"].ToString());
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DataTable print_result = null;
            //Determine whether to hit a two-dimensional code mark or a round of two-dimensional code mark
            //Print a QR code label
            if (dataGridView8.CurrentRow != null && dataGridView8.CurrentRow.Index > -1 && dataGridView8.SelectedRows.Count > 0)
            {
                int index = dataGridView8.CurrentRow.Index;
                string vQrCode = dataGridView8.Rows[index].Cells[0].Value == null ? "" : dataGridView8.Rows[index].Cells[0].Value.ToString();
                string vOrder = dataGridView8.Rows[index].Cells[1].Value == null ? "" : dataGridView8.Rows[index].Cells[1].Value.ToString();
                Dictionary<string, Object> p = new Dictionary<string, object>();
                p.Add("vQrCode", vQrCode);
                p.Add("vOrder", vOrder);
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_Remake_BarcodePringtingService", "GetQrCodeOne", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    print_result = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }

            }
            //Print a round of QR code labels
            else if (dataGridView9.CurrentRow != null && dataGridView9.CurrentRow.Index > -1 && dataGridView9.SelectedRows.Count > 0)
            {
                int index = dataGridView9.CurrentRow.Index;
                string vOrder = dataGridView9.Rows[index].Cells[0].Value == null ? "" : dataGridView9.Rows[index].Cells[0].Value.ToString();
                string vDept = dataGridView9.Rows[index].Cells[2].Value == null ? "" : dataGridView9.Rows[index].Cells[2].Value.ToString();
                string vPartNo = dataGridView9.Rows[index].Cells[4].Value == null ? "" : dataGridView9.Rows[index].Cells[4].Value.ToString();
                string vTurnNo = dataGridView9.Rows[index].Cells[6].Value == null ? "" : dataGridView9.Rows[index].Cells[6].Value.ToString();
                string vPointDate = dataGridView9.Rows[index].Cells[9].Value == null ? "" : dataGridView9.Rows[index].Cells[9].Value.ToString();
                Dictionary<string, Object> p = new Dictionary<string, object>();
                p.Add("vOrder", vOrder);
                p.Add("vDept", vDept);
                p.Add("vPartNo", vPartNo);
                p.Add("vTurnNo", vTurnNo);
                p.Add("vPointDate", vPointDate);
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_Remake_BarcodePringtingService", "GetQrCodeByTurnBefore", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    print_result = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please select what to print");
                return;
            }


            for (int i = 0; i < print_result.Rows.Count; i++)
            {
                string str = print_result.Rows[i]["order_no"].ToString();
                string str1 = print_result.Rows[i]["sales_order"].ToString();
                string temp_order = "";
                string temp_sales = "";
                string[] vs = str.Split(',');
                string[] vs1 = str1.Split(',');
                foreach (var s in vs)
                {
                    string t1 = s.Remove(0, 3);
                    Console.WriteLine(t1);
                    int p = int.Parse(t1);
                    temp_order += p + ",";
                }
                foreach (var s in vs1)
                {
                    string t1 = s.Remove(0, 1);
                    Console.WriteLine(t1);
                    int p = int.Parse(t1);
                    temp_sales += p + ",";
                }

                temp_order = temp_order.Remove(temp_order.Length - 1, 1);
                temp_sales = temp_sales.Remove(temp_sales.Length - 1, 1);
                print_result.Rows[i]["order_no"] = temp_order;
                print_result.Rows[i]["sales_order"] = temp_sales;
            }

            string path = Application.StartupPath + @"\报表" + "\\中仓二维码.frx";
            WakeOrder_QrCodePrint frm = new WakeOrder_QrCodePrint(print_result, path);
            frm.Show();
        }

        private void textBox26_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Enter == e.KeyCode)
            {
                Dictionary<string, string> p = new Dictionary<string, string>();
                p.Add("vOrder", this.textBox26.Text.ToString().Trim());
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_Remake_BarcodePringtingService", "GetOneContractNo", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    this.textBox23.Text = json;
                    this.button8_Click(sender, e);
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                    return;
                }
            }
        }

        private void textBox27_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Enter == e.KeyCode)
            {
                Dictionary<string, string> p = new Dictionary<string, string>();
                p.Add("vOrder", this.textBox27.Text.ToString().Trim());
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_Remake_BarcodePringtingService", "GetOneContractNo", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    this.textBox25.Text = json;
                    this.button10_Click(sender, e);
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                    return;
                }
            }
        }
    }
}
