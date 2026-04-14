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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_TMS_TierMeeting2_Main
{
    public partial class Tier3DeliveryForm : MaterialForm
    {
		private string date = "";
		private DataTable dtJson = new DataTable();
		public Tier3DeliveryForm(string date)
        {
			this.date = date;
            InitializeComponent();
			base.WindowState = FormWindowState.Maximized;
			this.dataGridView1.AutoGenerateColumns = false;
		}

        private void DetailDeliveryForm_Load(object sender, EventArgs e)
        {
			this.dateTimePicker2.Text = date;
			this.GetData(this.dateTimePicker2.Text);
			SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);
		}
		private void GetData(string date)
		{
			try
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("date", date);
				string text = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.Efficiency_KanbanServer", "SecondEfficiency_Query", Program.client.UserToken, JsonConvert.SerializeObject(dictionary));
				bool flag = Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(text)["IsSuccess"]);
				if (flag)
				{
					string text2 = JsonConvert.DeserializeObject<Dictionary<string, object>>(text)["RetData"].ToString();
					this.dtJson = JsonHelper.GetDataTableByJson(text2);
					this.dataGridView1.DataSource = this.dtJson;
					
					this.dataGridView1.Update();
					this.dataGridView1.RowPostPaint += this.dataGridView1_RowPostPaint;
				}
				else
				{
					MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(text)["ErrMsg"].ToString());
				}
			}
			catch (Exception ex)
			{
				MessageHelper.ShowErr(this, ex.Message);
			}
		}
		private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
		{
			for (int i = 0; i < this.dtJson.Rows.Count; i++)
			{
				decimal d = 0m;
				string text = string.IsNullOrEmpty(this.dtJson.Rows[i]["work_qty"].ToString()) ? "0" : this.dtJson.Rows[i]["work_qty"].ToString();
				string s = string.IsNullOrEmpty(this.dtJson.Rows[i]["label_qty"].ToString()) ? "0" : this.dtJson.Rows[i]["label_qty"].ToString();
				decimal num;
				bool flag = !text.Equals("0") && decimal.TryParse(text, out num) && decimal.TryParse(s, out num);
				if (flag)
				{
					d = decimal.Parse(s) / decimal.Parse(text) * 100m;
					bool flag2 = d >= 100m;
					if (flag2)
					{
						this.dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LawnGreen;
					}
					else
					{
						bool flag3 = d >= 95m && d < 100m;
						if (flag3)
						{
							this.dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
						}
						else
						{
							bool flag4 = d < 95m;
							if (flag4)
							{
								this.dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
							}
						}
					}
				}
				else
				{
					this.dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Aqua;
				}
			}
		}
		private void button2_Click(object sender, EventArgs e)
		{
			this.GetData(this.dateTimePicker2.Text);
		}

		private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			int index = this.dataGridView1.CurrentRow.Index;
			bool flag = index > -1 && this.dataGridView1.Rows[index].Cells[0].Value != null;
			if (flag)
			{
				Tier2DeliveryForm frm = new Tier2DeliveryForm(this.dateTimePicker2.Text, this.dataGridView1.Rows[index].Cells["code"].Value.ToString());
				frm.Show();
			}
		}
	}
}
