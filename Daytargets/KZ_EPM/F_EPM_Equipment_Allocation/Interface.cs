using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_EPM_Equipment_Allocation
{
    class Interface
    {
		public static void RunApp(Object obj)
		{
			try
			{
				Program.Client = obj as SJeMES_Framework.Class.ClientClass;
				Equipment_AllocationMainForm frm = new Equipment_AllocationMainForm();
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
