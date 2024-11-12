using MediatR;
using Microsoft.AspNetCore.Mvc;
using StargateAPI.Business.Commands;
using StargateAPI.Business.Queries;
using StargateAPI.Business.Results;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace StargateAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AstronautDutyController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AstronautDutyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{name}")]
        [SwaggerOperation(Summary = "Get astronaut duties by name", Description = "Fetches the duties for a specific astronaut by name.")]
        public async Task<IActionResult> GetAstronautDutiesByName(string name)  //Used to be GetAstronautDutyByName, changed this to be plural and return list of duties
        {
            try
            {
                var result = await _mediator.Send(new GetAstronautDutiesByName()
                {
                    Name = name
                });

                return this.GetResponse(result);
            }
            catch (Exception ex)
            {
                return this.GetResponse(new BaseResponse()
                {
                    Message = ex.Message,
                    Success = false,
                    ResponseCode = (int)HttpStatusCode.InternalServerError
                });
            }
        }

        [HttpPost("")]
        [SwaggerOperation(Summary = "Create a new astronaut duty", Description = "Creates a new astronaut duty record based on the provided details.")]
        public async Task<IActionResult> CreateAstronautDuty([FromBody] CreateAstronautDuty request)
        {
            //This was missing exception handling, added try catch

            try
            {
                var result = await _mediator.Send(request);
                return this.GetResponse(result);
            }
            catch (Exception ex)
            {
                var responseCode = ex is BadHttpRequestException
                    ? HttpStatusCode.BadRequest
                    : HttpStatusCode.InternalServerError;

                return this.GetResponse(new BaseResponse()
                {
                    Message = ex.Message,
                    Success = false,
                    ResponseCode = (int)responseCode
                });
            }
        }
    }
}