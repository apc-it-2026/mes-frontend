using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SJeMES_Control_Library.Forms
{
    public class FrmWare : Form
    {
        public bool is_sure = false;
        private DataGridView dataGridView;
        private Button btnConfirm;
        private Button btnCancel;
        public FormStartPosition StartPosition { get; internal set; }
        public FrmWare(DataTable dt)
        {
            InitializeComponent();

            if (dt != null)
            {
                dataGridView.DataSource = dt;

                foreach (DataGridViewColumn column in dataGridView.Columns)
                {
                    column.Width = 150;
                }
            }
        }

        private void InitializeComponent()
        {
            this.Text = "Confirm Import Data";
            this.Size = new Size(800, 580);
            this.StartPosition = FormStartPosition.CenterScreen;

            Font radioFont = new Font("Lucida Bright", 10.5F, FontStyle.Bold);

            dataGridView = new DataGridView
            {
                Left = 10,
                Top = 10,
                Width = this.ClientSize.Width - 20,
                Height = 400,
                ReadOnly = true,
                ScrollBars = ScrollBars.Both,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
                AllowUserToResizeColumns = true,
                AllowUserToResizeRows = false,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            btnConfirm = new Button
            {
                Text = "Confirm",
                Width = 120,
                Height = 35,
                Left = 200,
                Top = 470,
                BackColor = Color.FromArgb(46, 204, 113), // Green
                ForeColor = Color.White,
                Font = new Font("Lucida Bright", 10.5F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            btnConfirm.Click += BtnConfirm_Click;

            btnCancel = new Button
            {
                Text = "Cancel",
                Width = 120,
                Height = 35,
                Left = 460,
                Top = 470,
                BackColor = Color.FromArgb(231, 76, 60), // Red
                ForeColor = Color.White,
                Font = new Font("Lucida Bright", 10.5F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            btnCancel.Click += BtnCancel_Click;
            this.Controls.Add(dataGridView);
            this.Controls.Add(btnConfirm);
            this.Controls.Add(btnCancel);
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            is_sure = true;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            is_sure = false;
            this.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dataGridView?.Dispose();
                btnConfirm?.Dispose();
                btnCancel?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}