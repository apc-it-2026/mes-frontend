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

namespace TierMeeting.Delivery
{
    public partial class PoRateForm : MaterialForm
    {

		DataTable dataTable = new DataTable();

		public PoRateForm()
        {
            InitializeComponent();
        }

        public PoRateForm(string date)
        {
            InitializeComponent();
            this.dateTimePicker1.Text= date;
		}

		private void PoRateForm_Load(object sender, EventArgs e)
		{
			this.WindowState = FormWindowState.Maximized;
			this.dataGridView1.AutoGenerateColumns = false;
			SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);
			GetData();
		}

		private void btnQuery_Click(object sender, EventArgs e)
        {
            this.GetData();
        }


		private void GetData()
		{
			try
			{
				Dictionary<string, object> p = new Dictionary<string, object>();
				p.Add("date", this.dateTimePicker1.Text);
				string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
						Program.client.APIURL,
						"KZ_JMSAPI", "KZ_JMSAPI.Controllers.JMS_GoalAccomplished_ListServer",
											"Tier4_DayQuery",
						Program.client.UserToken,
						JsonConvert.SerializeObject(p));
				bool flag = Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]);		
				if (flag)
				{
					string text2 = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
					dataTable = JsonHelper.GetDataTableByJson(text2);
					this.dataGridView1.DataSource = dataTable;
					this.dataGridView1.RowPostPaint += this.dataGridView1_RowPostPaint;
					this.dataGridView1.Update();
				}
				else
				{
					if (dataTable!=null)
					{
						dataTable.Clear();
					}
					dataGridView1.DataSource = dataTable;
					this.dataGridView1.RowPostPaint += this.dataGridView1_RowPostPaint;
					this.dataGridView1.Update();
					MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());

				}
			}
			catch (Exception ex)
			{
				MessageHelper.ShowErr(this, ex.Message);
			}
		}



		private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
		{
			var grid = sender as DataGridView;
			var rowIdx = (e.RowIndex + 1).ToString();
			StringFormat centerFormat = new StringFormat()
			{
				Alignment = StringAlignment.Center,
				LineAlignment = StringAlignment.Center
			};
			Rectangle headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
			e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);

			for (int i = 0; i < this.dataTable.Rows.Count; i++)
			{
				string ok = string.IsNullOrEmpty(this.dataTable.Rows[i]["finish_situation"].ToString()) ? "NO" : this.dataTable.Rows[i]["finish_situation"].ToString();
				if (ok.ToUpper().Equals("NO"))
				{
					this.dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
				}
				else
				{
					this.dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Green;
				}
			}
		}

	}
}
