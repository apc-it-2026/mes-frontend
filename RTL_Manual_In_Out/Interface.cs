using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RTL_Manual_In_Out
{
	public class Interface
	{
		public static void RunApp(Object obj)
		{
			try
			{
				Program.Client = obj as SJeMES_Framework.Class.ClientClass;
                RTL_Manual_In_Out frm = new RTL_Manual_In_Out();
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
