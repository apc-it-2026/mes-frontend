using SJeMES_Framework.Class;
using System;
using System.Windows.Forms;

namespace AutoSendEmail
{
	public class Interface
	{
		public static void RunApp(Object obj)
		{
            try
            {
                Program.client = obj as ClientClass;
                AutoSendEmail frm = new AutoSendEmail();
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
