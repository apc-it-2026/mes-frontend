using F_Sample_SendReceive_Manage;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static F_SendReceive_Manage.BadList;

namespace F_SendReceive_Manage
{
    public partial class Bad_Registration : MaterialForm
    {
        private string colorWay = "";
        private string MODEL_MASTER = "";
        private string PATTERN_MASTER = "";
        private ComboBox comboBox1;
        private DataTable CausesDataTable;
        private DataTable StationDataTable;


        public Bad_Registration()
        {
            InitializeComponent();
            
        }

        public class ComboboxEntry
        {
            public string ENUM_CODE { get; set; }
            public string ENUM_VALUE { get; set; }
        }
        public DataTable ComboxBASE098MUI() 
        {
            DataTable dataTable = null;
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.SCM_Sample_ReceiveService", "GetComboxBASE098M", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(string.Empty));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string retData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dataTable = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(retData);
            }
            return dataTable;
        }

        public DataTable GetComboxSTATION()
        {
            DataTable dataTable = null;
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.SCM_Sample_ReceiveService", "GetComboxSTATION", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(string.Empty));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string retData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dataTable = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(retData);
            }
            return dataTable;
        }
        public Bad_Registration(string sample_no,string Art_no,string art_name,string purpose,string season,string colorWay,string MODEL_MASTER,string PATTERN_MASTER)
        {
            InitializeComponent();
            this.lbSampleNo.Text = sample_no;
            lbArtNo.Text = Art_no;
            lbArtName.Text = art_name;
            lbPurpose.Text = purpose;
            lbSeason.Text = season;
            lbRegisterDate.Text = "";
            this.colorWay = colorWay;
            this.MODEL_MASTER = MODEL_MASTER;
            this.PATTERN_MASTER = PATTERN_MASTER;

            DataTable dataTable = ComboxBASE098MUI();
            CausesDataTable = dataTable;
            List<string> list = new List<string>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                list.Add(dataTable.Rows[i]["code_name"].ToString());
            }
            //causes.DataSource = list;
            DataTable dataTable1 = GetComboxSTATION();
            StationDataTable = dataTable1;
            List<ComboboxEntry> STATION = new List<ComboboxEntry>();
            STATION.Add(new ComboboxEntry() { ENUM_CODE = "", ENUM_VALUE = "" });
            for (int i = 0; i < dataTable1.Rows.Count; i++)
            {
                STATION.Add(new ComboboxEntry() { ENUM_CODE = dataTable1.Rows[i]["station_no"].ToString(), ENUM_VALUE = dataTable1.Rows[i]["station_name"].ToString() });
            }
           // responsible_unit.DataSource = STATION;
            //responsible_unit.DisplayMember = "ENUM_VALUE";
           // responsible_unit.ValueMember = "ENUM_CODE";
            this.comboBox1 = new ComboBox();
            comboBox1.Visible = false;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            dataGridView1.Controls.Add(comboBox1);
            comboBox1.DropDownClosed += new EventHandler(comBox1_DropDownClosed);
            
        }
        DataGridViewRow p = new DataGridViewRow();

        private void btnInsertData_Click(object sender, EventArgs e)
        {
            BadList badList = new BadList(lbSampleNo.Text.Trim().ToString());
            badList.DataChangeItem += new BadList.DataChangeHandlerItem(DataChanged_DataGridRow);
            badList.ShowDialog();
         
        }

        public void DataChanged_DataGridRow(object sender, DataChangeEventArgsItem args)
        {
            p = args.rows;
            int v = dataGridView1.Rows.Add(); 
            
            dataGridView1.Rows[v].Cells["part_no"].Value = p.Cells["part_no"].Value.ToString();
            dataGridView1.Rows[v].Cells["part_name"].Value = p.Cells["part_name"].Value.ToString();
            dataGridView1.Rows[v].Cells["suppliers_code"].Value = p.Cells["suppliers_code"].Value.ToString();
            dataGridView1.Rows[v].Cells["suppliers_name"].Value = p.Cells["suppliers_name"].Value.ToString();
            dataGridView1.Rows[v].Cells["process_no"].Value = p.Cells["process_no"].Value.ToString();
            dataGridView1.Rows[v].Cells["process_name"].Value = p.Cells["process_name"].Value.ToString();
            //dataGridView1.Rows[v].Cells["Column12"].Value = p.Cells["part_no"].Value.ToString();
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("sample_no", lbSampleNo.Text.Trim().ToString());
            dictionary.Add("part_no", p.Cells["part_no"].Value.ToString());

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.SCM_Sample_ReceiveService", "QuerySizeNo", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(dictionary));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"])) 
            {
                comboBox1.DataSource = null;
                string retData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                Dictionary<string, string> dictionary2 = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(retData);
                DataTable dataTable = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dictionary2["size"]);
                List<string> list = new List<string>();
              //  list.Add("");
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    list.Add(dataTable.Rows[i]["SIZE_NO"].ToString());
                }
                comboBox1.DataSource = list;
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
           /* if (dataGridView1.CurrentCell.ColumnIndex == 7) 
            {
                ((ComboBox)e.Control).DropDownClosed -=new EventHandler(ComboBox_SelectedIndexChanged);
                ((ComboBox)e.Control).DropDownClosed += new EventHandler(ComboBox_SelectedIndexChanged);
            }*/
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rowIndex = dataGridView1.CurrentRow.Index;
            //Console.WriteLine(dataGridView1.Rows[rowIndex].Cells["Column5"].EditedFormattedValue);
            // DataGridViewRowCollection rows = dataGridView1.Rows;
            //Console.WriteLine(Column5.DataPropertyName);
            string part_no = dataGridView1.Rows[rowIndex].Cells["part_no"].Value.ToString();
            string suppliers_code = dataGridView1.Rows[rowIndex].Cells["suppliers_code"].Value.ToString();
            string process_no = dataGridView1.Rows[rowIndex].Cells["process_no"].Value.ToString();
            string size_no = dataGridView1.Rows[rowIndex].Cells["size_no"].EditedFormattedValue.ToString();
            //MessageBox.Show("成功");
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                string part_no1 = dataGridView1.Rows[i].Cells["part_no"].Value.ToString();
                string suppliers_code1 = dataGridView1.Rows[i].Cells["suppliers_code"].Value.ToString();
                string process_no1 = dataGridView1.Rows[i].Cells["process_no"].Value.ToString();
                string size_no1 = dataGridView1.Rows[i].Cells["size_no"].EditedFormattedValue.ToString();
                if (i == dataGridView1.CurrentRow.Index)
                {
                    continue;
                }
                if (part_no1 == part_no && suppliers_code == suppliers_code1 && process_no == process_no1 && size_no == size_no1)
                {
                    MessageBox.Show("存在相同的行，请勿重复添加");
                    return;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (lbBadRecordNo.Text != "") 
            {
                MessageBox.Show("凭证已保存！");
                return;
            }
            if (dataGridView1.Rows.Count <= 0)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("列表无数据保存", Program.client, "", Program.client.Language);
                MessageBox.Show(msg);
                return;
            }
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (null == dataGridView1.Rows[i].Cells["qty"].Value || null == dataGridView1.Rows[i].Cells["Size_no"].Value)
                {

                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("请勿提交空数据！", Program.client, "", Program.client.Language);
                    MessageBox.Show(msg);
                    return;
                }
                else
                {
                    int num;
                    bool v = int.TryParse(dataGridView1.Rows[i].Cells["qty"].Value.ToString(), out num);
                    
                }
            }
            DataTable dataTable = GetDgvToTable(dataGridView1);
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("sample",lbSampleNo.Text.ToString());
            dictionary.Add("purpose", lbPurpose.Text.ToString());
            dictionary.Add("ART_NAME", lbArtName.Text.ToString());
            dictionary.Add("ART_NO", lbArtNo.Text.ToString());
            dictionary.Add("season",lbSeason.Text.ToString());
            dictionary.Add("colorWay", colorWay);
            dictionary.Add("MODEL_MASTER", MODEL_MASTER);
            dictionary.Add("PATTERN_MASTER", PATTERN_MASTER);
            dictionary.Add("data",dataTable);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.SCM_Sample_ReceiveService", "InsertNG", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(dictionary));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string retData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                lbBadRecordNo.Text = retData;
                MessageBox.Show(retData, "保存成功");
                //  Dictionary<string, string> dictionary2 = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(retData);
            }
            else 
            {
                MessageBox.Show("保存失败");
            }

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = dataGridView1.CurrentRow.Index;

            string part_no = dataGridView1.Rows[rowIndex].Cells["part_no"].Value.ToString();
            string suppliers_code = dataGridView1.Rows[rowIndex].Cells["suppliers_code"].Value.ToString();
            string process_no = dataGridView1.Rows[rowIndex].Cells["process_no"].Value.ToString();
            string size_no = dataGridView1.Rows[rowIndex].Cells["size_no"].EditedFormattedValue.ToString();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                string part_no1 = dataGridView1.Rows[i].Cells["part_no"].Value.ToString();
                string suppliers_code1 = dataGridView1.Rows[i].Cells["suppliers_code"].Value.ToString();
                string process_no1 = dataGridView1.Rows[i].Cells["process_no"].Value.ToString();
                string size_no1 = dataGridView1.Rows[i].Cells["size_no"].EditedFormattedValue.ToString();
                if (i == dataGridView1.CurrentRow.Index)
                {
                    continue;
                }
                if (part_no1 == part_no && suppliers_code == suppliers_code1 && process_no == process_no1 && size_no == size_no1)
                {
                    dataGridView1.Rows[rowIndex].Cells["size_no"].Value = " ";
                    return;
                }
            }
        }

        private void btnDelDate_Click(object sender, EventArgs e)
        {
            int n = dataGridView1.Rows.Count-1;
            while (n>=0) 
            {
                if (dataGridView1.Rows[n].Cells["Column1"].Value != null) 
                {
                    if (dataGridView1.Rows[n].Cells["Column1"].Value.ToString() == "True")
                    {
                        dataGridView1.Rows.RemoveAt(n);
                    }
                }
                    
                n--;
            }
        
        }

        public DataTable GetDgvToTable(DataGridView dgv)
        {
            DataTable dt = new DataTable();

            // 列强制转换
            for (int count = 0; count < dgv.Columns.Count; count++)
            {
                DataColumn dc = new DataColumn(dgv.Columns[count].Name.ToString());
                dt.Columns.Add(dc);
            }

            // 循环行
            for (int count = 0; count < dgv.Rows.Count; count++)
            {
               /* if (dgv.Rows[count].Cells["qty"].Value == null || dgv.Rows[count].Cells["qty"].Value.ToString() == "" || dgv.Rows[count].Cells["qty"].Value.ToString() == "0")
                {
                    continue;
                }*/
                DataRow dr = dt.NewRow();
                for (int countsub = 0; countsub < dgv.Columns.Count; countsub++)
                {
                    dr[countsub] = Convert.ToString(dgv.Rows[count].Cells[countsub].Value);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewColumn column = dataGridView1.CurrentCell.OwningColumn;
            //如果是要显示下拉列表的列的话
            if (column.Name.Equals("Size_No")&&e.RowIndex>=0)
            {
                int columnIndex = dataGridView1.CurrentCell.ColumnIndex;
                int rowIndex = dataGridView1.CurrentCell.RowIndex;
                Rectangle rect = dataGridView1.GetCellDisplayRectangle(columnIndex, rowIndex, false);
                comboBox1.Left = rect.Left;
                comboBox1.Top = rect.Top;
                comboBox1.Width = rect.Width;
                comboBox1.Height = rect.Height;
                comboBox1.Focus();

                //将单元格的内容显示为下拉列表的当前项
                string consultingRoom = dataGridView1.Rows[rowIndex].Cells[columnIndex].Value == null? " ":dataGridView1.Rows[rowIndex].Cells[columnIndex].Value.ToString();
                int index = comboBox1.Items.IndexOf(consultingRoom);
                comboBox1.SelectedIndex = index;
                comboBox1.Visible = true;
            }
            else
            {
                comboBox1.Visible = false;
            }
            if (column.Name.Equals("causes") && e.RowIndex >= 0) 
            {
                causes causes1 = new causes(CausesDataTable,false,"CODE_NO", "CODE_NAME");
                Console.WriteLine(CausesDataTable.GetHashCode());
                causes1.sengMessage += new causes.sengDataListToMain(ToShowGetMessage);
                causes1.ShowDialog();
            }

            if (column.Name.Equals("responsible_unit")|| column.Name.Equals("responsible_unit_code") && e.RowIndex >= 0)
            {
                causes causes1 = new causes(StationDataTable, true,"STATION_NO", "STATION_NAME");
                Console.WriteLine(StationDataTable.GetHashCode());
                causes1.sengMessage += new causes.sengDataListToMain(ToShowGetStationMessage);
                causes1.ShowDialog();
            }

        }

        public void ToShowGetMessage(string s,string values)
        {
            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["causes"].Value = values;
        }

        public void ToShowGetStationMessage(string s, string values)
        {
            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["responsible_unit"].Value = s;
            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["responsible_unit_code"].Value = values;
        }
        private void comBox1_DropDownClosed(object sender, EventArgs e) 
        {
            int rowIndex = dataGridView1.CurrentRow.Index;
            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Size_No"].Value = comboBox1.SelectedValue?.ToString();
            string part_no = dataGridView1.Rows[rowIndex].Cells["part_no"].Value.ToString();
            string suppliers_code = dataGridView1.Rows[rowIndex].Cells["suppliers_code"].Value.ToString();
            string process_no = dataGridView1.Rows[rowIndex].Cells["process_no"].Value.ToString();
            string size_no = dataGridView1.Rows[rowIndex].Cells["size_no"].EditedFormattedValue.ToString();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                string part_no1 = dataGridView1.Rows[i].Cells["part_no"].Value.ToString();
                string suppliers_code1 = dataGridView1.Rows[i].Cells["suppliers_code"].Value.ToString();
                string process_no1 = dataGridView1.Rows[i].Cells["process_no"].Value.ToString();
                string size_no1 = dataGridView1.Rows[i].Cells["size_no"].EditedFormattedValue.ToString();
                if (i == dataGridView1.CurrentRow.Index)
                {
                    continue;
                }
                if (part_no1 == part_no && suppliers_code == suppliers_code1 && process_no == process_no1 && size_no == size_no1)
                {
                    dataGridView1.Rows[rowIndex].Cells["Size_No"].Value = "";
                    MessageBox.Show("存在相同的行");
                    return;
                }
            }
        }

        //加载Size列

    }
}
