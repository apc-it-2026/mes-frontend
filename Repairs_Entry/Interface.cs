using SJeMES_Framework.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KPIINPUT
{
    class Interface
    {
        public static void RunApp(Object obj)
        {
            try
            {
                string interfaceName = "KPIINPUT";
                Program.client = obj as SJeMES_Framework.Class.ClientClass;
                string FormName = Program.client.FormName;
                FormCollection collection;
                collection = Application.OpenForms;

                Form frm = new Form();
                switch (FormName)
                {
                    case "B GRADE REPORTS":
                        frm = new Repair();
                        break;
                    case "Haultings":
                        frm = new Haulting();
                        break;
                }
                var findFrm = collection[interfaceName + FormName];
                if (findFrm == null)
                {
                    frm.Owner = collection["frmMain"];
                    frm.Name = interfaceName + FormName;
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.Show();
                }
                else
                {
                    findFrm.Activate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            //try
            //{
            //    Program.client = obj as ClientClass;
            //    Repair frm = new Repair();
            //    FormCollection collection = Application.OpenForms;
            //    frm.Owner = collection["frmMain"];
            //    frm.Show();
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }
    }
}
