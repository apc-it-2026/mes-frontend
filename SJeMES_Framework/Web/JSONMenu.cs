using System;
using System.Collections.Generic;
using System.Text;

namespace SJeMES_Framework.Web
{
    public class JSONMenu
    {
        //public string path = string.Empty;
        //public string component = string.Empty;
        //public string redirect = string.Empty;
        //public bool hidden = false;
        //public string name = string.Empty;
        //public JSONMenuMeta meta = new JSONMenuMeta();
        //public string query = string.Empty;
        //public List<JSONMenu> children = new List<JSONMenu>();
        //public string action = string.Empty;
        //public string dllname = string.Empty;
        //public string classname = string.Empty;
        //public string method = string.Empty;
        //public string url = string.Empty;
        //public string module = string.Empty;


        public string menu_parent = string.Empty;
        public string menu_name = string.Empty;
        public string menu_info = string.Empty;
        public int menu_seq = 0;
        public string menu_action = string.Empty;
        public string menu_url = string.Empty;
        public string menu_dll = string.Empty;
        public string menu_class = string.Empty;
        public string menu_method = string.Empty;
        public string menu_module = string.Empty;
        public string ui_en = string.Empty;
        public string ui_yn = string.Empty;
        public Dictionary<string,JSONMenu> children = new Dictionary<string, JSONMenu>();

        public JSONMenu()
        { }
    }
}
