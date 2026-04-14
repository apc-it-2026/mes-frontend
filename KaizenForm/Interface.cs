using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KaizenForm
{
    class Interface
    {
        public static void RunApp(Object obj)
        {
            try
            {
                string interfaceName = "KaizenForm";
                Program.client = obj as SJeMES_Framework.Class.ClientClass;
                string FormName = Program.client.FormName;
                FormCollection collection;
                collection = Application.OpenForms;

                Form frm = null;
                switch (FormName)
                {
                    case "Kaizen_Form":
                        frm = new KaizenForm();
                        break;
                    case "Kaizen_Reports":
                        frm = new Kaizan_reports();
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
        }
    }
}
