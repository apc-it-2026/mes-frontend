using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJeMES_Framework.Web
{
    public class JSONPanelClassHListItem
    {
        public string label = string.Empty;
        public string prop = string.Empty;
        public List<JSONEnum> enumData;
        public int width = 100;

        public JSONPanelClassHListItem(string label,string prop)
        {
            this.label = label;
            this.prop = prop;
            this.enumData = new List<Web.JSONEnum>();
        }

        public JSONPanelClassHListItem()
        {
            enumData = new List<JSONEnum>();
        }
    }
}
