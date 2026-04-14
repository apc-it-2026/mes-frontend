using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SJeMES_Control_Library;
using SJeMES_Framework.WebAPI;
using Newtonsoft.Json;
using SJeMES_Framework.Common;
using Newtonsoft.Json.Linq;

namespace PlanningSchedule
{
    public partial class Reschedule : Form
    {
        private object _data;
/*        private string _org;
        private string _plant;
        private string _process;
        private string _line; */ 
      //  private string _status;
        private string _ipaddress; 

      /*  public class ComboboxEntry
        {
            public string Code { get; set; }
            public string Name { get; set; }
        }*/

        public Reschedule(object dataSource , string ipaddress  )  
        {
            InitializeComponent();

            _data = dataSource; 
           /* _org  = org; 
            _plant = plant;
            _process = process;*/
            _ipaddress = ipaddress; 
            // StyleComboBox(comboBox7); 
            StyleRoundedButton(button10, Color.FromArgb(241, 196, 15));
           // LoadLines(_org , _plant , _process  );   
        } 

       /* private void StyleComboBox(ComboBox comboBox)
        {
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList; // Prevent typing, select only
            comboBox.FlatStyle = FlatStyle.Flat;                 // Flat modern look
            comboBox.BackColor = Color.White;                    // Background color
            comboBox.ForeColor = Color.Black;                    // Text color
            comboBox.Font = new Font("Segoe UI", 10, FontStyle.Regular); // Font style
            comboBox.Margin = new Padding(2);
            comboBox.Cursor = Cursors.Hand;                      // Change cursor to hand
            comboBox.Width = 200;                                // Optional width

            // Optional: Add a border (simulate since WinForms ComboBox doesn’t support border color)
            comboBox.Region = new Region(comboBox.ClientRectangle);

            comboBox.DrawItem += (s, e) =>
            {
                e.DrawBackground();
                if (e.Index >= 0)
                {
                    string text = comboBox.Items[e.Index].ToString();
                    Brush brush = new SolidBrush(e.ForeColor);
                    e.Graphics.DrawString(text, e.Font, brush, e.Bounds);
                }
                e.DrawFocusRectangle();
            };
        }*/ 
      
        private void StyleRoundedButton(Button btn, Color color)
        {
            btn.BackColor = color;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;

            // Rounded corners using GraphicsPath
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radius = 10;
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(btn.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(btn.Width - radius, btn.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, btn.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();
            btn.Region = new Region(path);
        }
     
        /*  private void LoadLines(string OrgId, String Plant, String Process)  
        {
                try
                {
                    List<ComboboxEntry> Lines = new List<ComboboxEntry> { };
                    Dictionary<string, object> Data = new Dictionary<string, object>();
                    Data.Add("OrgId", OrgId);
                    Data.Add("Plant", Plant);
                    Data.Add("Process", Process);
                    string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_CUTMNT",
                        "KZ_CUTMNT.Controllers.PlanningController",
                        "LoadLines", Program.client.UserToken, JsonConvert.SerializeObject(Data));
                    ResultObject ret = JsonConvert.DeserializeObject<ResultObject>(retdata);
                    var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
                    if (result != null && result.ContainsKey("IsSuccess") && Convert.ToBoolean(result["IsSuccess"])
                       && result.ContainsKey("RetData") && result["RetData"] != "")
                    {
                        string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["RetData"].ToString();
                        DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                        Lines.Add(new ComboboxEntry() { Code = "", Name = "" });
                        for (int i = 0; i < dtJson.Rows.Count; i++)
                        {
                            Lines.Add(new ComboboxEntry() { Code = dtJson.Rows[i]["department_code"].ToString(), Name = dtJson.Rows[i]["department_name"].ToString() });
                        }

                        comboBox7.DataSource = Lines;
                        comboBox7.DisplayMember = "Name";
                        comboBox7.ValueMember = "Code";
                        comboBox7.SelectedIndexChanged += (s, e) =>
                        {
                            if (comboBox7.SelectedIndex >= 0 && comboBox7.SelectedValue != null)
                            {
                                string selectedCode = comboBox7.SelectedValue.ToString();
                                string selectedName = comboBox7.Text;
                                _line = selectedCode; 
                            }
                        };
                }
                }
                catch (Exception ex)
                {
                    MessageHelper.ShowErr(this, "Error: " + ex.Message);
                }
        }
*/
       
        private void button10_Click(object sender, EventArgs e)
        {
            if (!ConfirmAction("Do you want to save and continue?", "⚠️ Confirmation"))
                return;

            // 🟢 Step 2: Ask for reason
            string reason = PromptReason("Reason Required", "Please enter the reason for saving:");

            if (string.IsNullOrWhiteSpace(reason))
            {
                MessageHelper.ShowErr(this, "❌ Save cancelled. Reason is required.");
                return;
            } 
            ReschedulePos(reason);   
        } 

        private bool ConfirmAction(string message, string title)
        {
            DialogResult result = MessageBox.Show(
                message,
                title,
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question
            );

            return result == DialogResult.OK;
        }

        // ✅ Method 2: Reason Input Box
        private string PromptReason(string title, string prompt)
        {
            using (Form inputForm = new Form())
            {
                inputForm.Text = title;
                inputForm.StartPosition = FormStartPosition.CenterParent;
                inputForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                inputForm.ClientSize = new Size(350, 150);
                inputForm.MaximizeBox = false;
                inputForm.MinimizeBox = false;

                Label lblPrompt = new Label() { Text = prompt, Left = 15, Top = 15, Width = 320 };
                TextBox txtReason = new TextBox() { Left = 15, Top = 45, Width = 320 };
                Button btnOk = new Button() { Text = "OK", Left = 170, Width = 75, Top = 90, DialogResult = DialogResult.OK };
                Button btnCancel = new Button() { Text = "Cancel", Left = 260, Width = 75, Top = 90, DialogResult = DialogResult.Cancel };

                inputForm.Controls.AddRange(new Control[] { lblPrompt, txtReason, btnOk, btnCancel });
                inputForm.AcceptButton = btnOk;
                inputForm.CancelButton = btnCancel;

                DialogResult dialogResult = inputForm.ShowDialog();

                return dialogResult == DialogResult.OK ? txtReason.Text.Trim() : string.Empty;
            }
        }
        private void ReschedulePos( string reason ) 
        {
            try
            {
                var data = new Dictionary<string, object>
                {
                    ["week"] = $"{dateTimePicker3.Text}-{dateTimePicker4.Text}",
             /*       ["OrgId"] = _org,
                    ["Plant"] = _plant,
                    ["Process"] = _process,
                    ["line"] = _line ,*/ 
                    ["ipaddress"] = _ipaddress ,
                    ["reason"] = reason , 
                }; 

                data.Add("dataSource", _data); 
                string response = WebAPIHelper.Post(
                    Program.client.APIURL,
                    "KZ_CUTMNT",
                    "KZ_CUTMNT.Controllers.PlanningController", 
                    "RescheduleMethod", 
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(data)
                );

                var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(response);
                if (result == null || !result.ContainsKey("IsSuccess"))
                {
                    MessageHelper.ShowErr(this, "Invalid or empty response from server.");
                    return;
                }

                bool isSuccess = Convert.ToBoolean(result["IsSuccess"]);


                if (isSuccess && result["RetData"] != null)
                {
                    string json = result["RetData"].ToString();   // JSON string

                    // Parse the JSON array
                    JArray arr = JArray.Parse(json);

                    // Get first object
                    JObject obj = (JObject)arr[0];

                    string message = obj["Result"]?.ToString();

                    MessageHelper.ShowOK(this, message);
                   /* DataTable dt = (DataTable)_data; 

                    List<string> list = dt.AsEnumerable()
                                          .Select(row => row["SALES_ORDER"].ToString())
                                          .ToList(); */
                    //OpenSizeReschedule form = new OpenSizeReschedule(_data);   
                   // form.ShowDialog(); 
                } 

                else if (result.ContainsKey("ErrMsg") && !string.IsNullOrEmpty(result["ErrMsg"]?.ToString()))
                {
                    string apiErrorMessage = result["ErrMsg"].ToString();
                    MessageHelper.ShowErr(this, $"API Error: {apiErrorMessage}\nPlease contact the IT department.");
                }
                else
                {
                    MessageHelper.ShowErr(this, "The Reschedule Was not Done. Please Contact IT");  
                } 
            }
            catch (Exception ex)
            {
                MessageHelper.ShowErr(this, $"An error occurred. Please contact IT Department.\nDetails: {ex.Message}");
            } 
        }   


    }
}
