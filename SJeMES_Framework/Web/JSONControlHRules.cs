using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJeMES_Framework.Web
{
    public class JSONControlHRules
    {
        public string type = string.Empty;
        public string msg = string.Empty;

        public JSONControlHRules(string type,string msg)
        {
            this.type = type;
            this.msg = msg;
        }

        public JSONControlHRules() { }
    }
}
