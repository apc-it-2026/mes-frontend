using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutocompleteMenuNS;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using SJeMES_Framework.WebAPI;



namespace MonthlyManPowerUpload
{
    public partial class MonthlyManPowerUpload : MaterialForm
    {
        public string filePath = string.Empty;
        public string SafeFileName = string.Empty;
        string Descipline_Score_Card = string.Empty; 
        ComboBox SkillNames;
        List<string> lt = new List<string>();
        public MonthlyManPowerUpload()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);
            this.WindowState = FormWindowState.Maximized;
        }
        private void MonthlyManPowerUpload_Load(object sender, EventArgs e)
        {
           
            
                textBox2.ReadOnly = true;
                txtprodline.ReadOnly = true;
                textBox1.ReadOnly = true;
                GetDept();
            if(!Check_Packing_Emp())
            {
                dateTimePicker5.Text = DateTime.Now.AddMonths(1).ToString("yyyy/MM");
                if (Check_Model_Upload(dateTimePicker5.Text))
                {
                    GetManPower();
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Model data Is not Uploaded for this line");
                    textBox3.Text = "";
                    dataGridView5.DataSource = null;
                }

            }
            else
            {
                GetManPower();
            }
                
            
            
            
        }
        public bool Check_Model_Upload(string Month)
        {
            try
            {
                Dictionary<string, object> retData = new Dictionary<string, object>();
                retData.Add("ProdLine", textBox2.Text);
                retData.Add("Month", Month);

                string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.DayTargetsServer",
                                            "Check_Model_Upload",
                    Program.client.UserToken,
                    Newtonsoft.Json.JsonConvert.SerializeObject(retData)
                );
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (ret.IsSuccess)
                {
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                    if(dt.Rows.Count>0)
                    {
                        textBox3.Text = dt.Rows[0]["MODEL_NAME"].ToString();
                    }
                    return dt.Rows.Count > 0;
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                    return false;
                }
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
                return false;
            }
        }

        public bool Check_Packing_Emp()
        {
            try
            {
                string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.DayTargetsServer",
                                            "Check_Packing_Emp",
                    Program.client.UserToken,
                    Newtonsoft.Json.JsonConvert.SerializeObject(string.Empty)
                );
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (ret.IsSuccess)
                {
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                    return dt.Rows.Count > 0;
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                    return false;
                }
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
                return false;
            }
        }


        private bool Check_ME_Standard(DataTable dt, string prodline, string month,string SkillName)
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("data", dt);
            p.Add("prodline", prodline);
            p.Add("month", month);
            p.Add("SkillName", SkillName);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.DayTargetsServer", "Check_ME_Standard", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            return Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]);
        }

        public void GetSkillsList(string Barcode)
        {
            try
            {
                Dictionary<string, object> retData = new Dictionary<string, object>();
                retData.Add("Barcode", Barcode);

                string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder",
                                            "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer",
                                            "GetSkillsList",
                    Program.client.UserToken,
                    Newtonsoft.Json.JsonConvert.SerializeObject(retData)
                );
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (ret.IsSuccess)
                {
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                    lt.Clear();
                    foreach (DataRow dr in dt.Rows)
                    {
                        lt.Add(dr["skillname"].ToString());
                    }
                    SkillNames = new ComboBox();
                    SkillNames.DataSource = lt;
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                }
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }

        //    public DataTable Getskillcount(string skillname)
        //    {
        //        DataTable dt = new DataTable();
        //        DataTable resultdt = new DataTable();
        //        dt.Columns.Add("SkillName", typeof(string));
        //        resultdt.Columns.Add("SkillName", typeof(string));
        //        resultdt.Columns.Add("Count", typeof(int));

        //        foreach (DataGridViewRow row in dataGridView5.Rows)
        //        {
        //            if (!row.IsNewRow)
        //            {
        //                string skillName = row.Cells[3].Value != null
        //? row.Cells[3].Value.ToString()
        //: string.Empty;
        //                if (skillName == skillname)
        //                {
        //                    dt.Rows.Add(row.Cells[3].Value);
        //                }

        //            }
        //        }

        //        var skillCounts = dt.AsEnumerable()
        //        .GroupBy(r => r.Field<string>("SkillName"))
        //        .Select(g => new
        //        {
        //            SkillName = g.Key,
        //            Count = g.Count()
        //        });


        //        foreach (var skill in skillCounts)
        //        {
        //            resultdt.Rows.Add(skill.SkillName, skill.Count);
        //        }
        //        return resultdt;

        //    }


        public DataTable Getskillcount()
        {
            DataTable dt = new DataTable();
            DataTable resultdt = new DataTable();
            dt.Columns.Add("SkillName", typeof(string));
            resultdt.Columns.Add("SkillName", typeof(string));
            resultdt.Columns.Add("Count", typeof(int));

            foreach (DataGridViewRow row in dataGridView5.Rows)
            {
                if (!row.IsNewRow)
                {
                    string skillName = row.Cells[3].Value != null
    ? row.Cells[3].Value.ToString()
    : string.Empty;
                    //if (skillName == skillname)
                    //{
                    //    dt.Rows.Add(row.Cells[3].Value);
                    //}
                    if(!string.IsNullOrEmpty(skillName))
                    {
                        dt.Rows.Add(row.Cells[3].Value);
                    }
                }
            }

            var skillCounts = dt.AsEnumerable()
            .GroupBy(r => r.Field<string>("SkillName"))
            .Select(g => new
            {
                SkillName = g.Key,
                Count = g.Count()
            });


            foreach (var skill in skillCounts)
            {
                resultdt.Rows.Add(skill.SkillName, skill.Count);
            }
            return resultdt;

        }



        public void GetSkill_Level(string SkillName)
        {
            try
            {
                Dictionary<string, object> retData = new Dictionary<string, object>();
                retData.Add("SkillName", SkillName);

                string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder",
                                            "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer",
                                            "GetSkill_Level",
                    Program.client.UserToken,
                    Newtonsoft.Json.JsonConvert.SerializeObject(retData)
                );
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (ret.IsSuccess)
                {
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                    //dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells["skillname"].Value = dt.Rows[0]["name"];
                    //dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells["skilllevel"].Value = dt.Rows[0]["skill_level"];
                    //dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells["bonus"].Value = dt.Rows[0]["bonus"];
                    dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells[3].Value = dt.Rows[0]["name"];
                    dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells[4].Value = dt.Rows[0]["skill_level"];
                    dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells[5].Value = dt.Rows[0]["bonus"];
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                }
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }

        private void dataGridView5_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (dataGridView5.Columns[e.ColumnIndex].Name == "delete")
                {
                    var result = MessageBox.Show("Are you sure you want to delete this row?",
                                      "Confirm Delete",
                                      MessageBoxButtons.YesNo,
                                      MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        dataGridView5.Rows.RemoveAt(e.RowIndex);
                        SkillNames.Visible = false;
                        SkillNames.Dispose();
                    }
                }
                if (dataGridView5.Columns[e.ColumnIndex].Index == 3)
                {
                    string currentSkill = dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells[3].Value.ToString();
                    string remarks = dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells[7].Value.ToString();
                    string Barcode = dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells["barcode"].Value.ToString();
                    string DeptNo = textBox2.Text;
                    string ModelName = textBox3.Text;
                    using (Select_Skill popup = new Select_Skill(Barcode, DeptNo, ModelName))
                    {
                        popup.ShowDialog(this);
                        string skillname = popup.SkillName;
                        if(string.IsNullOrEmpty(skillname))
                        {
                            //dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells["skillname"].Value = "";
                            //dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells["skilllevel"].Value = "";
                            //dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells["bonus"].Value = "";
                            //dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells["excessapproved"].Value = "";
                            if(string.IsNullOrEmpty(dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells[3].Value.ToString()))
                            {
                            dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells[3].Value = "";
                            dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells[4].Value = "";
                            dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells[5].Value = "";
                            dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells[6].Value = "";
                            dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells[7].Value = "";
                            }
                            
                            return;
                        }
                        GetSkill_Level(skillname);
                        //DataTable dt = Getskillcount(skillname);
                        DataTable dt = Getskillcount();
                        if (Check_ME_Standard(dt, textBox2.Text, dateTimePicker5.Text, skillname))
                        {
                           //tring Barcode = dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells["barcode"].Value.ToString();

                            using (Select_Excess_Employee popup2 = new Select_Excess_Employee(Barcode, textBox2.Text, skillname))
                            {
                                popup2.ShowDialog(this);
                                string result2 = popup2.Result;
                                //string User = popup2.User;
                                string Remarks = popup2.Remarks;
                                dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells[3].Value = result2;
                                if (!string.IsNullOrEmpty(result2))
                                {
                                    GetSkill_Level(result2);
                                    //dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells[6].Value = User;
                                    dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells[7].Value = Remarks;
                                }
                                else
                                {
                                    //dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells["skillname"].Value = "";
                                    //dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells["skilllevel"].Value = "";
                                    //dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells["bonus"].Value = "";
                                    //dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells["excessapproved"].Value = "";
                                    if(string.IsNullOrEmpty(currentSkill))
                                    {
                                        dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells[3].Value = "";
                                        dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells[4].Value = "";
                                        dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells[5].Value = "";
                                        dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells[6].Value = "";
                                        dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells[7].Value = "";
                                    }
                                    else
                                    {
                                        dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells[3].Value = currentSkill;
                                        GetSkill_Level(currentSkill);
                                        dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells[7].Value = remarks;

                                    }
                                    
                                    //dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells[3].Value = "";
                                    //dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells[4].Value = "";
                                    //dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells[5].Value = "";
                                    //dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells[6].Value = "";
                                    //dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells[7].Value = "";
                                }

                            }
                            //SJeMES_Control_Library.MessageHelper.ShowErr(this, "Exceeds ME standards");

                        }
                        else
                        {
                            dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells[7].Value = "";
                        }
                    }
                    #region Combo box
                    //if (!(lt.Count > 0))
                    //{
                    //    SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Skill Data Available");
                    //    return;
                    //}
                    //SkillNames = new ComboBox();
                    //SkillNames.Enabled = true;
                    //SkillNames.DropDownStyle = ComboBoxStyle.DropDownList;
                    //SkillNames.DataSource = lt;
                    //SkillNames.DisplayMember = "NAME";
                    //SkillNames.ValueMember = "NAME";

                    //Rectangle rect = dataGridView5.GetCellDisplayRectangle(dataGridView5.CurrentCell.ColumnIndex, dataGridView5.CurrentCell.RowIndex, false);
                    //SkillNames.Left = rect.Left;
                    //SkillNames.Top = rect.Top;
                    //SkillNames.Width = rect.Width;
                    //SkillNames.Height = rect.Height;
                    //SkillNames.Visible = true;
                    //dataGridView5.Controls.Add(SkillNames);
                    //if (dataGridView5.Rows[e.RowIndex].Cells["skillname"].Value != null && !string.IsNullOrEmpty(dataGridView5.Rows[e.RowIndex].Cells["skillname"].Value.ToString()))
                    //{
                    //    SkillNames.SelectedValue = dataGridView5.Rows[e.RowIndex].Cells["skillname"].Value.ToString();

                    //}
                    //else
                    //{
                    //    SkillNames.SelectedIndex = -1;
                    //}
                    //SkillNames.Focus();
                    //SkillNames.SelectedIndexChanged += ProcessList_SelectedIndexChanged1;
                    //dataGridView5.CellEndEdit += dataGridView5_CellEndEdit;

                    //private void ProcessList_SelectedIndexChanged1(object sender, EventArgs e)
                    //{
                    //    dataGridView5.CurrentCell.Value = SkillNames.Text;
                    //    dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells["skillname"].Value = SkillNames.SelectedValue;
                    //    string skillname = SkillNames.SelectedValue.ToString();
                    //    GetSkill_Level(skillname);
                    //    DataTable dt = Getskillcount(skillname);
                    //    if (Check_ME_Standard(dt, textBox2.Text, dateTimePicker5.Text))
                    //    {
                    //        string Barcode = dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells["barcode"].Value.ToString();

                    //        using (Select_Excess_Employee popup2 = new Select_Excess_Employee(Barcode, textBox2.Text, skillname))
                    //        {
                    //            popup2.ShowDialog(this);
                    //            string result2 = popup2.Result;
                    //            string User = popup2.User;
                    //            dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells["skillname"].Value = result2;
                    //            if (!string.IsNullOrEmpty(result2))
                    //            {
                    //                GetSkill_Level(result2);
                    //                dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells["excess_approved_by"].Value = User;
                    //            }
                    //            else
                    //            {
                    //                dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells["skillname"].Value = "";
                    //                dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells["skill_level"].Value = "";
                    //                dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells["bonus"].Value = "";
                    //                dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells["excess_approved_by"].Value = "";
                    //            }

                    //        }
                    //        //SJeMES_Control_Library.MessageHelper.ShowErr(this, "Exceeds ME standards");

                    //    }
                    //    SkillNames.Visible = false;
                    //    SkillNames.Dispose();
                    //}
                    #endregion;
                }

            }
        }
        
        private void LoadSeDept()
        {
            autocompleteMenu1.Items = null;
            autocompleteMenu1.MaximumSize = new System.Drawing.Size(350, 350);
            var columnWidth = new int[] { 50, 250 };
            DataTable dt = GetAllDepts();
            int n = 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                autocompleteMenu1.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dt.Rows[i]["DEPARTMENT_CODE"].ToString() + " " + dt.Rows[i]["DEPARTMENT_NAME"].ToString() }, dt.Rows[i]["DEPARTMENT_CODE"].ToString()) { ColumnWidth = columnWidth, ImageIndex = n });
                n++;
            }
        }
        private DataTable GetAllDepts()
        {
            DataTable dt = new DataTable();
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.GeneralServer", "GetAllDepts", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(string.Empty));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
            }
            else
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("err-00003", Program.client, "", Program.client.Language);
                string exceptionMsg = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString();
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg + exceptionMsg);
            }
            return dt;
        }

        private bool CheckUserPermission()
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "CheckUserPermission", Program.client.UserToken, JsonConvert.SerializeObject(p));
            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
            return Convert.ToBoolean(retJson["IsSuccess"]);
        }

        private void GetDept()
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "GetDept", Program.client.UserToken, JsonConvert.SerializeObject(p));
            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                string json = retJson["RetData"].ToString();
                DataTable dtJson = JsonConvert.DeserializeObject<DataTable>(json);
                if (dtJson != null && dtJson.Rows.Count > 0)
                {
                    textBox2.Text = dtJson.Rows[0]["STAFF_DEPARTMENT"].ToString();
                    txtprodline.Text = dtJson.Rows[0]["STAFF_DEPARTMENT"].ToString();
                    textBox1.Text = dtJson.Rows[0]["STAFF_DEPARTMENT"].ToString();
                }

            }

        }
        public void GetManPower()
        {
            try
            {
                Dictionary<string, object> retData = new Dictionary<string, object>();
                retData.Add("ProdLine", textBox2.Text);
                retData.Add("ProdMonth", dateTimePicker5.Text);

                string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder",
                                            "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer",
                                            "GetManPower",
                    Program.client.UserToken,
                    Newtonsoft.Json.JsonConvert.SerializeObject(retData)
                );
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (ret.IsSuccess)
                {
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                    if (dt.Rows.Count > 0)
                    {
                        dataGridView5.DataSource = dt;
                    }
                    else
                    {
                        dataGridView5.DataSource = null;
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
                    }
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                }
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            DialogResult result = MessageBox.Show($@"Are you sure you want to save the data for {dateTimePicker5.Text}?", "Confirm Action", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    DataTable dt1 = new DataTable();
                    if (dataGridView5.Rows.Count > 0)
                    {
                        foreach (DataGridViewColumn column in dataGridView5.Columns)

                            dt1.Columns.Add(column.Name);

                        foreach (DataGridViewRow row in dataGridView5.Rows)
                        {
                            string skillName = row.Cells[3].Value != null ? row.Cells[3].Value.ToString() : string.Empty;
                            //string skillName = row.Cells["skillname"].Value != null ? row.Cells["skillname"].Value.ToString() : string.Empty;
                            if (!string.IsNullOrEmpty(skillName))
                            {
                                DataRow dRow = dt1.NewRow();
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    //dRow[cell.ColumnIndex] = cell.Value != null ? cell.Value : "";
                                    dRow[cell.ColumnIndex] = cell.Value != null ? cell.Value.ToString() : string.Empty;
                                }
                                dt1.Rows.Add(dRow);

                            }
                        }


                    }
                    else
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "Model data Is not Uploaded for this Month");
                        return;
                    }

                    Dictionary<string, object> retData = new Dictionary<string, object>();
                    retData.Add("ProdLine", textBox2.Text);
                    retData.Add("ProdMonth", dateTimePicker5.Text);
                    retData.Add("data", dt1);

                    string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder",
                                                "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer",
                                                "SaveMonthlyWorkingSkill",
                        Program.client.UserToken,
                        Newtonsoft.Json.JsonConvert.SerializeObject(retData)
                    );
                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                    if (ret.IsSuccess)
                    {
                        SJeMES_Control_Library.MessageHelper.ShowSuccess(this, ret.ErrMsg);
                        GetManPower();
                    }
                    else
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                        GetManPower();
                    }
                }
                catch (Exception ex)
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
                }
            }
            else
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetMonthlyReport(txtbcode.Text);
        }

        public void GetMonthlyReport(string Barcode)
        {
            try
            {
                Dictionary<string, object> retData = new Dictionary<string, object>();
                retData.Add("Barcode", Barcode);
                retData.Add("ProdLine", txtprodline.Text);
                retData.Add("ProdMonth", dateTimePicker1.Text);
                string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder",
                                            "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer",
                                            "GetMonthlyWorkingSkill",
                    Program.client.UserToken,
                    Newtonsoft.Json.JsonConvert.SerializeObject(retData)
                );
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (ret.IsSuccess)
                {
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                    if (dt.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dt;
                    }
                    else
                    {
                        dataGridView1.DataSource = null;
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
                    }

                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);

                }
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }
        

        private void txtbcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                GetMonthlyReport(txtbcode.Text.Trim().TrimStart('0'));
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!Check_Packing_Emp())
            {
                if (Check_Model_Upload(dateTimePicker5.Text))
                {
                    GetManPower();
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Model data Is not Uploaded for this line");
                }
            }
            else
            {
                GetManPower();
            }
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dateTimePicker5_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker5.Text == DateTime.Now.ToString("yyyy/MM")|| dateTimePicker5.Text == DateTime.Now.AddMonths(1).ToString("yyyy/MM"))
            {
                button2.Visible = true;
            }
            else
            {
                button2.Visible = false;
            }

            if (!Check_Packing_Emp())
            {
                if (!string.IsNullOrEmpty(textBox2.Text))
                {
                    if (Check_Model_Upload(dateTimePicker5.Text))
                    {
                        GetManPower();
                    }
                    else
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "Model data Is not Uploaded for this line");
                        textBox3.Text = "";
                        dataGridView5.DataSource = null;
                    }
                }
            }
            else
            {
                GetManPower();
            }

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
          if(e.KeyCode==Keys.Enter)
            {
                if (Check_Model_Upload(dateTimePicker5.Text))
                {
                    GetManPower();
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Model data Is not Uploaded for this line");
                    textBox3.Text = "";
                    dataGridView5.DataSource = null;
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tagName = tabControl1.SelectedTab.Name;
            TagDataLoad(tagName);
        }

        public void TagDataLoad(string tagName)
        {
            switch (tagName)
            {
                case "tabPage3":
                    tabchange();
                    break;
                default:
                    break;
            }
        }

        public void tabchange()
        {

            if (!Check_Packing_Emp())
            {
                    if (Check_Model_Upload(dateTimePicker2.Text))
                    {
                        GetEmpList();
                    }
                    else
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "Model data Is not Uploaded for this line");
                        dataGridView2.DataSource = null;
                    }
                
            }
            else
            {
                GetEmpList();
            }
        }
         
        public void GetEmpList()
        {
            try
            {
                Dictionary<string, object> retData = new Dictionary<string, object>();
                retData.Add("ProdLine", textBox1.Text);
                retData.Add("ProdMonth", dateTimePicker2.Text);

                string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder",
                                            "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer",
                                            "GetEmpList",
                    Program.client.UserToken,
                    Newtonsoft.Json.JsonConvert.SerializeObject(retData)
                );
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (ret.IsSuccess)
                {
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                    if (dt.Rows.Count > 0)
                    {
                        dataGridView2.DataSource = dt;
                    }
                    else
                    {
                        dataGridView2.DataSource = null;
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
                    }
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                }
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void dataGridView2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox textBox)
            {
                // Unsubscribe first to prevent multiple subscriptions
                textBox.KeyPress -= TextBox_KeyPress;

                // Subscribe to the KeyPress event
                textBox.KeyPress += TextBox_KeyPress;
            }
        }

       
        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (char.IsDigit(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == (char)Keys.Back)
            {
                if (textBox.Text.Contains("."))
                {
                    if (e.KeyChar == '.')
                    {
                        e.Handled = true;
                    }
                    else
                    {
                        string currentText2 = textBox.Text + e.KeyChar;
                        string[] parts = currentText2.Split('.');
                        if (parts.Length > 1 && parts[1].Length > 2)
                        {
                            if (e.KeyChar != '\b')
                            {
                                e.Handled = true;
                            }

                        }
                    }

                   
                }
                string currentText = textBox.Text + e.KeyChar;
                try
                {
                    Decimal value = Convert.ToDecimal(currentText);
                    if (value > 70.00m)
                    {
                        e.Handled = true;
                    }
                }
                catch (FormatException)
                {

                }
            }
            else
            {
                e.Handled = true; 
            }

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show($@"Are you sure you want to save the data for {dateTimePicker5.Text}?", "Confirm Action", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    DataTable dt1 = new DataTable();
                    if (dataGridView2.Rows.Count > 0)
                    {
                        foreach (DataGridViewColumn column in dataGridView2.Columns)

                            dt1.Columns.Add(column.Name);

                        foreach (DataGridViewRow row in dataGridView2.Rows)
                        {
                            string EmpScore = row.Cells[2].Value != null ? row.Cells[2].Value.ToString() : string.Empty;
                            if (!string.IsNullOrEmpty(EmpScore))
                            {
                                DataRow dRow = dt1.NewRow();
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    dRow[cell.ColumnIndex] = cell.Value != null ? cell.Value.ToString() : string.Empty;
                                }
                                dt1.Rows.Add(dRow);

                            }
                        }


                    }
                    else
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "Model data Is not Uploaded for this Month");
                        return;
                    }

                    Dictionary<string, object> retData = new Dictionary<string, object>();
                    retData.Add("ProdLine", textBox1.Text);
                    retData.Add("ProdMonth", dateTimePicker2.Text);
                    retData.Add("data", dt1);

                    string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder",
                                                "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer",
                                                "SaveEmpScore",
                        Program.client.UserToken,
                        Newtonsoft.Json.JsonConvert.SerializeObject(retData)
                    );
                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                    if (ret.IsSuccess)
                    {
                        SJeMES_Control_Library.MessageHelper.ShowSuccess(this, ret.ErrMsg);
                        GetEmpList();
                    }
                    else
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                        GetEmpList();
                    }
                }
                catch (Exception ex)
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            string path = string.Empty;
            ofd.Title = "请选择文件夹";
            ofd.Filter = "图像文件(.jpg;.jpg;.jpeg;.gif;.png;.jpg)|.jpg;.jpeg;.gif; *.png;*.jpg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                SafeFileName = System.IO.Path.GetFileName(ofd.FileName);
                filePath = ofd.FileName;
                UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(Program.client.UploadUrl, filePath, Program.client.UserToken);
                if (res.IsSuccess)
                {
                    var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                    //label1.Text = ofd.SafeFileName;
                    Descipline_Score_Card = resultDIC["guid"].ToString();
                    var webC = new System.Net.WebClient();
                    string url = Program.client.PicUrl + Convert.ToString(resultDIC["url"].ToString());
                    Image image = new Bitmap(webC.OpenRead(url));
                    UploadDescipline_Score_Card(Descipline_Score_Card);
                }
            }
        }

        public void UploadDescipline_Score_Card(string file_guid)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("ProdLine", textBox2.Text);
                data.Add("ProdMonth", dateTimePicker2.Text);
                data.Add("file_guid", file_guid);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL,
                        "KZ_SFCAPI_WorkOrder",
                        "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer",
                        "InsertScoreCard", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Uploaded successfully!");
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, j["ErrMsg"].ToString());
                }
                    
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.client, Program.client.WebServiceUrl, Program.client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }



        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            GetScoreCard();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if(dateTimePicker2.Text!= DateTime.Now.ToString("yyyy/MM"))
            {
                button4.Visible = false;
                button5.Visible = false;
            }
            else
            {
                button4.Visible = true;
                button5.Visible = true;
            }
            if (!Check_Packing_Emp())
            {
                if (Check_Model_Upload(dateTimePicker2.Text))
                {
                    GetEmpList();
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Model data Is not Uploaded for this line");
                    dataGridView2.DataSource = null;
                }
            }
            else
            {
                GetManPower();
            }
           
            
        }

        public void GetScoreCard()
        {
            try
            {
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("ProdLine", textBox2.Text);
                p.Add("ProdMonth", dateTimePicker2.Text);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.client.APIURL,
                                           "KZ_SFCAPI_WorkOrder",
                        "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer",
                        "GetScoreCard", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                }

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dt.Rows.Count > 0)
                {
                        var webC = new System.Net.WebClient();
                        string url = Program.client.PicUrl + Convert.ToString(dt.Rows[0]["file_url"].ToString());
                        Image image = new Bitmap(webC.OpenRead(url));
                        View_Skill_Card sc = new View_Skill_Card(image);
                        sc.Show();
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Image Uploaded");
                }
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            testAsync();
        }

        public async Task testAsync()
        {
            string apiUrl = "http://10.3.0.70:9090/whatsapp/WhatsappApi/SendMessage";
            var payload = new
            {
                numbers = new[] { "9640416084","8497929619"}, // Use the fetched phone number
                groups = new List<string>(),
                textMsg = "Hi",
                mediaurl = "",
                filename = ""
            };

            var jsonPayload = JsonConvert.SerializeObject(payload);

            using (var httpClient = new HttpClient())
            {
                try
                {
                    // Set the content type to application/json
                    var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                    // Send the POST request
                    var response = await httpClient.PostAsync(apiUrl, content); // Ensure url is defined

                    // Optionally log the response or handle errors here, but do not return
                    if (!response.IsSuccessStatusCode)
                    {
                        var responseBody = await response.Content.ReadAsStringAsync();
                        // Log the failure if needed
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions (logging, etc.) but do not return
                }
            }
        }

        public async Task<List<Data>> Test()
        {
            string apiUrl = "http://localhost:5109/api/Data/GetData";
            List<Data> dataList = new List<Data>();
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        // Parse the response JSON into a List<Data> using System.Text.Json (or use Newtonsoft.Json)
                        var responseBody = await response.Content.ReadAsStringAsync();
                        dataList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Data>>(responseBody);
                        dataGridView1.DataSource = dataList;
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions (logging, etc.) but do not return
                }
                return dataList;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Test();
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
        }
    }
}

