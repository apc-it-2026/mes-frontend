using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SJeMES_Framework.Common
{
    public class UIHelper
    {
        static Dictionary<string, Internationalization> Data = new Dictionary<string, Internationalization>();
      //static Dictionary<string, string> PP = new Dictionary<string, string>();
        static string text = string.Empty;
        static int N = 0;
        static DataTable dt_SJQDMS_UILAN = new DataTable();
        public static void GetAllUIInfo(SJeMES_Framework.Class.ClientClass Client,string WebServiceUrl,string Form)
        {
            Data = new Dictionary<string, Internationalization>();
            try
            {

                Dictionary<string, string> P = new Dictionary<string, string>();
                P.Add("ui_tittle", Form);
                string retdata = string.Empty;
                bool IsSuccess = true;
                string strDT = string.Empty;
                string ErrMsg = string.Empty;
                if (string.IsNullOrEmpty(Client.APIURL))
                {
                    retdata = WebServiceHelper.RunService(WebServiceUrl,
                        "SJEMS_API", "SJEMS_API.SJQMS_API",
                        "GetAllUIInfo", P);
                    IsSuccess = Convert.ToBoolean(Common.StringHelper.GetDataFromFirstTag(retdata, "<IsSuccess>", "</IsSuccess>"));
                    if (IsSuccess)
                    {
                        string retData = Common.StringHelper.GetDataFromFirstTag(retdata, "<RetData>", "</RetData>");
                        strDT = Common.StringHelper.GetDataFromFirstTag(retData, "<DataTable>", "</DataTable>");
                    }
                }
                else
                {
                    retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Client.APIURL,
              "SJ_SYSAPI", "SJ_SYSAPI.UIHelper", "GetAllUIInfo", Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(P));
                   var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
                    IsSuccess = Convert.ToBoolean(j["IsSuccess"].ToString());
                    if (IsSuccess)
                    {
                       strDT = j["RetData"].ToString();

                    }
                    else
                    {
                        ErrMsg = j["ErrMsg"].ToString();
                    }
                }

               

                // bool IsSuccess = Convert.ToBoolean(WebServiceHelper.GetDataFromFirstTag(XML, "<IsSuccess>", "</IsSuccess>"));
                
                if (IsSuccess)
                {
                   
                    //string retData = WebServiceHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>");

                    DataTable DT = new DataTable();
                    DT.Columns.Add("ui_code");
                    DT.Columns.Add("ui_cn");
                    DT.Columns.Add("ui_en");
                    DT.Columns.Add("ui_yn");

                    //string strDT = WebServiceHelper.GetDataFromFirstTag(retData, "<DataTable>", "</DataTable>");
                    
                    string[] s = new string[1];
                    s[0] = "@;";
                    string[] strRow = strDT.Split(s, StringSplitOptions.RemoveEmptyEntries);

                    s = new string[1];
                    s[0] = "@,";
                    foreach (string strTmp in strRow)
                    {
                        DataRow dr = DT.NewRow();
                        dr["ui_code"] = strTmp.Split(s, StringSplitOptions.None)[0];
                        dr["ui_cn"] = strTmp.Split(s, StringSplitOptions.None)[1];
                        dr["ui_en"] = strTmp.Split(s, StringSplitOptions.None)[2];
                        dr["ui_yn"] = strTmp.Split(s, StringSplitOptions.None)[3];
                        DT.Rows.Add(dr);
                    }

                    foreach (DataRow DR in DT.Rows)
                    {
                        Internationalization inter = new Internationalization();
                        inter.SetValue("cn", DR["ui_cn"].ToString());
                        inter.SetValue("en", DR["ui_en"].ToString());
                        inter.SetValue("yn", DR["ui_yn"].ToString());
                        if(!Data.ContainsKey(DR["ui_code"].ToString().Replace("\r", "").Replace("\n", "")))
                        Data.Add(DR["ui_code"].ToString().Replace("\r","").Replace("\n",""), inter);
                    }
                }
                else
                {
                    throw new Exception(ErrMsg);
                }

                
                
            }
            catch(Exception ex)
            {
                MessageBox.Show("[1]"+ex.Message);
            }
        }

        //public static string  GetLanguage(string s, SJeMES_Framework.Class.ClientClass Client,string WebServiceUrl,string Language)
        //{
        //    string ret = s;

        //    if (Data.Keys.Count == 0)
        //    {
        //        GetAllUIInfo(Client, WebServiceUrl);
        //    }

        //    if (Data.ContainsKey(s))
        //    {
        //        if(!string.IsNullOrEmpty(Data[s].GetValue(Language)))
        //        ret = Data[s].GetValue(Language);
        //    }
        //    else if (!string.IsNullOrEmpty(s))
        //    {
        //        Dictionary<string, string> P = new Dictionary<string, string>();
        //        P.Add("ui_code", s);
        //        P.Add("ui_cn", s);
        //        if (!string.IsNullOrEmpty(WebServiceUrl))
        //        {
        //            string XML = WebServiceHelper.RunService(WebServiceUrl,
        //           "SJEMS_API", "SJEMS_API.SJQMS_API",
        //           "SetUIInfo", P);
        //        }
        //        else
        //        {
        //            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Client.APIURL,
        //    "SJ_SYSAPI", "SJ_SYSAPI.UIHelper", "SetUIInfo", Client.UserToken, string.Empty);
        //        }


        //    }

        //    if(string.IsNullOrEmpty(ret))
        //    {
        //        ret = s;
        //    }

        //    return ret;
        //}

        //public static void UIUpdate2( Control control, SJeMES_Framework.Class.ClientClass Client,string WebServiceUrl,string Language)
        //{
        //    try
        //    {
        //        if(Data.Keys.Count ==0)
        //        {
        //            GetAllUIInfo(Client, WebServiceUrl);
        //        }

        //        if (control is ComboBox)
        //        {
        //            ComboBox c = control as ComboBox;

        //            for(int i = 0;i<c.Items.Count;i++)
        //            {
        //                if (Data.ContainsKey(c.Items[i].ToString().Trim().Replace("\r\n", "")))
        //                {
        //                    if (!string.IsNullOrEmpty(Data[c.Items[i].ToString().Trim().Replace("\r\n", "")].GetValue(Language)))
        //                        c.Items[i] = Data[c.Items[i].ToString().Trim().Replace("\r\n", "")].GetValue(Language);
        //                }

        //                else if (!string.IsNullOrEmpty(c.Items[i].ToString()))
        //                {
        //                    Dictionary<string, string> P = new Dictionary<string, string>();
        //                    P.Add("ui_code", c.Items[i].ToString().Trim().Replace("\r\n", ""));
        //                    P.Add("ui_cn", c.Items[i].ToString().Trim().Replace("\r\n", ""));

        //                    // string XML = WebServiceHelper.RunService(
        //                    //"SJQMS_API", "SJQMS_API.SJQMS_API",
        //                    //"SetUIInfo", P);
        //                    if (!string.IsNullOrEmpty(WebServiceUrl))
        //                    {
        //                        string XML = WebServiceHelper.RunService(WebServiceUrl,
        //                       "SJEMS_API", "SJEMS_API.SJQMS_API",
        //                       "SetUIInfo", P);
        //                    }
        //                    else
        //                    {
        //                        string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Client.APIURL,
        //                "SJ_SYSAPI", "SJ_SYSAPI.UIHelper", "SetUIInfo", Client.UserToken, string.Empty);
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (Data.ContainsKey(control.Text.Trim().Replace("\r\n", "")))
        //            {
        //                if (!string.IsNullOrEmpty(Data[control.Text.Trim().Replace("\r\n", "")].GetValue(Language)))
        //                    control.Text = Data[control.Text.Trim().Replace("\r\n", "")].GetValue(Language);
        //            }
        //            else if (!string.IsNullOrEmpty(control.Text.Trim()))
        //            {
        //                Dictionary<string, string> P = new Dictionary<string, string>();
        //                P.Add("ui_code", control.Text.Trim().Replace("\r\n", ""));
        //                P.Add("ui_cn", control.Text.Trim().Replace("\r\n", ""));

        //                if (!string.IsNullOrEmpty(WebServiceUrl))
        //                {
        //                    string XML = WebServiceHelper.RunService(WebServiceUrl,
        //                   "SJEMS_API", "SJEMS_API.SJQMS_API",
        //                   "SetUIInfo", P);
        //                }
        //                else
        //                {
        //                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Client.APIURL,
        //            "SJ_SYSAPI", "SJ_SYSAPI.UIHelper", "SetUIInfo", Client.UserToken, string.Empty);
        //                }
        //            }



        //            foreach (Control c in control.Controls)
        //            {

        //                if (c.Controls.Count > 0)
        //                {
        //                    UIUpdate2(c, Client, WebServiceUrl, Language);
        //                }
        //                if (c is ComboBox)
        //                {
        //                    ComboBox cc = c as ComboBox;

        //                    for (int i = 0; i < cc.Items.Count; i++)
        //                    {
        //                        if (Data.ContainsKey(cc.Items[i].ToString().Trim().Replace("\r\n", "")))
        //                        {
        //                            if (!string.IsNullOrEmpty(Data[cc.Items[i].ToString().Trim().Replace("\r\n", "")].GetValue(Language)))
        //                                cc.Items[i] = Data[cc.Items[i].ToString().Trim().Replace("\r\n", "")].GetValue(Language);
        //                        }
        //                        else if (!string.IsNullOrEmpty(cc.Items[i].ToString().Trim().Replace("\r\n", "")))
        //                        {
        //                            Dictionary<string, string> P = new Dictionary<string, string>();
        //                            P.Add("ui_code", cc.Items[i].ToString().Trim().Replace("\r\n", ""));
        //                            P.Add("ui_cn", cc.Items[i].ToString().Trim().Replace("\r\n", ""));

        //                            if (!string.IsNullOrEmpty(WebServiceUrl))
        //                            {
        //                                string XML = WebServiceHelper.RunService(WebServiceUrl,
        //                               "SJEMS_API", "SJEMS_API.SJQMS_API",
        //                               "SetUIInfo", P);
        //                            }
        //                            else
        //                            {
        //                                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Client.APIURL,
        //                        "SJ_SYSAPI", "SJ_SYSAPI.UIHelper", "SetUIInfo", Client.UserToken, string.Empty);
        //                            }
        //                        }
        //                    }
        //                }
        //                else if (Data.ContainsKey(c.Text.Replace("\r\n","")))
        //                {
        //                    if (!string.IsNullOrEmpty(Data[c.Text.Replace("\r\n", "")].GetValue(Language)))
        //                        c.Text = Data[c.Text.Replace("\r\n", "")].GetValue(Language);
        //                }
        //                else if (!string.IsNullOrEmpty(c.Text.Trim().Replace("\r\n", "")))
        //                {
        //                    Dictionary<string, string> P = new Dictionary<string, string>();
        //                    P.Add("ui_code", c.Text.Trim().Replace("\r\n", ""));
        //                    P.Add("ui_cn", c.Text.Trim().Replace("\r\n", ""));

        //                    if (!string.IsNullOrEmpty(WebServiceUrl))
        //                    {
        //                        string XML = WebServiceHelper.RunService(WebServiceUrl,
        //                       "SJEMS_API", "SJEMS_API.SJQMS_API",
        //                       "SetUIInfo", P);
        //                    }
        //                    else
        //                    {
        //                        string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Client.APIURL,
        //                "SJ_SYSAPI", "SJ_SYSAPI.UIHelper", "SetUIInfo", Client.UserToken, string.Empty);
        //                    }
        //                }

        //            }
        //        }

        //    }
        //    catch(Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}


        public static string UIUpdatenew(string Form, Control control, SJeMES_Framework.Class.ClientClass Client, string WebServiceUrl, string Language)
        {
            //TXTHelper.WriteLine("debug.txt", "UIUpdatenew 开始");

            try
            {
                if (Data == null || Data.Keys.Count == 0)
                {
                    GetAllUIInfo(Client, WebServiceUrl, Form);
                }
                if (dt_SJQDMS_UILAN == null || dt_SJQDMS_UILAN.Rows.Count == 0)
                {
                    string sql = @"
                                SELECT 
                                ui_tittle AS '功能名称',
                                ui_code AS '控件ID',
                                ui_id as '控件名称',
                                ui_cn AS '中文名称',
                                ui_en AS '英语名称',
                                ui_yn AS '越语名称'
                                FROM SJQDMS_UILAN where ui_tittle='" + Form + "'";
                    string URL = string.Empty;
                    dt_SJQDMS_UILAN = new DataTable();
                    if (!string.IsNullOrEmpty(WebServiceUrl))
                        dt_SJQDMS_UILAN = SJeMES_Framework.Common.WebServiceHelper.GetDataTable(WebServiceUrl, sql, new Dictionary<string, string>());
                    else
                    {
                        dt_SJQDMS_UILAN = Client.SYSGetDataTable(sql);
                    }
                }

                //MessageBox.Show("dt_SJQDMS_UILAN："+dt_SJQDMS_UILAN.Rows.Count);

                #region 右键菜单多语言 
                foreach (Control c in control.Controls)
                {
                    if (c.ContextMenuStrip is ContextMenuStrip)
                    {
                        ContextMenuStrip cr = c.ContextMenuStrip;

                        #region 保存菜单内容
                        if (cr.Items.Count > 0)
                        {
                            foreach (ToolStripItem items in cr.Items)
                            {
                                string fItem = items.Text;
                                if (string.IsNullOrEmpty(text))
                                    text = cr.Name + "#" + items.Name.Trim() + "#" + items.Text.Trim();
                                else
                                    text += "@@" + cr.Name + "#" + items.Name.Trim() + "#" + items.Text.Trim();

                                //判断是否有子菜单
                                if (items is ToolStripMenuItem)
                                {
                                    ToolStripMenuItem ChildMenu = (ToolStripMenuItem)items;
                                    if (ChildMenu.HasDropDownItems)
                                    {
                                        //如果有子菜单
                                        foreach (ToolStripMenuItem m in ChildMenu.DropDownItems)
                                        {
                                            if (string.IsNullOrEmpty(text))
                                                text = cr.Name + "#" + m.Name.Trim() + "#" + m.Text.Trim();
                                            else
                                                text += "@@" + cr.Name + "#" + m.Name.Trim() + "#" + m.Text.Trim();
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        #region 赋值菜单多语言
                        if (cr.Items.Count > 0)
                        {
                            DataRow[] drs = null;
                            if (dt_SJQDMS_UILAN != null && dt_SJQDMS_UILAN.Rows.Count > 0)
                                drs = dt_SJQDMS_UILAN.Select("功能名称='" + Form + "' and 控件ID='" + cr.Name + "'");

                            if (drs != null && drs.Length > 0)
                            {
                                foreach (ToolStripItem items in cr.Items)
                                {
                                    string fItem = items.Name.Trim();
                                    foreach (DataRow dr in drs)
                                    {
                                        if (fItem.Equals(dr["控件名称"].ToString()))
                                        {
                                            string cn_name = dr["中文名称"].ToString();
                                            string en_name = dr["英语名称"].ToString();
                                            string hk_name = dr["越语名称"].ToString();

                                            //赋值一级菜单 
                                            if (Language.Equals("cn") && !string.IsNullOrEmpty(cn_name))
                                                items.Text = dr["中文名称"].ToString();
                                            else if (Language.Equals("en") && !string.IsNullOrEmpty(en_name))
                                                items.Text = dr["英语名称"].ToString();
                                            else if (Language.Equals("hk") && !string.IsNullOrEmpty(hk_name))
                                                items.Text = dr["越语名称"].ToString();

                                            //判断是否有子菜单
                                            if (items is ToolStripMenuItem)
                                            {
                                                #region 赋值子级菜单
                                                ToolStripMenuItem ChildMenu = (ToolStripMenuItem)items;
                                                if (ChildMenu.HasDropDownItems)
                                                {
                                                    //如果有子菜单
                                                    foreach (ToolStripMenuItem m in ChildMenu.DropDownItems)
                                                    {
                                                        foreach (DataRow drr in drs)
                                                        {  
                                                            if (m.Name.Equals(drr["控件名称"].ToString()))
                                                            {
                                                                if (Language.Equals("cn") && !string.IsNullOrEmpty(drr["中文名称"].ToString()))
                                                                    m.Text = drr["中文名称"].ToString();
                                                                else if (Language.Equals("en") && !string.IsNullOrEmpty(drr["英语名称"].ToString()))
                                                                    m.Text = drr["英语名称"].ToString();
                                                                else if (Language.Equals("hk") && !string.IsNullOrEmpty(drr["越语名称"].ToString()))
                                                                    m.Text = drr["越语名称"].ToString();
                                                            }
                                                        }
                                                    }
                                                }
                                                #endregion
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                    }
                }
                #endregion

                #region ComboBox
                //if (control is ComboBox)
                //{
                //    ComboBox c = control as ComboBox;

                //    for (int i = 0; i < c.Items.Count; i++)
                //    {
                //        if (Data.ContainsKey(c.Items[i].ToString().Trim().Replace("\r\n", "")))
                //        {
                //            if (!string.IsNullOrEmpty(Data[c.Items[i].ToString().Trim().Replace("\r\n", "")].GetValue(Language)))
                //                c.Items[i] = Data[c.Items[i].ToString().Trim().Replace("\r\n", "")].GetValue(Language);
                //        }
                //        else if (!string.IsNullOrEmpty(c.Items[i].ToString()))
                //        {
                //            if (string.IsNullOrEmpty(text))
                //            {
                //                text = control.Name + "#" + c.Items[i].ToString().Trim().Replace("\r\n", "").Trim();
                //            }
                //            else
                //            {
                //                text += "@@" + control.Name + "#" + c.Items[i].ToString().Trim().Replace("\r\n", "").Trim();
                //            }
                //        }

                //        DataRow[] dr = dt_SJQDMS_UILAN.Select("控件名称='" + c.Items[i].ToString().Trim().Replace("\r\n", "") + "'");
                //        if (dr.Length > 0 && Language == "cn")
                //            c.Items[i] = !string.IsNullOrEmpty(dr[0]["中文名称"].ToString()) ? dr[0]["中文名称"].ToString() : c.Items[i].ToString().Trim().Replace("\r\n", "");// 
                //        if (dr.Length > 0 && Language == "en")
                //            c.Items[i] = !string.IsNullOrEmpty(dr[0]["英语名称"].ToString()) ? dr[0]["英语名称"].ToString() : c.Items[i].ToString().Trim().Replace("\r\n", "");// 
                //        if (dr.Length > 0 && Language == "hk")
                //            c.Items[i] = !string.IsNullOrEmpty(dr[0]["越语名称"].ToString()) ? dr[0]["越语名称"].ToString() : c.Items[i].ToString().Trim().Replace("\r\n", "");
                //    }
                //}
                #endregion

                #region DataGridView
                if (control is DataGridView)
                { 
                    DataGridView c = control as DataGridView;

                    for (int i = 0; i < c.Columns.Count; i++)
                    {
                        if(c.Columns[i].Visible)
                        {
                            if (Data.ContainsKey(c.Columns[i].ToString().Trim().Replace("\r\n", "")))
                            {
                                if (!string.IsNullOrEmpty(Data[c.Columns[i].HeaderText.ToString().Trim().Replace("\r\n", "")].GetValue(Language)))
                                    c.Columns[i].HeaderText = Data[c.Columns[i].HeaderText.ToString().Trim().Replace("\r\n", "")].GetValue(Language);
                            }
                            else if (!string.IsNullOrEmpty(c.Columns[i].HeaderText.ToString()))
                            {
                                if (string.IsNullOrEmpty(text))
                                    text = control.Name + "#" + c.Columns[i].Name.Trim().Replace("\r\n", "") + "#" + c.Columns[i].HeaderText.Trim().Replace("\r\n", "");
                                else
                                    text += "@@" + control.Name +"#"+ c.Columns[i].Name.Trim().Replace("\r\n", "") + "#" + c.Columns[i].HeaderText.Trim().Replace("\r\n", "");
                            }
                            DataRow[] dr = null;
                            if (dt_SJQDMS_UILAN != null && dt_SJQDMS_UILAN.Rows.Count > 0)
                                dr = dt_SJQDMS_UILAN.Select("控件名称='" + c.Columns[i].Name.ToString().Trim().Replace("\r\n", "") + "'");
                            if (dr!=null && dr.Length > 0 && Language == "cn")
                                c.Columns[i].HeaderText = !string.IsNullOrEmpty(dr[0]["中文名称"].ToString()) ? dr[0]["中文名称"].ToString() : c.Columns[i].HeaderText.ToString().Trim().Replace("\r\n", "");// 
                            if (dr != null && dr.Length > 0 && Language == "en")
                                c.Columns[i].HeaderText = !string.IsNullOrEmpty(dr[0]["英语名称"].ToString()) ? dr[0]["英语名称"].ToString() : c.Columns[i].HeaderText.ToString().Trim().Replace("\r\n", "");// 
                            if (dr != null && dr.Length > 0 && Language == "hk")
                                c.Columns[i].HeaderText = !string.IsNullOrEmpty(dr[0]["越语名称"].ToString()) ? dr[0]["越语名称"].ToString() : c.Columns[i].HeaderText.ToString().Trim().Replace("\r\n", "");
                        }
                    }
                     
                }
                #endregion

                else
                { 

                    #region 窗体名称多语言
                    DataRow[] drsT = null;
                    if (dt_SJQDMS_UILAN != null && dt_SJQDMS_UILAN.Rows.Count > 0)
                        drsT = dt_SJQDMS_UILAN.Select("功能名称='" + control.Name + "' and 控件名称='" + control.Text + "'");
                    if (drsT !=null && drsT.Length > 0)
                    {
                        string cn_name = drsT[0]["中文名称"].ToString();
                        string en_name = drsT[0]["英语名称"].ToString();
                        string hk_name = drsT[0]["越语名称"].ToString();

                        //赋值一级菜单 
                        if (Language.Equals("cn") && !string.IsNullOrEmpty(cn_name))
                            control.Text = cn_name;
                        else if (Language.Equals("en") && !string.IsNullOrEmpty(en_name))
                            control.Text = en_name;
                        else if (Language.Equals("hk") && !string.IsNullOrEmpty(hk_name))
                            control.Text = hk_name;
                    }
                    #endregion

                    if (Data.ContainsKey(control.Name))
                    {
                        if (!string.IsNullOrEmpty(Data[control.Name].GetValue(Language)))
                            control.Text = Data[control.Name].GetValue(Language);
                    }

                    if (Data.ContainsKey(Form + "." + control.Name))
                    {
                        if (!string.IsNullOrEmpty(Data[Form + "." + control.Name].GetValue(Language)))
                            control.Text = Data[Form + "." + control.Name].GetValue(Language);
                    }
                    else if (!string.IsNullOrEmpty(control.Text.Trim()) && !(control is TextBox) && !(control is RichTextBox) && !(control is ComboBox))
                    { 
                        if (string.IsNullOrEmpty(text))
                            text = control.Name + "#" + control.Name.Trim() + "#" + control.Text.Trim();
                        else
                            text += "@@" + control.Name + "#" + control.Name.Trim() + "#" + control.Text.Trim();

                    }

                    foreach (Control c in control.Controls)
                    {
                        if (c.Controls.Count > 0 && !c.Name.Contains("dataGridView"))
                        {
                            UIUpdatenew(Form, c, Client, WebServiceUrl, Language);
                        }

                        #region ComboBox 
                        //if (c is ComboBox)
                        //{
                        //    ComboBox cc = c as ComboBox;
                        //    DataTable dtcom = new DataTable();
                        //    string value = cc.ValueMember;
                        //    string name = cc.DisplayMember;
                        //    dtcom.Columns.Add(cc.DisplayMember);
                        //    dtcom.Columns.Add(cc.ValueMember);
                        //    //stopwatch.Start();
                        //    for (int i = 0; i < cc.Items.Count; i++)
                        //    {
                        //        if (string.IsNullOrEmpty(cc.ValueMember))
                        //        {
                        //            if (Data.ContainsKey(cc.Items[i].ToString().Trim().Replace("\r\n", "")))
                        //            {
                        //                if (!string.IsNullOrEmpty(Data[cc.Items[i].ToString().Trim().Replace("\r\n", "")].GetValue(Language)))
                        //                    cc.Items[i] = Data[cc.Items[i].ToString().Trim().Replace("\r\n", "")].GetValue(Language);
                        //            }
                        //            else if (!string.IsNullOrEmpty(cc.Items[i].ToString().Trim().Replace("\r\n", "")))
                        //            {
                        //                if (string.IsNullOrEmpty(text))
                        //                {
                        //                    text = c.Name + "#" + cc.Items[i].ToString().Trim().Replace("\r\n", "");
                        //                }
                        //                else
                        //                {
                        //                    text += "@@" + c.Name + "#" + cc.Items[i].ToString().Trim().Replace("\r\n", "");
                        //                }
                        //            }
                        //            if (dt_SJQDMS_UILAN != null)
                        //            {
                        //                DataRow[] dr = dt_SJQDMS_UILAN.Select("控件名称='" + cc.Items[i].ToString().Trim().Replace("\r\n", "") + "'");
                        //                if (dr.Length > 0 && Language == "cn")
                        //                    cc.Items[i] = !string.IsNullOrEmpty(dr[0]["中文名称"].ToString()) ? dr[0]["中文名称"].ToString() : cc.Items[i].ToString().Trim().Replace("\r\n", "");// && Program.Client.Language != "en"
                        //                if (dr.Length > 0 && Language == "en")
                        //                    cc.Items[i] = !string.IsNullOrEmpty(dr[0]["英语名称"].ToString()) ? dr[0]["英语名称"].ToString() : cc.Items[i].ToString().Trim().Replace("\r\n", "");// && Program.Client.Language != "en"
                        //                if (dr.Length > 0 && Language == "hk")
                        //                    cc.Items[i] = !string.IsNullOrEmpty(dr[0]["越语名称"].ToString()) ? dr[0]["越语名称"].ToString() : cc.Items[i].ToString().Trim().Replace("\r\n", "");
                        //            }
                        //        }
                        //        else
                        //        {

                        //            if (Data.ContainsKey(cc.GetItemText(cc.Items[i]).Trim().Replace("\r\n", "")))
                        //            {
                        //                if (!string.IsNullOrEmpty(Data[cc.GetItemText(cc.Items[i]).Trim().Replace("\r\n", "")].GetValue(Language)))
                        //                    cc.Items[i] = Data[cc.GetItemText(cc.Items[i]).Trim().Replace("\r\n", "")].GetValue(Language);
                        //            }
                        //            else if (!string.IsNullOrEmpty(cc.GetItemText(cc.Items[i]).Trim().Replace("\r\n", "")))
                        //            {
                        //                if (string.IsNullOrEmpty(text))
                        //                {
                        //                    text = c.Name + "#" + cc.GetItemText(cc.Items[i]).Trim().Replace("\r\n", "");
                        //                }
                        //                else
                        //                {
                        //                    text += "@@" + c.Name + "#" + cc.GetItemText(cc.Items[i]).Trim().Replace("\r\n", "");
                        //                }
                        //            }
                        //            if (dt_SJQDMS_UILAN != null)
                        //            {
                        //                DataRow[] dr = dt_SJQDMS_UILAN.Select("控件名称='" + cc.GetItemText(cc.Items[i]).Trim().Replace("\r\n", "") + "'");
                        //                if (dr.Length > 0 && Language == "cn")
                        //                {
                        //                    cc.SelectedIndex = i;
                        //                    DataRow drcom = dtcom.NewRow();
                        //                    drcom[0] = !string.IsNullOrEmpty(dr[0]["中文名称"].ToString()) ? dr[0]["中文名称"].ToString() : cc.GetItemText(cc.Items[i]).Trim().Replace("\r\n", "");
                        //                    drcom[1] = cc.SelectedValue;
                        //                    dtcom.Rows.Add(drcom);
                        //                }
                        //                if (dr.Length > 0 && Language == "en")
                        //                {
                        //                    cc.SelectedIndex = i;
                        //                    DataRow drcom = dtcom.NewRow();
                        //                    drcom[0] = !string.IsNullOrEmpty(dr[0]["英语名称"].ToString()) ? dr[0]["英语名称"].ToString() : cc.GetItemText(cc.Items[i]).Trim().Replace("\r\n", "");
                        //                    drcom[1] = cc.SelectedValue;
                        //                    dtcom.Rows.Add(drcom);
                        //                }
                        //                //cc.Text=!string.IsNullOrEmpty(dr[0]["英语名称"].ToString()) ? dr[0]["英语名称"].ToString() : cc.GetItemText(cc.Items[i]).Trim().Replace("\r\n", "");
                        //                //cc.SelectedItem= !string.IsNullOrEmpty(dr[0]["英语名称"].ToString()) ? dr[0]["英语名称"].ToString() : cc.GetItemText(cc.Items[i]).Trim().Replace("\r\n", "");// && Program.Client.Language != "en"
                        //                if (dr.Length > 0 && Language == "hk")
                        //                {
                        //                    cc.SelectedIndex = i;
                        //                    DataRow drcom = dtcom.NewRow();
                        //                    drcom[0] = !string.IsNullOrEmpty(dr[0]["越语名称"].ToString()) ? dr[0]["越语名称"].ToString() : cc.GetItemText(cc.Items[i]).Trim().Replace("\r\n", "");
                        //                    drcom[1] = cc.SelectedValue;
                        //                    dtcom.Rows.Add(drcom);
                        //                }
                        //            }

                        //            //cc.Text = !string.IsNullOrEmpty(dr[0]["越语名称"].ToString()) ? dr[0]["越语名称"].ToString() : cc.GetItemText(cc.Items[i]).Trim().Replace("\r\n", "");
                        //        }
                        //    }
                        //    if (dtcom.Rows.Count > 0)
                        //    {
                        //        cc.DataSource = null;
                        //        cc.DataSource = dtcom;
                        //        cc.DisplayMember = name;
                        //        cc.ValueMember = value;
                        //    } 
                        //} 
                        #endregion

                        if (c is DataGridView)
                        { 
                            DataGridView a = c as DataGridView;

                            for (int i = 0; i < a.Columns.Count; i++)
                            {
                                if(a.Columns[i].Visible)
                                {
                                    if (Data.ContainsKey(a.Columns[i].ToString().Trim().Replace("\r\n", "")))
                                    {
                                        if (!string.IsNullOrEmpty(Data[a.Columns[i].HeaderText.ToString().Trim().Replace("\r\n", "")].GetValue(Language)))
                                            a.Columns[i].HeaderText = Data[a.Columns[i].HeaderText.ToString().Trim().Replace("\r\n", "")].GetValue(Language);
                                    }
                                    else if (!string.IsNullOrEmpty(a.Columns[i].HeaderText.ToString()))
                                    {
                                        if (string.IsNullOrEmpty(text))
                                            text = c.Name + "#" + a.Columns[i].Name.Trim().Replace("\r\n", "") + "#" + a.Columns[i].HeaderText.Trim().Replace("\r\n", "");
                                        else
                                            text += "@@" + c.Name + "#" + a.Columns[i].Name.Trim().Replace("\r\n", "") + "#" + a.Columns[i].HeaderText.Trim().Replace("\r\n", "");
                                    }
                                    if (dt_SJQDMS_UILAN != null && dt_SJQDMS_UILAN.Rows.Count>0)
                                    {
                                        DataRow[] dr = dt_SJQDMS_UILAN.Select("控件名称='" + a.Columns[i].Name.ToString().Trim().Replace("\r\n", "") + "'");
                                        if (dr.Length > 0 && Language == "cn")
                                            a.Columns[i].HeaderText = !string.IsNullOrEmpty(dr[0]["中文名称"].ToString()) ? dr[0]["中文名称"].ToString() : a.Columns[i].HeaderText.ToString().Trim().Replace("\r\n", "");// 
                                        if (dr.Length > 0 && Language == "en")
                                            a.Columns[i].HeaderText = !string.IsNullOrEmpty(dr[0]["英语名称"].ToString()) ? dr[0]["英语名称"].ToString() : a.Columns[i].HeaderText.ToString().Trim().Replace("\r\n", "");// 
                                        if (dr.Length > 0 && Language == "hk")
                                            a.Columns[i].HeaderText = !string.IsNullOrEmpty(dr[0]["越语名称"].ToString()) ? dr[0]["越语名称"].ToString() : a.Columns[i].HeaderText.ToString().Trim().Replace("\r\n", "");
                                    }
                                }

                                //查找gridview下的buuton,更改文字  david
                                if (a.Columns[i].GetType() == typeof(DataGridViewButtonColumn))
                                {
                                    var bnt = a.Columns[i] as DataGridViewButtonColumn;
                                    bnt.Text = a.Columns[i].HeaderText;
                                }
                            }
                        }
                        else if (Data.ContainsKey(Form + "." + c.Name))
                        { 
                            if (!string.IsNullOrEmpty(Data[Form + "." + c.Name].GetValue(Language)))
                                c.Text = Data[Form + "." + c.Name].GetValue(Language);
                            if (dt_SJQDMS_UILAN != null && dt_SJQDMS_UILAN.Rows.Count > 0)
                            {
                                DataRow[] dr = dt_SJQDMS_UILAN.Select("控件名称='" + c.Text.Trim() + "'");
                                if (dr.Length > 0 && Language == "cn")
                                    c.Text = !string.IsNullOrEmpty(dr[0]["中文名称"].ToString()) ? dr[0]["中文名称"].ToString() : c.Text.Trim();
                                if (dr.Length > 0 && Language == "en")
                                    c.Text = !string.IsNullOrEmpty(dr[0]["英语名称"].ToString()) ? dr[0]["英语名称"].ToString() : c.Text.Trim();
                                if (dr.Length > 0 && Language == "hk")
                                    c.Text = !string.IsNullOrEmpty(dr[0]["越语名称"].ToString()) ? dr[0]["越语名称"].ToString() : c.Text.Trim();
                            }
                        }
                        else if (!string.IsNullOrEmpty(c.Text.Trim()) && !(c is TextBox) && !(c is RichTextBox) && !(c is ComboBox))
                        {
                            if (string.IsNullOrEmpty(text))
                                text = c.Name + "#" + c.Name.Trim() + "#" + c.Text.Trim();
                            else
                                text += "@@" + c.Name + "#" + c.Name.Trim() + "#" + c.Text.Trim();

                            if (dt_SJQDMS_UILAN != null && dt_SJQDMS_UILAN.Rows.Count > 0)
                            {
                                DataRow[] dr = dt_SJQDMS_UILAN.Select("控件名称='" + c.Name.Trim() + "'");
                                if (dr.Length > 0 && Language == "cn")
                                    c.Text = !string.IsNullOrEmpty(dr[0]["中文名称"].ToString()) ? dr[0]["中文名称"].ToString() : c.Text.Trim();
                                if (dr.Length > 0 && Language == "en")
                                    c.Text = !string.IsNullOrEmpty(dr[0]["英语名称"].ToString()) ? dr[0]["英语名称"].ToString() : c.Text.Trim();
                                if (dr.Length > 0 && Language == "hk")
                                    c.Text = !string.IsNullOrEmpty(dr[0]["越语名称"].ToString()) ? dr[0]["越语名称"].ToString() : c.Text.Trim();
                            }

                        }
                        else if (c.ContextMenuStrip is ContextMenuStrip)
                        {
                            ContextMenuStrip cr = c.ContextMenuStrip;
                            DataRow[] drs = null;
                            if (dt_SJQDMS_UILAN != null && dt_SJQDMS_UILAN.Rows.Count > 0)
                                drs = dt_SJQDMS_UILAN.Select("功能名称='" + Form + "' and 控件ID='" + cr.Name + "'");

                            if (cr.Items.Count > 0)
                            {
                                foreach (ToolStripItem items in cr.Items)
                                {
                                    string fItem = items.Name;
                                    if (string.IsNullOrEmpty(text))
                                        text = cr.Name + "#" + items.Name.Trim() + "#" + items.Text.Trim();
                                    else
                                        text += "@@" + cr.Name + "#" + items.Name.Trim() + "#" + items.Text.Trim();

                                    foreach (DataRow dr in drs)
                                    {
                                        if (fItem.Equals(dr["控件名称"].ToString()))
                                        {
                                            string cn_name = dr["中文名称"].ToString();
                                            string en_name = dr["英语名称"].ToString();
                                            string hk_name = dr["越语名称"].ToString();

                                            //赋值一级菜单 
                                            if (Language.Equals("cn") && !string.IsNullOrEmpty(cn_name))
                                                items.Text = dr["中文名称"].ToString();
                                            else if (Language.Equals("en") && !string.IsNullOrEmpty(en_name))
                                                items.Text = dr["英语名称"].ToString();
                                            else if (Language.Equals("hk") && !string.IsNullOrEmpty(hk_name))
                                                items.Text = dr["越语名称"].ToString();

                                            //判断是否有子菜单
                                            if (items is ToolStripMenuItem)
                                            {
                                                ToolStripMenuItem ChildMenu = (ToolStripMenuItem)items;
                                                if (ChildMenu.HasDropDownItems)
                                                {
                                                    //如果有子菜单
                                                    foreach (ToolStripMenuItem m in ChildMenu.DropDownItems)
                                                    {
                                                        foreach (DataRow drr in drs)
                                                        {
                                                            if (string.IsNullOrEmpty(text))
                                                                text = cr.Name + "#" + m.Name.Trim() + "#" + m.Text.Trim();
                                                            else
                                                                text += "@@" + cr.Name + "#" + m.Name.Trim() + "#" + m.Text.Trim();

                                                            if (m.Name.Equals(drr["控件名称"].ToString()))
                                                            {
                                                                if (Language.Equals("cn") && !string.IsNullOrEmpty(drr["中文名称"].ToString()))
                                                                    m.Text = drr["中文名称"].ToString();
                                                                else if (Language.Equals("en") && !string.IsNullOrEmpty(drr["英语名称"].ToString()))
                                                                    m.Text = drr["英语名称"].ToString();
                                                                else if (Language.Equals("hk") && !string.IsNullOrEmpty(drr["越语名称"].ToString()))
                                                                    m.Text = drr["越语名称"].ToString();
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                } 

            }
            catch (Exception ex)
            {
                MessageBox.Show("[2]" + ex);
            }
            return text;  
        }

        public static void UIUpdate(string Form, Control control, SJeMES_Framework.Class.ClientClass Client, string WebServiceUrl, string Language)
        {
            try
            {
                Data = null;
                dt_SJQDMS_UILAN = null;

                string res = UIUpdatenew(Form,  control,  Client,  WebServiceUrl,  Language);
                

                if (!string.IsNullOrEmpty(res))
                {
                    //st.Start();
                    Dictionary<string, string> PP = new Dictionary<string, string>();
                    PP.Add("ui_tittle", Form);

                    PP.Add("ui_cn", res);
                    if (!string.IsNullOrEmpty(WebServiceUrl))
                    {
                        string XML = WebServiceHelper.RunService(WebServiceUrl,
                       "SJEMS_API", "SJEMS_API.SJQMS_API",
                       "SetUIInfo", PP);
                    }
                    else
                    {
                        string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Client.APIURL,
                         "SJ_SYSAPI", "SJ_SYSAPI.UIHelper", "SetUIInfo", Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(PP));
                    }
                    //st.Stop();
                    //Debug.WriteLine("API 加载时间：" + st.ElapsedMilliseconds.ToString());
                    res = string.Empty;
                    text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("[3]" + ex.Message);
            }
        }


        /// <summary>
        /// 消息多语言
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="Client"></param>
        /// <param name="Language"></param>
        /// <returns></returns>
        public static string UImsg(string msg, SJeMES_Framework.Class.ClientClass Client,string WebServiceUrl, string Language)
        {
            string strDT = string.Empty;
            Data = new Dictionary<string, Internationalization>();
            try
            {

                Dictionary<string, string> P = new Dictionary<string, string>();
                string retdata = string.Empty;
                bool IsSuccess = true;
               
                string ErrMsg = string.Empty;
                Dictionary<string, string> data = new Dictionary<string, string>();
                data.Add("msg", msg);
                data.Add("language", Language);

                if (!string.IsNullOrEmpty(WebServiceUrl))
                {
                    retdata = WebServiceHelper.RunService(WebServiceUrl,
                                    "SJEMS_API", "SJEMS_API.SJQMS_API", "GetLanguage", data);
                    IsSuccess = Convert.ToBoolean(Common.StringHelper.GetDataFromFirstTag(retdata, "<IsSuccess>", "</IsSuccess>"));
                    if (IsSuccess)
                    {
                        strDT = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(
                            Common.StringHelper.GetDataFromFirstTag(retdata, "<RetData>", "</RetData>"))["data"].ToString();
                    }
                }
                else
                {
                    retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Client.APIURL,
                                                  "SJ_SYSAPI", "SJ_SYSAPI.Language", "GetLanguage", Client.UserToken,
                                                  Newtonsoft.Json.JsonConvert.SerializeObject(data));
                    var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
                    IsSuccess = Convert.ToBoolean(j["IsSuccess"].ToString());
                    if (IsSuccess)
                    {
                        strDT = j["RetData"].ToString();
                    }
                    else
                        throw new Exception(j["ErrMsg"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("[4]" + ex.Message);
            }
            return strDT;
        }

        public static Dictionary<string, object> UIListMsg(List<string> lst,SJeMES_Framework.Class.ClientClass Client ,string WebServiceUrl, string Language)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            try
            {
                if(lst.Count>0)
                {
                    string retdata = string.Empty;
                    bool IsSuccess = false;
                    Dictionary<string, object> data = new Dictionary<string, object>();


                    if (!string.IsNullOrEmpty(WebServiceUrl))
                    {
                        data.Add("lstKey", Newtonsoft.Json.JsonConvert.SerializeObject(lst));
                        data.Add("language", Language);
                        retdata = WebServiceHelper.RunService(WebServiceUrl,
                                        "SJEMS_API", "SJEMS_API.SJQMS_API", "GetListLanguage", data);
                        IsSuccess = Convert.ToBoolean(Common.StringHelper.GetDataFromFirstTag(retdata, "<IsSuccess>", "</IsSuccess>"));
                        if (IsSuccess)
                        {
                            Dictionary<string, object> dd = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(
                                Common.StringHelper.GetDataFromFirstTag(retdata, "<RetData>", "</RetData>"));

                            dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(
                                (Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(
                                Common.StringHelper.GetDataFromFirstTag(retdata, "<RetData>", "</RetData>"))["data"].ToString()));
                        }
                    }
                    else
                    {
                        data.Add("lstKey", lst);
                        data.Add("language", Language);
                        retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Client.APIURL,
                                                           "SJ_SYSAPI", "SJ_SYSAPI.Language", "GetListLanguage", Client.UserToken,
                                                           Newtonsoft.Json.JsonConvert.SerializeObject(data));
                        var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
                        IsSuccess = Convert.ToBoolean(j["IsSuccess"].ToString());
                        if (IsSuccess)
                        {
                            dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(j["RetData"].ToString());
                        }
                        else
                            throw new Exception(j["ErrMsg"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("[5]" + ex.Message);
            } 
            return dic;
        }

        /// <summary>
        /// webapi 调用
        /// </summary>
        /// <param name="Form"></param>
        /// <param name="Client"></param>
        /// <param name="Language"></param>
        /// <param name="dgv"></param>
        /// <returns></returns>
        public static string UIdataGridView(string Form, SJeMES_Framework.Class.ClientClass Client, string Language, string WebServiceUrl, params DataGridView[] dgv)
        {
            string strDT = string.Empty;
            Data = new Dictionary<string, Internationalization>();
            try
            {
                #region DataGridView
                for (int a = 0; a < dgv.Length; a++)
                {
                    if (dgv[a].Columns.Count > 0)
                    {
                        DataTable tblDatas = new DataTable("Datas");
                        DataColumn dc = null;
                        dc = tblDatas.Columns.Add("title", Type.GetType("System.String"));
                        for (int i = 0; i < dgv[a].Columns.Count; i++)
                        { 
                            if(dgv[a].Columns[i].Visible)
                            {
                                DataRow newRow;
                                newRow = tblDatas.NewRow();
                                newRow["title"] = dgv[a].Columns[i].Name.Trim() + "&" + dgv[a].Columns[i].Name.Trim();
                                tblDatas.Rows.Add(newRow);
                            }
                        }
                        Dictionary<string, string> P = new Dictionary<string, string>();
                        P.Add("ui_code", dgv[a].Name);
                        P.Add("ui_tittle", Form);
                        P.Add("Language", Language);
                        P.Add("dt", Newtonsoft.Json.JsonConvert.SerializeObject(tblDatas));
                        string retdata = string.Empty;
                        bool IsSuccess = true;
                        string ErrMsg = string.Empty;

                        if (!string.IsNullOrEmpty(WebServiceUrl))
                        {
                            retdata = WebServiceHelper.RunService(WebServiceUrl,
                                            "SJEMS_API", "SJEMS_API.SJQMS_API", "GetDgv", P);
                            IsSuccess = Convert.ToBoolean(Common.StringHelper.GetDataFromFirstTag(retdata, "<IsSuccess>", "</IsSuccess>"));
                            if (IsSuccess)
                            {
                                DataTable redt = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>
                                        (Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(
                                Common.StringHelper.GetDataFromFirstTag(retdata, "<RetData>", "</RetData>"))["dt"].ToString());
                                if (redt.Rows.Count > 0)
                                {
                                    foreach (DataGridViewColumn dgvc in dgv[a].Columns)
                                    {
                                        if(dgvc.Visible)
                                        {
                                            for (int i = 0; i < redt.Rows.Count; i++)
                                            {
                                                if (!string.IsNullOrEmpty(redt.Rows[i]["title"].ToString())
                                                   && redt.Rows[i]["name"].ToString().Equals(dgvc.Name))
                                                    dgvc.HeaderText = redt.Rows[i]["title"].ToString();
                                            }
                                        } 
                                    }
                                }
                            }
                        }
                        else
                        {
                            retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Client.APIURL,
                                         "SJ_SYSAPI", "SJ_SYSAPI.Language", "GetDgv", Client.UserToken, 
                                         Newtonsoft.Json.JsonConvert.SerializeObject(P));
                            var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
                            IsSuccess = Convert.ToBoolean(j["IsSuccess"].ToString());
                            if (IsSuccess)
                            {
                                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>
                            (Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(j["RetData"].ToString())["dt"].ToString());
                                if (dt.Rows.Count > 0)
                                {
                                    foreach (DataGridViewColumn dgvc in dgv[a].Columns)
                                    {
                                        if (dgvc.Visible)
                                        {
                                            for (int i = 0; i < dt.Rows.Count; i++)
                                            {
                                                if (!string.IsNullOrEmpty(dt.Rows[i]["title"].ToString())
                                                   && dt.Rows[i]["name"].ToString().Equals(dgvc.Name))
                                                    dgvc.HeaderText = dt.Rows[i]["title"].ToString();
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                ErrMsg = j["ErrMsg"].ToString();
                            }
                        }
                    }
                }
                
                #endregion
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("[6]" + ex.Message);
            }
            return strDT;
        }


        public static string UIVisiable(Control control, string moduleName, SJeMES_Framework.Class.ClientClass Client)
        {
            string reult = string.Empty;
            try
            {
                string retdata = string.Empty;
                bool IsSuccess = false;

                retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Client.APIURL,
                "SJ_SYSAPI", "SJ_SYSAPI.Role", "getActionCode", Client.UserToken, "{\"title\":\"" + moduleName + "\"}");
                var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
                IsSuccess = Convert.ToBoolean(ret["IsSuccess"].ToString());
                if (IsSuccess)
                {
                    reult = ret["RetData"].ToString();
                    if (!string.IsNullOrEmpty(reult))
                    {
                        //ADMIN 用户有全部的权限
                        if (reult.Equals("ADMIN"))
                        {
                            UIVisiable3(control);
                        }
                        else
                        {
                            for (int i = 0; i < reult.Split(',').Length; i++)
                            {
                                UIVisiable2(control, reult.Split(',')[i]);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("[7]" + ret["ErrMsg"].ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("[8]" + ex.Message);
            }
            return reult;
        }

        private static void UIVisiable2(Control control, string name)
        {
            Control[] ctls = control.Controls.Find("btn" + name, false);
            if (ctls.Length > 0)
            {
                ctls[0].Visible = true;
            }
            foreach (Control c in control.Controls)
            {
                if (c.Controls.Count > 0)
                    UIVisiable2(c, name);
            }
        }

        private static void UIVisiable3(Control control)
        {
            foreach (Control c in control.Controls)
            {
                c.Visible = true; 
                if(c.Controls.Count>0)
                {
                    UIVisiable3(c);
                }
            }
        }

        /// <summary>
        /// 列自适应
        /// </summary>
        /// <param name="dgv"></param>
        public static void LoadDgv(DataGridView dgv)
        {
            int widths = 0;
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                dgv.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);  // 自动调整列宽
                widths += dgv.Columns[i].Width;   // 计算调整列后单元列的宽度和                     
            }
            if (widths >= dgv.Size.Width)  // 如果调整列的宽度大于设定列宽
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;  // 调整列的模式 自动
            else
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;  // 如果小于 则填充
        }

        /// <summary>
        /// 列表项下拉窗口宽度自适应
        /// </summary>
        /// <param name="comboBox"></param>
        public static void AdjustComboBoxDropDownListWidth(object comboBox)
        {
            Graphics g = null;
            Font font = null;
            try
            {
                ComboBox senderComboBox = null;
                if (comboBox is ComboBox)
                    senderComboBox = (ComboBox)comboBox;
                else if (comboBox is ToolStripComboBox)
                    senderComboBox = ((ToolStripComboBox)comboBox).ComboBox;
                else
                    return;

                int width = senderComboBox.Width;
                g = senderComboBox.CreateGraphics();
                font = senderComboBox.Font;

                //checks if a scrollbar will be displayed.
                //If yes, then get its width to adjust the size of the drop down list.
                int vertScrollBarWidth =
                    (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
                    ? SystemInformation.VerticalScrollBarWidth : 0;

                int newWidth;
                foreach (object s in senderComboBox.Items)  //Loop through list items and check size of each items.
                {
                    if (s != null)
                    {
                        newWidth = (int)g.MeasureString(s.ToString().Trim(), font).Width
                            + vertScrollBarWidth;
                        if (width < newWidth)
                            width = newWidth;   //set the width of the drop down list to the width of the largest item.
                    }
                }
                senderComboBox.DropDownWidth = width;
            }
            catch
            { }
            finally
            {
                if (g != null)
                    g.Dispose();
            }
        }

    }
}
