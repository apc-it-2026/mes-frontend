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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static F_WOO_ManualInput.ManualInput_SelectTurn;

namespace F_WOO_ManualInput
{
    public partial class F_WOO_ManualInput : MaterialForm
    {
        public F_WOO_ManualInput()
        {
            InitializeComponent();

            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);

            LoadWakerOrder(); //Load ticket
            LoadWareHouse();
        }
        List<string> partListTailor = new List<string>();     //Cut out and out of stock parts
        List<string> partListVend = new List<string>();        //Manufacturer's warehousing parts

        string WAREHOUSE = string.Empty;
        //Load current repository
        public void LoadWareHouse()
        {
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_WOO_ManualInputServer", "GetWarehouse", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(string.Empty));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                WAREHOUSE = json;
            }
            else
            {
                MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        //Load master ticket
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
                //Bind the data source to the data source for cutting the inbound and outbound work order number
                textBox6.AutoCompleteCustomSource = orderSource;   //bind data source
                textBox6.AutoCompleteMode = AutoCompleteMode.Suggest;    //Show related dropdown
                textBox6.AutoCompleteSource = AutoCompleteSource.CustomSource;   //set properties
                //The main work order binding data source of the daily supporting report
                textBox16.AutoCompleteCustomSource = orderSource;   //bind data source 
                textBox16.AutoCompleteMode = AutoCompleteMode.Suggest;    //Show related dropdown
                textBox16.AutoCompleteSource = AutoCompleteSource.CustomSource;   //set properties
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(WAREHOUSE))
            {
                MessageBox.Show("No user organization relationship was queried");
                return;
            }
            this.comboBox1.Items.Clear();//Empty parts
            comboBox1.Text = "";
            textBox7.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            comboBox1.Items.Clear();
            comboBox1.Text = "";
            comboBox6.Text = "";
          
            dataGridView2.DataSource = "";
            //query parts 
            string vOrder = textBox6.Text;
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("vOrder", vOrder);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_BarcodePrinting_WakeOrderServer", "QueryPart", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
              //  for (int i = 0; i < dtJson.Rows.Count; i++)
           //     {
               //     this.comboBox1.Items.Add(dtJson.Rows[i]["udf01"].ToString().Trim() + "  " + dtJson.Rows[i]["name_t"].ToString().Trim());
              //  }
                partListTailor.Clear();  //Clear the original data in the list to avoid data duplication
                partListTailor.Add(""); //add blank line
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                  partListTailor.Add(dtJson.Rows[i]["udf01"].ToString().Trim() + "  " + dtJson.Rows[i]["name_t"].ToString().Trim());
                }
                comboBox1.Items.AddRange(partListTailor.ToArray());

            }
            else
            {
                MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                return;
            }

            //Query shoe type and art
            string ret1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_BarcodePrinting_WakeOrderServer", "GetArtShoeType", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                textBox7.Text = dtJson.Rows[0][0].ToString();  //art
                textBox4.Text = dtJson.Rows[0][1].ToString();  //Shoe type
            }
            else
            {
                MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["ErrMsg"].ToString());
            }

          



        }
        private void comboBox1_TextUpdate(object sender, EventArgs e)
        {
            string s = this.comboBox1.Text;  //Get the input content of the cb_material control

            List<string> strList = new List<string>();   //Store raw data (can be objects, strings...)
            strList.AddRange(partListTailor.ToArray());  // List<string> materials
            List<string> strListNew = new List<string>();
            //Empty combobox
            this.comboBox1.Items.Clear();
            //traverse all raw data
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
                this.comboBox1.Items.AddRange(strListNew.ToArray());
            }
            else  // When no conditions exist
            {
                // The following code is to add the value entered by itself when the query does not meet the conditions
                this.comboBox1.Items.Add(this.comboBox1.Text);
            }
            //Set the cursor position, if not set: the cursor position is always kept in the first column, resulting in the reverse order of the input keywords
            this.comboBox1.SelectionStart = this.comboBox1.Text.Length;  // Set the cursor position, if not set: the cursor position is always kept in the first column, resulting in the reverse order of the input keywords
            Cursor = Cursors.Default; //Keep the mouse pointer in the original state, sometimes the mouse pointer will be covered by the drop-down box, so you need to set it once
            this.comboBox1.DroppedDown = true; // Automatic pop-up drop-down box
        }



        public void DataChanged(object sender, DataChangeEventArgs args)
        {
            this.textBox5.Text = args.turn;
            this.textBox3.Text = args.production_line;
        }
        private void textBox5_Click(object sender, EventArgs e)
        {
            string vOrder = textBox6.Text;
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("vOrder", vOrder);
            if (!string.IsNullOrEmpty(vOrder))
            {
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_WOO_ManualInputServer", "GetRounds", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string RetData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    DataTable dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(RetData);
                    DataRow zero = dt.NewRow();
                    zero["TURN_NO"] = "0";
                    zero["ORDER_NO"] = dt.Rows[0][1].ToString();
                    zero["PRODUCTION_LINE"] = dt.Rows[0][2].ToString();
                    dt.Rows.Add(zero);
                    ManualInput_SelectTurn frm = new ManualInput_SelectTurn(dt);
                    frm.datachange += new DataChangeHandler(DataChanged);
                    frm.ShowDialog();
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "There is no round information for this ticket!");
                    return;
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please enter the ticket number!");
                return;
            }

            dataGridView2.DataSource = null;
            //Take out quantity
            p.Add("vTurnNo", textBox5.Text); //Rounds
            p.Add("vDept", textBox3.Text);   //group
                                             //Inquire about size
            DataTable tb1 = new DataTable();
            string ret1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_BarcodePrinting_WakeOrderServer", "getTurn", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["RetData"].ToString();
                DataTable dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dt.Rows.Count > 0)
                {
                    dataGridView2.Columns.Clear();
                    tb1.Columns.Add("Size");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        tb1.Columns.Add(dt.Rows[i][0].ToString());
                    }
                    DataRow tb1_dr = tb1.NewRow();
                    DataRow tb1_dr2 = tb1.NewRow();
                    tb1_dr[0] = "Number of rounds";
                    tb1_dr2[0] = "Number of entries";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        tb1_dr[i + 1] = dt.Rows[i][1].ToString();
                        tb1_dr2[i + 1] = string.Empty;
                    }
                    tb1.Rows.Add(tb1_dr);
                    tb1.Rows.Add(tb1_dr2);
                    dataGridView2.DataSource = tb1;
                    dataGridView2.Rows[0].ReadOnly = true;
                    dataGridView2.Rows[0].DefaultCellStyle.BackColor = Color.Lavender;
                    dataGridView2.Update();

                }

            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["ErrMsg"].ToString());
                return;
            }
            
        }

        /*
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.comboBox7.Items.Clear();//清空收发单位
            this.comboBox7.Text = "";
            //查询收发单位 
            string vOrder = textBox6.Text; //主工单
            string turn = textBox5.Text;  //轮次
            string vPartNo = Regex.Split(comboBox1.Text, "\\s+", RegexOptions.IgnoreCase)[0]; 
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("vOrder", vOrder);  //主工单
            p.Add("vPartNo", vPartNo);//部件编号
            p.Add("vTurn",turn);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_WOO_ManualInputServer", "QueryUnit", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    this.comboBox7.Items.Add(dtJson.Rows[i]["train_no"].ToString().Trim() + "  " + dtJson.Rows[i]["vend_name"].ToString().Trim());
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                return;
            }
        }
        */

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(WAREHOUSE))
            {
                MessageBox.Show("No user organization relationship was queried");
                return;
            }
            string type = comboBox6.Text;
            DialogResult dr;
            if (type.Equals("Cut and put into storage"))
            {
                 dr = MessageBox.Show("Whether the cropped data is determined to be submitted", "hint", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                if (dr == DialogResult.OK)
                {
                    Dictionary<string,object> p = getTailorDateP();
                    if (p.Count > 0)
                    {
                        TailorInsert(p);
                    }
                }
            }
            else if (type.Equals("Outbound tailoring"))
            {
                 dr = MessageBox.Show("Whether the data cut out of the library is determined to be submitted", "hint", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                if (dr == DialogResult.OK)
                {
                    Dictionary<string, object> p = getTailorDateP();
                    if (p.Count > 0)
                    {
                        TailorOut(p);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select the correct type");
                return;
            }
        }

        public Dictionary<string,object> getTailorDateP()
        {
            string vOrder_no = textBox6.Text;
            string vPart_no = Regex.Split(comboBox1.Text, "\\s+", RegexOptions.IgnoreCase)[0];
            string vArt = textBox7.Text;
            string vUnit = textBox3.Text;  //   crop group
            string vWareHouse = WAREHOUSE;
            string vTurn = textBox5.Text;
            DataTable dataTable = new DataTable();
            DataColumn size = new DataColumn("size");
            dataTable.Columns.Add(size);
            DataColumn number = new DataColumn("number");
            dataTable.Columns.Add(number);
            for (int i = 1; i < dataGridView2.Columns.Count; i++)
            {
                string sizetemp = dataGridView2.Columns[i].HeaderText.ToString(); ;
                string numbertemp = dataGridView2.Rows[1].Cells[i].Value == null ? "" : dataGridView2.Rows[1].Cells[i].Value.ToString();
                if (!numbertemp.Equals(""))
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["size"] = sizetemp;
                    dataRow["number"] = numbertemp;
                    dataTable.Rows.Add(dataRow);
                }
            }
            
            Dictionary<string, object> p = new Dictionary<string, object>();
            if (dataTable.Rows.Count < 1)
            {
                MessageBox.Show("No size information to save！");
                return p;
            }
            p.Add("vOrder_no", vOrder_no);
            p.Add("vPart_no", vPart_no);
            p.Add("vArt", vArt);
            p.Add("vUnit", vUnit);
            p.Add("vWareHouse", vWareHouse);
            p.Add("vTurn", vTurn);
            p.Add("dataTable", dataTable);
            return p;
        }

        public void TailorInsert(Dictionary<string,object> p)
        {
            string ret1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_WOO_ManualInputServer", "TailorInsert", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
            {
                MessageBox.Show("Successfully saved");
                dataGridView2.DataSource = null;
                comboBox1.Text = "";
                textBox5.Text = "";
                textBox3.Text = "";
                comboBox6.Text = "";
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["ErrMsg"].ToString());
            }
        }

        public void TailorOut(Dictionary<string, object> p)
        {
            string ret1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_WOO_ManualInputServer", "CutAndOut", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
            {
                MessageBox.Show("Successfully saved");
                dataGridView2.DataSource = null;
                comboBox1.Text = "";
                textBox5.Text = "";
                textBox3.Text = "";
                comboBox6.Text = "";
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["ErrMsg"].ToString());
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(WAREHOUSE))
            {
                MessageBox.Show("No user organization relationship was queried");
                return;
            }
            this.comboBox2.Items.Clear();//Empty parts
            comboBox2.Text = "";
            this.comboBox7.Items.Clear();//Clear outsourcing purchase order
            comboBox7.Text = "";
            comboBox3.Text = "";//outgoing unit
            comboBox4.Items.Clear(); //Process name
            comboBox4.Text = "";
            textBox15.Text = "";//Shoe type
            textBox14.Text = "";//Rounds
            textBox13.Text = "";//ART
            comboBox5.Text = "";//type

            dataGridView4.DataSource = "";  //Clear size quantity
            //query parts 
            string vOrder = textBox16.Text;
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("vOrder", vOrder);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_BarcodePrinting_WakeOrderServer", "QueryPart", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
               // for (int i = 0; i < dtJson.Rows.Count; i++)
                //{
                 //   this.comboBox2.Items.Add(dtJson.Rows[i]["udf01"].ToString().Trim() + "  " + dtJson.Rows[i]["name_t"].ToString().Trim());
               // }
                partListVend.Clear();  //Clear the original data in the list to avoid data duplication
                partListVend.Add(""); //add blank line
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    partListVend.Add(dtJson.Rows[i]["udf01"].ToString().Trim() + "  " + dtJson.Rows[i]["name_t"].ToString().Trim());
                }
                comboBox2.Items.AddRange(partListVend.ToArray());
            }
            else
            {
                MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                return;
            }
            //Query shoe type and art
            string ret1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_BarcodePrinting_WakeOrderServer", "GetArtShoeType", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                textBox13.Text = dtJson.Rows[0][0].ToString();  //art
                textBox15.Text = dtJson.Rows[0][1].ToString();  //Shoe type
            }
            else
            {
                MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["ErrMsg"].ToString());
            }
            //Bring out outsourcing purchase order
            string ret2 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_WOO_ManualInputServer", "LoadOutBill", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {

                    this.comboBox7.Items.Add(dtJson.Rows[i - 1]["ORDER_NO"].ToString());
                }

            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["ErrMsg"].ToString());
                return;
            }
        }
        private void comboBox2_TextUpdate(object sender, EventArgs e)
        {
            string s = this.comboBox2.Text;  //Get the input content of the cb_material control

            List<string> strList = new List<string>();   //Store raw data (can be objects, strings...)
            strList.AddRange(partListVend.ToArray());  // List<string> materials
            List<string> strListNew = new List<string>();
            //Empty combobox
            this.comboBox2.Items.Clear();
            //traverse all raw data
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
                this.comboBox2.Items.AddRange(strListNew.ToArray());
            }
            else  // When no conditions exist
            {
                // The following code is to add the value entered by itself when the query does not meet the conditions
                this.comboBox2.Items.Add(this.comboBox2.Text);
            }
            //Set the cursor position, if not set: the cursor position is always kept in the first column, resulting in the reverse order of the input keywords
            this.comboBox2.SelectionStart = this.comboBox2.Text.Length;  // Set the cursor position, if not set: the cursor position is always kept in the first column, resulting in the reverse order of the input keywords
            Cursor = Cursors.Default; //Keep the mouse pointer in the original state, sometimes the mouse pointer will be covered by the drop-down box, so you need to set it once
            this.comboBox2.DroppedDown = true; // Automatic pop-up drop-down box
        }


        private void textBox14_Click(object sender, EventArgs e)
        {
            string vOrder = textBox16.Text;
           
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("vOrder", vOrder);
            //Manufacturer's round table
            if (!string.IsNullOrEmpty(vOrder))
            {
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_WOO_ManualInputServer", "GetRounds", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string RetData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    DataTable dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(RetData);
                    DataRow zero = dt.NewRow();
                    zero["TURN_NO"] = "0";
                    zero["ORDER_NO"] = dt.Rows[0][1].ToString();
                  
                    zero["PRODUCTION_LINE"] = dt.Rows[0][2].ToString();
                    dt.Rows.Add(zero);
                    ManualInput_SelectTurn frm = new ManualInput_SelectTurn(dt);
                    frm.datachange += new DataChangeHandler(DataChanged1);
                    frm.ShowDialog();
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "There is no round information for this ticket!");
                    return;
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please enter the ticket number!");
                return;
            }

            dataGridView2.DataSource = null;
            //Take out quantity
            p.Add("vTurnNo", textBox14.Text); //Rounds
            p.Add("vDept", textBox1.Text);   //group
                                             //Inquire about size
            DataTable tb1 = new DataTable();
            string ret1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_BarcodePrinting_WakeOrderServer", "getTurn", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["RetData"].ToString();
                DataTable dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dt.Rows.Count > 0)
                {
                   
                    dataGridView4.Columns.Clear();
                    tb1.Columns.Add("Size");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        tb1.Columns.Add(dt.Rows[i][0].ToString());
                    }
                    DataRow tb1_dr = tb1.NewRow();
                    DataRow tb1_dr2 = tb1.NewRow();
                    tb1_dr[0] = "Number of rounds";
                    tb1_dr2[0] = "Number of entries";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        tb1_dr[i + 1] = dt.Rows[i][1].ToString();
                        tb1_dr2[i + 1] = string.Empty;
                    }
                    tb1.Rows.Add(tb1_dr);
                    tb1.Rows.Add(tb1_dr2);
                    dataGridView4.DataSource = tb1;
                    dataGridView4.Rows[0].ReadOnly = true;
                    dataGridView4.Rows[0].DefaultCellStyle.BackColor = Color.Lavender;
                    dataGridView4.Update();

                }

            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["ErrMsg"].ToString());
                return;
            }

        }
        public void DataChanged1(object sender, DataChangeEventArgs args)
        {
            this.textBox14.Text = args.turn;
           this.textBox1.Text = args.production_line;

        }
        //After selecting the part
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string vOrder_no = textBox16.Text;
            string vPart_no = Regex.Split(comboBox2.Text, "\\s+", RegexOptions.IgnoreCase)[0];
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("vOrder", vOrder_no);
            p.Add("vPart_no", vPart_no);

            //Load outgoing units
            string ret2 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_WOO_ManualInputServer", "GetProcessingUnit", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["RetData"].ToString();
                string[] s = json.Split(',');
                comboBox3.Items.Clear();
                if (s.Length > 0)
                {
                    for (int i = 0; i < s.Length; i++)
                    {
                        this.comboBox3.Items.Add(s[i]);
                    }
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["ErrMsg"].ToString());
                return;
            }

            //Loading process
            string vTurn = textBox14.Text;
            p.Add("vTurn", vTurn);
            string ret1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_WOO_ManualInputServer", "GetPross_Name", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["RetData"].ToString();
                DataTable dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                comboBox4.Items.Clear();
                for (int i = 1; i <= dt.Rows.Count; i++)
                {
                    this.comboBox4.Items.Add(dt.Rows[i - 1]["PROSS_NAME"].ToString());
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["ErrMsg"].ToString());
                return;
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string type = comboBox5.Text;
            if (type.Equals("Outbound manufacturer"))
            {
                OutVend();
            }
            else if (type.Equals("Manufacturer's warehousing"))
            {
                InVend();
            }
            else
            {
                MessageBox.Show("Please select the correct type");
            }
        }


        public void OutVend()
        {
            DialogResult  dr = MessageBox.Show("Whether the data of the outbound manufacturer is confirmed to be submitted", "hint", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if(dr == DialogResult.OK) { 
            string vOrder = textBox16.Text;
            string vArt = textBox13.Text;
            string vShoeType = textBox15.Text;
            string vPartNo = Regex.Split(comboBox2.Text, "\\s+", RegexOptions.IgnoreCase)[0];
            string vTurn = textBox14.Text;
            string vTrain = Regex.Split(comboBox3.Text, "-+", RegexOptions.IgnoreCase)[0];
            string vVendName = Regex.Split(comboBox3.Text, "-+", RegexOptions.IgnoreCase)[1];
            string vPROSS_NAME = comboBox4.Text;
            string vWareHouse = WAREHOUSE;
            //Get size quantity
            DataTable dataTable = new DataTable();
            DataColumn size = new DataColumn("size");
            dataTable.Columns.Add(size);
            DataColumn number = new DataColumn("number");
            dataTable.Columns.Add(number);
            for (int i = 1; i < dataGridView4.Columns.Count; i++)
            {
                string sizetemp = dataGridView4.Columns[i].HeaderText.ToString(); ;
                string numbertemp = dataGridView4.Rows[1].Cells[i].Value == null ? "" : dataGridView4.Rows[1].Cells[i].Value.ToString();
                if (!numbertemp.Equals(""))
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["size"] = sizetemp;
                    dataRow["number"] = numbertemp;
                    dataTable.Rows.Add(dataRow);
                }
            }

            if (dataTable.Rows.Count < 1)
            {
                MessageBox.Show("No size information to save！");
                return;
            }
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("vOrder", vOrder);
            p.Add("vArt", vArt);
            p.Add("vShoeType", vShoeType);
            p.Add("vPartNo", vPartNo);
            p.Add("vTurn", vTurn);
            p.Add("vTrain", vTrain);
            p.Add("vVendName", vVendName);
            p.Add("vPROSS_NAME", vPROSS_NAME);
            p.Add("vWareHouse", vWareHouse);
            p.Add("vDt", dataTable);
            string ret1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_WOO_ManualInputServer", "outVendoutVend", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["RetData"].ToString();
                MessageBox.Show("Submitted successfully，Generate work order is：" + json);
                dataGridView4.DataSource = null;
                textBox14.Text = null;
                comboBox2.Text = null;
                comboBox3.Text = null;
                comboBox4.Text = null;
                comboBox5.Text = null;
                comboBox7.Text = null;
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["ErrMsg"].ToString());
                return;
            }
            }


        }



        public void InVend()
        {
            DialogResult dr = MessageBox.Show("Whether the data stored by the manufacturer is confirmed to be submitted", "hint", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (dr == DialogResult.OK)
            {
                string vOrder = textBox16.Text;
                string vArt = textBox13.Text;
                string vShoeType = textBox15.Text;
                string vPartNo = Regex.Split(comboBox2.Text, "\\s+", RegexOptions.IgnoreCase)[0];
                string vTurn = textBox14.Text;
                string vTrain = Regex.Split(comboBox3.Text, "-+", RegexOptions.IgnoreCase)[0];
                string vVendName = Regex.Split(comboBox3.Text, "-+", RegexOptions.IgnoreCase)[1];
                string vPROSS_NAME = comboBox4.Text;
                string vWareHouse = WAREHOUSE;
                //Get size quantity
                DataTable dataTable = new DataTable();
                DataColumn size = new DataColumn("size");
                dataTable.Columns.Add(size);
                DataColumn number = new DataColumn("number");
                dataTable.Columns.Add(number);
                for (int i = 1; i < dataGridView4.Columns.Count; i++)
                {
                    string sizetemp = dataGridView4.Columns[i].HeaderText.ToString(); ;
                    string numbertemp = dataGridView4.Rows[1].Cells[i].Value == null ? "" : dataGridView4.Rows[1].Cells[i].Value.ToString();
                    if (!numbertemp.Equals(""))
                    {
                        DataRow dataRow = dataTable.NewRow();
                        dataRow["size"] = sizetemp;
                        dataRow["number"] = numbertemp;
                        dataTable.Rows.Add(dataRow);
                    }
                }

                if (dataTable.Rows.Count < 1)
                {
                    MessageBox.Show("No size information to save！");
                    return;
                }
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("vOrder", vOrder);
                p.Add("vArt", vArt);
                p.Add("vShoeType", vShoeType);
                p.Add("vPartNo", vPartNo);
                p.Add("vTurn", vTurn);
                p.Add("vTrain", vTrain);
                p.Add("vVendName", vVendName);
                p.Add("vPROSS_NAME", vPROSS_NAME);
                p.Add("vWareHouse", vWareHouse);
                p.Add("vDt", dataTable);
                p.Add("vOutBill", comboBox7.Text);//Outsourced purchase order
                string ret1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.F_WOO_ManualInputServer", "InVendoutVend", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
                {
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["RetData"].ToString();
                    DataTable dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                    MessageBox.Show("The submission is successful, and the generated work order is：" + json);
                    dataGridView4.DataSource = null;
                    textBox14.Text = null;
                    comboBox2.Text = null;
                    comboBox3.Text = null;
                    comboBox4.Text = null;
                    comboBox5.Text = null;
                    comboBox7.Text = null;
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["ErrMsg"].ToString());
                    return;
                }

            }

        }

       
    }
}
