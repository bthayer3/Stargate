using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using StargateAPI.Business.Commands;
using StargateAPI.Business.Data;

namespace StargateAPI.Business.PreProcessors
{
    public class UpdatePersonPreProcessor : IRequestPreProcessor<UpdatePerson>
    {
        private readonly StargateContext _context;

        public UpdatePersonPreProcessor(StargateContext context)
        {
            _context = context;
        }

        public async Task Process(UpdatePerson request, CancellationToken cancellationToken)
        {
            var foundPerson = await _context.People.AsNoTracking().FirstOrDefaultAsync(z => z.Id == request.Id, cancellationToken);

            if (foundPerson is null) throw new BadHttpRequestException($"Person does not exist for Id {request.Id}.");

            if (foundPerson.Name == request.Name) throw new BadHttpRequestException($"Name already matches for Id {request.Id}.");   //Review, leaving in avoids another db update but not API friendly. No harm in updating name to be the same. Possibly skip db update and return 200

            var personWithRequestedName = await _context.People.Where(p => p.Name.ToLower() == request.Name.ToLower()).FirstOrDefaultAsync(cancellationToken);

            if (personWithRequestedName is not null && personWithRequestedName.Id != request.Id) throw new BadHttpRequestException($"Cannot update name to '{request.Name}' as this name is already in use.");
        }
    }
}