using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using AutocompleteMenuNS;
using CommanClassLib.Properties;
using CommanClassLib.Util;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using SJeMES_Framework.Common;
using static System.Math;


namespace CommanClassLib
{
    public abstract partial class InOutPutBaseView : MaterialForm
    {
        readonly InOutPutBaseModel model;
        private string d_dept = ""; //There are branches with optional groups
        readonly Bitmap bitmapSmile;
        readonly Bitmap bitmapCry;

        protected DataTable dtInput;
        //protected DataTable dtReturn; //Fallback with subsequent increase

        public InOutPutBaseView()
        {
            InitializeComponent();
        }

        internal InOutPutBaseView(InOutPutBaseModel model) //InOutPutBaseView(new InOutPutBaseModel(Client))    Change according to the subclass called, so that the view layer and the model layer are decoupled
        {
            InitializeComponent();
            this.model = model;

            bitmapSmile = new Bitmap(Resources.smile);
            bitmapCry = new Bitmap(Resources.cry);
            WindowState = FormWindowState.Maximized;
            btnImage.Visible = false;
        }

        private void btnSeIDRefresh_Click(object sender, EventArgs e)
        {
            listSize.Items.Clear();
            SetButtonInvisable();
            LoadSeId();
        }

        private void SetButtonInvisable()
        {
            for (int i = 1; i < 10; i++)
            {
                Button button = (Button)Controls.Find("btn_c" + i, true)[0];
                //button.BackColor = Color.Green;
                button.Visible = false;
            }
        }

        private void LoadSeId() //string dllName, string className, string method, string data
        {
            autocompleteMenu1.Items = null;
            autocompleteMenu1.MaximumSize = new Size(300, 300);
            var columnWidth = new[] { 50, 300 };

            try
            {
                DataTable dt = model.GetDatatable("LoadSeId", string.Empty);
                int n = 1;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    autocompleteMenu1.AddItem(new MulticolumnAutocompleteItem(
                            new[] { n + "", dt.Rows[i]["SE_ID"] + " " + dt.Rows[i]["PO"] + " " + dt.Rows[i]["ART_NO"] }, dt.Rows[i]["SE_ID"] + "|" + dt.Rows[i]["PO"])
                        { ColumnWidth = columnWidth, ImageIndex = n });
                    n++;
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowErr(this, model.ErrorMessage + " | " + ex.Message);
            }
        }

        private void InOutPutBaseView_Load(object sender, EventArgs e)
        {
            tabControlBase.Height = Screen.GetBounds(this).Height - 70;

            WorkHoursMaintain frmWorkHour = new WorkHoursMaintain(model.Client.APIURL, model.Client.UserToken, model.Client, model.Client.Language);
            frmWorkHour.TopLevel = false;
            frmWorkHour.FormBorderStyle = FormBorderStyle.None;
            frmWorkHour.Dock = DockStyle.Fill;
            tabPageWorkingHours.Controls.Add(frmWorkHour);
            frmWorkHour.Show();
            tabControlBase.SelectedIndex = frmWorkHour.AfterShow();
            d_dept = frmWorkHour.d_dept;

            LoadSeId(); //There is try catch inside, no need to build a layer outside

            tbDayFinishQty.Text = LoadDayFinish();

            UIHelper.UIUpdate(Name, this, model.Client, "", model.Client.Language);
        }

        private string LoadDayFinish()
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("vDDept", d_dept);
            p.Add("vInOut", "IN");
            string data = JsonConvert.SerializeObject(p);
            try
            {
                return model.GetJson("LoadDayFinish", data);
            }
            catch (Exception ex)
            {
                MessageHelper.ShowErr(this, model.ErrorMessage + " | " + ex.Message);
                return "";
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            textTime.Text = DateTime.Now.ToString();
        }

        private void SetButtonGray(Button clickButton, bool isClick = true)
        {
            if (!isClick)
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
                clickButton.BackColor = Color.Gray;
                for (int i = 1; i < 10; i++)
                {
                    Button button = (Button)Controls.Find("btn_c" + i, true)[0];
                    button.Enabled = false;
                }
            }
        }

        private void SetButtonColor(Button clickButton, bool isClick = true)
        {
            if (!isClick)
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
                clickButton.BackColor = label12.BackColor;
                for (int i = 1; i < 10; i++)
                {
                    Button button = (Button)Controls.Find("btn_c" + i, true)[0];
                    button.Enabled = true;
                }
            }
        }

        private void reflashListSize()
        {
            listSize.Items.Remove(listSize.SelectedItem);
            dtInput.Rows.RemoveAt(listSize.SelectedIndex);
            listSize.SelectedIndex = -1;
        }

        private void btn_c_Click(object sender, EventArgs e)
        {
            if (HasDept())
            {
                if (string.IsNullOrWhiteSpace(GetDept()) && string.IsNullOrEmpty(GetDept()))
                {
                    string msg = UIHelper.UImsg("Please enter input group！", model.Client, "", model.Client.Language);
                    MessageHelper.ShowErr(this, msg);
                    return;
                }
            }

            SetButtonGray((Button)sender);
            DataRow dataRow = dtInput.Rows[listSize.SelectedIndex];
            //Computational correlation is written in the logic layer
            if (dtInput.Rows.Count > 0)
            {
                int qty = Convert.ToInt32(((Button)sender).Text);
                int UnFinishQty = Convert.ToInt32(tbSizeQty.Text) - Convert.ToInt32(tbSizeFinishQty.Text);
                //int oldunFinishQty = UnFinishQty;
                string scan_ip = IPUtil.GetIpAddress();

                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("vOrgId", dataRow["org_id"]);
                p.Add("vSeId", dataRow["se_id"]);
                p.Add("vSeSeq", dataRow["se_seq"]);
                p.Add("vSizeNo", dataRow["size_no"]);
                p.Add("vDDept", d_dept);
                p.Add("vSizeSeq", dataRow["SIZE_SEQ"]);
                p.Add("vQty", qty);
                p.Add("vIP", scan_ip);
                p.Add("vPO", dataRow["po"]);
                p.Add("vArtNo", dataRow["art_no"]);
                p.Add("vSeDay", dataRow["se_day"]);
                string data = JsonConvert.SerializeObject(p);

                try
                {
                    if (model.UpdateDatatable("updateInFinshQty", data))
                    {
                        if (qty == UnFinishQty)
                        {
                            reflashListSize();
                        }
                        else if (qty > UnFinishQty && label12.BackColor == Color.CornflowerBlue)
                        {
                            ScanFailed();
                            MessageBox.Show("The amount invested is greater than the amount that can be invested！");
                            btnSeIDRefresh_Click(sender, e);
                            return;
                        }

                        ScanSucess();
                        UnFinishQty = UnFinishQty - qty;
                        tbDayFinishQty.Text = (Convert.ToInt32(tbDayFinishQty.Text) + qty).ToString();
                    }
                    else
                    {
                        ScanFailed();
                    }
                }
                catch (Exception ex)
                {
                    //UnFinishQty = oldunFinishQty;
                    ScanFailed();
                    MessageHelper.ShowErr(this, model.ErrorMessage + " | " + ex.Message);
                }

                DisplayQtyButton(UnFinishQty);
            }

            Thread.Sleep(1000);
            SetButtonColor((Button)sender);
        }

        /// <summary>
        ///     Clear the button corresponding to the selected size
        /// </summary>
        /// <param name="uFinishQty">unfinished quantity</param>
        private void DisplayQtyButton(int uFinishQty)
        {
            for (int i = 1; i < 10; i++)
            {
                Button button = (Button)Controls.Find("btn_c" + i, true)[0];
                if (uFinishQty >= Abs(int.Parse(button.Text)))
                {
                    button.Visible = true;
                }
                else
                {
                    button.Visible = false;
                }
            }
        }

        private void ScanFailed() //Scan failed
        {
            btnImage.Visible = true;
            btnImage.BackgroundImage = bitmapCry;
            btnImage.BackColor = Color.Transparent;
            btnImage.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void ScanSucess() //Scan was successful
        {
            btnImage.Visible = true;
            btnImage.BackgroundImage = bitmapSmile;
            btnImage.BackColor = Color.Transparent;
            btnImage.BackgroundImageLayout = ImageLayout.Stretch;
        }


        protected virtual void DoFullQrCode(string txtQrCode) //Leave room for subclasses to override
        {
            string[] str = txtQrCode.Split(',');
            int length = str.Length;
            if (model.ParseQrCode(length))
            {
                listSize.Items.Clear();
                SetButtonInvisable();
                string scan_ip = IPUtil.GetIpAddress();
                string org_id = str[1];
                string se_id = str[2];
                string se_seq = "1";
                string size_no = str[6];
                int qty = int.Parse(str[7]);
                string size_seq = str[8];
                string art_no = str[9];

                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("vSeId", se_id);
                p.Add("vSeSeq", se_seq);
                p.Add("vSizeNo", size_no);
                string data = JsonConvert.SerializeObject(p);

                try
                {
                    DataTable dt = model.GetDatatable("LoadDayFinish", data);

                    if (!model.BeforeInputJudge(dt))
                    {
                        string msg02 = UIHelper.UImsg(model.ErrorMessage, model.Client, "", model.Client.Language);
                        MessageBox.Show(msg02);
                        tbQuerySeID.Text = "";
                        ScanFailed();
                        return;
                    }

                    string po = dt.Rows[0]["PO"].ToString();
                    tbPo.Text = po;
                    string se_day = dt.Rows[0]["SE_DAY"].ToString().Substring(0, 10);
                    int finish_qty = (int)decimal.Parse(dt.Rows[0]["FINISH_QTY"].ToString());
                    int se_qty = (int)decimal.Parse(dt.Rows[0]["SE_QTY"].ToString());


                    //if (updateInFinshQty(ord_id, se_id, se_seq, size_no, size_seq, qty, scan_ip, vPO, art_no, se_day))  //Can I use template mode? Is it better for subclasses to set specific data parameters?
                    p.Clear();
                    p.Add("vOrgId", org_id);
                    p.Add("vSeId", se_id);
                    p.Add("vSeSeq", se_seq);
                    p.Add("vSizeNo", size_no);
                    p.Add("vDDept", d_dept);
                    p.Add("vSizeSeq", size_seq);
                    p.Add("vQty", qty);
                    p.Add("vIP", scan_ip);
                    p.Add("vPO", po);
                    p.Add("vArtNo", art_no);
                    p.Add("vSeDay", se_day);
                    data = JsonConvert.SerializeObject(p);

                    if (model.UpdateDatatable("updateFinshQty", data))
                    {
                        tbSizeFinishQty.Text = (finish_qty + qty).ToString();
                        tbDayFinishQty.Text = (int.Parse(tbDayFinishQty.Text) + qty).ToString();
                        tbSizeQty.Text = se_qty.ToString();
                        tbSize.Text = size_no;
                        tbQty.Text = qty.ToString();
                        ScanSucess();
                    }
                    else
                    {
                        ScanFailed();
                    }
                }
                catch (Exception ex)
                {
                    ScanFailed();
                    MessageHelper.ShowErr(this, model.ErrorMessage + " | " + ex.Message);
                }
            }
        }

        protected virtual void DoSelfQrCode(string txtQrCode)
        {
            if (HasDept())
            {
                if (!JudgeDeptValue(out string deptMessage))
                {
                    string msg = UIHelper.UImsg(deptMessage, model.Client, "", model.Client.Language);
                    MessageHelper.ShowErr(this, msg);
                    tbQuerySeID.Text = "";
                    return;
                }
            }

            string se_id = txtQrCode.Split('|')[0];
            tbPo.Text = "";

            if (!string.IsNullOrEmpty(se_id)) //load size
            {
                listSize.Items.Clear();

                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("vSeId", se_id);
                string data = JsonConvert.SerializeObject(p);
                dtInput = model.GetDatatable("GetSeSize", data);

                if (dtInput.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dtInput.Rows)
                    {
                        listSize.Items.Add(dataRow["size_no"]);
                    }

                    tbPo.Text = txtQrCode.Split('|')[1];
                    tbQuerySeID.Text = "";
                }

                SetButtonInvisable();
            }

            SetProductInfo(0, 0);
        }

        protected abstract bool JudgeDeptValue(out string deptMessage); //Determine whether the group meets the conditions

        protected abstract void SetDept(); //set group value

        protected abstract string GetDept(); //get group value

        protected virtual bool HasDept() //Is there a group
        {
            return false;
        }

        protected virtual void SetProductInfo(int qty, int sizeFinish)
        {
            tbSizeQty.Text = qty.ToString();
            tbSizeFinishQty.Text = sizeFinish.ToString();
            tbSize.Text = "";
            tbQty.Text = "";
        }


        private void tbQuerySeID_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string txtQrCode = tbQuerySeID.Text.ToUpper();
                //B,200,A0A19040071,SM0A190415000049,485,1,9,10,48,EF9370,03996
                //Category, organization, order, unique code of the ticket, ticket number, order sequence, size, quantity, size serial number, art, model number

                if (!string.IsNullOrWhiteSpace(txtQrCode) && !string.IsNullOrEmpty(txtQrCode) && txtQrCode.Contains(","))
                {
                    //Scan the QR code
                    DoFullQrCode(txtQrCode);
                }
                else
                {
                    //hand-selected order
                    DoSelfQrCode(txtQrCode);
                }
            }
        }

        private void listSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listSize.SelectedIndex > -1)
            {
                DataRow dataRow = dtInput.Rows[listSize.SelectedIndex];

                int qty = Convert.ToInt32(dataRow["SE_QTY"]);

                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("vOrgId", dataRow["org_id"]);
                p.Add("vSeId", dataRow["se_id"]);
                p.Add("vSeSeq", dataRow["se_seq"]);
                p.Add("vSizeNo", dataRow["size_no"]);
                string data = JsonConvert.SerializeObject(p);

                int sizeFinishQty = Convert.ToInt32(model.GetJson("GeSizeFinishQty", data));

                DisplayQtyButton(qty - sizeFinishQty);

                SetProductInfo(qty, sizeFinishQty);

                tbQuerySeID.Text = "";

                if (HasDept())
                {
                    SetDept();
                }
            }
            else
            {
                SetProductInfo(0, 0);
            }
        }
    }
}