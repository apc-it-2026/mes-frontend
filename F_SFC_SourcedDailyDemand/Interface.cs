using System;
using System.Windows.Forms;
using SJeMES_Framework.Class;

namespace F_SFC_SourcedDailyDemand
{
    public class Interface
    {
        public static void RunApp(object obj)
        {
            try
            {
                Program.client = obj as ClientClass;
                F_SFC_SourcedDailyDemand frm = new F_SFC_SourcedDailyDemand();
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