using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_EPM_Index_Summary
{
	public class Interface
	{
		public static void RunApp(Object obj)
		{
			try
			{ 
				Program.Client = obj as SJeMES_Framework.Class.ClientClass;
				Index_SummaryMainForm frm = new Index_SummaryMainForm();
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
