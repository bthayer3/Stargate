using MediatR;
using Microsoft.EntityFrameworkCore;
using StargateAPI.Business.Commands;
using StargateAPI.Business.Data;
using StargateAPI.Business.Results;

namespace StargateAPI.Business.Handlers
{
    public class UpdatePersonHandler : IRequestHandler<UpdatePerson, UpdatePersonResult>
    {
        private readonly StargateContext _context;

        public UpdatePersonHandler(StargateContext context)
        {
            _context = context;
        }
        public async Task<UpdatePersonResult> Handle(UpdatePerson request, CancellationToken cancellationToken)
        {
            var person = await _context.People.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
            person.Name = request.Name;

            _context.People.Update(person);

            await _context.SaveChangesAsync(cancellationToken);

            return new UpdatePersonResult()
            {
                Id = request.Id
            };
        }
    }
}