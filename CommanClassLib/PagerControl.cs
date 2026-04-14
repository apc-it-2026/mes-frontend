using System;
using System.Windows.Forms;

namespace CommanClassLib
{
    public partial class PagerControl : UserControl
    {
        /// <summary>
        ///     出自:https://www.136.la/tech/show-394862.html
        /// </summary>
        public PagerControl()
        {
            InitializeComponent();
        }

        #region 分页字段和属性

        private int pageIndex = 1;

        /// <summary>
        ///     当前页数
        /// </summary>
        public virtual int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value; }
        }

        private int pageSize = 100;

        /// <summary>
        ///     每页记录数
        /// </summary>
        public virtual int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        private int recordCount;

        /// <summary>
        ///     总记录数
        /// </summary>
        public virtual int RecordCount
        {
            get { return recordCount; }
            set { recordCount = value; }
        }

        private int pageCount;

        /// <summary>
        ///     总页数
        /// </summary>
        public int PageCount
        {
            get
            {
                if (pageSize != 0)
                {
                    pageCount = GetPageCount();
                }

                return pageCount;
            }
        }

        #endregion

        #region 页码变化时触发事件

        public event EventHandler OnPageChanged;

        #endregion

        #region 分页及相关事件功能实现

        /// <summary>
        ///     设窗体控件全部可用
        /// </summary>
        private void SetFormCtrEnabled()
        {
            linkFirst.Enabled = true;
            linkPrevious.Enabled = true;
            linkNext.Enabled = true;
            linkLast.Enabled = true;
        }

        /// <summary>
        ///     计算总页数
        /// </summary>
        /// <returns></returns>
        private int GetPageCount()
        {
            if (PageSize == 0)
            {
                return 0;
            }

            int pageCount = RecordCount / PageSize;
            if (RecordCount % PageSize == 0)
            {
                pageCount = RecordCount / PageSize;
            }
            else
            {
                pageCount = RecordCount / PageSize + 1;
            }

            return pageCount;
        }

        /// <summary>
        ///     用于客户端调用
        /// </summary>
        public void DrawControl(int count)
        {
            recordCount = count;
            DrawControl(false);
        }

        /// <summary>
        ///     根据不同的条件，改变页面控件的呈现状态
        /// </summary>
        private void DrawControl(bool callEvent)
        {
            txtCurrentPage.Text = PageIndex.ToString(); //当前页
            lblPageCount.Text = PageCount.ToString(); //总页数
            lblTotalCount.Text = RecordCount.ToString(); //总记录数
            txtPageSize.Text = PageSize.ToString(); //每页记录数

            if (callEvent && OnPageChanged != null)
            {
                OnPageChanged(this, null); //当前分页数字改变时，触发委托事件  
            }

            SetFormCtrEnabled();
            if (PageCount == 1) //有且仅有一页时  
            {
                linkFirst.Enabled = false;
                linkPrevious.Enabled = false;
                linkNext.Enabled = false;
                linkLast.Enabled = false;
            }
            else if (PageIndex == 1) //当前页为第一页时  
            {
                linkFirst.Enabled = false;
                linkPrevious.Enabled = false;
            }
            else if (PageIndex == PageCount) //当前页为最后一页时  
            {
                linkNext.Enabled = false;
                linkLast.Enabled = false;
            }
        }

        #endregion

        #region 相关控件事件

        //首页按钮  
        private void linkFirst_Click(object sender, EventArgs e)
        {
            PageIndex = 1;
            DrawControl(true);
        }

        //上一页按钮  
        private void linkPrevious_Click(object sender, EventArgs e)
        {
            PageIndex = Math.Max(1, PageIndex - 1);
            DrawControl(true);
        }

        //下一页按钮  
        private void linkNext_Click(object sender, EventArgs e)
        {
            PageIndex = Math.Min(PageCount, PageIndex + 1);
            DrawControl(true);
        }

        //尾页按钮  
        private void linkLast_Click(object sender, EventArgs e)
        {
            PageIndex = PageCount;
            DrawControl(true);
        }

        /// <summary>
        ///     按下enter键，执行跳转页面功能
        /// </summary>
        private void txtPageNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            int num = 0;
            if (int.TryParse(txtCurrentPage.Text.Trim(), out num) && num > 0)
            {
                PageIndex = num;
                DrawControl(true);
            }
        }

        /// <summary>
        ///     跳转页数限制
        /// </summary>
        private void txtCurrentPage_TextChanged(object sender, EventArgs e)
        {
            int num = 0;
            if (int.TryParse(txtCurrentPage.Text.Trim(), out num) && num > 0)
            {
                //TryParse 函数，将字符串转换成等效的整数，返回bool型，判断是否转换成功。  
                //输入除数字以外的字符是转换不成功的  

                if (num > PageCount) //输入数量大于最大页数时，文本框自动显示最大页数  
                {
                    txtCurrentPage.Text = PageCount.ToString();
                }
            }
        }

        #endregion

        bool isTextChanged;

        /// <summary>
        ///     每页显示的记录数改变时
        /// </summary>
        private void txtPageSize_TextChanged(object sender, EventArgs e)
        {
            int num = 0;
            //输入不符合规范时，默认设置为100  
            if (!int.TryParse(txtPageSize.Text.Trim(), out num) || num <= 0)
            {
                num = 100;
                txtPageSize.Text = "100";
            }
            else
            {
                isTextChanged = true;
            }

            pageSize = num;
        }

        /// <summary>
        ///     光标离开 每页设置文本框时，显示到首页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPageSize_Leave(object sender, EventArgs e)
        {
            if (isTextChanged)
            {
                isTextChanged = false;
                linkFirst_Click(null, null);
            }
        }
    }
}