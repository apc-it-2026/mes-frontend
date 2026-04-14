using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SJeMES_Framework.WinForm.CommonControls
{
    public class MenuItemV2
    {
        public string MenuName;
        public string MenuInfo;
        public int MenuSeq;
        public Dictionary<int, MenuItemV3> MenuItemsV3s;
        public MenuV2 Control;
        

        public static Dictionary<int,MenuItemV2> GetMI2(System.Data.DataTable dt)
        {
            Dictionary<int, MenuItemV2> ret = new Dictionary<int,MenuItemV2>();

            int seq = 1;

            foreach (System.Data.DataRow dr in dt.Rows)
            {
                MenuItemV2 mi2 = new MenuItemV2();
                mi2.MenuName = dr["menu_name"].ToString();
                mi2.MenuInfo = dr["menu_info"].ToString();
                mi2.MenuSeq =Convert.ToInt32( dr["menu_seq"].ToString());
                

                ret.Add(seq, mi2);

                seq++;
            }

            return ret;
        }
    }
}
