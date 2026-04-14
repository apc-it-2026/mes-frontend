using System;
using System.Windows.Forms;
using SJeMES_Framework.Class;

namespace WorkingCalendarReport
{
    public class Interface
    {
        public static void RunApp(object obj)
        {
            try
            {
                Program.client = obj as ClientClass;
                WorkingCalendarForm frm = new WorkingCalendarForm();
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