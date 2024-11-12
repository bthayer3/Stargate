using StargateAPI.Business.Dtos;

namespace StargateAPI.Business.Results
{
    public class GetPeopleResult : BaseResponse
    {
        public List<PersonAstronaut> People { get; set; } = [];
    }
}