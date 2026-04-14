using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJeMES_Framework.Web
{
    public class JSONPanelClassB
    {
        public string Title;
        public int seq;
        public string table; //对应的数据表
        public List<string> tableKeys = new List<string>(); //主键
        public List<JSONControlB> tableHead; //返回前端表格控件的字段信息
        public List<string> HeadKeys = new List<string>(); //和表头关联的外键
        public bool disabled = false; // 表身是否能编辑

    
        public JSONPanelClassB()
        {

        }
    }
}
