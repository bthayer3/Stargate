namespace StargateAPI.Business.Results
{
    public class CreateAstronautDutyResult : BaseResponse
    {
        public int Id { get; set; }  //Why was this nullable? Changed this to be not nullable
    }
}