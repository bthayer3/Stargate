using StargateAPI.Business.Dtos;

namespace StargateAPI.Business.Results
{
    public class GetAstronautDutiesByNameResult : BaseResponse
    {
        public PersonAstronaut Person { get; set; }

        public List<AstronautDutyResult> AstronautDuties { get; set; } = [];
    }
}