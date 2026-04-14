using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SJeMES_Framework.WinForm.FormDesign.Control
{
    public class TextBoxClass: ControlClass
    {
        public string Title;
        public bool Enable;
        public string DataKey;
        public string Value;
        public bool Edit;
        public bool Add;
        public string DataType;
        public string DateTimeFormat;
        public string DataSelectSQL;
        public string DataShowSQL;
        public List<string> Keys;
        public Dictionary<string, string> DataEnum;
        public string WebServiceUrl;
        public string DefaultValue;
        public bool IsAddOrEdit;
        public Class.OrgClass Org;
        public bool IsNull;
        public int width;

        public TextBoxClass(Class.OrgClass Org, string XML,string WebServiceUrl,bool IsAddOrEdit)
        {
            try
            {
                this.Org = Org;
                this.IsAddOrEdit = IsAddOrEdit;
                this.WebServiceUrl = WebServiceUrl;
                Title = Common.StringHelper.GetDataFromFirstTag(XML, "<Title>", "</Title>");
                DataType = Common.StringHelper.GetDataFromFirstTag(XML, "<DataType>", "</DataType>");

                try
                {
                    Add = Convert.ToBoolean(Common.StringHelper.GetDataFromFirstTag(XML, "<Add>", "</Add>"));
                }
                catch { Add = true; }

                try
                {
                    Edit = Convert.ToBoolean(Common.StringHelper.GetDataFromFirstTag(XML, "<Edit>", "</Edit>")); ;
                }
                catch { Edit = true; }

                
                ControlType = Common.StringHelper.GetDataFromFirstTag(XML, "<ControlType>", "</ControlType>");
                DataKey = Common.StringHelper.GetDataFromFirstTag(XML, "<DataKey>", "</DataKey>");

                try
                {
                    Enable = Convert.ToBoolean(Common.StringHelper.GetDataFromFirstTag(XML, "<Enable>", "</Enable>"));
                }
                catch { Enable = true; }
               
                DataSelectSQL = Common.StringHelper.GetDataFromFirstTag(XML, "<DataSelectSQL>", "</DataSelectSQL>");
                DataShowSQL = Common.StringHelper.GetDataFromFirstTag(XML, "<DataShowSQL>", "</DataShowSQL>");
                DateTimeFormat= Common.StringHelper.GetDataFromFirstTag(XML, "<DateTimeFormat>", "</DateTimeFormat>");
                DefaultValue = Common.StringHelper.GetDataFromFirstTag(XML, "<DefaultValue>", "</DefaultValue>");

                width = 200;

                try
                {
                    width =Convert.ToInt32( Common.StringHelper.GetDataFromFirstTag(XML, "<Width>", "</Width>"));
                }
                catch { }

                IsNull = true;

                if(XML.Contains("<IsNull>") && XML.Contains("</IsNull>"))
                {
                    if (string.IsNullOrEmpty(Common.StringHelper.GetDataFromFirstTag(XML, "<IsNull>", "</IsNull>")))
                    {
                        IsNull = true;
                    }
                    else
                    {
                        IsNull = Convert.ToBoolean(Common.StringHelper.GetDataFromFirstTag(XML, "<IsNull>", "</IsNull>"));
                    }
                }


                string xmlDataEnum = Common.StringHelper.GetDataFromFirstTag(XML, "<DataEnum>", "</DataEnum>");
                DataEnum = new Dictionary<string, string>();
                if (xmlDataEnum.IndexOf("@:")>-1 && xmlDataEnum.IndexOf("@;")>-1)
                {
                    string[] s =new string[1];
                    s[0] = "@;";
                    foreach (string xmlTmp in xmlDataEnum.Split(s, StringSplitOptions.RemoveEmptyEntries))
                    {
                        s[0] = "@:";
                        DataEnum.Add(xmlTmp.Split(s, StringSplitOptions.None)[0], xmlTmp.Split(s, StringSplitOptions.None)[1]);
                    }
                }

                Keys = new List<string>();

                string strTMP = Common.StringHelper.GetDataFromFirstTag(XML, "<Keys>", "</Keys>");
                if (strTMP.LastIndexOf(",") > -1)
                {
                    string[] s = strTMP.Split(',');
                    foreach (string ss in s)
                    {
                        Keys.Add(ss);
                    }
                }
                else if(!string.IsNullOrEmpty(strTMP))
                {
                    Keys.Add(strTMP);
                }

            }
            catch(Exception ex) { MessageBox.Show(XML + " " + ex.Message);  }
        }

        public TextBoxClass(Class.OrgClass Org, Control.DataViewColumnClass DCC, string WebServiceUrl, bool IsAddOrEdit)
        {
            try
            {
                this.Org = Org;
                this.IsAddOrEdit = IsAddOrEdit;
                this.WebServiceUrl = WebServiceUrl;
                Title = DCC.Title;
                DataType = DCC.DataType;
                Add = DCC.Add;
                Edit = DCC.Edit;
                ControlType = "TextBox";
                DataKey = DCC.DataKey;
                Enable = DCC.Enable;
                DataSelectSQL = DCC.DataSelectSQL;
                DataShowSQL = DCC.DataShowSQL;
                DateTimeFormat = DCC.DateTimeFormat;
                DefaultValue = DCC.DefaultValue;
                Keys = DCC.Keys;
                DataEnum = DCC.DataEnum;
                IsNull = DCC.IsNull;
                this.FC = DCC.FC;
                this.width = DCC.Width;

            }
            catch { }
        }

        public TextBoxClass(Class.OrgClass Org, TextBoxClass TBC, string WebServiceUrl, bool IsAddOrEdit)
        {
            try
            {
                this.Org = Org;
                this.IsAddOrEdit = IsAddOrEdit;
                this.WebServiceUrl = WebServiceUrl;
                Title = TBC.Title;
                DataType = TBC.DataType;
                Add = TBC.Add;
                Edit = TBC.Edit;
                ControlType = "TextBox";
                DataKey = TBC.DataKey;
                Enable = TBC.Enable;
                DataSelectSQL = TBC.DataSelectSQL;
                DataShowSQL = TBC.DataShowSQL;
                DateTimeFormat = TBC.DateTimeFormat;
                DefaultValue = TBC.DefaultValue;
                Keys = TBC.Keys;
                DataEnum = TBC.DataEnum;
                IsNull = TBC.IsNull;
                this.FC = TBC.FC;
                this.width = TBC.width;

            }
            catch { }
        }


        public Panel GetControls(string ParentName,Forms.FormClass FC)
        {
            this.ParentName = ParentName;
            Panel panelRet = new Panel();

            try
            {
                this.FC = FC;
                this.Control = panelRet;
                this.ControlName = panelRet.Name = ParentName + "_Paneltxt" + this.Title;
                panelRet.Width = 180;
                panelRet.Dock = DockStyle.Left;
                panelRet.Margin = new Padding(0);

                Label labTilte = new Label();
                labTilte.Name = panelRet.Name + "_labTitle";
                labTilte.Text = this.Title;
                labTilte.Font = new System.Drawing.Font("宋体", 9);
                labTilte.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                labTilte.ForeColor = System.Drawing.Color.Black;
                labTilte.AutoSize = false;
                labTilte.Dock = DockStyle.Fill;
                panelRet.Controls.Add(labTilte);

                if (this.DataType == "String" || this.DataType == "Int" || this.DataType == "Float" || this.DataType == "DataSource"
                    || this.DataType=="OtherData" || this.DataType == "HeadDataShow"|| this.DataType == "SYSUserCode")
                {
                    TextBox txtValue = new TextBox();
                    txtValue.Name = panelRet.Name + "_txtValue";
                    txtValue.Enabled = this.Enable;
                    txtValue.Width = 100;
                    txtValue.Font = new System.Drawing.Font("黑体", 9);
                    txtValue.TextAlign = HorizontalAlignment.Left;
                    txtValue.ForeColor = System.Drawing.Color.Black;
                    txtValue.Dock = DockStyle.Right;
                    txtValue.TextChanged += TxtValue_TextChanged;
                    txtValue.KeyPress += TxtValue_KeyPress;
                    if(this.DataType == "DataSource")
                    {
                        
                        txtValue.MouseDoubleClick += TxtValue_MouseDoubleClick;
                    }

                 

                    panelRet.Controls.Add(txtValue);
                }
                else if (this.DataType == "Bool" || this.DataType == "Enum")
                {
                    ComboBox cbb = new ComboBox();
                    cbb.Name = panelRet.Name + "_txtValue";
                    cbb.Enabled = this.Enable;
                    cbb.Width = 100;
                    cbb.Font = new System.Drawing.Font("黑体", 9);
                    cbb.ForeColor = System.Drawing.Color.Black;
                    if (this.DataType == "Bool")
                    {
                        cbb.Items.Add("是");
                        cbb.Items.Add("否");
                    }
                    else if (this.DataType == "Enum")
                    {
                        foreach (string key in this.DataEnum.Keys)
                        {
                            cbb.Items.Add(key);
                        }
                    }
                    cbb.SelectedIndexChanged += Cbb_SelectedIndexChanged;
                    cbb.Dock = DockStyle.Right;
                    panelRet.Controls.Add(cbb);
                }
                else if (this.DataType == "Date" || this.DataType == "Time" || this.DataType == "DateTime")
                {
                    DateTimePicker dtp = new DateTimePicker();
                    dtp.Name = panelRet.Name + "_txtValue";
                    dtp.Enabled = this.Enable;
                    dtp.Width = 100;
                    dtp.Font = new System.Drawing.Font("黑体", 9);
                    dtp.ForeColor = System.Drawing.Color.Black;


                    dtp.Format = DateTimePickerFormat.Custom;
                    dtp.CustomFormat = this.DateTimeFormat;

                    dtp.TextChanged += Dtp_ValueChanged;
                    dtp.Dock = DockStyle.Right;
                    panelRet.Controls.Add(dtp);
                }
            }
            catch { }
            return panelRet;
        }

        

        private void TxtValue_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if ((this.Status == "Add" && this.Add) || ( this.Status == "Edit" && this.Edit))
            {

                

                string sql = this.DataSelectSQL;

                if (sql.Contains("@DATA."))
                {
                    List<string> ss = SJeMES_Framework.Common.StringHelper.GetDataFromTag(sql, "@DATA.", "@");

                    
                    Forms.FormBodyEditAndAdd frm2 = (Forms.FormBodyEditAndAdd)this.Control.FindForm();

                    foreach (string s in ss)
                    {
                        foreach (string key in frm2.Childrens.Keys)
                        {
                            TextBoxClass tbc = (TextBoxClass)frm2.Childrens[key];

                            if (tbc.DataKey == s)
                            {
                                sql = sql.Replace("@DATA." + s + "@", tbc.Value);
                            }
                        }
                    }
                }

                CommonForm.frmSearchData frm = new CommonForm.frmSearchData(Org,WebServiceUrl, sql, true, true);
                frm.ShowDialog();

                string sTmp = string.Empty;

                List<string> sRows = Common.StringHelper.GetDataFromTag(frm.ReturnDataXML, "<Row>", "</Row>");

                if (sRows.Count > 0)
                {
                    string Wheresql = string.Empty;

                    if (this.Keys.Count > 0)
                    {
                        sTmp = Common.StringHelper.GetDataFromFirstTag(frm.ReturnDataXML, "<" + this.Keys[0] + ">", "</" + this.Keys[0] +">");
                        this.SetData(sTmp);

                       
                    }
                }

                
            }
        }

        private void Dtp_ValueChanged(object sender, EventArgs e)
        {
            this.Value = ((DateTimePicker)sender).Text;
        }

        private void Cbb_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(this.DataType)
            {
                case "Bool":
                    if(((ComboBox)sender).Text =="是")
                    {
                        this.Value = "True";
                    }
                    else
                    {
                        this.Value = "False";
                    }
                    break;
                case "Enum":
                    this.Value = DataEnum[((ComboBox)sender).Text];
                    break;
            }
           
        }

        private void TxtValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (this.DataType)
            {
                case "Int":
                    if (!(e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == 8 || e.KeyChar == 13))
                        e.Handled = true;
                    break;
                case "Float":
                    if (!(e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '.' || e.KeyChar == 8 || e.KeyChar == 13))
                        e.Handled = true;
                    break;
                case "String":
                    break;
                default:
                    e.Handled = true;
                    break;
            }
        }

        private void TxtValue_TextChanged(object sender, EventArgs e)
        {
            

            if (this.DataType == "String" || this.DataType == "Int" || this.DataType == "Float")
                this.Value = ((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).Text;


                if (!IsAddOrEdit)
                {
                    foreach (string key in this.FC.FormPanels.Keys)
                    {
                        if (this.FC.FormPanels[key].ControlName == this.ParentName)
                            foreach (string key2 in this.FC.FormPanels[key].Childrens.Keys)
                            {
                                TextBoxClass tbc = (TextBoxClass)this.FC.FormPanels[key].Childrens[key2];

                                if (tbc.DefaultValue == this.DataKey && tbc.DataType == "OtherData")
                                {
                                    string value = Common.WebServiceHelper.GetString(Org,WebServiceUrl, tbc.DataSelectSQL.Replace("?", Value), new Dictionary<string, string>());
                                    tbc.SetData(value);
                                }
                            }
                    }
                }
                else
                {
                   Forms.FormBodyEditAndAdd frm =(Forms.FormBodyEditAndAdd) this.Control.FindForm();
                    foreach (string key in frm.Childrens.Keys)
                    {
                        TextBoxClass tbc = (TextBoxClass)frm.Childrens[key];

                        if (tbc.DefaultValue == this.DataKey && tbc.DataType == "OtherData")
                        {
                            string value = Common.WebServiceHelper.GetString(Org,WebServiceUrl, tbc.DataSelectSQL.Replace("?", Value), new Dictionary<string, string>());
                            tbc.SetData(value);
                        }
                    }

                }
            

            if ((this.DataType == "String" || this.DataType == "Int" || this.DataType == "Float" || this.DataType == "DataSource")
                && ((this.Status == "Add" && this.Add) || (this.Status == "Edit" && this.Edit)))
            {
                if (string.IsNullOrEmpty(((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).Text.Trim())
                && !this.IsNull)
                {
                    ((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).BackColor = Color.Red;
                }
                else
                {
                    if (this.DataType == "String" || this.DataType == "Int" || this.DataType == "Float")
                        ((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).BackColor = Color.White;
                    if (this.DataType == "DataSource")
                        ((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).BackColor = Color.LightYellow;
                }
            }
        }

        public void SetData(System.Data.DataRow dr)
        {
            try
            {
                this.Value = dr[DataKey].ToString();
                if (this.DataType == "String" || this.DataType == "Int" || this.DataType == "Float" || this.DataType == "OtherData" || this.DataType == "SYSUserCode")
                {
                    ((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).Text = dr[DataKey].ToString();

                }
                if (this.DataType == "Bool")
                {
                    if (string.IsNullOrEmpty(dr[DataKey].ToString()))
                    {
                        ((ComboBox)this.Control.Controls[this.ControlName + "_txtValue"]).Text = string.Empty;
                        ((ComboBox)this.Control.Controls[this.ControlName + "_txtValue"]).SelectedIndex = -1;
                    }
                    else if (dr[DataKey].ToString().ToLower() == "true")
                    {
                        ((ComboBox)this.Control.Controls[this.ControlName + "_txtValue"]).SelectedIndex = 0;
                    }
                    else if (dr[DataKey].ToString().ToLower() == "false")
                    {
                        ((ComboBox)this.Control.Controls[this.ControlName + "_txtValue"]).SelectedIndex = 1;
                    }
                }
                if (this.DataType == "Enum")
                {

                    ((ComboBox)this.Control.Controls[this.ControlName + "_txtValue"]).Text = this.Value;

                    foreach (string key in DataEnum.Keys)
                    {
                        if (DataEnum[key] == dr[DataKey].ToString())
                        {
                            ((ComboBox)this.Control.Controls[this.ControlName + "_txtValue"]).Text = key;
                        }
                    }

                }
                if (this.DataType == "Date" || this.DataType == "Time" || this.DataType == "DateTime")
                {

                    try
                    {
                        ((DateTimePicker)this.Control.Controls[this.ControlName + "_txtValue"]).Value = Convert.ToDateTime(Value);
                    }
                    catch
                    {
                        ((DateTimePicker)this.Control.Controls[this.ControlName + "_txtValue"]).Value = DateTime.Now;
                    }

                }
                if (this.DataType == "DataSource"||this.DataType== "HeadDataShow")
                {
                    string tmp = Common.WebServiceHelper.GetString(Org, WebServiceUrl, this.DataShowSQL.Replace("?", dr[DataKey].ToString()), new Dictionary<string, string>());
                    if (!string.IsNullOrEmpty(tmp))
                    {
                        ((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).Text = tmp;
                    }
                    else
                    {
                        ((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).Text = Value;
                    }
                }
                

                if ((this.DataType == "String" || this.DataType == "Int" || this.DataType == "Float" || this.DataType == "DataSource")
                && ((this.Status == "Add" && this.Add) || (this.Status == "Edit" && this.Edit)))
                {
                    if (string.IsNullOrEmpty(((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).Text.Trim())
                    && !this.IsNull)
                    {
                        ((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).BackColor = Color.Red;
                    }
                    else
                    {
                        if (this.DataType == "String" || this.DataType == "Int" || this.DataType == "Float")
                            ((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).BackColor = Color.White;
                        if (this.DataType == "DataSource")
                            ((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).BackColor = Color.LightYellow;
                    }
                }


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SetDefaultValueData(System.Data.DataRow dr)
        {
            try
            {
                if (dr.Table.Columns.Contains(DefaultValue))
                {
                    this.Value = dr[DefaultValue].ToString();
                }
                
                else
                {
                    if (this.DataType == "Date" || this.DataType =="Time" || this.DataType=="DateTime")
                    {
                        this.Value = DateTime.Now.ToString(this.DateTimeFormat);
                    }
                    else
                    {
                        this.Value = DefaultValue;
                    }
                    
                }
                if(this.DataType== "SYSUserCode")
                {
                    ((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).Text = Program.Org.User.UserCode;
                    this.Value= Program.Org.User.UserCode;
                }

           
                if (this.DataType == "String" || this.DataType == "Int" || this.DataType == "Float")
                {
                    ((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).Text = Value;

                }
                if (this.DataType == "Bool")
                {
                    if (string.IsNullOrEmpty(Value))
                    {
                        ((ComboBox)this.Control.Controls[this.ControlName + "_txtValue"]).Text = string.Empty;
                        ((ComboBox)this.Control.Controls[this.ControlName + "_txtValue"]).SelectedIndex = -1;
                    }
                    else if (Value.ToLower() == "true")
                    {
                        ((ComboBox)this.Control.Controls[this.ControlName + "_txtValue"]).SelectedIndex = 0;
                    }
                    else if (Value.ToLower() == "false")
                    {
                        ((ComboBox)this.Control.Controls[this.ControlName + "_txtValue"]).SelectedIndex = 1;
                    }
                }
                if (this.DataType == "Enum")
                {

                    ((ComboBox)this.Control.Controls[this.ControlName + "_txtValue"]).Text = string.Empty;

                    foreach (string key in DataEnum.Keys)
                    {
                        if (DataEnum[key] == Value)
                        {
                            ((ComboBox)this.Control.Controls[this.ControlName + "_txtValue"]).Text = key;
                        }
                    }

                }
                if (this.DataType == "DataSource" || this.DataType == "HeadDataShow")
                {

                    ((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).Text = Common.WebServiceHelper.GetString(Org,WebServiceUrl, this.DataShowSQL.Replace("?", Value), new Dictionary<string, string>());
                    if(this.DataType == "HeadDataShow")
                        this.Value = ((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).Text;
                    
                }
                


                if ((this.DataType == "String" || this.DataType == "Int" || this.DataType == "Float" || this.DataType == "DataSource")
                && ((this.Status == "Add" && this.Add) || (this.Status == "Edit" && this.Edit)))
                {
                    if (string.IsNullOrEmpty(((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).Text.Trim())
                    && !this.IsNull)
                    {
                        ((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).BackColor = Color.Red;
                    }
                    else
                    {
                        if (this.DataType == "String" || this.DataType == "Int" || this.DataType == "Float")
                            ((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).BackColor = Color.White;
                        if (this.DataType == "DataSource")
                            ((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).BackColor = Color.LightYellow;
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SetData(string Value)
        {
            this.Value = Value;
            ((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).Text = Value;
            if (this.DataType == "String" || this.DataType == "Int" || this.DataType == "Float" || this.DataType == "OtherData")
            {
                ((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).Text = Value;


            }
            if (this.DataType == "Bool")
            {
                if (string.IsNullOrEmpty(Value))
                {
                    ((ComboBox)this.Control.Controls[this.ControlName + "_txtValue"]).Text = string.Empty;
                    ((ComboBox)this.Control.Controls[this.ControlName + "_txtValue"]).SelectedIndex = -1;
                }
                else if (Value.ToLower() == "true")
                {
                    ((ComboBox)this.Control.Controls[this.ControlName + "_txtValue"]).SelectedIndex = 0;
                }
                else if (Value.ToLower() == "false")
                {
                    ((ComboBox)this.Control.Controls[this.ControlName + "_txtValue"]).SelectedIndex = 1;
                }
            }
            if (this.DataType == "Enum")
            {

                ((ComboBox)this.Control.Controls[this.ControlName + "_txtValue"]).Text = string.Empty;
                foreach (string key in DataEnum.Keys)
                {
                    if (DataEnum[key] == Value)
                    {
                        ((ComboBox)this.Control.Controls[this.ControlName + "_txtValue"]).Text = key;
                    }
                }
            }
            if (this.DataType == "DataSource" || this.DataType == "HeadDataShow")
            {
                ((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).Text = Common.WebServiceHelper.GetString(Org, WebServiceUrl, this.DataShowSQL.Replace("?", Value), new Dictionary<string, string>());
                if (this.DataType == "HeadDataShow")
                    this.Value = ((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).Text;
            }
            



            if ((this.DataType == "String" || this.DataType == "Int" || this.DataType == "Float" || this.DataType == "DataSource")
                && ((this.Status == "Add" && this.Add) || (this.Status == "Edit" && this.Edit)))
            {
                if (string.IsNullOrEmpty(((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).Text.Trim())
                && !this.IsNull)
                {
                    ((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).BackColor = Color.Red;
                }
                else
                {
                    if (this.DataType == "String" || this.DataType == "Int" || this.DataType == "Float")
                        ((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).BackColor = Color.White;
                    if (this.DataType == "DataSource")
                        ((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).BackColor = Color.LightYellow;
                }
            }

        }

        public void SetStatus(string Status)
        {
            this.Status = Status;
            switch (Status)
            {
                case "Normal":
                    if (this.DataType == "String" || this.DataType == "Int" || this.DataType == "Float" || this.DataType == "DataSource"
                        || this.DataType == "OtherData")
                    {
                        ((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).BackColor = SystemColors.Control;

                        ((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).ReadOnly = true;
                    }
                    else if(this.DataType =="Bool" || this.DataType =="Enum")
                    {
                        ((ComboBox)this.Control.Controls[this.ControlName + "_txtValue"]).Enabled = false;
                    }
                    else if (this.DataType == "Date" || this.DataType == "Time" || this.DataType =="DateTime")
                    {
                        ((DateTimePicker)this.Control.Controls[this.ControlName + "_txtValue"]).Enabled = false;
                    }
                    break;
                case "Add":
                   
                    if (this.Add)
                    {
                        if (this.DataType == "String" || this.DataType == "Int" || this.DataType == "Float" || this.DataType == "DataSource")
                        {
                            ((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).Text = string.Empty;

                            if (this.DataType == "DataSource")
                            {

                                ((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).BackColor = System.Drawing.Color.LightYellow;
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).Text.Trim())
                   && !this.IsNull)
                                {
                                    ((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).BackColor = Color.Red;
                                    ((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).ReadOnly = false;
                                }
                                else
                                {
                                    ((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).BackColor = Color.White;
                                    ((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).ReadOnly = false;
                                }
                            }
                            
                            
                        }

                        else if (this.DataType == "Bool" || this.DataType == "Enum")
                        {
                           
                            ((ComboBox)this.Control.Controls[this.ControlName + "_txtValue"]).Enabled = true;
                        }
                        else if (this.DataType == "Date" || this.DataType == "Time" || this.DataType == "DateTime")
                        {
                           
                            ((DateTimePicker)this.Control.Controls[this.ControlName + "_txtValue"]).Enabled = true;
                        }
                        else if(this.DataType == "OtherData")
                        {
                            ((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).BackColor = SystemColors.Control;

                            ((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).ReadOnly = true;
                        }
                        
                    }
                    else
                    {
                        if (this.DataType == "String" || this.DataType == "Int" || this.DataType == "Float" || this.DataType == "DataSource"
                        || this.DataType == "OtherData")
                        {
                            ((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).BackColor = SystemColors.Control;

                            ((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).ReadOnly = true;
                        }
                        else if (this.DataType == "Bool" || this.DataType == "Enum")
                        {
                            ((ComboBox)this.Control.Controls[this.ControlName + "_txtValue"]).Enabled = false;
                        }
                        else if (this.DataType == "Date" || this.DataType == "Time" || this.DataType == "DateTime")
                        {
                            ((DateTimePicker)this.Control.Controls[this.ControlName + "_txtValue"]).Enabled = false;
                        }
                    }
                    
                    break;
                case "Edit":
                    if (this.Edit)
                    {
                        if (this.DataType == "String" || this.DataType == "Int" || this.DataType == "Float" || this.DataType == "DataSource")
                        {
                            if (this.DataType == "DataSource")
                            {
                                ((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).BackColor = System.Drawing.Color.LightYellow;
                            }
                            else
                            {
                                ((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).BackColor = Color.White;
                                ((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).ReadOnly = false;
                            }
                        }
                        else if (this.DataType == "Bool" || this.DataType == "Enum")
                        {
                            
                            ((ComboBox)this.Control.Controls[this.ControlName + "_txtValue"]).Enabled = true;
                        }
                        else if (this.DataType == "Date" || this.DataType == "Time" || this.DataType == "DateTime")
                        {
                            
                            ((DateTimePicker)this.Control.Controls[this.ControlName + "_txtValue"]).Enabled = true;
                        }
                        else if(this.DataType == "OtherData")
                        {
                            ((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).BackColor = SystemColors.Control;

                            ((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).ReadOnly = true;
                        }
                    }
                    else
                    {
                        if (this.DataType == "String" || this.DataType == "Int" || this.DataType == "Float" || this.DataType == "DataSource"
                       || this.DataType == "OtherData")
                        {
                            ((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).BackColor = SystemColors.Control;

                            ((TextBox)this.Control.Controls[this.ControlName + "_txtValue"]).ReadOnly = true;
                        }
                        else if (this.DataType == "Bool" || this.DataType == "Enum")
                        {
                            ((ComboBox)this.Control.Controls[this.ControlName + "_txtValue"]).Enabled = false;
                        }
                        else if (this.DataType == "Date" || this.DataType == "Time" || this.DataType == "DateTime")
                        {
                            ((DateTimePicker)this.Control.Controls[this.ControlName + "_txtValue"]).Enabled = false;
                        }
                    }
                    
                    break;
            }

        }
    }
}
