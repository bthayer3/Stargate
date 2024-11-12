using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using StargateAPI.Business.Data;
using StargateAPI.Business.Dtos;

namespace StargateTests
{
    public class TestWebApplicationFactory : WebApplicationFactory<Program>
    {
        private readonly string _dbName = Guid.NewGuid().ToString();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((context, config) =>
            {
                // Override environment variable for tests
                var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (string.IsNullOrEmpty(environment))
                {
                    Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");
                }
            }).ConfigureServices(services =>
            {
                // Remove the existing DbContext registration
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<StargateContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Register DbContext with a unique in-memory database
                services.AddDbContext<StargateContext>(options =>
                {
                    options.UseInMemoryDatabase(_dbName);
                    options.ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                });

                // Build the service provider and seed the database
                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<StargateContext>();
                dbContext.Database.EnsureCreated();
                SeedTestData(dbContext);
            });
        }

        private static void SeedTestData(StargateContext dbContext)
        {
            dbContext.People.AddRange(new Person
            {
                Id = 1,
                Name = "Mark Pooler"
            }, new Person
            {
                Id = 2,
                Name = "Grace Johnson"
            }, new Person
            {
                Id = 3,
                Name = "Jake Lee"
            }, new Person
            {
                Id = 4,
                Name = "Robert Bush"
            });

            dbContext.AstronautDetails.AddRange(new AstronautDetail
            {
                Id = 1,
                PersonId = 2,
                CurrentRank = StargateAPI.Business.Enums.Rank.PVT,
                CurrentDutyTitle = StargateAPI.Business.Enums.DutyTitle.MissionSpecialist,
                CareerStartDate = new DateTime(2024, 1, 1),
                CareerEndDate = null
            }, new AstronautDetail
            {
                Id = 2,
                PersonId = 3,
                CurrentRank = StargateAPI.Business.Enums.Rank.SPC,
                CurrentDutyTitle = StargateAPI.Business.Enums.DutyTitle.Commander,
                CareerStartDate = new DateTime(2022, 3, 15),
                CareerEndDate = null
            }, new AstronautDetail
            {
                Id = 3,
                PersonId = 4,
                CurrentRank = StargateAPI.Business.Enums.Rank.GOA,
                CurrentDutyTitle = StargateAPI.Business.Enums.DutyTitle.Retired,
                CareerStartDate = new DateTime(2022, 9, 19),
                CareerEndDate = new DateTime(2024, 2, 13),
            });

            dbContext.AstronautDuties.AddRange(new AstronautDuty
            {
                Id = 1,
                PersonId = 2,
                Rank = StargateAPI.Business.Enums.Rank.PVT,
                DutyTitle = StargateAPI.Business.Enums.DutyTitle.MissionSpecialist,
                DutyStartDate = new DateTime(2024, 1, 1),
                DutyEndDate = null
            }, new AstronautDuty
            {
                Id = 2,
                PersonId = 3,
                Rank = StargateAPI.Business.Enums.Rank.PVT,
                DutyTitle = StargateAPI.Business.Enums.DutyTitle.FlightEngineer,
                DutyStartDate = new DateTime(2022, 3, 15),
                DutyEndDate = new DateTime(2022, 8, 10)
            }, new AstronautDuty
            {
                Id = 3,
                PersonId = 3,
                Rank = StargateAPI.Business.Enums.Rank.PFC,
                DutyTitle = StargateAPI.Business.Enums.DutyTitle.LunarModulePilot,
                DutyStartDate = new DateTime(2022, 8, 11),
                DutyEndDate = new DateTime(2023, 1, 20)
            }, new AstronautDuty
            {
                Id = 4,
                PersonId = 3,
                Rank = StargateAPI.Business.Enums.Rank.SPC,
                DutyTitle = StargateAPI.Business.Enums.DutyTitle.Commander,
                DutyStartDate = new DateTime(2023, 1, 21),
                DutyEndDate = null
            }, new AstronautDuty
            {
                Id = 5,
                PersonId = 4,
                Rank = StargateAPI.Business.Enums.Rank.PFC,
                DutyTitle = StargateAPI.Business.Enums.DutyTitle.LunarModulePilot,
                DutyStartDate = new DateTime(2022, 9, 19),
                DutyEndDate = new DateTime(2023, 3, 12)
            }, new AstronautDuty
            {
                Id = 6,
                PersonId = 4,
                Rank = StargateAPI.Business.Enums.Rank.SPC,
                DutyTitle = StargateAPI.Business.Enums.DutyTitle.Commander,
                DutyStartDate = new DateTime(2023, 3, 13),
                DutyEndDate = new DateTime(2024, 2, 13),
            }, new AstronautDuty
            {
                Id = 7,
                PersonId = 4,
                Rank = StargateAPI.Business.Enums.Rank.SPC,
                DutyTitle = StargateAPI.Business.Enums.DutyTitle.Retired,
                DutyStartDate = new DateTime(2024, 2, 14),
                DutyEndDate = null
            });

            dbContext.SaveChanges();







            // Check if any data already exists, to avoid duplicates
            //if (!dbContext.People.Any())
            //{
            //    dbContext.People.AddRange(new Person
            //    {
            //        Id = 1,
            //        Name = "Mark Pooler"
            //    }, new Person
            //    {
            //        Id = 2,
            //        Name = "Grace Johnson"
            //    }, new Person
            //    {
            //        Id = 3,
            //        Name = "Jake Lee"
            //    }, new Person
            //    {
            //        Id = 4,
            //        Name = "Robert Bush"
            //    });

            //    dbContext.SaveChanges();
            //}


            //if (!dbContext.AstronautDetails.Any())
            //{
            //    dbContext.AstronautDetails.AddRange(new AstronautDetail
            //    {
            //        Id = 1,
            //        PersonId = 2,
            //        CurrentRank = StargateAPI.Business.Enums.Rank.PVT,
            //        CurrentDutyTitle = StargateAPI.Business.Enums.DutyTitle.MissionSpecialist,
            //        CareerStartDate = new DateTime(2024, 1, 1),
            //        CareerEndDate = null
            //    }, new AstronautDetail
            //    {
            //        Id = 2,
            //        PersonId = 3,
            //        CurrentRank = StargateAPI.Business.Enums.Rank.SPC,
            //        CurrentDutyTitle = StargateAPI.Business.Enums.DutyTitle.Commander,
            //        CareerStartDate = new DateTime(2022, 3, 15),
            //        CareerEndDate = null
            //    }, new AstronautDetail
            //    {
            //        Id = 3,
            //        PersonId = 4,
            //        CurrentRank = StargateAPI.Business.Enums.Rank.GOA,
            //        CurrentDutyTitle = StargateAPI.Business.Enums.DutyTitle.Retired,
            //        CareerStartDate = new DateTime(2022, 9, 19),
            //        CareerEndDate = new DateTime(2024, 2, 13),
            //    });

            //    dbContext.SaveChanges();
            //}


            //if (!dbContext.AstronautDuties.Any())
            //{
            //    dbContext.AstronautDuties.AddRange(new AstronautDuty
            //    {
            //        Id = 1,
            //        PersonId = 2,
            //        Rank = StargateAPI.Business.Enums.Rank.PVT,
            //        DutyTitle = StargateAPI.Business.Enums.DutyTitle.MissionSpecialist,
            //        DutyStartDate = new DateTime(2024, 1, 1),
            //        DutyEndDate = null
            //    }, new AstronautDuty
            //    {
            //        Id = 2,
            //        PersonId = 3,
            //        Rank = StargateAPI.Business.Enums.Rank.PVT,
            //        DutyTitle = StargateAPI.Business.Enums.DutyTitle.FlightEngineer,
            //        DutyStartDate = new DateTime(2022, 3, 15),
            //        DutyEndDate = new DateTime(2022, 8, 10)
            //    }, new AstronautDuty
            //    {
            //        Id = 3,
            //        PersonId = 3,
            //        Rank = StargateAPI.Business.Enums.Rank.PFC,
            //        DutyTitle = StargateAPI.Business.Enums.DutyTitle.LunarModulePilot,
            //        DutyStartDate = new DateTime(2022, 8, 11),
            //        DutyEndDate = new DateTime(2023, 1, 20)
            //    }, new AstronautDuty
            //    {
            //        Id = 4,
            //        PersonId = 3,
            //        Rank = StargateAPI.Business.Enums.Rank.SPC,
            //        DutyTitle = StargateAPI.Business.Enums.DutyTitle.Commander,
            //        DutyStartDate = new DateTime(2023, 1, 21),
            //        DutyEndDate = null
            //    }, new AstronautDuty
            //    {
            //        Id = 5,
            //        PersonId = 4,
            //        Rank = StargateAPI.Business.Enums.Rank.PFC,
            //        DutyTitle = StargateAPI.Business.Enums.DutyTitle.LunarModulePilot,
            //        DutyStartDate = new DateTime(2022, 9, 19),
            //        DutyEndDate = new DateTime(2023, 3, 12)
            //    }, new AstronautDuty
            //    {
            //        Id = 6,
            //        PersonId = 4,
            //        Rank = StargateAPI.Business.Enums.Rank.SPC,
            //        DutyTitle = StargateAPI.Business.Enums.DutyTitle.Commander,
            //        DutyStartDate = new DateTime(2023, 3, 13),
            //        DutyEndDate = new DateTime(2024, 2, 13),
            //    }, new AstronautDuty
            //    {
            //        Id = 7,
            //        PersonId = 4,
            //        Rank = StargateAPI.Business.Enums.Rank.SPC,
            //        DutyTitle = StargateAPI.Business.Enums.DutyTitle.Retired,
            //        DutyStartDate = new DateTime(2024, 2, 14),
            //        DutyEndDate = null
            //    });

            //    dbContext.SaveChanges();
            //}
        }
    }
}
