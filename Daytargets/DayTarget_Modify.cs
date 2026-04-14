using DayTargets;
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

namespace DayTargets
{
    public partial class DayTargetModifyForm : MaterialForm
    {
        string d_dept = "";
        string work_day = "";
        string work_qty = "";
        string note = "";

        public DayTargetModifyForm()
        {
            InitializeComponent();

            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);

        }

        public DayTargetModifyForm(string dept, string workDate, string workQty, string strNote)
        {
            InitializeComponent();
            d_dept = dept;
            work_day = workDate;
            work_qty = workQty;
            note = strNote;
        }

        private void DayTargetModifyForm_Load(object sender, EventArgs e)
        {
            textDept.Text = d_dept;
            textWorkDay.Text = work_day;
            textWorkQty.Text = work_qty;
            textNote.Text = note;

            textWorkQty.Focus();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textWorkQty.Text.ToString().Trim() == "")
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please fill in today's goal！");
                return;
            }
            string qty = textWorkQty.Text.ToString().Trim();
            UpdateWorkTarget(d_dept, work_day, qty, textNote.Text.ToString());
        }

        private void UpdateWorkTarget(string dept, string workDay, string qty, string note)
        {
            Dictionary<string, Object> d = new Dictionary<string, object>();
            d.Add("vDDept", dept);
            d.Add("vWorkDay", workDay);
            d.Add("vWorkQty", qty);
            d.Add("vNote", note);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.DayTargetsServer", "UpdateWorkTarget",
                            Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(d));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string tips = SJeMES_Framework.Common.UIHelper.UImsg("Tips", Program.client, "",Program.client.Language);
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("Successfully modified！", Program.client,"", Program.client.Language);

                DialogResult result = MessageBox.Show(msg, tips);
                if (result == DialogResult.OK)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                string tips = SJeMES_Framework.Common.UIHelper.UImsg("err-00003", Program.client,"", Program.client.Language);
                string msg = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString();
                MessageBox.Show(msg, tips);
            }
        }

        //To control the goal of the day, you can only enter 0-9 and backspace
        private void textWorkQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '\b')
            {
                e.Handled = false;
            }
        }
    }
}
