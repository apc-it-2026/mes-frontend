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

namespace F_Work_SendReceive_Manage
{
    public partial class causes : MaterialForm
    {

        public delegate void sengDataListToMain(string names,string values);

        public event sengDataListToMain sengMessage;
        private string content1;
        private string content2;
        DataTable dataTable = new DataTable();
        public causes()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="table">传入列表数据</param>
        /// <param name="choose">判断是否显示选择列</param>
        /// <param name="str1">列1名</param>
        /// <param name="str2">列2名</param>
        public causes(DataTable table,bool choose,string str1,string str2)
        {
            InitializeComponent();
           
            dataTable = table.Copy();
            
            Console.WriteLine(table.GetHashCode());
            Console.WriteLine(dataTable.GetHashCode());
            dataGridView1.DataSource = table;
            if (choose)
            {
                dataGridView1.Columns["Column1"].Visible = false;
            }
            this.content1 = str1;
            this.content2 = str2;
            dataGridView1.Columns[content1].ReadOnly = true;
            dataGridView1.Columns[content2].ReadOnly = true;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dataGridView1.CurrentRow.Index;
            dataGridView1.Rows[index].Cells[content1].Value.ToString();
            dataGridView1.Rows[index].Cells[content2].Value.ToString();
            sengMessage?.Invoke(dataGridView1.Rows[index].Cells[content1].Value.ToString(), dataGridView1.Rows[index].Cells[content2].Value.ToString());
            this.Close();
            this.Dispose();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string codeCode = "";
            string codeName = "";
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if(null != dataGridView1.Rows[i].Cells["Column1"].Value && "True" == dataGridView1.Rows[i].Cells["Column1"].Value.ToString())
                {
                    codeCode += dataGridView1.Rows[i].Cells[content1].Value.ToString()+",";
                    codeName += dataGridView1.Rows[i].Cells[content2].Value.ToString()+",";
                }
            }
            codeCode = codeCode.Substring(0, codeCode.Length - 1);
            codeName = codeName.Substring(0, codeName.Length - 1);
            sengMessage?.Invoke(codeCode, codeName);
            this.Close();
            this.Dispose();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            string textInfo = textBox1.Text;
            string[] pp;

            string filter = content1 + " like '%" + textInfo + "%'";
            if (!"".Equals(content2))
            {
                if (textInfo.Contains("#"))
                {
                    int k = 0;
                    pp = textInfo.Split('#');
                    foreach (var item in pp)
                    {
                        if (k == 0)
                        {
                            filter += " or " + content2 + " like '%" + item + "%'";
                        }
                        else
                        {
                            filter += " and " + content2 + " like '%" + item + "%'";

                        }
                        k++;
                    }
                }
                else
                {
                    filter += " or " + content2 + " like '%" + textInfo + "%'";

                }

            }
            DataView dv = dataTable.DefaultView;
            dv.RowFilter = filter;
            dataGridView1.DataSource = dv;
        }
    }
}
