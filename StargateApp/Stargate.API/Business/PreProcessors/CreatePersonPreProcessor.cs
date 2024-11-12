using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using StargateAPI.Business.Commands;
using StargateAPI.Business.Data;

namespace StargateAPI.Business.PreProcessors
{
    public class CreatePersonPreProcessor : IRequestPreProcessor<CreatePerson>
    {
        private readonly StargateContext _context;

        public CreatePersonPreProcessor(StargateContext context)
        {
            _context = context;
        }

        //Converted to async
        public async Task Process(CreatePerson request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Name)) throw new BadHttpRequestException("Name is required to create a person.");

            //Wanted to use string ordinal ignore casing but was not working
            var person = await _context.People.AsNoTracking().FirstOrDefaultAsync(z => z.Name.ToLower() == request.Name.ToLower(), cancellationToken);

            if (person is not null) throw new BadHttpRequestException($"Person by the name '{request.Name}' already exists.");
        }
    }
}