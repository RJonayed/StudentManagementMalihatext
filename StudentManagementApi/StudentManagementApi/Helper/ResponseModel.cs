using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ubiety.Dns.Core.Common;

namespace StudentManagementApi.Helper
{
    public class ResponseModel
    {
        public ResponseModel(ResponseCodes responseCode, string responseMessage, object dataSet)
        {
            ResponseCode = responseCode;
            ResponseMessage = responseMessage;
            DateSet = dataSet;
        }
        public ResponseCodes ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public object DateSet { get; set; }
    }
}
