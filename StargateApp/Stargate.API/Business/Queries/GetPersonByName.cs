using MediatR;
using Microsoft.EntityFrameworkCore;
using StargateAPI.Business.Data;
using StargateAPI.Business.Dtos;
using StargateAPI.Business.Enums;
using StargateAPI.Business.Results;

namespace StargateAPI.Business.Queries
{
    public class GetPersonByName : IRequest<GetPersonByNameResult>
    {
        public required string Name { get; set; } = string.Empty;
    }

    public class GetPersonByNameHandler : IRequestHandler<GetPersonByName, GetPersonByNameResult>
    {
        private readonly StargateContext _context;

        public GetPersonByNameHandler(StargateContext context)
        {
            _context = context;
        }

        public async Task<GetPersonByNameResult> Handle(GetPersonByName request, CancellationToken cancellationToken)
        {
            var result = new GetPersonByNameResult();

            // Removed the hard coded SQl string, risk of SQL injection
            //var query = $"SELECT a.Id as PersonId, a.Name, b.CurrentRank, b.CurrentDutyTitle, b.CareerStartDate, b.CareerEndDate FROM [Person] a LEFT JOIN [AstronautDetail] b on b.PersonId = a.Id WHERE '{request.Name}' = a.Name";

            var people = await _context.People
                .Where(p => p.Name.ToLower() == request.Name.ToLower()) // Filter by Name
                .GroupJoin(
                    _context.AstronautDetails,  // Left join with AstronautDetail
                    p => p.Id,
                    a => a.PersonId,
                    (p, astronautGroup) => new PersonAstronaut
                    {
                        PersonId = p.Id,
                        Name = p.Name,
                        CurrentRank = astronautGroup.FirstOrDefault() != null ? astronautGroup.FirstOrDefault().CurrentRank.GetPrettyDescription() : "",
                        CurrentDutyTitle = astronautGroup.FirstOrDefault() != null ? astronautGroup.FirstOrDefault().CurrentDutyTitle.GetPrettyDescription() : "",
                        CareerStartDate = astronautGroup.FirstOrDefault() != null ? astronautGroup.FirstOrDefault().CareerStartDate : null,
                        CareerEndDate = astronautGroup.FirstOrDefault() != null ? astronautGroup.FirstOrDefault().CareerEndDate : null,
                    })
                .ToListAsync(cancellationToken);

            result.Person = people.FirstOrDefault();

            return result;
        }
    }
}