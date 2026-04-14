using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJeMES_Framework.Web
{
    public class JSONControlBOption
    {
        public string label=string.Empty;
        public string value=string.Empty;

        public JSONControlBOption(string label,string value)
        {
            this.label = label;
            this.value = value;
        }

        public JSONControlBOption()
        {

        }
    }
}
