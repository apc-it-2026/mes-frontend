using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SJeMES_Framework.WinForm.CommonControls
{
    public class MenuItemV3
    {
        public string MenuName;
        public string MenuInfo;
        public int MenuSeq;
        public string MenuAction;
        public string MenuUrl;
        public string MenuDll;
        public string MenuClass;
        public string MenuMethod;
        public string MenuModule;
        public string MenuImg;
        public MenuV3 Control;

        public static Dictionary<int,MenuItemV3> GetMI3(System.Data.DataTable dt)
        {
            Dictionary<int, MenuItemV3> ret = new Dictionary<int, MenuItemV3>();
            int seq = 1;
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                MenuItemV3 mi3 = new MenuItemV3();
                mi3.MenuName = dr["menu_name"].ToString();
                mi3.MenuInfo = dr["menu_info"].ToString();
                mi3.MenuSeq =Convert.ToInt32( dr["menu_seq"].ToString());
                mi3.MenuAction = dr["menu_action"].ToString();
                mi3.MenuUrl = dr["menu_url"].ToString();
                mi3.MenuDll = dr["menu_dll"].ToString();
                mi3.MenuClass = dr["menu_class"].ToString();
                mi3.MenuMethod = dr["menu_method"].ToString();
                mi3.MenuModule = dr["menu_module"].ToString();
                mi3.MenuImg = dr["menu_img"].ToString();

                ret.Add(seq, mi3);

                seq++;
            }

            return ret;
        }
    }
}
