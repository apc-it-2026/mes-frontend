using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJeMES_Framework.Web
{
    public class JSONControlH
    {

        public string type = string.Empty;// 支持类型text(文本输入框),password(密码输入框),radio(单选),checkbox(多tfp选框),datePicker(日期选择),seelct(下拉选择),txt(纯文本)，不能为空
        public string name = string.Empty;//控件名
        public JSONControlHItem Item;
        public JSONControlHItemControl control;
        public List<JSONControlHRules> rules;
        public JSONOther otherData;
        public List<JSONEnum> enumData;
        public bool systemFiled = false;
        public int width = 100;
        public string headId = "";
    
       


        public JSONControlH()
        {
            Item = new SJeMES_Framework.Web.JSONControlHItem(string.Empty);
            control = new SJeMES_Framework.Web.JSONControlHItemControl(string.Empty,string.Empty);
            rules = new List<SJeMES_Framework.Web.JSONControlHRules>();

            otherData = new Web.JSONOther();
            enumData = new List<Web.JSONEnum>();
        }
    }
}
