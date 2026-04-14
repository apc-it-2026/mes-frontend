using System;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
using SJeMES_Framework.Class;
using SJeMES_Framework.Common;
using SJeMES_Framework.WebAPI;

namespace CommanClassLib
{
    internal delegate void JudgeDelegate(DataTable dataTable);

    internal abstract class InOutPutBaseModel
    {
        public readonly ClientClass Client;
        public string ErrorMessage { get; set; }
        public readonly List<Dictionary<string, string>> listDictionary;
        protected readonly JudgeDelegate judgeDelegate;


        internal InOutPutBaseModel(ClientClass Client, List<Dictionary<string, string>> listDictionary, JudgeDelegate judgeDelegate)
        {
            this.Client = Client;
            this.listDictionary = listDictionary;
            this.judgeDelegate = judgeDelegate;
        }

        internal string GetJson(string functionName, string data)
        {
            string resulteValue = "";
            foreach (Dictionary<string, string> dictionary in listDictionary) //Passed in from the logic layer constructor
            {
                if (dictionary["FunctionName"] == functionName)
                {
                    string ret = WebAPIHelper.Post(Client.APIURL, dictionary["dllName"], dictionary["className"], dictionary["method"], Client.UserToken, data);
                    if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                    {
                        resulteValue = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    }
                    else
                    {
                        ErrorMessage = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString();
                    }
                }
            }

            return resulteValue;
        }

        internal DataTable GetDatatable(string functionName, string data)
        {
            string json = GetJson(functionName, data);
            if (string.IsNullOrEmpty(json) || string.IsNullOrWhiteSpace(json))
            {
                return null;
            }
            else
            {
                return JsonHelper.GetDataTableByJson(json);
            }
        }

        internal bool UpdateDatatable(string functionName, string data)
        {
            ErrorMessage = string.Empty;
            GetJson(functionName, data);
            if (string.IsNullOrEmpty(ErrorMessage) || string.IsNullOrWhiteSpace(ErrorMessage))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal bool ParseQrCode(int length)
        {
            if (length == 11)
            {
                return true;
            }
            else
            {
                ErrorMessage = UIHelper.UImsg("QR code length is wrong，Please contact your system administrator！", Client, "", Client.Language);
                return false;
            }
        }

        internal bool BeforeInputJudge(DataTable dataTable) //The judgment after scanning the QR code is not a standard process, use the delegate to call the judgment method of the subclass
        {
            bool returnValue = true;
            ErrorMessage = "";
            judgeDelegate?.Invoke(dataTable); //You can use multicast delegation, the return value cannot be judged, and multicast only takes the final judgment

            if (ErrorMessage != "")
            {
                returnValue = false;
            }

            return returnValue;
        }
    }
}