using StargateAPI.Business.Dtos;

namespace StargateAPI.Business.Results
{
    public class GetPersonByNameResult : BaseResponse
    {
        public PersonAstronaut? Person { get; set; }
    }
}