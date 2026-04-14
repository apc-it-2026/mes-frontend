using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_Bad_Registration
{
    public partial class ucPage : UserControl
    {
        public ucPage()
        {
            InitializeComponent();
        }
        private int totalCount;
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount
        {
            get { return totalCount; }
            set
            {
                totalCount = value;
                InitPageInfo();
            }
        }

        private int pageSize = 10;
        /// <summary>
        /// 每页显示的记录数
        /// </summary>
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        private int pageNum;
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageNum
        {
            get
            {
                if (TotalCount == 0)
                    pageNum = 0;
                else
                {
                    //101条   每页20    5页多一行   +1 页
                    if (TotalCount % PageSize > 0)
                    {
                        pageNum = TotalCount / PageSize + 1;
                    }
                    else
                    {
                        pageNum = TotalCount / PageSize;
                    }
                }
                return pageNum;
            }

        }

        private int currentPage = 1;
        /// <summary>
        /// 当前页索引
        /// </summary>
        public int CurrentPage
        {
            get { return currentPage; }
            set { currentPage = value; }
        }

        private int startIndex = 1;
        /// <summary>
        /// 当前页开始索引（第一条数据的索引）
        /// </summary>
        public int StartIndex
        {
            get { return (CurrentPage - 1) * PageSize + 1; }
            set { startIndex = value; }
        }

        /// <summary>
        /// 初始化分页信息
        /// </summary>
        private void InitPageInfo()
        {
            //已到最后一页，重置到第一页
            if (TotalCount == 0 || (TotalCount > 0 && CurrentPage > PageNum))
            {
                CurrentPage = 1;
                btnPrev.Enabled = false;
                btnNext.Enabled = true;
                btnLast.Enabled = true;
                btnFirst.Enabled = false;
            }
            lblPageInfo.Text = string.Format("总共{0}条记录，共{1}页，当前第{2}页", TotalCount, PageNum, CurrentPage);
            txtPage.Text = CurrentPage.ToString();
        }

        /// <summary>
        /// 翻页委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void PagerHandler(object sender, EventArgs e);
        //翻页事件
        public event PagerHandler PageChanged;

        /// <summary>
        /// 实现翻页
        /// </summary>
        private void OnPagerChanged()
        {
            if (PageChanged != null)
            {
                PageChanged.Invoke(this, new EventArgs());
                InitPageInfo();
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (TotalCount > 0)
            {
                CurrentPage = 1;
                OnPagerChanged();//应用方 重新查询数据
                btnPrev.Enabled = false;
                btnNext.Enabled = true;
                btnLast.Enabled = true;
                btnFirst.Enabled = false;
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (TotalCount > 0)
            {
                CurrentPage = CurrentPage - 1;
                OnPagerChanged();//应用方 重新查询数据
                if (CurrentPage == 1)
                {
                    btnPrev.Enabled = false;
                    btnFirst.Enabled = false;
                }
                else
                {
                    btnPrev.Enabled = true;
                    btnFirst.Enabled = true;
                }
                btnNext.Enabled = true;
                btnLast.Enabled = true;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (TotalCount > 0)
            {
                CurrentPage = CurrentPage + 1;
                OnPagerChanged();//应用方 重新查询数据
                if (CurrentPage == PageNum)
                {
                    btnNext.Enabled = false;
                    btnLast.Enabled = false;
                }
                else
                {
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;
                }
                btnFirst.Enabled = true;
                btnPrev.Enabled = true;
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (TotalCount > 0)
            {
                CurrentPage = PageNum;
                OnPagerChanged();//应用方 重新查询数据
                btnPrev.Enabled = true;
                btnNext.Enabled = false;
                btnLast.Enabled = false;
                btnFirst.Enabled = true;

            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (TotalCount > 0)
            {
                if (!string.IsNullOrEmpty(txtPage.Text) && !Regex.IsMatch(txtPage.Text, @"^[\d]*$"))
                {
                    MessageBox.Show("请输入正确的页码！");
                    return;
                }
                int page = int.Parse(txtPage.Text);
                if (page == 0 || page == 1)
                {
                    page = 1;
                    btnPrev.Enabled = false;
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;
                    btnFirst.Enabled = false;
                }
                else if (page >= PageNum)
                {
                    page = PageNum;
                    btnPrev.Enabled = true;
                    btnNext.Enabled = false;
                    btnLast.Enabled = false;
                    btnFirst.Enabled = true;
                }
                else
                {
                    btnPrev.Enabled = true;
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;
                    btnFirst.Enabled = true;
                }
                CurrentPage = page;
                OnPagerChanged();//应用方 重新查询数据
            }
        }

        private void txtPage_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
