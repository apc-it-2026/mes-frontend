using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJeMES_Framework.Web
{
    public class JSONEnum
    {
        public string label = string.Empty;
        public string value = string.Empty;
  

        public JSONEnum(string Label,string Value)
        {
            this.value = Value;
            this.label = Label;
        }

        public JSONEnum()
        {

        }
    }
}
