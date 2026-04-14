using MaterialSkin.Controls;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using SJeMES_Framework.Common;
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
using static F_CraftProductOutPut.SelectPartNo;

namespace F_CraftProductOutPut
{
    public partial class F_CraftProductOutPut : MaterialForm
    {
        public F_CraftProductOutPut()
        {
            InitializeComponent();
        }

        private void F_CraftProductOutPut_Load(object sender, EventArgs e)
        {
            SelectSampleNO();
            txtTodayOutNum.Text = "0";
            SelectUserInfo();
            SelectTodayOutput();
        }
        int listIndexSize = -1;
        Button button_qty;
        bool ByClick = true;
        public class ComboboxEntry
        {
            public string Code { get; set; }
            public string Name { get; set; }
        }
        /// <summary>
        /// 查询登录人信息
        /// </summary>
        private void SelectUserInfo()
        {
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_CraftProductPutIntoServer", "SelectUserInfo", Program.client.UserToken, JsonConvert.SerializeObject(string.Empty));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                if (dtJson.Rows.Count>0)
                {
                    txtWorkCenterNo.Text = dtJson.Rows[0]["staff_department"].ToString();
                    txtWorkCenter.Text = dtJson.Rows[0]["department_name"].ToString();
                    txtUserNo.Text = dtJson.Rows[0]["staff_no"].ToString();
                    txtUserName.Text = dtJson.Rows[0]["staff_name"].ToString();
                }
                else
                {
                    MessageHelper.ShowErr(this,"没有查询到登陆人的信息!");
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        /// <summary>
        /// 查询今日已产出
        /// </summary>
        private void SelectTodayOutput()
        {
            if (string.IsNullOrEmpty(txtWorkCenterNo.Text))
            {
                MessageHelper.ShowErr(this, "工作中心代号为空");
                return;
            }
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("grp_dept", txtWorkCenterNo.Text);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_CraftProductPutIntoServer", "SelectTodayOutput", Program.client.UserToken, JsonConvert.SerializeObject(dic));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                if (json == "")
                {
                    txtTodayOutNum.Text = "0";
                }
                else
                {
                    txtTodayOutNum.Text = json;
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        /// <summary>
        /// 查询样品单号
        /// </summary>
        private void SelectSampleNO()
        {
            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_CraftProductPutIntoServer", "SelectSampleNO", Program.client.UserToken, JsonConvert.SerializeObject(string.Empty));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {
                    collection.Add(dtJson.Rows[i - 1]["sample_no"].ToString());
                }
                txtSampleNo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtSampleNo.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtSampleNo.AutoCompleteCustomSource = collection;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void txtSampleNo_KeyDown(object sender, KeyEventArgs e)//查询工艺
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("sample_no", txtSampleNo.Text);
            List<ComboboxEntry> craftList = new List<ComboboxEntry>();
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_CraftProductPutIntoServer", "SelectCraft", Program.client.UserToken, JsonConvert.SerializeObject(dic));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonConvert.DeserializeObject<DataTable>(json);
                //DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                craftList.Add(new ComboboxEntry() { Code = "", Name = "" });
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    craftList.Add(new ComboboxEntry() { Code = dtJson.Rows[i]["process_no"].ToString(), Name = dtJson.Rows[i]["process_name"].ToString() });
                }
                //cbCraft.DataSource = craftList;
                //cbCraft.DisplayMember = "Name";
                //cbCraft.ValueMember = "Code";
                txtPartName.Text = "";
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                //cbCraft.DataSource = null;
            }
        }

        private void btnSelectPartNo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSampleNo.Text))
            {
                MessageHelper.ShowErr(this, "样品单不能为空！");
                return;
            }
            if (string.IsNullOrEmpty(txtCraftName.Text))
            {
                MessageHelper.ShowErr(this, "工艺不能为空！");
                return;
            }
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("sample_no", txtSampleNo.Text);
            dic.Add("process_no", label19.Text);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_CraftProductPutIntoServer", "SelectPartSizeNumOutPut", Program.client.UserToken, JsonConvert.SerializeObject(dic));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                //DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                DataTable dtJson = JsonConvert.DeserializeObject<DataTable>(json);
                SelectPartNo selectPartNo = new SelectPartNo(dtJson);
                selectPartNo.DataChange += new SelectPartNo.DataChangeHandler(DataChanged_SizeQty);
                selectPartNo.ShowDialog();
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        DataTable dt = new DataTable();
        private void DataChanged_SizeQty(object sender, DataChangeEventArgs args)
        {
            displayQtyButton(0);
            setSizeButtonToDefault();
            txtOutNum.Text = "";
            txtSizeIntoNum.Text = "";
            txtSizeOutNum.Text = "";
            txtSize.Text = "";
            txtNum.Text = "";
            //DataTable dt = new DataTable();
            dt = args.dtSizeNumPart;
            if (dt.Rows.Count > 0)
            {
                string strSize = "";
                string strPart = "";
                string strPartNo = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == dt.Rows.Count - 1)
                    {
                        strSize += dt.Rows[i]["notputsize"].ToString();
                        strPart += dt.Rows[i]["partName"].ToString();
                        strPartNo += "'" + dt.Rows[i]["partNo"].ToString() + "'";
                    }
                    else
                    {
                        strSize += dt.Rows[i]["notputsize"].ToString() + ",";
                        strPart += dt.Rows[i]["partName"].ToString() + ",";
                        strPartNo += "'" + dt.Rows[i]["partNo"].ToString() + "',";
                    }
                }
                try
                {
                    string[] sizeNos = strSize.Split(',');
                    double[] sizeNo = Array.ConvertAll<string, double>(sizeNos, s => double.Parse(s));
                    Array.Sort(sizeNo);
                    listSize.Items.Clear();
                    for (int i = 0; i < sizeNo.Length; i++)
                    {
                        if (listSize.Items.Contains(sizeNo[i]))
                        {
                            continue;
                        }
                        listSize.Items.Add(sizeNo[i]);
                    }
                }
                catch (Exception)
                {

                    string[] sizeNos = strSize.Split(',');
                    listSize.Items.Clear();
                    for (int i = 0; i < sizeNos.Length; i++)
                    {
                        if (listSize.Items.Contains(sizeNos[i]))
                        {
                            continue;
                        }
                        listSize.Items.Add(sizeNos[i]);
                    }
                }
                label17.Text = strPartNo;
                txtPartName.Text = strPart;
                txtProcess.Text = txtCraftName.Text;
                txtSample.Text = txtSampleNo.Text;
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("sample_no", txtSampleNo.Text);
                dic.Add("process_no", label19.Text);
                dic.Add("part_no", strPartNo);
                string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_CraftProductPutIntoServer", "SelectArtPurpose", Program.client.UserToken, JsonConvert.SerializeObject(dic));
                if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                    txtPurpose.Text = dtJson.Rows[0]["purpose"].ToString();
                    txtArt.Text = dtJson.Rows[0]["art_no"].ToString();
                }
                else
                {
                    MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }
            else
            {
                MessageHelper.ShowErr(this, "未查询到该样品单的尺码数量！");
            }
        }
        /// <summary>
        /// 清空根据选中size对应的button
        /// </summary>
        /// <param name="uFinishQty">未完工的数量</param>
        private void displayQtyButton(double uFinishQty)
        {
            for (int i = 1; i < 10; i++)
            {
                Button button = (Button)Controls.Find("btn_c" + i, true)[0];
                if (uFinishQty >= System.Math.Abs(double.Parse(button.Text)))
                {
                    button.Visible = true;
                }
                else
                {
                    button.Visible = false;
                }
            }
        }
        private void setSizeButtonToDefault()
        {
            for (int i = 1; i < 10; i++)
            {
                Button button = (Button)Controls.Find("btn_c" + i, true)[0];
                button.Visible = false;
            }
        }

        private void listSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listSize.SelectedIndex > -1)
            {
                try
                {
                    setSizeButtonBackColorToDefault();
                    listIndexSize = listSize.SelectedIndex;
                    string size_no = listSize.SelectedItem.ToString();
                    txtSize.Text = size_no;
                    string part_no = "";
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string[] sizes = dt.Rows[i]["notputsize"].ToString().Split(',');
                            for (int j = 0; j < sizes.Length; j++)
                            {
                                if (sizes[j] == size_no)
                                {
                                    part_no += "'" + dt.Rows[i]["partNo"].ToString() + "',";
                                }
                            }
                        }
                    }
                    string[] partArray = part_no.Split(',');
                    if (partArray.Length == 2)
                    {
                        part_no = part_no.Replace(",", "");
                    }
                    if (partArray.Length > 2)
                    {
                        part_no = part_no.Remove(part_no.Length - 1, 1);

                    }
                    label20.Text = part_no;
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    //sample_no, process_no, part_no, size_no
                    dic.Add("sample_no", txtSampleNo.Text);
                    dic.Add("process_no", label19.Text);
                    //dic.Add("part_no", label17.Text);
                    dic.Add("part_no", label20.Text);
                    dic.Add("size_no", size_no);
                    string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_CraftProductPutIntoServer", "SelectOutNum", Program.client.UserToken, JsonConvert.SerializeObject(dic));
                    if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                    {
                        string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                        Dictionary<string, string> p = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                        string result = p["num"].ToString();
                        double num = 0;
                        if (!string.IsNullOrEmpty(result))
                        {
                            num = double.Parse(result);
                        }
                        displayQtyButton(num);
                        button2.Text = "";
                        button2.Text += "可产出数量:" + num + "";
                        label18.Text = num.ToString();
                    }
                    else
                    {
                        MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                        displayQtyButton(0);
                    }
                    SelectSizeOut(dic);
                }
                catch (Exception ex)
                {
                    MessageHelper.ShowErr(this, ex.Message);
                }
            }
            else
            {
                txtSizeIntoNum.Text = 0.ToString();
                txtSizeOutNum.Text = 0.ToString();
                txtSize.Text = 0.ToString();
                txtNum.Text = "";
            }
        }
        /// <summary>
        /// 设置size的按钮为蓝色
        /// </summary>
        private void setSizeButtonBackColorToDefault()
        {
            for (int i = 1; i < 10; i++)
            {
                Button button = (Button)Controls.Find("btn_c" + i, true)[0];
                button.BackColor = System.Drawing.Color.CornflowerBlue;
                button.Enabled = true;
            };
        }
        private void SelectSizeOut(Dictionary<string, string> dic)
        {
            string ret2 = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_CraftProductPutIntoServer", "SelectSizeOut", Program.client.UserToken, JsonConvert.SerializeObject(dic));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["RetData"].ToString();
                txtSizeOutNum.Text = json;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["ErrMsg"].ToString());
            }
            string ret3 = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_CraftProductPutIntoServer", "SelectSizeInput", Program.client.UserToken, JsonConvert.SerializeObject(dic));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret3)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret3)["RetData"].ToString();
                txtSizeIntoNum.Text = json;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret3)["ErrMsg"].ToString());
            }
        }

        private void btn_c1_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            txtNum.Text = button.Text;
            FinishQty(((Button)sender).Text, (Button)sender);
        }
        private void FinishQty(string num, Button button)
        {
            if (button.Text == "确定")
            {
                ByClick = false;
            }
            else
            {
                ByClick = true;
                setSizeButtonBackColorToDefault();
            }
            button_qty = button;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("sample_no", txtSampleNo.Text);
            dic.Add("process_no", label19.Text);
            //dic.Add("part_no", label17.Text);
            dic.Add("part_no", label20.Text);
            dic.Add("size_no", listSize.SelectedItem.ToString());
            dic.Add("num", num);
            dic.Add("dept", txtWorkCenterNo.Text);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_CraftProductPutIntoServer", "InsertOut", Program.client.UserToken, JsonConvert.SerializeObject(dic));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                SetButtonEnable(button_qty);
                SelectTodayOutput();
                Dictionary<string, string> p = new Dictionary<string, string>();
                p.Add("sample_no", txtSampleNo.Text);
                p.Add("process_no", label19.Text);
                //p.Add("part_no", label17.Text);
                p.Add("part_no", label20.Text);
                p.Add("size_no", listSize.SelectedItem.ToString());
                string ret1 = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_CraftProductPutIntoServer", "SelectOutNum", Program.client.UserToken, JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
                {
                    string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["RetData"].ToString();
                    Dictionary<string, string> a = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                    string result = a["num"].ToString();
                    double num1 = 0;
                    if (!string.IsNullOrEmpty(result))
                    {
                        num1 = double.Parse(result);
                    }
                    displayQtyButton(num1);
                    button2.Text = "";
                    button2.Text = "可产出数量:" + num1 + "";
                    label18.Text = num1.ToString();
                    SelectSizeOut(p);
                }
                else
                {
                    MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["ErrMsg"].ToString());
                    displayQtyButton(0);
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void SetButtonEnable(Button clickButton)
        {
            if (!ByClick)
            {
                for (int i = 1; i < 10; i++)
                {
                    Button button = (Button)Controls.Find("btn_c" + i, true)[0];
                    button.Enabled = false;
                    button.BackColor = Color.Gray;
                }
            }
            else
            {
                if (clickButton.Text != "确定")
                {
                    clickButton.BackColor = Color.Gray;
                }
                for (int i = 1; i < 10; i++)
                {
                    Button button = (Button)Controls.Find("btn_c" + i, true)[0];
                    button.Enabled = false;
                }
            }
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            if (!ByClick)
            {
                for (int i = 1; i < 10; i++)
                {
                    Button button = (Button)Controls.Find("btn_c" + i, true)[0];
                    button.Enabled = true;
                    button.BackColor = label12.BackColor;
                }
            }
            else
            {
                button_qty.BackColor = label12.BackColor;
                for (int i = 1; i < 10; i++)
                {
                    Button button = (Button)Controls.Find("btn_c" + i, true)[0];
                    button.Enabled = true;
                }
            }
            timer1.Stop();
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            if (listSize.SelectedIndex != -1)
            {
                if (string.IsNullOrEmpty(txtOutNum.Text))
                {
                    MessageHelper.ShowErr(this, "输入的数量为空！");
                    return;
                }
                if (double.Parse(txtOutNum.Text) > double.Parse(label18.Text))
                {
                    MessageHelper.ShowErr(this, "产出的数量不能大于工艺投入数量！");
                    return;
                }
                txtNum.Text = txtOutNum.Text;
                FinishQty(txtOutNum.Text, (Button)sender);
            }
            else
            {
                MessageHelper.ShowErr(this, "请先选择尺码！");
            }
        }

        private void txtSampleNo_TextChanged(object sender, EventArgs e)
        {
            txtCraftName.Text = "";
            label19.Text = "";
        }

        private void btnSelectCraft_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSampleNo.Text))
            {
                MessageHelper.ShowErr(this, "样品单不能为空！");
                return;
            }
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("sample_no", txtSampleNo.Text);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_CraftProductPutIntoServer", "SelectCraft", Program.client.UserToken, JsonConvert.SerializeObject(dic));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonConvert.DeserializeObject<DataTable>(json);
                OutPutSelectCraft outPutSelectCraft = new OutPutSelectCraft(dtJson);
                outPutSelectCraft.DataChange += new OutPutSelectCraft.DataChangeHandler(DataChanged_SelectCraft);
                outPutSelectCraft.ShowDialog();
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void DataChanged_SelectCraft(object sender, OutPutSelectCraft.DataChangeEventArgs args)
        {
            DataTable dt = args.dtCarft;
            if (dt.Rows.Count > 0)
            {
                string processNo = dt.Rows[0]["process_no"].ToString();
                string processName = dt.Rows[0]["process_name"].ToString();
                label19.Text = processNo;
                txtCraftName.Text = processName;
            }
            else
            {
                MessageHelper.ShowErr(this, "没有查询到工艺！");
                return;
            }
        }
    }
}
