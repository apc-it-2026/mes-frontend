using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SJeMES_Framework.WinForm.FormXML.Forms
{
    public class FormClass
    {
        public string DesignXML;
        public string FormCode;
        public string FormName;
        public string FormInfo;
        public string FormTitle;
        public int FormWidth;
        public int FormHight;
        public string FormType;
        public string FormReportCode;
        public int PanelsCount;
        public Class.OrgClass Org;

        public Dictionary<string, FormPanel.FormPanelClass> FormPanels;

        public DataSourceClass DataSource;


        public FormClass(string XML,string WebServiceUrl,Class.OrgClass Org)
        {
            try
            {
                this.DesignXML = XML;
                this.Org = Org;
                FormCode = Common.StringHelper.GetDataFromFirstTag(XML, "<FormCode>", "</FormCode");
                FormName = Common.StringHelper.GetDataFromFirstTag(XML, "<FormName>", "</FormName");
                FormInfo = Common.StringHelper.GetDataFromFirstTag(XML, "<FormTitle>", "</FormInfo");
                FormTitle = Common.StringHelper.GetDataFromFirstTag(XML, "<FormTitle>", "</FormTitle");
                FormWidth = Convert.ToInt32(Common.StringHelper.GetDataFromFirstTag(XML, "<FormWidth>", "</FormWidth>"));
                FormHight = Convert.ToInt32(Common.StringHelper.GetDataFromFirstTag(XML, "<FormHight>", "</FormHight>"));
                FormType = Common.StringHelper.GetDataFromFirstTag(XML, "<FormType>", "</FormType>");
                PanelsCount = Convert.ToInt32(Common.StringHelper.GetDataFromFirstTag(XML, "<PanelsCount>", "</PanelsCount>"));

                if(XML.Contains("<FormReportCode>"))
                    FormReportCode = Common.StringHelper.GetDataFromFirstTag(XML, "<FormReportCode>", "</FormReportCode");


                string xmlFormPanels = Common.StringHelper.GetDataFromFirstTag(XML, "<FormPanels>", "</FormPanels");

                FormPanels = new Dictionary<string, FormPanel.FormPanelClass>();
                for(int i=1;i<=PanelsCount;i++)
                {
                    string xmlPanel = Common.StringHelper.GetDataFromFirstTag(xmlFormPanels, "<Panel" + i + ">", "</Panel" + i + ">");
                    string PanelType = Common.StringHelper.GetDataFromFirstTag(xmlPanel, "<PanelType>", "</PanelType>");

                    switch (PanelType)
                    {
                        case "HeadPanel":
                            FormPanels.Add("Panel" + i, new FormPanel.HeadPanelClass(xmlPanel, WebServiceUrl, Org));
                            break;
                        case "BodyPanel":
                            FormPanels.Add("Panel" + i, new FormPanel.BodyPanelClass(xmlPanel, WebServiceUrl,Org));
                            break;
                    }

                    
                }

                string xmlDataSource = Common.StringHelper.GetDataFromFirstTag(XML, "<DataSource>", "</DataSource>");

                DataSource = new Forms.DataSourceClass(xmlDataSource);
            }
            catch { }
        } 
      
    }
}
