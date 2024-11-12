using MediatR;
using StargateAPI.Business.Results;

namespace StargateAPI.Business.Commands
{
    public class UpdatePerson : IRequest<UpdatePersonResult>
    {
        public int Id { get; set; }

        public required string Name { get; set; } = string.Empty;
    }
}