using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_Framework.Common
{
    /// <summary>
    /// 通用样式判断类
    /// </summary>
    public class GenClass: NotNull
    {
        /// <summary>
        /// 自适应dataGridView列内容大小
        /// </summary>
        /// <param name="dgv"></param>
        public static void AutoSizeColumn(DataGridView dgv)
        {
            int width = 0;
            for (int i = 1; i < dgv.Columns.Count; i++)
            {
                dgv.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);
                width += dgv.Columns[i].Width;

            }
            if (width > dgv.Size.Width)
            {
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            }
            else
            {
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }
        /// <summary>
        /// 自适应dataGridView列内容大小
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="num">从左往右冻结列的总数</param>
        public static void AutoSizeColumn(DataGridView dgv,int num)
        {
            for (int i = 0; i < num+1; i++)
            {
                dgv.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns[i].Frozen = true;
            }
            int width = 0;
            for (int i = 1; i < dgv.Columns.Count; i++)
            {
                dgv.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);
                width += dgv.Columns[i].Width;

            }
            if (width > dgv.Size.Width)
            {
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            }
            else
            {
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }
        /// <summary>
        /// 通用dataGridView统一样式
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="operation">操作Name</param>
        public static void AutoSizeColumnStyle(DataGridView dgv)
        {
            //表头样式,小四加粗
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("微软雅黑", 12f, FontStyle.Bold);
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 33;
            //表身样式,五号加粗
            dgv.RowHeadersDefaultCellStyle.Font = new Font("微软雅黑", 10.5f, FontStyle.Bold);//左边序号样式
            dgv.RowsDefaultCellStyle.Font = new Font("微软雅黑", 10.5f);
            dgv.RowTemplate.Height = 30;
            //奇数背景颜色
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(224, 224, 224);
            //dgv背景颜色
            dgv.BackgroundColor = SystemColors.Control;
            //网格线颜色
            dgv.GridColor = SystemColors.ControlDark;
            //控件边框
            dgv.Dock = DockStyle.Fill;
        }
    }
}
