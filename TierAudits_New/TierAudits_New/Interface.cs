using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TierAudits_New
{
    public class Interface
    {

        public static void RunApp(Object obj)
        {
            try
            {
                Program.Client = obj as SJeMES_Framework.Class.ClientClass;
                Tier_visual frm = new Tier_visual();
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
