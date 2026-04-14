using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SJeMES_Framework.WinForm.FormDesign.FormPanel
{
    public class FormPanelClass
    {
        public string Title;
        public string PanelType;
        public int Hight;
        public Control.ToolsBarClass ToolsBar;
        public int ChildrensCount;
        public Dictionary<string, Control.ControlClass> Childrens;
        public Forms.FormClass FC;
        public string ParentName;
        public System.Windows.Forms.Control Control;
        public string ControlName;
        public string Status;

        public string WebServiceUrl;


        public void SetStatus(string Status)
        {
            switch (PanelType)
            {
                case "HeadPanel":
                    ((HeadPanelClass)this).SetStatus(Status);
                    break;
                case "BodyPanel":
                    ((BodyPanelClass)this).SetStatus(Status);
                    break;
            }

        }
    }
}
