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
using System.Windows.Forms.DataVisualization.Charting;

namespace F_TMS_TierMeeting2_Main.Delivery
{
    public partial class POComRateForm : MaterialForm
    {
       
        
        string dept = "";
        DateTime date = DateTime.Now;
         string type = ""; 
        string title = "";
        string column = "";



public POComRateForm(string dept,DateTime date,string type)
        {
            InitializeComponent();
            this.dept = dept;
            this.date = date;
            this.type = type;
            if (type == "2")
            {
                title = "组别";
                column = "DEPARTMENT_NAME";
            }
            else if (type == "3")
            {
                title = "部门";
                column = "udf07";
            }
            else if (type == "4")
            {
                title = "区域";
                column = "";
            }
            dataGridView1.EnableHeadersVisualStyles = false;//这样就可以使用当前的主题的样式了，这句话十分关键！
            //dataGridView1.Rows.Add(2);
            //dataGridView1.Rows[0].Cells[0].Value = "PO目标个数";
            //dataGridView1.Rows[1].Cells[0].Value = "已完成PO个数";
            //dataGridView1.Rows[2].Cells[0].Value = "PO完成率(%)";
         

        }

        private void POComRateForm_Load(object sender, EventArgs e)
        {
            GetData(dept, date, type);
        }
        private void GetData(string dept, DateTime date, string type) {
            DataTable table = new DataTable();
            DataTable dt = getPOComrate(dept, date, type);
            Console.WriteLine();


            SetChart(dt);


            //设置表头

            table.Columns.Add(title, typeof(string));
            foreach (DataRow dr in dt.Rows)
            {
                table.Columns.Add(dr[0].ToString(), typeof(string));
            }
            table.Columns.Add("合计", typeof(string));
            //计算合计  
            double sum1 = 0;
            double sum2 = 0;
            double rate = 0;
           
            foreach (DataRow dr in dt.Rows)
            {
                sum1 += double.Parse(dr[1].ToString());
                sum2 += double.Parse(dr[2].ToString());
               
            }
            rate = Math.Round(100 * sum2 / sum1, 2);
            //循环遍历数据
            for (int i = 1; i < dt.Columns.Count; i++)
            {
                
                DataRow r = table.NewRow();
                foreach (DataRow dr in dt.Rows)
                {
                    if (i == 1)
                    {
                        r[title] = "PO目标个数";
                        r["合计"] = sum1;
                    }
                    else if (i == 2)
                    {
                        r[title] = "已完成PO个数";
                        r["合计"] = sum2;
                    }
                    else if (i == 3)
                    {
                        r[title] = "PO完成率(%)";
                        r["合计"] = rate ;
                    }
                    string head = dr[0].ToString();
                    r[head] = dr[i];

                }
                table.Rows.Add(r);
            }
            dataGridView1.DataSource = table;
            int row_height = dataGridView1.Height / (dataGridView1.Rows.Count+1);
            dataGridView1.ColumnHeadersHeight = row_height;
            foreach (DataGridViewRow dgvr in dataGridView1.Rows) {
                dgvr.Height = row_height;
            }
            for (int i = 0; i < this.dataGridView1.Columns.Count; i++)
            {
                this.dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                if (i != 0&&dataGridView1.Rows.Count>0)
                {
                    double a_rate = Convert.ToDouble(this.dataGridView1.Rows[2].Cells[i].Value);
                    if (a_rate >= 100)
                    {
                        dataGridView1.Rows[2].Cells[i].Style.BackColor = Color.Green;
                    }
                    else if (a_rate >= 95)
                    {
                        dataGridView1.Rows[2].Cells[i].Style.BackColor = Color.Yellow;
                    }
                    else
                    {
                        dataGridView1.Rows[2].Cells[i].Style.BackColor = Color.Red;
                    }
                }
            }
            
        }

        private void SetChart(DataTable dataTable) {


           #region   设置PO完成率
                    
                    Series dataTable1Series = new Series("PO完成率");
         
                    dataTable1Series.Points.DataBind(dataTable.AsEnumerable(), column, "RATE", "");
                
                    dataTable1Series.XValueType = ChartValueType.Time; //设置X轴类型为时间
                
                    dataTable1Series.ChartType = SeriesChartType.Column;
                    dataTable1Series.ChartArea="";

            #endregion

            #region   设置累计PO完成率
                 DataTable dt = dataTable.Copy();
                dt.Columns.Add("TOTAL_RATE", Type.GetType("System.String"));
                double total_target = 0;
                double total_finish = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    total_target += Convert.ToDouble(dr["TAGET_PO"].ToString());
                    total_finish += Convert.ToDouble(dr["FINISH_PO"].ToString());
                    dr["TOTAL_RATE"] = total_target == 0 ? 100 : Math.Round(100 * total_finish/total_target, 2);
                }

                Series total_rate_Series = new Series("累计PO完成率");
                total_rate_Series.Points.DataBind(dt.AsEnumerable(), column, "TOTAL_RATE", "");

                total_rate_Series.XValueType = ChartValueType.Time; //设置X轴类型为时间

                total_rate_Series.ChartType = SeriesChartType.Line;
               
            total_rate_Series.ChartArea = "";

            #endregion


                chart1.Series.Clear();
                chart1.Series.Add(dataTable1Series);
                chart1.Series.Add(total_rate_Series);
                chart1.Titles.Clear();
                chart1.Legends.Add("");
            
                if (base.Height > 800) chart1.ChartAreas[0].AxisX.Interval = 0;
                chart1.Series["PO完成率"].XValueMember = column;
                chart1.Series["PO完成率"].YValueMembers = "RATE";

                chart1.Series["累计PO完成率"].XValueMember = column;
                chart1.Series["累计PO完成率"].YValueMembers = "TOTAL_RATE";
                chart1.Series["累计PO完成率"].IsValueShownAsLabel = true;
                chart1.Series["累计PO完成率"].Font = new Font(chart1.Series["累计PO完成率"].Font.FontFamily, 15); ;
                chart1.Series["累计PO完成率"].MarkerSize = 20;
                chart1.Series["累计PO完成率"].MarkerStyle = MarkerStyle.Circle;
                chart1.Series["累计PO完成率"].MarkerColor = Color.Red;
                chart1.Series["累计PO完成率"].MarkerBorderColor = Color.Red;
                chart1.Series["累计PO完成率"].BorderWidth = 5;
                chart1.Series["累计PO完成率"].LabelForeColor = Color.Black;
                
                chart1.Series["累计PO完成率"].Color = Color.Black;
                
                
                chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
               

                chart1.ChartAreas[0].AxisY.Minimum = 0;
                chart1.ChartAreas[0].AxisY.Maximum = 100;


            //  chart1.Titles[0].ForeColor = textColor;
            //设置图表颜色
            foreach (DataPoint point in chart1.Series["PO完成率"].Points)
            {
                double c_rate = Convert.ToDouble(point.YValues[0].ToString());
                if (c_rate >= 100)
                {
                    point.Color = Color.Green;


                }
                else if (c_rate >= 95)
                {

                    point.Color = Color.Yellow;

                }
                else
                {

                    point.Color = Color.Red;

                }
            }
            








        }

        public DataTable getPOComrate(string dept,DateTime date,string type)
        {
            DataTable dtJson = new DataTable();
            Dictionary<string, string> p = new Dictionary<string, string>();        
            p.Add("dept", dept);        
            p.Add("time", date.ToString("yyyy/MM/dd"));
            p.Add("num", type);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "TierMeeting", "TierMeeting.Controllers.TierMeetingServer", "GetPOComRate", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dtJson = JsonHelper.GetDataTableByJson(json);
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            return dtJson;
        }
    }
}
