using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SJeMES_Framework.WinForm.FormDesign.Control
{
    public class ButtonClass:ControlClass
    {
        public bool Enable;
        public string ButtonType;
        public string Dock;


        public ButtonClass(string XML)
        {
            try
            {
                Enable = Convert.ToBoolean(Common.StringHelper.GetDataFromFirstTag(XML, "<Enable>", "</Enable>").Trim());
                ButtonType = Common.StringHelper.GetDataFromFirstTag(XML, "<ButtonType>", "</ButtonType").Trim();
                ControlType = Common.StringHelper.GetDataFromFirstTag(XML, "<ControlType>", "</ControlType").Trim();
                Dock = Common.StringHelper.GetDataFromFirstTag(XML, "<Dock>", "</Dock>").Trim();
            }
            catch { }
        }

        public Button GetControls(string ParentName,Forms.FormClass FC)
        {
            this.ParentName = ParentName;
            this.FC = FC;
            Button buttonRet = new Button();
            try
            {
                this.Control = buttonRet;
                this.ControlName = buttonRet.Name = ParentName + "_Button" + this.ButtonType;
                switch (this.ButtonType)
                {
                    case "Add":
                        buttonRet.Text = "新增";
                        break;
                    case "Edit":
                        buttonRet.Text = "修改";
                        break;
                    case "Del":
                        buttonRet.Text = "删除";
                        break;
                    case "Search":
                        buttonRet.Text = "查询";
                        break;
                    case "More":
                        buttonRet.Text = "更多功能";
                        break;
                    case "Print":
                        buttonRet.Text = "打印";
                        break;
                    case "DoWork":
                        buttonRet.Text = "操作";
                        break;
                    case "Save":
                        buttonRet.Text = "保存";
                        break;
                    case "Cancel":
                        buttonRet.Text = "取消";
                        break;
                    case "Exit":
                        buttonRet.Text = "退出";
                        break;
                }

               
                switch(this.Dock)
                {
                    case "Left":
                        buttonRet.Dock = DockStyle.Left;
                        break;
                    case "Right":
                        buttonRet.Dock = DockStyle.Right;
                        break;
                    case "None":
                        buttonRet.Dock = DockStyle.None;
                        break;
                    case "Fill":
                        buttonRet.Dock = DockStyle.Fill;
                        break;
                    case "Top":
                        buttonRet.Dock = DockStyle.Top;
                        break;
                    case "Bottom":
                        buttonRet.Dock = DockStyle.Bottom;
                        break;
                    default:
                        buttonRet.Dock = DockStyle.None;
                        break;
                }
                buttonRet.Width = 80;
                buttonRet.Height = 20;
                buttonRet.Font = new System.Drawing.Font("微软雅黑", 9);
                buttonRet.ForeColor = System.Drawing.Color.Black;
                buttonRet.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            }
            catch
            {

            }

            return buttonRet;
        }

        public void SetStatus(string Status)
        {
            this.Status = Status;
            if (this.Control != null)
            {
                switch (Status)
                {
                    case "Normal":
                        
                            if (ButtonType == "Add" || ButtonType == "Edit" || ButtonType == "Del" || ButtonType == "Search" || ButtonType == "More")
                            {
                                this.Control.Visible = true;
                            }


                        if (ButtonType == "Save" || ButtonType == "Cancel")
                        {
                            this.Control.Visible = false;
                        }
                        break;
                    case "Add":
                       
                            if (ButtonType == "Add" || ButtonType == "Edit" || ButtonType == "Del" || ButtonType == "Search" || ButtonType == "More")
                            {
                                this.Control.Visible = false;
                            }

                            if (ButtonType == "Save" || ButtonType == "Cancel")
                            {
                                this.Control.Visible = true;
                            }
                        
                        break;
                    case "Edit":
                        
                            if (ButtonType == "Add" || ButtonType == "Edit" || ButtonType == "Del" || ButtonType == "Search" || ButtonType == "More")
                            {
                                this.Control.Visible = false;
                            }

                            if (ButtonType == "Save" || ButtonType == "Cancel")
                            {
                                this.Control.Visible = true;
                            }
                        
                        break;

                }
            }
        }
    }
}
