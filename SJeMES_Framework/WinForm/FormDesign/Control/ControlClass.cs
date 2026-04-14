using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SJeMES_Framework.WinForm.FormDesign.Control
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
                case "Button":
                    ((ButtonClass)this).SetStatus(Status);
                    break;
                case "TextBox":
                    ((TextBoxClass)this).SetStatus(Status);
                    break;
                case "ToolsBar":
                    ((ToolsBarClass)this).SetStatus(Status);
                    break;
                case "DataView":
                    ((DataViewClass)this).SetStatus(Status);
                    break;
            }

        }
    }
}
