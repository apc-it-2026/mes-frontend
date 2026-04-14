using System;
using System.Windows.Forms;
using SJeMES_Framework.Class;

namespace WorkingHoursQuery
{
    public class Interface
    {
        public static void RunApp(object obj)
        {
            try
            {
                Program.client = obj as ClientClass;
                WorkingHoursQuery frm = new WorkingHoursQuery();
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