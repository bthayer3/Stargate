using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using StargateAPI.Business.Commands;
using StargateAPI.Business.Data;
using StargateAPI.Business.Enums;
using StargateAPI.Business.Queries;

namespace StargateAPI.Business.PreProcessors
{
    public class CreateAstronautDutyPreProcessor : IRequestPreProcessor<CreateAstronautDuty>
    {
        private readonly StargateContext _context;

        private readonly GetPersonByNameHandler _personHandler;

        public CreateAstronautDutyPreProcessor(StargateContext context, GetPersonByNameHandler personHandler)
        {
            _context = context;
            _personHandler = personHandler;
        }

        //Converted to be async
        public async Task Process(CreateAstronautDuty request, CancellationToken cancellationToken)
        {
            var personToLookup = new GetPersonByName
            {
                Name = request.Name
            };

            // Call the Handle method and get the result
            var personResult = await _personHandler.Handle(personToLookup, cancellationToken);

            if (personResult.Person is null) throw new BadHttpRequestException("Person does not exist.");

            if (Enum.TryParse(personResult.Person.CurrentDutyTitle, out DutyTitle currentDutyTitle) && currentDutyTitle == DutyTitle.Retired)
            {
                //If already retired, prevent adding more duties. Cannot come out of retirement
                throw new BadHttpRequestException($"Cannot create new astronaut duty for person because person is '{DutyTitle.Retired.GetPrettyDescription()}'.");
            }

            if (!Enum.TryParse(request.Rank, out Rank specifiedRank)) throw new BadHttpRequestException($"The value '{request.Rank}' is not a valid type for {nameof(CreateAstronautDuty.Rank)}. These are the valid types: {EnumExtensions.GetValuesAsString<Rank>()}.");

            if (!Enum.TryParse(request.DutyTitle, out DutyTitle specifiedDutyTitle)) throw new BadHttpRequestException($"The value '{request.DutyTitle}' is not a valid type for {nameof(CreateAstronautDuty.DutyTitle)}. These are the valid types: {EnumExtensions.GetValuesAsString<DutyTitle>()}.");

            var lastAstronautDuty = await _context.AstronautDuties
                .Where(ad => ad.PersonId == personResult.Person.PersonId)
                .OrderByDescending(ad => ad.DutyStartDate)
                .FirstOrDefaultAsync(cancellationToken);

            if (lastAstronautDuty != null)
            {
                if (request.DutyStartDate <= lastAstronautDuty.DutyStartDate)
                {
                    //Trying to insert a new astronaut duty before an old or current duty
                    throw new BadHttpRequestException($"New astronaut duty must be after {lastAstronautDuty.DutyStartDate:MM/dd/yyyy}.");
                }

                if (specifiedDutyTitle == lastAstronautDuty.DutyTitle)
                {
                    //Expecting a new duty title in order to insert new astronaut duty
                    throw new BadHttpRequestException($"New astronaut duty must be a different duty instead of '{specifiedDutyTitle.GetPrettyDescription()}'.");
                }
            }
        }
    }
}