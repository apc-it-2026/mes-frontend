using System;
using System.Windows.Forms;
using SJeMES_Framework.Class;

namespace CuttingAllocation
{
    public class Interface
    {
        public static void RunApp(object obj)
        {
            try
            {
                Program.client = obj as ClientClass;
                CuttingAllocation frm = new CuttingAllocation();
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