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
    public class PersonController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("")]
        [SwaggerOperation(Summary = "Get all people", Description = "Fetches a list of all people in the system.")]
        public async Task<IActionResult> GetPeople()
        {
            try
            {
                var result = await _mediator.Send(new GetPeople());

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

        [HttpGet("{name}")]
        [SwaggerOperation(Summary = "Get a person by name", Description = "Fetches the details of a person by their name.")]
        public async Task<IActionResult> GetPersonByName(string name)
        {
            try
            {
                var result = await _mediator.Send(new GetPersonByName()
                {
                    Name = name
                });

                return this.GetResponse(result);
            }
            catch (Exception ex)
            {
                return this.GetResponse(new BaseResponse
                {
                    Message = ex.Message,
                    Success = false,
                    ResponseCode = (int)HttpStatusCode.InternalServerError
                });
            }
        }

        [HttpPost("")]
        [SwaggerOperation(Summary = "Create a new person", Description = "Adds a new person to the system.")]
        public async Task<IActionResult> CreatePerson([FromBody] string name)
        {
            try
            {
                var result = await _mediator.Send(new CreatePerson()
                {
                    Name = name
                });

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

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update a person", Description = "Updates the name of a person by their ID.")]
        public async Task<IActionResult> UpdatePerson(int id, [FromBody] string newName)
        {
            try
            {
                var result = await _mediator.Send(new UpdatePerson
                {
                    Id = id,
                    Name = newName
                });

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