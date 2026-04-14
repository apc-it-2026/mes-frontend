using AutocompleteMenuNS;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace F_Safety_System
{
    public partial class Safety_System : Form
    {
        public List<Dictionary<string, object>> filediclist = new List<Dictionary<string, object>>();
        public Dictionary<string, object> filedic = new Dictionary<string, object>();
        byte[] fileContent;
        string base64Image;
        float Pointx = 0;
        float Pointy = 0;

        float Lastx = 0;
        float Lasty = 0;
        DataTable dt;
        public string User, UDF05, Device_Status, Device_No, SAFETY, SUP_BARCODE, SIG, INSERT_DATE;
      //  byte[] sig;
        AutoCompleteStringCollection Autodata;
        private Point? previousPoint = null;
        private Pen pen = new Pen(Color.Red, 2);
        byte[] signatureBytes;


        private Dictionary<int, List<Point>> signatureObject = new Dictionary<int, List<Point>>();
        private Pen signaturePen = new Pen(Color.Black, 2);
        private List<Point> currentCurvePoints;
        private int currentCurve = -1;



        public Safety_System()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            // Somewhere in your form's initialization code, perhaps in the constructor
            Operator_txt.KeyPress += Operator_txt_KeyPress;

        }
        private void Pressbtn_Click(object sender, EventArgs e)
        {
            byte[] imageData = null;
            try
            {
                string Device_FA = FA.Text.ToUpper();
                dt = new DataTable();
                Dictionary<string, string> kk = new Dictionary<string, string>();
                kk.Add("MachineNO", Device_FA.ToString());
                string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineUniappServer", "GetmaintenanceListBySNid",
                    Program.Client.UserToken, JsonConvert.SerializeObject(kk));
                if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    dt = JsonConvert.DeserializeObject<DataTable>(json);
                    if (dt.Rows.Count < 0)
                    {
                        string msg = "No Data!";
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                        return;
                    }
                    if (!dt.Columns.Contains("SAFETY_NAME") && !dt.Columns.Contains("SUP_BARCODE"))
                    {
                        DeviceFAtxt.Text = dt.Rows[0]["MACHINE_NO"].ToString();
                        DeviceNametxt.Text = dt.Rows[0]["MACHINE_NAME"].ToString();
                        DeviceModeltxt.Text = dt.Rows[0]["TYPE"].ToString();
                        Storagepostxt.Text = dt.Rows[0]["ADDRESS"].ToString();
                        User = dt.Rows[0]["by_user"].ToString();
                        UDF05 = dt.Rows[0]["UDF05"].ToString();
                        Device_Status = dt.Rows[0]["DEVICE_STATUS"].ToString();
                        Device_No = dt.Rows[0]["DEVICE_NO"].ToString();
                        Getsupervisorname();
                    }

                    else
                    {
                        DeviceFAtxt.Text = dt.Rows[0]["MACHINE_NO"].ToString();
                        DeviceNametxt.Text = dt.Rows[0]["MACHINE_NAME"].ToString();
                        DeviceModeltxt.Text = dt.Rows[0]["TYPE"].ToString();
                        Storagepostxt.Text = dt.Rows[0]["ADDRESS"].ToString();
                        User = dt.Rows[0]["by_user"].ToString();
                        UDF05 = dt.Rows[0]["UDF05"].ToString();
                        Device_Status = dt.Rows[0]["DEVICE_STATUS"].ToString();
                        Device_No= dt.Rows[0]["DEVICE_NO"].ToString();
                        SAFETY = dt.Rows[0]["SAFETY_NAME"].ToString();
                        SUP_BARCODE = dt.Rows[0]["SUP_BARCODE"].ToString();
                        SIG = dt.Rows[0]["SIGNATURE"].ToString();
                        INSERT_DATE = dt.Rows[0]["INSERT_DATE"].ToString();
                        installatio_date_txt.Text = INSERT_DATE;
                        base64Image = SIG.Replace("\"", "");

                        safety_dev_txt.Text = SAFETY;
                        Operator_txt.Text = SUP_BARCODE;
                        Getsupervisorname();
                        // Convert the Base64-encoded string to a byte array
                        imageData = Convert.FromBase64String(base64Image);
                        Image image1 = ByteArrayToImage(imageData);
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                        pictureBox1.Image = image1;
                        if (pictureBox1.Image != null)
                            Clearbtn.Visible = true;
                    }
                }
                else
                {
                    MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("An error occurred: " + ex.Message);
                MessageHelper.ShowErr(this, "An error occurred. Please Enter Currect FA number");
            }
        }
        private Image ByteArrayToImage(byte[] byteArray)
        {
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }

        private void Safety_System_Load(object sender, EventArgs e)
        {
            LoadModel();
            autocompleteMenu1.SetAutocompleteMenu(FA, autocompleteMenu1);
            // no smaller than design time size
            this.MinimumSize = new System.Drawing.Size(this.Width, this.Height);

            // no larger than screen size
            this.MaximumSize = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            string Install_date = DateTime.Now.ToString();
            installatio_date_txt.Text = Install_date;
            pictureBox2.Visible = false;

        }
        public void LoadModel()
        {

            FA.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            FA.AutoCompleteSource = AutoCompleteSource.CustomSource;
            Autodata = new AutoCompleteStringCollection();
            dt = new DataTable();
            Dictionary<string, string> kk = new Dictionary<string, string>();
            string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineUniappServer", "AutoMachineload",
                Program.Client.UserToken, JsonConvert.SerializeObject(kk));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dt = JsonConvert.DeserializeObject<DataTable>(json);
                if (dt.Rows.Count <= 0)
                {
                    MessageBox.Show("No data Found!");
                    return;
                }
                else
                {
                    autocompleteMenu1.MaximumSize = new Size(250, 350);
                    var columnWidth = new[] { 50, 200 };
                    int n = 1;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        autocompleteMenu1.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dt.Rows[i]["MACHINE_NO"].ToString() }, dt.Rows[i]["MACHINE_NO"].ToString()) { ColumnWidth = columnWidth, ImageIndex = n });
                        n++;
                    }
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
          
            if (e.Button != MouseButtons.Left || currentCurve < 0)
                return;
            signatureObject[currentCurve].Add(e.Location);
            pictureBox1.Invalidate();


        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            previousPoint = null;
        }

        
        public void Getsupervisorname()
        {

            Dictionary<string, string> kk = new Dictionary<string, string>();
            kk.Add("location", Storagepostxt.Text.ToString());
            string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineUniappServer", "Getsupervisorname",
                Program.Client.UserToken, JsonConvert.SerializeObject(kk));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dt = JsonConvert.DeserializeObject<DataTable>(json);
                if (dt.Rows.Count < 0)
                {
                    string msg = "No Data!";
                    MessageHelper.ShowErr(this, msg);
                    return;
                }
                if (dt.Columns.Contains("SUPERVISOR"))
                {
                    supernametxt.Text = dt.Rows[0]["SUPERVISOR"].ToString();

                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void Closebtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        private void Operator_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // If the pressed key is not a digit or a control key, set Handled to true
                e.Handled = true;
            }

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            //previousPoint = e.Location;
            currentCurvePoints = new List<Point>();
            currentCurve += 1;
            signatureObject.Add(currentCurve, currentCurvePoints);

        }

        private void Submitbtn_Click(object sender, EventArgs e)
        {
           
            try
            {
                if (FA.Text == "")
                {
                    MessageHelper.ShowErr(this, "Please Select FA Number");
                    return;
                }
                if (DeviceFAtxt.Text == "")
                {
                    MessageHelper.ShowErr(this, "FA Number Should not be Empty");
                    return;
                }
                if (DeviceNametxt.Text == "")
                {
                    MessageHelper.ShowErr(this, "DeviceName Should not be Empty");
                    return;
                }
                if (Storagepostxt.Text == "")
                {
                    MessageHelper.ShowErr(this, "Location Should not be Empty");
                    return;
                }
                if(safety_dev_txt.Text =="")
                {
                    MessageHelper.ShowErr(this, "Safety device Should not be Empty");
                    return;
                }
                if (installatio_date_txt.Text == "")
                {
                    MessageHelper.ShowErr(this, "Installation date Should not be Empty");
                   
                }
                if (Operator_txt.Text == "")
                {
                    MessageHelper.ShowErr(this, "Machine Operator Barcode Should not be Empty");

                }
                if (UDF05 == "")
                {
                    MessageHelper.ShowErr(this, "Machine IN_Date Barcode Should not be Empty");
                    return;

                }
                if (Device_Status == "")
                {
                    MessageHelper.ShowErr(this, "Machine Status  Should not be Empty");

                }

                else
                {
                    string Device_FA        = DeviceFAtxt.Text.ToUpper();
                    string Devicename       = DeviceNametxt.Text.ToUpper();
                    string Devicemodel      = DeviceModeltxt.Text.ToUpper();
                    string Devicelocation   = Storagepostxt.Text.ToUpper();
                    string safety_dev       = safety_dev_txt.Text.ToUpper();
                                   
                    string Operator_Barcode = Operator_txt.Text.ToUpper();             
                    using (Bitmap imgSignature = new Bitmap(pictureBox1.Width, pictureBox1.Height, PixelFormat.Format32bppArgb))
                    {
                        using (Graphics g = Graphics.FromImage(imgSignature))
                        {
                            DrawSignature(g);
                            // imgSignature.Save(signaturePath, ImageFormat.Png);
                            pictureBox2.Image = new Bitmap(imgSignature);
                        }
                    }            
                    byte[] byteArray1 =null;


                    // Check if pictureBox1 has an image
                    if (pictureBox2.Image != null )
                    {
                        byteArray1= PictureBoxToByteArray(pictureBox2);
                        // Add the byte array to filediclist
                        Dictionary<string, object> filedic = new Dictionary<string, object>();
                        filedic.Add("file_content", byteArray1);
                        filediclist.Add(filedic);
                    }
                    else
                    {
                        // Handle the case where pictureBox1 does not have an image
                        Console.WriteLine("Error: pictureBox1 does not have an image.");
                    }

                    string base64Image = Convert.ToBase64String(byteArray1);
          
                    Dictionary<string, object> SS = new Dictionary<string, object>();
                  // string signatureHex = BitConverter.ToString(signatureBytes).Replace("-", "");
                    SS.Add("MachineNO", Device_FA);
                    SS.Add("MachineName", Devicename);
                    SS.Add("Type", Devicemodel);
                    SS.Add("Address", Devicelocation);

                    SS.Add("IN_DATE", UDF05);
                    SS.Add("STATUS", Device_Status);

                    SS.Add("SafetyName", safety_dev);
                    SS.Add("Installdate", installatio_date_txt.Text);
                    SS.Add("Operator_Barcode", Operator_Barcode);
                    // SS.Add("signature", signatureBytes != null ? signatureBytes : (object)DBNull.Value);
                   SS.Add("signature", base64Image);
                    SS.Add("DEVICE_NO", Device_No);

                    //string BZtable = JsonConvert.SerializeObject(SS);
                    string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineUniappServer", "SaveMachineData",
                        Program.Client.UserToken, JsonConvert.SerializeObject(SS));
                    if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                    {
                        string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                        // dt = JsonConvert.DeserializeObject<DataTable>(json);
                        if (json == "1")
                        {
                            MessageHelper.ShowSuccess(this, "Dear:" + User + " Submitted Succesfully");
                        }
                       else if (json == "2")
                        {
                            MessageHelper.ShowSuccess(this, "Dear:" + User + " Updated Succesfully");
                        }
                        else
                        {
                            MessageHelper.ShowErr(this, "Failed Submission");
                            return;
                        }
                    }
                    else
                    {
                        MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                    }
                }
                FA.Text = "";
                DeviceFAtxt.Text = "";
                DeviceNametxt.Text = "";
                DeviceModeltxt.Text = "";
                Storagepostxt.Text = "";
                safety_dev_txt.Text = "";
                DeviceFAtxt.Text = "";              
                pictureBox1.Image = null;
                Operator_txt.Text = "";
                installatio_date_txt.Text = "";
                currentCurve = -1;
                signatureObject.Clear();
                pictureBox1.Invalidate();
                supernametxt.Text = "";





            }
            catch (WebException webEx)
            {

                Console.WriteLine("WebException: " + webEx.Message);
                MessageHelper.ShowErr(this, "A web error occurred. Please try again.");

            }
            catch (Exception ex)
            {

                Console.WriteLine("An error occurred: " + ex.Message);
                MessageHelper.ShowErr(this, "An error occurred. Not Found this FA Number");
            }
        }

        private void FA_KeyDown(object sender, KeyEventArgs e)
        {
            byte[] imageData = null;
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string Device_FA = FA.Text.ToUpper();
                    dt = new DataTable();
                    Dictionary<string, string> kk = new Dictionary<string, string>();
                    kk.Add("MachineNO", Device_FA.ToString());
                    string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineUniappServer", "GetmaintenanceListBySNid",
                        Program.Client.UserToken, JsonConvert.SerializeObject(kk));
                    if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                    {
                        string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                        dt = JsonConvert.DeserializeObject<DataTable>(json);
                        if (dt.Rows.Count < 0)
                        {
                            string msg = "No Data!";
                            MessageHelper.ShowErr(this, msg);
                            return;
                        }
                        if (!dt.Columns.Contains("SAFETY_NAME") && !dt.Columns.Contains("SUP_BARCODE"))
                        {
                            DeviceFAtxt.Text = dt.Rows[0]["MACHINE_NO"].ToString();
                            DeviceNametxt.Text = dt.Rows[0]["MACHINE_NAME"].ToString();
                            DeviceModeltxt.Text = dt.Rows[0]["TYPE"].ToString();
                            Storagepostxt.Text = dt.Rows[0]["ADDRESS"].ToString();
                            User = dt.Rows[0]["by_user"].ToString();
                            UDF05 = dt.Rows[0]["UDF05"].ToString();
                            Device_Status = dt.Rows[0]["DEVICE_STATUS"].ToString();
                            Device_No = dt.Rows[0]["DEVICE_NO"].ToString();
                            Getsupervisorname();
                        }

                        else
                        {
                            DeviceFAtxt.Text = dt.Rows[0]["MACHINE_NO"].ToString();
                            DeviceNametxt.Text = dt.Rows[0]["MACHINE_NAME"].ToString();
                            DeviceModeltxt.Text = dt.Rows[0]["TYPE"].ToString();
                            Storagepostxt.Text = dt.Rows[0]["ADDRESS"].ToString();
                            User = dt.Rows[0]["by_user"].ToString();
                            UDF05 = dt.Rows[0]["UDF05"].ToString();
                            Device_Status = dt.Rows[0]["DEVICE_STATUS"].ToString();
                            Device_No = dt.Rows[0]["DEVICE_NO"].ToString();
                            SAFETY = dt.Rows[0]["SAFETY_NAME"].ToString();
                            SUP_BARCODE = dt.Rows[0]["SUP_BARCODE"].ToString();
                            SIG = dt.Rows[0]["SIGNATURE"].ToString();
                            INSERT_DATE = dt.Rows[0]["INSERT_DATE"].ToString();
                            installatio_date_txt.Text = INSERT_DATE;
                            base64Image = SIG.Replace("\"", "");

                            safety_dev_txt.Text = SAFETY;
                            Operator_txt.Text = SUP_BARCODE;
                            Getsupervisorname();
                            // Convert the Base64-encoded string to a byte array
                            imageData = Convert.FromBase64String(base64Image);
                            Image image1 = ByteArrayToImage(imageData);
                            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                            pictureBox1.Image = image1;
                            if (pictureBox1.Image != null)
                                Clearbtn.Visible = true;
                        }
                    }
                    else
                    {
                        //MessageBox.Show("Error");
                        MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("An error occurred: " + ex.Message);
                MessageHelper.ShowErr(this, "An error occurred. Please Enter Currect FA number");
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (currentCurve < 0 || signatureObject[currentCurve].Count == 0)
                return;
            DrawSignature(e.Graphics);

        }
        private void DrawSignature(Graphics g)
        {
            g.CompositingMode = CompositingMode.SourceOver;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            foreach (var curve in signatureObject)
            {
                if (curve.Value.Count < 2)
                    continue;
                using (GraphicsPath gPath = new GraphicsPath())
                {
                    gPath.AddCurve(curve.Value.ToArray(), 0.5F);
                    g.DrawPath(signaturePen, gPath);
                }
            }
        }

        private void Clearbtn_Click(object sender, EventArgs e)
        {
            currentCurve = -1;
            signatureObject.Clear();
            pictureBox1.Invalidate();

        }
       // Define the PictureBoxToByteArray method outside of any code block
        public byte[] PictureBoxToByteArray(PictureBox pictureBox)
        {
            if (pictureBox.Image != null)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    pictureBox.Image.Save(stream, ImageFormat.Png);
                    return stream.ToArray();
                }
            }
            else
            {
                // Handle the case where pictureBox does not have an image
                Console.WriteLine("Error: PictureBox does not have an image.");
                return null;
            }
        }


    }
}
