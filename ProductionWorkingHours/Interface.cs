using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductionWorkingHours
{
	public class Interface
	{
		public static void RunApp(Object obj)
		{
			try
			{
				Program.Client = obj as SJeMES_Framework.Class.ClientClass;
				ProductionWorkingHours frm = new ProductionWorkingHours();
				FormCollection collection = Application.OpenForms;
				frm.Owner = collection["frmMain"];
				frm.StartPosition = FormStartPosition.CenterScreen;
				frm.Show();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
