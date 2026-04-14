using System;
using System.Windows.Forms;
using SJeMES_Framework.Class;

namespace WorkingHoursRecord
{
    public class Interface
    {
        public static void RunApp(object obj)
        {
            try
            {
                Program.client = obj as ClientClass;
                WorkingHoursRecord frm = new WorkingHoursRecord();
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