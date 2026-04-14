using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SJeMES_Framework.WinForm.CommonControls
{
    public class MenuItemV1
    {
        public string MenuName;
        public string MenuInfo;
        public int MenuSeq;
        public string MenuImg;
        public Dictionary<int, MenuItemV2> MenuItemV2s;
        public MenuV1 Control;
        public bool IsClicked = false;


        public static Dictionary<int, MenuItemV1> GetMI1(System.Data.DataTable dt)
        {
            Dictionary<int, MenuItemV1> ret = new Dictionary<int, MenuItemV1>();

            int seq = 1;

            foreach(System.Data.DataRow dr in dt.Rows)
            {
                MenuItemV1 mi1 = new MenuItemV1();
                mi1.MenuName = dr["menu_name"].ToString();
                mi1.MenuInfo = dr["menu_info"].ToString();
                mi1.MenuSeq =Convert.ToInt32( dr["menu_seq"].ToString());
                mi1.MenuImg = dr["menu_img"].ToString();

                ret.Add(seq, mi1);

                seq++;
            }

            return ret;
        }
    }
}
