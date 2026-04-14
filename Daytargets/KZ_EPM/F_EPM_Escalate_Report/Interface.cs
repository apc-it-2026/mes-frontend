using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_EPM_Escalate_Report
{
    public class Interface
    {

		public static void RunApp(Object obj)
		{
            try
            {
                Program.Client = obj as SJeMES_Framework.Class.ClientClass;
                Escalate_Report frm = new Escalate_Report();
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
