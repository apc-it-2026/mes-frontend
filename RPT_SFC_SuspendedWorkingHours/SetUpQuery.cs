using AutocompleteMenuNS;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using static RPT_SFC_SuspendedWorkingHours.ArtQuery;
using static RPT_SFC_SuspendedWorkingHours.ArtQueryQuery;

namespace RPT_SFC_SuspendedWorkingHours
{
    public partial class SetUpQuery : MaterialForm
    {
        List<string> productionNamelist = new List<string>();//work center name
        List<string> LIABILITYNAMEsetlist = new List<string>();//Responsible unit
        List<string> LIABILITYNAMEquerylist = new List<string>();//Responsible unit
        List<string> productionNamesetlist = new List<string>();//work center name
                                                                //Create a datatable to receive data from the API (query all statements based on conditions)
        DataTable dataTableQueryAll = new DataTable();
        public SetUpQuery()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }


        private void Savebuild_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> build = new Dictionary<string, object>();
            string art = this.ART_SET.Text.ToString();
            string PRODUCTIONLINE_NAME = this.PRODUCTIONLINE_NAME_SET.Text.ToString();
            string PAUSEDATE = this.PAUSEDATE_SET.Text.ToString();
            string PROCESSNAME = this.PROCESSNAME_SET.Text.ToString();
            string SHOETYPENAME = this.SHOETYPENAME_SET.Text.ToString();
            string MOLDNO = this.MOLDNO_SET.Text.ToString();
            string LIABILITYNAME = this.LIABILITYNAME_SET.Text.ToString();
            string PARTNAME = this.PARTNAME_SET.Text.ToString();
            string PAUSEHOUR = this.PAUSEHOUR_SET.Text.ToString();
            string INFLUPEOPLE = this.INFLUPEOPLE_SET.Text.ToString();
            // double PAUSEALLHOUR = double.Parse(INFLUPEOPLE) * double.Parse(PAUSEHOUR);
            string INFLUYIELD = this.INFLUYIELD_SET.Text.ToString();
            string PAUSEREASON = this.PAUSEREASON_SET.Text.ToString();
            string REMARKS = this.REMARKS_SET.Text.ToString();
            string LASTMODIFYID = this.LASTMODIFYID_SET.Text.ToString();
            string LASTMODIRYNAME = this.LASTMODIRYNAME_SET.Text.ToString();
            string LASEMODITYTIME = this.LASEMODITYTIME_SET.Text.ToString();
            string STATUSBUILD = this.STATUSBUILD_SET.Text.ToString();
            build.Add("ART", art);
            build.Add("Work Center", PRODUCTIONLINE_NAME);
            build.Add("Process name", PROCESSNAME);
            build.Add("Responsible unit", LIABILITYNAME);
            build.Add("Part Name", PARTNAME);
            build.Add("Pause time", PAUSEHOUR);
            build.Add("Number of people affected", INFLUPEOPLE);
            build.Add("Affect yield", INFLUYIELD);
            build.Add("Pause reason", PAUSEREASON);
            Boolean temp = true;
            foreach (KeyValuePair<string, object> tips in build)
            {
                if ("".Equals(tips.Value.ToString()))
                {
                    MessageBox.Show(tips.Key + "Is empty");
                    temp = false;
                    break;
                }
            }

            //If none of the necessary conditions are empty, go to insert and query
            if (temp)
            {
                try
                {
                    double PAUSEALLHOUR = double.Parse(INFLUPEOPLE) * double.Parse(PAUSEHOUR);
                    double INFLUYIELD1 = double.Parse(INFLUYIELD);
                    //  If the creator is empty, create a new one, if the creator exists, modify the existing data
                    if ("".Equals(STATUSBUILD))
                    {
                        STATUSBUILD = "Take Effect";
                    }
                    Dictionary<string, object> p = new Dictionary<string, object>();
                    p.Add("art", art);
                    p.Add("PRODUCTIONLINE_NAME", PRODUCTIONLINE_NAME);
                    p.Add("PAUSEDATE", PAUSEDATE);
                    p.Add("PROCESSNAME", PROCESSNAME);
                    p.Add("SHOETYPENAME", SHOETYPENAME);
                    p.Add("MOLDNO", MOLDNO);
                    p.Add("LIABILITYNAME", LIABILITYNAME);
                    p.Add("PARTNAME", PARTNAME);
                    p.Add("PAUSEHOUR", PAUSEHOUR);
                    p.Add("INFLUPEOPLE", INFLUPEOPLE);
                    p.Add("PAUSEALLHOUR", PAUSEALLHOUR);
                    p.Add("INFLUYIELD", INFLUYIELD1);
                    p.Add("PAUSEREASON", PAUSEREASON);
                    p.Add("REMARKS", REMARKS);
                    p.Add("LASTMODIFYID", LASTMODIFYID);
                    p.Add("LASTMODIRYNAME", LASTMODIRYNAME);
                    p.Add("LASEMODITYTIME", LASEMODITYTIME);
                    p.Add("STATUS", STATUSBUILD);
                    if (this.BUILDNO_SET.Text.ToString().Equals(""))
                    {
                        try
                        {
                            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.SuspendedWorkingHoursServer", "InsertSuspendedWorkHours", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                            {
                                this.PAUSEALLHOUR_SET.Text = PAUSEALLHOUR.ToString();
                                MessageBox.Show("Inserted successfully");
                            }
                        }
                        catch (Exception ex)
                        {
                            SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
                        }
                    }
                    else

                    {
                        try
                        {
                            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.SuspendedWorkingHoursServer", "UpdateSuspendedWorkHours", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

                            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                            {
                                this.PAUSEALLHOUR_SET.Text = PAUSEALLHOUR.ToString();
                                MessageBox.Show("update completed");
                            }
                        }
                        catch (Exception ex)
                        {
                            SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Pause time, affects people, affects output as numbers");
                }


            }
        }

        private void query_Click(object sender, EventArgs e)
        {
            while (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows.RemoveAt(0);
            }
            string PRODUCTIONLINE_NAME = this.PRODUCTIONLINE_NAME_query.Text.ToString();
            string art = this.artquery.Text.ToString();
            string firstime = this.dateTimefirst.Text.ToString();
            string lasttime = this.dateTimelast.Text.ToString();
            string LIABILITYNAME_query = this.LIABILITYNAME_query.Text.ToString();
            string status = this.statusquery.Text.ToString();
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("PRODUCTIONLINE_NAME", PRODUCTIONLINE_NAME);
            p.Add("art", art);
            p.Add("firstime", firstime);
            p.Add("lasttime", lasttime);
            p.Add("LIABILITYNAME_query", LIABILITYNAME_query);
            p.Add("status", status);

            try
            {
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.SuspendedWorkingHoursServer", "QuerySuspendedWorkHours", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {

                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    dataTableQueryAll = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                    DataRow TotaldgvRow2 = WinFormLib.TotalRow.GetTotalRow(dataTableQueryAll);        //return DataRow


                    TotaldgvRow2[dataTableQueryAll.Columns["PAUSEDATE"]] = string.Empty;
                    TotaldgvRow2[dataTableQueryAll.Columns["ART"]] = string.Empty;
                    TotaldgvRow2[dataTableQueryAll.Columns["PARTNAME"]] = string.Empty;
                    TotaldgvRow2[dataTableQueryAll.Columns["PAUSEREASON"]] = "Total";
                    TotaldgvRow2[dataTableQueryAll.Columns["REMARKS"]] = string.Empty;

                    dataTableQueryAll.Rows.Add(TotaldgvRow2);
                    dataGridView1.DataSource = dataTableQueryAll.DefaultView;

                    dataGridView1.Update();
                }
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }


        private void PRODUCTIONLINE_NAME_Load(object sender, EventArgs e)
        {
            //Create a list whose generic type is AutocompleteItem
            //AutocompleteItem is a row of data in a two-dimensional table

            //Call the corresponding method of the API, get the data from the database, and return it in the form of a string
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.SuspendedWorkingHoursServer", "QueryProductionName", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(""));
            //    string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.SuspendedWorkingHoursServer", "QueryProductionName", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject("noParam"));
            //  string ret3 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.SuspendedWorkingHoursServer", "QueryProductionName", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject("noParam"));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                //Add multi-column menu to items3
                productionNamelist.Add("");
                //Add each row of data in datatable to list3
                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {
                    // items3.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i - 1]["DEPARTMENT_CODE"].ToString() }, dtJson.Rows[i - 1]["DEPARTMENT_CODE"].ToString()));
                    productionNamelist.Add(dtJson.Rows[i - 1]["DEPARTMENT_NAME"].ToString());
                }
            }
            //Assign the value of item3 to comboBox1.DataSource
            //  PRODUCTIONLINE_NAME_query.DataSource = productionNamelist;
            PRODUCTIONLINE_NAME_query.Items.AddRange(productionNamelist.ToArray());






            string ret1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.SuspendedWorkingHoursServer", "LoadLiabiltyName", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(""));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                LIABILITYNAMEsetlist.Add("");
                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {
                    LIABILITYNAMEsetlist.Add(dtJson.Rows[i - 1]["ORG"].ToString());
                }
            }

            LIABILITYNAME_SET.Items.AddRange(LIABILITYNAMEsetlist.ToArray());


            string ret2 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.SuspendedWorkingHoursServer", "LoadLiabiltyName", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(""));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                //Add multi-column menu to items3
                LIABILITYNAMEquerylist.Add("");
                //Add each row of data in datatable to list3
                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {
                    // items3.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i - 1]["DEPARTMENT_CODE"].ToString() }, dtJson.Rows[i - 1]["DEPARTMENT_CODE"].ToString()));
                    LIABILITYNAMEquerylist.Add(dtJson.Rows[i - 1]["ORG"].ToString());
                }
            }
            //Assign the value of item3 to comboBox1.DataSource
            //  PRODUCTIONLINE_NAME_query.DataSource = productionNamelist;
            LIABILITYNAME_query.Items.AddRange(LIABILITYNAMEquerylist.ToArray());

            string ret3 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.SuspendedWorkingHoursServer", "QueryProductionName", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(""));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret3)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret3)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                productionNamesetlist.Add("");
                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {
                    productionNamesetlist.Add(dtJson.Rows[i - 1]["DEPARTMENT_NAME"].ToString());
                }
            }
            PRODUCTIONLINE_NAME_SET.Items.AddRange(productionNamesetlist.ToArray());


            List<string> items4 = new List<string>();//work center name
            string ret4 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.SuspendedWorkingHoursServer", "LoadRoutNo", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(""));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret4)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret4)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                //Add multi-column menu to items3
                items4.Add("");
                //Add each row of data in datatable to list3
                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {
                    // items3.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i - 1]["DEPARTMENT_CODE"].ToString() }, dtJson.Rows[i - 1]["DEPARTMENT_CODE"].ToString()));
                    items4.Add(dtJson.Rows[i - 1]["rout_name_z"].ToString());
                }
            }
            //Assign the value of item3 to comboBox1.DataSource 
            //  PRODUCTIONLINE_NAME_query.DataSource = productionNamelist;
            PROCESSNAME_SET.Items.AddRange(items4.ToArray());



        }

        private void PRODUCTIONLINE_NAME_query_textupdate(object sender, EventArgs e)
        {
            string s = this.PRODUCTIONLINE_NAME_query.Text;  //Get the input content of the cb_material control
            List<string> strList = new List<string>();   //Store raw data (can be objects, strings...)
            strList.AddRange(productionNamelist.ToArray());  // List<string> materials
            List<string> strListNew = new List<string>();
            //Empty combobox
            this.PRODUCTIONLINE_NAME_query.Items.Clear();
            //traverse all raw data
            foreach (var item in strList)
            {
                // According to the fuzzy query of the input value, the qualified value is stored in the new strListNew collection
                if (item.Contains(this.PRODUCTIONLINE_NAME_query.Text))
                {
                    strListNew.Add(item);
                }
            }
            if (strListNew.Count >= 1) // Eligible content exists
            {
                //Add eligible content to the combobox
                this.PRODUCTIONLINE_NAME_query.Items.AddRange(strListNew.ToArray());
            }
            else  // When no conditions exist
            {
                // The following code is to add the value entered by itself when the query does not meet the conditions
                this.PRODUCTIONLINE_NAME_query.Items.Add(this.PRODUCTIONLINE_NAME_query.Text);
            }
            //Set the cursor position, if not set: the cursor position is always kept in the first column, resulting in the reverse order of the input keywords
            this.PRODUCTIONLINE_NAME_query.SelectionStart = this.PRODUCTIONLINE_NAME_query.Text.Length;  // Set the cursor position, if not set: the cursor position is always kept in the first column, resulting in the reverse order of the input keywords
            Cursor = Cursors.Default; //Keep the mouse pointer in the original state, sometimes the mouse pointer will be covered by the drop-down box, so you need to set it once
            this.PRODUCTIONLINE_NAME_query.DroppedDown = true; // Automatic pop-up drop-down box
        }

        private void LIABILITYNAMESet_textupdate(object sender, EventArgs e)
        {
            string s1 = this.LIABILITYNAME_SET.Text;
            List<string> strList = new List<string>();   //Store raw data (can be objects, strings...)
            strList.AddRange(LIABILITYNAMEsetlist.ToArray());  // List<string> materials
            List<string> strListNew = new List<string>();
            //Empty combobox
            this.LIABILITYNAME_SET.Items.Clear();
            //traverse all raw data
            foreach (var item in strList)
            {
                // According to the fuzzy query of the input value, the qualified value is stored in the new strListNew collection
                if (item.Contains(s1))
                {
                    strListNew.Add(item);
                }
            }
            if (strListNew.Count >= 1) // Eligible content exists
            {
                //Add eligible content to the combobox
                this.LIABILITYNAME_SET.Items.AddRange(strListNew.ToArray());
            }
            else  // When no conditions exist
            {
                // The following code is to add the value entered by itself when the query does not meet the conditions
                this.LIABILITYNAME_SET.Items.Add(this.LIABILITYNAME_SET.Text);
            }
            //Set the cursor position, if not set: the cursor position is always kept in the first column, resulting in the reverse order of the input keywords
            this.LIABILITYNAME_SET.SelectionStart = this.LIABILITYNAME_SET.Text.Length;  // Set the cursor position, if not set: the cursor position is always kept in the first column, resulting in the reverse order of the input keywords
            Cursor = Cursors.Default; //Keep the mouse pointer in the original state, sometimes the mouse pointer will be covered by the drop-down box, so you need to set it once
            this.LIABILITYNAME_SET.DroppedDown = true; // Automatic pop-up drop-down box


        }

        private void LIABILITYNAMEQUery_textupdate(object sender, EventArgs e)
        {
            string s = this.LIABILITYNAME_query.Text;  //Get the input content of the cb_material control


            List<string> strList = new List<string>();   //Store raw data (can be objects, strings...)
            strList.AddRange(LIABILITYNAMEquerylist.ToArray());  // List<string> materials
            List<string> strListNew = new List<string>();
            //Empty combobox
            this.LIABILITYNAME_query.Items.Clear();
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
                this.LIABILITYNAME_query.Items.AddRange(strListNew.ToArray());
            }
            else  // When no conditions exist
            {
                // The following code is to add the value entered by itself when the query does not meet the conditions
                this.LIABILITYNAME_query.Items.Add(this.LIABILITYNAME_query.Text);
            }
            //Set the cursor position, if not set: the cursor position is always kept in the first column, resulting in the reverse order of the input keywords
            this.LIABILITYNAME_query.SelectionStart = this.LIABILITYNAME_query.Text.Length;  // Set the cursor position, if not set: the cursor position is always kept in the first column, resulting in the reverse order of the input keywords
            Cursor = Cursors.Default; //Keep the mouse pointer in the original state, sometimes the mouse pointer will be covered by the drop-down box, so you need to set it once
            this.LIABILITYNAME_query.DroppedDown = true; // Automatic pop-up drop-down box
        }



        private void PRODUCTIONLINE_NAME_set_textupdate(object sender, EventArgs e)
        {
            string s = this.PRODUCTIONLINE_NAME_SET.Text;  //Get the input content of the cb_material control
            List<string> strList = new List<string>();   //Store raw data (can be objects, strings...)
            strList.AddRange(productionNamesetlist.ToArray());  // List<string> materials
            List<string> strListNew = new List<string>();
            //Empty combobox
            this.PRODUCTIONLINE_NAME_SET.Items.Clear();
            //traverse all raw data
            foreach (var item in strList)
            {
                // According to the fuzzy query of the input value, the qualified value is stored in the new strListNew collection
                if (item.Contains(this.PRODUCTIONLINE_NAME_SET.Text))
                {
                    strListNew.Add(item);
                }
            }
            if (strListNew.Count >= 1) // Eligible content exists
            {
                //Add eligible content to the combobox
                this.PRODUCTIONLINE_NAME_SET.Items.AddRange(strListNew.ToArray());
            }
            else  // When no conditions exist
            {
                // The following code is to add the value entered by itself when the query does not meet the conditions
                this.PRODUCTIONLINE_NAME_SET.Items.Add(this.PRODUCTIONLINE_NAME_SET.Text);
            }
            //Set the cursor position, if not set: the cursor position is always kept in the first column, resulting in the reverse order of the input keywords
            this.PRODUCTIONLINE_NAME_SET.SelectionStart = this.PRODUCTIONLINE_NAME_SET.Text.Length;  // Set the cursor position, if not set: the cursor position is always kept in the first column, resulting in the reverse order of the input keywords
            Cursor = Cursors.Default; //Keep the mouse pointer in the original state, sometimes the mouse pointer will be covered by the drop-down box, so you need to set it once
            this.PRODUCTIONLINE_NAME_SET.DroppedDown = true; // Automatic pop-up drop-down box
        }



        private void Delete_Click(object sender, EventArgs e)
        {

            string status = this.STATUSBUILD_SET.Text.ToString();
            if (status.Equals("Take Effect"))
            {
                Dictionary<string, object> build = new Dictionary<string, object>();
                string art = this.ART_SET.Text.ToString();
                string PRODUCTIONLINE_NAME = this.PRODUCTIONLINE_NAME_SET.Text.ToString();
                string PAUSEDATE = this.PAUSEDATE_SET.Text.ToString();
                string PROCESSNAME = this.PROCESSNAME_SET.Text.ToString();
                string SHOETYPENAME = this.SHOETYPENAME_SET.Text.ToString();
                string MOLDNO = this.MOLDNO_SET.Text.ToString();
                string LIABILITYNAME = this.LIABILITYNAME_SET.Text.ToString();
                string PARTNAME = this.PARTNAME_SET.Text.ToString();
                string PAUSEHOUR = this.PAUSEHOUR_SET.Text.ToString();
                string INFLUPEOPLE = this.INFLUPEOPLE_SET.Text.ToString();
                // double PAUSEALLHOUR = double.Parse(INFLUPEOPLE) * double.Parse(PAUSEHOUR);
                string INFLUYIELD = this.INFLUYIELD_SET.Text.ToString();
                string PAUSEREASON = this.PAUSEREASON_SET.Text.ToString();
                string REMARKS = this.REMARKS_SET.Text.ToString();
                string LASTMODIFYID = this.LASTMODIFYID_SET.Text.ToString();
                string LASTMODIRYNAME = this.LASTMODIRYNAME_SET.Text.ToString();
                string LASEMODITYTIME = this.LASEMODITYTIME_SET.Text.ToString();
                string STATUSBUILD = this.STATUSBUILD_SET.Text.ToString();
                build.Add("ART", art);
                build.Add("Work Center", PRODUCTIONLINE_NAME);
                build.Add("Process name", PROCESSNAME);
                build.Add("Responsible unit", LIABILITYNAME);
                build.Add("Part Name", PARTNAME);
                build.Add("Pause time", PAUSEHOUR);
                build.Add("Number of people affected", INFLUPEOPLE);
                build.Add("Affect yield", INFLUYIELD);
                build.Add("Pause reason", PAUSEREASON);
                Boolean temp = true;
                foreach (KeyValuePair<string, object> tips in build)
                {
                    if ("".Equals(tips.Value.ToString()))
                    {
                        MessageBox.Show(tips.Key + "Is empty");
                        temp = false;
                        break;
                    }
                }
                if (temp)
                {
                    double PAUSEALLHOUR = double.Parse(INFLUPEOPLE) * double.Parse(PAUSEHOUR);
                    Dictionary<string, object> p = new Dictionary<string, object>();
                    p.Add("ART", art);
                    p.Add("PRODUCTIONLINE_NAME", PRODUCTIONLINE_NAME);
                    p.Add("PAUSEDATE", PAUSEDATE);
                    p.Add("PROCESSNAME", PROCESSNAME);
                    p.Add("SHOETYPENAME", SHOETYPENAME);
                    p.Add("MOLDNO", MOLDNO);
                    p.Add("LIABILITYNAME", LIABILITYNAME);
                    p.Add("PARTNAME", PARTNAME);
                    p.Add("PAUSEHOUR", PAUSEHOUR);
                    p.Add("INFLUPEOPLE", INFLUPEOPLE);
                    p.Add("PAUSEALLHOUR", PAUSEALLHOUR);
                    p.Add("INFLUYIELD", INFLUYIELD);
                    p.Add("PAUSEREASON", PAUSEREASON);
                    p.Add("REMARKS", REMARKS);
                    p.Add("LASTMODIFYID", LASTMODIFYID);
                    p.Add("LASTMODIRYNAME", LASTMODIRYNAME);
                    p.Add("LASEMODITYTIME", LASEMODITYTIME);
                    p.Add("STATUS", STATUSBUILD);

                    try
                    {
                        string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.SuspendedWorkingHoursServer", "UpdateStasus", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

                        if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                        {
                            MessageBox.Show("Set to delete successfully");
                        }

                    }
                    catch (Exception ex)
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
                    }
                }
            }
            else if ("".Equals(status))
            {
                MessageBox.Show("The record is not saved, please save the data first");
            }
            else
            {
                MessageBox.Show("The status is already deleted, no need to delete it again");
            }
        }

        private void export_Click(object sender, EventArgs e)
        {
            string reportType;
            // if (dataGridView1.Rows.Count > 0)
            //{
            int index = dataGridView1.CurrentRow.Index;
            //   string type = dataGridView1.Rows[index].Cells[2].Value.ToString();
            //  if (type.Equals("B"))
            //     reportType = "\\中仓出厂商.frx";
            //   else
            //   reportType = "\\厂商入中仓.frx";
            reportType = "\\停顿工时.frx";
            string path = Application.StartupPath + @"\报表" + reportType;
            Form1 frm = new Form1(dataTableQueryAll, path);
            frm.Show();
            // }

        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < dataGridView1.RowCount - 1)
            {
                if (e.ColumnIndex == dataGridView1.Columns["Edit"].Index)
                {
                    if (!loadSetupData(e))
                    {
                        MessageBox.Show("The data has been modified, please check and then edit");
                        return;

                    }

                    string STATUS = dataGridView1.Rows[e.RowIndex].Cells[dataGridView1.Columns["dgv_STATUS"].Index].Value == null ? "" : dataGridView1.Rows[e.RowIndex].Cells[dataGridView1.Columns["dgv_STATUS"].Index].Value.ToString();
                    //判断状态是否为删除
                    if (STATUS.Equals("Delete"))
                    {
                        // this.LIABILITYNAME_SET.DropDownStyle = ComboBoxStyle.DropDownList;  只能从下拉框中选内容
                        this.PRODUCTIONLINE_NAME_SET.Enabled = false;
                        this.PAUSEDATE_SET.Enabled = false;
                        this.PROCESSNAME_SET.Enabled = false;
                        this.LIABILITYNAME_SET.Enabled = false;
                        this.PARTNAME_SET.ReadOnly = true;
                        this.PAUSEHOUR_SET.ReadOnly = true;
                        this.INFLUPEOPLE_SET.ReadOnly = true;
                        this.PAUSEREASON_SET.ReadOnly = true;
                        this.INFLUYIELD_SET.ReadOnly = true;
                        this.REMARKS_SET.ReadOnly = true;
                        this.savebuild.Enabled = false;
                        this.Delete.Enabled = false;
                        this.resetbuild.Enabled = false;
                        this.button4.Enabled = false;
                    }
                    else
                    {
                        this.PRODUCTIONLINE_NAME_SET.Enabled = false;
                        this.PAUSEDATE_SET.Enabled = false;
                        this.PROCESSNAME_SET.Enabled = false;
                        this.LIABILITYNAME_SET.Enabled = false;
                        this.PARTNAME_SET.ReadOnly = true;
                        this.PAUSEHOUR_SET.ReadOnly = false;
                        this.INFLUPEOPLE_SET.ReadOnly = false;
                        this.PAUSEREASON_SET.ReadOnly = false;
                        this.INFLUYIELD_SET.ReadOnly = false;
                        this.REMARKS_SET.ReadOnly = false;
                        this.savebuild.Enabled = true;
                        this.Delete.Enabled = true;
                        this.resetbuild.Enabled = true;
                        this.button4.Enabled = true;
                    }
                    Save.SelectTab(0);
                }
                if (e.ColumnIndex == dataGridView1.Columns["look"].Index)
                {
                    loadSetupData(e);
                    // this.LIABILITYNAME_SET.DropDownStyle = ComboBoxStyle.DropDownList;  只能从下拉框中选内容
                    this.PRODUCTIONLINE_NAME_SET.Enabled = false;

                    this.PAUSEDATE_SET.Enabled = false;
                    //this.SHOETYPENAME_SET.ReadOnly = true;
                    this.PROCESSNAME_SET.Enabled = false;
                    // this.MOLDNO_SET.ReadOnly = true;
                    this.LIABILITYNAME_SET.Enabled = false;
                    this.PARTNAME_SET.ReadOnly = true;
                    this.PAUSEHOUR_SET.ReadOnly = true;
                    this.INFLUPEOPLE_SET.ReadOnly = true;
                    // this.PAUSEALLHOUR_SET.ReadOnly = true;
                    this.PAUSEREASON_SET.ReadOnly = true;
                    this.INFLUYIELD_SET.ReadOnly = true;
                    this.REMARKS_SET.ReadOnly = true;
                    this.savebuild.Enabled = false;
                    this.Delete.Enabled = false;
                    this.resetbuild.Enabled = false;
                    this.button4.Enabled = false;
                    //this.savebuild.Click -= Savebuild_Click;
                    // this.Delete.Click -= Delete_Click;
                    // this.resetbuild.Click -= resetbuild_Click;
                    // this.button4.Click -= button4_Click;
                    Save.SelectTab(0);
                }
            }
        }

        public bool loadSetupData(DataGridViewCellEventArgs e)
        {
            string PRODUCTIONLINE_NAME = dataGridView1.Rows[e.RowIndex].Cells[dataGridView1.Columns["dgv_PRODUCTIONLINE"].Index].Value == null ? "" : dataGridView1.Rows[e.RowIndex].Cells[dataGridView1.Columns["dgv_PRODUCTIONLINE"].Index].Value.ToString();
            string PAUSEDATE = dataGridView1.Rows[e.RowIndex].Cells[dataGridView1.Columns["dgv_PAUSEDATE"].Index].Value == null ? "" : dataGridView1.Rows[e.RowIndex].Cells[dataGridView1.Columns["dgv_PAUSEDATE"].Index].Value.ToString();
            string SHOETYPENAME = dataGridView1.Rows[e.RowIndex].Cells[dataGridView1.Columns["dgv_SHOETYPENAME"].Index].Value == null ? "" : dataGridView1.Rows[e.RowIndex].Cells[dataGridView1.Columns["dgv_SHOETYPENAME"].Index].Value.ToString();
            string ART = dataGridView1.Rows[e.RowIndex].Cells[dataGridView1.Columns["dgv_ART"].Index].Value == null ? "" : dataGridView1.Rows[e.RowIndex].Cells[dataGridView1.Columns["dgv_ART"].Index].Value.ToString();
            string MOLDNO = dataGridView1.Rows[e.RowIndex].Cells[dataGridView1.Columns["dgv_MOLDNO"].Index].Value == null ? "" : dataGridView1.Rows[e.RowIndex].Cells[dataGridView1.Columns["dgv_MOLDNO"].Index].Value.ToString();
            string PROCESSNAME = dataGridView1.Rows[e.RowIndex].Cells[dataGridView1.Columns["dgv_PROCESSNAME"].Index].Value == null ? "" : dataGridView1.Rows[e.RowIndex].Cells[dataGridView1.Columns["dgv_PROCESSNAME"].Index].Value.ToString();
            string PARTNAME = dataGridView1.Rows[e.RowIndex].Cells[dataGridView1.Columns["dgv_PARTNAME"].Index].Value == null ? "" : dataGridView1.Rows[e.RowIndex].Cells[dataGridView1.Columns["dgv_PARTNAME"].Index].Value.ToString();
            string LIABILITYNAME = dataGridView1.Rows[e.RowIndex].Cells[dataGridView1.Columns["dgv_LIABILITYNAME"].Index].Value == null ? "" : dataGridView1.Rows[e.RowIndex].Cells[dataGridView1.Columns["dgv_LIABILITYNAME"].Index].Value.ToString();
            string PAUSEREASON = dataGridView1.Rows[e.RowIndex].Cells[dataGridView1.Columns["dgv_PAUSEREASON"].Index].Value == null ? "" : dataGridView1.Rows[e.RowIndex].Cells[dataGridView1.Columns["dgv_PAUSEREASON"].Index].Value.ToString();
            string PAUSEHOUR = dataGridView1.Rows[e.RowIndex].Cells[dataGridView1.Columns["dgv_PAUSEHOUR"].Index].Value == null ? "" : dataGridView1.Rows[e.RowIndex].Cells[dataGridView1.Columns["dgv_PAUSEHOUR"].Index].Value.ToString();
            string INFLUPEOPLE = dataGridView1.Rows[e.RowIndex].Cells[dataGridView1.Columns["dgv_INFLUPEOPLE"].Index].Value == null ? "" : dataGridView1.Rows[e.RowIndex].Cells[dataGridView1.Columns["dgv_INFLUPEOPLE"].Index].Value.ToString();
            string PAUSEALLHOUR = dataGridView1.Rows[e.RowIndex].Cells[dataGridView1.Columns["dgv_PAUSEALLHOUR"].Index].Value == null ? "" : dataGridView1.Rows[e.RowIndex].Cells[dataGridView1.Columns["dgv_PAUSEALLHOUR"].Index].Value.ToString();
            string INFLUYIELD = dataGridView1.Rows[e.RowIndex].Cells[dataGridView1.Columns["dgv_INFLUYIELD"].Index].Value == null ? "" : dataGridView1.Rows[e.RowIndex].Cells[dataGridView1.Columns["dgv_INFLUYIELD"].Index].Value.ToString();
            string REMARKS = dataGridView1.Rows[e.RowIndex].Cells[dataGridView1.Columns["dgv_REMARKS"].Index].Value == null ? "" : dataGridView1.Rows[e.RowIndex].Cells[dataGridView1.Columns["dgv_REMARKS"].Index].Value.ToString();
            string STATUS = dataGridView1.Rows[e.RowIndex].Cells[dataGridView1.Columns["dgv_STATUS"].Index].Value == null ? "" : dataGridView1.Rows[e.RowIndex].Cells[dataGridView1.Columns["dgv_STATUS"].Index].Value.ToString();
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("PRODUCTIONLINE_NAME", PRODUCTIONLINE_NAME);
            p.Add("PAUSEDATE", PAUSEDATE);
            p.Add("SHOETYPENAME", SHOETYPENAME);
            p.Add("ART", ART);
            p.Add("MOLDNO", MOLDNO);
            p.Add("PROCESSNAME", PROCESSNAME);
            p.Add("LIABILITYNAME", LIABILITYNAME);
            p.Add("PARTNAME", PARTNAME);
            p.Add("PAUSEREASON", PAUSEREASON);
            p.Add("PAUSEHOUR", PAUSEHOUR);
            p.Add("INFLUPEOPLE", INFLUPEOPLE);
            p.Add("PAUSEALLHOUR", PAUSEALLHOUR);
            p.Add("INFLUYIELD", INFLUYIELD);
            p.Add("REMARKS", REMARKS);
            p.Add("STATUS", STATUS);
            DataTable dataTable = getExistData(p);
            if (dataTable.Rows.Count > 0)
            {
                this.PRODUCTIONLINE_NAME_SET.Text = dataTable.Rows[0][0].ToString();
                this.ART_SET.Text = dataTable.Rows[0][1].ToString();
                this.PAUSEDATE_SET.Text = dataTable.Rows[0][2].ToString();
                this.SHOETYPENAME_SET.Text = dataTable.Rows[0][3].ToString();
                this.PROCESSNAME_SET.Text = dataTable.Rows[0][5].ToString();
                this.MOLDNO_SET.Text = dataTable.Rows[0][4].ToString();
                this.LIABILITYNAME_SET.Text = dataTable.Rows[0][7].ToString();
                this.PARTNAME_SET.Text = dataTable.Rows[0][6].ToString();
                this.PAUSEHOUR_SET.Text = dataTable.Rows[0][9].ToString();
                this.INFLUPEOPLE_SET.Text = dataTable.Rows[0][10].ToString();
                this.PAUSEALLHOUR_SET.Text = dataTable.Rows[0][11].ToString();
                this.PAUSEREASON_SET.Text = dataTable.Rows[0][8].ToString();
                this.INFLUYIELD_SET.Text = dataTable.Rows[0][12].ToString();
                this.REMARKS_SET.Text = dataTable.Rows[0][13].ToString();
                this.BUILDNO_SET.Text = dataTable.Rows[0][16].ToString();
                this.BUILDNAME_SET.Text = dataTable.Rows[0][17].ToString();
                this.BUILDTIME_SET.Text = dataTable.Rows[0][15].ToString();
                this.LASTMODIFYID_SET.Text = dataTable.Rows[0][18].ToString();
                this.LASTMODIRYNAME_SET.Text = dataTable.Rows[0][19].ToString();
                this.LASEMODITYTIME_SET.Text = dataTable.Rows[0][20].ToString();
                this.STATUSBUILD_SET.Text = dataTable.Rows[0][14].ToString();
                return true;
            }
            return false;

        }

        // query all data
        public DataTable getExistData(Dictionary<string, object> p)
        {
            DataTable dataTable = new DataTable();
            try
            {
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.SuspendedWorkingHoursServer", "GetExistData", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {

                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    dataTable = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                    //  dataGridView1.DataSource = dataTable.DefaultView;

                    //dataGridView1.Update();
                }
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
            return dataTable;
        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void resetbuild_Click(object sender, EventArgs e)
        {
            this.PRODUCTIONLINE_NAME_SET.Enabled = true;
            this.PAUSEDATE_SET.Enabled = true;
            this.PROCESSNAME_SET.Enabled = true;
            this.LIABILITYNAME_SET.Enabled = true;
            this.PARTNAME_SET.ReadOnly = false;
            this.PRODUCTIONLINE_NAME_SET.Text = "";
            this.ART_SET.Text = "";
            this.PAUSEDATE_SET.Text = "";
            this.SHOETYPENAME_SET.Text = "";
            this.PROCESSNAME_SET.Text = "";
            this.MOLDNO_SET.Text = "";
            this.LIABILITYNAME_SET.Text = "";
            this.PARTNAME_SET.Text = "";
            this.PAUSEHOUR_SET.Text = "";
            this.INFLUPEOPLE_SET.Text = "";
            this.PAUSEALLHOUR_SET.Text = "";
            this.PAUSEREASON_SET.Text = "";
            this.INFLUYIELD_SET.Text = "";
            this.REMARKS_SET.Text = "";
            this.BUILDNO_SET.Text = "";
            this.BUILDNAME_SET.Text = "";
            this.BUILDTIME_SET.Text = "";
            this.LASTMODIFYID_SET.Text = "";
            this.LASTMODIRYNAME_SET.Text = "";
            this.LASEMODITYTIME_SET.Text = "";
            this.STATUSBUILD_SET.Text = "";

        }

        private void resetquery_Click(object sender, EventArgs e)
        {
            this.PRODUCTIONLINE_NAME_query.Text = "";
            this.artquery.Text = "";
            this.dateTimefirst.Text = "";
            this.dateTimelast.Text = "";
            this.LIABILITYNAME_query.Text = "";
            this.statusquery.Text = "";
            while (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows.RemoveAt(0);
            }
        }

        private void artquery_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ArtQueryQuery frm = new ArtQueryQuery();
            frm.DataChange2 += new ArtQueryQuery.DataChangeHandler2(DataChanged2);

            frm.ShowDialog();
        }


        public void DataChanged2(object sender, DataChangeEventArgs2 args)
        {
            artquery.Text = args.prod_no;
        }




        private void button4_Click(object sender, EventArgs e)
        {
            ArtQuery frm = new ArtQuery();
            frm.DataChange += new ArtQuery.DataChangeHandler(DataChanged);
            frm.ShowDialog();
        }

        public void DataChanged(object sender, DataChangeEventArgs args)
        {
            ART_SET.Text = args.prod_no;
            SHOETYPENAME_SET.Text = args.name_t;
            MOLDNO_SET.Text = args.mold_no;
        }
    }
}
