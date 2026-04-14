using F_Safety_System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_Safety_System
{
    public class Interface
    {

		public static void RunApp(Object obj)
		{
            try
            {
                Program.Client = obj as SJeMES_Framework.Class.ClientClass;
                Safety_System frm = new Safety_System();
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
