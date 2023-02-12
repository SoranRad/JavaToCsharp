using CMSApplication.Enums;
using System.Data;

namespace CMSApplication.Models
{
    public class ResponseModel
    {
        public ResponseModel(ResponseCode responseCode,string responseMessage,object dataSet)
        {
            ResponseCode = responseCode;
            ResponseMessage = responseMessage;
            DataSet = dataSet;


        }

        public ResponseCode ResponseCode { get; private set; }
        public string ResponseMessage { get; private set; }
        public object DataSet { get; private set; }
    }
}
