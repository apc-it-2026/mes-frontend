using Newtonsoft.Json;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkingIEAchievementInput
{
    internal class ErrHelper
    {
        public string text(string date)
        {
            string txt = $@"--查组别是否有标准工时
select distinct A.WORK_DAY,A.SCAN_DETPT,B.WORK_HOURS from(
--查有无工艺路线TXT是否为空
             select max(a.production_order),to_date(to_char(max(SCAN_DATE),'yyyy/mm/dd'),'yyyy/mm/dd') as work_day, ORG_ID,
            a.ART_NO,a.PROCESS_NO,a.SCAN_DETPT,nvl(max(b.UDF01),0) as THT,sum(a.label_qty) qty,trunc(max(b.UDF01)*sum(a.label_qty)/3600,2) AS get_hour,
            (select sum(ONLINETIME) from SFC_ONLINEANDOFFLINE_M where STARTTIME between to_date('{date}','yyyy/mm/dd') and to_date('{date}','yyyy/mm/dd')+1 and DEPTNO=a.SCAN_DETPT) as sum_fact_hour
            from sfc_trackout_list a left join MES010A1 b on a.production_order=b.production_order  and b.PROCEDURE_NO=a.PROCESS_NO where
        
            SCAN_DATE between to_date('{date}','yyyy/mm/dd') and to_date('{date}','yyyy/mm/dd')+1 and ART_NO is not null 
             AND a.PROCESS_NO IN ( SELECT ROUT_NO FROM BASE24M)
             group by ART_NO,PROCESS_NO,SCAN_DETPT,ORG_ID 
 -------  end 1        
               ) A left join mes_workinghours_01 B on A.WORK_DAY=B.WORK_DAY and A.SCAN_DETPT=B.D_DEPT where SUM_FACT_HOUR is null 
 -------end 2
             ";
            return txt;
        }
        public bool IsMangager()
        {
            Dictionary<string, object> parm = new Dictionary<string, object>();
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "IsMangager", Program.client.UserToken, JsonConvert.SerializeObject(parm));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                bool isMangager = JsonConvert.DeserializeObject<bool>(json);
                return isMangager;
            }
            else
            {
                MessageBox.Show(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                return false;
            }
        }
    }
}
