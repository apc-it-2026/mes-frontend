using SJeMES_Framework.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RR_TEAMS_UPLOAD
{
    internal class Interface
    {
        public static void RunApp(object obj)
        {
            try
            {
                Program.client = obj as ClientClass;
                Form1 frm = new Form1();
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
