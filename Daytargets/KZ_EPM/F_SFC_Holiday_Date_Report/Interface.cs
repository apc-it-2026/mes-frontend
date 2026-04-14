using System;
using System.Windows.Forms;
using F_SFC_Holiday_Date_Report;
using SJeMES_Framework.Class;

namespace F_SFC_Holiday_Date_Report
{
    public class Interface
    {
        public static void RunApp(object obj)
        {
            try
            {
                Program.client = obj as ClientClass;
                Holiday_DateForm frm = new Holiday_DateForm();
                FormCollection collection = Application.OpenForms;
                frm.Owner = collection["frmMain"];
                frm.Show();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}