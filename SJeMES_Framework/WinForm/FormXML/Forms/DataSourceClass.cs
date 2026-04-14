using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SJeMES_Framework.WinForm.FormXML.Forms
{
    public class DataSourceClass
    {
        
        public int TableCount;
        public Dictionary<string, TableClass> Tables;

        public DataSourceClass(string XML)
        {
            
            TableCount = Convert.ToInt32(Common.StringHelper.GetDataFromFirstTag(XML, "<TableCount>", "</TableCount>"));
            Tables = new Dictionary<string, TableClass>();

            for(int i=1;i<=TableCount;i++)
            {
                Tables.Add("Table" + i, new TableClass(Common.StringHelper.GetDataFromFirstTag(XML, "<Table" + i + ">", "</Table" + i + ">")));
            }
        }
    }
}
