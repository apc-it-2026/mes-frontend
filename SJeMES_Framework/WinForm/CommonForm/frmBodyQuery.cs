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
    public partial class frmBodyQuery : Form
    {
        private FormDesign.Forms.FormClass FC;
        private FormXML.Forms.FormClass FC2;
        private TabControl tabBody;
        Dictionary<string, object> dic;
        public string sqlWhere;
        public string pageName;
        public int cboIndex;

        DataTable conditionTab = new DataTable();

        public frmBodyQuery(TabControl tabBody, FormDesign.Forms.FormClass FC)
        {
            this.tabBody = tabBody;
            this.FC = FC;
            sqlWhere = string.Empty;
           
            InitializeComponent();
            txt_Where.Text = string.Empty;
            if (conditionTab.Columns.Count == 0)
            {
                DataColumn dc = null;
                dc = conditionTab.Columns.Add("字段名称", Type.GetType("System.String"));
                dc = conditionTab.Columns.Add("查询条件", Type.GetType("System.String"));
                dc = conditionTab.Columns.Add("查询内容", Type.GetType("System.String"));
                dc = conditionTab.Columns.Add("条件关系", Type.GetType("System.String"));
            }
            SetValue();
        }
        public frmBodyQuery(FormDesign.Forms.FormClass FC)
        {
            this.FC = FC;
            sqlWhere = string.Empty;
           
            InitializeComponent();
            txt_Where.Text = string.Empty;
            if (conditionTab.Columns.Count == 0)
            {
                DataColumn dc = null;
                dc = conditionTab.Columns.Add("字段名称", Type.GetType("System.String"));
                dc = conditionTab.Columns.Add("查询条件", Type.GetType("System.String"));
                dc = conditionTab.Columns.Add("查询内容", Type.GetType("System.String"));
                dc = conditionTab.Columns.Add("条件关系", Type.GetType("System.String"));
            }
            SetValueByFC();
        }

        public frmBodyQuery(TabControl tabBody, FormXML.Forms.FormClass FC)
        {
            this.tabBody = tabBody;
            this.FC2 = FC;
            sqlWhere = string.Empty;

            InitializeComponent();
            txt_Where.Text = string.Empty;
            if (conditionTab.Columns.Count == 0)
            {
                DataColumn dc = null;
                dc = conditionTab.Columns.Add("字段名称", Type.GetType("System.String"));
                dc = conditionTab.Columns.Add("查询条件", Type.GetType("System.String"));
                dc = conditionTab.Columns.Add("查询内容", Type.GetType("System.String"));
                dc = conditionTab.Columns.Add("条件关系", Type.GetType("System.String"));
            }
            SetValue();
        }
        public frmBodyQuery(FormXML.Forms.FormClass FC)
        {
            this.FC2 = FC;
            sqlWhere = string.Empty;

            InitializeComponent();
            txt_Where.Text = string.Empty;
            if (conditionTab.Columns.Count == 0)
            {
                DataColumn dc = null;
                dc = conditionTab.Columns.Add("字段名称", Type.GetType("System.String"));
                dc = conditionTab.Columns.Add("查询条件", Type.GetType("System.String"));
                dc = conditionTab.Columns.Add("查询内容", Type.GetType("System.String"));
                dc = conditionTab.Columns.Add("条件关系", Type.GetType("System.String"));
            }
            SetValueByFC();
        }


        private void SetValueByFC()
        {
            try
            {
                dic = new Dictionary<string, object>();

                if (FC != null)
                {
                    foreach (var item in FC.FormPanels)
                    {
                        List<string> lst = new List<string>();
                        if (item.Value.ParentName.IndexOf("FormDesignTMP_TabBody") > -1)
                        {
                            cbo_PageName.Items.Add(item.Value.Title);
                            string dgvName = item.Value.ParentName + "_BodyPanel_" + item.Value.Title + "_panelChildrens_DataView";
                            var dgv = (DataGridView)item.Value.Control.Controls.Find(dgvName, true)[0];
                            for (int i = 0; i < dgv.Columns.Count; i++)
                            {
                                if (dgv.Columns[i].HeaderText != "行号")
                                    lst.Add(dgv.Columns[i].HeaderText + "     " + dgv.Columns[i].DataPropertyName);
                            }
                            dic.Add(item.Value.Title, lst);
                        }

                    }
                }
                else
                {
                    foreach (var item in FC2.FormPanels)
                    {
                        List<string> lst = new List<string>();
                        if (item.Value.ParentName.IndexOf("FormDesignTMP_TabBody") > -1)
                        {
                            cbo_PageName.Items.Add(item.Value.Title);
                            string dgvName = item.Value.ParentName + "_BodyPanel_" + item.Value.Title + "_panelChildrens_DataView";
                            var dgv = (DataGridView)item.Value.Control.Controls.Find(dgvName, true)[0];
                            for (int i = 0; i < dgv.Columns.Count; i++)
                            {
                                if (dgv.Columns[i].HeaderText != "行号")
                                    lst.Add(dgv.Columns[i].HeaderText + "     " + dgv.Columns[i].DataPropertyName);
                            }
                            dic.Add(item.Value.Title, lst);
                        }

                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void SetValue()
        {
            dic = new Dictionary<string, object>();
            foreach (Control ctr in tabBody.Controls)
            {
                List<string> lst = new List<string>();
                if(ctr is TabPage)
                {
                    cbo_PageName.Items.Add(ctr.Text);
                    string dgvName = ctr.Name + "_BodyPanel_" + ctr.Text + "_panelChildrens_DataView";
                    var dgv = (DataGridView)ctr.Controls.Find(dgvName, true)[0];
                    for (int i = 0; i < dgv.Columns.Count; i++)
                    {
                        if(dgv.Columns[i].HeaderText!="行号")
                        lst.Add(dgv.Columns[i].HeaderText+"     "+dgv.Columns[i].DataPropertyName);
                    }
                    dic.Add(ctr.Text, lst);
                }
            }
         
        }

        private void cbo_PageName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cbo_PageName.Text))
            { 
                cbo_FieldName.Items.Clear();
                cbo_FieldName.Items.AddRange(((List<string>)dic[cbo_PageName.Text]).ToArray());
                cbo_FieldName.SelectedIndex = cbo_FieldName.SelectedIndex > 0 ? 0 : -1;
                txt_Where.Text = string.Empty;
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
       
        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbo_PageName.Text))
            {
                MessageBox.Show("请选择页签！");
                return;
            }
            if (string.IsNullOrEmpty(cbo_FieldName.Text))
            {
                MessageBox.Show("请选择字段！");
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
            //var dataRow = conditionTab.Select("字段名称='" + cbo_FieldName.Text + "' and 查询条件='" + cbo_Where.Text + "' and 查询内容='" + txt_Content.Text + "'");
            //if (dataRow.Count() > 0)
            //{
            //    MessageBox.Show("当前条件已存在！");
            //    return;
            //}
            foreach (TabPage tab in ((FormDesign.FormDesignTMP)((FormDesign.FormPanel.HeadPanelClass)this.FC.FormPanels["Panel1"]).Control.FindForm()).tabBody.TabPages)
            {
                if (cbo_PageName.Text == tab.Text)
                {
                    var num = tab.Name.Split('_')[2].Replace("TabPage", "");

                    var tableName = FC.DataSource.Tables["Table" + num].TableName;

                    //拼接SQL条件
                    if (string.IsNullOrEmpty(sqlWhere))
                        sqlWhere = "AND ()";
                    sqlWhere = sqlWhere.Insert(sqlWhere.LastIndexOf(')'), "\r\n" + relationship + " " + tableName+"."+cbo_FieldName.Text.Split(' ')[5].Trim() + JudeCondition(cbo_Where.Text, txt_Content.Text));
                    txt_Where.Text = sqlWhere;

                    DataRow dr = conditionTab.NewRow();
                    dr["字段名称"] = cbo_FieldName.Text;
                    dr["查询条件"] = cbo_Where.Text;
                    dr["查询内容"] = txt_Content.Text;
                    dr["条件关系"] = relationship;
                    conditionTab.Rows.Add(dr);
                }
            }
           


            //dgvContent.DataSource = conditionTab;
            //dgvContent.ClearSelection();
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
                case "开始于":
                    str = " like '" + val + "%' ";
                    break;
                case "结束于":
                    str = " like '%" + val + "' ";
                    break;
                case "不开始于":
                    str = " not like '" + val + "%' ";
                    break;
                case "不结束于":
                    str = " not like '%" + val + "' ";
                    break;
            }
            return str;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            pageName = cbo_PageName.Text;
            this.Close();
            
        }

        private void frmBodyQuery_Load(object sender, EventArgs e)
        {
            sqlWhere = string.Empty;
            cbo_PageName.SelectedIndex = cboIndex;
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            txt_Where.Text = string.Empty;
            sqlWhere = string.Empty;
            conditionTab = new DataTable();
            if (conditionTab.Columns.Count == 0)
            {
                DataColumn dc = null;
                dc = conditionTab.Columns.Add("字段名称", Type.GetType("System.String"));
                dc = conditionTab.Columns.Add("查询条件", Type.GetType("System.String"));
                dc = conditionTab.Columns.Add("查询内容", Type.GetType("System.String"));
                dc = conditionTab.Columns.Add("条件关系", Type.GetType("System.String"));
            }
        }
    }
}
