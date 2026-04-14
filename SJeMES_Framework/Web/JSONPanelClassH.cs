using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJeMES_Framework.Web
{
    public class JSONPanelClassH
    {
        public string table = string.Empty;
        public List<string> tableKeys = new List<string>();
        public List<JSONControlH> formData;
        public bool isDocModule = false;
    

     
        public JSONPanelClassH()
        {

        }
    }
}
