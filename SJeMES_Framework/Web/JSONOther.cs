using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJeMES_Framework.Web
{
    public class JSONOther
    {
        public string label = string.Empty;
        public string sql = string.Empty;
  

        public JSONOther(string Label,string OtherSql)
        {
            this.sql = OtherSql;
            this.label = Label;
        }

        public JSONOther()
        {

        }
    }
}
