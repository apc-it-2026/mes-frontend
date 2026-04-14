using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Production_Kanban
{
    public partial class DragForm : Form
    {
        private int count = 1;
        public DragForm()
        {
            InitializeComponent();
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        private void DragForm_DragEnter(object sender, DragEventArgs e)
        {
            if ((e.Data.GetDataPresent(typeof(Button))))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void DragForm_DragDrop(object sender, DragEventArgs e)
        {
            //拖放完毕之后，自动生成新控件
            Button btn = new Button();
            btn.Size = button1.Size;
            btn.Location = this.PointToClient(new Point(e.X, e.Y));
            //用这个方法计算出客户端容器界面的X，Y坐标。否则直接使用X，Y是屏幕坐标
            this.Controls.Add(btn);
            btn.Text = "按钮" + count.ToString();
            count = count + 1;
        }

        private void DragForm_Load(object sender, EventArgs e)
        {
            this.AllowDrop = true;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            Test();
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button == System.Windows.Forms.MouseButtons.Left))
            {
                button1.DoDragDrop(button1, DragDropEffects.Copy | DragDropEffects.Move);
                //形成拖拽效果，移动+拷贝的组合效果
            }
        }

        private void Test()
        {
            //try
            //{
            //    string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.Production_KanbanServer", "Test", Program.client.UserToken, string.Empty);
            //    if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            //    {
            //        string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
            //        DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
            //        try
            //        {
            //            byte[] bytes = System.Text.Encoding.Default.GetBytes("");
            //            MemoryStream memStream = new MemoryStream(bytes);
            //            Bitmap myImage = new Bitmap(memStream);
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show(ex.Message);
            //            pictureBox1.Image = null;
            //        }         
            //    }
            //    else
            //    {
            //        SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            pictureBox1.Image = Image.FromFile("E:\\001.jpg");
            //pictureBox1.Image = Image.FromStream(System.Net.WebRequest.Create(http://www.xyhhxx.com/images/logo/logo.gif).GetResponse().GetResponseStream());
        }
    }
}
