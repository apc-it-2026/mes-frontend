using Newtonsoft.Json;
using SJeMES_Control_Library;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KaizenForm
{
    public partial class Imageview : Form
    {
        public Imageview(Image image)
        {
            InitializeComponent();
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.Image = image;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.Image = image;
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.Image = image;
            

        }

       

        private void Imageview_Load(object sender, EventArgs e)
        {

        }



        public Imageview(Image image ,Image image1 ,Image image2)
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Image = image;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.Image = image1;
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.Image = image2;

        }

      




    }
}
