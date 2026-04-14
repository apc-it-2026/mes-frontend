using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StickBottomInputByOrder2
{
	public class Interface
	{
		public static void RunApp(Object obj)
		{
			try
			{
				Program.Client = obj as SJeMES_Framework.Class.ClientClass;
				StickBottomInputByOrder2 frm = new StickBottomInputByOrder2();
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
