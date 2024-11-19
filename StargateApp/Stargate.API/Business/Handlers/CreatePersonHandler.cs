using MediatR;
using StargateAPI.Business.Commands;
using StargateAPI.Business.Data;
using StargateAPI.Business.Dtos;

namespace StargateAPI.Business.Handlers
{
    public class CreatePersonHandler : IRequestHandler<CreatePerson, Stargate.API.Business.Results.CreatePersonResult>
    {
        private readonly StargateContext _context;

        public CreatePersonHandler(StargateContext context)
        {
            _context = context;
        }
        public async Task<Stargate.API.Business.Results.CreatePersonResult> Handle(CreatePerson request, CancellationToken cancellationToken)
        {
            var newPerson = new Person()
            {
                Name = request.Name
            };

            await _context.People.AddAsync(newPerson, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);  //Fixed this, cancellationToken was not passed in

            return new Stargate.API.Business.Results.CreatePersonResult()
            {
                Id = newPerson.Id,
                ResponseCode = 201
            };
        }
    }
}