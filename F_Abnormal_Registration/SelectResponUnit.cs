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

namespace F_Abnormal_Registration
{
    public partial class SelectResponUnit : MaterialForm
    {
        public delegate void DataChangeHandler(object sender, DataChangeEventArgs args);
        public event DataChangeHandler DataChange;
        DataTable dataDt = null;
        string condition1;
        string condition2;

        public SelectResponUnit(DataTable dt,string t1,string t2)
        {
            InitializeComponent();
            dataDt = dt;
            condition1 = t1;
            condition2 = t2;
            dataGridView1.DataSource = dataDt;
        }

        // 调用事件函数
        public void OnDataChange(object sender, DataChangeEventArgs args)
        {
            DataChange?.Invoke(this, args);
        }

        //与主窗体传递的值
        public class DataChangeEventArgs : EventArgs
        {
            public string value1 { get; set; }
            public string value2 { get; set; }

            public DataChangeEventArgs(string t1, string t2)
            {
                value1 = t1;
                value2 = t2;
            }
        }

        //用户筛选过滤
        private void selectUnit1_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            string textInfo = selectUnit1.Text;
            string filter = condition1 + " like '%" + textInfo + "%' or " + condition2 + " like '%" + textInfo + "%'";
            DataView dv = dataDt.DefaultView;
            dv.RowFilter = filter;
            dataGridView1.DataSource = dv;
        }

        //点确认传递选中的值到主窗体
        private void butOk1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index > -1)
            {
                int index = dataGridView1.CurrentRow.Index;
                string value1 = dataGridView1.Rows[index].Cells[0].Value.ToString();
                string value2 = dataGridView1.Rows[index].Cells[1].Value.ToString();
                OnDataChange(this, new DataChangeEventArgs(value1,value2));
            }
            this.Close();
        }

        //双击传递选中的值到主窗体
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index > -1)
            {
                string value1 = dataGridView1.Rows[index].Cells[0].Value.ToString();
                string value2 = dataGridView1.Rows[index].Cells[1].Value.ToString();
                OnDataChange(this, new DataChangeEventArgs(value1, value2));
                this.Close();
                this.Dispose();
            }
        }

        private void ItemSelectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.selectUnit1.Text = "";

        }

    }
}
