using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SJeMES_Framework.WinForm.FormXML.Control
{
    public class ControlClass
    {
        public string ControlType;
        public string ControlName;
        public System.Windows.Forms.Control Control;
        public string ParentName;
        public Forms.FormClass FC;
        public string Status;

        public void SetStatus(string Status)
        {
            this.Status = Status;
            switch (ControlType)
            {
               
                case "TextBox":
                    ((TextBoxClass)this).SetStatus(Status);
                    break;
               
                case "DataView":
                    ((DataViewClass)this).SetStatus(Status);
                    break;
            }

        }
    }
}
