using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionOutputOrderQuery
{
	public class Interface
	{
		public static void RunApp(Object obj)
		{
			try
			{
				Program.client = obj as SJeMES_Framework.Class.ClientClass;
				ProductionOutputOrderQuery frm = new ProductionOutputOrderQuery();
				frm.TopMost = true;
				frm.Show();
			}
			catch (Exception ex)
			{
				//SJeMES_Control_Library.MessageHelper.ShowErr(, ex.Message);
				throw ex;
			}
		}
	}
}
