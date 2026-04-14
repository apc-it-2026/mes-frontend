using System;
using System.Windows.Forms;
using SJeMES_Framework.Class;

namespace StitchingInput2
{
    public class Interface
    {
        public static void RunApp(object obj)
        {
            try
            {
                Program.client = obj as ClientClass;
                StitchingInput2 frm = new StitchingInput2();
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