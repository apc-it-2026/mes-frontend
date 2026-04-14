using System;
using System.Windows.Forms;
using SJeMES_Framework.Class;

namespace F_MMS_InOut_BillAddOrder
{
	public class Interface
	{
		public static void RunApp(object obj)
		{
			try
			{
				Program.client = obj as ClientClass;
                InOut_BillOrder frm = new InOut_BillOrder();
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
