using System.Net;

namespace StargateAPI.Business.Results
{
    public class BaseResponse   //Moved to Dtos from Controllers folder
    {
        public bool Success { get; set; } = true;

        public string Message { get; set; } = "Successful";

        public int ResponseCode { get; set; } = (int)HttpStatusCode.OK;
    }
}