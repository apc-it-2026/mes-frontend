using System;
using System.Windows.Forms;
using SJeMES_Framework.Class;

namespace StitchingOutput2
{
    public class Interface
    {
        public static void RunApp(object obj)
        {
            try
            {
                Program.client = obj as ClientClass;
                StitchingOutput2 frm = new StitchingOutput2();
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