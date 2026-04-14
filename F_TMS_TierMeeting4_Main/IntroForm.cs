using MaterialSkin.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;
using TierMeeting;
namespace TierMeeting
{
    public partial class IntroForm : MaterialForm
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        public IntroForm()
        {
            InitializeComponent();
        }
        private void IntroForm_Load(object sender, EventArgs e)
        {
            InitUI();
        }
        private void InitUI() {
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);
            this.WindowState = FormWindowState.Maximized;
            tableLayoutPanel3.BackColor = Color.FromArgb(50, Color.Black);
            if (base.Height<1000) ResizeText();
        }
        private void ResizeText() {
            int temp = 25;
            lblAnalyzingTool.Font = new Font("SimSun", temp, FontStyle.Bold); ;
            lblAttendanceRecord.Font = new Font("SimSun", temp, FontStyle.Bold); ;
            lblBasicInformation.Font = new Font("SimSun", temp, FontStyle.Bold); ;
            lblIndicatorReview.Font = new Font("SimSun", temp, FontStyle.Bold); ;
            lblMeetingCharter.Font = new Font("SimSun", temp, FontStyle.Bold); ;
            lblAnnouncement.Font = new Font("SimSun", temp, FontStyle.Bold); 
            lblLean.Font = new Font("Gill Sans MT", 150, FontStyle.Bold); 
            lblTierMeetingChinese.Font = new Font("SimSun", 50, FontStyle.Bold); 
            lblTierMeetingEnglish.Font = new Font("SimSun", 50, FontStyle.Bold); 
        }

        private void lblMeetingCharter_Click(object sender, EventArgs e)
        {
            Form frm = new TMS_MeetingCharter();
            frm.Show();
        }

        private void lblBasicInformation_Click(object sender, EventArgs e)
        {
            Form frm = new TMS_BasicInformation();
            frm.Show();
        }

        private void lblAttendanceRecord_Click(object sender, EventArgs e)
        {
            Form frm = new TMS_AttendanceRecord();
            frm.Show();
        }

        private void lblIndicatorReview_Click(object sender, EventArgs e)
        {
            Form frm = new MainForm();
            frm.Show();
        }

        private void lblAnalyzingTool_Click(object sender, EventArgs e)
        {
            Form frm = new TMS_AnalyzingTool();
            frm.Show();
        }
    }
}
