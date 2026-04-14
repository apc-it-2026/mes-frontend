using System;
using System.Windows.Forms;
using SJeMES_Framework.Class;

namespace AutoGetDataCountSendMail
{
    public class Interface
    {
        public static void RunApp(object obj)
        {
            try
            {
                Program.client = obj as ClientClass;
                AutoGetDataCountSendMailForm frm = new AutoGetDataCountSendMailForm();
                FormCollection collection = Application.OpenForms;
                frm.Owner = collection["frmMain"];
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