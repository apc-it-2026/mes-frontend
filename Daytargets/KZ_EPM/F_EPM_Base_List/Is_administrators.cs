using Newtonsoft.Json;
using SJeMES_Control_Library;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_EPM_Base_List
{
    internal class Is_administrators
    {
        public bool IsMangager()
        {
            Dictionary<string, object> parm = new Dictionary<string, object>();
            string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "IsMangager", Program.Client.UserToken, JsonConvert.SerializeObject(parm));
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
