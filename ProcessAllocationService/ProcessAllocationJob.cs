using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Quartz;
using SJeMES_Framework.WebAPI;

namespace ProcessAllocationService
{
    class ProcessAllocationJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(ProcessAllocationEnableDate);
        }

        /// <summary>
        ///     Write back the date according to [Effective Date] of the transfer record sheet MES010A10, when this date is reached, write back Y to PRINT2 in the MES010M sheet
        /// </summary>
        private void ProcessAllocationEnableDate()
        {
            Dictionary<string, object> parm = new Dictionary<string, object>();

            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "ProcessAllocationEnableDate", Program.client.UserToken, JsonConvert.SerializeObject(parm));

            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                string json = retJson["RetData"].ToString();

                int count = int.Parse(json);

                LogHelper.Info("successfully updated" + count);
            }
            else
            {
                LogHelper.Error($"The error message is:{retJson["ErrMsg"]}");
            }
        }
    }
}