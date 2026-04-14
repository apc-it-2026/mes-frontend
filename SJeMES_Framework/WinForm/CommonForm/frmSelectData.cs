using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SJeMES_Framework.WinForm.CommonForm
{
    public partial class frmSelectData : Form
    {
       
        bool IsOneLine = false;
        bool IsSelectFirstItem = false;
        bool IsAdvanced = false;
        
        public string ReturnDataXML = string.Empty;
       
        DataTable DT = null;

       
        #region 构造函数
        public frmSelectData(DataTable DT, bool IsOneLine, bool IsSelectFirstItem)
        {
            InitializeComponent();
      
            this.DT = DT;

            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;


            this.IsOneLine = IsOneLine;
            this.IsSelectFirstItem = IsSelectFirstItem;

            DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();
            newColumn.HeaderText = "";
            dataGridView1.Columns.Insert(0, newColumn);
            dataGridView1.DataSource = DT.DefaultView;
            

        }

       
        #endregion

        

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (IsOneLine)
                {
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        dataGridView1.Rows[i].Cells[0].Value = false;
                    }
                }

                dataGridView1.Rows[e.RowIndex].Cells[0].Value = !Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {


            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                if (Convert.ToBoolean(r.Cells[0].Value))
                {
                    if (string.IsNullOrEmpty(ReturnDataXML))
                    {
                        ReturnDataXML += "<ReturnDataXML>";
                    }

                    ReturnDataXML += "<Columns>";
                    for (int i = 1; i < dataGridView1.Columns.Count; i++)
                    {
                        ReturnDataXML += dataGridView1.Columns[i].Name + @"@;";
                    }

                    ReturnDataXML = ReturnDataXML.Remove(ReturnDataXML.Length - 2);
                    ReturnDataXML += "</Columns>";

                    ReturnDataXML += "<Row>";

                    for(int i=1;i<dataGridView1.Columns.Count;i++)
                    {
                        ReturnDataXML += "<" + dataGridView1.Columns[i].Name + ">"
                            + r.Cells[dataGridView1.Columns[i].Name].Value.ToString()
                            + "</" + dataGridView1.Columns[i].Name + ">";
                    }

                    ReturnDataXML += "</Row>";

                }
            }

            if (!string.IsNullOrEmpty(ReturnDataXML))
            {
                ReturnDataXML += "</ReturnDataXML>";
            }


            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            foreach(DataGridViewRow dgvr in dataGridView1.Rows)
            {
                dgvr.Cells[0].Value = checkBox1.Checked;
            }
        }

        private void frmSearchData_Load(object sender, EventArgs e)
        {
            this.checkBox1.Visible = !IsOneLine;
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (IsOneLine)
            {
                if (e.RowIndex > -1)
                {

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        dataGridView1.Rows[i].Cells[0].Value = false;
                    }


                    dataGridView1.Rows[e.RowIndex].Cells[0].Value = true;
                    btnOk_Click(btnOk, new EventArgs());
                }
            }
        }
    }
}
