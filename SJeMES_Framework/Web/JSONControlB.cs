using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJeMES_Framework.Web
{
    public  class JSONControlB
    {
        public string label = string.Empty;// 表头显示的名称’,
        public string prop=string.Empty;//对应列表数据的key’,
        public string type=string.Empty;// 修改时显示的类型’,  //input 可选select,date
        public bool IsNull = true;
        public List<JSONControlBOption> options;// 下拉选择集合，仅在type=select时有效
        public bool IsAdd = true;
        public bool IsEdit = true;
        public bool disabled = false;
        public JSONOther otherData;
        public List<JSONEnum> enumData;
        public bool systemFiled = false;
        public string format = string.Empty;//日期格式化
        public int width = 200;
        public List<JSONControlHRules> rules;
        public string datatype = string.Empty;

        public JSONControlB(string label,string prop,string type,bool IsNull)
        {
            this.label = label;
            this.prop = prop;
            this.type = type;
            
            this.IsNull = IsNull;
            
            this.options = new List<SJeMES_Framework.Web.JSONControlBOption>();
            otherData = new Web.JSONOther();
            enumData = new List<Web.JSONEnum>();
            rules = new List<JSONControlHRules>();
        }

        public JSONControlB()
        {
            this.options = new List<SJeMES_Framework.Web.JSONControlBOption>();
            otherData = new Web.JSONOther();
            enumData = new List<Web.JSONEnum>();
            rules = new List<JSONControlHRules>();
        }

    }
}
