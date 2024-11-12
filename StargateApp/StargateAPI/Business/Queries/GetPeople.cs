using MediatR;
using Microsoft.EntityFrameworkCore;
using StargateAPI.Business.Data;
using StargateAPI.Business.Dtos;
using StargateAPI.Business.Enums;
using StargateAPI.Business.Results;

namespace StargateAPI.Business.Queries
{
    public class GetPeople : IRequest<GetPeopleResult>
    {
    }

    public class GetPeopleHandler : IRequestHandler<GetPeople, GetPeopleResult>
    {
        public readonly StargateContext _context;

        public GetPeopleHandler(StargateContext context)
        {
            _context = context;
        }

        public async Task<GetPeopleResult> Handle(GetPeople request, CancellationToken cancellationToken)
        {
            var result = new GetPeopleResult();

            // Removed the hard coded SQl string, risk of SQL injection
            //var query = $"SELECT a.Id as PersonId, a.Name, b.CurrentRank, b.CurrentDutyTitle, b.CareerStartDate, b.CareerEndDate FROM [Person] a LEFT JOIN [AstronautDetail] b on b.PersonId = a.Id";

            var people = await _context.People
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

            result.People = people;

            return result;
        }
    }
}