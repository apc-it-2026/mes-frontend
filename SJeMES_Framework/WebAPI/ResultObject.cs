using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SJeMES_Framework.WebAPI
{
    public class ResultObject
    {
        public bool IsSuccess;
        public string ErrMsg;
        public string RetData;
        public object RetData1;

        public ResultObject()
        {
            IsSuccess = true;
            ErrMsg = string.Empty;
            RetData = string.Empty;
            RetData1 = string.Empty;
        }
    }

    public class UploadFileResultDto
    {
        public string ErrMsg { get; set; }
        public bool IsSuccess { get; set; }
        public object ReturnObj { get; set; }
    }
}