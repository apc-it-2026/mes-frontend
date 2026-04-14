using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_TMS_TierMeeting2_Main
{
	public class Interface
	{
		public static void RunApp(Object obj)
		{
			try
			{ 
				Program.client = obj as SJeMES_Framework.Class.ClientClass;
				IntroForm frm = new IntroForm();
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
