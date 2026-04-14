using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJeMES_Framework.Web
{
    public class JSONControlHItemControl
    {
        public string value = string.Empty;//初始值
        public string placeholder = string.Empty; // input输入框占位符，仅在type=text,password,datePicker,select时有效
        public List<JSONControlCheckBoxData> data;
        public bool IsAdd = true;
        public bool IsEdit = true;
        public bool disabled = false;
        public string format = string.Empty;//日期格式化 ymd ymdhms hms
        public string type = string.Empty;

        public JSONControlHItemControl(string value,string placeholder)
        {
            this.value = value;
            this.placeholder = placeholder;
            data = new List<SJeMES_Framework.Web.JSONControlCheckBoxData>();
        }

        public JSONControlHItemControl()
        {

        }
    }
}
