using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using SJeMES_Framework.WebAPI;
using MaterialSkin.Controls;
using System.Linq;

namespace WorkingHoursRecord
{
    public partial class WorkingHoursRecord : MaterialForm
    {
        private string staff_no = "";

        private bool isInit;

        //Added by Ashok for skill selecting on 2024/09/26
        ComboBox SkillNames;  
        List<string> lt = new List<string>();
        private FlowLayoutPanel flowLayoutPanel;
        private Button showPopupButton;

        /// <summary>
        ///     Whether to trigger the comboBoxClasssHour_SelectedIndexChanged method for the first time, make a mark
        /// </summary>
        private bool comboBoxClasssHourIndexFormLoad = true;

        /// <summary>
        ///     DataTable table of workTimeDtJson
        /// </summary>
        private DataTable workTimeDtJson = new DataTable();

        public WorkingHoursRecord()
        {
            InitializeComponent();

            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);


            if (DateTime.Now.Hour == 0)
            {
                dataGridViewOnLine.DataSource = null;
                dataGridViewOffLine.DataSource = null;
                txtOnlineNumber.Text = "";
                txtOperator.Text = "";
                txtWaterSpider.Text = "";
                txtUniversalWork.Text = "";
                txtMultipleSequenceWork.Text = "";
                comboBoxClasssHour.Text = "";
                txtMorningOnTime.Text = "";
                txtMorningEndTime.Text = "";
                txtAfternoonOnTime.Text = "";
                txtAfternoonEndTime.Text = "";
            }
        }


        private void WorkingHoursRecordForm_Load(object sender, EventArgs e)
        {
            string udf05 = GetDept(); //udf05 is the work center
            workTimeDtJson = GetWorkTime("", udf05);

            txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

            Dictionary<string, object> parm = new Dictionary<string, object>();
            parm.Add("status", 0); //online record
            parm.Add("deptNo", txtDeptNo.Text); //Department = Work Center
            GetOnlineAndOfflineList(parm, true);
            GetDeptManpower();
            GetModel();
            txtOnlineNumber.Text = dataGridViewOnLine.RowCount.ToString();
            if(string.IsNullOrEmpty(txtWaterSpider.Text))
            {
                txtWaterSpider.ReadOnly = false;
            }
            else
            {
                txtWaterSpider.ReadOnly = true;
            }

            #region Timer time control is exactly the same as the drag control, but the drag control must set Elapsed=true to take effect

            //System.Timers.Timer timer = new System.Timers.Timer();
            //timer.Interval = 500;//间隔时间为5s

            //timer.Elapsed += delegate
            //{
            //    Console.WriteLine($"Timer Thread: {Thread.CurrentThread.ManagedThreadId}");

            //    Console.WriteLine($"Is Thread Pool: {Thread.CurrentThread.IsThreadPoolThread}");

            //    Console.WriteLine("Timer Action");

            //    timer.Stop();
            //};

            //timer.Start();

            //Console.WriteLine("Main Action.");
            //Console.WriteLine($"Main Thread: {Thread.CurrentThread.ManagedThreadId}");

            #endregion

            isInit = true; //has been initialized
        }

        public string GetShiftCode()
        {
            Dictionary<string, object> parm = new Dictionary<string, object>();
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "GetShiftCodeByStaffNo", Program.client.UserToken, JsonConvert.SerializeObject(parm));
            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(retJson["RetData"].ToString());
                if (dt.Rows.Count > 0)
                {
                    //When the working hours are saved in the personnel table, the working hours may not be the same day.
                    //If the working hours are not the same day, the default working hours will be displayed. If yes, select the saved working hours.
                    if (dt.Rows[0]["workhourdate"].ToDate().ToString("yyyy/MM/dd") == DateTime.Now.Date.ToDate().ToString("yyyy/MM/dd"))
                    {
                        string shiftCode = dt.Rows[0]["shiftcode"].ToString();
                        return !string.IsNullOrEmpty(shiftCode) ? shiftCode : "";
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }
            else
            {
                MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
                return "";
            }
        }

        private void GetModel()
        {
            Dictionary<string, object> parm = new Dictionary<string, object>();
            parm.Add("deptNo", txtDeptNo.Text);  
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "GetModel", Program.client.UserToken, JsonConvert.SerializeObject(parm));
            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
            string model = string.Empty;
            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(retJson["RetData"].ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach(DataRow dr in dt.Rows)
                    {
                      model += "'"+dr["MODEL_NAME"].ToString()+"'"+",";
                    }
                    //lblmodel.Text = dt.Rows[0]["MODELNAME"].ToString();
                    model = model.TrimEnd(',', ' ');
                    lblmodel.Text = model;
                }
            }
            else
            {
                MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
            }
        }

        private void GetDeptManpower()
        {
            Dictionary<string, object> parm = new Dictionary<string, object>();
            parm.Add("deptNo", txtDeptNo.Text); //Department = Work Center
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "GetDeptManpower", Program.client.UserToken, JsonConvert.SerializeObject(parm));
            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(retJson["RetData"].ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    txtOperator.Text = dt.Rows[0]["JOCKEY_QTY"].ToString();
                    txtWaterSpider.Text = dt.Rows[0]["UDF01"].ToString();
                    txtUniversalWork.Text = dt.Rows[0]["OMNIPOTENT_WORKER"].ToString();
                    //txtMultipleSequenceWork.Text = dt.Rows[0]["PLURIPOTENT_WORKER"].ToString();
                }
            }
            else
            {
                MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
            }
        }


        /// <summary>
        ///     Get hours
        /// </summary>
        private DataTable GetWorkTime(string hour, string udf05)
        {
            Dictionary<string, object> parm = new Dictionary<string, object>();
            //if (!string.IsNullOrEmpty(hour))
            //{
            //    parm.Add("shiftName", hour);
            //}

            if (!string.IsNullOrEmpty(udf05))
            {
                parm.Add("plant_code", udf05); //Factory center code
            }

            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "GetWorkTime", Program.client.UserToken, JsonConvert.SerializeObject(parm));
            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                DataTable dtJson = JsonConvert.DeserializeObject<DataTable>(retJson["RetData"].ToString());

                if (string.IsNullOrEmpty(hour))
                {
                    //bool isSaveWorkHour = false;
                    //DataTable dt = GetOnlineAndOffline(txtStaffDepartemnt.Text);//部门编号
                    //string shiftCode = "";
                    //if (dt != null && dt.Rows.Count > 0)
                    //{
                    //    if (!string.IsNullOrEmpty(dt.Rows[0]["shiftcode"].ToString()))
                    //    {
                    //        //这种做法就是保存了记录的工时是当天的就取当天的记录出来，然后显示出来。也可以实现，只是ShiftCode保存的是否要经常改变，拿第一条数据出来，也是一种很好的做法。
                    //        shiftCode = dt.Rows[0]["shiftcode"].ToString();
                    //        isSaveWorkHour = true;
                    //    }
                    //}

                    string hrShiftCode = GetShiftCode();

                    bool isHasSaved = false;

                    if (dtJson != null && dtJson.Rows.Count > 0)
                    {
                        comboBoxClasssHour.Items.Clear();
                        //comboBoxClasssHour.Items.Insert(0, "请选择");
                        int number = 0;
                        foreach (DataRow item in dtJson.Rows)
                        {
                            comboBoxClasssHour.Items.Add(item["shift_name"].ToString()); //This must be in front and must have a value to set the first item as the default item

                            comboBoxClasssHourIndexFormLoad = false; //Globally remove the flag that triggers the comboBoxClasssHour_SelectedIndexChanged method

                            string default_item = item["default_item"].ToString();

                            if (!string.IsNullOrEmpty(hrShiftCode) && item["shift_code"].ToString() == hrShiftCode)
                            {
                                //comboBoxClasssHour.SelectedIndex = number;//这里是不可以这样写，因为下面会触发comboBoxClasssHour_SelectedIndexChanged方法。
                                //comboBoxClasssHour.Text = tempHour;//这个也不行，因为会触发下面会comboBoxClasssHour_SelectedIndexChanged方法
                                //comboBoxClasssHour.SelectedText好像也不行,具体没有认真看

                                string tempHour = item["shift_name"].ToString();
                                comboBoxClasssHour.SelectedIndex = number; //As soon as the value is assigned, the comboBoxClasssHour_SelectedIndexChanged method is triggered immediately.
                                txtMorningOnTime.Text = item["am_from"].ToString();
                                txtMorningEndTime.Text = item["am_to"].ToString();
                                txtAfternoonOnTime.Text = item["pm_from"].ToString();
                                txtAfternoonEndTime.Text = item["pm_to"].ToString();
                                txtShiftCode.Text = item["shift_code"].ToString();
                                isHasSaved = true;
                            }

                            if (!string.IsNullOrWhiteSpace(default_item) && default_item == "Y" && !isHasSaved)
                            {
                                comboBoxClasssHour.SelectedIndex = number; //As soon as the value is assigned, the comboBoxClasssHour_SelectedIndexChanged method is triggered immediately.
                                txtMorningOnTime.Text = item["am_from"].ToString();
                                txtMorningEndTime.Text = item["am_to"].ToString();
                                txtAfternoonOnTime.Text = item["pm_from"].ToString();
                                txtAfternoonEndTime.Text = item["pm_to"].ToString();
                                txtShiftCode.Text = item["shift_code"].ToString();
                            }

                            number++;
                        }

                        comboBoxClasssHourIndexFormLoad = true;
                    }
                    else
                    {
                        comboBoxClasssHour.Text = "";
                    }
                }

                return dtJson;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                errorProvider1.SetError(comboBoxClasssHour, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString()); //shift error
                return null;
            }
        }

        /// <summary>
        ///     Get factory information code
        /// </summary>
        private string GetDept()
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
                    txtDeptName.Text = dtJson.Rows[0]["Department_Name"].ToString();
                    staff_no = dtJson.Rows[0]["STAFF_NO"].ToString();
                    txtDeptNo.Text = dtJson.Rows[0]["STAFF_DEPARTMENT"].ToString();
                    return dtJson.Rows[0]["udf05"].ToString();
                }
                else
                {
                    txtDeptName.Text = "";
                    return "";
                }
            }
            else
            {
                MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
                return "";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int number = 0;
            if (!string.IsNullOrEmpty(txtOperator.Text.Trim()))
            {
                number = int.Parse(txtOperator.Text.Trim());
            }

            if (number <= 1)
            {
                MessageHelper.ShowErr(this, "The number of operators must be greater than 1 to save");
                return;
            }

            if (dataGridViewOnLine.Rows.Count == 0 && dataGridViewOffLine.Rows.Count == 0)
            {
                MessageHelper.ShowErr(this, "There must be an employee online record or termination record to keep");
                return;
            }
            if (string.IsNullOrEmpty(txtWaterSpider.Text.Trim()))
            {
                MessageHelper.ShowErr(this, "WaterSpider count must be 0 or more");
                return;
            }
            if (string.IsNullOrEmpty(txtUniversalWork.Text.Trim()))
            {
                MessageHelper.ShowErr(this, "Full Skill Count must be 0 or more");
                return;
            }

            int TotalEmp = Convert.ToInt32(txtUniversalWork.Text) + Convert.ToInt32(txtMultipleSequenceWork.Text) + Convert.ToInt32(txtWaterSpider.Text) + Convert.ToInt32(txtOperator.Text);

            if(TotalEmp != Convert.ToInt32(txtOnlineNumber.Text))
            {
                MessageHelper.ShowErr(this, "Online Count is not equal to mentioned count");
                return;
            }
            if(string.IsNullOrEmpty(comboBoxClasssHour.Text))
            {
                MessageHelper.ShowErr(this, "Shift Code cannot be empty");
                return;
            }

            string workCenter = txtDeptNo.Text; //Work center number
            string date = txtDate.Text; //date
            string operator1 = txtOperator.Text; //operator
            string waterSpider = txtWaterSpider.Text; //water spider
            string universalWork = txtUniversalWork.Text; //all-rounder
            string multipleSequenceWork = txtMultipleSequenceWork.Text; //Multi-process
            string morningOnTime = txtMorningOnTime.Text;
            string morningOffTime = txtMorningEndTime.Text;
            string afternoonTime = txtAfternoonOnTime.Text;
            string afternoonEndTime = txtAfternoonEndTime.Text;

            //DataTable dt1 = new DataTable();

            //if (dataGridViewOnLine.Rows.Count > 0)
            //{
            //    foreach (DataGridViewColumn column in dataGridViewOnLine.Columns)
            //        dt1.Columns.Add(column.HeaderText);


            //    foreach (DataGridViewRow row in dataGridViewOnLine.Rows)
            //    {
            //        DataRow dRow = dt1.NewRow();
            //        foreach (DataGridViewCell cell in row.Cells)
            //        {
            //            dRow[cell.ColumnIndex] = cell.Value;
            //        }
            //        dt1.Rows.Add(dRow);

            //    }
            //}
            Dictionary<string, object> parm = new Dictionary<string, object>();
            parm.Add("workCenter", workCenter);
            parm.Add("date", date);
            parm.Add("operator1", operator1);
            parm.Add("waterSpider", waterSpider);
            parm.Add("universalWork", universalWork);
            parm.Add("multipleSequenceWork", multipleSequenceWork);
            parm.Add("shiftcode", txtShiftCode.Text); //Shift code
            parm.Add("morningOnTime", morningOnTime);
            parm.Add("morningOffTime", morningOffTime);
            parm.Add("afternoonOnTime", afternoonTime);
            parm.Add("afternoonEndTime", afternoonEndTime);
            parm.Add("staffNo", staff_no);
            //parm.Add("dt", dt1);//Added by Ashok on 2024/09/26 for selecting Working skill
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "InsertWorkingTime", Program.client.UserToken, JsonConvert.SerializeObject(parm));
            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                string json = retJson["RetData"].ToString();

                if (!string.IsNullOrEmpty(json))
                {
                    MessageHelper.ShowSuccess(this, "Successfully saved！");
                }
                else
                {
                    MessageHelper.ShowErr(this, "Failed to save！");
                }
            }
            else
            {
                MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
            }
        }

        #region Method Added by Ashok to autosave Scan Details on 2026/02/05
        public void SaveWorkingHoursData()
        {
            int number = 0;
            if (!string.IsNullOrEmpty(txtOperator.Text.Trim()))
            {
                number = int.Parse(txtOperator.Text.Trim());
            }

            if (number <= 1)
            {
                MessageHelper.ShowErr(this, "The number of operators must be greater than 1 to save");
                return;
            }

            if (dataGridViewOnLine.Rows.Count == 0 && dataGridViewOffLine.Rows.Count == 0)
            {
                MessageHelper.ShowErr(this, "There must be an employee online record or termination record to keep");
                return;
            }
            if (string.IsNullOrEmpty(txtWaterSpider.Text.Trim()))
            {
                MessageHelper.ShowErr(this, "WaterSpider count must be 0 or more");
                return;
            }
            if (string.IsNullOrEmpty(txtUniversalWork.Text.Trim()))
            {
                MessageHelper.ShowErr(this, "Full Skill Count must be 0 or more");
                return;
            }

            int TotalEmp = Convert.ToInt32(txtUniversalWork.Text) + Convert.ToInt32(txtMultipleSequenceWork.Text) + Convert.ToInt32(txtWaterSpider.Text) + Convert.ToInt32(txtOperator.Text);

            if (TotalEmp != Convert.ToInt32(txtOnlineNumber.Text))
            {
                MessageHelper.ShowErr(this, "Online Count is not equal to mentioned count");
                return;
            }
            if (string.IsNullOrEmpty(comboBoxClasssHour.Text))
            {
                MessageHelper.ShowErr(this, "Shift Code cannot be empty");
                return;
            }

            string workCenter = txtDeptNo.Text; //Work center number
            string date = txtDate.Text; //date
            string operator1 = txtOperator.Text; //operator
            string waterSpider = txtWaterSpider.Text; //water spider
            string universalWork = txtUniversalWork.Text; //all-rounder
            string multipleSequenceWork = txtMultipleSequenceWork.Text; //Multi-process
            string morningOnTime = txtMorningOnTime.Text;
            string morningOffTime = txtMorningEndTime.Text;
            string afternoonTime = txtAfternoonOnTime.Text;
            string afternoonEndTime = txtAfternoonEndTime.Text;
            
            Dictionary<string, object> parm = new Dictionary<string, object>();
            parm.Add("workCenter", workCenter);
            parm.Add("date", date);
            parm.Add("operator1", operator1);
            parm.Add("waterSpider", waterSpider);
            parm.Add("universalWork", universalWork);
            parm.Add("multipleSequenceWork", multipleSequenceWork);
            parm.Add("shiftcode", txtShiftCode.Text); //Shift code
            parm.Add("morningOnTime", morningOnTime);
            parm.Add("morningOffTime", morningOffTime);
            parm.Add("afternoonOnTime", afternoonTime);
            parm.Add("afternoonEndTime", afternoonEndTime);
            parm.Add("staffNo", staff_no);
            //parm.Add("dt", dt1);//Added by Ashok on 2024/09/26 for selecting Working skill
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "InsertWorkingTime", Program.client.UserToken, JsonConvert.SerializeObject(parm));
            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                string json = retJson["RetData"].ToString();

                if (!string.IsNullOrEmpty(json))
                {
                    MessageHelper.ShowSuccess(this, "Successfully saved！");
                }
                else
                {
                    MessageHelper.ShowErr(this, "Failed to save！");
                }
            }
            else
            {
                MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
            }
        }
        #endregion
        private void comboBoxClasssHour_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxClasssHourIndexFormLoad)
                {
                    foreach (DataRow item in workTimeDtJson.Rows)
                    {
                        if (item["shift_name"].ToString() == comboBoxClasssHour.Text)
                        {
                            //comboBoxClasssHour.SelectedText = comboBoxClasssHour.Text;//这个不要，不然会造成循环调用
                            //comboBoxClasssHour.SelectedIndex = 1;//这个不要，不然会造成循环调用

                            txtMorningOnTime.Text = item["am_from"].ToString();
                            txtMorningEndTime.Text = item["am_to"].ToString();
                            txtAfternoonOnTime.Text = item["pm_from"].ToString();
                            txtAfternoonEndTime.Text = item["pm_to"].ToString();
                            txtShiftCode.Text = item["shift_code"].ToString();
                            break;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw exception;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="isFirstLoad">Form interface is loaded for the first time</param>
        /// <param name="isExists">whether the employee exists</param>
        private void GetOnlineAndOfflineList(Dictionary<string, object> dictionary, bool isFirstLoad, bool isExists = false)
        {
            bool isLeaved = false; //have left
            bool isNoPerson = false; //no such employee
            dictionary["status"] = 0; //online record

            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "Get_SFC_ONLINEANDOFFLINE_M_List", Program.client.UserToken, JsonConvert.SerializeObject(dictionary));

            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                DataTable dtJson2 = JsonConvert.DeserializeObject<DataTable>(retJson["RetData"].ToString());

                if (dtJson2 != null && dtJson2.Rows.Count > 0)
                {
                    dtJson2.DefaultView.Sort = "starttime ASC";
                    dtJson2 = dtJson2.DefaultView.ToTable();

                    var multiSkillEmployees = dtJson2.AsEnumerable()
                      .Where(row => row.Field<string>("Skill_type") == "Multi_Skill");
                   
                    if (multiSkillEmployees.Any())
                    {
                        DataTable dt1 = multiSkillEmployees.CopyToDataTable();
                        txtMultipleSequenceWork.Text = dt1.Rows.Count.ToString();
                    }
                    else
                    {
                        txtMultipleSequenceWork.Text = "0";
                    }


                    //DataTable dt1 = dtJson2.AsEnumerable()
                    //           .Where(row => row.Field<string>("Skill_type") == "Multi_Skill")
                    //           .CopyToDataTable(); // Create a new DataTable with filtered rows


                    dataGridViewOnLine.AutoGenerateColumns = false;
                    dataGridViewOnLine.DataSource = dtJson2;
                    
                    if (!isFirstLoad && !isExists)
                    {
                        labelErrorMsg.Visible = true;
                        //if (dtJson2.Rows[dtJson2.Rows.Count - 1]["EmpNo"].ToString() == txtQRCode.Text.Trim())
                        if (dtJson2.Rows[dtJson2.Rows.Count - 1]["EmpNo"].ToString() == txtQRCode.Text.Trim().TrimStart('0'))// //Added by Ashok on 20240730
                        {
                            labelErrorMsg.Visible = true;
                            //labelErrorMsg.Text = $"Scan was successful \r\n\r\n Staff：{dtJson2.Rows[dtJson2.Rows.Count - 1]["EmpName"]}，joined";
                            labelErrorMsg.Text = $"Scan was successful.  Staff：{dtJson2.Rows[dtJson2.Rows.Count - 1]["EmpName"]}，joined";
                            panel1.BackColor = Color.Green;
                            labelErrorMsg.ForeColor = Color.White;
                        }
                        else
                        {
                            labelErrorMsg.Visible = true;
                            labelErrorMsg.Text = "Scan failed! If there is no such employee, please scan the correct employee QR code。";
                            panel1.BackColor = Color.Red;
                            labelErrorMsg.ForeColor = Color.White;
                            isNoPerson = true;
                        }
                    }
                    else if (isExists)
                    {
                        labelErrorMsg.Visible = true;
                        //if (dtJson2.Rows[dtJson2.Rows.Count - 1]["EmpNo"].ToString() == txtQRCode.Text.Trim())
                        if (dtJson2.Rows[dtJson2.Rows.Count - 1]["EmpNo"].ToString() == txtQRCode.Text.Trim().TrimStart('0'))// //Added by Ashok on 20240730
                        {
                            labelErrorMsg.Visible = true;
                            //labelErrorMsg.Text = $"Scan was successful \r\n\r\n Staff：{dtJson2.Rows[dtJson2.Rows.Count - 1]["EmpName"]}，joined";
                            labelErrorMsg.Text = $"Scan was successful. Staff：{dtJson2.Rows[dtJson2.Rows.Count - 1]["EmpName"]}，joined";
                            panel1.BackColor = Color.Green;
                            labelErrorMsg.ForeColor = Color.White;
                            //labelErrorMsg.ForeColor = Color.Green;
                        }
                        else
                        {
                            isLeaved = true;
                        }
                    }
                }
                else
                {
                    dataGridViewOnLine.DataSource = null;
                    if (!isFirstLoad && !isExists)
                    {
                        labelErrorMsg.Visible = true;
                        labelErrorMsg.Text = "Scan failed! If there is no such employee, please scan the correct employee QR code。";
                        //labelErrorMsg.ForeColor = Color.Red;
                        panel1.BackColor = Color.Red;
                        labelErrorMsg.ForeColor = Color.White;
                        isNoPerson = true;
                    }
                    else if (!isFirstLoad && isExists)
                    {
                        isLeaved = true;
                    }
                }
            }

            if (isNoPerson)
            {
                return;
            }

            dictionary["status"] = 1; //Offline recording
            string ret3 = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "Get_SFC_ONLINEANDOFFLINE_M_List", Program.client.UserToken, JsonConvert.SerializeObject(dictionary));
            var ret3Json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret3);
            if (Convert.ToBoolean(ret3Json["IsSuccess"]))
            {
                string json3 = ret3Json["RetData"].ToString();

                DataTable dtJson3 = JsonConvert.DeserializeObject<DataTable>(json3);

                if (dtJson3 != null && dtJson3.Rows.Count > 0)
                {
                    dtJson3.DefaultView.Sort = "starttime ASC";
                    dtJson3 = dtJson3.DefaultView.ToTable();

                    dataGridViewOffLine.AutoGenerateColumns = false;
                    dataGridViewOffLine.DataSource = dtJson3;
                }

                if (isLeaved)
                {
                    foreach (DataRow item in dtJson3.Rows)
                    {
                       // if (item["EmpNo"].ToString() == txtQRCode.Text.Trim()) //As long as the employee number is determined here, the employee name can be determined. to return immediately.
                        if (item["EmpNo"].ToString() == txtQRCode.Text.Trim().TrimStart('0')) //Added by Ashok on 20240730
                        {
                            labelErrorMsg.Visible = true;
                            labelErrorMsg.Text = $"Scan was successful. Staff：{item["EmpName"]}，has left";
                            ///labelErrorMsg.Text = $"Scan was successful \r\n\r\n Staff：{item["EmpName"]}，has left";
                            // labelErrorMsg.ForeColor = Color.Red;

                            panel1.BackColor = Color.Red;
                            labelErrorMsg.ForeColor = Color.White;
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        ///     insert employee
        /// </summary>
        /// <param name="empNo">staff code</param>
        private void InsertPerson(string empNo)
        {
            if (comboBoxClasssHour.Items.Count == 0)
            {
                MessageHelper.ShowErr(this, "You didn't set a shift! Please go to set the shift information first, then select the shift and then scan");
                return;
            }
            else if (comboBoxClasssHour.Items.Count > 0 && string.IsNullOrWhiteSpace(comboBoxClasssHour.Text))
            {
                MessageHelper.ShowErr(this, "You don't have a shift! Please select a shift before scanning");
                return;
            } 
            labelErrorMsg.Text = "";
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("empNO", empNo);
            p.Add("deptNo", txtDeptNo.Text);
            p.Add("deptName", txtDeptName.Text);
            p.Add("shiftCode", txtShiftCode.Text);

            DataTable dtMv_Ep_Main = GetMv_Ep_MainPerson(empNo, txtDeptNo.Text);//txtDeptNo.Text Added by Ashok
            if (dtMv_Ep_Main != null && dtMv_Ep_Main.Rows.Count > 0)
            {
                string empName = dtMv_Ep_Main.Rows[0]["EmpName"].ToString();
                p.Add("empName", empName);
            }
            else
            {
                MessageHelper.ShowErr(this, "no such employee，add failed！");
                return;
            }
            #region Added by Ashok for selecting working skill on 2024/09/27
            bool isOnlinePerson = IsOnlinePerson(empNo, txtDeptNo.Text);
            if (!isOnlinePerson)
            {
                string Barcode = txtQRCode.Text.Trim().TrimStart('0');
                string DeptNo = txtDeptNo.Text;
                string ModelName = lblmodel.Text;

                using (Select_Skill popup = new Select_Skill(Barcode, DeptNo, ModelName))
                {
                    popup.ShowDialog(this);
                    string result = popup.Result;
                    //if(result=="2")
                    //{
                    //    //MessageHelper.ShowErr(this, "Skill Count Exceeds the ME Standard Count");
                    //    labelErrorMsg.Visible = true;
                    //    labelErrorMsg.Text = "Skill Count Exceeds the ME Standard Count";
                    //    panel1.BackColor = Color.Red;
                    //    labelErrorMsg.ForeColor = Color.White;
                    //    return;
                    //}
                }
            }
            #endregion
            bool isExistsPerson = IsExistsPerson(empNo, txtDeptNo.Text); //If it exists, it is the second punch, which is offline recording

            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "InsertPerson", Program.client.UserToken, JsonConvert.SerializeObject(p));

            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                int count = int.Parse(retJson["RetData"].ToString());
                if (count > 0)
                { 
                    GetOnlineAndOfflineList(p, false, isExistsPerson);
                    txtOnlineNumber.Text = dataGridViewOnLine.RowCount.ToString();
                }
                else
                { 
                    //Added by Ashok on 20240726
                    labelErrorMsg.Visible = true;
                    labelErrorMsg.Text = retJson["ErrMsg"].ToString();
                    panel1.BackColor = Color.Red;
                    labelErrorMsg.ForeColor = Color.White;

                    // MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
                }
            }
            else
            {
                //Added by Ashok on 20240726
                labelErrorMsg.Visible = true;
                labelErrorMsg.Text = retJson["ErrMsg"].ToString();
                panel1.BackColor = Color.Red;
                labelErrorMsg.ForeColor = Color.White;
                //MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
            }
        }
        private bool IsOnlinePerson(string empNo, string Staff_Dept)   //Added by Ashok
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("empNo", empNo);
            p.Add("Staff_Dept", Staff_Dept);   
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "IsOnlinePerson", Program.client.UserToken, JsonConvert.SerializeObject(p));
            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                bool isExists = JsonConvert.DeserializeObject<bool>(retJson["RetData"].ToString());
                return isExists;
            }
            else
            {
                MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
                return false;
            }
        }

        private bool IsExistsPerson(string empNo,string Staff_Dept)  //Staff_Dept Added by Ashok
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("empNo", empNo);
            p.Add("Staff_Dept", Staff_Dept);  //Added by Ashok
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "IsExistsPerson", Program.client.UserToken, JsonConvert.SerializeObject(p));
            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                bool isExists = JsonConvert.DeserializeObject<bool>(retJson["RetData"].ToString());
                return isExists;
            }
            else
            {
                MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
                return false;
            }
        }

        private DataTable GetMv_Ep_MainPerson(string empNo,string Staff_Dept)  //Staff_Dept Added by Ashok
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("empNo", empNo);
            p.Add("Staff_Dept", Staff_Dept);//Added by Ashok
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "GetMv_Ep_MainPerson", Program.client.UserToken, JsonConvert.SerializeObject(p));
            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(retJson["RetData"].ToString());
                return dt;
            }
            else
            {
                MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
                return null;
            }
        }


        /// <summary>
        ///     stop completely
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime endTime = new DateTime();

            if (!isInit)
            {
                return;
            }

            if (!string.IsNullOrWhiteSpace(txtAfternoonEndTime.Text) && txtAfternoonEndTime.Text != DateTime.MinValue.ToString())
            {
                endTime = DateTime.Parse(txtAfternoonEndTime.Text);
            }
            else
            {
                return;
            }

            if (DateTime.Now > endTime) //The time endTime is only used to make judgments and is not used below, otherwise the optional time will cause errors。
            {
                DataTable dtDeptNos = GetWorkTime();

                int totalCount = 0; //Total number of updates
                if (dtDeptNos != null)
                {
                    foreach (DataRow row in dtDeptNos.Rows)
                    {
                        Dictionary<string, object> parm = new Dictionary<string, object>();
                        parm.Add("isOverTime", true); //Whether it exceeds the effective working hours range of the shift
                        parm.Add("status", 1); //Offline, if it exists, it is the second punch, which is offline recording
                        parm.Add("deptNo", txtDeptNo.Text); //Department = Work Center
                        parm.Add("startTime", row["startTime"].ToString());
                        parm.Add("empNo", row["empNo"].ToString());
                        parm.Add("onlinetime", row["onlinetime"].ToString());
                        DataTable workTimeDt = GetWorkTime(row["shiftCode"].ToString());
                        if (workTimeDt != null && workTimeDt.Rows.Count > 0)
                        {
                            parm.Add("morningOnTime", workTimeDt.Rows[0]["am_from"].ToString());
                            parm.Add("morningOffTime", workTimeDt.Rows[0]["am_to"].ToString());
                            parm.Add("afternoonOnTime", workTimeDt.Rows[0]["pm_from"].ToString());
                            parm.Add("afternoonEndTime", workTimeDt.Rows[0]["pm_to"].ToString());
                        }

                        string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "OverTimeCount", Program.client.UserToken, JsonConvert.SerializeObject(parm));

                        var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
                        if (retJson["IsSuccess"].ToBool())
                        {
                            string insertCountJson = retJson["RetData"].ToString();

                            if (!string.IsNullOrEmpty(insertCountJson))
                            {
                                totalCount += int.Parse(insertCountJson);
                            }
                        }
                        else
                        {
                            MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
                            return;
                        }
                    }

                    MessageHelper.ShowSuccess(this, $"Automatically updated successfully{totalCount}Records！");

                    Dictionary<string, object> parm2 = new Dictionary<string, object>();
                    parm2.Add("deptNo", txtDeptNo.Text); //Department = Work Center
                    GetOnlineAndOfflineList(parm2, false, true);
                }
            }
        }

        private DataTable GetWorkTime()
        {
            Dictionary<string, object> parm = new Dictionary<string, object>();
            parm.Add("status", 0); //online record。

            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "GetOnlineAndOfflineDepts", Program.client.UserToken, JsonConvert.SerializeObject(parm));

            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);

            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                DataTable dtDeptNos = JsonConvert.DeserializeObject<DataTable>(retJson["RetData"].ToString());
                return dtDeptNos;
            }
            else
            {
                MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
                return null;
            }
        }


        /// <summary>
        ///     Get hours
        /// </summary>
        private DataTable GetWorkTime(string shift_code)
        {
            Dictionary<string, object> parm = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(shift_code))
            {
                parm.Add("shift_code", shift_code);
            }

            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "GetWorkTime", Program.client.UserToken, JsonConvert.SerializeObject(parm));
            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                DataTable dtJson = JsonConvert.DeserializeObject<DataTable>(retJson["RetData"].ToString());
                return dtJson;
            }
            else
            {
                MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
                return null;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> parm = new Dictionary<string, object>();
            parm.Add("status", 0); //online record
            parm.Add("deptNo", txtDeptNo.Text); //Department = Work Center
            GetOnlineAndOfflineList(parm, true);
        }

        private void txtQRCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) //Triggered when the OK button is pressed
            {
                if (!IsWaterSpiderValid())
                {
                    MessageBox.Show("Please enter Water Spider count before scanning.");
                    txtWaterSpider.ReadOnly = false;
                    return;
                }
                else
                {
                    txtWaterSpider.ReadOnly = true;
                }

                InsertPerson(txtQRCode.Text.Trim().TrimStart('0')); // //Added by Ashok on 20240730
                txtQRCode.Text = "";
                #region To Check Employee Department before scanning
                //if (Check_Employee_Dept(txtQRCode.Text.Trim().TrimStart('0'),txtDeptName.Text))
                //{
                //    //InsertPerson(txtQRCode.Text.Trim()); // 
                //    InsertPerson(txtQRCode.Text.Trim().TrimStart('0')); // //Added by Ashok on 20240730
                //    txtQRCode.Text = "";
                //}
                //else
                //{
                //    labelErrorMsg.Visible = true;
                //    labelErrorMsg.Text ="This Employee is not belongs to this line or swapped to another line";
                //    panel1.BackColor = Color.Red;
                //    labelErrorMsg.ForeColor = Color.White;
                //}
                #endregion
            }
        }


        #region Added by Ashok to Autosave EmployeeDetails on 2026/02/04
        private bool IsWaterSpiderValid()
        {
            return int.TryParse(txtWaterSpider.Text, out int ws) && ws > 0;
        }
        #endregion

        #region Added by Ashok for selecting working skill on 2024/09/26
        //public void GetSkillsList(string Barcode)
        //{
        //    try
        //    {
        //        Dictionary<string, object> retData = new Dictionary<string, object>();
        //        retData.Add("Barcode", Barcode);

        //        string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder",
        //                                    "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer",
        //                                    "GetSkillsList",
        //            Program.client.UserToken,
        //            Newtonsoft.Json.JsonConvert.SerializeObject(retData)
        //        );
        //        ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
        //        if (ret.IsSuccess)
        //        {
        //            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
        //            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
        //            lt.Clear();
        //            foreach (DataRow dr in dt.Rows)
        //            {
        //                lt.Add(dr["skill_name"].ToString());
        //            }
        //            SkillNames = new ComboBox();
        //            SkillNames.DataSource = lt; 
        //        }
        //        else
        //        {
        //            SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
        //    }
        //}



        private void dataGridViewOnLine_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex > -1)
            //{
            //    if (dataGridViewOnLine.Columns[e.ColumnIndex].Name == "Skill_Name")
            //    {
            //        string Barcode = dataGridViewOnLine.Rows[dataGridViewOnLine.CurrentCell.RowIndex].Cells["EmpNo"].Value.ToString();
            //        GetSkillsList(Barcode);
            //        SkillNames = new ComboBox();
            //        SkillNames.Enabled = true;
            //        SkillNames.DropDownStyle = ComboBoxStyle.DropDownList;
            //        SkillNames.DataSource = lt;
            //        SkillNames.DisplayMember = "NAME";
            //        SkillNames.ValueMember = "NAME";

            //        Rectangle rect = dataGridViewOnLine.GetCellDisplayRectangle(dataGridViewOnLine.CurrentCell.ColumnIndex, dataGridViewOnLine.CurrentCell.RowIndex, false);
            //        SkillNames.Left = rect.Left;
            //        SkillNames.Top = rect.Top;
            //        SkillNames.Width = rect.Width;
            //        SkillNames.Height = rect.Height;
            //        SkillNames.Visible = true;
            //        dataGridViewOnLine.Controls.Add(SkillNames);
            //        if (dataGridViewOnLine.Rows[e.RowIndex].Cells["Skill_Name"].Value != null && !string.IsNullOrEmpty(dataGridViewOnLine.Rows[e.RowIndex].Cells["Skill_Name"].Value.ToString()))
            //        {
            //            SkillNames.SelectedValue = dataGridViewOnLine.Rows[e.RowIndex].Cells["Skill_Name"].Value.ToString();
            //        }
            //        else
            //        {
            //            SkillNames.SelectedIndex = 0;
            //        }
            //        SkillNames.Focus();
            //        SkillNames.SelectedIndexChanged += ProcessList_SelectedIndexChanged1;
            //        dataGridViewOnLine.CellEndEdit += dataGridViewOnLine_CellEndEdit;
            //    }
            //}
        }

        private void dataGridViewOnLine_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // dataGridViewOnLine.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        //private void ProcessList_SelectedIndexChanged1(object sender, EventArgs e)
        //{
        //    dataGridViewOnLine.CurrentCell.Value = SkillNames.Text;
        //    dataGridViewOnLine.Rows[dataGridViewOnLine.CurrentCell.RowIndex].Cells["Skill_Name"].Value = SkillNames.SelectedValue;
        //    SkillNames.Visible = false;
        //    SkillNames.Dispose();
        //}


        #endregion


        #region Added by Ashok for system enhancement on 2025/07/15
        private void TxtOperator_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; 
            }
        }

        private void TxtWaterSpider_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; 
            }
        }

        private void TxtUniversalWork_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtMultipleSequenceWork_TextChanged(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtMultipleSequenceWork.Text) || Convert.ToInt32(txtMultipleSequenceWork.Text)==0 || (string.IsNullOrEmpty(txtOnlineNumber.Text)) || Convert.ToInt32(txtOnlineNumber.Text) == 0)
            {
                txt_multi_percent.Text = "0";
            }
            else
            { 
                txt_multi_percent.Text = ((Convert.ToDouble(txtMultipleSequenceWork.Text) / Convert.ToDouble(txtOnlineNumber.Text)) * 100).ToString("F2");
            }
        }

        private void TxtUniversalWork_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUniversalWork.Text) || Convert.ToInt32(txtUniversalWork.Text) == 0 || (string.IsNullOrEmpty(txtOnlineNumber.Text))||Convert.ToInt32(txtOnlineNumber.Text) == 0)
            {
                txt_full_percent.Text = "0";
            }
            else
            {
                txt_full_percent.Text = ((Convert.ToDouble(txtUniversalWork.Text) / Convert.ToDouble(txtOnlineNumber.Text)) * 100).ToString("F2");
            }
        }

        private void TxtOnlineNumber_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMultipleSequenceWork.Text) || Convert.ToInt32(txtMultipleSequenceWork.Text) == 0 || (string.IsNullOrEmpty(txtOnlineNumber.Text)) || Convert.ToInt32(txtOnlineNumber.Text) == 0)
            {
                txt_multi_percent.Text = "0";
            }
            else
            {
                txt_multi_percent.Text = ((Convert.ToDouble(txtMultipleSequenceWork.Text) / Convert.ToDouble(txtOnlineNumber.Text)) * 100).ToString("F2");
            }

            if (string.IsNullOrEmpty(txtUniversalWork.Text) || Convert.ToInt32(txtUniversalWork.Text) == 0 || (string.IsNullOrEmpty(txtOnlineNumber.Text)) || Convert.ToInt32(txtOnlineNumber.Text) == 0)
            {
                txt_full_percent.Text = "0";
            }
            else
            {
                txt_full_percent.Text = ((Convert.ToDouble(txtUniversalWork.Text) / Convert.ToDouble(txtOnlineNumber.Text)) * 100).ToString("F2");
            }

            if (string.IsNullOrEmpty(txtMultipleSequenceWork.Text)  || string.IsNullOrEmpty(txtOnlineNumber.Text))
            {
                txtOperator.Text = "0";
            }
            else
            {
                txtOperator.Text = Convert.ToInt32(txtOnlineNumber.Text) - Convert.ToInt32(txtMultipleSequenceWork.Text)- Convert.ToInt32(txtWaterSpider.Text) >= 0 ? (Convert.ToInt32(txtOnlineNumber.Text) - Convert.ToInt32(txtMultipleSequenceWork.Text) - Convert.ToInt32(txtWaterSpider.Text)).ToString() : "0";
            }

            if (string.IsNullOrEmpty(txtOnlineNumber.Text) || Convert.ToInt32(txtOnlineNumber.Text)==0)
            {
                txtWaterSpider.Text = "";
                txtWaterSpider.ReadOnly = false;
            }

        }

        public bool Check_Employee_Dept(string EmpNo,string Dept)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("EmpNo", EmpNo);
            p.Add("Dept", Dept);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.GeneralServer", "Check_Employee_Dept", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            return ret.IsSuccess;
        }

        #endregion

        private void TxtOperator_TextChanged(object sender, EventArgs e)
        {

            if (int.TryParse(txtOperator.Text, out int op) && int.TryParse(txtOnlineNumber.Text, out int online) &&int.TryParse(txtMultipleSequenceWork.Text, out int multi) &&int.TryParse(txtWaterSpider.Text, out int ws) && op > 1 && online == (multi + op + ws))
                SaveWorkingHoursData();


           
        }
    }
}