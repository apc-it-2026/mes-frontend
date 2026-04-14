using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SJeMES_Framework.DBHelper;

namespace SJeMES_Framework.WinForm.CommonForm
{
    public partial class FrmAdvancedSearch : Form
    {

        private static DataTable conditionTab = new DataTable();
        private string seletDataSql;
        public string sqlWhere;
        public bool isClose = false;
        private DataBase DB;

        public FrmAdvancedSearch(string seletDataSql)
        {
            this.seletDataSql = seletDataSql;

            InitializeComponent();
            if (conditionTab.Columns.Count == 0)
            {
                DataColumn dc = null;
                dc = conditionTab.Columns.Add("字段名称", Type.GetType("System.String"));
                dc = conditionTab.Columns.Add("查询条件", Type.GetType("System.String"));
                dc = conditionTab.Columns.Add("查询内容", Type.GetType("System.String"));
                dc = conditionTab.Columns.Add("拼接关系", Type.GetType("System.String"));
            }

            SetValue();


        }

        public FrmAdvancedSearch(string seletDataSql, DataBase DB)
        {
            this.seletDataSql = seletDataSql;
            this.DB = DB;
            InitializeComponent();
            if (conditionTab.Columns.Count == 0)
            {
                DataColumn dc = null;
                dc = conditionTab.Columns.Add("字段名称", Type.GetType("System.String"));
                dc = conditionTab.Columns.Add("查询条件", Type.GetType("System.String"));
                dc = conditionTab.Columns.Add("查询内容", Type.GetType("System.String"));
                dc = conditionTab.Columns.Add("拼接关系", Type.GetType("System.String"));
            }
            SetValueDB();
        }

        private void SetValue()
        {
            try
            {
                string newSql = "select top 1 *  from (" + seletDataSql + ")tab where 1=1 ";
                Dictionary<string, string> p = new Dictionary<string, string>();
                p.Add("sql", newSql);
                string XML = SJeMES_Framework.Common.WebServiceHelper.RunService(Program.Org, Program.WebServiceUrl, "SJ_MESAPI", "SJ_MESAPI.DataBase", "GetDataTable", p);
                string dtXML = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>");
                var dt = SJeMES_Framework.Common.StringHelper.GetDataTableFromXML(dtXML);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    cbo_FieldName.Items.Add(dt.Columns[i].ToString());
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return;
            }

        }
        private void SetValueDB()
        {
            try
            {
                string newSql = "select top 1 *  from (" + seletDataSql + ")tab where 1=1 ";
                var dt = DB.GetDataTable(newSql);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    cbo_FieldName.Items.Add(dt.Columns[i].ToString());
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return;
            }

        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            sqlWhere = string.Empty;
            //for (int i = 0; i < conditionTab.Rows.Count; i++)
            //{
            //    sqlWhere += "and T." + conditionTab.Rows[i]["字段名称"].ToString() + JudeCondition(conditionTab.Rows[i]["查询条件"].ToString(), conditionTab.Rows[i]["查询内容"].ToString());
            //}
            isClose = true;
            conditionTab.Clear();
            this.Close();
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            cbo_FieldName.Text = string.Empty;
            cbo_Where.Text = string.Empty;
            txt_Content.Text = string.Empty;
            conditionTab.Clear();
            dgvContent.DataSource = conditionTab;
            dgvContent.ClearSelection();
            // dgvContent.Columns[3].Visible = false;

        }
        private void btn_Condition_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbo_FieldName.Text))
            {
                MessageBox.Show("请选择字段名称！");
                return;
            }
            if (string.IsNullOrEmpty(cbo_Where.Text))
            {
                MessageBox.Show("请选择查询条件！");
                return;
            }
            if (string.IsNullOrEmpty(txt_Content.Text))
            {
                MessageBox.Show("请输入查询内容！");
                return;
            }
            var relationship = string.Empty;
            if (conditionTab.Rows.Count > 0)
            {
                if (!rdb_AND.Checked && !rdb_OR.Checked)
                {
                    MessageBox.Show("请选择拼接关系！");
                    return;
                }
                if (rdb_AND.Checked)
                    relationship = "AND";
                else if (rdb_OR.Checked)
                    relationship = "OR";
            }

            var dataRow = conditionTab.Select("字段名称='" + cbo_FieldName.Text + "' and 查询条件='" + cbo_Where.Text + "' and 查询内容='" + txt_Content.Text + "'");
            if (dataRow.Count() > 0)
            {
                MessageBox.Show("当前条件已存在！");
                return;
            }


            DataRow dr = conditionTab.NewRow();
            dr["字段名称"] = cbo_FieldName.Text;
            dr["查询条件"] = cbo_Where.Text;
            dr["查询内容"] = txt_Content.Text;
            dr["拼接关系"] = relationship;
            conditionTab.Rows.Add(dr);
            dgvContent.DataSource = conditionTab;
            dgvContent.ClearSelection();
            // dgvContent.Columns[3].Visible = false;
            cbo_FieldName.SelectedIndex = -1;
            cbo_Where.SelectedIndex = -1;
            txt_Content.Text = "";

        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            try
            {
                sqlWhere = " AND(";
                for (int i = 0; i < conditionTab.Rows.Count; i++)
                {
                    sqlWhere += conditionTab.Rows[i]["拼接关系"].ToString() + " T." + conditionTab.Rows[i]["字段名称"].ToString() + JudeCondition(conditionTab.Rows[i]["查询条件"].ToString(), conditionTab.Rows[i]["查询内容"].ToString());
                }
                sqlWhere += ")";
                conditionTab.Clear();
                if (sqlWhere == " AND()")
                    sqlWhere = string.Empty;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        private string JudeCondition(string where, string val)
        {
            string str = string.Empty;
            switch (where)
            {
                case "等于":
                    str = " ='" + val + "' ";
                    break;
                case "模糊包含":
                    str = " like'%" + val + "%' ";
                    break;
                case "不等于":
                    str = " !='" + val + "' ";
                    break;
                case "大于":
                    str = " >'" + val + "' ";
                    break;
                case "小于":
                    str = " <'" + val + "' ";
                    break;
                case "大于等于":
                    str = " >='" + val + "' ";
                    break;
                case "小于等于":
                    str = " <='" + val + "' ";
                    break;
            }
            return str;
        }

        private void FrmAdvancedSearch_Load(object sender, EventArgs e)
        {
            dgvContent.DataSource = conditionTab;
            dgvContent.ClearSelection();
            //dgvContent.Columns[3].Visible = false;
        }

        private void dgvContent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex > -1)
            //{
            //    cbo_FieldName.Text = dgvContent.Rows[e.RowIndex].Cells["字段名称"].Value.ToString();
            //    cbo_Where.Text = dgvContent.Rows[e.RowIndex].Cells["查询条件"].Value.ToString();
            //    txt_Content.Text = dgvContent.Rows[e.RowIndex].Cells["查询内容"].Value.ToString();
            //}
        }
    }
}
