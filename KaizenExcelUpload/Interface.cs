using KaizenExcelUpload;
using SJeMES_Framework.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KaizenExcelUpload
{
    class Interface
    {
        public static void RunApp(object obj)
        {
            try
            {
                Program.client = obj as ClientClass;
                KaizenUpload frm = new KaizenUpload();
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
