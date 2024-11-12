using MediatR;
using Microsoft.EntityFrameworkCore;
using StargateAPI.Business.Data;
using StargateAPI.Business.Enums;
using StargateAPI.Business.Results;

namespace StargateAPI.Business.Queries
{
    public class GetAstronautDutiesByName : IRequest<GetAstronautDutiesByNameResult>
    {
        public string Name { get; set; } = string.Empty;
    }

    public class GetAstronautDutiesByNameHandler : IRequestHandler<GetAstronautDutiesByName, GetAstronautDutiesByNameResult>
    {
        private readonly StargateContext _context;

        private readonly GetPersonByNameHandler _personHandler;

        public GetAstronautDutiesByNameHandler(StargateContext context, GetPersonByNameHandler personHandler)
        {
            _context = context;
            _personHandler = personHandler;
        }

        public async Task<GetAstronautDutiesByNameResult> Handle(GetAstronautDutiesByName request, CancellationToken cancellationToken)
        {
            var result = new GetAstronautDutiesByNameResult();

            var person = new GetPersonByName
            {
                Name = request.Name
            };

            // Call the Handle method and get the result
            var personResult = await _personHandler.Handle(person, cancellationToken);

            result.Person = personResult.Person;

            //var query = $"SELECT * FROM [AstronautDuty] WHERE {personResult.Person.PersonId} = PersonId Order By DutyStartDate Desc";


            //Use LINQ below instead of SQL above, prevent SQL injection
            if (personResult?.Person != null)
            {
                var duties = await _context.AstronautDuties
                    .Where(ad => ad.PersonId == personResult.Person.PersonId)
                    .OrderByDescending(ad => ad.DutyStartDate)
                    .Select(duty => new AstronautDutyResult
                    {
                        Id = duty.Id,
                        Rank = duty.Rank.GetPrettyDescription(),
                        DutyTitle = duty.DutyTitle.GetPrettyDescription(),
                        DutyStartDate = duty.DutyStartDate,
                        DutyEndDate = duty.DutyEndDate
                    })
                    .ToListAsync(cancellationToken);

                result.AstronautDuties = duties;
            }
            else
            {
                result.AstronautDuties = new List<AstronautDutyResult>();
            }

            return result;
        }
    }
}