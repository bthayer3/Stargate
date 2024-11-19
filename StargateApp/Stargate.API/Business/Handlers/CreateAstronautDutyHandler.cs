using MediatR;
using Microsoft.EntityFrameworkCore;
using StargateAPI.Business.Commands;
using StargateAPI.Business.Data;
using StargateAPI.Business.Dtos;
using StargateAPI.Business.Enums;
using StargateAPI.Business.Results;

namespace StargateAPI.Business.Handlers
{
    public class CreateAstronautDutyHandler : IRequestHandler<CreateAstronautDuty, CreateAstronautDutyResult>
    {
        private readonly StargateContext _context;

        public CreateAstronautDutyHandler(StargateContext context)
        {
            _context = context;
        }
        public async Task<CreateAstronautDutyResult> Handle(CreateAstronautDuty request, CancellationToken cancellationToken)
        {
            //Todo, combine the 2 individual selects below into 1 request, could use the person handle GetPersonByName

            //Major addition, wrapped this all in a transaction scope due to multiple database changes happening

            using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                var person = await _context.People.Where(p => p.Name.ToLower() == request.Name.ToLower()).FirstOrDefaultAsync(cancellationToken);
                var astronautDetail = await _context.AstronautDetails.Where(z => z.PersonId == person.Id).FirstOrDefaultAsync(cancellationToken);

                var specifiedRank = Enum.Parse<Rank>(request.Rank);
                var specifiedDutyTitle = Enum.Parse<DutyTitle>(request.DutyTitle);

                if (astronautDetail is null)
                {
                    //Current person does not have an astronaut detail

                    astronautDetail = new AstronautDetail  //Simplified object setup, removed redundancy
                    {
                        PersonId = person.Id,
                        CurrentRank = specifiedRank,
                        CurrentDutyTitle = specifiedDutyTitle,
                        CareerStartDate = request.DutyStartDate.Date   //Career is starting, only set start date here
                    };

                    if (specifiedDutyTitle == DutyTitle.Retired)
                    {
                        astronautDetail.CareerEndDate = request.DutyStartDate.Date;
                    }

                    await _context.AstronautDetails.AddAsync(astronautDetail, cancellationToken);
                }
                else
                {
                    astronautDetail.CurrentRank = specifiedRank;
                    astronautDetail.CurrentDutyTitle = specifiedDutyTitle;

                    if (specifiedDutyTitle == DutyTitle.Retired)
                    {
                        astronautDetail.CareerEndDate = request.DutyStartDate.AddDays(-1).Date;
                    }

                    _context.AstronautDetails.Update(astronautDetail);
                }

                var lastAstronautDuty = await _context.AstronautDuties
                    .Where(ad => ad.PersonId == person.Id)
                    .OrderByDescending(ad => ad.DutyStartDate)
                    .FirstOrDefaultAsync(cancellationToken);

                if (lastAstronautDuty != null)
                {
                    //Has a previous astronaut duty
                    lastAstronautDuty.DutyEndDate = request.DutyStartDate.AddDays(-1).Date;
                    _context.AstronautDuties.Update(lastAstronautDuty);
                }

                var newAstronautDuty = new AstronautDuty()
                {
                    PersonId = person.Id,
                    Rank = specifiedRank,
                    DutyTitle = specifiedDutyTitle,
                    DutyStartDate = request.DutyStartDate.Date,
                    DutyEndDate = null
                };

                await _context.AstronautDuties.AddAsync(newAstronautDuty, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);  //Fixed not passing cancellationToken in
                await transaction.CommitAsync(cancellationToken);

                return new CreateAstronautDutyResult()
                {
                    Id = newAstronautDuty.Id,
                    ResponseCode = 201
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw new Exception("An internal server error occurred while processing the request.");
            }
        }
    }
}