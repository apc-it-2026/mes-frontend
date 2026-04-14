using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJeMES_Framework.Web
{
    public class JSONControlHItem
    {
        public string label = string.Empty;//显示的表单名称，不能为空
        public string className = string.Empty; // 当前项的样式名
      


        public JSONControlHItem(string label)
        {
            this.label = label;
            className = "ModuleClass";
        }

        public JSONControlHItem()
        {

        }
    }
}
