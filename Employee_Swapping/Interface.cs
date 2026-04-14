using System;
using System.Windows.Forms;
using SJeMES_Framework.Class; 

namespace Employee_Swapping
{
    public class Interface
    {
        public static void RunApp(object obj)
        {
            try
            {
                Program.client = obj as ClientClass;
                Employee_Swapping frm = new Employee_Swapping();
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