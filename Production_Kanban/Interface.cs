using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Production_Kanban
{
	public class Interface
	{
		public static string line;
		public static string date;
		public static void RunApp(Object obj)
		{
			try
			{
				Program.Client = obj as SJeMES_Framework.Class.ClientClass;
				//date = null;
				//line = null;
				Production_KanbanForm frm = new Production_KanbanForm();
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

		public static void RunCustomize(Object obj, Object arg1,Object arg2)
		{
			try
			{
				Program.Client = obj as SJeMES_Framework.Class.ClientClass;
				line = arg1 as string;
				date = arg2 as string;
                //Production_KanbanForm frm = new Production_KanbanForm();
                //if (Screen.AllScreens.Count() != 1)
                //{
                //    for (int i = 0; i < Screen.AllScreens.Count(); i++)
                //    {
                //        Screen s = Screen.AllScreens[i];
                //        if (!s.Primary)
                //        {
                //            frm.Location = new System.Drawing.Point(s.Bounds.X, s.Bounds.Y);
                //            frm.Size = new System.Drawing.Size(Screen.AllScreens[i].Bounds.Width, Screen.AllScreens[i].Bounds.Height);
                //        }
                //    }
                //}
                //frm.StartPosition = FormStartPosition.CenterScreen;
                //frm.ShowDialog();
                //frm.TopMost = true;
                Production_KanbanForm frm = new Production_KanbanForm();
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

		public static void RunMyApp(Object obj)
		{
			try
			{
				Program.Client = obj as SJeMES_Framework.Class.ClientClass;
				date = DateTime.Now.ToString("yyyy/MM/dd");
				line = null;
				Production_KanbanForm frm = new Production_KanbanForm();
				if (Screen.AllScreens.Count() != 1)
				{
					for (int i = 0; i < Screen.AllScreens.Count(); i++)
					{
						Screen s = Screen.AllScreens[i];
						if (!s.Primary)
						{
							frm.Location = new System.Drawing.Point(s.Bounds.X, s.Bounds.Y);
							frm.Size = new System.Drawing.Size(Screen.AllScreens[i].Bounds.Width, Screen.AllScreens[i].Bounds.Height);
						}
					}
				}
				frm.StartPosition = FormStartPosition.CenterScreen;
				frm.SendToBack();
				frm.Show();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
