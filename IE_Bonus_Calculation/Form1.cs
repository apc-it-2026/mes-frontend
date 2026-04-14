using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Windows.Forms;


namespace IE_Bonus_Calculation
{
    public partial class IE_Bonus_Calculator : MaterialForm
    {

        DataTable dt = new DataTable();

        public IE_Bonus_Calculator()
        {
            InitializeComponent();
        }

        private void IE_Bonus_Calculator_Load(object sender, EventArgs e)
        {
            richTextBox1.KeyPress += NumericTextBox_KeyPress;
            richTextBox2.KeyPress += NumericTextBox_KeyPress;
            richTextBox3.KeyPress += NumericTextBox_KeyPress;
            richTextBox4.KeyPress += NumericTextBox_KeyPress;
            richTextBox5.KeyPress += NumericTextBox_KeyPress;

            // Data grid data table

            dt.Columns.Add("Department");
            dt.Columns.Add("Plan Target");
            dt.Columns.Add("Output");
            dt.Columns.Add("Final HC");
            dt.Columns.Add("Work Hours");
            dt.Columns.Add("IE Target");
            dt.Columns.Add("Actual MH");
            dt.Columns.Add("Standard MH");
            dt.Columns.Add("IE Rate");
            dt.Columns.Add("Bonus");

            dataGridView1.DataSource = dt;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 6️⃣ Set header style (background color, font)
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);

            // 7️⃣ Set cell style (optional highlight)
            dataGridView1.DefaultCellStyle.BackColor = Color.Beige;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.LightBlue;

            // 8️⃣ Optional: Make the grid read-only
            dataGridView1.ReadOnly = true;

            // 9️⃣ Refresh to apply styling
            dataGridView1.Refresh();

        }

        private void NumericTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            RichTextBox textbox = sender as RichTextBox;

            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            if (e.KeyChar == '.' && textbox.Text.Contains('.'))
            {
                e.Handled = true;
                return;
            }

            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Calculate_Bonus();
        }

        private void Calculate_Bonus()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(richTextBox7.Text))
                {
                    MessageBox.Show("Please Enter Department.");
                    richTextBox7.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(richTextBox1.Text))
                {
                    MessageBox.Show("Please Enter Plan Target.");
                    richTextBox1.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(richTextBox2.Text))
                {
                    MessageBox.Show("Please Enter Output.");
                    richTextBox2.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(richTextBox3.Text))
                {
                    MessageBox.Show("Please Enter Final Head Count.");
                    richTextBox3.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(richTextBox4.Text))
                {
                    MessageBox.Show("Please Enter Work Hours.");
                    richTextBox4.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(richTextBox5.Text))
                {
                    MessageBox.Show("Please Enter IE Target.");
                    richTextBox5.Focus();
                    return;
                }

                string department = richTextBox7.Text.Trim();
                int planTarget = int.Parse(richTextBox1.Text.Trim());
                int output = int.Parse(richTextBox2.Text.Trim());
                int finalHC = int.Parse(richTextBox3.Text.Trim());
                decimal workHours = decimal.Parse(richTextBox4.Text.Trim());
                decimal ieTarget = decimal.Parse(richTextBox5.Text.Trim());

                decimal actualMH = (finalHC == 0 || workHours == 0) ? 0 : Math.Round(finalHC * workHours, 2);

                decimal standardMH = (output == 0 || ieTarget == 0) ? 0 : Math.Round(output / ieTarget, 2);

                decimal ieRate = 0;
                if (standardMH > 0 && actualMH >0)
                {
                    ieRate = Math.Floor((standardMH / actualMH) * 100);

                }

                decimal bonus = 0;
                if (ieRate >= 35)
                {
                    bonus = ieRate * 1.4m;
                }
                else
                {
                    bonus = 0;
                }

                richTextBox6.Text = bonus.ToString("0.00");


                bool rowUpdated = false;

                // Check if department already exists
                foreach (DataRow row in dt.Rows)
                {
                    if (row["Department"].ToString() == department)
                    {
                        row["Plan Target"] = planTarget;
                        row["Output"] = output;
                        row["Final HC"] = finalHC;
                        row["Work Hours"] = workHours;
                        row["IE Target"] = ieTarget;
                        row["Actual MH"] = actualMH;
                        row["Standard MH"] = standardMH;
                        row["IE Rate"] = ieRate;
                        row["Bonus"] = bonus;
                        rowUpdated = true;
                        break;
                    }
                }

                // If department not found, insert new row at the top
                if (!rowUpdated)
                {
                    DataRow newRow = dt.NewRow();
                    newRow["Department"] = department;
                    newRow["Plan Target"] = planTarget;
                    newRow["Output"] = output;
                    newRow["Final HC"] = finalHC;
                    newRow["Work Hours"] = workHours;
                    newRow["IE Target"] = ieTarget;
                    newRow["Actual MH"] = actualMH;
                    newRow["Standard MH"] = standardMH;
                    newRow["IE Rate"] = ieRate;
                    newRow["Bonus"] = bonus;

                    dt.Rows.InsertAt(newRow, 0); // Top row
                }


                dataGridView1.DataSource = dt;


                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                
                dataGridView1.Refresh();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            Clear_Fields();
        }

        private void Clear_Fields()
        {
            richTextBox1.Clear();
            richTextBox2.Clear();
            richTextBox3.Clear();
            richTextBox4.Clear();
            richTextBox5.Clear();
            richTextBox6.Clear();
            richTextBox7.Clear();
            //dataGridView1.DataSource = null;
            //dataGridView1.Rows.Clear();
            //dataGridView1.Columns.Clear();

            richTextBox7.Focus();
        }

       private void TextBox_Validation()
        {
            richTextBox6.Clear();
            //dataGridView1.DataSource = null;
            //dataGridView1.Rows.Clear();
            //dataGridView1.Columns.Clear();
        }

        private void RichTextBox1_TextChanged(object sender, EventArgs e)
        {
            TextBox_Validation();
            richTextBox1.Focus();
        }

        private void RichTextBox2_TextChanged(object sender, EventArgs e)
        {
            TextBox_Validation();
            richTextBox2.Focus();
        }

        private void RichTextBox3_TextChanged(object sender, EventArgs e)
        {
            TextBox_Validation();
            richTextBox3.Focus();
        }

        private void RichTextBox4_TextChanged(object sender, EventArgs e)
        {
            TextBox_Validation();
            richTextBox4.Focus();
        }

        private void RichTextBox5_TextChanged(object sender, EventArgs e)
        {
            TextBox_Validation();
            richTextBox5.Focus();
        }

        private void RichTextBox7_TextChanged(object sender, EventArgs e)
        {
            TextBox_Validation();
            richTextBox7.Focus();

            int selStart = richTextBox7.SelectionStart;
            richTextBox7.Text = richTextBox7.Text.ToUpper();
            richTextBox7.SelectionStart = selStart;
        }

        private void Export_Excel_Click(object sender, EventArgs e)
        {
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"); 
            string fileName = $"IE_Bonus_{currentDate}.xls";
            ExportExcels(fileName, dataGridView1);
        }

       


        private void ExportExcels(string fileName, DataGridView myDGV)
        {
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                DefaultExt = "xls",
                Filter = "Excel Files|*.xls",
                FileName = fileName
            };

            // ✅ Check if the user clicked OK
            if (saveDialog.ShowDialog() != DialogResult.OK)
            {
                MessageBox.Show("Export cancelled.");
                return;
            }

            string saveFileName = saveDialog.FileName;

            // Create Excel app
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            if (xlApp == null)
            {
                MessageBox.Show("Unable to create Excel object. Maybe Excel is not installed.");
                return;
            }

            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];

            // Write headers
            for (int i = 0; i < myDGV.ColumnCount; i++)
            {
                worksheet.Cells[1, i + 1] = "'" + myDGV.Columns[i].HeaderText.ToString();
            }

            // Write data
            for (int r = 0; r < myDGV.Rows.Count; r++)
            {
                for (int i = 0; i < myDGV.ColumnCount; i++)
                {
                    worksheet.Cells[r + 2, i + 1].NumberFormatLocal = "@";
                    worksheet.Cells[r + 2, i + 1] = myDGV.Rows[r].Cells[i].Value;
                }
                System.Windows.Forms.Application.DoEvents();
            }

            worksheet.Columns.EntireColumn.AutoFit();

            try
            {
                workbook.Saved = true;
                workbook.SaveCopyAs(saveFileName);
                MessageBox.Show("Successfully saved", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting file. It might be open.\n" + ex.Message);
            }
            finally
            {
                xlApp.Quit();
                GC.Collect();
            }
        }


        private void Import_Excel_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel Files|*.xlsx;*xls";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ExcelData_Calculation(ofd.FileName);
            }
        }

        private void ExcelData_Calculation(string filePath)
        {
            try
            {
                dt.Clear();
                dt.Rows.Clear();
                Clear_Fields();

                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook workbook = excelApp.Workbooks.Open(filePath);
                Microsoft.Office.Interop.Excel._Worksheet worksheet = workbook.Sheets[1];
                Microsoft.Office.Interop.Excel.Range range = worksheet.UsedRange;

                int rowCount = range.Rows.Count;

                for (int row = 2; row <= rowCount; row++)
                {
                    string department = (range.Cells[row, 1] as Microsoft.Office.Interop.Excel.Range).Text.ToString();
                    int planTarget = int.Parse((range.Cells[row, 2] as Microsoft.Office.Interop.Excel.Range).Text.ToString());
                    int output = int.Parse((range.Cells[row, 3] as Microsoft.Office.Interop.Excel.Range).Text.ToString());
                    int finalHC = int.Parse((range.Cells[row, 4] as Microsoft.Office.Interop.Excel.Range).Text.ToString());
                    decimal workHours = decimal.Parse((range.Cells[row, 5] as Microsoft.Office.Interop.Excel.Range).Text.ToString());
                    decimal ieTarget = decimal.Parse((range.Cells[row, 6] as Microsoft.Office.Interop.Excel.Range).Text.ToString());

                    decimal actualMH = Math.Round(finalHC * workHours, 2);
                    decimal standardMH = Math.Round(output / ieTarget, 2);
                    decimal ieRate = (actualMH > 0) ? Math.Floor((standardMH / actualMH) * 100) : 0;
                    decimal bonus = (ieRate >= 35) ? ieRate * 1.4m : 0;

                    dt.Rows.Add(department, planTarget, output, finalHC, workHours, ieTarget, actualMH, standardMH, ieRate, bonus);

                }

                workbook.Close(false);
                excelApp.Quit();

                // Release COM objects
                System.Runtime.InteropServices.Marshal.ReleaseComObject(range);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

                dataGridView1.DataSource = dt;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void TableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
