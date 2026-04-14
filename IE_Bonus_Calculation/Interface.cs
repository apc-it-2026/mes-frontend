using SJeMES_Framework.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IE_Bonus_Calculation
{
    class Interface
    {
        public static void RunApp(Object obj)
        {
            try
            {
                Program.client = obj as SJeMES_Framework.Class.ClientClass;
                IE_Bonus_Calculator frm = new IE_Bonus_Calculator();
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
