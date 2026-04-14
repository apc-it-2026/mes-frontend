
namespace InvestmentReturnOrder
{
    partial class InvestmentReturnOrder
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        /// 
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvIntoProduction = new System.Windows.Forms.DataGridView();
            this.SE_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.main_prod_order = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRODUCTION_ORDER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SIZE_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ROUT_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.D_DEPT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.In_Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Out_Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Transfer_Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.This_Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransferDept = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label_SalesOrder = new System.Windows.Forms.Label();
            this.label_SubProdOrder = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox_MainProdOrder = new System.Windows.Forms.TextBox();
            this.textBox_SalesOrder = new System.Windows.Forms.TextBox();
            this.textBox_SubProdOrder = new System.Windows.Forms.TextBox();
            this.label_MainProdOrder = new System.Windows.Forms.Label();
            this.label_DeptNo = new System.Windows.Forms.Label();
            this.textBox_DeptNo = new System.Windows.Forms.TextBox();
            this.label_RoutNo = new System.Windows.Forms.Label();
            this.label_OrgId = new System.Windows.Forms.Label();
            this.comboBox_RoutNo = new System.Windows.Forms.ComboBox();
            this.comboBox_OrgId = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIntoProduction)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl1.Font = new System.Drawing.Font("SimSun", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(0, 66);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1269, 653);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvIntoProduction);
            this.tabPage1.Controls.Add(this.tableLayoutPanel2);
            this.tabPage1.Controls.Add(this.tableLayoutPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 40);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(1261, 609);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "生产调拨";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvIntoProduction
            // 
            this.dgvIntoProduction.AllowDrop = true;
            this.dgvIntoProduction.AllowUserToAddRows = false;
            this.dgvIntoProduction.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvIntoProduction.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.CornflowerBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("SimSun", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvIntoProduction.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvIntoProduction.ColumnHeadersHeight = 50;
            this.dgvIntoProduction.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SE_ID,
            this.main_prod_order,
            this.PRODUCTION_ORDER,
            this.SIZE_NO,
            this.ROUT_NO,
            this.D_DEPT,
            this.QTY,
            this.In_Qty,
            this.Out_Qty,
            this.Transfer_Qty,
            this.This_Qty,
            this.TransferDept});
            this.dgvIntoProduction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvIntoProduction.EnableHeadersVisualStyles = false;
            this.dgvIntoProduction.Location = new System.Drawing.Point(2, 161);
            this.dgvIntoProduction.Margin = new System.Windows.Forms.Padding(2);
            this.dgvIntoProduction.Name = "dgvIntoProduction";
            this.dgvIntoProduction.RowHeadersWidth = 51;
            this.dgvIntoProduction.RowTemplate.Height = 27;
            this.dgvIntoProduction.Size = new System.Drawing.Size(1257, 446);
            this.dgvIntoProduction.TabIndex = 2;
            this.dgvIntoProduction.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvIntoProduction_CellEndEdit);
            this.dgvIntoProduction.SelectionChanged += new System.EventHandler(this.dgvIntoProduction_SelectionChanged);
            // 
            // SE_ID
            // 
            this.SE_ID.DataPropertyName = "SE_ID";
            this.SE_ID.HeaderText = "销售订单";
            this.SE_ID.MinimumWidth = 6;
            this.SE_ID.Name = "SE_ID";
            this.SE_ID.ReadOnly = true;
            this.SE_ID.Width = 158;
            // 
            // main_prod_order
            // 
            this.main_prod_order.DataPropertyName = "MAIN_PROD_ORDER";
            this.main_prod_order.HeaderText = "主工单";
            this.main_prod_order.Name = "main_prod_order";
            this.main_prod_order.ReadOnly = true;
            this.main_prod_order.Width = 128;
            // 
            // PRODUCTION_ORDER
            // 
            this.PRODUCTION_ORDER.DataPropertyName = "PRODUCTION_ORDER";
            this.PRODUCTION_ORDER.HeaderText = "子工单";
            this.PRODUCTION_ORDER.MinimumWidth = 6;
            this.PRODUCTION_ORDER.Name = "PRODUCTION_ORDER";
            this.PRODUCTION_ORDER.ReadOnly = true;
            this.PRODUCTION_ORDER.Width = 128;
            // 
            // SIZE_NO
            // 
            this.SIZE_NO.DataPropertyName = "SIZE_NO";
            this.SIZE_NO.HeaderText = "尺码";
            this.SIZE_NO.MinimumWidth = 6;
            this.SIZE_NO.Name = "SIZE_NO";
            this.SIZE_NO.ReadOnly = true;
            this.SIZE_NO.Width = 98;
            // 
            // ROUT_NO
            // 
            this.ROUT_NO.DataPropertyName = "ROUT_NO";
            this.ROUT_NO.HeaderText = "制程";
            this.ROUT_NO.Name = "ROUT_NO";
            this.ROUT_NO.ReadOnly = true;
            this.ROUT_NO.Width = 98;
            // 
            // D_DEPT
            // 
            this.D_DEPT.DataPropertyName = "D_DEPT";
            this.D_DEPT.HeaderText = "工作中心";
            this.D_DEPT.MinimumWidth = 6;
            this.D_DEPT.Name = "D_DEPT";
            this.D_DEPT.ReadOnly = true;
            this.D_DEPT.Width = 158;
            // 
            // QTY
            // 
            this.QTY.DataPropertyName = "QTY";
            this.QTY.HeaderText = "子工单数量";
            this.QTY.MinimumWidth = 6;
            this.QTY.Name = "QTY";
            this.QTY.ReadOnly = true;
            this.QTY.Width = 188;
            // 
            // In_Qty
            // 
            this.In_Qty.DataPropertyName = "IN_QTY";
            this.In_Qty.HeaderText = "累计投产数量";
            this.In_Qty.MinimumWidth = 6;
            this.In_Qty.Name = "In_Qty";
            this.In_Qty.ReadOnly = true;
            this.In_Qty.Width = 218;
            // 
            // Out_Qty
            // 
            this.Out_Qty.DataPropertyName = "OUT_QTY";
            this.Out_Qty.HeaderText = "累计完工数量";
            this.Out_Qty.MinimumWidth = 6;
            this.Out_Qty.Name = "Out_Qty";
            this.Out_Qty.ReadOnly = true;
            this.Out_Qty.Width = 218;
            // 
            // Transfer_Qty
            // 
            this.Transfer_Qty.DataPropertyName = "TRANSFER_QTY";
            this.Transfer_Qty.HeaderText = "可调拨数量";
            this.Transfer_Qty.MinimumWidth = 6;
            this.Transfer_Qty.Name = "Transfer_Qty";
            this.Transfer_Qty.ReadOnly = true;
            this.Transfer_Qty.Width = 188;
            // 
            // This_Qty
            // 
            this.This_Qty.DataPropertyName = "This_Qty";
            this.This_Qty.HeaderText = "本次调拨数量";
            this.This_Qty.MinimumWidth = 6;
            this.This_Qty.Name = "This_Qty";
            this.This_Qty.Width = 218;
            // 
            // TransferDept
            // 
            this.TransferDept.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.TransferDept.DataPropertyName = "TransferDept";
            this.TransferDept.HeaderText = "调拨至";
            this.TransferDept.MinimumWidth = 6;
            this.TransferDept.Name = "TransferDept";
            this.TransferDept.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TransferDept.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.TransferDept.Width = 170;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 9;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 152F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 196F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 166F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 256F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 185F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 237F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 95F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 194F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel2.Controls.Add(this.label_SalesOrder, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label_SubProdOrder, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.textBox_SalesOrder, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.textBox_SubProdOrder, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.label_MainProdOrder, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label_DeptNo, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.textBox_DeptNo, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.label_RoutNo, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.label_OrgId, 4, 1);
            this.tableLayoutPanel2.Controls.Add(this.comboBox_RoutNo, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.comboBox_OrgId, 5, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(2, 56);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.15254F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.84746F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1257, 105);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // label_SalesOrder
            // 
            this.label_SalesOrder.AutoSize = true;
            this.label_SalesOrder.Dock = System.Windows.Forms.DockStyle.Right;
            this.label_SalesOrder.Location = new System.Drawing.Point(2, 51);
            this.label_SalesOrder.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_SalesOrder.Name = "label_SalesOrder";
            this.label_SalesOrder.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_SalesOrder.Size = new System.Drawing.Size(148, 54);
            this.label_SalesOrder.TabIndex = 1;
            this.label_SalesOrder.Text = "销售订单:";
            this.label_SalesOrder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_SubProdOrder
            // 
            this.label_SubProdOrder.AutoSize = true;
            this.label_SubProdOrder.Dock = System.Windows.Forms.DockStyle.Right;
            this.label_SubProdOrder.Location = new System.Drawing.Point(394, 0);
            this.label_SubProdOrder.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_SubProdOrder.Name = "label_SubProdOrder";
            this.label_SubProdOrder.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_SubProdOrder.Size = new System.Drawing.Size(118, 51);
            this.label_SubProdOrder.TabIndex = 2;
            this.label_SubProdOrder.Text = "子工单:";
            this.label_SubProdOrder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox_MainProdOrder);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(154, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(192, 47);
            this.panel1.TabIndex = 8;
            // 
            // textBox_MainProdOrder
            // 
            this.textBox_MainProdOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_MainProdOrder.Location = new System.Drawing.Point(0, 0);
            this.textBox_MainProdOrder.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_MainProdOrder.Name = "textBox_MainProdOrder";
            this.textBox_MainProdOrder.Size = new System.Drawing.Size(192, 41);
            this.textBox_MainProdOrder.TabIndex = 10;
            // 
            // textBox_SalesOrder
            // 
            this.textBox_SalesOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_SalesOrder.Location = new System.Drawing.Point(154, 53);
            this.textBox_SalesOrder.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_SalesOrder.Name = "textBox_SalesOrder";
            this.textBox_SalesOrder.Size = new System.Drawing.Size(192, 41);
            this.textBox_SalesOrder.TabIndex = 9;
            // 
            // textBox_SubProdOrder
            // 
            this.textBox_SubProdOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_SubProdOrder.Location = new System.Drawing.Point(516, 2);
            this.textBox_SubProdOrder.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_SubProdOrder.Name = "textBox_SubProdOrder";
            this.textBox_SubProdOrder.Size = new System.Drawing.Size(252, 41);
            this.textBox_SubProdOrder.TabIndex = 10;
            // 
            // label_MainProdOrder
            // 
            this.label_MainProdOrder.AutoSize = true;
            this.label_MainProdOrder.Dock = System.Windows.Forms.DockStyle.Right;
            this.label_MainProdOrder.Location = new System.Drawing.Point(32, 0);
            this.label_MainProdOrder.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_MainProdOrder.Name = "label_MainProdOrder";
            this.label_MainProdOrder.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_MainProdOrder.Size = new System.Drawing.Size(118, 51);
            this.label_MainProdOrder.TabIndex = 19;
            this.label_MainProdOrder.Text = "主工单:";
            this.label_MainProdOrder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_DeptNo
            // 
            this.label_DeptNo.AutoSize = true;
            this.label_DeptNo.Dock = System.Windows.Forms.DockStyle.Right;
            this.label_DeptNo.Location = new System.Drawing.Point(364, 51);
            this.label_DeptNo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_DeptNo.Name = "label_DeptNo";
            this.label_DeptNo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_DeptNo.Size = new System.Drawing.Size(148, 54);
            this.label_DeptNo.TabIndex = 5;
            this.label_DeptNo.Text = "工作中心:";
            this.label_DeptNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox_DeptNo
            // 
            this.textBox_DeptNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_DeptNo.Location = new System.Drawing.Point(516, 53);
            this.textBox_DeptNo.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_DeptNo.Name = "textBox_DeptNo";
            this.textBox_DeptNo.Size = new System.Drawing.Size(252, 41);
            this.textBox_DeptNo.TabIndex = 18;
            // 
            // label_RoutNo
            // 
            this.label_RoutNo.AutoSize = true;
            this.label_RoutNo.Dock = System.Windows.Forms.DockStyle.Right;
            this.label_RoutNo.Location = new System.Drawing.Point(850, 0);
            this.label_RoutNo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_RoutNo.Name = "label_RoutNo";
            this.label_RoutNo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_RoutNo.Size = new System.Drawing.Size(103, 51);
            this.label_RoutNo.TabIndex = 4;
            this.label_RoutNo.Text = "*制程:";
            this.label_RoutNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_OrgId
            // 
            this.label_OrgId.AutoSize = true;
            this.label_OrgId.Dock = System.Windows.Forms.DockStyle.Right;
            this.label_OrgId.Location = new System.Drawing.Point(850, 51);
            this.label_OrgId.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_OrgId.Name = "label_OrgId";
            this.label_OrgId.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_OrgId.Size = new System.Drawing.Size(103, 54);
            this.label_OrgId.TabIndex = 20;
            this.label_OrgId.Text = "*工厂:";
            this.label_OrgId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBox_RoutNo
            // 
            this.comboBox_RoutNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox_RoutNo.FormattingEnabled = true;
            this.comboBox_RoutNo.Location = new System.Drawing.Point(958, 3);
            this.comboBox_RoutNo.Name = "comboBox_RoutNo";
            this.comboBox_RoutNo.Size = new System.Drawing.Size(231, 38);
            this.comboBox_RoutNo.TabIndex = 23;
            // 
            // comboBox_OrgId
            // 
            this.comboBox_OrgId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox_OrgId.FormattingEnabled = true;
            this.comboBox_OrgId.Location = new System.Drawing.Point(958, 54);
            this.comboBox_OrgId.Name = "comboBox_OrgId";
            this.comboBox_OrgId.Size = new System.Drawing.Size(231, 38);
            this.comboBox_OrgId.TabIndex = 24;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.btnQuery, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnSave, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnExport, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.button1, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnRefresh, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 2);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1257, 54);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnQuery
            // 
            this.btnQuery.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnQuery.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnQuery.Location = new System.Drawing.Point(2, 2);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(2);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(124, 50);
            this.btnQuery.TabIndex = 0;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = false;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSave.Location = new System.Drawing.Point(152, 2);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(124, 50);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnExport.Location = new System.Drawing.Point(602, 2);
            this.btnExport.Margin = new System.Windows.Forms.Padding(2);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(124, 50);
            this.btnExport.TabIndex = 2;
            this.btnExport.Text = "导出";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Visible = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.button1.Location = new System.Drawing.Point(452, 2);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(124, 50);
            this.button1.TabIndex = 4;
            this.button1.Text = "关闭";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnRefresh.Location = new System.Drawing.Point(302, 2);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(124, 50);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "重置";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // InvestmentReturnOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1269, 719);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "InvestmentReturnOrder";
            this.Text = "生产调拨";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.InvestmentReturnOrderForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvIntoProduction)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label_SalesOrder;
        private System.Windows.Forms.Label label_SubProdOrder;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox_SalesOrder;
        private System.Windows.Forms.TextBox textBox_SubProdOrder;
        private System.Windows.Forms.DataGridView dgvIntoProduction;
        private System.Windows.Forms.TextBox textBox_DeptNo;
        private System.Windows.Forms.Label label_MainProdOrder;
        private System.Windows.Forms.Label label_DeptNo;
        private System.Windows.Forms.Label label_RoutNo;
        private System.Windows.Forms.Label label_OrgId;
        private System.Windows.Forms.TextBox textBox_MainProdOrder;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox_RoutNo;
        private System.Windows.Forms.ComboBox comboBox_OrgId;
        private System.Windows.Forms.DataGridViewTextBoxColumn SE_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn main_prod_order;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRODUCTION_ORDER;
        private System.Windows.Forms.DataGridViewTextBoxColumn SIZE_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ROUT_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn D_DEPT;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn In_Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Out_Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Transfer_Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn This_Qty;
        private System.Windows.Forms.DataGridViewComboBoxColumn TransferDept;
    }
}

