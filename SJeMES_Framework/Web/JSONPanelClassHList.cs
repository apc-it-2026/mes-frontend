using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJeMES_Framework.Web
{
    public class JSONPanelClassHList
    {
        public string tablename = string.Empty;
        public List<JSONPanelClassHListItem> tableHead;
        public JSONPanelClassHList()
        {
            tableHead = new List<SJeMES_Framework.Web.JSONPanelClassHListItem>();
        }
    }
}
